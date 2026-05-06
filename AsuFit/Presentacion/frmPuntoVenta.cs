using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmPuntoVenta : Form
    {
        // 1. Declaramos la variable global para este formulario
        private Usuario usuarioActual;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmPuntoVenta(Usuario userLogueDado)
        {
            InitializeComponent();
            usuarioActual = userLogueDado;
            SendMessage(txtBuscarProducto.Handle, EM_SETCUEBANNER, 1, "Buscar producto...");
        }

        private InventarioNegocio negocio = new InventarioNegocio();
        private DataTable dtCatalogo;

        private void frmPuntoVenta_Load(object sender, EventArgs e)
        {
            ConfigurarCarrito();
            ConfigurarFiltros();

            // Traemos TODOS los productos una sola vez a la memoria
            dtCatalogo = negocio.ListarProductos();

            // Agregamos una columna invisible y le quitamos los acentos a los nombres
            dtCatalogo.Columns.Add("NombreBusqueda", typeof(string));
            foreach (DataRow row in dtCatalogo.Rows)
            {
                string nombreOriginal = row["Nombre"].ToString();
                row["NombreBusqueda"] = QuitarAcentos(nombreOriginal);
            }

            // LLAMAMOS A NUESTRA NUEVA FUNCIÓN AQUÍ
            ConfigurarAutocompletado();

            // Generamos las tarjetas mostrando "Todas" las categorías y ordenadas de la A a la Z
            GenerarTarjetas("", "Nombre ASC");
        }

        // --- 1. CONFIGURACIÓN DE TABLA Y FILTROS ---

        private void ConfigurarCarrito()
        {
            dgvCarrito.Columns.Clear();
            dgvCarrito.Columns.Add("IdProducto", "ID");
            dgvCarrito.Columns.Add("Nombre", "Producto");
            dgvCarrito.Columns.Add("CodigoBarras", "Código");
            dgvCarrito.Columns["CodigoBarras"].Visible = false; // Lo mantenemos oculto en la grilla para que no ensucie el diseño

            // --- NUEVO: BOTÓN MENOS ---
            DataGridViewButtonColumn colMenos = new DataGridViewButtonColumn();
            colMenos.Name = "Restar";
            colMenos.HeaderText = "";
            colMenos.Text = "-";
            colMenos.UseColumnTextForButtonValue = true;
            colMenos.FlatStyle = FlatStyle.Flat;
            colMenos.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            colMenos.DefaultCellStyle.ForeColor = Color.White;
            dgvCarrito.Columns.Add(colMenos);

            dgvCarrito.Columns.Add("Cantidad", "Cant.");

            // --- NUEVO: BOTÓN MÁS ---
            DataGridViewButtonColumn colMas = new DataGridViewButtonColumn();
            colMas.Name = "Sumar";
            colMas.HeaderText = "";
            colMas.Text = "+";
            colMas.UseColumnTextForButtonValue = true;
            colMas.FlatStyle = FlatStyle.Flat;
            colMas.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            colMas.DefaultCellStyle.ForeColor = Color.White;
            dgvCarrito.Columns.Add(colMas);

            dgvCarrito.Columns.Add("Precio", "Precio U.");
            dgvCarrito.Columns.Add("Subtotal", "Subtotal");

            DataGridViewButtonColumn colEliminar = new DataGridViewButtonColumn();
            colEliminar.Name = "Eliminar";
            colEliminar.HeaderText = "";
            colEliminar.Text = "X";
            colEliminar.UseColumnTextForButtonValue = true;
            colEliminar.FlatStyle = FlatStyle.Flat;
            colEliminar.DefaultCellStyle.BackColor = Color.IndianRed;
            colEliminar.DefaultCellStyle.ForeColor = Color.White;
            dgvCarrito.Columns.Add(colEliminar);

            dgvCarrito.Columns["IdProducto"].Visible = false;
            dgvCarrito.AllowUserToAddRows = false;
            dgvCarrito.RowHeadersVisible = false;
            dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCarrito.ReadOnly = true;
            dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvCarrito.Columns["Precio"].DefaultCellStyle.Format = "N0";
            dgvCarrito.Columns["Subtotal"].DefaultCellStyle.Format = "N0";

            // Ajustamos los anchos para que se vea estético
            dgvCarrito.Columns["Nombre"].FillWeight = 180;
            dgvCarrito.Columns["Restar"].FillWeight = 30;
            dgvCarrito.Columns["Cantidad"].FillWeight = 50;
            dgvCarrito.Columns["Sumar"].FillWeight = 30;
            dgvCarrito.Columns["Precio"].FillWeight = 90;
            dgvCarrito.Columns["Subtotal"].FillWeight = 90;
            dgvCarrito.Columns["Eliminar"].FillWeight = 40;

            // Alineación profesional de los textos
            dgvCarrito.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCarrito.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvCarrito.CellContentClick += dgvCarrito_CellContentClick;
            dgvCarrito.CellContentDoubleClick += dgvCarrito_CellContentClick;

        }

        private void ConfigurarFiltros()
        {
            // Opciones para la categoría
            cmbFiltroCategoria.Items.Add("Todas");
            cmbFiltroCategoria.Items.Add("Suplementos");
            cmbFiltroCategoria.Items.Add("Bebidas");
            cmbFiltroCategoria.Items.Add("Snacks");
            cmbFiltroCategoria.SelectedIndex = 0;

            // Opciones para ordenar
            cmbOrdenar.Items.Add("Nombre (A-Z)");
            cmbOrdenar.Items.Add("Nombre (Z-A)");
            cmbOrdenar.Items.Add("Precio (Menor a Mayor)");
            cmbOrdenar.Items.Add("Precio (Mayor a Menor)");
            cmbOrdenar.SelectedIndex = 0;

            // Le decimos qué hacer cuando el usuario cambie de opción
            cmbFiltroCategoria.SelectedIndexChanged += CombosFiltro_SelectedIndexChanged;
            cmbOrdenar.SelectedIndexChanged += CombosFiltro_SelectedIndexChanged;
        }

        // --- 2. LÓGICA DE FILTRADO (Buscador + Categoría + Orden) ---

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void CombosFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        // Método para quitar tildes a los textos
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

        // Método para cargar las sugerencias tipo Google en el buscador
        private void ConfigurarAutocompletado()
        {
            AutoCompleteStringCollection listaSugerencias = new AutoCompleteStringCollection();

            foreach (DataRow row in dtCatalogo.Rows)
            {
                string nombreOriginal = row["Nombre"].ToString();
                string nombreSinAcento = row["NombreBusqueda"].ToString();

                // 1. Agregamos la frase completa (Ej: "Batido de Proteina Listo 330ml")
                if (!listaSugerencias.Contains(nombreOriginal))
                    listaSugerencias.Add(nombreOriginal);

                if (!listaSugerencias.Contains(nombreSinAcento))
                    listaSugerencias.Add(nombreSinAcento);

                // 2. EL TRUCO: Dividimos la frase por los espacios
                string[] palabras = nombreSinAcento.Split(' ');

                foreach (string palabra in palabras)
                {
                    // Solo agregamos palabras de más de 2 letras para no llenar la lista con "de", "en", "el"
                    if (palabra.Length > 2 && !listaSugerencias.Contains(palabra))
                    {
                        listaSugerencias.Add(palabra);
                        // Esto agregará "Proteina", "Batido", "Listo", etc.
                    }
                }
            }

            txtBuscarProducto.AutoCompleteCustomSource = listaSugerencias;
            txtBuscarProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtBuscarProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void AplicarFiltros()
        {
            if (dtCatalogo == null) return;

            string filtro = "";

            // 1. Filtramos por texto si escribieron algo
            if (!string.IsNullOrWhiteSpace(txtBuscarProducto.Text))
            {
                // Le quitamos los acentos a lo que el usuario escribió en el cuadro de búsqueda
                string textoLimpio = QuitarAcentos(txtBuscarProducto.Text);

                // Buscamos en nuestra columna invisible (que tampoco tiene acentos)
                filtro = "NombreBusqueda LIKE '%" + textoLimpio + "%'";
            }

            // 2. Filtramos por categoría
            if (cmbFiltroCategoria.Text != "Todas")
            {
                if (filtro.Length > 0)
                {
                    filtro += " AND ";
                }
                filtro += "Categoria = '" + cmbFiltroCategoria.Text + "'";
            }

            // 3. Ordenamiento
            string orden = "Nombre ASC";

            if (cmbOrdenar.Text == "Nombre (Z-A)") orden = "Nombre DESC";
            else if (cmbOrdenar.Text == "Precio (Menor a Mayor)") orden = "PrecioVenta ASC";
            else if (cmbOrdenar.Text == "Precio (Mayor a Menor)") orden = "PrecioVenta DESC";

            GenerarTarjetas(filtro, orden);
        }

        // --- 3. GENERADOR DE TARJETAS DINÁMICO ---

        private void GenerarTarjetas(string filtro, string orden)
        {
            flpCatalogo.Controls.Clear();

            DataView vistaFiltrada = dtCatalogo.DefaultView;
            vistaFiltrada.RowFilter = filtro;
            vistaFiltrada.Sort = orden;

            foreach (DataRowView rowView in vistaFiltrada)
            {
                DataRow row = rowView.Row;

                Panel pnlCard = new Panel();
                pnlCard.Size = new Size(180, 240);
                pnlCard.BackColor = Color.FromArgb(45, 45, 48);
                pnlCard.Margin = new Padding(10);

                PictureBox pic = new PictureBox();
                pic.Size = new Size(140, 110);
                pic.Location = new Point(20, 15);
                pic.BackColor = Color.WhiteSmoke;
                pic.SizeMode = PictureBoxSizeMode.StretchImage;

                // --- MAGIA DE FOTOS: LEER LA FOTO DE LA CARPETA ---
                string codigo = row["CodigoBarras"].ToString();
                string rutaFoto = @"C:\AsuFit_Fotos\" + codigo + ".jpg";

                if (System.IO.File.Exists(rutaFoto))
                {
                    // Usamos FileStream para no bloquear la imagen en caso de que queramos editarla luego
                    using (System.IO.FileStream fs = new System.IO.FileStream(rutaFoto, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        pic.Image = Image.FromStream(fs);
                    }
                }
                else
                {
                    pic.Image = null; // Queda el fondo gris/blanco si no hay foto asignada
                }
                // ---------------------------------------------------

                Label lblNombre = new Label();
                lblNombre.Text = row["Nombre"].ToString();
                lblNombre.ForeColor = Color.White;

                lblNombre.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                lblNombre.Location = new Point(10, 135);
                lblNombre.AutoSize = false;
                lblNombre.Size = new Size(160, 35);

                Label lblPrecio = new Label();
                lblPrecio.Text = "Gs. " + Convert.ToDecimal(row["PrecioVenta"]).ToString("N0");
                lblPrecio.ForeColor = Color.White;

                lblPrecio.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                lblPrecio.Location = new Point(10, 170);
                lblPrecio.AutoSize = true;

                Label lblStock = new Label();

                lblStock.Text = "Stock: " + row["StockActual"].ToString();
                lblStock.ForeColor = Color.LightGray;
                lblStock.Font = new Font("Segoe UI", 9, FontStyle.Regular);

                lblStock.Location = new Point(115, 175);
                lblStock.AutoSize = true;

                Button btnAgregar = new Button();
                btnAgregar.Text = "AGREGAR AL CARRITO";

                btnAgregar.BackColor = Color.MediumSeaGreen;
                btnAgregar.ForeColor = Color.White;
                btnAgregar.FlatStyle = FlatStyle.Flat;
                btnAgregar.FlatAppearance.BorderSize = 0;
                btnAgregar.Location = new Point(10, 200);

                btnAgregar.Size = new Size(160, 30);
                btnAgregar.Cursor = Cursors.Hand;
                btnAgregar.Tag = row["IdProducto"];
                btnAgregar.Click += BtnAgregar_Click;

                pnlCard.Controls.Add(pic);
                pnlCard.Controls.Add(lblNombre);
                pnlCard.Controls.Add(lblPrecio);
                pnlCard.Controls.Add(lblStock);
                pnlCard.Controls.Add(btnAgregar);

                flpCatalogo.Controls.Add(pnlCard);
            }
        }

        // --- 4. LÓGICA DEL CARRITO ---

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            Button btnClick = (Button)sender;
            int idProducto = Convert.ToInt32(btnClick.Tag);

            DataRow[] filaProducto = dtCatalogo.Select("IdProducto = " + idProducto);

            if (filaProducto.Length > 0)
            {
                string nombre = filaProducto[0]["Nombre"].ToString();
                decimal precio = Convert.ToDecimal(filaProducto[0]["PrecioVenta"]);
                string codigoBarras = filaProducto[0]["CodigoBarras"].ToString();
                bool existeEnCarrito = false;

                foreach (DataGridViewRow row in dgvCarrito.Rows)
                {
                    if (Convert.ToInt32(row.Cells["IdProducto"].Value) == idProducto)
                    {
                        int nuevaCantidad = Convert.ToInt32(row.Cells["Cantidad"].Value) + 1;
                        row.Cells["Cantidad"].Value = nuevaCantidad;
                        row.Cells["Subtotal"].Value = nuevaCantidad * precio;
                        existeEnCarrito = true;
                        break;
                    }
                }

                if (!existeEnCarrito)
                {
                    // LA SOLUCIÓN: Creamos una fila vacía y le asignamos los valores por su NOMBRE exacto
                    int rowIndex = dgvCarrito.Rows.Add();
                    DataGridViewRow nuevaFila = dgvCarrito.Rows[rowIndex];

                    nuevaFila.Cells["IdProducto"].Value = idProducto;
                    nuevaFila.Cells["CodigoBarras"].Value = codigoBarras; // <-- LÍNEA NUEVA
                    nuevaFila.Cells["Nombre"].Value = nombre;
                    nuevaFila.Cells["Cantidad"].Value = 1;
                    nuevaFila.Cells["Precio"].Value = precio;
                    nuevaFila.Cells["Subtotal"].Value = precio;
                }

                ActualizarTotal();
            }
        }

        private void dgvCarrito_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificamos que no hayan hecho clic en los títulos de las columnas
            if (e.RowIndex >= 0)
            {
                string nombreColumna = dgvCarrito.Columns[e.ColumnIndex].Name;

                // Obtenemos los valores actuales de la fila
                int cantidadActual = Convert.ToInt32(dgvCarrito.Rows[e.RowIndex].Cells["Cantidad"].Value);
                decimal precioUnitario = Convert.ToDecimal(dgvCarrito.Rows[e.RowIndex].Cells["Precio"].Value);

                if (nombreColumna == "Sumar")
                {
                    cantidadActual++;
                    dgvCarrito.Rows[e.RowIndex].Cells["Cantidad"].Value = cantidadActual;
                    dgvCarrito.Rows[e.RowIndex].Cells["Subtotal"].Value = cantidadActual * precioUnitario;
                    ActualizarTotal();
                }
                else if (nombreColumna == "Restar")
                {
                    if (cantidadActual > 1)
                    {
                        cantidadActual--;
                        dgvCarrito.Rows[e.RowIndex].Cells["Cantidad"].Value = cantidadActual;
                        dgvCarrito.Rows[e.RowIndex].Cells["Subtotal"].Value = cantidadActual * precioUnitario;
                        ActualizarTotal();
                    }
                    else
                    {
                        // Si la cantidad es 1 y le da a restar, directamente eliminamos el producto del carrito
                        dgvCarrito.Rows.RemoveAt(e.RowIndex);
                        ActualizarTotal();
                    }
                }
                else if (nombreColumna == "Eliminar")
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
                totalPagar += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }
            lblTotalPagar.Text = "Gs. " + totalPagar.ToString("N0");
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
                // 1. Pasamos los productos de la grilla local a la Nube (CarritoGlobal)
                foreach (DataGridViewRow row in dgvCarrito.Rows)
                {
                    int idProd = Convert.ToInt32(row.Cells["IdProducto"].Value);
                    string codigoDeBarras = row.Cells["CodigoBarras"].Value.ToString(); // Atrapamos el código
                    string concepto = row.Cells["Nombre"].Value.ToString();
                    int cant = Convert.ToInt32(row.Cells["Cantidad"].Value);
                    decimal precio = Convert.ToDecimal(row.Cells["Precio"].Value);

                    CarritoGlobal.AgregarItem(idProd, codigoDeBarras, concepto, cant, precio); // Lo mandamos al Carrito Global
                }

                // 2. Refrescamos la ventana de Caja
                Form cajaAbierta = Application.OpenForms["frmCajaCobro"];
                if (cajaAbierta != null) cajaAbierta.Close();

                frmCajaCobro nuevaCaja = new frmCajaCobro(usuarioActual);

                // 3. CAMBIO CLAVE: Abrimos con ShowDialog() para que espere la respuesta de la caja
                if (nuevaCaja.ShowDialog() == DialogResult.OK)
                {
                    // 1. Limpiamos la grilla del carrito local y el total a pagar
                    dgvCarrito.Rows.Clear();
                    ActualizarTotal(); // Esto dejará el texto visualmente en "Gs. 0"

                    // --- LA MAGIA DEL AUTO-REFRESH ---

                    // 2. Volvemos a traer TODOS los productos de la base de datos (con el stock ya descontado)
                    dtCatalogo = negocio.ListarProductos();

                    // 3. Recreamos nuestra columna invisible para que el buscador sin acentos siga funcionando
                    dtCatalogo.Columns.Add("NombreBusqueda", typeof(string));
                    foreach (DataRow fila in dtCatalogo.Rows)
                    {
                        string nombreOriginal = fila["Nombre"].ToString();
                        fila["NombreBusqueda"] = QuitarAcentos(nombreOriginal);
                    }

                    // 4. Mandamos a redibujar las tarjetas
                    // Al llamar a AplicarFiltros(), automáticamente se borran las tarjetas viejas,
                    // se leen los datos nuevos y se generan las tarjetas con el nuevo stock
                    AplicarFiltros();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar productos a la caja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCarrito_SelectionChanged(object sender, EventArgs e)
        {
            // Esto hace que apenas la fila intente pintarse de azul, el sistema la desmarque a la velocidad de la luz
            dgvCarrito.ClearSelection();
        }
    }
}