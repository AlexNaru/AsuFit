using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmRegistrarUsuario : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTORES
        private Usuario usuarioAEditar = null;
        private bool puedeGuardar = false;

        // Inicializa el formulario en contexto de inserción de un nuevo registro para el flujo principal.
        public frmRegistrarUsuario()
        {
            InitializeComponent();
            btnCancelar.Visible = false;
            lblPreguntaSeguridad.Text = "¿Palabra o numero de seguridad?";
        }

        // Inicializa el formulario en modo de diálogo modal, exponiendo controles de cancelación.
        public frmRegistrarUsuario(bool esVentanaEmergente)
        {
            InitializeComponent();
            btnCancelar.Visible = true;
            lblPreguntaSeguridad.Text = "¿Palabra o numero de seguridad?";
        }

        // Inicializa el formulario en contexto de modificación, adaptando la interfaz para la edición de una entidad existente.
        public frmRegistrarUsuario(Usuario usuarioAEditar)
        {
            InitializeComponent();
            this.usuarioAEditar = usuarioAEditar;
            btnCancelar.Visible = true;
            btnGuardar.Text = "ACTUALIZAR DATOS";
            lblPreguntaSeguridad.Text = "¿Palabra o numero de seguridad?";

            CargarDatosEnPantalla();
        }
        #endregion

        #region 2. CICLO DE VIDA DEL FORMULARIO Y CARGA DE DATOS
        // Orquesta la configuración geométrica, inyección de paletas cromáticas y suscripción de eventos de seguridad al renderizar la vista.
        private void frmRegistrarUsuario_Load(object sender, EventArgs e)
        {
            ConfigurarTemaOscuro();
            ConfigurarComportamientoComboBox();
            SuscribirFiltrosDeSeguridad();
            CambiarEstadoBoton(false);

            // Sincroniza el encabezado no elegible si no se encuentra en modo edición
            if (usuarioAEditar == null && cmbRol.Items.Count > 0)
            {
                cmbRol.SelectedIndex = 0;
            }
        }

        // Intercepta la finalización del renderizado para forzar el foco inicial en el primer control de entrada.
        private void frmRegistrarUsuario_Shown(object sender, EventArgs e)
        {
            txtNombreCompleto.Focus();
        }

        // Vuelca la estructura de la entidad en memoria hacia los controles correspondientes, omitiendo datos sensibles por seguridad.
        private void CargarDatosEnPantalla()
        {
            if (usuarioAEditar != null)
            {
                txtNombreCompleto.Text = usuarioAEditar.NombreCompleto;
                txtUsername.Text = usuarioAEditar.Username;
                cmbRol.Text = usuarioAEditar.Rol;
                txtEmail.Text = usuarioAEditar.Email;

                // Se omite la carga de la respuesta de seguridad para forzar la reescritura de la credencial encriptada.
                txtRespuesta.Text = "";

                chkActivo.Checked = (usuarioAEditar.Estado == "Activo");
            }
        }
        #endregion

        #region 3. GESTIÓN DE SEGURIDAD Y RESTRICCIONES DE ENTRADA
        // Suscribe programáticamente todos los controles a sus respectivos filtros de sanitización e intercepción de inyecciones.
        private void SuscribirFiltrosDeSeguridad()
        {
            txtNombreCompleto.KeyPress += txtAlfabetico_KeyPress;
            txtUsername.KeyPress += txtUsername_KeyPress;
            txtPassword.KeyPress += txtSinEspacios_KeyPress;
            txtConfirmarPassword.KeyPress += txtSinEspacios_KeyPress;
            txtRespuesta.KeyPress += txtAntiInyeccion_KeyPress;

            // FIX: Instanciamos un menú contextual en blanco para anular el clic derecho nativo de Windows
            ContextMenuStrip menuVacio = new ContextMenuStrip();

            foreach (Control grp in this.Controls)
            {
                if (grp is GroupBox)
                {
                    foreach (Control txt in grp.Controls)
                    {
                        if (txt is TextBox textBox)
                        {
                            textBox.KeyDown += BloquearPegado_KeyDown;
                            textBox.ContextMenuStrip = menuVacio; // Bloquea la opción de "Pegar" con el mouse
                        }
                    }
                }
            }
        }

        // Invalida la ejecución de combinaciones de teclado orientadas a la inserción forzada de datos provenientes del portapapeles.
        private void BloquearPegado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
            }
        }

        // Aplica exclusión a caracteres no pertenecientes al alfabeto, permitiendo separadores de espacio convencionales.
        private void txtAlfabetico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Limita el ingreso exclusivamente a combinaciones alfanuméricas y caracteres separadores técnicos aprobados.
        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '_')
            {
                e.Handled = true;
            }
        }

        // Rechaza el ingreso de caracteres de espacio en blanco para prevenir credenciales inválidas.
        private void txtSinEspacios_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Neutraliza caracteres reservados de T-SQL para mitigar vulnerabilidades de inyección en campos de texto libre.
        private void txtAntiInyeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'' || e.KeyChar == '"' || e.KeyChar == ';')
            {
                e.Handled = true;
            }
        }
        #endregion

        #region 4. ESTILOS VISUALES Y RENDERIZADO
        // Despacha la configuración de la paleta corporativa sobre el contenedor raíz.
        private void ConfigurarTemaOscuro()
        {
            this.BackColor = Color.FromArgb(25, 28, 35);
            AplicarTemaOscuroRecursivo(this);
        }

        // Itera recursivamente sobre el árbol de controles inyectando las propiedades cromáticas y tipográficas del diseño.
        private void AplicarTemaOscuroRecursivo(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Panel || c is GroupBox)
                {
                    c.BackColor = Color.FromArgb(25, 28, 35);
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
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                    btn.Height = 35;

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

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c);
            }
        }

        // Fuerza la inmutabilidad de la estructura del selector de roles y retira enfoques residuales.
        private void ConfigurarComportamientoComboBox()
        {
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRol.DropDownClosed += (s, e) => this.BeginInvoke(new Action(() => this.ActiveControl = null));
        }
        #endregion

        #region 5. NAVEGACIÓN Y FOCO
        // Evalúa de forma concurrente la integridad de la estructura de datos obligatoria para habilitar transacciones.
        private void VerificarCamposObligatorios(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombreCompleto.Text) &&
                !string.IsNullOrWhiteSpace(txtUsername.Text) &&
                !string.IsNullOrWhiteSpace(txtPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtConfirmarPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !string.IsNullOrWhiteSpace(txtRespuesta.Text) &&
                cmbRol.SelectedIndex != 0)
            {
                CambiarEstadoBoton(true);
            }
            else
            {
                CambiarEstadoBoton(false);
            }
        }

        // Intercepta la pulsación de la tecla Enter para gestionar el salto secuencial entre campos de entrada de la UI.
        private void NavegacionEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Control ctrlActivo = sender as Control;

                if (ctrlActivo != null && ctrlActivo.Name == "chkActivo")
                {
                    txtEmail.Focus();
                    return;
                }

                TextBox txtActivo = sender as TextBox;

                if (txtActivo != null)
                {
                    if (string.IsNullOrWhiteSpace(txtActivo.Text))
                    {
                        MessageBox.Show("Este campo es obligatorio y no puede estar vacío.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (txtActivo.Name == "txtConfirmarPassword")
                    {
                        if (txtPassword.Text != txtConfirmarPassword.Text)
                        {
                            MessageBox.Show("Las contraseñas no coinciden. Por favor volvé a ingresarlas.", "Error de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtConfirmarPassword.Clear();
                            txtConfirmarPassword.Focus();
                            return;
                        }

                        cmbRol.Focus();
                        cmbRol.DroppedDown = true;
                        return;
                    }
                }

                this.SelectNextControl(ctrlActivo, true, true, true, true);
            }
        }

        // Transfiere el enfoque operativo al componente de activación lógica tras consolidar el rol.
        private void cmbRol_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkActivo.Focus();
        }

        // Resguarda el flujo direccional del carro cuando se interactúa desde el combo desplegable.
        private void cmbRol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                chkActivo.Focus();
            }
        }

        // Muta la disponibilidad interactiva de confirmación basándose en el cumplimiento de las restricciones formales.
        private void CambiarEstadoBoton(bool activo)
        {
            puedeGuardar = activo;
            btnGuardar.Cursor = activo ? Cursors.Hand : Cursors.Default;
        }
        #endregion

        #region 6. LÓGICA DE VALIDACIÓN Y PERSISTENCIA
        // Empaqueta los datos de la interfaz y delega el flujo completo a la capa de Negocio para su validación y persistencia.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!puedeGuardar)
            {
                MessageBox.Show("Por favor, completá todos los campos obligatorios.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text != txtConfirmarPassword.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor verificalas.", "Error de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarPassword.Focus();
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            bool exito = false;
            string mensajeError = string.Empty;

            if (usuarioAEditar == null)
            {
                Usuario nuevo = new Usuario();
                nuevo.NombreCompleto = txtNombreCompleto.Text.Trim();
                nuevo.Username = txtUsername.Text.Trim();
                nuevo.Password = AsuFit.Negocio.SeguridadHelper.HashearContrasena(txtPassword.Text.Trim());
                nuevo.Rol = cmbRol.Text;
                nuevo.Email = txtEmail.Text.Trim();
                nuevo.RespuestaSeguridad = AsuFit.Negocio.SeguridadHelper.HashearContrasena(txtRespuesta.Text.Trim());
                nuevo.PreguntaSeguridad = lblPreguntaSeguridad.Text;
                nuevo.Estado = chkActivo.Checked ? "Activo" : "Inactivo";

                exito = negocio.RegistrarUsuario(nuevo, out mensajeError);

                if (exito)
                {
                    MessageBox.Show("¡Usuario registrado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (this.Modal) this.Close();
                    else
                    {
                        txtNombreCompleto.Clear();
                        txtUsername.Clear();
                        txtPassword.Clear();
                        txtConfirmarPassword.Clear();
                        txtEmail.Clear();
                        txtRespuesta.Clear();
                        cmbRol.SelectedIndex = -1;
                        txtNombreCompleto.Focus();
                    }
                }
            }
            else
            {
                usuarioAEditar.NombreCompleto = txtNombreCompleto.Text.Trim();
                usuarioAEditar.Username = txtUsername.Text.Trim();
                usuarioAEditar.Password = AsuFit.Negocio.SeguridadHelper.HashearContrasena(txtPassword.Text.Trim());
                usuarioAEditar.Rol = cmbRol.Text;
                usuarioAEditar.Email = txtEmail.Text.Trim();
                usuarioAEditar.RespuestaSeguridad = AsuFit.Negocio.SeguridadHelper.HashearContrasena(txtRespuesta.Text.Trim());
                usuarioAEditar.PreguntaSeguridad = lblPreguntaSeguridad.Text;
                usuarioAEditar.Estado = chkActivo.Checked ? "Activo" : "Inactivo";

                exito = negocio.EditarUsuario(usuarioAEditar, out mensajeError);

                if (exito)
                {
                    MessageBox.Show("Usuario actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

            if (!exito)
            {
                MessageBox.Show(mensajeError, "Aviso de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Finaliza el ciclo de vida del diálogo liberando recursos instanciados sin persistir cambios.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 7. INTERACCIÓN DE CONTROLES VISUALES
        // Modifica el estado del enmascaramiento criptográfico a nivel visual e intercambia la representación icónica asociada.
        private void AlternarVisibilidad(TextBox cajaTexto, PictureBox ojito)
        {
            cajaTexto.UseSystemPasswordChar = !cajaTexto.UseSystemPasswordChar;
            ojito.Image = cajaTexto.UseSystemPasswordChar ? Properties.Resources.ojo_cerrado : Properties.Resources.ojo_abierto;
        }

        private void picMostrarPass_Click(object sender, EventArgs e)
        {
            AlternarVisibilidad(txtPassword, picMostrarPass);
        }

        private void picMostrarPass_DoubleClick(object sender, EventArgs e)
        {
            AlternarVisibilidad(txtPassword, picMostrarPass);
        }

        private void picMostrarConfirmPass_Click(object sender, EventArgs e)
        {
            AlternarVisibilidad(txtConfirmarPassword, picMostrarConfirmPass);
        }

        private void picMostrarConfirmPass_DoubleClick(object sender, EventArgs e)
        {
            AlternarVisibilidad(txtConfirmarPassword, picMostrarConfirmPass);
        }

        private void picMostrarRespuesta_Click(object sender, EventArgs e)
        {
            AlternarVisibilidad(txtRespuesta, picMostrarRespuesta);
        }

        private void picMostrarRespuesta_DoubleClick(object sender, EventArgs e)
        {
            AlternarVisibilidad(txtRespuesta, picMostrarRespuesta);
        }
        #endregion

        #region 8. GESTIÓN DE PERMISOS DE USUARIO
        // Impide la elevación de privilegios no autorizada y la autodesactivación de la cuenta en sesión mediante bloqueos de UI.
        public void BloquearPermisosParaEmpleado()
        {
            cmbRol.Enabled = false;
            chkActivo.Enabled = false;
        }
        #endregion
    }
}