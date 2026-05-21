using System;
using System.Data.SqlClient;
using AsuFit.Entidades;

namespace AsuFit.Datos
{
    public class AsistenciaDatos
    {
        public bool RegistrarAsistencia(Asistencia obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "INSERT INTO Asistencias (IdSocio, FechaHora) VALUES (@IdSocio, GETDATE())";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@IdSocio", obj.IdSocio);

                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
        }
    }
}