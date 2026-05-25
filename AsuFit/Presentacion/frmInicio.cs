using AsuFit.Datos;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AsuFit.Presentacion
{
    public partial class frmInicio : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private InventarioNegocio negocioInventario = new InventarioNegocio();

        public frmInicio()
        {
            InitializeComponent();

            // Bloquea la autogeneración de columnas para mantener el layout definido en el diseñador
            dgvVencimientos.AutoGenerateColumns = false;
            dgvVencidos.AutoGenerateColumns = false;
            dgvProductosStock.AutoGenerateColumns = false;
            dgvProductosStockBajo.AutoGenerateColumns = false;
        }
        #endregion

        #region 2. EVENTOS DE CICLO DE VIDA (Load)
        private void frmInicio_Load(object sender, EventArgs e)
        {
            // Aplicación del tema oscuro a controles nativos
            AplicarTemaGraficoOscuro();
            AplicarTemaOscuroGrillas(dgvProductosStock);
            AplicarTemaOscuroGrillas(dgvProductosStockBajo);
            AplicarTemaOscuroGrillas(dgvVencimientos);
            AplicarTemaOscuroGrillas(dgvVencidos);

            // Obtención y enlace de datos del dashboard
            CargarDashboard();
            CargarTablasInventario();
            CargarVencimientos();

            // Ejecución del proceso de mensajería automatizada
            EnviarCorreosVencimiento();
        }
        #endregion

        #region 3. MÓDULO DE MÉTRICAS Y FINANZAS
        // Obtiene los KPI principales y actualiza los indicadores visuales
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

        // Construye y enlaza la serie de datos para el control Chart
        private void ConfigurarGrafico(decimal ingresos, decimal egresos)
        {
            chartFinanzas.Series.Clear();

            Series serie = new Series("Balance Mensual");
            serie.ChartType = SeriesChartType.Column;
            serie["PointWidth"] = "0.7"; // Ajusta el grosor de las columnas

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
        // Enlaza las tablas de inventario general y alertas de stock
        private void CargarTablasInventario()
        {
            DataTable dtTodos = negocioInventario.ListarProductosBasico();
            if (dtTodos != null)
            {
                dgvProductosStock.DataSource = dtTodos;
                ConfigurarColumnasBasicas(dgvProductosStock);
            }

            DataTable dtBajo = negocioInventario.ListarProductosStockBajo();
            if (dtBajo != null)
            {
                dgvProductosStockBajo.DataSource = dtBajo;
                ConfigurarColumnasBasicas(dgvProductosStockBajo);
            }
        }

        private void dgvProductosStock_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvProductosStock.ClearSelection();
        }

        private void dgvProductosStockBajo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvProductosStockBajo.ClearSelection();
        }

        // Aplica formato condicional según el nivel de existencias (Stock 0 = Crítico, >0 = Advertencia)
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
        // Enlaza las tablas de notificaciones de membresías
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

                dgvVencidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVencidos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los vencimientos: " + ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVencimientos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvVencimientos.ClearSelection();
        }

        private void dgvVencidos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvVencidos.ClearSelection();
        }

        // Formato condicional: Alerta visual para membresías por vencer
        private void dgvVencimientos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvVencimientos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                dgvVencimientos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        // Formato condicional: Alerta visual para membresías vencidas
        private void dgvVencidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvVencidos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                dgvVencidos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }
        #endregion

        #region 6. UTILIDADES Y PROCESOS AUTOMATIZADOS
        // Establece las propiedades estándar de solo lectura y autoajuste para los DataGridViews
        private void ConfigurarColumnasBasicas(DataGridView dgv)
        {
            if (dgv.Columns.Count > 0)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.ReadOnly = true;
                dgv.RowHeadersVisible = false;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.AllowUserToAddRows = false;
            }
        }

        // Recupera la configuración SMTP y envía notificaciones automáticas a los socios pertinentes
        private void EnviarCorreosVencimiento()
        {
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    oConexion.Open();

                    string configQuery = "SELECT CorreoEmisor, ContrasenaCorreo, DiasAviso1, DiasAviso2 FROM Configuracion WHERE IdConfiguracion = 1";
                    SqlCommand cmdConfig = new SqlCommand(configQuery, oConexion);
                    SqlDataReader reader = cmdConfig.ExecuteReader();

                    string correoGym = "";
                    string passGym = "";
                    int avisoLejano = 7;
                    int avisoCercano = 1;

                    if (reader.Read())
                    {
                        correoGym = reader["CorreoEmisor"].ToString();
                        passGym = reader["ContrasenaCorreo"].ToString();
                        if (reader["DiasAviso1"] != DBNull.Value) avisoLejano = Convert.ToInt32(reader["DiasAviso1"]);
                        if (reader["DiasAviso2"] != DBNull.Value) avisoCercano = Convert.ToInt32(reader["DiasAviso2"]);
                    }
                    reader.Close();

                    if (string.IsNullOrEmpty(correoGym) || string.IsNullOrEmpty(passGym)) return;

                    string query = $@"
                        SELECT IdSocio, Nombre, Apellido, Email, FechaVencimiento, 
                               DATEDIFF(day, GETDATE(), FechaVencimiento) AS DiasRestantes
                        FROM Socios
                        WHERE Estado = 'Activo' 
                        AND Email IS NOT NULL AND Email LIKE '%@%'
                        AND DATEDIFF(day, GETDATE(), FechaVencimiento) IN (0, {avisoCercano}, {avisoCercano + 1}, {avisoLejano - 1}, {avisoLejano}, {avisoLejano + 1})
                        AND (FechaUltimoAviso IS NULL OR DATEDIFF(day, FechaUltimoAviso, GETDATE()) >= 4)";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dtAvisos = new DataTable();

                    da.Fill(dtAvisos);

                    if (dtAvisos.Rows.Count > 0)
                    {
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(correoGym, passGym);

                        foreach (DataRow row in dtAvisos.Rows)
                        {
                            string emailDestino = row["Email"].ToString();
                            string nombre = row["Nombre"].ToString();
                            int dias = Convert.ToInt32(row["DiasRestantes"]);
                            int idSocio = Convert.ToInt32(row["IdSocio"]);
                            DateTime fechaVence = Convert.ToDateTime(row["FechaVencimiento"]);

                            MailMessage correo = new MailMessage();
                            correo.From = new MailAddress(correoGym, "AsuFit GYM");
                            correo.To.Add(emailDestino);

                            string diaTexto = "";
                            if (dias == 0) diaTexto = "HOY";
                            else if (dias == avisoCercano) diaTexto = "MAÑANA";
                            else diaTexto = $"en {dias} días";

                            correo.Subject = $"Aviso de Vencimiento AsuFit - Tu plan vence {diaTexto}";

                            CultureInfo idiomaEspanol = new CultureInfo("es-ES");
                            string fechaNatural = fechaVence.ToString("dddd d 'de' MMMM", idiomaEspanol);

                            correo.Body = $"Hola {nombre},\n\nTe recordamos que tu membresía en AsuFit Gym vence el {fechaNatural}.\n\n¡Te esperamos en la recepción para renovar y seguir entrenando con todo!\n\nSaludos,\nEl equipo de AsuFit Gym";
                            correo.IsBodyHtml = false;

                            try
                            {
                                smtp.Send(correo);
                                string updateQuery = "UPDATE Socios SET FechaUltimoAviso = GETDATE() WHERE IdSocio = @IdSocio";
                                SqlCommand cmdUpdate = new SqlCommand(updateQuery, oConexion);
                                cmdUpdate.Parameters.AddWithValue("@IdSocio", idSocio);
                                cmdUpdate.ExecuteNonQuery();
                            }
                            catch
                            {
                                continue; // Omite errores de envío individuales
                            }
                        }
                    }
                }
            }
            catch
            {
                // Manejo de excepciones silenciado intencionalmente en capa de presentación
            }
        }
        #endregion

        #region 7. ESTILOS VISUALES Y RENDERIZADO
        // Sobrescribe las propiedades visuales del control Chart para integrarlo al tema oscuro
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

        // Aplica estilo plano y colores personalizados a las instancias de DataGridView
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

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
        }
        #endregion
    }
}