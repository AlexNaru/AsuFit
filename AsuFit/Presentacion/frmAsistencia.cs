using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmAsistencia : Form
    {
        #region 1. CONSTRUCTOR Y VARIABLES GLOBALES
        // Inicializa los componentes de la interfaz de asistencia (Modo Kiosco)
        public frmAsistencia()
        {
            InitializeComponent();
        }
        #endregion

        #region 2. CICLO DE VIDA Y CONFIGURACIÓN INICIAL
        // Gestiona la carga de la ventana, aplicando el escalado, restricciones de seguridad y activando el reloj del sistema.
        private void frmAsistencia_Load(object sender, EventArgs e)
        {
            AplicarEscalaKiosco();
            LimpiarPantalla();

            // Anulación del menú nativo de Windows (Inhabilita pegado mediante clic derecho)
            txtCedula.ContextMenuStrip = new ContextMenuStrip();

            timerReloj.Start();
        }

        // Aplica un multiplicador de resolución para adaptar la interfaz a pantallas de gran formato (ej. TV 50").
        private void AplicarEscalaKiosco()
        {
            float factorEscala = 1.5f;

            this.Scale(new SizeF(factorEscala, factorEscala));
            AjustarFuentesEImagen(this, factorEscala);
        }

        // Ejecuta un recorrido recursivo en el árbol de controles para escalar tipografías, excluyendo imágenes para evitar pixelación.
        private void AjustarFuentesEImagen(Control contenedor, float factor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c.Font != null)
                {
                    c.Font = new Font(c.Font.FontFamily, c.Font.Size * factor, c.Font.Style);
                }

                if (c is PictureBox pic)
                {
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                }

                if (c.HasChildren) AjustarFuentesEImagen(c, factor);
            }
        }

        // Restituye de forma forzosa el foco al campo de entrada una vez que el formulario ha sido completamente renderizado.
        private void frmAsistencia_Shown(object sender, EventArgs e)
        {
            txtCedula.Focus();
        }
        #endregion

        #region 3. GESTIÓN VISUAL DE ALERTAS Y ESTADOS
        // Muestra una alerta temporal utilizando tipografía de acento sobre fondo oscuro, manteniendo la estética premium.
        private async void MostrarAlerta(Color colorEstado, string mensaje, string nombre, string plan, string vencimiento)
        {
            txtCedula.Enabled = false;
            txtCedula.Visible = false;

            // MANTENEMOS EL FONDO OSCURO SIEMPRE
            pnlEstado.BackColor = Color.FromArgb(35, 39, 47);

            // 1. TÍTULO: Usa el color de la alerta (Verde, Rojo, etc.)
            lblMensaje.Text = mensaje.ToUpper();
            lblMensaje.ForeColor = colorEstado;
            lblMensaje.Visible = true;

            // 2. NOMBRE: Blanco puro para destacar a la persona
            lblNombre.Text = nombre.ToUpper();
            lblNombre.ForeColor = Color.White;
            lblNombre.Visible = true;

            // 3. PLAN: Gris plata
            if (!string.IsNullOrEmpty(plan))
            {
                lblTipoPlan.Text = "PLAN: " + plan.ToUpper();
                lblTipoPlan.ForeColor = Color.Silver;
                lblTipoPlan.Visible = true;
            }
            else
            {
                lblTipoPlan.Visible = false;
            }

            // 4. VENCIMIENTO: Gris plata
            lblVencimiento.Text = vencimiento;
            lblVencimiento.ForeColor = Color.Silver;
            lblVencimiento.Visible = true;

            CentrarControles();

            // Retardo asíncrono para mantener la alerta en pantalla
            await Task.Delay(5000);

            if (this.IsDisposed) return;

            LimpiarPantalla();
        }

        // Restaura los parámetros visuales iniciales de AsuFit y reactiva la captura de datos tras la finalización de una alerta.
        private void LimpiarPantalla()
        {
            pnlEstado.BackColor = Color.FromArgb(35, 39, 47);

            lblMensaje.Text = "¡BIENVENIDO/A!";
            lblMensaje.ForeColor = Color.FromArgb(0, 229, 255);
            lblMensaje.Visible = true;

            lblNombre.Text = "";
            lblNombre.Visible = false;

            lblTipoPlan.Text = "";
            lblTipoPlan.Visible = false;

            lblVencimiento.Text = "Ingresa tu número de cédula y presiona ENTER";
            lblVencimiento.ForeColor = Color.Silver;
            lblVencimiento.Visible = true;

            txtCedula.Clear();
            txtCedula.Enabled = true;
            txtCedula.Visible = true;

            CentrarControles();
            txtCedula.Focus();
        }

        // Modifica la propiedad geométrica "Left" de los controles para mantener un alineamiento central horizontal constante.
        private void CentrarControles()
        {
            lblMensaje.Left = (pnlEstado.Width - lblMensaje.Width) / 2;
            lblNombre.Left = (pnlEstado.Width - lblNombre.Width) / 2;
            lblTipoPlan.Left = (pnlEstado.Width - lblTipoPlan.Width) / 2;
            lblVencimiento.Left = (pnlEstado.Width - lblVencimiento.Width) / 2;
            txtCedula.Left = (pnlEstado.Width - txtCedula.Width) / 2;
        }
        #endregion

        #region 4. CAPTURA DE DATOS Y SEGURIDAD (LECTOR / TECLADO)
        // Filtro estricto (White-list): Solo permite la inserción de caracteres numéricos y la tecla de retroceso (Backspace).
        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        // Intercepta la tecla ENTER para disparar el flujo de acceso y bloquea atajos de teclado del portapapeles.
        private void txtCedula_KeyDown(object sender, KeyEventArgs e)
        {
            // Bloquea el intento de pegar datos (Ctrl + V / Shift + Insert)
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (string.IsNullOrWhiteSpace(txtCedula.Text)) return;

                ProcesarAcceso(txtCedula.Text.Trim());
            }
        }
        #endregion

        #region 5. LÓGICA DE NEGOCIO Y REGLAS DE ACCESO
        // Delega la evaluación de acceso a la capa de Negocio y formatea la respuesta visual según el dictamen.
        private void ProcesarAcceso(string cedula)
        {
            SocioNegocio negocio = new SocioNegocio();
            string detalleVencimiento = "";

            Socio socio = negocio.BuscarSocioPorCedula(cedula);

            // Delegación de lógica algorítmica y temporal a la capa de negocio
            int estadoAcceso = negocio.EvaluarAccesoSocio(socio, out detalleVencimiento);

            if (estadoAcceso == -1)
            {
                MostrarAlerta(Color.DarkOrange, "Socio no encontrado", "", "", "Por favor, pasá por recepción para registrarte.");
                return;
            }

            string nombreCompleto = $"{socio.Nombre} {socio.Apellido}";

            if (estadoAcceso == 0)
            {
                MostrarAlerta(Color.DarkRed, "Acceso Denegado", nombreCompleto, socio.NombrePlan, "Tu usuario está inactivo o sin membresía vigente.");
                return;
            }

            if (estadoAcceso == -2)
            {
                MostrarAlerta(Color.Crimson, "Plan Vencido", nombreCompleto, socio.NombrePlan, detalleVencimiento);
                return;
            }

            // Registro silencioso de asistencia para accesos válidos (Estado 1 o 2)
            negocio.RegistrarAsistencia(socio.IdSocio);

            if (estadoAcceso == 2)
            {
                MostrarAlerta(Color.Gold, "¡Acceso Permitido! (Próximo a vencer)", nombreCompleto, socio.NombrePlan, detalleVencimiento);
            }
            else
            {
                MostrarAlerta(Color.ForestGreen, "¡Acceso Permitido!", nombreCompleto, socio.NombrePlan, detalleVencimiento);
            }
        }
        #endregion

        #region 6. SINCRONIZACIÓN DE RELOJ (UI)
        // Ejecuta la actualización periódica de las etiquetas de tiempo en la interfaz utilizando la localización local.
        private void timerReloj_Tick(object sender, EventArgs e)
        {
            CultureInfo idioma = new CultureInfo("es-ES");
            lblHora.Text = "HORA: " + DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = "FECHA: " + DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy", idioma).ToUpper();
        }
        #endregion
    }
}