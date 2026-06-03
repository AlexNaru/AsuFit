using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmCerrarCaja : Form
    {
        private int idTurno;
        private string nombreCajero;
        private DateTime fechaApertura;
        private decimal fondoInicial;
        private decimal ingEfectivo;
        private decimal ingTrans;
        private decimal gastos;
        private decimal totalEsperado;

        // 1. CONSTRUCTOR
        public frmCerrarCaja(int idTurno, string nombreCajero, DateTime fechaApertura, decimal fondoInicial, decimal ingEfectivo, decimal ingTrans, decimal gastos, decimal totalEsperado)
        {
            InitializeComponent();
            this.idTurno = idTurno;
            this.nombreCajero = nombreCajero;
            this.fechaApertura = fechaApertura;
            this.fondoInicial = fondoInicial;
            this.ingEfectivo = ingEfectivo;
            this.ingTrans = ingTrans;
            this.gastos = gastos;
            this.totalEsperado = totalEsperado;
        }

        // 2. EVENTO LOAD
        private void frmCerrarCaja_Load(object sender, EventArgs e)
        {
            ConfigurarTemaYEscala();
            CentrarFormulario();

            txtCajeroCierre.ReadOnly = true;
            txtCajeroCierre.Text = nombreCajero;

            txtCajeroCierre.SelectionStart = txtCajeroCierre.Text.Length;
            txtCajeroCierre.SelectionLength = 0;

            this.ActiveControl = txtMontoContado;
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

            if (btnConfirmarCierre != null)
            {
                btnConfirmarCierre.BackColor = Color.IndianRed;
                btnConfirmarCierre.ForeColor = Color.White;
                btnConfirmarCierre.FlatStyle = FlatStyle.Flat;
                btnConfirmarCierre.FlatAppearance.BorderSize = 0;
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
            this.CenterToScreen();
        }
        #endregion

        // 3. EVENTOS BOTONES
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnConfirmarCierre_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMontoContado.Text))
            {
                MessageBox.Show("Debes ingresar el dinero físico que contaste.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal montoContado = Convert.ToDecimal(txtMontoContado.Text);
            decimal diferencia = montoContado - totalEsperado;

            DialogResult respuesta = MessageBox.Show($"Efectivo Contado: Gs. {montoContado:N0}\nDiferencia (Descuadre): Gs. {diferencia:N0}\n\n¿Estás seguro de cerrar el turno con estos valores?", "Confirmar Cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    TurnoCaja turnoCierre = new TurnoCaja();
                    turnoCierre.IdTurno = idTurno;
                    turnoCierre.IngresosEfectivo = ingEfectivo;
                    turnoCierre.IngresosTransferencia = ingTrans;
                    turnoCierre.GastosEfectivo = gastos;
                    turnoCierre.MontoEsperado = totalEsperado;
                    turnoCierre.MontoContado = montoContado;
                    turnoCierre.Diferencia = diferencia;

                    ArqueoNegocio negocio = new ArqueoNegocio();
                    bool exito = negocio.CerrarCaja(turnoCierre);

                    if (exito)
                    {
                        frmResumenArqueo frmResumen = new frmResumenArqueo(idTurno, nombreCajero, fechaApertura, ingTrans, ingEfectivo, fondoInicial, gastos, totalEsperado, montoContado, diferencia);
                        frmResumen.ShowDialog();

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cerrar la caja: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtMontoContado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }
    }
}