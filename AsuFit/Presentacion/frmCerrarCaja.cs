using System;
using System.Windows.Forms;
using AsuFit.Negocio;

namespace AsuFit.Presentacion
{
    public partial class frmCerrarCaja : Form
    {
        // Variables para atrapar los datos que nos envía la pantalla principal
        private int idTurno;
        private string nombreCajero;
        private DateTime fechaApertura; // NUEVA
        private decimal fondoInicial;
        private decimal ingEfectivo;
        private decimal ingTrans;
        private decimal gastos;
        private decimal totalEsperado;

        // 1. CONSTRUCTOR (Recibe los datos en vivo)
        public frmCerrarCaja(int idTurno, string nombreCajero, DateTime fechaApertura, decimal fondoInicial, decimal ingEfectivo, decimal ingTrans, decimal gastos, decimal totalEsperado)
        {
            InitializeComponent();
            this.idTurno = idTurno;
            this.nombreCajero = nombreCajero;
            this.fechaApertura = fechaApertura; // Guardamos la fecha
            this.fondoInicial = fondoInicial;
            this.ingEfectivo = ingEfectivo;
            this.ingTrans = ingTrans;
            this.gastos = gastos;
            this.totalEsperado = totalEsperado;
        }

        // 2. EVENTO LOAD
        private void frmCerrarCaja_Load(object sender, EventArgs e)
        {
            txtCajeroCierre.ReadOnly = true;
            txtCajeroCierre.Text = nombreCajero;
        }

        // 3. EVENTO BOTÓN CANCELAR
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // 4. EVENTO BOTÓN CONFIRMAR CIERRE
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
                    ArqueoNegocio negocio = new ArqueoNegocio();
                    bool exito = negocio.CerrarCaja(idTurno, ingEfectivo, ingTrans, gastos, totalEsperado, montoContado, diferencia);

                    if (exito)
                    {
                        // Abrimos la pantalla de resumen pasándole toda la plata calculada
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

        // 5. EVENTO KEYPRESS (Solo números)
        private void txtMontoContado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}