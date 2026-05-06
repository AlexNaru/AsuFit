using AsuFit.Negocio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmArqueoCaja : Form
    {
        private decimal totalSistema = 0;
        private AsuFit.Entidades.Usuario usuarioActual;

        public frmArqueoCaja(AsuFit.Entidades.Usuario user)
        {
            InitializeComponent();
        }

        private void frmArqueoCaja_Load(object sender, EventArgs e)
        {
            // Al abrir la pantalla, busca la plata de hoy
            CargarTotalSistema();
        }

        // --- 1. EVENTOS DE ACTUALIZACIÓN AUTOMÁTICA ---

        private void dtpFechaArqueo_ValueChanged(object sender, EventArgs e)
        {
            // Si cambian la fecha, recalcula la plata de ese día
            CargarTotalSistema();
        }

        private void txtEfectivoCaja_TextChanged(object sender, EventArgs e)
        {
            // A medida que el cajero escribe sus billetes, calcula la diferencia
            CalcularDiferencia();
        }

        // --- 2. MÉTODOS DE CÁLCULO ---

        private void CargarTotalSistema()
        {
            try
            {
                ArqueoNegocio negocio = new ArqueoNegocio();
                totalSistema = negocio.ObtenerTotalDelDia(dtpFechaArqueo.Value);
                lblTotalSistema.Text = totalSistema.ToString("N0");

                // Forzamos a que recalcule la diferencia
                CalcularDiferencia();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el total: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularDiferencia()
        {
            decimal efectivoCaja = 0;

            // Si el cajero escribió algo, lo convertimos a número
            if (!string.IsNullOrWhiteSpace(txtEfectivoCaja.Text))
            {
                decimal.TryParse(txtEfectivoCaja.Text, out efectivoCaja);
            }

            decimal diferencia = efectivoCaja - totalSistema;
            lblDiferencia.Text = diferencia.ToString("N0");

            // Semáforo de colores
            if (diferencia == 0)
            {
                lblDiferencia.ForeColor = Color.Green; // Todo perfecto
            }
            else if (diferencia < 0)
            {
                lblDiferencia.ForeColor = Color.Red; // Falta plata
            }
            else
            {
                lblDiferencia.ForeColor = Color.Goldenrod; // Sobra plata
            }
        }

        // --- 3. GUARDAR EL CIERRE ---

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEfectivoCaja.Text))
            {
                MessageBox.Show("Por favor, ingresá el efectivo físico que contaste en la caja.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal efectivoCaja = Convert.ToDecimal(txtEfectivoCaja.Text);
            decimal diferencia = efectivoCaja - totalSistema;

            DialogResult confirmacion = MessageBox.Show($"¿Confirmar cierre de caja con una diferencia de {diferencia.ToString("N0")} Gs?", "Confirmar Arqueo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    ArqueoNegocio negocio = new ArqueoNegocio();
                    // Si en tu clase Usuario la propiedad se llama distinto, cámbiala aquí (ej. usuarioActual.Username)
                    if (negocio.RegistrarCierre(totalSistema, efectivoCaja, diferencia, usuarioActual.NombreCompleto))
                    {
                        MessageBox.Show("Arqueo registrado exitosamente. La caja ha sido cerrada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el arqueo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHistorialArqueos_Click(object sender, EventArgs e)
        {
            // Abrimos la pantalla que ya tenés lista y blindada
            frmHistorialArqueos frm = new frmHistorialArqueos();
            frm.ShowDialog();
        }
    }
}