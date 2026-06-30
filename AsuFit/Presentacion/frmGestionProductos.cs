using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmGestionProductos : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private Usuario usuarioActual;
        private InventarioNegocio negocio = new InventarioNegocio();
        private ProveedorNegocio negocioProveedor = new ProveedorNegocio();
        private DataTable dtProductos;

        private string rutaFotoOrigen = "";
        private const string carpetaFotos = @"C:\AsuFit_Fotos\";

        public frmGestionProductos(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;
            dgvProductos.AutoGenerateColumns = false;

            ConfigurarTemaOscuroGrilla(dgvProductos);
        }
        #endregion

        #region 2. ESTILOS VISUALES Y COMPORTAMIENTO UI
        // Aplica el estilo visual del sistema a la grilla
        private void ConfigurarTemaOscuroGrilla(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(25, 28, 35);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(50, 55, 65);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(25, 28, 35);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 229, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 35;
        }

        private void ConfigurarTextosDeAyuda()
        {
            AplicarPlaceholder(txtBuscarProducto, "Buscar por código o nombre...");
            AplicarPlaceholder(txtCodigo, "Ej: 7898000...");
            AplicarPlaceholder(txtNombre, "Ej: Energizante...");
            AplicarPlaceholder(txtPrecio, "0");
            AplicarPlaceholder(txtStock, "0");
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
                    string entradaUsuario = txt.Text.Replace(textoAyuda, "");
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

        // Evita que el placeholder se procese como un valor real al guardar o validar
        private string ObtenerTextoReal(TextBox txt)
        {
            if (txt.Text == (string)txt.Tag) return "";
            return txt.Text;
        }

        // Libera el foco del componente de forma asíncrona mitigando selecciones residuales del sistema operativo.
        private void QuitarFocoCombo_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => this.ActiveControl = null));
        }
        #endregion

        #region 3. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmGestionProductos_Load(object sender, EventArgs e)
        {
            ConfigurarTextosDeAyuda();
            ConfigurarFiltros();
            CargarProveedores();
            CargarGrilla();

            // Activamos los escudos de seguridad al cargar la ventana
            SuscribirFiltrosDeSeguridad();

            // Vinculación del evento para limpiar el resaltado azul al elegir una opción
            cmbCategoria.DropDownClosed += QuitarFocoCombo_DropDownClosed;
            cmbProveedor.DropDownClosed += QuitarFocoCombo_DropDownClosed;
            if (cmbIva != null) cmbIva.DropDownClosed += QuitarFocoCombo_DropDownClosed;

            if (!Directory.Exists(carpetaFotos)) Directory.CreateDirectory(carpetaFotos);

            dgvProductos.ClearSelection();
            dgvProductos.CurrentCell = null;

            ConfigurarAutocompletado();

            // Libera el foco para permitir visualizar los placeholders y colores correctos
            this.ActiveControl = null;
        }

        private void ConfigurarFiltros()
        {
            // Catálogo inmutable de categorías del gimnasio
            cmbCategoria.Items.Clear();
            cmbCategoria.Items.Add("Suplementos");
            cmbCategoria.Items.Add("Bebidas");
            cmbCategoria.Items.Add("Snacks");

            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;

            // Inicialización y blindaje del combo de IVA por si no se cargó en el diseñador
            if (cmbIva != null)
            {
                cmbIva.Items.Clear();
                cmbIva.Items.Add("10");
                cmbIva.Items.Add("5");
                cmbIva.Items.Add("0");

                if (cmbIva.Items.Count > 0) cmbIva.SelectedIndex = 0; // Selecciona '10' por defecto
            }
        }

        // Obtiene el directorio de proveedores activos y selecciona el primer registro hábil.
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

                if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        // Remueve tildes para facilitar la búsqueda en tiempo real
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
        #endregion

        #region 4. BÚSQUEDA Y GESTIÓN DE GRILLA
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
            }
        }

        private void FiltrarDatos()
        {
            if (dtProductos == null) return;

            string filtroEstado = chkMostrarInactivos.Checked ? "Estado = 'Inactivo'" : "Estado = 'Activo'";
            string textoBusqueda = QuitarAcentos(ObtenerTextoReal(txtBuscarProducto)).ToLower().Replace("'", "''");
            string filtroFinal = filtroEstado;

            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                filtroFinal = $"{filtroEstado} AND NombreBusqueda LIKE '%{textoBusqueda}%'";
            }

            DataView dv = dtProductos.DefaultView;
            dv.RowFilter = filtroFinal;
            dgvProductos.DataSource = dv;

            dgvProductos.ClearSelection();
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

                txtId.Text = fila.Cells["colProductoId"].Value.ToString();

                txtCodigo.Text = fila.Cells["colProductoCodigo"].Value.ToString();
                txtCodigo.ForeColor = Color.White;

                txtNombre.Text = fila.Cells["colProductoNombre"].Value.ToString();
                txtNombre.ForeColor = Color.White;

                cmbCategoria.Text = fila.Cells["colProductoCategoria"].Value.ToString();

                txtPrecio.Text = Convert.ToDecimal(fila.Cells["colProductoPrecioVenta"].Value).ToString("N0");
                txtPrecio.ForeColor = Color.White;

                txtStock.Text = fila.Cells["colProductoStock"].Value.ToString();
                txtStock.ForeColor = Color.White;

                if (fila.Cells["colProductoProveedor"].Value != DBNull.Value && fila.Cells["colProductoProveedor"].Value != null)
                {
                    cmbProveedor.Text = fila.Cells["colProductoProveedor"].Value.ToString();
                }
                else
                {
                    if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;
                }

                if (dgvProductos.Columns.Contains("colProductoIva") && fila.Cells["colProductoIva"].Value != DBNull.Value)
                {
                    cmbIva.Text = fila.Cells["colProductoIva"].Value.ToString();
                }
                else
                {
                    if (cmbIva != null && cmbIva.Items.Count > 0) cmbIva.SelectedIndex = 0;
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

        // Aplica formato condicional de alertas de inventario respetando la paleta del sistema
        private void dgvProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                if (row.Cells["colProductoStock"].Value != DBNull.Value && row.Cells["colProductoStockMin"].Value != DBNull.Value)
                {
                    int stockActual = Convert.ToInt32(row.Cells["colProductoStock"].Value);
                    int stockMinimo = Convert.ToInt32(row.Cells["colProductoStockMin"].Value);

                    if (stockActual == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if (stockActual <= stockMinimo)
                    {
                        row.DefaultCellStyle.BackColor = Color.Gold;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(25, 28, 35);
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
            dgvProductos.ClearSelection();
        }
        #endregion

        #region 5. ACCIONES DEL FORMULARIO (CRUD Y FOTO)
        private void btnSubirFoto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ObtenerTextoReal(txtCodigo)))
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
            string nombreReal = ObtenerTextoReal(txtNombre);
            string precioReal = ObtenerTextoReal(txtPrecio);
            string stockReal = ObtenerTextoReal(txtStock);

            if (string.IsNullOrWhiteSpace(nombreReal) || string.IsNullOrWhiteSpace(precioReal))
            {
                MessageBox.Show("El Nombre y el Precio son campos obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Producto objProducto = new Producto();
                objProducto.IdProducto = string.IsNullOrWhiteSpace(txtId.Text) ? 0 : Convert.ToInt32(txtId.Text);
                objProducto.CodigoBarras = ObtenerTextoReal(txtCodigo);
                objProducto.Nombre = nombreReal;
                objProducto.Categoria = cmbCategoria.Text;
                objProducto.PrecioVenta = Convert.ToDecimal(precioReal);
                objProducto.StockActual = string.IsNullOrWhiteSpace(stockReal) ? 0 : Convert.ToInt32(stockReal);

                if (cmbProveedor.SelectedValue != null)
                {
                    objProducto.IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                }

                objProducto.PorcentajeIva = 10;

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

                    // Guardado físico de la imagen en el directorio configurado
                    if (!string.IsNullOrEmpty(rutaFotoOrigen))
                    {
                        string rutaDestino = carpetaFotos + objProducto.CodigoBarras + ".jpg";
                        if (File.Exists(rutaDestino))
                        {
                            File.Delete(rutaDestino);
                        }
                        File.Copy(rutaFotoOrigen, rutaDestino);
                    }

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

        // Revierte las casillas de edición a su estado nominal preservando el término de búsqueda en la grilla.
        private void LimpiarFormulario()
        {
            txtId.Clear();
            txtCodigo.Clear();
            txtNombre.Clear();

            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
            if (cmbProveedor.Items.Count > 0) cmbProveedor.SelectedIndex = 0;
            if (cmbIva != null && cmbIva.Items.Count > 0) cmbIva.SelectedIndex = 0;

            txtPrecio.Clear();
            txtStock.Clear();
            picFoto.Image = null;
            rutaFotoOrigen = "";

            dgvProductos.ClearSelection();

            ConfigurarTextosDeAyuda();
            this.ActiveControl = null;
        }

        // Libera la selección al hacer clic en un área vacía del formulario
        private void frmGestionProductos_Click(object sender, EventArgs e)
        {
            dgvProductos.ClearSelection();
            LimpiarFormulario();
        }
        #endregion


        #region 6. GESTIÓN DE SEGURIDAD Y RESTRICCIONES DE ENTRADA
        // Suscribe programáticamente todos los controles a sus filtros y bloqueos
        private void SuscribirFiltrosDeSeguridad()
        {
            // 1. Filtros físicos de teclado
            txtPrecio.KeyPress += txtSoloNumeros_KeyPress;
            txtStock.KeyPress += txtSoloNumeros_KeyPress;
            txtCodigo.KeyPress += txtSoloNumeros_KeyPress; // El código de barras suele ser numérico

            txtNombre.KeyPress += txtAntiInyeccion_KeyPress;
            txtBuscarProducto.KeyPress += txtAntiInyeccion_KeyPress;

            // 2. Anulación del menú contextual nativo de Windows (Clic derecho)
            ContextMenuStrip menuVacio = new ContextMenuStrip();

            // 3. Recorremos el formulario para bloquear el atajo Ctrl+V y el ratón
            foreach (Control contenedor in this.Controls)
            {
                AsignarBloqueosRecursivo(contenedor, menuVacio);
            }
        }

        // Busca todas las cajas de texto sin importar en qué panel estén escondidas
        private void AsignarBloqueosRecursivo(Control contenedor, ContextMenuStrip menuVacio)
        {
            if (contenedor is TextBox txt)
            {
                txt.KeyDown += BloquearPegado_KeyDown;
                txt.ContextMenuStrip = menuVacio; // Adiós clic derecho
            }

            foreach (Control hijo in contenedor.Controls)
            {
                AsignarBloqueosRecursivo(hijo, menuVacio);
            }
        }

        // Invalida la ejecución de inserción desde el portapapeles
        private void BloquearPegado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
            }
        }

        // Restringe el campo para que solo acepte dígitos numéricos y la tecla de borrar
        private void txtSoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Neutraliza caracteres reservados de T-SQL para mitigar vulnerabilidades
        private void txtAntiInyeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'' || e.KeyChar == '"' || e.KeyChar == ';')
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}