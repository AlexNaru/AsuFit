using System;
using System.Windows.Forms;
using AsuFit.Negocio;      // <-- Ahora llamamos a Negocio
using AsuFit.Entidades;

namespace AsuFit.Presentacion
{
    public partial class frmAbrirCaja : Form
    {
        private Usuario usuarioActual;

        // 1. CONSTRUCTOR
        public frmAbrirCaja(Usuario user)
        {
            InitializeComponent();
            usuarioActual = user;
        }

        // 2. EVENTO: frmAbrirCaja_Load
        private void frmAbrirCaja_Load(object sender, EventArgs e)
        {
            txtCajero.ReadOnly = true;
            if (usuarioActual != null)
            {
                txtCajero.Text = usuarioActual.NombreCompleto;
            }
        }

        // 3. EVENTO: btnCancelar_Click
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // 4. EVENTO: btnEmpezar_Click
        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMontoInicial.Text))
            {
                MessageBox.Show("Debes ingresar el monto de dinero base con el que empiezas tu turno.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtMontoInicial.Text, out decimal fondoInicial))
            {
                MessageBox.Show("El monto ingresado no es válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // --- AQUÍ ESTÁ LA MAGIA DE LAS 3 CAPAS ---
                TurnoCaja nuevoTurno = new TurnoCaja();
                nuevoTurno.IdUsuario = usuarioActual.IdUsuario;
                nuevoTurno.CajeroNombre = usuarioActual.NombreCompleto;
                nuevoTurno.FondoInicial = fondoInicial;

                ArqueoNegocio negocio = new ArqueoNegocio();
                bool exito = negocio.AbrirCaja(nuevoTurno);

                if (exito)
                {
                    // Opcional: GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Caja", "Apertura de Turno", $"Fondo inicial: Gs. {fondoInicial:N0}");

                    MessageBox.Show("¡Turno iniciado con éxito! La caja ya está abierta.", "Caja Abierta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la caja: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 5. EVENTO: txtMontoInicial_KeyPress
        private void txtMontoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}