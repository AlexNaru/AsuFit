using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
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
        // Inicializa el rango nominal de fechas, ejecuta la consulta estadística base y aplica políticas de restricción UI.
        private void frmReportes_Load(object sender, EventArgs e)
        {
            ConfigurarTemaYEscala();

            dtpDesde.Value = DateTime.Now.AddDays(-1).Date;
            dtpHasta.Value = DateTime.Now;

            dtpDesdeTop.Value = DateTime.Now.AddDays(-1).Date;
            dtpHastaTop.Value = DateTime.Now;

            SincronizarTextosFecha();

            CargarReporteIngresos();
            CargarTopProductos();

            // Restricción UI: Neutraliza el menú contextual nativo del sistema en calendarios, cuadros adyacentes y grillas
            ContextMenuStrip menuNulo = new ContextMenuStrip();

            dtpDesde.ContextMenuStrip = menuNulo;
            dtpHasta.ContextMenuStrip = menuNulo;
            dtpDesdeTop.ContextMenuStrip = menuNulo;
            dtpHastaTop.ContextMenuStrip = menuNulo;

            if (txtDesde != null) txtDesde.ContextMenuStrip = menuNulo;
            if (txtHasta != null) txtHasta.ContextMenuStrip = menuNulo;
            if (txtDesdeTop != null) txtDesdeTop.ContextMenuStrip = menuNulo;
            if (txtHastaTop != null) txtHastaTop.ContextMenuStrip = menuNulo;

            dgvIngresos.ContextMenuStrip = menuNulo;
            dgvTopProductos.ContextMenuStrip = menuNulo;
        }
        #endregion

        #region 3. ESTILOS VISUALES (TEMA OSCURO Y ESCALADO)
        private void ConfigurarTemaYEscala()
        {
            // Solo necesitamos la fuente, el Dashboard ya se encarga de la escala física
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            // Fondo general
            this.BackColor = Color.FromArgb(25, 28, 35);

            AplicarTemaOscuroRecursivo(this, fuenteActual);

            // Aplicamos diseño premium a ambas tablas
            ConfigurarTemaOscuroGrilla(dgvIngresos, fuenteActual);
            ConfigurarTemaOscuroGrilla(dgvTopProductos, fuenteActual);
        }

        private void AplicarTemaOscuroRecursivo(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Panel || c is GroupBox || c is TabPage)
                {
                    c.BackColor = Color.FromArgb(25, 28, 35); // Fondo de las pestañas
                    c.ForeColor = Color.White;
                }
                else if (c is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                    lbl.Font = new Font("Segoe UI", fuente, lbl.Font.Style);

                    // Destacamos el total recaudado en CIAN y NEGRITA
                    if (lbl.Name == "lblTotalIngresos")
                    {
                        lbl.ForeColor = Color.FromArgb(0, 229, 255);
                        lbl.Font = new Font("Segoe UI", fuente + 2, FontStyle.Bold); // Un poco más grande
                    }
                }
                else if (c is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(50, 55, 65);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.ReadOnly = true; // Para los de fecha
                    txt.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
                }
                else if (c is TabControl tab)
                {
                    tab.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c, fuente);
            }
        }

        private void ConfigurarTemaOscuroGrilla(DataGridView dgv, float fuente)
        {
            dgv.BackgroundColor = Color.FromArgb(25, 28, 35);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(50, 55, 65);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", fuente, FontStyle.Bold);

            // FIX: Evitar el azul nativo al hacer clic en las cabeceras
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(25, 28, 35);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 229, 255); // Cian
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 35;
        }

        // Sincroniza los valores de los calendarios con los TextBox decorativos
        private void SincronizarTextosFecha()
        {
            // Pestaña 1
            if (txtDesde != null) txtDesde.Text = dtpDesde.Value.ToShortDateString();
            if (txtHasta != null) txtHasta.Text = dtpHasta.Value.ToShortDateString();

            // Pestaña 2
            if (txtDesdeTop != null) txtDesdeTop.Text = dtpDesdeTop.Value.ToShortDateString();
            if (txtHastaTop != null) txtHastaTop.Text = dtpHastaTop.Value.ToShortDateString();
        }
        #endregion

        #region 4. PESTAÑA 1: INGRESOS POR FECHAS
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            SincronizarTextosFecha();
            CargarReporteIngresos();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            SincronizarTextosFecha();
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

        #region 5. PESTAÑA 2: TOP PRODUCTOS MÁS VENDIDOS
        private void dtpDesdeTop_ValueChanged(object sender, EventArgs e)
        {
            SincronizarTextosFecha();
            CargarTopProductos();
        }

        private void dtpHastaTop_ValueChanged(object sender, EventArgs e)
        {
            SincronizarTextosFecha();
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