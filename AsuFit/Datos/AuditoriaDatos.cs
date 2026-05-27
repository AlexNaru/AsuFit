using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    // Provee los métodos de lectura para la visualización del registro de eventos del sistema.
    public class AuditoriaDatos
    {
        #region LECTURA DE EVENTOS
        // Obtiene el listado histórico de interacciones registradas dentro de un rango de fechas.
        public DataTable ListarAuditoria(DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"SELECT FechaHora, Usuario, Modulo, Accion, Detalle 
                                     FROM LogAuditoria 
                                     WHERE CAST(FechaHora AS DATE) >= @Desde 
                                     AND CAST(FechaHora AS DATE) <= @Hasta 
                                     ORDER BY FechaHora DESC";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Desde", desde.Date);
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Date);

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