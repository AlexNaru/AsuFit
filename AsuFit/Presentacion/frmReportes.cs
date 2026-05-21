using AsuFit.Negocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmReportes : Form
    {
        // Instanciamos nuestra nueva capa de negocio
        private ReportesNegocio negocio = new ReportesNegocio();

        public frmReportes()
        {
            InitializeComponent();

            // Bloqueamos las columnas automáticas de ambas tablas
            dgvIngresos.AutoGenerateColumns = false;
            dgvTopProductos.AutoGenerateColumns = false;
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            // Fechas para la pestaña 1 (Ingresos)
            dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpHasta.Value = DateTime.Now;
            CargarReporteIngresos();

            // Fechas para la pestaña 2 (Top Productos)
            dtpDesdeTop.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpHastaTop.Value = DateTime.Now;
            CargarTopProductos();
        }

        private void CargarReporteIngresos()
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                dgvIngresos.DataSource = null;
                lblTotalIngresos.Text = "TOTAL RECAUDADO: Gs. 0";
                return;
            }

            try
            {
                // Llamamos a la capa de negocio, el formulario ya no sabe nada de SQL
                DataTable dtIngresos = negocio.ListarIngresosPorFechas(dtpDesde.Value, dtpHasta.Value);
                dgvIngresos.DataSource = dtIngresos;

                // Sumamos los totales
                decimal sumaTotal = 0;
                foreach (DataRow fila in dtIngresos.Rows)
                {
                    sumaTotal += Convert.ToDecimal(fila["Total"]);
                }

                lblTotalIngresos.Text = "TOTAL RECAUDADO: Gs. " + sumaTotal.ToString("N0");

                dgvIngresos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            CargarReporteIngresos();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            CargarReporteIngresos();
        }

        private void CargarTopProductos()
        {
            if (dtpDesdeTop.Value.Date > dtpHastaTop.Value.Date) return;

            try
            {
                // Llamamos a la capa de negocio
                DataTable dtTop = negocio.ListarTopProductos(dtpDesdeTop.Value, dtpHastaTop.Value);
                dgvTopProductos.DataSource = dtTop;

                dgvTopProductos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el Top 5: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpDesdeTop_ValueChanged(object sender, EventArgs e)
        {
            CargarTopProductos();
        }

        private void dtpHastaTop_ValueChanged(object sender, EventArgs e)
        {
            CargarTopProductos();
        }
    }
}