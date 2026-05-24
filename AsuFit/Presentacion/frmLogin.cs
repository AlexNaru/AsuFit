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
        #region 1. VARIABLES Y CONSTRUCTOR

        // Bandera de control para evitar que el usuario haga doble clic rápido en "Ingresar"
        private bool iniciandoSesion = false;

        public frmLogin()
        {
            InitializeComponent();

            // Fuerzo el redibujado en memoria para evitar parpadeos visuales
            this.DoubleBuffered = true;

            // 1. Hago que la ventana nazca invisible (0% opacidad) para ocultar el escalado del logo
            this.Opacity = 0;

            // 2. Enlazo el evento Shown para que se ejecute justo cuando termine de cargar todo
            this.Shown += new EventHandler(frmLogin_Shown);
        }

        #endregion

        #region 2. CARGA DEL FORMULARIO Y DISEÑO

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // 1. CONGELAMOS la interfaz gráfica para que no se vean los tirones de tamaño
            this.SuspendLayout();

            // 2. Aplicamos la escala y ajustamos las fuentes proporcionales
            this.Scale(new SizeF(1.6f, 1.6f));
            AjustarFuentes(this, 1.6f);
            this.CenterToScreen();

            // 3. Foco inicial en el usuario
            this.ActiveControl = txtUsername;
            txtPassword.Enabled = false;

            // 4. DESCONGELAMOS la interfaz para que se dibuje de golpe
            this.ResumeLayout(false);
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            // 5. Como ya se escaló y acomodó todo en las sombras, muestro la ventana al 100% de opacidad
            this.Opacity = 1;
        }

        // Metodo recursivo para multiplicar el tamaño de letra de todos los controles
        private void AjustarFuentes(Control contenedor, float factor)
        {
            foreach (Control c in contenedor.Controls)
            {
                c.Font = new Font(c.Font.FontFamily, c.Font.Size * factor, c.Font.Style);

                // Si hay controles agrupados (como dentro del Panel oscuro), entro a revisarlos también
                if (c.HasChildren)
                {
                    AjustarFuentes(c, factor);
                }
            }
        }

        #endregion

        #region 3. EVENTOS DE LOS CAMPOS DE TEXTO (USUARIO Y CONTRASEÑA)

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            // Al presionar Enter, paso el foco a la contraseña
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Quito el sonido de error de Windows

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
            // Habilito el boton de ingresar solo si ya escribieron algo en la clave
            if (!iniciandoSesion)
            {
                btnIngresar.Enabled = !string.IsNullOrWhiteSpace(txtPassword.Text);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            // Al presionar Enter en la contraseña, simulo el clic en INICIAR SESION
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

        private void pictureBoxOjo_Click(object sender, EventArgs e)
        {
            // Alternar visibilidad de la contraseña
            if (txtPassword.UseSystemPasswordChar == true)
            {
                // Muestro el texto y cambio al ícono de ojo abierto
                txtPassword.UseSystemPasswordChar = false;
                pictureBoxOjo.Image = Properties.Resources.ojo_abierto;
            }
            else
            {
                // Oculto el texto y cambio al ícono de ojo cerrado/tachado
                txtPassword.UseSystemPasswordChar = true;
                pictureBoxOjo.Image = Properties.Resources.ojo_cerrado;
            }
        }

        private void pictureBoxOjo_DoubleClick(object sender, EventArgs e)
        {
            // Si el usuario hace doble clic muy rápido, fuerzo a que actúe como un clic normal
            pictureBoxOjo_Click(sender, e);
        }

        #endregion

        #region 4. BOTONES Y ACCIONES PRINCIPALES

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Bloqueo multiples clics
            if (iniciandoSesion) return;

            iniciandoSesion = true;
            btnIngresar.Enabled = false;

            // Feedback visual de carga
            string textoOriginal = btnIngresar.Text;
            btnIngresar.Text = "Conectando...";
            Application.DoEvents(); // Fuerzo el redibujado de la UI para que se vea el "Conectando..."

            try
            {
                // Valido credenciales contra la BD
                UsuarioNegocio objNegocio = new UsuarioNegocio();
                Usuario user = objNegocio.Loguear(txtUsername.Text.Trim(), txtPassword.Text.Trim());

                if (user != null)
                {
                    // Registro el ingreso en auditoria
                    GestorAuditoria.Registrar(user.NombreCompleto, "Seguridad", "Inicio de Sesión", "El usuario ingresó al sistema.");

                    this.Hide();

                    // Levanto el sistema principal
                    frmDashboard pantallaPrincipal = new frmDashboard(user);
                    pantallaPrincipal.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos, o el usuario está Inactivo.",
                                    "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Limpio para reintentar
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
            // Levanto la ventana de recuperacion de clave
            frmRecuperarAcceso ventanaRecuperar = new frmRecuperarAcceso();
            ventanaRecuperar.ShowDialog();
        }

        #endregion

        #region 5. EVENTOS DEL SISTEMA

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Mato el proceso si cierran desde la 'X' de la ventana
            Application.Exit();
        }

        #endregion
    }
}