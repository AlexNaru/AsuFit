using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class ArqueoDatos
    {
        // Trae la suma neta de la caja: Ingresos (Cuotas + Productos) menos Egresos (Gastos + Compras)
        public decimal ObtenerTotalDelDia(DateTime fecha)
        {
            decimal totalNeto = 0;
            using (System.Data.SqlClient.SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    // Consulta ajustada EXACTAMENTE a tu base de datos actual
                    string query = @"
                        DECLARE @Ingresos DECIMAL(18,2) = 0;
                        DECLARE @Egresos DECIMAL(18,2) = 0;

                        -- 1. SUMAR INGRESOS (Mensualidades en 'Pagos' + Productos en 'Ventas')
                        SET @Ingresos = 
                            ISNULL((SELECT SUM(Monto) FROM Pagos WHERE CAST(FechaPago AS DATE) = CAST(@Fecha AS DATE)), 0) + 
                            ISNULL((SELECT SUM(Total) FROM Ventas WHERE CAST(Fecha AS DATE) = CAST(@Fecha AS DATE)), 0);

                        -- 2. SUMAR EGRESOS (Gastos + Compras de Mercadería)
                        SET @Egresos = 
                            ISNULL((SELECT SUM(Monto) FROM Gastos WHERE CAST(FechaGasto AS DATE) = CAST(@Fecha AS DATE)), 0) + 
                            ISNULL((SELECT SUM(CostoTotal) FROM IngresosMercaderia WHERE CAST(FechaIngreso AS DATE) = CAST(@Fecha AS DATE)), 0);

                        -- 3. RETORNAR EL NETO ESPERADO EN LA CAJA
                        SELECT (@Ingresos - @Egresos) AS TotalCaja;";

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Fecha", fecha);

                    oConexion.Open();
                    totalNeto = Convert.ToDecimal(cmd.ExecuteScalar());
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return totalNeto;
        }

        // Guarda el cierre de caja en la tabla nueva
        public bool RegistrarCierre(decimal totalSistema, decimal efectivoCaja, decimal diferencia, string usuario)
        {
            bool respuesta = false;
            using (System.Data.SqlClient.SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    // Agregamos UsuarioRegistra al INSERT
                    string query = @"INSERT INTO ArqueosCaja (FechaHora, TotalIngresosSistema, EfectivoDeclarado, Diferencia, Estado, UsuarioRegistra) 
                                     VALUES (GETDATE(), @TotalSist, @Efectivo, @Dif, 'CERRADO', @Usuario)";

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@TotalSist", totalSistema);
                    cmd.Parameters.AddWithValue("@Efectivo", efectivoCaja);
                    cmd.Parameters.AddWithValue("@Dif", diferencia);
                    cmd.Parameters.AddWithValue("@Usuario", usuario); // Mandamos el nombre a SQL

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return respuesta;
        }

        public DataTable ListarHistorialArqueos(DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"SELECT IdArqueo, 
                                            FechaHora, 
                                            TotalIngresosSistema, 
                                            EfectivoDeclarado, 
                                            Diferencia, 
                                            UsuarioRegistra, 
                                            Estado 
                                     FROM ArqueosCaja 
                                     WHERE CAST(FechaHora AS DATE) >= @Desde 
                                     AND CAST(FechaHora AS DATE) <= @Hasta
                                     ORDER BY FechaHora DESC";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
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