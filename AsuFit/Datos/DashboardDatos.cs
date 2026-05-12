using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class DashboardDatos
    {
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

                    // 1. Asistencias del Día (Personas que entraron al gimnasio HOY)
                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Asistencias WHERE CAST(FechaHora AS DATE) = CAST(GETDATE() AS DATE)", oConexion);
                    activos = Convert.ToInt32(cmd1.ExecuteScalar());

                    // 2. INGRESOS DEL MES (Todo está unificado ahora en la tabla 'Ventas')
                    string queryIngresos = @"
                        SELECT ISNULL(SUM(Total), 0) 
                        FROM Ventas 
                        WHERE MONTH(Fecha) = MONTH(GETDATE()) 
                        AND YEAR(Fecha) = YEAR(GETDATE())";
                    SqlCommand cmd2 = new SqlCommand(queryIngresos, oConexion);
                    ingresos = Convert.ToDecimal(cmd2.ExecuteScalar());

                    // 3. EGRESOS DEL MES (Luz/Sueldos en 'Gastos' + Proveedores en 'IngresosMercaderia')
                    string queryEgresos = @"
                        SELECT 
                            ISNULL((SELECT SUM(Monto) FROM Gastos WHERE MONTH(FechaGasto) = MONTH(GETDATE()) AND YEAR(FechaGasto) = YEAR(GETDATE())), 0) + 
                            ISNULL((SELECT SUM(CostoTotal) FROM IngresosMercaderia WHERE MONTH(FechaIngreso) = MONTH(GETDATE()) AND YEAR(FechaIngreso) = YEAR(GETDATE())), 0)";
                    SqlCommand cmd3 = new SqlCommand(queryEgresos, oConexion);
                    egresos = Convert.ToDecimal(cmd3.ExecuteScalar());

                    // 4. Vencimientos en los próximos 7 días
                    SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM Socios WHERE FechaVencimiento BETWEEN GETDATE() AND DATEADD(day, 7, GETDATE())", oConexion);
                    vencimientos = Convert.ToInt32(cmd4.ExecuteScalar());
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public DataTable ListarVencimientosProximos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    oConexion.Open();
                    // Traemos los socios que vencen en los próximos 7 días y que aún figuran como "Activo"
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
    }
}