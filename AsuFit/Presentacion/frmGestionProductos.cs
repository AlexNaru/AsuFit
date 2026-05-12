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
        }

        // Capas de negocio conectadas
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

            // EL SECRETO DE INGRESO DE MERCADERÍA:
            // 1. Primero se cargan las sugerencias
            ConfigurarAutocompletado();
            // 2. Después se pinta el texto de fondo gris
            SendMessage(txtBuscarProducto.Handle, EM_SETCUEBANNER, 1, "Buscar producto...");
            // 3. Foco a la caja de texto
            txtBuscarProducto.Focus();
        }

        // --- 1. CONFIGURACIÓN INICIAL ---
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
                dv.RowFilter = "Estado = 'Activo'"; // Solo mostramos los activos en el combo

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

        // --- 2. FILTROS Y GRILLA ---
        private void FiltrarDatos()
        {
            if (dtProductos == null) return;

            string filtroEstado = chkMostrarInactivos.Checked ? "Estado = 'Inactivo'" : "Estado = 'Activo'";

            // Lectura directa de lo que escribe el usuario
            string textoBusqueda = QuitarAcentos(txtBuscarProducto.Text.Trim()).ToLower().Replace("'", "''");

            string filtroFinal = filtroEstado;

            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                filtroFinal = $"{filtroEstado} AND NombreBusqueda LIKE '%{textoBusqueda}%'";
            }

            DataView dv = dtProductos.DefaultView;
            dv.RowFilter = filtroFinal;
            dgvProductos.DataSource = dv;

            if (dgvProductos.Columns.Contains("Estado")) dgvProductos.Columns["Estado"].Visible = false;
            if (dgvProductos.Columns.Contains("NombreBusqueda")) dgvProductos.Columns["NombreBusqueda"].Visible = false;
            if (dgvProductos.Columns.Contains("IdProducto")) dgvProductos.Columns["IdProducto"].Visible = false;
            if (dgvProductos.Columns.Contains("IdProveedor")) dgvProductos.Columns["IdProveedor"].Visible = false;

            dgvProductos.ClearSelection();
        }

        private void CargarGrilla()
        {
            dtProductos = negocio.ListarProductos();

            if (dtProductos != null)
            {
                // Crear columna oculta sin acentos para búsquedas
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

                dgvProductos.AllowUserToAddRows = false;
                dgvProductos.RowHeadersVisible = false;
                dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProductos.ReadOnly = true;

                if (dgvProductos.Columns.Contains("PrecioVenta"))
                    dgvProductos.Columns["PrecioVenta"].DefaultCellStyle.Format = "N0";

                if (dgvProductos.Columns.Contains("Proveedor"))
                    dgvProductos.Columns["Proveedor"].HeaderText = "Proveedor";
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

        // --- 3. LÓGICA DE GESTIÓN (CRUD) ---
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                txtId.Text = fila.Cells["IdProducto"].Value.ToString();
                txtCodigo.Text = fila.Cells["CodigoBarras"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                cmbCategoria.Text = fila.Cells["Categoria"].Value.ToString();
                txtPrecio.Text = Convert.ToDecimal(fila.Cells["PrecioVenta"].Value).ToString("N0");
                txtStock.Text = fila.Cells["StockActual"].Value.ToString();

                // Auto-seleccionar el proveedor
                if (fila.Cells["Proveedor"].Value != DBNull.Value && fila.Cells["Proveedor"].Value != null)
                {
                    cmbProveedor.Text = fila.Cells["Proveedor"].Value.ToString();
                }
                else
                {
                    cmbProveedor.SelectedIndex = -1;
                }

                // --- NUEVO: LEER EL IVA DESDE LA GRILLA AL COMBOBOX VISUAL ---
                if (dgvProductos.Columns.Contains("PorcentajeIva") && fila.Cells["PorcentajeIva"].Value != DBNull.Value)
                {
                    cmbIva.Text = fila.Cells["PorcentajeIva"].Value.ToString();
                }
                else
                {
                    cmbIva.SelectedIndex = -1;
                }

                string codigo = fila.Cells["CodigoBarras"].Value.ToString();
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
            try
            {
                if (txtCodigo.Text == "" || txtNombre.Text == "" || txtPrecio.Text == "" || txtStock.Text == "" || cmbCategoria.Text == "" || cmbProveedor.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, complete todos los campos obligatorios, incluyendo el Proveedor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = txtId.Text == "" ? 0 : Convert.ToInt32(txtId.Text);
                string codigo = txtCodigo.Text;
                decimal precio = Convert.ToDecimal(txtPrecio.Text);
                int stock = Convert.ToInt32(txtStock.Text);
                int idProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);

                // --- SOLUCIÓN AL ERROR CS7036 ---
                // Leemos el IVA de la grilla (si existe), si no, asumimos 10% por defecto.
                int ivaAsignado = 10;
                if (id > 0 && dgvProductos.CurrentRow != null && dgvProductos.Columns.Contains("PorcentajeIva") && dgvProductos.CurrentRow.Cells["PorcentajeIva"].Value != DBNull.Value)
                {
                    ivaAsignado = Convert.ToInt32(dgvProductos.CurrentRow.Cells["PorcentajeIva"].Value);
                }

                // Ahora enviamos los 8 parámetros sin problema
                bool exito = negocio.GuardarProducto(id, codigo, txtNombre.Text, cmbCategoria.Text, precio, stock, idProveedor, ivaAsignado);

                if (exito)
                {
                    string accion = (id == 0) ? "Nuevo Producto" : "Edición";
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Inventario", accion, $"Guardó el producto '{txtNombre.Text}' (Cod: {codigo}).");

                    if (rutaFotoOrigen != "")
                    {
                        string rutaFinal = carpetaFotos + codigo + ".jpg";

                        if (File.Exists(rutaFinal))
                        {
                            picFoto.Image = null;
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            File.Delete(rutaFinal);
                        }

                        File.Copy(rutaFotoOrigen, rutaFinal);
                        rutaFotoOrigen = "";
                    }

                    MessageBox.Show("Producto guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarGrilla();
                }
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
                string estadoActual = dgvProductos.CurrentRow.Cells["Estado"].Value.ToString();

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

            // --- LÍNEA NUEVA PARA LIMPIAR EL IVA ---
            if (cmbIva != null) cmbIva.SelectedIndex = -1;

            txtPrecio.Clear();
            txtStock.Clear();
            picFoto.Image = null;
            rutaFotoOrigen = "";

            // Limpieza normal como en el otro formulario
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
                int stockActual = Convert.ToInt32(row.Cells["StockActual"].Value);
                int stockMinimo = Convert.ToInt32(row.Cells["StockMinimo"].Value);

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