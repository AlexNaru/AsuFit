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

        public frmRegistrarUsuario()
        {
            InitializeComponent();
            btnCancelar.Visible = false;
            lblPreguntaSeguridad.Text = "¿Palabra o numero de seguridad?";
        }

        public frmRegistrarUsuario(bool esVentanaEmergente)
        {
            InitializeComponent();
            btnCancelar.Visible = true;
            lblPreguntaSeguridad.Text = "¿Palabra o numero de seguridad?";
        }

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

        #region 2. INICIALIZACIÓN DE PANTALLA
        private void frmRegistrarUsuario_Load(object sender, EventArgs e)
        {
            ConfigurarTemaOscuro(); // Inyectamos la paleta de colores básica
            CambiarEstadoBoton(false);

            // Aseguramos que al cargar, las contraseñas estén ocultas por defecto
            txtPassword.UseSystemPasswordChar = true;
            txtConfirmarPassword.UseSystemPasswordChar = true;
            txtRespuesta.UseSystemPasswordChar = true;
        }

        private void frmRegistrarUsuario_Shown(object sender, EventArgs e)
        {
            txtNombreCompleto.Focus();
        }

        private void CargarDatosEnPantalla()
        {
            if (usuarioAEditar != null)
            {
                txtNombreCompleto.Text = usuarioAEditar.NombreCompleto;
                txtUsername.Text = usuarioAEditar.Username;
                cmbRol.Text = usuarioAEditar.Rol;
                txtEmail.Text = usuarioAEditar.Email;
                txtRespuesta.Text = usuarioAEditar.RespuestaSeguridad;

                chkActivo.Checked = (usuarioAEditar.Estado == "Activo");
            }
        }
        #endregion

        #region 3. ESTILOS VISUALES (TEMA OSCURO BÁSICO)
        private void ConfigurarTemaOscuro()
        {
            // El tamaño de la fuente y la escala ya son controlados por el Dashboard o el form padre.
            // Aquí solo nos encargamos de que los colores hagan juego con el sistema.
            this.BackColor = Color.FromArgb(25, 28, 35);

            AplicarTemaOscuroRecursivo(this);
        }

        private void AplicarTemaOscuroRecursivo(Control contenedor)
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
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                    btn.Height = 35; // Estándar de botones AsuFit

                    if (btn.Name.Contains("Cancelar"))
                    {
                        btn.BackColor = Color.FromArgb(50, 55, 65); // Gris para cancelar
                        btn.ForeColor = Color.White;
                    }
                    else
                    {
                        btn.BackColor = Color.FromArgb(0, 229, 255); // Cian AsuFit para Guardar
                        btn.ForeColor = Color.Black;
                    }
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c);
            }
        }
        #endregion

        #region 4. CAMPOS DEL FORMULARIO Y NAVEGACIÓN VERTICAL
        private void VerificarCamposObligatorios(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombreCompleto.Text) &&
                !string.IsNullOrWhiteSpace(txtUsername.Text) &&
                !string.IsNullOrWhiteSpace(txtPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtConfirmarPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !string.IsNullOrWhiteSpace(txtRespuesta.Text) &&
                cmbRol.SelectedIndex != -1)
            {
                CambiarEstadoBoton(true);
            }
            else
            {
                CambiarEstadoBoton(false);
            }
        }

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

                    if (txtActivo.Name == "txtEmail")
                    {
                        string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                        if (!Regex.IsMatch(txtEmail.Text, patronEmail))
                        {
                            MessageBox.Show("Por favor, ingresá un correo válido (debe contener '@' y un punto).", "Validación de Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtEmail.Focus();
                            return;
                        }
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

        private void cmbRol_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkActivo.Focus();
        }

        private void cmbRol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                chkActivo.Focus();
            }
        }

        private void CambiarEstadoBoton(bool activo)
        {
            puedeGuardar = activo;
            btnGuardar.Cursor = activo ? Cursors.Hand : Cursors.Default;
        }
        #endregion

        #region 5. BOTONES INFERIORES: GUARDAR Y CANCELAR
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

            string patronMail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(txtEmail.Text, patronMail))
            {
                MessageBox.Show("Por favor, ingresá un correo válido.", "Validación de Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            int idActual = usuarioAEditar != null ? usuarioAEditar.IdUsuario : 0;

            if (negocio.ExisteUsername(txtUsername.Text.Trim(), idActual))
            {
                MessageBox.Show("El nombre de usuario (Username) ya está en uso. Elegí otro.", "Usuario Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
                return;
            }

            bool exito = false;
            string mensajeError = string.Empty;

            if (usuarioAEditar == null)
            {
                Usuario nuevo = new Usuario();
                nuevo.NombreCompleto = txtNombreCompleto.Text.Trim();
                nuevo.Username = txtUsername.Text.Trim();
                nuevo.Password = txtPassword.Text.Trim();
                nuevo.Rol = cmbRol.Text;
                nuevo.Email = txtEmail.Text.Trim();
                nuevo.RespuestaSeguridad = txtRespuesta.Text.Trim();
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
                usuarioAEditar.Password = txtPassword.Text.Trim();
                usuarioAEditar.Rol = cmbRol.Text;
                usuarioAEditar.Email = txtEmail.Text.Trim();
                usuarioAEditar.RespuestaSeguridad = txtRespuesta.Text.Trim();
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
                MessageBox.Show(mensajeError, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 6. MOSTRAR / OCULTAR CONTRASEÑAS Y RESPUESTA
        // Método maestro que hace la magia para cualquier caja y cualquier ojito
        private void AlternarVisibilidad(TextBox cajaTexto, PictureBox ojito)
        {
            cajaTexto.UseSystemPasswordChar = !cajaTexto.UseSystemPasswordChar;
            ojito.Image = cajaTexto.UseSystemPasswordChar ? Properties.Resources.ojo_cerrado : Properties.Resources.ojo_abierto;
        }

        // --- 1. CONTRASEÑA PRINCIPAL ---
        private void picMostrarPass_Click(object sender, EventArgs e)
        {
            AlternarVisibilidad(txtPassword, picMostrarPass);
        }
        private void picMostrarPass_DoubleClick(object sender, EventArgs e)
        {
            AlternarVisibilidad(txtPassword, picMostrarPass); // Soluciona el clic rápido
        }

        // --- 2. CONFIRMAR CONTRASEÑA ---
        private void picMostrarConfirmPass_Click(object sender, EventArgs e)
        {
            AlternarVisibilidad(txtConfirmarPassword, picMostrarConfirmPass);
        }
        private void picMostrarConfirmPass_DoubleClick(object sender, EventArgs e)
        {
            AlternarVisibilidad(txtConfirmarPassword, picMostrarConfirmPass);
        }

        // --- 3. RESPUESTA DE SEGURIDAD ---
        private void picMostrarRespuesta_Click(object sender, EventArgs e)
        {
            AlternarVisibilidad(txtRespuesta, picMostrarRespuesta);
        }
        private void picMostrarRespuesta_DoubleClick(object sender, EventArgs e)
        {
            AlternarVisibilidad(txtRespuesta, picMostrarRespuesta);
        }
        #endregion
    }
}