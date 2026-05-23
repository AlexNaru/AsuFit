using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsuFit.Negocio;
using AsuFit.Entidades;
using AsuFit.Datos;

namespace AsuFit.Presentacion
{
    public partial class frmLogin : Form
    {
        private bool iniciandoSesion = false;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (iniciandoSesion) return;

            iniciandoSesion = true;
            btnIngresar.Enabled = false;
            string textoOriginal = btnIngresar.Text;
            btnIngresar.Text = "Conectando...";
            Application.DoEvents();

            try
            {
                UsuarioNegocio objNegocio = new UsuarioNegocio();
                Usuario user = objNegocio.Loguear(txtUsername.Text.Trim(), txtPassword.Text.Trim());

                if (user != null)
                {
                    GestorAuditoria.Registrar(user.NombreCompleto, "Seguridad", "Inicio de Sesión", "El usuario ingresó al sistema.");

                    // EL ORDEN SEGURO:
                    // 1. Ocultamos el Login primero para limpiar la pantalla
                    this.Hide();

                    // 2. Mostramos tu mensaje de bienvenida personalizado
                    MessageBox.Show($"¡Bienvenido a AsuFit, {user.NombreCompleto}!",
                                    "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. Finalmente abrimos el Dashboard
                    frmDashboard pantallaPrincipal = new frmDashboard(user);
                    pantallaPrincipal.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos, o el usuario está Inactivo.",
                                    "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtPassword.Clear();
                    txtUsername.Focus();

                    iniciandoSesion = false;
                    btnIngresar.Text = textoOriginal;
                    btnIngresar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión con el servidor. Por favor, verifica tu conexión a internet (Puerto 1433) o intenta más tarde.\n\nDetalle técnico: " + ex.Message,
                                "Error de Red", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                iniciandoSesion = false;
                btnIngresar.Text = textoOriginal;
                btnIngresar.Enabled = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lnkRecuperarAcceso_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRecuperarAcceso ventanaRecuperar = new frmRecuperarAcceso();
            ventanaRecuperar.ShowDialog();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Scale(new SizeF(1.6f, 1.6f));
            this.CenterToScreen();

            this.ActiveControl = txtUsername;
            txtPassword.Enabled = false;
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("El campo de usuario no puede estar vacío.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    txtPassword.Enabled = true;
                    txtPassword.Focus();
                }
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!iniciandoSesion)
            {
                btnIngresar.Enabled = !string.IsNullOrWhiteSpace(txtPassword.Text);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (btnIngresar.Enabled && !iniciandoSesion)
                {
                    btnIngresar.PerformClick();
                }
                else if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Debes ingresar una contraseña.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}