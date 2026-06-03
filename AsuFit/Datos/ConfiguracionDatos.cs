using AsuFit.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class ConfiguracionDatos
    {
        public Configuracion ObtenerConfiguracion()
        {
            Configuracion obj = new Configuracion();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "SELECT * FROM Configuracion WHERE IdConfiguracion = 1";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    oConexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            obj.NombreGimnasio = reader["NombreGimnasio"].ToString();
                            obj.Ruc = reader["RUC"].ToString();
                            obj.Direccion = reader["Direccion"].ToString();
                            obj.Telefono = reader["Telefono"].ToString();
                            obj.CorreoEmisor = reader["CorreoEmisor"].ToString();
                            obj.ContrasenaCorreo = reader["ContrasenaCorreo"].ToString();
                            obj.DiasAviso1 = Convert.ToInt32(reader["DiasAviso1"]);
                            obj.DiasAviso2 = Convert.ToInt32(reader["DiasAviso2"]);
                            obj.RutaBackup = reader["RutaBackup"] != DBNull.Value ? reader["RutaBackup"].ToString() : "";

                            if (reader["Logo"] != DBNull.Value) obj.Logo = (byte[])reader["Logo"];
                        }
                    }
                }
                catch (Exception) { throw; }
            }
            return obj;
        }

        public bool ActualizarDatosGenerales(Configuracion obj)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"UPDATE Configuracion SET 
                             NombreGimnasio = @Nombre, RUC = @RUC, Direccion = @Direccion, 
                             Telefono = @Telefono, RutaBackup = @RutaBackup, Logo = @Logo
                             WHERE IdConfiguracion = 1";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Nombre", obj.NombreGimnasio);
                    cmd.Parameters.AddWithValue("@RUC", obj.Ruc);
                    cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@RutaBackup", obj.RutaBackup);

                    if (obj.Logo != null) cmd.Parameters.AddWithValue("@Logo", obj.Logo);
                    else cmd.Parameters.Add("@Logo", SqlDbType.VarBinary).Value = DBNull.Value;

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
            return respuesta;
        }

        public bool ActualizarNotificaciones(Configuracion obj)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"UPDATE Configuracion SET 
                                     CorreoEmisor = @Correo, ContrasenaCorreo = @Contrasena, 
                                     DiasAviso1 = @Dias1, DiasAviso2 = @Dias2
                                     WHERE IdConfiguracion = 1";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Correo", obj.CorreoEmisor);
                    cmd.Parameters.AddWithValue("@Contrasena", obj.ContrasenaCorreo);
                    cmd.Parameters.AddWithValue("@Dias1", obj.DiasAviso1);
                    cmd.Parameters.AddWithValue("@Dias2", obj.DiasAviso2);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
            return respuesta;
        }

        public void GenerarBackup(string rutaCompleta)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = $"BACKUP DATABASE AsuFitDB TO DISK = '{rutaCompleta}' WITH FORMAT, MEDIANAME = 'AsuFit_Backups', NAME = 'Respaldo Completo AsuFit'";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception) { throw; }
            }
        }
    }
}