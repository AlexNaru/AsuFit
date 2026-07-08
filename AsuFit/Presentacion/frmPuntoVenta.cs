using AsuFit.Entidades;
using AsuFit.Negocio;
using AsuFit.Datos;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmPuntoVenta : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR

        private Usuario usuarioActual;
        private InventarioNegocio negocio = new InventarioNegocio();
        private DataTable dtCatalogo;

        public frmPuntoVenta(Usuario userLogueDado)
        {
            InitializeComponent();

            this.Load += new EventHandler(frmPuntoVenta_Load);

            usuarioActual = userLogueDado;
            dgvCarrito.AutoGenerateColumns = false;

            this.DoubleBuffered = true;
            this.Opacity = 0;
            this.Shown += new EventHandler(frmPuntoVenta_Shown);
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS

        private void frmPuntoVenta_Load(object sender, EventArgs e)
        {
            flpCatalogo.SuspendLayout();

            ConfigurarTemaOscuro();
            ConfigurarCarrito();
            ConfigurarFiltros();

            dtCatalogo = negocio.ListarProductos();

            dtCatalogo.Columns.Add("NombreBusqueda", typeof(string));
            foreach (DataRow row in dtCatalogo.Rows)
            {
                string nombreOriginal = row["Nombre"].ToString();
                row["NombreBusqueda"] = QuitarAcentos(nombreOriginal);
            }

            ConfigurarAutocompletado();

            // Generamos todas las tarjetas UNA SOLA VEZ y las dejamos en memoria.
            GenerarTarjetasIniciales();

            // Aplicamos los filtros iniciales (Solo ocultará o mostrará, sin volver a cargar imágenes)
            AplicarFiltros();

            AplicarPlaceholder(txtBuscarProducto, "Buscar Bebidas, Snacks o Suplementos...");

            // Inyecta un menú nulo para neutralizar el clic derecho en el cuadro de búsqueda
            txtBuscarProducto.ContextMenuStrip = new ContextMenuStrip();
            txtBuscarProducto.KeyDown += txtBuscarProducto_KeyDown;
            txtBuscarProducto.KeyPress += txtBuscarProducto_KeyPress;

            //Recupera visualmente el carrito si el cajero regresó de la caja
            SincronizarCarritoVisual();

            flpCatalogo.ResumeLayout(false);

            this.ActiveControl = null;
        }

        // Lee los datos del CarritoGlobal en memoria y los dibuja en la grilla para permitir su edición
        private void SincronizarCarritoVisual()
        {
            dgvCarrito.Rows.Clear();

            if (CarritoGlobal.Detalles != null && CarritoGlobal.Detalles.Rows.Count > 0)
            {
                foreach (DataRow fila in CarritoGlobal.Detalles.Rows)
                {
                    int idProd = Convert.ToInt32(fila["IdProducto"]);

                    // Solo recuperamos productos de inventario (IdProducto > 0), omitiendo suscripciones
                    if (idProd > 0)
                    {
                        int rowIndex = dgvCarrito.Rows.Add();
                        DataGridViewRow row = dgvCarrito.Rows[rowIndex];

                        row.Cells["colCarritoId"].Value = idProd;
                        row.Cells["colCarritoCodigo"].Value = fila["CodigoBarras"].ToString();
                        row.Cells["colCarritoNombre"].Value = fila["Concepto"].ToString();
                        row.Cells["colCarritoCantidad"].Value = Convert.ToInt32(fila["Cantidad"]);
                        row.Cells["colCarritoPrecio"].Value = Convert.ToDecimal(fila["PrecioUnitario"]);
                        row.Cells["colCarritoSubtotal"].Value = Convert.ToDecimal(fila["SubTotal"]);
                        row.Cells["colCarritoIva"].Value = Convert.ToInt32(fila["PorcentajeIva"]);
                    }
                }
                ActualizarTotal();
            }
        }

        private void frmPuntoVenta_Shown(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void ConfigurarCarrito()
        {
            dgvCarrito.AllowUserToAddRows = false;
            dgvCarrito.RowHeadersVisible = false;
            dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCarrito.ReadOnly = true;
            dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvCarrito.CellContentClick -= dgvCarrito_CellContentClick;
            dgvCarrito.CellContentDoubleClick -= dgvCarrito_CellContentClick;
            dgvCarrito.CellContentClick += dgvCarrito_CellContentClick;
            dgvCarrito.CellContentDoubleClick += dgvCarrito_CellContentClick;
        }

        private void ConfigurarFiltros()
        {
            // FIX: Forzamos el estilo DropDownList para evitar que se puedan editar y se bugueen
            cmbFiltroCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrdenar.DropDownStyle = ComboBoxStyle.DropDownList;

            if (cmbFiltroCategoria.Items.Count > 0) cmbFiltroCategoria.SelectedIndex = 0;
            if (cmbOrdenar.Items.Count > 0) cmbOrdenar.SelectedIndex = 0;

            cmbFiltroCategoria.SelectedIndexChanged += CombosFiltro_SelectedIndexChanged;
            cmbOrdenar.SelectedIndexChanged += CombosFiltro_SelectedIndexChanged;

            cmbFiltroCategoria.DropDownClosed += CombosFiltro_DropDownClosed;
            cmbOrdenar.DropDownClosed += CombosFiltro_DropDownClosed;
        }
        #endregion

        #region 3. ESTILOS VISUALES (TEMA OSCURO)
        private void ConfigurarTemaOscuro()
        {
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            this.BackColor = Color.FromArgb(25, 28, 35);

            AplicarTemaOscuroRecursivo(this, fuenteActual);
            ConfigurarTemaOscuroGrilla(dgvCarrito, fuenteActual);

            if (btnFinalizarVenta != null)
            {
                btnFinalizarVenta.BackColor = Color.FromArgb(25, 28, 35);
                btnFinalizarVenta.ForeColor = Color.White;
                btnFinalizarVenta.Font = new Font("Segoe UI", fuenteActual, FontStyle.Bold);
                btnFinalizarVenta.FlatStyle = FlatStyle.Flat;
                btnFinalizarVenta.FlatAppearance.BorderSize = 1;
                btnFinalizarVenta.FlatAppearance.BorderColor = Color.FromArgb(0, 229, 255); // Borde Cian
                btnFinalizarVenta.Cursor = Cursors.Hand;
            }

            if (btnLimpiarCarrito != null)
            {
                btnLimpiarCarrito.BackColor = Color.FromArgb(25, 28, 35);
                btnLimpiarCarrito.ForeColor = Color.White;
                btnLimpiarCarrito.Font = new Font("Segoe UI", fuenteActual, FontStyle.Bold);
                btnLimpiarCarrito.FlatStyle = FlatStyle.Flat;
                btnLimpiarCarrito.FlatAppearance.BorderSize = 1;
                btnLimpiarCarrito.FlatAppearance.BorderColor = Color.FromArgb(0, 229, 255); // Borde Cian
                btnLimpiarCarrito.Cursor = Cursors.Hand;
            }
        }

        private void AplicarTemaOscuroRecursivo(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Panel || c is GroupBox)
                {
                    if (c.Name != "flpCatalogo")
                    {
                        c.BackColor = Color.FromArgb(35, 39, 47);
                    }
                    c.ForeColor = Color.White;
                }
                else if (c is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                    lbl.Font = new Font("Segoe UI", fuente, lbl.Font.Style);
                }
                else if (c is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(50, 55, 65);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
                }
                else if (c is ComboBox cmb)
                {
                    cmb.BackColor = Color.FromArgb(50, 55, 65);
                    cmb.ForeColor = Color.White;
                    cmb.FlatStyle = FlatStyle.Flat;
                    cmb.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
                }
                else if (c is Button btn && btn.Name != "btnFinalizarVenta" && btn.Name != "btnLimpiarCarrito")
                {
                    btn.BackColor = Color.FromArgb(50, 55, 65);
                    btn.ForeColor = Color.White;
                    btn.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c, fuente);
            }
        }

        private void ConfigurarTemaOscuroGrilla(DataGridView dgv, float fuente)
        {
            if (dgv == null) return;

            dgv.BackgroundColor = Color.FromArgb(25, 28, 35);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(50, 55, 65);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(25, 28, 35);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 229, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 35;
        }

        // Gestiona el comportamiento de la marca de agua con desvanecimiento dinámico estilo AsuFit
        private void AplicarPlaceholder(TextBox txt, string textoAyuda)
        {
            txt.Tag = textoAyuda;

            if (string.IsNullOrWhiteSpace(txt.Text) || txt.Text == textoAyuda)
            {
                txt.Text = textoAyuda;
                txt.ForeColor = Color.Silver;
            }
            else
            {
                txt.ForeColor = Color.White;
            }

            txt.Enter += delegate
            {
                if (txt.Text == textoAyuda)
                {
                    this.BeginInvoke(new Action(() => txt.SelectionStart = 0));
                }
            };

            // Intercepta el clic y el arrastre del mouse para impedir que pinten de azul la ayuda
            txt.MouseDown += delegate
            {
                if (txt.Text == textoAyuda)
                {
                    txt.SelectionStart = 0;
                    txt.SelectionLength = 0;
                }
            };

            txt.MouseMove += delegate
            {
                if (txt.Text == textoAyuda && txt.SelectionLength > 0)
                {
                    txt.SelectionStart = 0;
                    txt.SelectionLength = 0;
                }
            };

            txt.TextChanged += delegate
            {
                if (txt.Text != textoAyuda && txt.ForeColor == Color.Silver)
                {
                    string entradaUsuario = txt.Text;

                    // Solo quitamos el bloque exacto del placeholder, sin destruir caracteres similares
                    if (entradaUsuario.StartsWith(textoAyuda))
                        entradaUsuario = entradaUsuario.Substring(textoAyuda.Length);
                    else if (entradaUsuario.EndsWith(textoAyuda))
                        entradaUsuario = entradaUsuario.Substring(0, entradaUsuario.Length - textoAyuda.Length);

                    txt.ForeColor = Color.White;
                    txt.Text = entradaUsuario;
                    txt.SelectionStart = txt.Text.Length;
                }
                else if (string.IsNullOrEmpty(txt.Text))
                {
                    txt.ForeColor = Color.Silver;
                    txt.Text = textoAyuda;
                    txt.SelectionStart = 0;
                }
            };

            txt.KeyDown += delegate (object sender, KeyEventArgs e)
            {
                if (txt.Text == textoAyuda && (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
                {
                    e.SuppressKeyPress = true;
                }
            };
        }
        #endregion

        #region 4. SECCIÓN SUPERIOR: BÚSQUEDA Y FILTROS

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarProducto.Text != "Buscar Bebidas, Snacks o Suplementos...")
            {
                AplicarFiltros();
            }
        }

        private void CombosFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void CombosFiltro_DropDownClosed(object sender, EventArgs e)
        {
            // FIX: Usar BeginInvoke evita interrumpir el cierre natural del ComboBox y destraba el control
            this.BeginInvoke(new Action(() => this.ActiveControl = null));
        }

        private void AplicarFiltros()
        {
            if (dtCatalogo == null || flpCatalogo.Controls.Count == 0) return;
            string filtro = "";

            string textoBusqueda = txtBuscarProducto.Text;
            if (textoBusqueda == "Buscar Bebidas, Snacks o Suplementos...") textoBusqueda = "";

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                // PROTECCIÓN CRÍTICA: Sanitiza la entrada duplicando comillas simples para anular inyecciones lógicas en DataView
                string textoLimpio = QuitarAcentos(textoBusqueda).Replace("'", "''");
                filtro = "NombreBusqueda LIKE '%" + textoLimpio + "%'";
            }

            if (cmbFiltroCategoria.Text != "Todas" && !string.IsNullOrWhiteSpace(cmbFiltroCategoria.Text))
            {
                if (filtro.Length > 0) filtro += " AND ";
                filtro += "Categoria = '" + cmbFiltroCategoria.Text + "'";
            }

            if (filtro.Length > 0) filtro += " AND ";
            filtro += "StockActual > 0 AND Estado = 'Activo'";

            string orden = "Nombre ASC";
            if (cmbOrdenar.Text == "Nombre (Z-A)") orden = "Nombre DESC";
            else if (cmbOrdenar.Text == "Precio (Menor a Mayor)") orden = "PrecioVenta ASC";
            else if (cmbOrdenar.Text == "Precio (Mayor a Menor)") orden = "PrecioVenta DESC";

            // Congelamos visualmente el panel
            flpCatalogo.SuspendLayout();

            DataView vistaFiltrada = dtCatalogo.DefaultView;
            vistaFiltrada.RowFilter = filtro;
            vistaFiltrada.Sort = orden;

            // 1. Ocultamos TODAS las tarjetas primero
            foreach (Control c in flpCatalogo.Controls)
            {
                c.Visible = false;
            }

            // 2. Mostramos SOLO las que cumplen el filtro y las ordenamos
            int index = 0;
            foreach (DataRowView rowView in vistaFiltrada)
            {
                string idProd = rowView["IdProducto"].ToString();

                // Buscamos la tarjeta por su nombre único (Ej: "card_5")
                Control[] cards = flpCatalogo.Controls.Find("card_" + idProd, false);
                if (cards.Length > 0)
                {
                    Control pnl = cards[0];
                    pnl.Visible = true;
                    flpCatalogo.Controls.SetChildIndex(pnl, index); // Ordena la tarjeta visualmente
                    index++;
                }
            }

            flpCatalogo.ResumeLayout();
        }

        private void ConfigurarAutocompletado()
        {
            AutoCompleteStringCollection listaSugerencias = new AutoCompleteStringCollection();

            foreach (DataRow row in dtCatalogo.Rows)
            {
                string nombreOriginal = row["Nombre"].ToString();
                string nombreSinAcento = row["NombreBusqueda"].ToString();

                if (!listaSugerencias.Contains(nombreOriginal)) listaSugerencias.Add(nombreOriginal);
                if (!listaSugerencias.Contains(nombreSinAcento)) listaSugerencias.Add(nombreSinAcento);

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

        #region 5. SECCIÓN IZQUIERDA: CATÁLOGO DE PRODUCTOS (TARJETAS)

        // Este método se ejecuta UNA SOLA VEZ y guarda las tarjetas en memoria
        private void GenerarTarjetasIniciales()
        {
            flpCatalogo.SuspendLayout();
            flpCatalogo.Controls.Clear();

            float escalaGlobal = Properties.Settings.Default.EscalaInterfaz;
            float tamanoTexto = (escalaGlobal > 1.0f) ? 11.5f : 9f;

            foreach (DataRow row in dtCatalogo.Rows)
            {
                Panel pnlCard = new Panel();
                pnlCard.Name = "card_" + row["IdProducto"].ToString(); // Nombre único para encontrarla rápido
                pnlCard.Size = new Size(180, 240);
                pnlCard.BackColor = Color.FromArgb(45, 45, 48);
                pnlCard.Margin = new Padding(10);

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

                Label lblNombre = new Label();
                lblNombre.Text = row["Nombre"].ToString();
                lblNombre.ForeColor = Color.White;
                lblNombre.Font = new Font("Segoe UI", (escalaGlobal > 1.0f) ? 10f : 9f, FontStyle.Regular);
                lblNombre.Location = new Point(10, 132);
                lblNombre.AutoSize = false;
                lblNombre.Size = new Size(160, 32);

                Label lblPrecio = new Label();
                lblPrecio.Text = "Gs. " + Convert.ToDecimal(row["PrecioVenta"]).ToString("N0");
                lblPrecio.ForeColor = Color.White;
                lblPrecio.Font = new Font("Segoe UI", (escalaGlobal > 1.0f) ? 12f : 9f, FontStyle.Bold);    
                lblPrecio.Location = new Point(10, (escalaGlobal > 1.0f) ? 170 : 171);
                lblPrecio.AutoSize = true;

                Label lblStock = new Label();
                lblStock.Text = "Stock: " + row["StockActual"].ToString();
                lblStock.ForeColor = Color.LightGray;
                lblStock.Font = new Font("Segoe UI", tamanoTexto, FontStyle.Regular);
                lblStock.Location = new Point(115, 171);
                lblStock.AutoSize = true;

                Button btnAgregar = new Button();
                btnAgregar.Text = "AGREGAR AL CARRITO";
                btnAgregar.BackColor = Color.FromArgb(0, 229, 255);
                btnAgregar.ForeColor = Color.Black;
                btnAgregar.FlatStyle = FlatStyle.Flat;
                btnAgregar.FlatAppearance.BorderSize = 0;
                btnAgregar.Location = new Point(10, 198);
                btnAgregar.Size = new Size(160, 28);
                btnAgregar.Cursor = Cursors.Hand;
                btnAgregar.Tag = row["IdProducto"];
                btnAgregar.Click += BtnAgregar_Click;
                btnAgregar.Font = new Font("Segoe UI", 8, FontStyle.Bold);

                pnlCard.Controls.Add(pic);
                pnlCard.Controls.Add(lblNombre);
                pnlCard.Controls.Add(lblPrecio);
                pnlCard.Controls.Add(lblStock);
                pnlCard.Controls.Add(btnAgregar);

                pnlCard.Scale(new SizeF(escalaGlobal, escalaGlobal));

                flpCatalogo.Controls.Add(pnlCard);
            }

            flpCatalogo.ResumeLayout();
        }

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
                int iva = filaProducto[0]["PorcentajeIva"] != DBNull.Value ? Convert.ToInt32(filaProducto[0]["PorcentajeIva"]) : 10;

                bool existeEnCarrito = false;

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

        #region 6. SECCIÓN DERECHA: CARRITO DE COMPRAS Y TABLA

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

        private void dgvCarrito_SelectionChanged(object sender, EventArgs e)
        {
            dgvCarrito.ClearSelection();
        }
        #endregion

        #region 7. ACCIONES FINALES: LIMPIAR Y VENDER

        public void LimpiarGrillaVisual()
        {
            dgvCarrito.Rows.Clear();
            ActualizarTotal();
        }

        private void btnLimpiarCarrito_Click(object sender, EventArgs e)
        {
            LimpiarGrillaVisual();

            for (int i = CarritoGlobal.Detalles.Rows.Count - 1; i >= 0; i--)
            {
                if (Convert.ToInt32(CarritoGlobal.Detalles.Rows[i]["IdProducto"]) > 0)
                {
                    decimal subTotalFila = Convert.ToDecimal(CarritoGlobal.Detalles.Rows[i]["SubTotal"]);
                    CarritoGlobal.TotalAPagar -= subTotalFila;
                    CarritoGlobal.Detalles.Rows.RemoveAt(i);
                }
            }
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
                // Descuenta los productos actuales para evitar duplicar sumatorias
                for (int i = CarritoGlobal.Detalles.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(CarritoGlobal.Detalles.Rows[i]["IdProducto"]) > 0)
                    {
                        decimal subTotalFila = Convert.ToDecimal(CarritoGlobal.Detalles.Rows[i]["SubTotal"]);
                        CarritoGlobal.TotalAPagar -= subTotalFila;
                        CarritoGlobal.Detalles.Rows.RemoveAt(i);
                    }
                }

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

                // Abrir en modo Modal (bloquea el formulario de atrás)
                frmCajaCobro nuevaCaja = new frmCajaCobro(usuarioActual);
                CentrarSobreContenedor(nuevaCaja);
                nuevaCaja.ShowDialog();

                // Al cerrar la caja (por pagar o por agregar cosas), actualizamos la vista
                SincronizarCarritoVisual();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar productos a la caja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Intercepta combinaciones de teclas prohibidas para mitigar ingresos masivos no sanitizados
        private void txtBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
            }
        }

        // Lista Blanca Comercial: Permite caracteres alfanuméricos, espacios, guiones y porcentajes, destruyendo delimitadores relacionales
        private void txtBuscarProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (!char.IsControl(c) && !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c) &&
                c != '-' && c != '%' && c != '.' && c != '+' && c != '#')
            {
                e.Handled = true;
            }
        }

        // Proyecta las coordenadas absolutas en pantalla del panel principal para anclar ventanas transaccionales secundarias.
        private void CentrarSobreContenedor(Form frm)
        {
            Form dashboard = Application.OpenForms["frmDashboard"];
            if (dashboard != null)
            {
                Control[] contenedores = dashboard.Controls.Find("pnlContenedor", true);
                if (contenedores.Length > 0)
                {
                    Control pnl = contenedores[0];
                    frm.StartPosition = FormStartPosition.Manual;
                    Point pos = pnl.PointToScreen(Point.Empty);
                    int x = pos.X + (pnl.Width - frm.Width) / 2;
                    int y = pos.Y + (pnl.Height - frm.Height) / 2;
                    frm.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);
                    return;
                }
            }
            frm.StartPosition = FormStartPosition.CenterScreen;
        }
        #endregion
    }
}