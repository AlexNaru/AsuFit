using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmGestionProductos : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;
        private Usuario usuarioActual;

        public frmGestionProductos(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;

            // --- EL CAMBIO CLAVE: Bloqueamos las columnas automáticas ---
            dgvProductos.AutoGenerateColumns = false;
        }

        private InventarioNegocio negocio = new InventarioNegocio();
        private ProveedorNegocio negocioProveedor = new ProveedorNegocio();

        private DataTable dtProductos;
        private string rutaFotoOrigen = "";
        private const string carpetaFotos = @"C:\AsuFit_Fotos\";

        private void frmGestionProductos_Load(object sender, EventArgs e)
        {
            ConfigurarFiltros();
            CargarProveedores();
            CargarGrilla();
            if (!Directory.Exists(carpetaFotos)) Directory.CreateDirectory(carpetaFotos);

            dgvProductos.ClearSelection();
            dgvProductos.CurrentCell = null;

            ConfigurarAutocompletado();
            SendMessage(txtBuscarProducto.Handle, EM_SETCUEBANNER, 1, "Buscar producto...");
            txtBuscarProducto.Focus();
        }

        private void ConfigurarFiltros()
        {
            cmbCategoria.Items.Clear();
            cmbCategoria.Items.Add("Suplementos");
            cmbCategoria.Items.Add("Bebidas");
            cmbCategoria.Items.Add("Snacks");
        }

        private void CargarProveedores()
        {
            try
            {
                DataTable dtProveedores = negocioProveedor.ListarProveedores();
                DataView dv = new DataView(dtProveedores);
                dv.RowFilter = "Estado = 'Activo'";

                cmbProveedor.DisplayMember = "Nombre";
                cmbProveedor.ValueMember = "IdProveedor";
                cmbProveedor.DataSource = dv.ToTable();
                cmbProveedor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarDatos()
        {
            if (dtProductos == null) return;

            string filtroEstado = chkMostrarInactivos.Checked ? "Estado = 'Inactivo'" : "Estado = 'Activo'";
            string textoBusqueda = QuitarAcentos(txtBuscarProducto.Text.Trim()).ToLower().Replace("'", "''");
            string filtroFinal = filtroEstado;

            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                filtroFinal = $"{filtroEstado} AND NombreBusqueda LIKE '%{textoBusqueda}%'";
            }

            DataView dv = dtProductos.DefaultView;
            dv.RowFilter = filtroFinal;
            dgvProductos.DataSource = dv;

            // --- CÓDIGO LIMPIO: Ya no ocultamos columnas desde aquí ---

            dgvProductos.ClearSelection();
        }

        private void CargarGrilla()
        {
            dtProductos = negocio.ListarProductos();

            if (dtProductos != null)
            {
                if (!dtProductos.Columns.Contains("NombreBusqueda"))
                {
                    dtProductos.Columns.Add("NombreBusqueda", typeof(string));
                    foreach (DataRow row in dtProductos.Rows)
                    {
                        string nombreOriginal = row["Nombre"].ToString();
                        row["NombreBusqueda"] = QuitarAcentos(nombreOriginal).ToLower();
                    }
                }

                FiltrarDatos();

                // --- CÓDIGO LIMPIO: Eliminamos configuraciones manuales de formato y texto ---

                dgvProductos.AllowUserToAddRows = false;
                dgvProductos.RowHeadersVisible = false;
                dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProductos.ReadOnly = true;
            }
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void chkMostrarInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
            dgvProductos.ClearSelection();
            LimpiarFormulario();
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                // --- ACTUALIZADO: Referencias con los nuevos nombres visuales ---
                txtId.Text = fila.Cells["colProductoId"].Value.ToString();
                txtCodigo.Text = fila.Cells["colProductoCodigo"].Value.ToString();
                txtNombre.Text = fila.Cells["colProductoNombre"].Value.ToString();
                cmbCategoria.Text = fila.Cells["colProductoCategoria"].Value.ToString();
                txtPrecio.Text = Convert.ToDecimal(fila.Cells["colProductoPrecioVenta"].Value).ToString("N0");
                txtStock.Text = fila.Cells["colProductoStock"].Value.ToString();

                if (fila.Cells["colProductoProveedor"].Value != DBNull.Value && fila.Cells["colProductoProveedor"].Value != null)
                {
                    cmbProveedor.Text = fila.Cells["colProductoProveedor"].Value.ToString();
                }
                else
                {
                    cmbProveedor.SelectedIndex = -1;
                }

                if (dgvProductos.Columns.Contains("colProductoIva") && fila.Cells["colProductoIva"].Value != DBNull.Value)
                {
                    cmbIva.Text = fila.Cells["colProductoIva"].Value.ToString();
                }
                else
                {
                    cmbIva.SelectedIndex = -1;
                }

                string codigo = fila.Cells["colProductoCodigo"].Value.ToString();
                string rutaFoto = carpetaFotos + codigo + ".jpg";

                if (File.Exists(rutaFoto))
                {
                    picFoto.Image = Image.FromFile(rutaFoto);
                }
                else
                {
                    picFoto.Image = null;
                }

                rutaFotoOrigen = "";
            }
        }

        private void btnSubirFoto_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Trim() == "")
            {
                MessageBox.Show("Por favor, ingrese primero el Código de Barras.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Imágenes|*.jpg;*.jpeg;*.png";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                rutaFotoOrigen = openFile.FileName;
                try
                {
                    using (Image imgTemp = Image.FromFile(rutaFotoOrigen))
                    {
                        picFoto.Image = new Bitmap(imgTemp);
                    }
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("El formato de esta imagen no es compatible o el archivo está dañado. Intente con otra imagen.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rutaFotoOrigen = "";
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("El Nombre y el Precio son campos obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Producto objProducto = new Producto();
                objProducto.IdProducto = string.IsNullOrWhiteSpace(txtId.Text) ? 0 : Convert.ToInt32(txtId.Text);
                objProducto.CodigoBarras = txtCodigo.Text.Trim();
                objProducto.Nombre = txtNombre.Text.Trim();
                objProducto.Categoria = cmbCategoria.Text;
                objProducto.PrecioVenta = Convert.ToDecimal(txtPrecio.Text.Trim());
                objProducto.StockActual = string.IsNullOrWhiteSpace(txtStock.Text) ? 0 : Convert.ToInt32(txtStock.Text.Trim());

                if (cmbProveedor.SelectedValue != null)
                {
                    objProducto.IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                }

                objProducto.PorcentajeIva = 10;
                // --- ACTUALIZADO: Buscando la columna de IVA por su nuevo nombre ---
                if (objProducto.IdProducto > 0 && dgvProductos.CurrentRow != null &&
                    dgvProductos.Columns.Contains("colProductoIva") &&
                    dgvProductos.CurrentRow.Cells["colProductoIva"].Value != DBNull.Value)
                {
                    objProducto.PorcentajeIva = Convert.ToInt32(dgvProductos.CurrentRow.Cells["colProductoIva"].Value);
                }

                bool exito = negocio.GuardarProducto(objProducto);

                if (exito)
                {
                    string accion = objProducto.IdProducto == 0 ? "Alta" : "Edición";
                    string detalle = objProducto.IdProducto == 0
                        ? $"Se registró el producto '{objProducto.Nombre}'."
                        : $"Se modificó el producto '{objProducto.Nombre}'.";

                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Inventario", accion, detalle);

                    MessageBox.Show("Producto guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                    CargarGrilla();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingresá solo números válidos en Precio y Stock.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Seleccione un producto de la grilla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(txtId.Text);

                // --- ACTUALIZADO: Leyendo el estado desde la columna correcta ---
                string estadoActual = dgvProductos.CurrentRow.Cells["colProductoEstado"].Value.ToString();

                string nuevoEstado = estadoActual == "Activo" ? "Inactivo" : "Activo";
                string mensaje = estadoActual == "Activo" ? "¿Desea dar de baja (desactivar) este producto?" : "¿Desea reactivar este producto?";

                DialogResult result = MessageBox.Show(mensaje, "Confirmar Cambio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool exito = negocio.CambiarEstado(id, nuevoEstado);

                    if (exito)
                    {
                        GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Inventario", "Cambio de Estado", $"Cambió el estado del producto ID {id} a {nuevoEstado}.");
                        MessageBox.Show($"El estado del producto se cambió a: {nuevoEstado}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();
                        CargarGrilla();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtId.Clear();
            txtCodigo.Clear();
            txtNombre.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;

            if (cmbIva != null) cmbIva.SelectedIndex = -1;

            txtPrecio.Clear();
            txtStock.Clear();
            picFoto.Image = null;
            rutaFotoOrigen = "";

            txtBuscarProducto.Clear();
            dgvProductos.ClearSelection();
        }

        private void frmGestionProductos_Click(object sender, EventArgs e)
        {
            dgvProductos.ClearSelection();
            LimpiarFormulario();
        }

        private void dgvProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                // --- ACTUALIZADO: Pintado de celdas referenciando a las nuevas columnas ---
                if (row.Cells["colProductoStock"].Value != DBNull.Value && row.Cells["colProductoStockMin"].Value != DBNull.Value)
                {
                    int stockActual = Convert.ToInt32(row.Cells["colProductoStock"].Value);
                    int stockMinimo = Convert.ToInt32(row.Cells["colProductoStockMin"].Value);

                    if (stockActual == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.IndianRed;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if (stockActual <= stockMinimo)
                    {
                        row.DefaultCellStyle.BackColor = Color.Khaki;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
            dgvProductos.ClearSelection();
        }

        private void ConfigurarAutocompletado()
        {
            if (dtProductos == null) return;
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
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    constructor.Append(c);
            }
            return constructor.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}