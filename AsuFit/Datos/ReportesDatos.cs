using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    // Encapsula la lógica de consultas complejas para la generación de reportes financieros.
    public class ReportesDatos
    {
        #region GENERACIÓN DE REPORTES
        // Obtiene el detalle de ventas realizadas en un rango de fechas.
        public DataTable ObtenerIngresosPorFechas(DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"SELECT Fecha, TipoComprobante AS Comprobante, MetodoPago, Total 
                                     FROM Ventas 
                                     WHERE CAST(Fecha AS DATE) BETWEEN @FechaDesde AND @FechaHasta
                                     ORDER BY Fecha DESC";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@FechaDesde", desde.Date);
                    cmd.Parameters.AddWithValue("@FechaHasta", hasta.Date);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception) { throw; }
            }
            return dt;
        }

        // Recupera el Top 5 de productos más vendidos en un periodo determinado.
        public DataTable ObtenerTopProductos(DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"SELECT TOP 5 
                                        P.Nombre AS Producto, 
                                        SUM(VD.Cantidad) AS CantidadVendida, 
                                        SUM(VD.SubTotal) AS Ingresos
                                     FROM VentasDetalle VD
                                     INNER JOIN Productos P ON VD.IdProducto = P.IdProducto
                                     INNER JOIN Ventas V ON VD.IdVenta = V.IdVenta
                                     WHERE CAST(V.Fecha AS DATE) BETWEEN @FechaDesde AND @FechaHasta
                                     AND VD.IdProducto IS NOT NULL
                                     GROUP BY P.Nombre
                                     ORDER BY CantidadVendida DESC";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@FechaDesde", desde.Date);
                    cmd.Parameters.AddWithValue("@FechaHasta", hasta.Date);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception) { throw; }
            }
            return dt;
        }
        #endregion
    }
}