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

            // Restricción física de longitud para Cédulas de Identidad paraguayas
            txtCedula.MaxLength = 8;

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
        // Muestra una alerta temporal de color en pantalla completa, bloquea el teclado y detalla la validación de acceso al usuario.
        private async void MostrarAlerta(Color colorFondo, string mensaje, string nombre, string plan, string vencimiento)
        {
            txtCedula.Enabled = false;
            txtCedula.Visible = false;

            pnlEstado.BackColor = colorFondo;

            Color colorTexto = (colorFondo == Color.Gold || colorFondo == Color.Yellow) ? Color.Black : Color.White;

            lblMensaje.Text = mensaje.ToUpper();
            lblMensaje.ForeColor = colorTexto;
            lblMensaje.Visible = true;

            lblNombre.Text = nombre.ToUpper();
            lblNombre.ForeColor = colorTexto;
            lblNombre.Visible = true;

            if (!string.IsNullOrEmpty(plan))
            {
                lblTipoPlan.Text = "PLAN: " + plan.ToUpper();
                lblTipoPlan.ForeColor = colorTexto;
                lblTipoPlan.Visible = true;
            }
            else
            {
                lblTipoPlan.Visible = false;
            }

            lblVencimiento.Text = vencimiento;
            lblVencimiento.ForeColor = colorTexto;
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

            lblVencimiento.Text = "Ingresá tu número de cédula y presioná ENTER";
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
        // Recupera el registro del socio de la base de datos, evalúa fechas de vencimiento y audita la entrada en el historial.
        private void ProcesarAcceso(string cedula)
        {
            SocioNegocio negocio = new SocioNegocio();
            Socio socio = negocio.BuscarSocioPorCedula(cedula);

            if (socio == null)
            {
                MostrarAlerta(Color.DarkOrange, "Socio no encontrado", "", "", "Por favor, pasá por recepción para registrarte.");
                return;
            }

            if (socio.Estado != "Activo")
            {
                MostrarAlerta(Color.DarkRed, "Acceso Denegado", $"{socio.Nombre} {socio.Apellido}", "", "Tu usuario se encuentra inactivo.");
                return;
            }

            if (!socio.FechaVencimiento.HasValue)
            {
                MostrarAlerta(Color.Crimson, "Sin Plan Activo", $"{socio.Nombre} {socio.Apellido}", "", "No registrás membresías vigentes. Pasá por caja.");
                return;
            }

            string nombreCompleto = $"{socio.Nombre} {socio.Apellido}";
            string fechaVenceTexto = socio.FechaVencimiento.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (socio.FechaVencimiento.Value.Date >= DateTime.Now.Date)
            {
                Asistencia nuevaAsistencia = new Asistencia();
                nuevaAsistencia.IdSocio = socio.IdSocio;

                AsistenciaNegocio negocioAsistencia = new AsistenciaNegocio();
                negocioAsistencia.RegistrarAsistencia(nuevaAsistencia);

                int diasRestantes = (socio.FechaVencimiento.Value.Date - DateTime.Now.Date).Days;
                string detalleVencimiento = $"Quedan {diasRestantes} días de plan. Vence el: {fechaVenceTexto}";

                if (diasRestantes == 1)
                {
                    MostrarAlerta(Color.Gold, "¡Acceso Permitido! (Próximo a vencer)", nombreCompleto, socio.NombrePlan, detalleVencimiento);
                }
                else
                {
                    MostrarAlerta(Color.ForestGreen, "¡Acceso Permitido!", nombreCompleto, socio.NombrePlan, detalleVencimiento);
                }
            }
            else
            {
                MostrarAlerta(Color.Crimson, "Plan Vencido", nombreCompleto, socio.NombrePlan, $"Tu plan venció el día: {fechaVenceTexto}. Pasá por caja.");
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