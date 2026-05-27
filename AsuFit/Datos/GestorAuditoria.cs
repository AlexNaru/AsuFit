using System;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    // Clase estática encargada de registrar los eventos críticos en la bitácora del sistema.
    public static class GestorAuditoria
    {
        #region REGISTRO DE EVENTOS
        // Almacena una acción realizada por el usuario en el historial de auditoría.
        public static void Registrar(string usuario, string modulo, string accion, string detalle)
        {
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    string query = "INSERT INTO LogAuditoria (Usuario, Modulo, Accion, Detalle, FechaHora) VALUES (@Usu, @Mod, @Acc, @Det, @Fec)";
                    SqlCommand cmd = new SqlCommand(query, oConexion);

                    cmd.Parameters.AddWithValue("@Usu", usuario);
                    cmd.Parameters.AddWithValue("@Mod", modulo);
                    cmd.Parameters.AddWithValue("@Acc", accion);
                    cmd.Parameters.AddWithValue("@Det", detalle);
                    cmd.Parameters.AddWithValue("@Fec", DateTime.Now);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // Supresión intencionada: El fallo en auditoría no debe bloquear el uso del sistema.
            }
        }
        #endregion
    }
}