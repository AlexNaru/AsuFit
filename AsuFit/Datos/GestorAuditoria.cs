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
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    // 1. Agregamos la columna FechaHora y su parámetro @Fec a la consulta
                    string query = "INSERT INTO LogAuditoria (Usuario, Modulo, Accion, Detalle, FechaHora) VALUES (@Usu, @Mod, @Acc, @Det, @Fec)";
                    SqlCommand cmd = new SqlCommand(query, oConexion);

                    cmd.Parameters.AddWithValue("@Usu", usuario);
                    cmd.Parameters.AddWithValue("@Mod", modulo);
                    cmd.Parameters.AddWithValue("@Acc", accion);
                    cmd.Parameters.AddWithValue("@Det", detalle);

                    // 2. LA SOLUCIÓN: C# captura la hora de tu Windows y obliga a Somee a guardarla
                    cmd.Parameters.AddWithValue("@Fec", DateTime.Now);

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