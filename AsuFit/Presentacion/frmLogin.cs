using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsuFit.Negocio;   // Para conectar con el puente
using AsuFit.Entidades; // Para reconocer al objeto Usuario
using AsuFit.Datos;

namespace AsuFit.Presentacion
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // 1. Instanciamos la capa de negocio
            UsuarioNegocio objNegocio = new UsuarioNegocio();

            // 2. Intentamos el login (usamos Trim() para evitar errores por espacios accidentales)
            Usuario user = objNegocio.Loguear(txtUsername.Text.Trim(), txtPassword.Text.Trim());

            // 3. Verificamos si los datos son correctos
            if (user != null)
            {
                // --- LÍNEA DE AUDITORÍA: REGISTRO DE LOGIN ---
                GestorAuditoria.Registrar(user.NombreCompleto, "Seguridad", "Inicio de Sesión", "El usuario ingresó al sistema.");

                MessageBox.Show($"¡Bienvenido a AsuFit, {user.NombreCompleto}!", "Acceso Concedido",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ocultamos la pantalla de Login
                this.Hide();

                // IMPORTANTE: Aquí llamaremos a tu Dashboard. 
                // Es buena idea pasarle el objeto 'user' para que el Dashboard sepa quién entró y qué rol tiene.
                frmDashboard pantallaPrincipal = new frmDashboard(user);
                pantallaPrincipal.Show();
            }
            else
            {
                // Si el usuario no existe, la clave es mal o está inactivo
                MessageBox.Show("Usuario o contraseña incorrectos, o el usuario está Inactivo.",
                                "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Esto cierra toda la aplicación por completo
        }

        private void lnkRecuperarAcceso_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRecuperarAcceso ventanaRecuperar = new frmRecuperarAcceso();
            ventanaRecuperar.ShowDialog();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Esta es la forma segura de dar foco antes de que la ventana sea visible
            this.ActiveControl = txtUsername;

            txtPassword.Enabled = false; // Contraseña visible pero bloqueada [cite: 14]
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            // Si apretó Enter...
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el sonido "ding" de Windows

                // Validamos que no esté vacío
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("El campo de usuario no puede estar vacío.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Si está bien, habilitamos la contraseña y le pasamos el foco
                    txtPassword.Enabled = true;
                    txtPassword.Focus();
                }
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // El botón se habilita SOLO si la contraseña tiene algún texto
            btnIngresar.Enabled = !string.IsNullOrWhiteSpace(txtPassword.Text);
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                // Si el botón está habilitado (es decir, la contraseña no está vacía)
                if (btnIngresar.Enabled)
                {
                    btnIngresar.Focus();
                }
                else
                {
                    MessageBox.Show("Debes ingresar una contraseña.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
