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
        private InventarioNegocio negocioInventario = new InventarioNegocio();

        public frmInicio()
        {
            InitializeComponent();
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            CargarDashboard();
            CargarVencimientos();
            CargarTablasInventario();
            EnviarCorreosVencimiento();
        }

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

            DataTable dtVencimientos = negocio.ListarVencimientosProximos();
            dgvVencimientos.DataSource = dtVencimientos;

            if (dgvVencimientos.Columns.Contains("Telefono"))
            {
                dgvVencimientos.Columns["Telefono"].Visible = false;
            }

            if (dgvVencimientos.Columns.Contains("FechaVencimiento"))
            {
                dgvVencimientos.Columns["FechaVencimiento"].DefaultCellStyle.Format = "dd-MM-yyyy HH:mm";
                dgvVencimientos.Columns["FechaVencimiento"].HeaderText = "Vencimiento";
            }
        }

        private void ConfigurarGrafico(decimal ingresos, decimal egresos)
        {
            chartFinanzas.Series.Clear();

            Series serie = new Series("Balance Mensual");
            serie.ChartType = SeriesChartType.Column;

            decimal utilidad = ingresos - egresos;

            serie.Points.AddXY("Ingresos", ingresos);
            serie.Points[0].Color = Color.MediumSeaGreen;

            serie.Points.AddXY("Egresos", egresos);
            serie.Points[1].Color = Color.IndianRed;

            serie.Points.AddXY("Utilidad", utilidad);
            serie.Points[2].Color = Color.RoyalBlue;

            chartFinanzas.Series.Add(serie);
        }

        private void dgvVencimientos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvVencimientos.ClearSelection();
        }

        private void CargarVencimientos()
        {
            try
            {
                SocioNegocio negocio = new SocioNegocio();
                var listaVencidos = negocio.ListarVencidos();

                dgvVencidos.DataSource = listaVencidos;
                lblVencimientos.Text = listaVencidos.Count.ToString();

                if (dgvVencidos.Columns.Count > 0)
                {
                    foreach (DataGridViewColumn col in dgvVencidos.Columns)
                    {
                        if (col.Name != "Nombre" && col.Name != "Apellido" && col.Name != "FechaVencimiento")
                        {
                            col.Visible = false;
                        }
                    }

                    if (dgvVencidos.Columns.Contains("FechaVencimiento"))
                    {
                        dgvVencidos.Columns["FechaVencimiento"].DefaultCellStyle.Format = "dd-MM-yyyy HH:mm";
                        dgvVencidos.Columns["FechaVencimiento"].HeaderText = "Vencimiento";
                    }

                    dgvVencidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }

                dgvVencidos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los vencimientos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- MOTOR DE CORREOS CONECTADO A LA BASE DE DATOS ---
        private void EnviarCorreosVencimiento()
        {
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    oConexion.Open();

                    // 1. OBTENEMOS LA CONFIGURACIÓN DESDE SQL
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
                    reader.Close(); // Cerramos el lector para poder ejecutar la siguiente consulta

                    // Si no hay correo configurado, salimos del método
                    if (string.IsNullOrEmpty(correoGym) || string.IsNullOrEmpty(passGym)) return;

                    // 2. BUSCAMOS SOCIOS (Usando los días dinámicos configurados por el dueño)
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
                        smtp.Credentials = new NetworkCredential(correoGym, passGym); // Credenciales desde BD

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

                            // Lógica de Asunto Dinámico
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
                                continue;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Try-Catch silencioso
            }
        }

        private void dgvVencidos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvVencidos.ClearSelection();
        }

        private void dgvVencidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvVencidos.Columns.Contains("FechaVencimiento"))
            {
                var celdaFecha = dgvVencidos.Rows[e.RowIndex].Cells["FechaVencimiento"].Value;

                if (celdaFecha != null && celdaFecha != DBNull.Value)
                {
                    DateTime fechaVencimiento = Convert.ToDateTime(celdaFecha);

                    if (fechaVencimiento.Date < DateTime.Now.Date)
                    {
                        dgvVencidos.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                        dgvVencidos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                    }
                }
            }
        }

        private void dgvVencimientos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgvVencimientos.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Gold;
            dgvVencimientos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
        }

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

        private void ConfigurarColumnasBasicas(DataGridView dgv)
        {
            if (dgv.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Name != "Nombre" && col.Name != "StockActual")
                    {
                        col.Visible = false;
                    }
                }

                if (dgv.Columns.Contains("StockActual"))
                {
                    dgv.Columns["StockActual"].HeaderText = "Stock";
                }
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.ReadOnly = true;
                dgv.RowHeadersVisible = false;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.AllowUserToAddRows = false;
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
    }
}