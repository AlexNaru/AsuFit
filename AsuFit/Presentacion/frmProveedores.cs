using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmProveedores : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private DataTable dtProveedores;
        private ProveedorNegocio negocio = new ProveedorNegocio();
        private Usuario usuarioActual;

        public frmProveedores(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;
            dgvProveedores.AutoGenerateColumns = false;
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmProveedores_Load(object sender, EventArgs e)
        {
            ConfigurarTemaOscuro();
            CargarGrilla();

            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;

            SuscribirFiltrosDeSeguridad();

            // Aplicamos el placeholder interactivo estilo AsuFit
            AplicarPlaceholder(txtBuscarProveedor, "Buscar por Nombre o RUC...");

            // Liberamos el foco para que se vea el placeholder
            this.ActiveControl = null;
        }

        private void CargarGrilla()
        {
            try
            {
                dtProveedores = negocio.ListarProveedores();
                if (dtProveedores != null)
                {
                    FiltrarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Base de Datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 3. ESTILOS VISUALES (UI)
        private void ConfigurarTemaOscuro()
        {
            float fuenteGlobal = Properties.Settings.Default.TamanoFuente;

            this.BackColor = Color.FromArgb(25, 28, 35);

            AplicarTemaOscuroRecursivo(this, fuenteGlobal);
            ConfigurarTemaOscuroGrilla(dgvProveedores, fuenteGlobal);
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
                else if (c is CheckBox chk)
                {
                    chk.ForeColor = Color.White;
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

                    if (btn.Name.Contains("Limpiar") || btn.Name.Contains("Cancelar"))
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

            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

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

        #region 4. SECCIÓN IZQUIERDA: CATÁLOGO Y BÚSQUEDA
        private void FiltrarDatos()
        {
            if (dtProveedores == null) return;

            string filtroEstado = chkMostrarInactivos.Checked ? "Estado = 'Inactivo'" : "Estado = 'Activo'";
            string textoBusqueda = txtBuscarProveedor.Text == "Buscar por Nombre o RUC..." ? "" : txtBuscarProveedor.Text.Trim();

            string filtroFinal = filtroEstado;

            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                string busquedaSegura = textoBusqueda.Replace("'", "''");
                filtroFinal = $"{filtroEstado} AND (Nombre LIKE '%{busquedaSegura}%' OR RUC LIKE '%{busquedaSegura}%')";
            }

            DataView dv = dtProveedores.DefaultView;
            dv.RowFilter = filtroFinal;
            dgvProveedores.DataSource = dv;

            ActualizarResumen();
            dgvProveedores.ClearSelection();
        }

        private void txtBuscarProveedor_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarProveedor.Text != "Buscar por Nombre o RUC...")
                FiltrarDatos();
        }

        private void chkMostrarInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void dgvProveedores_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvProveedores.ClearSelection();
        }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProveedores.Rows[e.RowIndex];

                txtId.Text = fila.Cells["colProvId"].Value.ToString();
                txtRuc.Text = fila.Cells["colProvRuc"].Value.ToString();
                txtNombre.Text = fila.Cells["colProvNombre"].Value.ToString();
                cmbCategoria.Text = fila.Cells["colProvCategoria"].Value.ToString();
                txtContacto.Text = fila.Cells["colProvContacto"].Value.ToString();
                txtTelefono.Text = fila.Cells["colProvTelefono"].Value.ToString();
                txtCorreo.Text = fila.Cells["colProvCorreo"].Value.ToString();
                txtDireccion.Text = fila.Cells["colProvDireccion"].Value.ToString();
                txtCiudad.Text = fila.Cells["colProvCiudad"].Value.ToString();
                chkActivo.Checked = (fila.Cells["colProvEstado"].Value.ToString() == "Activo");
            }
        }
        #endregion

        #region 5. SECCIÓN DERECHA SUPERIOR: DETALLES DEL PROVEEDOR Y ACCIONES
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtRuc.Text))
            {
                MessageBox.Show("El Nombre y el RUC son campos obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Proveedor objProveedor = new Proveedor();
                objProveedor.IdProveedor = string.IsNullOrWhiteSpace(txtId.Text) ? 0 : Convert.ToInt32(txtId.Text);
                objProveedor.Nombre = txtNombre.Text.Trim();
                objProveedor.RUC = txtRuc.Text.Trim();
                objProveedor.Categoria = cmbCategoria.Text;
                objProveedor.Contacto = txtContacto.Text.Trim();
                objProveedor.Telefono = txtTelefono.Text.Trim();
                objProveedor.Correo = txtCorreo.Text.Trim();
                objProveedor.Direccion = txtDireccion.Text.Trim();
                objProveedor.Ciudad = txtCiudad.Text.Trim();
                objProveedor.Estado = chkActivo.Checked ? "Activo" : "Inactivo";

                bool exito = negocio.GuardarProveedor(objProveedor);

                if (exito)
                {
                    string accion = objProveedor.IdProveedor == 0 ? "Alta" : "Edición";
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Proveedores", accion, $"Se gestionó al proveedor '{objProveedor.Nombre}'.");
                    MessageBox.Show("Proveedor guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Por favor, seleccione un proveedor de la lista primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idProveedor = Convert.ToInt32(txtId.Text);
                string nuevoEstadoTexto = chkActivo.Checked ? "Inactivo" : "Activo";

                if (negocio.CambiarEstado(idProveedor, nuevoEstadoTexto))
                {
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Proveedores", "Cambio de Estado", $"Estado del proveedor ID {idProveedor} a {nuevoEstadoTexto}.");
                    MessageBox.Show($"El estado del proveedor ha sido cambiado a: {nuevoEstadoTexto}.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar el estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtId.Clear();
            txtRuc.Clear();
            txtNombre.Clear();
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
            txtContacto.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            txtCiudad.Clear();
            chkActivo.Checked = true;

            dgvProveedores.ClearSelection();

            // Preserva la lectura pasiva del buscador moderno
            txtBuscarProveedor.Text = "";
            this.ActiveControl = null;
        }

        private void LimpiarSeleccion_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        // --- EVENTOS PARA QUITAR EL AZUL DEL COMBOBOX ---
        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => this.ActiveControl = null));
        }

        private void cmbCategoria_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => this.ActiveControl = null));
        }
        #endregion

        #region 6. SECCIÓN DERECHA INFERIOR: RESUMEN
        private void ActualizarResumen()
        {
            if (dtProveedores == null) return;

            int total = dtProveedores.Rows.Count;
            int activos = 0;
            int inactivos = 0;

            foreach (DataRow row in dtProveedores.Rows)
            {
                if (row["Estado"].ToString() == "Activo") activos++;
                else inactivos++;
            }

            lblTotal.Text = total.ToString();
            lblActivos.Text = activos.ToString();
            lblInactivos.Text = inactivos.ToString();
        }
        #endregion

        #region 7. GESTIÓN DE SEGURIDAD Y RESTRICCIONES DE ENTRADA
        // Suscribe programáticamente todos los controles a sus filtros de sanitización transaccional.
        private void SuscribirFiltrosDeSeguridad()
        {
            txtRuc.KeyPress += txtRuc_KeyPress;
            txtTelefono.KeyPress += txtTelefono_KeyPress;
            txtCorreo.KeyPress += txtEmail_KeyPress;

            txtNombre.KeyPress += txtAntiInyeccion_KeyPress;
            txtContacto.KeyPress += txtAntiInyeccion_KeyPress;
            txtDireccion.KeyPress += txtAntiInyeccion_KeyPress;
            txtCiudad.KeyPress += txtAntiInyeccion_KeyPress;
            txtBuscarProveedor.KeyPress += txtAntiInyeccion_KeyPress;

            ContextMenuStrip menuVacio = new ContextMenuStrip();
            foreach (Control contenedor in this.Controls)
            {
                AsignarBloqueosRecursivo(contenedor, menuVacio);
            }
        }

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

        private void BloquearPegado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
                e.Handled = true;
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '+')
                e.Handled = true;
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '@' && e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != '_')
                e.Handled = true;
        }

        private void txtAntiInyeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'' || e.KeyChar == '"' || e.KeyChar == ';')
                e.Handled = true;
        }
        #endregion
    }
}