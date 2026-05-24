using AsuFit.Entidades;
using AsuFit.Negocio;
using AsuFit.Datos;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmPuntoVenta : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR

        // Almacena el usuario que está realizando la venta
        private Usuario usuarioActual;
        // Capa de negocio para consultar la base de datos
        private InventarioNegocio negocio = new InventarioNegocio();
        // Tabla en memoria con todos los productos disponibles
        private DataTable dtCatalogo;

        // --- Librería de Windows para poner el "Placeholder" en el buscador ---
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmPuntoVenta(Usuario userLogueDado)
        {
            InitializeComponent();
            usuarioActual = userLogueDado;
            dgvCarrito.AutoGenerateColumns = false;

            // 1. Evitamos parpadeos al dibujar los controles
            this.DoubleBuffered = true;
            // 2. Iniciamos el formulario invisible para ocultar la carga de tarjetas
            this.Opacity = 0;
            // 3. Programamos que aparezca de golpe cuando esté listo
            this.Shown += new EventHandler(frmPuntoVenta_Shown);
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS

        private void frmPuntoVenta_Load(object sender, EventArgs e)
        {
            // Congelamos visualmente el panel de tarjetas para que no se vea cómo se arman
            flpCatalogo.SuspendLayout();

            ConfigurarCarrito();
            ConfigurarFiltros();

            // Traemos el inventario de la Base de Datos
            dtCatalogo = negocio.ListarProductos();

            // Creamos una columna "invisible" para poder buscar productos sin importar las tildes
            dtCatalogo.Columns.Add("NombreBusqueda", typeof(string));
            foreach (DataRow row in dtCatalogo.Rows)
            {
                string nombreOriginal = row["Nombre"].ToString();
                row["NombreBusqueda"] = QuitarAcentos(nombreOriginal);
            }

            ConfigurarAutocompletado();

            // Forzamos la primera carga de tarjetas aplicando los filtros base
            AplicarFiltros();

            // Ponemos el texto de guía en la caja de búsqueda
            SendMessage(txtBuscarProducto.Handle, EM_SETCUEBANNER, 1, "Buscar Bebidas, Snacks o Suplementos...");

            // Descongelamos el panel
            flpCatalogo.ResumeLayout(false);
        }

        private void frmPuntoVenta_Shown(object sender, EventArgs e)
        {
            // Cuando la ventana terminó de procesar todo, la mostramos al 100%
            this.Opacity = 1;
        }

        private void ConfigurarCarrito()
        {
            // Ajustes visuales de la tabla del carrito
            dgvCarrito.AllowUserToAddRows = false;
            dgvCarrito.RowHeadersVisible = false;
            dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCarrito.ReadOnly = true;
            dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Evitamos que los eventos de clic se registren dos veces por error
            dgvCarrito.CellContentClick -= dgvCarrito_CellContentClick;
            dgvCarrito.CellContentDoubleClick -= dgvCarrito_CellContentClick;
            dgvCarrito.CellContentClick += dgvCarrito_CellContentClick;
            dgvCarrito.CellContentDoubleClick += dgvCarrito_CellContentClick;
        }

        private void ConfigurarFiltros()
        {
            // Si hay categorías cargadas, seleccionamos la primera ("Todas")
            if (cmbFiltroCategoria.Items.Count > 0) cmbFiltroCategoria.SelectedIndex = 0;
            if (cmbOrdenar.Items.Count > 0) cmbOrdenar.SelectedIndex = 0;

            // Conectamos el cambio de filtro para que recargue las tarjetas automáticamente
            cmbFiltroCategoria.SelectedIndexChanged += CombosFiltro_SelectedIndexChanged;
            cmbOrdenar.SelectedIndexChanged += CombosFiltro_SelectedIndexChanged;
        }
        #endregion

        #region 3. SECCIÓN SUPERIOR: BÚSQUEDA Y FILTROS

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            // Busca mientras el usuario va escribiendo
            AplicarFiltros();
        }

        private void CombosFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        // --- MOTOR CENTRAL DE FILTRADO ---
        private void AplicarFiltros()
        {
            if (dtCatalogo == null) return;
            string filtro = "";

            // 1. Filtro de Texto (Buscador)
            if (!string.IsNullOrWhiteSpace(txtBuscarProducto.Text))
            {
                string textoLimpio = QuitarAcentos(txtBuscarProducto.Text);
                filtro = "NombreBusqueda LIKE '%" + textoLimpio + "%'";
            }

            // 2. Filtro de Categoría
            if (cmbFiltroCategoria.Text != "Todas")
            {
                if (filtro.Length > 0) filtro += " AND ";
                filtro += "Categoria = '" + cmbFiltroCategoria.Text + "'";
            }

            // 3. Filtro de Reglas (Solo productos con stock mayor a cero y activos)
            if (filtro.Length > 0) filtro += " AND ";
            filtro += "StockActual > 0 AND Estado = 'Activo'";

            // 4. Ordenamiento
            string orden = "Nombre ASC";
            if (cmbOrdenar.Text == "Nombre (Z-A)") orden = "Nombre DESC";
            else if (cmbOrdenar.Text == "Precio (Menor a Mayor)") orden = "PrecioVenta ASC";
            else if (cmbOrdenar.Text == "Precio (Mayor a Menor)") orden = "PrecioVenta DESC";

            // Mandamos a dibujar las tarjetas con estos filtros
            GenerarTarjetas(filtro, orden);
        }

        // Configura el menú desplegable al escribir en el buscador
        private void ConfigurarAutocompletado()
        {
            AutoCompleteStringCollection listaSugerencias = new AutoCompleteStringCollection();

            foreach (DataRow row in dtCatalogo.Rows)
            {
                string nombreOriginal = row["Nombre"].ToString();
                string nombreSinAcento = row["NombreBusqueda"].ToString();

                if (!listaSugerencias.Contains(nombreOriginal)) listaSugerencias.Add(nombreOriginal);
                if (!listaSugerencias.Contains(nombreSinAcento)) listaSugerencias.Add(nombreSinAcento);

                // Agrega palabras sueltas mayores a 2 letras (ej: "Proteina")
                string[] palabras = nombreSinAcento.Split(' ');
                foreach (string palabra in palabras)
                {
                    if (palabra.Length > 2 && !listaSugerencias.Contains(palabra))
                    {
                        listaSugerencias.Add(palabra);
                    }
                }
            }

            txtBuscarProducto.AutoCompleteCustomSource = listaSugerencias;
            txtBuscarProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtBuscarProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        // Utilidad para limpiar textos (Ej: "BCAA's" a "BCAAs")
        private string QuitarAcentos(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return texto;

            string conAcentos = "áéíóúÁÉÍÓÚ";
            string sinAcentos = "aeiouAEIOU";

            for (int i = 0; i < conAcentos.Length; i++)
            {
                texto = texto.Replace(conAcentos[i], sinAcentos[i]);
            }
            return texto;
        }
        #endregion

        #region 4. SECCIÓN IZQUIERDA: CATÁLOGO DE PRODUCTOS (TARJETAS)

        // --- DIBUJA LOS PRODUCTOS EN PANTALLA ---
        private void GenerarTarjetas(string filtro, string orden)
        {
            flpCatalogo.SuspendLayout();
            flpCatalogo.Controls.Clear();

            // Aplicamos los filtros a la tabla en memoria
            DataView vistaFiltrada = dtCatalogo.DefaultView;
            vistaFiltrada.RowFilter = filtro;
            vistaFiltrada.Sort = orden;

            foreach (DataRowView rowView in vistaFiltrada)
            {
                DataRow row = rowView.Row;

                // 1. Armamos el recuadro principal (Tarjeta)
                Panel pnlCard = new Panel();
                pnlCard.Size = new Size(180, 240);
                pnlCard.BackColor = Color.FromArgb(45, 45, 48); // Gris oscuro premium
                pnlCard.Margin = new Padding(10);

                // 2. Colocamos la foto
                PictureBox pic = new PictureBox();
                pic.Size = new Size(140, 110);
                pic.Location = new Point(20, 15);
                pic.BackColor = Color.WhiteSmoke;
                pic.SizeMode = PictureBoxSizeMode.StretchImage;

                string codigo = row["CodigoBarras"].ToString();
                string rutaFoto = @"C:\AsuFit_Fotos\" + codigo + ".jpg";

                if (System.IO.File.Exists(rutaFoto))
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(rutaFoto, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        pic.Image = Image.FromStream(fs);
                    }
                }

                // 3. Nombre del producto
                Label lblNombre = new Label();
                lblNombre.Text = row["Nombre"].ToString();
                lblNombre.ForeColor = Color.White;
                lblNombre.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                lblNombre.Location = new Point(10, 135);
                lblNombre.AutoSize = false;
                lblNombre.Size = new Size(160, 35);

                // 4. Precio
                Label lblPrecio = new Label();
                lblPrecio.Text = "Gs. " + Convert.ToDecimal(row["PrecioVenta"]).ToString("N0");
                lblPrecio.ForeColor = Color.White;
                lblPrecio.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                lblPrecio.Location = new Point(10, 170);
                lblPrecio.AutoSize = true;

                // 5. Stock disponible
                Label lblStock = new Label();
                lblStock.Text = "Stock: " + row["StockActual"].ToString();
                lblStock.ForeColor = Color.LightGray;
                lblStock.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                lblStock.Location = new Point(115, 175);
                lblStock.AutoSize = true;

                // 6. Botón de agregar
                Button btnAgregar = new Button();
                btnAgregar.Text = "AGREGAR AL CARRITO";
                btnAgregar.BackColor = Color.FromArgb(0, 229, 255); // Color Cian AsuFit
                btnAgregar.ForeColor = Color.FromArgb(25, 28, 35); // Texto Oscuro
                btnAgregar.FlatStyle = FlatStyle.Flat;
                btnAgregar.FlatAppearance.BorderSize = 0;
                btnAgregar.Location = new Point(10, 200);
                btnAgregar.Size = new Size(160, 30);
                btnAgregar.Cursor = Cursors.Hand;
                btnAgregar.Tag = row["IdProducto"]; // Guardamos el ID secreto en el botón
                btnAgregar.Click += BtnAgregar_Click;
                btnAgregar.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                // Unimos todas las piezas a la tarjeta
                pnlCard.Controls.Add(pic);
                pnlCard.Controls.Add(lblNombre);
                pnlCard.Controls.Add(lblPrecio);
                pnlCard.Controls.Add(lblStock);
                pnlCard.Controls.Add(btnAgregar);

                // Escalamos la tarjeta (como pediste en tu diseño)
                pnlCard.Scale(new SizeF(1.4f, 1.4f));

                // Metemos la tarjeta terminada en el panel gigante
                flpCatalogo.Controls.Add(pnlCard);
            }

            flpCatalogo.ResumeLayout();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            // Extraemos el ID del producto desde el botón que fue clickeado
            Button btnClick = (Button)sender;
            int idProducto = Convert.ToInt32(btnClick.Tag);

            // Buscamos los datos del producto en el catálogo en memoria
            DataRow[] filaProducto = dtCatalogo.Select("IdProducto = " + idProducto);

            if (filaProducto.Length > 0)
            {
                string nombre = filaProducto[0]["Nombre"].ToString();
                decimal precio = Convert.ToDecimal(filaProducto[0]["PrecioVenta"]);
                string codigoBarras = filaProducto[0]["CodigoBarras"].ToString();
                int iva = filaProducto[0]["PorcentajeIva"] != DBNull.Value ? Convert.ToInt32(filaProducto[0]["PorcentajeIva"]) : 10;

                bool existeEnCarrito = false;

                // Revisamos si el producto ya está en el carrito para solo sumarle +1
                foreach (DataGridViewRow row in dgvCarrito.Rows)
                {
                    if (Convert.ToInt32(row.Cells["colCarritoId"].Value) == idProducto)
                    {
                        int nuevaCantidad = Convert.ToInt32(row.Cells["colCarritoCantidad"].Value) + 1;
                        row.Cells["colCarritoCantidad"].Value = nuevaCantidad;
                        row.Cells["colCarritoSubtotal"].Value = nuevaCantidad * precio;
                        existeEnCarrito = true;
                        break;
                    }
                }

                // Si no existía, creamos una nueva fila
                if (!existeEnCarrito)
                {
                    int rowIndex = dgvCarrito.Rows.Add();
                    DataGridViewRow nuevaFila = dgvCarrito.Rows[rowIndex];

                    nuevaFila.Cells["colCarritoId"].Value = idProducto;
                    nuevaFila.Cells["colCarritoCodigo"].Value = codigoBarras;
                    nuevaFila.Cells["colCarritoNombre"].Value = nombre;
                    nuevaFila.Cells["colCarritoCantidad"].Value = 1;
                    nuevaFila.Cells["colCarritoPrecio"].Value = precio;
                    nuevaFila.Cells["colCarritoSubtotal"].Value = precio;
                    nuevaFila.Cells["colCarritoIva"].Value = iva;
                }

                ActualizarTotal();
            }
        }
        #endregion

        #region 5. SECCIÓN DERECHA: CARRITO DE COMPRAS Y TABLA

        // --- MANEJO DE LOS BOTONCITOS +, - Y X DENTRO DE LA TABLA ---
        private void dgvCarrito_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string nombreColumna = dgvCarrito.Columns[e.ColumnIndex].Name;

                int cantidadActual = Convert.ToInt32(dgvCarrito.Rows[e.RowIndex].Cells["colCarritoCantidad"].Value);
                decimal precioUnitario = Convert.ToDecimal(dgvCarrito.Rows[e.RowIndex].Cells["colCarritoPrecio"].Value);

                if (nombreColumna == "colCarritoSumar")
                {
                    cantidadActual++;
                    dgvCarrito.Rows[e.RowIndex].Cells["colCarritoCantidad"].Value = cantidadActual;
                    dgvCarrito.Rows[e.RowIndex].Cells["colCarritoSubtotal"].Value = cantidadActual * precioUnitario;
                    ActualizarTotal();
                }
                else if (nombreColumna == "colCarritoRestar")
                {
                    if (cantidadActual > 1)
                    {
                        cantidadActual--;
                        dgvCarrito.Rows[e.RowIndex].Cells["colCarritoCantidad"].Value = cantidadActual;
                        dgvCarrito.Rows[e.RowIndex].Cells["colCarritoSubtotal"].Value = cantidadActual * precioUnitario;
                        ActualizarTotal();
                    }
                    else
                    {
                        // Si era 1 y le resta, lo elimina del carrito
                        dgvCarrito.Rows.RemoveAt(e.RowIndex);
                        ActualizarTotal();
                    }
                }
                else if (nombreColumna == "colCarritoEliminar")
                {
                    dgvCarrito.Rows.RemoveAt(e.RowIndex);
                    ActualizarTotal();
                }
            }
        }

        private void ActualizarTotal()
        {
            decimal totalPagar = 0;

            foreach (DataGridViewRow row in dgvCarrito.Rows)
            {
                totalPagar += Convert.ToDecimal(row.Cells["colCarritoSubtotal"].Value);
            }
            lblTotalPagar.Text = "Gs. " + totalPagar.ToString("N0");
        }

        // Quita la selección azul fea que aparece al hacer clic en una celda
        private void dgvCarrito_SelectionChanged(object sender, EventArgs e)
        {
            dgvCarrito.ClearSelection();
        }
        #endregion

        #region 6. ACCIONES FINALES: LIMPIAR Y VENDER

        // Vacía la tabla visual del carrito
        public void LimpiarGrillaVisual()
        {
            dgvCarrito.Rows.Clear();
            ActualizarTotal();
        }

        private void btnLimpiarCarrito_Click(object sender, EventArgs e)
        {
            LimpiarGrillaVisual();

            // Limpia los datos de la memoria global
            for (int i = CarritoGlobal.Detalles.Rows.Count - 1; i >= 0; i--)
            {
                if (Convert.ToInt32(CarritoGlobal.Detalles.Rows[i]["IdProducto"]) > 0)
                {
                    CarritoGlobal.Detalles.Rows.RemoveAt(i);
                }
            }

            // Si la caja de cobro estaba abierta, le avisa que se limpió el carrito
            frmCajaCobro cajaAbierta = Application.OpenForms["frmCajaCobro"] as frmCajaCobro;
            if (cajaAbierta != null) cajaAbierta.ActualizarPantallaDesdeCarrito();
        }

        private void btnFinalizarVenta_Click(object sender, EventArgs e)
        {
            if (dgvCarrito.Rows.Count == 0 && CarritoGlobal.Detalles.Rows.Count == 0)
            {
                MessageBox.Show("El carrito está vacío. Agregue productos antes de continuar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Limpiamos el carrito global viejo antes de mandar la nueva tanda
                for (int i = CarritoGlobal.Detalles.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(CarritoGlobal.Detalles.Rows[i]["IdProducto"]) > 0)
                    {
                        CarritoGlobal.Detalles.Rows.RemoveAt(i);
                    }
                }

                // Transferimos todo lo de la grilla visual al carrito global (memoria)
                foreach (DataGridViewRow row in dgvCarrito.Rows)
                {
                    int idProd = Convert.ToInt32(row.Cells["colCarritoId"].Value);
                    string codigoDeBarras = row.Cells["colCarritoCodigo"].Value.ToString();
                    string concepto = row.Cells["colCarritoNombre"].Value.ToString();
                    int cant = Convert.ToInt32(row.Cells["colCarritoCantidad"].Value);
                    decimal precio = Convert.ToDecimal(row.Cells["colCarritoPrecio"].Value);
                    int iva = Convert.ToInt32(row.Cells["colCarritoIva"].Value);

                    CarritoGlobal.AgregarItem(idProd, codigoDeBarras, concepto, cant, precio, iva);
                }

                // Abrimos la ventana de cobro (o la traemos al frente si ya estaba abierta)
                frmCajaCobro cajaAbierta = Application.OpenForms["frmCajaCobro"] as frmCajaCobro;

                if (cajaAbierta != null)
                {
                    cajaAbierta.WindowState = FormWindowState.Normal;
                    cajaAbierta.BringToFront();
                    cajaAbierta.ActualizarPantallaDesdeCarrito();
                }
                else
                {
                    frmCajaCobro nuevaCaja = new frmCajaCobro(usuarioActual);
                    nuevaCaja.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar productos a la caja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}