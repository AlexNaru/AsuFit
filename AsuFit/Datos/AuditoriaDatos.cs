using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class AuditoriaDatos
    {
        public DataTable ListarAuditoria()
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    // CÓDIGO LIMPIO: Traemos los nombres crudos reales sin alias
                    string query = "SELECT FechaHora, Usuario, Modulo, Accion, Detalle FROM LogAuditoria ORDER BY FechaHora DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, oConexion);
                    da.Fill(dt);
                }
                catch (Exception) { throw; }
            }
            return dt;
        }
    }
}