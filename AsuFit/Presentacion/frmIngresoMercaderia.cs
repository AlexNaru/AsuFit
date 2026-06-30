using System;
using AsuFit.Entidades;
using AsuFit.Datos;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AsuFit.Negocio;
using System.Text;
using System.Globalization;

namespace AsuFit.Presentacion
{
    public partial class frmIngresoMercaderia : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private InventarioNegocio negocio = new InventarioNegocio();
        private ProveedorNegocio negocioProveedor = new ProveedorNegocio();
        private IngresoMercaderiaNegocio negocioIngreso = new IngresoMercaderiaNegocio();
        private DataTable dtProductos;
        private Usuario usuarioActual;

        public frmIngresoMercaderia(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;
            dgvProductos.AutoGenerateColumns = false;
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmIngresoMercaderia_Load(object sender, EventArgs e)
        {
            ConfigurarTemaOscuro();
            CargarProveedores();
            CargarGrilla();
            ConfigurarAutocompletado();

            SuscribirFiltrosDeSeguridad();

            // Aplicamos el placeholder interactivo estilo AsuFit
            AplicarPlaceholder(txtBuscarProducto, "Buscar producto en el catálogo...");

            // Liberamos el foco para que se vea el placeholder
            this.ActiveControl = null;
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
            (dgvProductos.DataSource as DataTable).DefaultView.RowFilter = "Estado = 'Activo'";
            dgvProductos.ClearSelection();
        }
        #endregion

        #region 3. ESTILOS VISUALES (UI)
        private void ConfigurarTemaOscuro()
        {
            float fuenteGlobal = Properties.Settings.Default.TamanoFuente;

            this.BackColor = Color.FromArgb(25, 28, 35);

            AplicarTemaOscuroRecursivo(this, fuenteGlobal);
            ConfigurarTemaOscuroGrilla(dgvProductos, fuenteGlobal);
        }

        private void AplicarTemaOscuroRecursivo(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Panel || c is GroupBox)
                {
                    c.BackColor = Color.FromArgb(35, 39, 47);
                    c.ForeColor = Color.White;
                }
                else if (c is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                }
                else if (c is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(50, 55, 65);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (c is ComboBox cmb)
                {
                    cmb.BackColor = Color.FromArgb(50, 55, 65);
                    cmb.ForeColor = Color.White;
                    cmb.FlatStyle = FlatStyle.Flat;
                }
                else if (c is Button btn)
                {
                    btn.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;

                    if (btn.Name.Contains("Cancelar") || btn.Name.Contains("Limpiar"))
                    {
                        btn.BackColor = Color.FromArgb(50, 55, 65);
                        btn.ForeColor = Color.White;
                    }
                    else
                    {
                        btn.BackColor = Color.FromArgb(0, 229, 255);
                        btn.ForeColor = Color.Black;
                    }
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c, fuente);
            }
        }

