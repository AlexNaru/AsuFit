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
        #region 1. CONSTRUCTOR Y VARIABLES
        public frmAsistencia()
        {
            InitializeComponent();
        }
        #endregion

        #region 2. CICLO DE VIDA DEL FORMULARIO
        private void frmAsistencia_Load(object sender, EventArgs e)
        {
            AplicarEscalaKiosco();
            LimpiarPantalla();
            timerReloj.Start();
        }

        #region ESCALADO ESPECIAL (MODO KIOSCO)
        private void AplicarEscalaKiosco()
        {
            // Factor de aumento: 1.5f significa 50% más grande. 
            // Si en tu TV de 50" lo sigues viendo pequeño, súbelo a 2.0f (el doble) o 2.5f.
            float factorEscala = 1.5f;

            // 1. Escala el tamaño y la posición de todos los controles físicos
            this.Scale(new SizeF(factorEscala, factorEscala));

            // 2. Protege el Logo y escala las letras de forma recursiva
            AjustarFuentesEImagen(this, factorEscala);
        }

        private void AjustarFuentesEImagen(Control contenedor, float factor)
        {
            foreach (Control c in contenedor.Controls)
            {
                // Aumenta la tipografía de forma proporcional
                if (c.Font != null)
                {
                    c.Font = new Font(c.Font.FontFamily, c.Font.Size * factor, c.Font.Style);
                }

                // PROTECCIÓN DE LA IMAGEN: Evita que el logo se rompa o se corte
                if (c is PictureBox pic)
                {
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                }

                if (c.HasChildren) AjustarFuentesEImagen(c, factor);
            }
        }
        #endregion

        private void frmAsistencia_Shown(object sender, EventArgs e)
        {
            // Garantiza que el cursor esté siempre listo para el lector de código de barras o teclado numérico
            txtCedula.Focus();
        }
        #endregion

        #region 3. GESTIÓN DE ESTADOS VISUALES (MODO KIOSCO)
        // Controla la transición de colores y mensajes basados en el estado de la membresía del socio
        private async void MostrarAlerta(Color colorFondo, string mensaje, string nombre, string plan, string vencimiento)
        {
            // Bloqueo de entrada para evitar múltiples lecturas simultáneas
            txtCedula.Enabled = false;
            txtCedula.Visible = false;

            pnlEstado.BackColor = colorFondo;

            // Contraste dinámico: Texto negro sobre fondos claros (Amarillo), Texto blanco sobre oscuros (Rojo/Verde)
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

            // Tiempo de visualización de la alerta (5 segundos) antes de reiniciar el kiosco
            await Task.Delay(5000);

            if (this.IsDisposed) return;

            LimpiarPantalla();
        }

        // Restablece la interfaz al modo de espera (Tema Oscuro Premium)
        private void LimpiarPantalla()
        {
            pnlEstado.BackColor = Color.FromArgb(35, 39, 47); // Gris oscuro resaltado

            lblMensaje.Text = "¡BIENVENIDO/A!";
            lblMensaje.ForeColor = Color.FromArgb(0, 229, 255); // Cian AsuFit
            lblMensaje.Visible = true;

            lblNombre.Text = "";
            lblNombre.Visible = false;

            lblTipoPlan.Text = "";
            lblTipoPlan.Visible = false;

            lblVencimiento.Text = "Ingresá tu número de cédula y presioná ENTER";
            lblVencimiento.ForeColor = Color.Silver; // Gris claro para instrucciones
            lblVencimiento.Visible = true;

            txtCedula.Clear();
            txtCedula.Enabled = true;
            txtCedula.Visible = true;

            CentrarControles();
            txtCedula.Focus();
        }

        // Recalcula dinámicamente el eje X de los controles para mantener una alineación central perfecta
        private void CentrarControles()
        {
            lblMensaje.Left = (pnlEstado.Width - lblMensaje.Width) / 2;
            lblNombre.Left = (pnlEstado.Width - lblNombre.Width) / 2;
            lblTipoPlan.Left = (pnlEstado.Width - lblTipoPlan.Width) / 2;
            lblVencimiento.Left = (pnlEstado.Width - lblVencimiento.Width) / 2;
            txtCedula.Left = (pnlEstado.Width - txtCedula.Width) / 2;
        }
        #endregion

        #region 4. CAPTURA DE DATOS (LECTOR / TECLADO)
        // Restringe la entrada a caracteres numéricos exclusivamente
        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Intercepta la tecla ENTER (generada por teclado o lector de código de barras) para procesar el acceso
        private void txtCedula_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (string.IsNullOrWhiteSpace(txtCedula.Text)) return;

                ProcesarAcceso(txtCedula.Text.Trim());
            }
        }
        #endregion

        #region 5. LÓGICA DE NEGOCIO Y REGLAS DE ACCESO
        // Evalúa el estado del socio y determina si se permite o deniega la entrada a las instalaciones
        private void ProcesarAcceso(string cedula)
        {
            SocioNegocio negocio = new SocioNegocio();
            Socio socio = negocio.BuscarSocioPorCedula(cedula);

            // Regla 1: El socio no existe en la base de datos
            if (socio == null)
            {
                MostrarAlerta(Color.DarkOrange, "Socio no encontrado", "", "", "Por favor, pasá por recepción para registrarte.");
                return;
            }

            // Regla 2: El socio está marcado como inactivo (baja lógica)
            if (socio.Estado != "Activo")
            {
                MostrarAlerta(Color.DarkRed, "Acceso Denegado", $"{socio.Nombre} {socio.Apellido}", "", "Tu usuario se encuentra inactivo.");
                return;
            }

            // Regla 3: El socio está activo pero nunca se le asignó un plan o venció hace mucho y se borró
            if (!socio.FechaVencimiento.HasValue)
            {
                MostrarAlerta(Color.Crimson, "Sin Plan Activo", $"{socio.Nombre} {socio.Apellido}", "", "No registrás membresías vigentes. Pasá por caja.");
                return;
            }

            string nombreCompleto = $"{socio.Nombre} {socio.Apellido}";
            string fechaVenceTexto = socio.FechaVencimiento.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            // Regla 4: Evaluación de fechas (Acceso Permitido vs Denegado por Vencimiento)
            if (socio.FechaVencimiento.Value.Date >= DateTime.Now.Date)
            {
                // Registro de la asistencia en el historial
                Asistencia nuevaAsistencia = new Asistencia();
                nuevaAsistencia.IdSocio = socio.IdSocio;

                AsistenciaNegocio negocioAsistencia = new AsistenciaNegocio();
                negocioAsistencia.RegistrarAsistencia(nuevaAsistencia);

                int diasRestantes = (socio.FechaVencimiento.Value.Date - DateTime.Now.Date).Days;
                string detalleVencimiento = $"Quedan {diasRestantes} días de plan. Vence el: {fechaVenceTexto}";

                // Alerta preventiva si el plan vence mañana
                if (diasRestantes == 1)
                {
                    MostrarAlerta(Color.Gold, "¡Acceso Permitido! (Próximo a vencer)", nombreCompleto, socio.NombrePlan, detalleVencimiento);
                }
                // Acceso normal
                else
                {
                    MostrarAlerta(Color.ForestGreen, "¡Acceso Permitido!", nombreCompleto, socio.NombrePlan, detalleVencimiento);
                }
            }
            else
            {
                // Regla 5: Plan vencido
                MostrarAlerta(Color.Crimson, "Plan Vencido", nombreCompleto, socio.NombrePlan, $"Tu plan venció el día: {fechaVenceTexto}. Pasá por caja.");
            }
        }
        #endregion

        #region 6. RELOJ DEL SISTEMA
        // Mantiene actualizada la visualización de la fecha y hora en tiempo real
        private void timerReloj_Tick(object sender, EventArgs e)
        {
            CultureInfo idioma = new CultureInfo("es-ES");
            lblHora.Text = "HORA: " + DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = "FECHA: " + DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy", idioma).ToUpper();
        }
        #endregion
    }
}