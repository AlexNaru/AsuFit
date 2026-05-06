using System;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public static class GestorAuditoria
    {
        public static void Registrar(string usuario, string modulo, string accion, string detalle)
        {
            try
            {
                // Como ya estamos en la capa de Datos, llamamos directo a tu clase Conexion
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    string query = "INSERT INTO LogAuditoria (Usuario, Modulo, Accion, Detalle) VALUES (@Usu, @Mod, @Acc, @Det)";
                    SqlCommand cmd = new SqlCommand(query, oConexion);

                    cmd.Parameters.AddWithValue("@Usu", usuario);
                    cmd.Parameters.AddWithValue("@Mod", modulo);
                    cmd.Parameters.AddWithValue("@Acc", accion);
                    cmd.Parameters.AddWithValue("@Det", detalle);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // Un error de auditoría nunca debe "colgar" el sistema.
                // Por eso el catch está vacío: si falla, simplemente no guarda el log, pero el usuario puede seguir trabajando.
            }
        }
    }
}