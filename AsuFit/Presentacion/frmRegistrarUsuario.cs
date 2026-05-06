using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmRegistrarUsuario : Form
    {
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

        private void frmRegistrarUsuario_Load(object sender, EventArgs e)
        {
            CambiarEstadoBoton(false);
        }

        // --- LA SOLUCIÓN DEFINITIVA PARA EL FOCUS ---
        private void frmRegistrarUsuario_Shown(object sender, EventArgs e)
        {
            txtNombreCompleto.Focus();
        }

        private void CambiarEstadoBoton(bool activo)
        {
            puedeGuardar = activo;

            if (activo)
            {
                btnGuardar.Cursor = Cursors.Hand;
            }
            else
            {
                btnGuardar.Cursor = Cursors.Default;
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
                    // 1. VALIDACIÓN DE VACÍOS (AHORA TODOS SON OBLIGATORIOS)
                    if (string.IsNullOrWhiteSpace(txtActivo.Text))
                    {
                        MessageBox.Show("Este campo es obligatorio y no puede estar vacío.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 2. VALIDACIÓN TEMPRANA DEL EMAIL (AHORA OBLIGATORIO)
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

                    // 3. VALIDACIÓN TEMPRANA DE CONTRASEÑAS
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

        private void VerificarCamposObligatorios(object sender, EventArgs e)
        {
            // AHORA EL EMAIL TAMBIÉN ESTÁ EN LA LISTA ESTRICTA DE REQUISITOS
            if (!string.IsNullOrWhiteSpace(txtNombreCompleto.Text) &&
                !string.IsNullOrWhiteSpace(txtUsername.Text) &&
                !string.IsNullOrWhiteSpace(txtPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtConfirmarPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtEmail.Text) && // <-- Agregado
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

            // VALIDACIÓN DEL CORREO OBLIGATORIO AL GUARDAR
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
                nuevo.Email = txtEmail.Text.Trim(); // Ahora toma el valor real sin excepciones
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
                usuarioAEditar.Rol = cmbRol.Text;
                usuarioAEditar.Email = txtEmail.Text.Trim(); // Valor real
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
    }
}