        private void ConfigurarTemaOscuroGrilla(DataGridView dgv, float fuente)
        {
            dgv.BackgroundColor = Color.FromArgb(25, 28, 35);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(50, 55, 65);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", fuente, FontStyle.Bold);

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
        #endregion

        #region 4. MÉTODOS AUXILIARES DE FORMULARIO EMERGENTE
        private void PrepararFormularioComoDashboard(Form frm)
        {
            float escalaActual = Properties.Settings.Default.EscalaInterfaz;
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            frm.Scale(new SizeF(escalaActual, escalaActual));
            AjustarFuentesPopup(frm, fuenteActual);

            frm.StartPosition = FormStartPosition.Manual;

            if (this.Parent != null)
            {
                Point posicionPanelAbsoluta = this.Parent.PointToScreen(Point.Empty);
                int x = posicionPanelAbsoluta.X + (this.Parent.Width - frm.Width) / 2;
                int y = posicionPanelAbsoluta.Y + (this.Parent.Height - frm.Height) / 2;

                frm.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterParent;
            }
        }

        private void AjustarFuentesPopup(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is TextBox || c is ComboBox || c is Label || c is NumericUpDown || c is Button)
                {
                    if (c is Button) c.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
                    else c.Font = new Font("Segoe UI", fuente, c.Font.Style);
                }
                if (c.HasChildren) AjustarFuentesPopup(c, fuente);
            }
        }
        #endregion

        #region 5. SECCIÓN IZQUIERDA: BÚSQUEDA Y SELECCIÓN
        private void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            frmAgregarProveedor frmPopup = new frmAgregarProveedor();
            PrepararFormularioComoDashboard(frmPopup);
            frmPopup.ShowDialog();

            CargarProveedores();
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            frmNuevoProducto frmPopup = new frmNuevoProducto(usuarioActual);
            PrepararFormularioComoDashboard(frmPopup);
            frmPopup.ShowDialog();

            CargarGrilla();
            ConfigurarAutocompletado();

            if (frmPopup.ProductoRecienCreado != "")
            {
                txtBuscarProducto.Text = frmPopup.ProductoRecienCreado;
                txtBuscarProducto.ForeColor = Color.White; // Aseguramos color al asignar
            }
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            if (dtProductos == null) return;

            string textoBusqueda = txtBuscarProducto.Text;

            // Si el texto es el de ayuda, buscamos en blanco para mostrar todo
            if (textoBusqueda == "Buscar producto en el catálogo...") textoBusqueda = "";

            string textoLimpio = QuitarAcentos(textoBusqueda).ToLower();
            string filtro = "Estado = 'Activo' AND NombreBusqueda LIKE '%" + textoLimpio + "%'";
            (dgvProductos.DataSource as DataTable).DefaultView.RowFilter = filtro;
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

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

        private void dgvProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvProductos.ClearSelection();
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
        #endregion

        #region 6. SECCIÓN CENTRAL Y DERECHA: DETALLES, RESUMEN Y CÁLCULOS
        private void ActualizarResumen()
        {
            int cantidad = 0;
            decimal costoTotal = 0;

            string textoCantidad = txtCantidadIngreso.Text.Replace(".", "").Trim();
            string textoCosto = txtCostoTotal.Text.Replace(".", "").Trim();

            if (!string.IsNullOrWhiteSpace(textoCantidad)) int.TryParse(textoCantidad, out cantidad);
            if (!string.IsNullOrWhiteSpace(textoCosto)) decimal.TryParse(textoCosto, out costoTotal);

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
            txtResumenProducto.Text = txtIdProductoSeleccionado.Text != "" ? lblDetalleProducto.Text.Replace("Producto: ", "") : "";
            txtResumenCantidad.Text = cantidad.ToString();
            txtResumenTotal.Text = costoTotal > 0 ? "Gs. " + costoTotal.ToString("N0") : "Gs. 0";

            if (dgvProductos.CurrentRow != null && txtIdProductoSeleccionado.Text != "")
            {
                int stockActual = Convert.ToInt32(dgvProductos.CurrentRow.Cells["colIngresoStock"].Value);
                int nuevoStock = stockActual + cantidad;
                txtResumenNuevoStock.Text = nuevoStock.ToString();
            }
            else
            {
                txtResumenNuevoStock.Text = "0";
            }
        }

        private void txtCantidadIngreso_TextChanged(object sender, EventArgs e) { ActualizarResumen(); }
        private void txtCostoTotal_TextChanged(object sender, EventArgs e) { ActualizarResumen(); }
        private void cmbProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarResumen();

            this.ActiveControl = null;
        }

        // Libera el foco del componente de forma asíncrona mitigando selecciones residuales del sistema operativo.
        private void cmbProveedores_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => this.ActiveControl = null));
        }
        #endregion

        #region 7. ACCIONES INFERIORES: CONFIRMAR Y LIMPIAR
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

            // Restauramos el buscador a su estado original interactivo
            txtBuscarProducto.Text = "";
            txtBuscarProducto.Focus(); // Para forzar el evento Leave y que dibuje el color Silver
            this.ActiveControl = null;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LimpiarSeleccion_Click(object sender, EventArgs e)
        {
            dgvProductos.ClearSelection();
            txtIdProductoSeleccionado.Clear();
            btnLimpiar_Click(null, null);
        }
        #endregion

        #region 8. GESTIÓN DE SEGURIDAD Y RESTRICCIONES DE ENTRADA
        // Suscribe programáticamente todos los controles a sus filtros de sanitización y bloqueos físicos.
        private void SuscribirFiltrosDeSeguridad()
        {
            txtBuscarProducto.KeyPress += txtAntiInyeccion_KeyPress;
            txtCantidadIngreso.KeyPress += txtSoloNumeros_KeyPress;
            txtCostoTotal.KeyPress += txtSoloNumeros_KeyPress;

            ContextMenuStrip menuVacio = new ContextMenuStrip();

            foreach (Control contenedor in this.Controls)
            {
                AsignarBloqueosRecursivo(contenedor, menuVacio);
            }
        }

        // Inspecciona la jerarquía de la vista capturando TextBoxes en cualquier nivel de anidamiento.
        private void AsignarBloqueosRecursivo(Control contenedor, ContextMenuStrip menuVacio)
        {
            if (contenedor is TextBox txt)
            {
                txt.KeyDown += BloquearPegado_KeyDown;
                txt.ContextMenuStrip = menuVacio;
            }

            foreach (Control hijo in contenedor.Controls)
            {
                AsignarBloqueosRecursivo(hijo, menuVacio);
            }
        }

        // Invalida combinaciones de teclado orientadas a la inserción masiva desde el portapapeles.
        private void BloquearPegado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
            }
        }

        // Limita el ingreso de datos exclusivamente a secuencias numéricas y retroceso.
        private void txtSoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Neutraliza caracteres reservados de T-SQL para mitigar vulnerabilidades.
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