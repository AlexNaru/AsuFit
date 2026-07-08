using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AsuFit.Presentacion
{
    public partial class frmInicio : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private InventarioNegocio negocioInventario = new InventarioNegocio();

        // Inicializa la instancia y bloquea la autogeneración de columnas para respetar el diseño visual.
        public frmInicio()
        {
            InitializeComponent();

            dgvVencimientos.AutoGenerateColumns = false;
            dgvVencidos.AutoGenerateColumns = false;
            dgvProductosStock.AutoGenerateColumns = false;
            dgvProductosStockBajo.AutoGenerateColumns = false;
        }
        #endregion

        #region 2. CICLO DE VIDA DEL FORMULARIO
        // Orquesta la inicialización de temas cromáticos y la carga asíncrona de datos para el dashboard al iniciar la vista.
        private void frmInicio_Load(object sender, EventArgs e)
        {
            AplicarTemaGraficoOscuro();
            AplicarTemaOscuroGrillas(dgvProductosStock);
            AplicarTemaOscuroGrillas(dgvProductosStockBajo);
            AplicarTemaOscuroGrillas(dgvVencimientos);
            AplicarTemaOscuroGrillas(dgvVencidos);

            CargarDashboard();
            CargarTablasInventario();
            CargarVencimientos();

            SocioNegocio negocioSocio = new SocioNegocio();
            negocioSocio.ProcesarEnvioCorreosVencimiento();
        }
        #endregion

        #region 3. MÓDULO DE MÉTRICAS Y FINANZAS
        // Extrae los indicadores clave de rendimiento (KPIs) desde la capa de negocio y actualiza la interfaz.
        private void CargarDashboard()
        {
            DashboardNegocio negocio = new DashboardNegocio();
            int activos, vencimientos;
            decimal ingresos, egresos;

            negocio.ObtenerMeticasPrincipales(out activos, out ingresos, out egresos, out vencimientos);

            lblActivos.Text = activos.ToString();
            lblProximosVencimientos.Text = vencimientos.ToString();
            lblIngresos.Text = ingresos.ToString("N0") + " Gs.";
            lblEgresos.Text = egresos.ToString("N0") + " Gs.";
            lblUtilidad.Text = (ingresos - egresos).ToString("N0") + " Gs.";

            ConfigurarGrafico(ingresos, egresos);
        }

        // Renderiza las series de datos financieros en el control Chart aplicando la paleta de colores corporativa.
        private void ConfigurarGrafico(decimal ingresos, decimal egresos)
        {
            chartFinanzas.Series.Clear();

            Series serie = new Series("Balance Mensual");
            serie.ChartType = SeriesChartType.Column;
            serie["PointWidth"] = "0.7";

            decimal utilidad = ingresos - egresos;

            serie.Points.AddXY("Ingresos", ingresos);
            serie.Points[0].Color = Color.MediumSeaGreen;

            serie.Points.AddXY("Egresos", egresos);
            serie.Points[1].Color = Color.IndianRed;

            serie.Points.AddXY("Utilidad", utilidad);
            serie.Points[2].Color = Color.RoyalBlue;

            chartFinanzas.Series.Add(serie);
        }
        #endregion

        #region 4. MÓDULO DE INVENTARIO
        // Recupera los conjuntos de datos del inventario y los enlaza a sus respectivas grillas de visualización.
        private void CargarTablasInventario()
        {
            DataTable dtTodos = negocioInventario.ListarProductosBasico();
            if (dtTodos != null)
            {
                dgvProductosStock.DataSource = dtTodos;
            }

            DataTable dtBajo = negocioInventario.ListarProductosStockBajo();
            if (dtBajo != null)
            {
                dgvProductosStockBajo.DataSource = dtBajo;
            }
        }

        // Elimina el resaltado por defecto de la primera fila tras completar el enlace de datos.
        private void dgvProductosStock_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvProductosStock.ClearSelection();
        }

        // Elimina el resaltado por defecto de la primera fila tras completar el enlace de datos.
        private void dgvProductosStockBajo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvProductosStockBajo.ClearSelection();
        }

        // Inyecta formato condicional en las celdas para alertar visualmente sobre quiebres o advertencias de stock.
        private void dgvProductosStockBajo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int stockActual = -1;

                if (dgvProductosStockBajo.Columns.Contains("StockActual") && dgvProductosStockBajo.Rows[e.RowIndex].Cells["StockActual"].Value != null)
                {
                    int.TryParse(dgvProductosStockBajo.Rows[e.RowIndex].Cells["StockActual"].Value.ToString(), out stockActual);
                }
                else if (dgvProductosStockBajo.Columns.Count > 1 && dgvProductosStockBajo.Rows[e.RowIndex].Cells[1].Value != null)
                {
                    int.TryParse(dgvProductosStockBajo.Rows[e.RowIndex].Cells[1].Value.ToString(), out stockActual);
                }

                if (stockActual == 0)
                {
                    dgvProductosStockBajo.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                    dgvProductosStockBajo.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    dgvProductosStockBajo.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                    dgvProductosStockBajo.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        #endregion

        #region 5. MÓDULO DE SOCIOS Y VENCIMIENTOS
        // Recupera las listas de membresías en periodo de gracia o caducadas y las enlaza a la interfaz.
        private void CargarVencimientos()
        {
            try
            {
                DashboardNegocio negocioDash = new DashboardNegocio();
                DataTable dtVencimientos = negocioDash.ListarVencimientosProximos();
                dgvVencimientos.DataSource = dtVencimientos;

                SocioNegocio negocioSocio = new SocioNegocio();
                var listaVencidos = negocioSocio.ListarVencidos();
                dgvVencidos.DataSource = listaVencidos;
                lblVencimientos.Text = listaVencidos.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los vencimientos: " + ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Elimina el resaltado por defecto de la primera fila tras completar el enlace de datos.
        private void dgvVencimientos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvVencimientos.ClearSelection();
        }

        // Elimina el resaltado por defecto de la primera fila tras completar el enlace de datos.
        private void dgvVencidos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvVencidos.ClearSelection();
        }

        // Aplica formato condicional de advertencia (color oro) para los socios próximos a vencer.
        private void dgvVencimientos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvVencimientos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                dgvVencimientos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        // Aplica formato condicional de alerta crítica (color coral) para los socios con membresía expirada.
        private void dgvVencidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvVencidos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                dgvVencidos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }
        #endregion

        #region 6. RENDERIZADO DE TEMAS VISUALES
        // Configura la paleta de colores oscuros para el área y los ejes del gráfico financiero.
        private void AplicarTemaGraficoOscuro()
        {
            var grafico = chartFinanzas;

            grafico.BackColor = Color.FromArgb(35, 39, 47);
            grafico.ChartAreas[0].BackColor = Color.FromArgb(35, 39, 47);

            grafico.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            grafico.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;

            grafico.ChartAreas[0].AxisX.LineColor = Color.Gray;
            grafico.ChartAreas[0].AxisY.LineColor = Color.Gray;
            grafico.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(50, 55, 65);
            grafico.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(50, 55, 65);

            if (grafico.Legends.Count > 0)
            {
                grafico.Legends[0].BackColor = Color.FromArgb(35, 39, 47);
                grafico.Legends[0].ForeColor = Color.White;
            }
        }

        // Inyecta dinámicamente las propiedades cromáticas corporativas sobre las estructuras de las grillas.
        private void AplicarTemaOscuroGrillas(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(35, 39, 47);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(50, 55, 65);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 28, 35);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(35, 39, 47);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 229, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        #endregion

        #region 7. MÉTODOS AUXILIARES DE FORMULARIO EMERGENTE

        // Configura la escala, fuente y posición del formulario emergente respetando las dimensiones del Dashboard contenedor
        private void PrepararFormularioComoDashboard(Form frm)
        {
            float escalaActual = Properties.Settings.Default.EscalaInterfaz;

            frm.Scale(new SizeF(escalaActual, escalaActual));
            AjustarFuentes(frm);

            frm.StartPosition = FormStartPosition.Manual;

            if (this.Parent != null)
            {
                Point posicionPanelAbsoluta = this.Parent.PointToScreen(Point.Empty);
                int x = posicionPanelAbsoluta.X + (this.Parent.Width - frm.Width) / 2;
                int y = posicionPanelAbsoluta.Y + (this.Parent.Height - frm.Height) / 2;

                frm.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterParent;
            }
        }

        // Ajusta recursivamente el tamaño de fuente utilizando la configuración dinámica del sistema
        private void AjustarFuentes(Control contenedor)
        {
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            foreach (Control c in contenedor.Controls)
            {
                if (c is TextBox || c is ComboBox || c is Label || c is DataGridView)
                {
                    c.Font = new Font("Segoe UI", fuenteActual, c.Font.Style);
                }
                else if (c.HasChildren)
                {
                    AjustarFuentes(c);
                }
            }
        }

        #endregion

        #region 8. NAVEGACIÓN ANALÍTICA (DRILL-DOWN)

        // Intercepta el clic sobre el indicador de ingresos para instanciar y desplegar la vista detallada de transacciones.
        private void lblIngresos_Click(object sender, EventArgs e)
        {
            frmHistorialTransacciones frmHistorial = new frmHistorialTransacciones();

            PrepararFormularioComoDashboard(frmHistorial);

            frmHistorial.ShowDialog();
        }

        #endregion
    }
}