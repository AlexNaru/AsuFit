using AsuFit.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class PagoDatos
    {
        // Recibimos el objeto Pago, y la cantidad de días que le tenemos que sumar a su cuenta
        public bool RegistrarCobro(Pago objPago, int diasPlan, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                // Iniciamos la TRANSACCIÓN: "O se hace todo, o no se hace nada"
                SqlTransaction transaccion = oConexion.BeginTransaction();

                try
                {
                    // 1. INSERTAR EL PAGO
                    string queryPago = @"INSERT INTO Pagos (IdSocio, Monto, FechaPago, MetodoPago, Concepto) 
                                       VALUES (@IdSocio, @Monto, GETDATE(), @MetodoPago, @Concepto)";

                    SqlCommand cmdPago = new SqlCommand(queryPago, oConexion, transaccion);
                    cmdPago.Parameters.AddWithValue("@IdSocio", objPago.IdSocio);
                    cmdPago.Parameters.AddWithValue("@Monto", objPago.Monto);
                    cmdPago.Parameters.AddWithValue("@MetodoPago", objPago.MetodoPago);
                    cmdPago.Parameters.AddWithValue("@Concepto", objPago.Concepto);

                    cmdPago.ExecuteNonQuery();

                    // 2. ACTUALIZAR AL SOCIO (La renovación)
                    // Truco Pro: Si su plan todavía no venció, le sumamos los días a lo que ya le quedaba. 
                    // Si ya estaba vencido, le sumamos los días a partir de HOY (GETDATE()).
                    string querySocio = @"UPDATE Socios SET 
                                          Estado = 'Activo',
                                          FechaVencimiento = CASE 
                                            WHEN FechaVencimiento > GETDATE() THEN DATEADD(day, @Dias, FechaVencimiento)
                                            ELSE DATEADD(day, @Dias, GETDATE()) 
                                          END
                                          WHERE IdSocio = @IdSocio";

                    SqlCommand cmdSocio = new SqlCommand(querySocio, oConexion, transaccion);
                    cmdSocio.Parameters.AddWithValue("@Dias", diasPlan);
                    cmdSocio.Parameters.AddWithValue("@IdSocio", objPago.IdSocio);

                    cmdSocio.ExecuteNonQuery();

                    // Si las 2 cosas salieron bien, confirmamos (Commit)
                    transaccion.Commit();
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    // Si algo falló, deshacemos todo para que no queden datos a medias (Rollback)
                    transaccion.Rollback();
                    mensaje = "Error al procesar el pago: " + ex.Message;
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public DataTable ListarHistorialPagos(DateTime desde, DateTime hasta, string busqueda)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    // Usamos CAST(FechaPago AS DATE) para ignorar la hora y buscar solo por el día
                    string query = @"SELECT p.IdPago, 
                                    s.Cedula, 
                                    s.Nombre + ' ' + s.Apellido AS Socio, 
                                    p.FechaPago, 
                                    p.Monto, 
                                    p.MetodoPago, 
                                    p.Concepto 
                                 FROM Pagos p
                                 INNER JOIN Socios s ON p.IdSocio = s.IdSocio
                                 WHERE CAST(p.FechaPago AS DATE) >= @Desde 
                                 AND CAST(p.FechaPago AS DATE) <= @Hasta
                                 AND (s.Cedula LIKE @Busqueda OR s.Nombre LIKE @Busqueda OR s.Apellido LIKE @Busqueda)
                                 ORDER BY p.FechaPago DESC"; // DESC para ver los más nuevos primero

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);
                    cmd.Parameters.AddWithValue("@Busqueda", "%" + busqueda + "%");

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