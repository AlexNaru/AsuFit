using System;
using System.Drawing;
using System.Windows.Forms;
using AsuFit.Negocio;
using AsuFit.Entidades;
using AsuFit.Datos;

namespace AsuFit.Presentacion
{
    public partial class frmLogin : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private bool iniciandoSesion = false;
        private int intentosFallidos = 0;

        // Inicializa la instancia del formulario, configura el doble búfer de gráficos y prepara la transición de opacidad inicial.
        public frmLogin()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.Opacity = 0;
            this.Shown += new EventHandler(frmLogin_Shown);
        }
        #endregion

        #region 2. CARGA DEL FORMULARIO Y DISEÑO
        // Aplica el factor de escala, establece límites de seguridad en las entradas de texto y asigna el foco inicial al cargar la ventana.
        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.SuspendLayout();

            this.Scale(new SizeF(1.6f, 1.6f));
            AjustarFuentes(this, 1.6f);
            this.CenterToScreen();

            txtUsername.MaxLength = 50;
            txtPassword.MaxLength = 50;

            this.ActiveControl = txtUsername;

            // Bloqueo físico contra pegado y menú contextual
            SuscribirFiltrosDeSeguridad();

            this.ResumeLayout(false);
        }

        // Restablece la opacidad al 100% una vez que la ventana se ha renderizado completamente, evitando parpadeos visuales.
        private void frmLogin_Shown(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        // Recorre recursivamente los controles de la interfaz para aplicar un factor de multiplicación al tamaño de sus fuentes.
        private void AjustarFuentes(Control contenedor, float factor)
        {
            foreach (Control c in contenedor.Controls)
            {
                c.Font = new Font(c.Font.FontFamily, c.Font.Size * factor, c.Font.Style);
                if (c.HasChildren)
                {
                    AjustarFuentes(c, factor);
                }
            }
        }
        #endregion

        #region 3. SECCIÓN DE ENTRADA: USUARIO
        // Intercepta el tipeo del teclado para bloquear espacios y caracteres especiales, actuando como primera capa contra inyecciones SQL.
        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                return;
            }

            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Gestiona la navegación hacia el siguiente campo al presionar Enter, previa validación de contenido nulo o vacío.
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
                    txtPassword.Focus();
                }
            }
        }
        #endregion

        #region 4. SECCIÓN DE ENTRADA: CONTRASEÑA Y VISIBILIDAD
        // Actúa como guardia de seguridad UX: verifica que exista un usuario ingresado antes de permitir el foco en la contraseña.
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Por favor, ingresá primero tu usuario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Focus();
            }
        }

        // Gestiona el atajo de teclado para disparar el proceso de inicio de sesión al presionar Enter.
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (!iniciandoSesion)
                {
                    btnIngresar.PerformClick();
                }
            }
        }

        // Alterna la máscara de encriptación visual de la contraseña y actualiza el recurso gráfico del indicador visual.
        private void pictureBoxOjo_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            pictureBoxOjo.Image = txtPassword.UseSystemPasswordChar ? Properties.Resources.ojo_cerrado : Properties.Resources.ojo_abierto;
        }

        // Reutiliza la lógica de alternancia de visibilidad en caso de interacciones rápidas (doble clic) sobre el control.
        private void pictureBoxOjo_DoubleClick(object sender, EventArgs e)
        {
            pictureBoxOjo_Click(sender, e);
        }
        #endregion

        #region 5. BOTONES DE ACCIÓN Y NAVEGACIÓN
        // Ejecuta las validaciones pre-conexión, aplica el algoritmo SHA-256 a la credencial, verifica contra la base de datos y gestiona el flujo de acceso o bloqueo.
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (iniciandoSesion) return;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Debes ingresar tu usuario.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Falta ingresar la contraseña.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            iniciandoSesion = true;
            btnIngresar.Enabled = false;

            string textoOriginal = btnIngresar.Text;
            btnIngresar.Text = "Conectando...";
            Application.DoEvents();

            try
            {
                string passPlana = txtPassword.Text.Trim();
                string passHasheada = AsuFit.Negocio.SeguridadHelper.HashearContrasena(passPlana);

                UsuarioNegocio objNegocio = new UsuarioNegocio();
                Usuario user = objNegocio.Loguear(txtUsername.Text.Trim(), passHasheada);

                if (user != null)
                {
                    GestorAuditoria.Registrar(user.NombreCompleto, "Seguridad", "Inicio de Sesión", "El usuario ingresó al sistema.");

                    this.Hide();
                    frmDashboard pantallaPrincipal = new frmDashboard(user);
                    pantallaPrincipal.Show();
                }
                else
                {
                    intentosFallidos++;

                    if (intentosFallidos >= 3)
                    {
                        MessageBox.Show("Has superado el límite de 3 intentos fallidos por razones de seguridad. El sistema se cerrará.",
                                        "Alerta de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        GestorAuditoria.Registrar("Desconocido", "Seguridad", "Intento de Intrusión", $"Múltiples fallos con usuario: {txtUsername.Text.Trim()}");
                        Application.Exit();
                        return;
                    }

                    int intentosRestantes = 3 - intentosFallidos;
                    MessageBox.Show($"Usuario o contraseña incorrectos, o el usuario está Inactivo.\n\nTe quedan {intentosRestantes} intentos.",
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
                MessageBox.Show("Error de conexión con el servidor. Por favor, verifica tu red.\n\nDetalle técnico: " + ex.Message,
                                "Error de Red", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                iniciandoSesion = false;
                btnIngresar.Text = textoOriginal;
                btnIngresar.Enabled = true;
            }
        }

        // Finaliza el proceso principal de la aplicación desde la interfaz gráfica.
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Instancia y despliega el formulario modal para el restablecimiento de credenciales de usuario.
        private void lnkRecuperarAcceso_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRecuperarAcceso ventanaRecuperar = new frmRecuperarAcceso();
            ventanaRecuperar.ShowDialog();
        }
        #endregion

        #region 6. EVENTOS DEL SISTEMA
        // Garantiza que la finalización del proceso se ejecute de manera forzada si el formulario es cerrado desde el control nativo de Windows (X).
        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region 7. GESTIÓN DE SEGURIDAD Y RESTRICCIONES DE ENTRADA
        // Suscribe programáticamente las barreras de contención física para silenciar accesos del portapapeles
        private void SuscribirFiltrosDeSeguridad()
        {
            ContextMenuStrip menuVacio = new ContextMenuStrip();

            foreach (Control contenedor in this.Controls)
            {
                AsignarBloqueosRecursivo(contenedor, menuVacio);
            }
        }

        // Inspecciona la jerarquía visual neutralizando menús contextuales y comandos de pegado en cajas de texto
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

        // Intercepta e invalida accesos rápidos de inserción masiva desde el teclado
        private void BloquearPegado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
            }
        }
        #endregion
    }
}