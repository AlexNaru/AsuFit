using AsuFit.Negocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmReportes : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private ReportesNegocio negocio = new ReportesNegocio();

        public frmReportes()
        {
            InitializeComponent();
            dgvIngresos.AutoGenerateColumns = false;
            dgvTopProductos.AutoGenerateColumns = false;
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA INICIAL
        private void frmReportes_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpHasta.Value = DateTime.Now;
            CargarReporteIngresos();

            dtpDesdeTop.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpHastaTop.Value = DateTime.Now;
            CargarTopProductos();
        }
        #endregion

        #region 3. PESTAÑA 1: INGRESOS POR FECHAS
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            CargarReporteIngresos();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            CargarReporteIngresos();
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
                DataTable dtIngresos = negocio.ListarIngresosPorFechas(dtpDesde.Value, dtpHasta.Value);
                dgvIngresos.DataSource = dtIngresos;

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
        #endregion

        #region 4. PESTAÑA 2: TOP PRODUCTOS MÁS VENDIDOS
        private void dtpDesdeTop_ValueChanged(object sender, EventArgs e)
        {
            CargarTopProductos();
        }

        private void dtpHastaTop_ValueChanged(object sender, EventArgs e)
        {
            CargarTopProductos();
        }

        private void CargarTopProductos()
        {
            if (dtpDesdeTop.Value.Date > dtpHastaTop.Value.Date) return;

            try
            {
                DataTable dtTop = negocio.ListarTopProductos(dtpDesdeTop.Value, dtpHastaTop.Value);
                dgvTopProductos.DataSource = dtTop;

                dgvTopProductos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el Top 5: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}