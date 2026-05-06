using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmAsistencia : Form
    {
        public frmAsistencia()
        {
            InitializeComponent();
        }

        private void frmAsistencia_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
            timerReloj.Start(); // Arranca el motor del reloj
        }

        private void frmAsistencia_Shown(object sender, EventArgs e)
        {
            txtCedula.Focus();
        }

        // --- NUEVA VALIDACIÓN: SOLO NÚMEROS ---
        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite números y teclas de control (como el Backspace para borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Si es una letra, la bloquea mágicamente antes de que aparezca
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

        private void ProcesarAcceso(string cedula)
        {
            SocioNegocio negocio = new SocioNegocio();
            Socio socio = negocio.BuscarSocioPorCedula(cedula);

            if (socio == null)
            {
                MostrarAlerta(Color.DarkOrange, "SOCIO NO ENCONTRADO.", "Por favor, pasá por recepción para registrarte.");
                return;
            }

            if (socio.Estado != "Activo")
            {
                MostrarAlerta(Color.DarkRed, "ACCESO DENEGADO", $"{socio.Nombre} {socio.Apellido}\nTu usuario está inactivo.");
                return;
            }

            if (!socio.FechaVencimiento.HasValue)
            {
                MostrarAlerta(Color.Crimson, "SIN PLAN", $"{socio.Nombre} {socio.Apellido}\nNo tenés ningún plan activo. Pasá por caja.");
                return;
            }

            if (socio.FechaVencimiento.Value.Date >= DateTime.Now.Date)
            {
                // ¡ENTRA! -> Guardamos el registro en la base de datos silenciosamente
                negocio.RegistrarAsistencia(socio.IdSocio);
                int diasRestantes = (socio.FechaVencimiento.Value.Date - DateTime.Now.Date).Days;

                // ACÁ ESTÁ LA MAGIA RECUPERADA: Le pasamos socio.NombrePlan al final
                MostrarAlerta(Color.ForestGreen, "¡BIENVENIDO/A!", $"{socio.Nombre} {socio.Apellido}\nTe quedan {diasRestantes} días de plan.", socio.NombrePlan);
            }
            else
            {
                MostrarAlerta(Color.Crimson, "PLAN VENCIDO", $"{socio.Nombre} {socio.Apellido}\nTu plan venció el {socio.FechaVencimiento.Value.ToShortDateString()}. Pasá por caja.");
            }
        }

        // ACÁ RECUPERAMOS EL PARÁMETRO: string tipoPlan = ""
        private async void MostrarAlerta(Color colorFondo, string mensajePrincipal, string detalle, string tipoPlan = "")
        {
            // 1. BLOQUEAMOS EL INGRESO DE DATOS
            txtCedula.Enabled = false;
            txtCedula.Visible = false;

            // 2. Mostramos el mensaje (Verde, Rojo, etc.)
            pnlEstado.BackColor = colorFondo;
            lblMensaje.Text = mensajePrincipal;
            lblDetalle.Text = detalle;

            // RECUPERAMOS LA VISTA DEL TIPO DE PLAN
            if (!string.IsNullOrEmpty(tipoPlan))
            {
                lblTipoPlan.Text = "TU PLAN: " + tipoPlan.ToUpper();
                lblTipoPlan.Visible = true;
            }
            else
            {
                lblTipoPlan.Visible = false;
            }

            txtCedula.Clear();

            // 3. Esperamos 5 segundos (5000 milisegundos)
            await Task.Delay(5000);

            // Parche de seguridad
            if (this.IsDisposed) return;

            // 4. LIMPIAMOS LA PANTALLA Y VOLVEMOS A HABILITAR EL TEXTBOX
            LimpiarPantalla();
            txtCedula.Enabled = true;

            // Forzamos el foco de nuevo para que quede titilando
            txtCedula.Focus();
        }

        private void LimpiarPantalla()
        {
            txtCedula.Enabled = true;
            txtCedula.Visible = true;
            pnlEstado.BackColor = Color.Silver;
            lblMensaje.Text = "¡BIENVENIDO/A!";
            lblMensaje.ForeColor = Color.Black;
            lblDetalle.Text = "Ingresá tu número de cédula y presioná ENTER";
            lblDetalle.ForeColor = Color.Black;

            // VOLVEMOS A OCULTAR EL LABEL AL LIMPIAR
            if (lblTipoPlan != null)
            {
                lblTipoPlan.Text = "";
                lblTipoPlan.Visible = false;
            }
        }

        private void timerReloj_Tick(object sender, EventArgs e)
        {
            lblHora.Text = "Hora: " + DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = "Fecha: " + DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy").ToUpper();
        }
    }
}