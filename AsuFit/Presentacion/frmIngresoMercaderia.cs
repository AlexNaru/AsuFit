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

        private InventarioNegocio negocio = new InventarioNegocio();
        private ProveedorNegocio negocioProveedor = new ProveedorNegocio();
        private IngresoMercaderiaNegocio negocioIngreso = new IngresoMercaderiaNegocio();
        private DataTable dtProductos;
        private Usuario usuarioActual;

        public frmIngresoMercaderia(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;

            // --- EL CAMBIO CLAVE: Bloqueamos las columnas automáticas ---
            dgvProductos.AutoGenerateColumns = false;
        }

        private void frmIngresoMercaderia_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            CargarGrilla();
            ConfigurarAutocompletado();
            SendMessage(txtBuscarProducto.Handle, EM_SETCUEBANNER, 1, "Buscar producto en el catálogo...");

            txtBuscarProducto.Focus();
        }

        private void CargarProveedores()
        {
            try
            {
                DataTable dtProveedores = negocioProveedor.ListarProveedores();
                DataView dv = new DataView(dtProveedores);
                dv.RowFilter = "Estado = 'Activo'";

                cmbProveedores.DisplayMember = "Nombre";
                cmbProveedores.ValueMember = "IdProveedor";
                cmbProveedores.DataSource = dv.ToTable();

                cmbProveedores.SelectedIndex = -1;
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

            // Filtramos la data subyacente para mostrar solo los activos
            (dgvProductos.DataSource as DataTable).DefaultView.RowFilter = "Estado = 'Activo'";

            // --- CÓDIGO LIMPIO: Toda la configuración de ocultar/renombrar columnas se eliminó ---

            dgvProductos.ClearSelection();
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                // ACTUALIZADO: Leemos usando los Name de las columnas
                txtIdProductoSeleccionado.Text = fila.Cells["colIngresoId"].Value.ToString();
                lblDetalleProducto.Text = "Producto: " + fila.Cells["colIngresoNombre"].Value.ToString();
                lblDetalleCodigo.Text = "Código: " + fila.Cells["colIngresoCodigo"].Value.ToString();
                lblDetalleCategoria.Text = "Categoría: " + fila.Cells["colIngresoCategoria"].Value.ToString();
                lblDetalleStock.Text = "Stock Actual: " + fila.Cells["colIngresoStock"].Value.ToString();
                lblDetalleStockMinimo.Text = "Stock Mínimo: " + fila.Cells["colIngresoStockMin"].Value.ToString();

                if (fila.Cells["colIngresoProveedor"].Value != DBNull.Value && fila.Cells["colIngresoProveedor"].Value != null)
                {
                    cmbProveedores.Text = fila.Cells["colIngresoProveedor"].Value.ToString();
                    lblProveedorProducto.Text = "Proveedor: " + fila.Cells["colIngresoProveedor"].Value.ToString();
                }
                else
                {
                    cmbProveedores.SelectedIndex = -1;
                    lblProveedorProducto.Text = "Proveedor: Sin asignar";
                }

                string codigo = fila.Cells["colIngresoCodigo"].Value.ToString();
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

            txtResumenProveedor.Text = cmbProveedores.SelectedIndex != -1 ? cmbProveedores.Text : "";

            if (txtIdProductoSeleccionado.Text != "")
                txtResumenProducto.Text = lblDetalleProducto.Text.Replace("Producto: ", "");
            else
                txtResumenProducto.Text = "";

            txtResumenCantidad.Text = cantidad.ToString();
            txtResumenTotal.Text = costoTotal > 0 ? "Gs. " + costoTotal.ToString("N0") : "Gs. 0";

            if (dgvProductos.CurrentRow != null && txtIdProductoSeleccionado.Text != "")
            {
                // ACTUALIZADO: Leemos el stock de la columna correcta
                int stockActual = Convert.ToInt32(dgvProductos.CurrentRow.Cells["colIngresoStock"].Value);
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
                int idProveedorReal = Convert.ToInt32(cmbProveedores.SelectedValue);

                bool exito = negocioIngreso.RegistrarIngreso(idProveedorReal, idProducto, cantidad, costoTotal, DateTime.Now, "Ingreso desde sistema");

                if (exito)
                {
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Inventario", "Ingreso de Mercadería", $"Ingresaron {cantidad} unid. de producto ID {idProducto} (Prov: {cmbProveedores.Text}).");
                    MessageBox.Show("¡Mercadería ingresada y stock actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLimpiar_Click(null, null);
                    CargarGrilla();
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
            lblProveedorProducto.Text = "Proveedor: ";
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
            string filtro = "Estado = 'Activo' AND NombreBusqueda LIKE '%" + textoLimpio + "%'";
            (dgvProductos.DataSource as DataTable).DefaultView.RowFilter = filtro;
        }

        private void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            frmAgregarProveedor frmPopup = new frmAgregarProveedor();
            frmPopup.ShowDialog();
            CargarProveedores();
        }

        private void ConfigurarAutocompletado()
        {
            AutoCompleteStringCollection listaSugerencias = new AutoCompleteStringCollection();

            foreach (DataRow row in dtProductos.Rows)
            {
                if (row["Estado"].ToString() == "Activo")
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

            if (frmPopup.ProductoRecienCreado != "")
            {
                txtBuscarProducto.Text = frmPopup.ProductoRecienCreado;
            }
        }
    }
}