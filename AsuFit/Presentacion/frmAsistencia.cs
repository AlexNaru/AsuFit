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

        #region 2. INICIALIZACIÓN (Load)
        private void frmAsistencia_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
            timerReloj.Start();
        }

        private void frmAsistencia_Shown(object sender, EventArgs e)
        {
            txtCedula.Focus();
        }
        #endregion

        #region 3. GESTIÓN DE ESTADOS VISUALES (Panel Central)
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

            // Recalcular posiciones (Left) después de modificar el texto para centrado preciso
            CentrarControles();

            await Task.Delay(5000);

            if (this.IsDisposed) return;

            LimpiarPantalla();
        }

        private void LimpiarPantalla()
        {
            pnlEstado.BackColor = Color.FromArgb(240, 240, 240);

            lblMensaje.Text = "¡BIENVENIDO/A!";
            lblMensaje.ForeColor = Color.Black;
            lblMensaje.Visible = true;

            lblNombre.Text = "";
            lblNombre.Visible = false;

            lblTipoPlan.Text = "";
            lblTipoPlan.Visible = false;

            lblVencimiento.Text = "Ingresá tu número de cédula y presioná ENTER";
            lblVencimiento.ForeColor = Color.DimGray;
            lblVencimiento.Visible = true;

            txtCedula.Clear();
            txtCedula.Enabled = true;
            txtCedula.Visible = true;

            CentrarControles();
            txtCedula.Focus();
        }

        private void CentrarControles()
        {
            // Cálculo del centro horizontal respecto al contenedor (pnlEstado)
            lblMensaje.Left = (pnlEstado.Width - lblMensaje.Width) / 2;
            lblNombre.Left = (pnlEstado.Width - lblNombre.Width) / 2;
            lblTipoPlan.Left = (pnlEstado.Width - lblTipoPlan.Width) / 2;
            lblVencimiento.Left = (pnlEstado.Width - lblVencimiento.Width) / 2;
            txtCedula.Left = (pnlEstado.Width - txtCedula.Width) / 2;
        }
        #endregion

        #region 4. CAPTURA DE DATOS (TextBox)
        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

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

        #region 5. LÓGICA DE NEGOCIO (Validación de Acceso)
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

            // InvariantCulture fuerza la barra '/' en la fecha ignorando la región del sistema operativo
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

        #region 6. RELOJ (Sección Inferior)
        private void timerReloj_Tick(object sender, EventArgs e)
        {
            CultureInfo idioma = new CultureInfo("es-ES");
            lblHora.Text = "HORA: " + DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = "FECHA: " + DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy", idioma).ToUpper();
        }
        #endregion
    }
}