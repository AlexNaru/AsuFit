using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmHistorialArqueos : Form
    {
        public frmHistorialArqueos()
        {
            InitializeComponent();

            // --- EL CAMBIO CLAVE: Bloqueamos las columnas automáticas ---
            dgvArqueos.AutoGenerateColumns = false;
        }

        private void frmHistorialArqueos_Load(object sender, EventArgs e)
        {
            // Por defecto, carga los últimos 7 días
            dtpDesde.Value = DateTime.Now.AddDays(-7);
            dtpHasta.Value = DateTime.Now;

            CargarHistorial();
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            CargarHistorial();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            CargarHistorial();
        }

        private void CargarHistorial()
        {
            try
            {
                ArqueoNegocio negocio = new ArqueoNegocio();
                DataTable dt = negocio.ListarHistorialArqueos(dtpDesde.Value, dtpHasta.Value);

                // Pasamos los datos, la grilla buscará los DataPropertyName
                dgvArqueos.DataSource = dt;

                // --- CÓDIGO LIMPIO: Toda la configuración de columnas se eliminó ---
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ejecutamos el coloreo aquí para asegurar que las filas ya estén creadas
        private void dgvArqueos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ColorearDiferencias();
            dgvArqueos.ClearSelection();
        }

        private void ColorearDiferencias()
        {
            foreach (DataGridViewRow fila in dgvArqueos.Rows)
            {
                // ACTUALIZADO: Apuntamos al nuevo Name de la columna visual
                if (fila.Cells["colArqueoDiferencia"].Value != null && fila.Cells["colArqueoDiferencia"].Value != DBNull.Value)
                {
                    decimal diferencia = Convert.ToDecimal(fila.Cells["colArqueoDiferencia"].Value);

                    if (diferencia == 0)
                    {
                        fila.Cells["colArqueoDiferencia"].Style.ForeColor = Color.Green;
                    }
                    else if (diferencia < 0)
                    {
                        fila.Cells["colArqueoDiferencia"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        fila.Cells["colArqueoDiferencia"].Style.ForeColor = Color.Goldenrod;
                    }

                    fila.Cells["colArqueoDiferencia"].Style.Font = new Font(dgvArqueos.Font, FontStyle.Bold);
                }
            }
        }

        private void btnVerPDF_Click(object sender, EventArgs e)
        {
            if (dgvArqueos.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecciona un arqueo de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ACTUALIZADO: Leemos usando los nuevos Names
            string estado = dgvArqueos.CurrentRow.Cells["colArqueoEstado"].Value.ToString();
            string idTurno = dgvArqueos.CurrentRow.Cells["colArqueoId"].Value.ToString();

            if (estado == "Cerrada")
            {
                string rutaDescargas = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string nombreArchivo = $"Ticket_Arqueo_Turno_{idTurno}.pdf";
                string rutaCompleta = System.IO.Path.Combine(rutaDescargas, nombreArchivo);

                if (System.IO.File.Exists(rutaCompleta))
                {
                    System.Diagnostics.Process.Start(rutaCompleta);
                }
                else
                {
                    MessageBox.Show($"No se encontró el archivo PDF para este arqueo en la ruta:\n{rutaCompleta}", "Archivo no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Este turno aún se encuentra ABIERTO.", "Turno en curso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}