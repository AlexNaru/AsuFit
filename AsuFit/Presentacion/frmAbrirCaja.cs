using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmAbrirCaja : Form
    {
        private Usuario usuarioActual;

        // 1. CONSTRUCTOR
        public frmAbrirCaja(Usuario user)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmAbrirCaja_Load);
            usuarioActual = user;
        }

        // 2. EVENTOS DEL FORMULARIO
        private void frmAbrirCaja_Load(object sender, EventArgs e)
        {
            ConfigurarTemaYEscala();
            CentrarFormulario();

            txtCajero.ReadOnly = true;

            // El cursor rebota a la caja de dinero inicial
            txtCajero.Enter += delegate { this.ActiveControl = txtMontoInicial; };

            if (usuarioActual != null)
            {
                txtCajero.Text = usuarioActual.NombreCompleto;
            }

            txtCajero.SelectionStart = txtCajero.Text.Length;
            txtCajero.SelectionLength = 0;

            this.ActiveControl = txtMontoInicial;

            // Sella ambos cuadros de texto contra menús contextuales de Windows
            txtMontoInicial.ContextMenuStrip = new ContextMenuStrip();
            txtCajero.ContextMenuStrip = new ContextMenuStrip();
        }

        #region ESTILOS VISUALES Y ESCALADO
        private void ConfigurarTemaYEscala()
        {
            // BLOQUEO DE REDIMENSIONAMIENTO Y PANTALLA COMPLETA
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            float escalaActual = Properties.Settings.Default.EscalaInterfaz;
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            this.Scale(new SizeF(escalaActual, escalaActual));
            AjustarFuentesRecursivo(this, fuenteActual);

            this.BackColor = Color.FromArgb(25, 28, 35);
            AplicarTemaOscuroRecursivo(this);

            if (btnEmpezar != null)
            {
                btnEmpezar.BackColor = Color.FromArgb(0, 229, 255);
                btnEmpezar.ForeColor = Color.Black;
                btnEmpezar.FlatStyle = FlatStyle.Flat;
                btnEmpezar.FlatAppearance.BorderSize = 0;
            }

            if (btnCancelar != null)
            {
                btnCancelar.BackColor = Color.FromArgb(50, 55, 65);
                btnCancelar.ForeColor = Color.White;
                btnCancelar.FlatStyle = FlatStyle.Flat;
                btnCancelar.FlatAppearance.BorderSize = 0;
            }
        }

        private void AplicarTemaOscuroRecursivo(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Panel || c is GroupBox)
                {
                    c.BackColor = Color.FromArgb(35, 39, 47);
                    c.ForeColor = Color.White;
                }
                else if (c is Label lbl) lbl.ForeColor = Color.White;
                else if (c is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(50, 55, 65);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c);
            }
        }

        private void AjustarFuentesRecursivo(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is TextBox || c is ComboBox || c is Label || c is Button)
                {
                    c.Font = new Font("Segoe UI", fuente, c.Font.Style);
                }
                if (c.HasChildren) AjustarFuentesRecursivo(c, fuente);
            }
        }

        // Calcula el centro exacto basándose ÚNICAMENTE en el panel derecho del Dashboard
        private void CentrarFormulario()
        {
            Form padre = Application.OpenForms["frmDashboard"];
            if (padre != null)
            {
                Control[] controles = padre.Controls.Find("pnlContenedor", true);
                if (controles.Length > 0)
                {
                    Control contenedor = controles[0];
                    Point posicionAbsoluta = contenedor.PointToScreen(Point.Empty);

                    this.StartPosition = FormStartPosition.Manual;
                    int x = posicionAbsoluta.X + (contenedor.Width - this.Width) / 2;
                    int y = posicionAbsoluta.Y + (contenedor.Height - this.Height) / 2;
                    this.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);
                    return;
                }
            }
            this.CenterToScreen(); // Respaldo por si no encuentra el Dashboard
        }
        #endregion

        // Procesa la apertura transaccional de un nuevo turno de caja, validando el fondo inicial y registrando el evento en la auditoría del sistema.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMontoInicial.Text))
            {
                MensajeAsuFit.Mostrar("Debes ingresar el monto de dinero base con el que empiezas tu turno.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtMontoInicial.Text, out decimal fondoInicial))
            {
                MensajeAsuFit.Mostrar("El monto ingresado no es válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                TurnoCaja nuevoTurno = new TurnoCaja();
                nuevoTurno.IdUsuario = usuarioActual.IdUsuario;
                nuevoTurno.CajeroNombre = usuarioActual.NombreCompleto;
                nuevoTurno.FondoInicial = fondoInicial;

                ArqueoNegocio negocio = new ArqueoNegocio();
                bool exito = negocio.AbrirCaja(nuevoTurno);

                if (exito)
                {
                    AsuFit.Datos.GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Caja", "Apertura de Turno", $"Se aperturó un nuevo turno de caja con un fondo inicial declarado de Gs. {fondoInicial:N0}.");
                    MensajeAsuFit.Mostrar(this, "¡Turno iniciado con éxito! La caja ya está abierta.", "Caja Abierta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MensajeAsuFit.Mostrar(this, "Error al abrir la caja: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMontoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Filtro numérico puro: solo dígitos y teclas de control del sistema
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}