using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    // Centraliza la recopilación de información estadística y métricas de desempeño para el panel principal.
    public class DashboardDatos
    {
        #region CONSOLIDACIÓN DE MÉTRICAS
        // Ejecuta consultas analíticas agrupadas para recuperar indicadores clave de rendimiento (KPIs).
        public void ObtenerMeticasPrincipales(out int activos, out decimal ingresos, out decimal egresos, out int vencimientos)
        {
            activos = 0;
            ingresos = 0;
            egresos = 0;
            vencimientos = 0;

            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    oConexion.Open();

                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Asistencias WHERE CAST(FechaHora AS DATE) = CAST(GETDATE() AS DATE)", oConexion);
                    activos = Convert.ToInt32(cmd1.ExecuteScalar());

                    string queryIngresos = @"
                        SELECT ISNULL(SUM(Total), 0) 
                        FROM Ventas 
                        WHERE MONTH(Fecha) = MONTH(GETDATE()) 
                        AND YEAR(Fecha) = YEAR(GETDATE())";
                    SqlCommand cmd2 = new SqlCommand(queryIngresos, oConexion);
                    ingresos = Convert.ToDecimal(cmd2.ExecuteScalar());

                    string queryEgresos = @"
                        SELECT 
                            ISNULL((SELECT SUM(Monto) FROM Gastos WHERE MONTH(FechaGasto) = MONTH(GETDATE()) AND YEAR(FechaGasto) = YEAR(GETDATE())), 0) + 
                            ISNULL((SELECT SUM(CostoTotal) FROM IngresosMercaderia WHERE MONTH(FechaIngreso) = MONTH(GETDATE()) AND YEAR(FechaIngreso) = YEAR(GETDATE())), 0)";
                    SqlCommand cmd3 = new SqlCommand(queryEgresos, oConexion);
                    egresos = Convert.ToDecimal(cmd3.ExecuteScalar());

                    SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM Socios WHERE FechaVencimiento BETWEEN GETDATE() AND DATEADD(day, 7, GETDATE())", oConexion);
                    vencimientos = Convert.ToInt32(cmd4.ExecuteScalar());
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Genera un listado de los socios cuyas suscripciones caducarán dentro del umbral de 7 días.
        public DataTable ListarVencimientosProximos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    oConexion.Open();

                    string query = @"SELECT Nombre, Apellido, Telefono, FechaVencimiento 
                                     FROM Socios 
                                     WHERE FechaVencimiento > GETDATE() 
                                     AND FechaVencimiento <= DATEADD(day, 7, GETDATE())
                                     AND Estado = 'Activo'
                                     ORDER BY FechaVencimiento ASC";

                    SqlDataAdapter da = new SqlDataAdapter(query, oConexion);
                    da.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return dt;
        }
        #endregion
    }
}