using AsuFit.Datos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmHistorialVentas : Form
    {
        public frmHistorialVentas()
        {
            InitializeComponent();
        }

        // EVENTO: Al abrir el formulario
        private void frmHistorialVentas_Load(object sender, EventArgs e)
        {
            // Ponemos por defecto desde el día 1 del mes actual hasta hoy
            dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpHasta.Value = DateTime.Now.Date;
            BuscarVentas();
        }

        // EVENTO: Al escribir en la barra de búsqueda
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarVentas();
        }

        // EVENTO: Al cambiar la fecha de inicio
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            BuscarVentas();
        }

        // EVENTO: Al cambiar la fecha de fin
        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            BuscarVentas();
        }

        // MÉTODO PRINCIPAL: El que hace la consulta a SQL
        private void BuscarVentas()
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"
                        SELECT 
                            v.IdVenta AS [N° Venta],
                            v.Fecha AS [Fecha de Venta],
                            ISNULL(s.Nombre + ' ' + s.Apellido, 'Cliente Ocasional') AS [Cliente],
                            v.MetodoPago AS [Método],
                            v.Total AS [Total Cobrado]
                        FROM Ventas v
                        LEFT JOIN Socios s ON v.IdSocio = s.IdSocio
                        WHERE CAST(v.Fecha AS DATE) BETWEEN @Desde AND @Hasta
                        AND (ISNULL(s.Nombre, '') LIKE '%' + @Filtro + '%' 
                             OR ISNULL(s.Apellido, '') LIKE '%' + @Filtro + '%'
                             OR s.Cedula LIKE '%' + @Filtro + '%'
                             OR CAST(v.IdVenta AS VARCHAR) LIKE '%' + @Filtro + '%')
                        ORDER BY v.IdVenta DESC";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Desde", dtpDesde.Value.Date);
                    cmd.Parameters.AddWithValue("@Hasta", dtpHasta.Value.Date);
                    cmd.Parameters.AddWithValue("@Filtro", txtBuscar.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dtVentas = new DataTable();
                    da.Fill(dtVentas);

                    dgvVentas.DataSource = dtVentas;

                    // Diseño visual de la tabla
                    if (dgvVentas.Columns.Count > 0)
                    {
                        dgvVentas.Columns["Total Cobrado"].DefaultCellStyle.Format = "N0";
                    }

                    CalcularTotales(dtVentas);
                }
                catch (Exception ex)
                {
                    // Si da error, mostramos un mensaje (útil para diagnosticar)
                    MessageBox.Show("Error al cargar el historial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // MÉTODO SECUNDARIO: Suma los totales y cuenta las ventas
        private void CalcularTotales(DataTable dt)
        {
            decimal totalRecaudado = 0;
            foreach (DataRow row in dt.Rows)
            {
                totalRecaudado += Convert.ToDecimal(row["Total Cobrado"]);
            }

            lblCantidadVentas.Text = dt.Rows.Count.ToString();
            lblTotalRecaudado.Text = "Gs. " + totalRecaudado.ToString("N0");
        }

        // EVENTO: Al hacer clic en "Ver Detalle"
        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (dgvVentas.SelectedRows.Count > 0)
            {
                string idVenta = dgvVentas.SelectedRows[0].Cells["N° Venta"].Value.ToString();
                MessageBox.Show($"En el futuro, aquí se abrirá un popup con el detalle de los productos de la Venta N° {idVenta}", "Detalle de Venta");
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una venta de la tabla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}