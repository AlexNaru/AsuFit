using AsuFit.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
        }

        // 1. EL MÉTODO CENTRAL QUE HACE EL TRABAJO
        private void CargarReporteIngresos()
        {
            // Validamos en silencio: si ponen una fecha Desde mayor que Hasta, limpiamos y no hacemos nada
            // (No usamos MessageBox aquí para no molestar al usuario mientras hace clics en el calendario)
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                dgvIngresos.DataSource = null;
                lblTotalIngresos.Text = "TOTAL RECAUDADO: Gs. 0";
                return;
            }

            DataTable dtIngresos = new DataTable();
            decimal sumaTotal = 0;

            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"SELECT Fecha, TipoComprobante AS Comprobante, MetodoPago AS [Método de Pago], Total 
                             FROM Ventas 
                             WHERE CAST(Fecha AS DATE) BETWEEN @FechaDesde AND @FechaHasta
                             ORDER BY Fecha DESC";

                    SqlCommand cmd = new SqlCommand(query, oConexion);

                    cmd.Parameters.AddWithValue("@FechaDesde", dtpDesde.Value.Date);
                    cmd.Parameters.AddWithValue("@FechaHasta", dtpHasta.Value.Date);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    oConexion.Open();
                    da.Fill(dtIngresos);

                    dgvIngresos.DataSource = dtIngresos;

                    foreach (DataRow fila in dtIngresos.Rows)
                    {
                        sumaTotal += Convert.ToDecimal(fila["Total"]);
                    }

                    lblTotalIngresos.Text = "TOTAL RECAUDADO: Gs. " + sumaTotal.ToString("N0");

                    if (dgvIngresos.Columns.Count > 0)
                    {
                        dgvIngresos.Columns["Total"].DefaultCellStyle.Format = "N0";
                        dgvIngresos.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvIngresos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 2. EVENTO LOAD (Cuando se abre la pantalla)
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

        // 3. EVENTOS DE LOS CALENDARIOS (Selecciona el dtpDesde en diseño, ve a sus eventos "Rayito" y doble clic en ValueChanged)
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            CargarReporteIngresos();
        }

        // Haz lo mismo para el dtpHasta
        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            CargarReporteIngresos();
        }

        private void CargarTopProductos()
        {
            // Validación silenciosa de fechas
            if (dtpDesdeTop.Value.Date > dtpHastaTop.Value.Date) return;

            DataTable dtTop = new DataTable();

            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    // Esta consulta une Ventas, VentasDetalle y Productos.
                    // Agrupa por nombre de producto y los ordena de mayor a menor según la cantidad vendida.
                    // Ignora las filas donde IdProducto es NULL (para no contar las mensualidades como si fueran un "producto").
                    string query = @"SELECT TOP 5 
                                P.Nombre AS Producto, 
                                SUM(VD.Cantidad) AS [Cantidad Vendida], 
                                SUM(VD.SubTotal) AS Ingresos
                             FROM VentasDetalle VD
                             INNER JOIN Productos P ON VD.IdProducto = P.IdProducto
                             INNER JOIN Ventas V ON VD.IdVenta = V.IdVenta
                             WHERE CAST(V.Fecha AS DATE) BETWEEN @FechaDesde AND @FechaHasta
                             AND VD.IdProducto IS NOT NULL
                             GROUP BY P.Nombre
                             ORDER BY [Cantidad Vendida] DESC";

                    SqlCommand cmd = new SqlCommand(query, oConexion);

                    cmd.Parameters.AddWithValue("@FechaDesde", dtpDesdeTop.Value.Date);
                    cmd.Parameters.AddWithValue("@FechaHasta", dtpHastaTop.Value.Date);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    oConexion.Open();
                    da.Fill(dtTop);

                    dgvTopProductos.DataSource = dtTop;

                    // Mejoramos el diseño visual de la grilla del Top 5
                    if (dgvTopProductos.Columns.Count > 0)
                    {
                        dgvTopProductos.Columns["Ingresos"].DefaultCellStyle.Format = "N0";
                        dgvTopProductos.Columns["Ingresos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvTopProductos.Columns["Cantidad Vendida"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        dgvTopProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgvTopProductos.Columns["Producto"].FillWeight = 200; // Le damos más espacio al nombre
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el Top 5: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
