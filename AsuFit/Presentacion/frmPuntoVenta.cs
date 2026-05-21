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
        private Usuario usuarioActual;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmPuntoVenta(Usuario userLogueDado)
        {
            InitializeComponent();
            usuarioActual = userLogueDado;
            dgvCarrito.AutoGenerateColumns = false;
        }

        private InventarioNegocio negocio = new InventarioNegocio();
        private DataTable dtCatalogo;

        private void frmPuntoVenta_Load(object sender, EventArgs e)
        {
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
            AplicarFiltros();

            SendMessage(txtBuscarProducto.Handle, EM_SETCUEBANNER, 1, "Buscar Bebidas, Snacks o Suplementos...");
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
            cmbFiltroCategoria.Items.Add("Todas");
            cmbFiltroCategoria.Items.Add("Suplementos");
            cmbFiltroCategoria.Items.Add("Bebidas");
            cmbFiltroCategoria.Items.Add("Snacks");
            cmbFiltroCategoria.SelectedIndex = 0;

            cmbOrdenar.Items.Add("Nombre (A-Z)");
            cmbOrdenar.Items.Add("Nombre (Z-A)");
            cmbOrdenar.Items.Add("Precio (Menor a Mayor)");
            cmbOrdenar.Items.Add("Precio (Mayor a Menor)");
            cmbOrdenar.SelectedIndex = 0;

            cmbFiltroCategoria.SelectedIndexChanged += CombosFiltro_SelectedIndexChanged;
            cmbOrdenar.SelectedIndexChanged += CombosFiltro_SelectedIndexChanged;
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void CombosFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
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

        private void ConfigurarAutocompletado()
        {
            AutoCompleteStringCollection listaSugerencias = new AutoCompleteStringCollection();

            foreach (DataRow row in dtCatalogo.Rows)
            {
                string nombreOriginal = row["Nombre"].ToString();
                string nombreSinAcento = row["NombreBusqueda"].ToString();

                if (!listaSugerencias.Contains(nombreOriginal))
                    listaSugerencias.Add(nombreOriginal);

                if (!listaSugerencias.Contains(nombreSinAcento))
                    listaSugerencias.Add(nombreSinAcento);

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

        private void AplicarFiltros()
        {
            if (dtCatalogo == null) return;
            string filtro = "";

            if (!string.IsNullOrWhiteSpace(txtBuscarProducto.Text))
            {
                string textoLimpio = QuitarAcentos(txtBuscarProducto.Text);
                filtro = "NombreBusqueda LIKE '%" + textoLimpio + "%'";
            }

            if (cmbFiltroCategoria.Text != "Todas")
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

            GenerarTarjetas(filtro, orden);
        }

        private void GenerarTarjetas(string filtro, string orden)
        {
            flpCatalogo.SuspendLayout();
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

                string codigo = row["CodigoBarras"].ToString();
                string rutaFoto = @"C:\AsuFit_Fotos\" + codigo + ".jpg";

                if (System.IO.File.Exists(rutaFoto))
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(rutaFoto, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        pic.Image = Image.FromStream(fs);
                    }
                }
                else
                {
                    pic.Image = null;
                }

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
                    CarritoGlobal.Detalles.Rows.RemoveAt(i);
                }
            }

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
                for (int i = CarritoGlobal.Detalles.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(CarritoGlobal.Detalles.Rows[i]["IdProducto"]) > 0)
                    {
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

        private void dgvCarrito_SelectionChanged(object sender, EventArgs e)
        {
            dgvCarrito.ClearSelection();
        }
    }
}