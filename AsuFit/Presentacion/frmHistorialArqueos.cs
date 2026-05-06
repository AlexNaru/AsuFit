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
                dgvArqueos.DataSource = dt;

                if (dgvArqueos.Columns.Count > 0)
                {
                    // Ocultamos el ID
                    dgvArqueos.Columns["IdArqueo"].Visible = false;

                    // Emprolijamos el resto de la tabla
                    dgvArqueos.Columns["FechaHora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    dgvArqueos.Columns["TotalIngresosSistema"].DefaultCellStyle.Format = "N0";
                    dgvArqueos.Columns["EfectivoDeclarado"].DefaultCellStyle.Format = "N0";
                    dgvArqueos.Columns["Diferencia"].DefaultCellStyle.Format = "N0";
                    dgvArqueos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    // Le cambiamos el título para que se vea más lindo
                    dgvArqueos.Columns["UsuarioRegistra"].HeaderText = "Cajero";
                    dgvArqueos.Columns["UsuarioRegistra"].DefaultCellStyle.Format = "N0";
                }

                ColorearDiferencias();
                dgvArqueos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- PINTA LOS NÚMEROS DE COLORES SEGÚN SI SOBRÓ O FALTÓ PLATA ---
        private void ColorearDiferencias()
        {
            foreach (DataGridViewRow fila in dgvArqueos.Rows)
            {
                if (fila.Cells["Diferencia"].Value != null && fila.Cells["Diferencia"].Value != DBNull.Value)
                {
                    decimal diferencia = Convert.ToDecimal(fila.Cells["Diferencia"].Value);

                    if (diferencia == 0)
                    {
                        fila.Cells["Diferencia"].Style.ForeColor = Color.Green;
                    }
                    else if (diferencia < 0)
                    {
                        fila.Cells["Diferencia"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        fila.Cells["Diferencia"].Style.ForeColor = Color.Goldenrod;
                    }

                    // Ponemos la letra en negrita para que resalte más
                    fila.Cells["Diferencia"].Style.Font = new Font(dgvArqueos.Font, FontStyle.Bold);
                }
            }
        }
    }
}