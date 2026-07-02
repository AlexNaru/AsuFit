using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmAgregarProveedor : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTORES
        private ProveedorNegocio negocioProveedor = new ProveedorNegocio();

        public frmAgregarProveedor()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmAgregarProveedor_Load);
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmAgregarProveedor_Load(object sender, EventArgs e)
        {
            ConfigurarTemaOscuro();

            // Selección por defecto y prevención de foco remanente
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
            cmbCategoria.DropDownClosed += QuitarFocoCombo_DropDownClosed;

            SuscribirFiltrosDeSeguridad();

            CargarResumen();

            // Libera el foco para evitar contornos nativos de Windows
            this.ActiveControl = null;
        }

        private void CargarResumen()
        {
            try
            {
                DataTable dt = negocioProveedor.ListarProveedores();

                if (dt != null)
                {
                    int total = dt.Rows.Count;
                    int activos = 0;
                    int inactivos = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Estado"].ToString() == "Activo") activos++;
                        else inactivos++;
                    }

                    // Se verifica que los Labels existan antes de asignar
                    if (lblTotal != null) lblTotal.Text = total.ToString();
                    if (lblActivos != null) lblActivos.Text = activos.ToString();
                    if (lblInactivos != null) lblInactivos.Text = inactivos.ToString();
                }
            }
            catch (Exception)
            {
                // Fallback silencioso: Si hay error de red temporal, los contadores quedan en 0 
                // sin interrumpir la experiencia del usuario.
            }
        }
        #endregion

        #region 3. ESTILOS VISUALES (TEMA OSCURO)
        private void ConfigurarTemaOscuro()
        {
            float fuenteGlobal = Properties.Settings.Default.TamanoFuente;

            // Fondo general del formulario emergente
            this.BackColor = Color.FromArgb(25, 28, 35);

            AplicarTemaOscuroRecursivo(this, fuenteGlobal);
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
                else if (c is Label || c is CheckBox)
                {
                    c.ForeColor = Color.White;
                    c.Font = new Font("Segoe UI", fuente, c.Font.Style);
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
                else if (c is Button btn)
                {
                    btn.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;

                    if (btn.Name.Contains("Cancelar"))
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

        private void QuitarFocoCombo_DropDownClosed(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        #endregion

        #region 4. ACCIONES DEL FORMULARIO
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtRuc.Text))
            {
                MessageBox.Show("El Nombre y el RUC son campos obligatorios.", "Aviso de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Mapeo de la Entidad
                Proveedor objProveedor = new Proveedor();
                objProveedor.IdProveedor = 0;
                objProveedor.Nombre = txtNombre.Text.Trim();
                objProveedor.RUC = txtRuc.Text.Trim();
                objProveedor.Categoria = cmbCategoria.SelectedItem != null ? cmbCategoria.SelectedItem.ToString() : "";
                objProveedor.Contacto = txtContacto.Text.Trim();
                objProveedor.Telefono = txtTelefono.Text.Trim();
                objProveedor.Correo = txtCorreo.Text.Trim();
                objProveedor.Direccion = txtDireccion.Text.Trim();
                objProveedor.Ciudad = txtCiudad.Text.Trim();

                // Búsqueda dinámica del CheckBox de estado para mantener compatibilidad
                Control[] controlesEstado = this.Controls.Find("chkEstado", true);
                if (controlesEstado.Length > 0 && controlesEstado[0] is CheckBox chk)
                {
                    objProveedor.Estado = chk.Checked ? "Activo" : "Inactivo";
                }
                else
                {
                    objProveedor.Estado = "Activo"; // Fallback por defecto
                }

                // Delegación a la capa de negocio
                string mensajeError = "";
                bool exito = negocioProveedor.GuardarProveedor(objProveedor, out mensajeError);

                if (exito)
                {
                    MessageBox.Show("¡Proveedor registrado con éxito!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(mensajeError, "Aviso de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el proveedor: " + ex.Message, "Excepción Crítica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 5. GESTIÓN DE SEGURIDAD Y RESTRICCIONES
        private void SuscribirFiltrosDeSeguridad()
        {
            if (txtRuc != null) txtRuc.KeyPress += txtRuc_KeyPress;
            if (txtTelefono != null) txtTelefono.KeyPress += txtTelefono_KeyPress;
            if (txtCorreo != null) txtCorreo.KeyPress += txtEmail_KeyPress;

            if (txtNombre != null) txtNombre.KeyPress += txtAntiInyeccion_KeyPress;
            if (txtContacto != null) txtContacto.KeyPress += txtAntiInyeccion_KeyPress;
            if (txtDireccion != null) txtDireccion.KeyPress += txtAntiInyeccion_KeyPress;
            if (txtCiudad != null) txtCiudad.KeyPress += txtAntiInyeccion_KeyPress;

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
                txt.KeyDown += (s, e) => { if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert) e.SuppressKeyPress = true; };
                txt.ContextMenuStrip = menuVacio;
            }
            foreach (Control hijo in contenedor.Controls) AsignarBloqueosRecursivo(hijo, menuVacio);
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-') e.Handled = true;
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '+') e.Handled = true;
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '@' && e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != '_') e.Handled = true;
        }

        private void txtAntiInyeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'' || e.KeyChar == '"' || e.KeyChar == ';') e.Handled = true;
        }
        #endregion
    }
}