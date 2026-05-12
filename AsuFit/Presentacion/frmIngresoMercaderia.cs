using System;
using AsuFit.Entidades;
using AsuFit.Datos;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AsuFit.Negocio;
using System.Runtime.InteropServices;
using System.Text;
using System.Globalization;

namespace AsuFit.Presentacion
{
    public partial class frmIngresoMercaderia : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        // 1. SOLUCIÓN AL ERROR CS0103: Declaramos las tres capas de negocio que necesitamos
        private InventarioNegocio negocio = new InventarioNegocio();
        private ProveedorNegocio negocioProveedor = new ProveedorNegocio();
        private IngresoMercaderiaNegocio negocioIngreso = new IngresoMercaderiaNegocio();
        private DataTable dtProductos;
        private Usuario usuarioActual;

        public frmIngresoMercaderia(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;
        }

        private void frmIngresoMercaderia_Load(object sender, EventArgs e)
        {
            CargarProveedores(); // Ahora carga los reales
            CargarGrilla();
            ConfigurarAutocompletado();
            SendMessage(txtBuscarProducto.Handle, EM_SETCUEBANNER, 1, "Buscar producto en el catálogo...");

            txtBuscarProducto.Focus();
        }

        // 2. NUEVO MÉTODO: Carga los proveedores reales desde SQL Server
        private void CargarProveedores()
        {
            try
            {
                DataTable dtProveedores = negocioProveedor.ListarProveedores();

                // Filtramos para que solo aparezcan los proveedores que están "Activos"
                DataView dv = new DataView(dtProveedores);
                dv.RowFilter = "Estado = 'Activo'";

                // Le decimos al ComboBox qué mostrar y qué valor oculto guardar (el ID)
                cmbProveedores.DisplayMember = "Nombre";
                cmbProveedores.ValueMember = "IdProveedor";
                cmbProveedores.DataSource = dv.ToTable();

                cmbProveedores.SelectedIndex = -1; // Lo dejamos en blanco al inicio
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGrilla()
        {
            dtProductos = negocio.ListarProductos();

            if (!dtProductos.Columns.Contains("NombreBusqueda"))
            {
                dtProductos.Columns.Add("NombreBusqueda", typeof(string));

                foreach (DataRow row in dtProductos.Rows)
                {
                    string nombreOriginal = row["Nombre"].ToString();
                    row["NombreBusqueda"] = QuitarAcentos(nombreOriginal).ToLower();
                }
            }

            dgvProductos.DataSource = dtProductos;

            if (dgvProductos.Columns.Contains("NombreBusqueda")) dgvProductos.Columns["NombreBusqueda"].Visible = false;
            if (dgvProductos.Columns.Contains("IdProducto")) dgvProductos.Columns["IdProducto"].Visible = false;
            if (dgvProductos.Columns.Contains("PrecioCompra")) dgvProductos.Columns["PrecioCompra"].Visible = false;

            if (dgvProductos.Columns.Contains("IdProveedor")) dgvProductos.Columns["IdProveedor"].Visible = false;

            // Filtramos para que SOLO salgan los activos (A diferencia de Gestión de Productos)
            (dgvProductos.DataSource as DataTable).DefaultView.RowFilter = "Estado = 'Activo'";
            if (dgvProductos.Columns.Contains("Estado")) dgvProductos.Columns["Estado"].Visible = false;

            if (dgvProductos.Columns.Contains("Proveedor"))
            {
                dgvProductos.Columns["Proveedor"].Visible = true;
                dgvProductos.Columns["Proveedor"].HeaderText = "Proveedor Principal";
            }

            if (dgvProductos.Columns.Contains("StockMinimo"))
            {
                dgvProductos.Columns["StockMinimo"].Visible = true;
                dgvProductos.Columns["StockMinimo"].HeaderText = "Stock Mín.";
                dgvProductos.Columns["StockMinimo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.ReadOnly = true;
            dgvProductos.ClearSelection();
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                txtIdProductoSeleccionado.Text = fila.Cells["IdProducto"].Value.ToString();
                lblDetalleProducto.Text = "Producto: " + fila.Cells["Nombre"].Value.ToString();
                lblDetalleCodigo.Text = "Código: " + fila.Cells["CodigoBarras"].Value.ToString();
                lblDetalleCategoria.Text = "Categoría: " + fila.Cells["Categoria"].Value.ToString();
                lblDetalleStock.Text = "Stock Actual: " + fila.Cells["StockActual"].Value.ToString();
                lblDetalleStockMinimo.Text = "Stock Mínimo: " + fila.Cells["StockMinimo"].Value.ToString();

                // Auto-seleccionar el proveedor del producto en el ComboBox y actualizar el Label
                if (fila.Cells["Proveedor"].Value != DBNull.Value && fila.Cells["Proveedor"].Value != null)
                {
                    cmbProveedores.Text = fila.Cells["Proveedor"].Value.ToString();
                    lblProveedorProducto.Text = "Proveedor: " + fila.Cells["Proveedor"].Value.ToString();
                }
                else
                {
                    cmbProveedores.SelectedIndex = -1;
                    lblProveedorProducto.Text = "Proveedor: Sin asignar";
                }

                // Cargar la imagen del producto
                string codigo = fila.Cells["CodigoBarras"].Value.ToString();
                string rutaFoto = @"C:\AsuFit_Fotos\" + codigo + ".jpg";

                if (System.IO.File.Exists(rutaFoto))
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(rutaFoto, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        picProducto.Image = Image.FromStream(fs);
                    }
                }
                else
                {
                    picProducto.Image = null;
                }

                ActualizarResumen();
            }
        }

        private void ActualizarResumen()
        {
            int cantidad = 0;
            decimal costoTotal = 0;

            string textoCantidad = txtCantidadIngreso.Text.Replace(".", "").Trim();
            string textoCosto = txtCostoTotal.Text.Replace(".", "").Trim();

            if (!string.IsNullOrWhiteSpace(textoCantidad))
                int.TryParse(textoCantidad, out cantidad);

            if (!string.IsNullOrWhiteSpace(textoCosto))
                decimal.TryParse(textoCosto, out costoTotal);

            if (cantidad > 0 && costoTotal > 0)
            {
                decimal costoUnitario = costoTotal / cantidad;
                txtCostoUnitario.Text = Math.Round(costoUnitario, 0).ToString("N0");
            }
            else
            {
                txtCostoUnitario.Text = "0";
            }

            // Evitamos error si el ComboBox está vacío
            txtResumenProveedor.Text = cmbProveedores.SelectedIndex != -1 ? cmbProveedores.Text : "";

            if (txtIdProductoSeleccionado.Text != "")
                txtResumenProducto.Text = lblDetalleProducto.Text.Replace("Producto: ", "");
            else
                txtResumenProducto.Text = "";

            txtResumenCantidad.Text = cantidad.ToString();
            txtResumenTotal.Text = costoTotal > 0 ? "Gs. " + costoTotal.ToString("N0") : "Gs. 0";

            if (dgvProductos.CurrentRow != null && txtIdProductoSeleccionado.Text != "")
            {
                int stockActual = Convert.ToInt32(dgvProductos.CurrentRow.Cells["StockActual"].Value);
                int nuevoStock = stockActual + cantidad;
                txtResumenNuevoStock.Text = nuevoStock.ToString();
            }
            else
            {
                txtResumenNuevoStock.Text = "0";
            }
        }

        private void btnConfirmarIngreso_Click(object sender, EventArgs e)
        {
            if (txtIdProductoSeleccionado.Text == "")
            {
                MessageBox.Show("Por favor, seleccione un producto de la lista primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCantidadIngreso.Text) || string.IsNullOrWhiteSpace(txtCostoTotal.Text) || cmbProveedores.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, ingrese la cantidad, el costo total y seleccione un proveedor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idProducto = Convert.ToInt32(txtIdProductoSeleccionado.Text);
                int cantidad = Convert.ToInt32(txtCantidadIngreso.Text);
                decimal costoTotal = Convert.ToDecimal(txtCostoTotal.Text);

                // 3. CAPTURAMOS EL ID REAL DEL PROVEEDOR
                int idProveedorReal = Convert.ToInt32(cmbProveedores.SelectedValue);

                // Registramos el ingreso en la Base de Datos usando la transacción
                bool exito = negocioIngreso.RegistrarIngreso(idProveedorReal, idProducto, cantidad, costoTotal, DateTime.Now, "Ingreso desde sistema");

                if (exito)
                {
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Inventario", "Ingreso de Mercadería", $"Ingresaron {cantidad} unid. de producto ID {idProducto} (Prov: {cmbProveedores.Text}).");
                    MessageBox.Show("¡Mercadería ingresada y stock actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLimpiar_Click(null, null);
                    CargarGrilla(); // Refrescamos la tabla para ver el nuevo stock
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIdProductoSeleccionado.Clear();
            txtBuscarProducto.Clear();
            txtCantidadIngreso.Clear();
            txtCostoUnitario.Clear();
            txtCostoTotal.Clear();
            cmbProveedores.SelectedIndex = -1;

            lblDetalleProducto.Text = "Producto: ";
            lblDetalleCodigo.Text = "Código: ";
            lblDetalleCategoria.Text = "Categoría: ";
            lblDetalleStock.Text = "Stock Actual: ";
            lblDetalleStockMinimo.Text = "Stock Mínimo: ";
            lblProveedorProducto.Text = "Proveedor: "; // <-- Agregado para limpiar bien
            picProducto.Image = null;

            txtResumenProveedor.Clear();
            txtResumenProducto.Clear();
            txtResumenCantidad.Clear();
            txtResumenTotal.Clear();
            txtResumenNuevoStock.Clear();

            dgvProductos.ClearSelection();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCantidadIngreso_TextChanged(object sender, EventArgs e) { ActualizarResumen(); }
        private void txtCostoTotal_TextChanged(object sender, EventArgs e) { ActualizarResumen(); }
        private void cmbProveedores_SelectedIndexChanged(object sender, EventArgs e) { ActualizarResumen(); }

        private void txtCantidadIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void txtCostoTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            if (dtProductos == null) return;

            string textoLimpio = QuitarAcentos(txtBuscarProducto.Text);
            string filtro = "NombreBusqueda LIKE '%" + textoLimpio + "%'";
            (dgvProductos.DataSource as DataTable).DefaultView.RowFilter = filtro;
        }

        private void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            // 1. Instanciamos el nuevo mini-formulario
            frmAgregarProveedor frmPopup = new frmAgregarProveedor();

            // 2. Lo abrimos con ShowDialog() para que el usuario no pueda hacer 
            // clic en la ventana de atrás hasta que termine de guardar el proveedor.
            frmPopup.ShowDialog();

            // 3. ¡EL TRUCO DE ORO! 
            // Cuando el usuario cierre la ventanita, el código continuará aquí.
            // Entonces, mandamos a recargar el ComboBox para que el proveedor 
            // que acaban de crear aparezca mágicamente en la lista.
            CargarProveedores();
        }

        private void ConfigurarAutocompletado()
        {
            AutoCompleteStringCollection listaSugerencias = new AutoCompleteStringCollection();

            foreach (DataRow row in dtProductos.Rows)
            {
                string nombreOriginal = row["Nombre"].ToString();
                string nombreSinAcento = row["NombreBusqueda"].ToString();

                if (!listaSugerencias.Contains(nombreOriginal)) listaSugerencias.Add(nombreOriginal);

                string[] palabras = nombreSinAcento.Split(' ');

                foreach (string palabra in palabras)
                {
                    if (palabra.Length > 2 && !listaSugerencias.Contains(palabra))
                        listaSugerencias.Add(palabra);
                }
            }
            txtBuscarProducto.AutoCompleteCustomSource = listaSugerencias;
            txtBuscarProducto.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtBuscarProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private string QuitarAcentos(string texto)
        {
            var textoNormalizado = texto.Normalize(NormalizationForm.FormD);
            var constructor = new StringBuilder();

            foreach (var c in textoNormalizado)
            {
                var categoria = CharUnicodeInfo.GetUnicodeCategory(c);
                if (categoria != UnicodeCategory.NonSpacingMark) constructor.Append(c);
            }
            return constructor.ToString().Normalize(NormalizationForm.FormC);
        }

        private void dgvProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvProductos.ClearSelection();
        }

        private void LimpiarSeleccion_Click(object sender, EventArgs e)
        {
            dgvProductos.ClearSelection();
            txtIdProductoSeleccionado.Clear();
            btnLimpiar_Click(null, null);
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            frmNuevoProducto frmPopup = new frmNuevoProducto(usuarioActual);
            frmPopup.ShowDialog();

            CargarGrilla();
            ConfigurarAutocompletado();

            // EL TRUCO: Si la ventanita nos dejó un nombre guardado, lo pegamos en el buscador
            if (frmPopup.ProductoRecienCreado != "")
            {
                txtBuscarProducto.Text = frmPopup.ProductoRecienCreado;
            }
        }
    }
}