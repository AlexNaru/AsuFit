using AsuFit.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class SocioDatos
    {
        // Método para registrar socio normal
        public bool RegistrarSocio(Socio objSocio)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"INSERT INTO Socios 
                                    (Cedula, Nombre, Apellido, Email, Telefono, FechaNacimiento, NombreContactoEmergencia, TelefonoEmergencia, FechaRegistro, IdPlan, FechaVencimiento, Estado, RUC) 
                                    VALUES 
                                    (@Cedula, @Nombre, @Apellido, @Email, @Telefono, @FechaNacimiento, @NombreContactoEmergencia, @TelefonoEmergencia, @FechaRegistro, @IdPlan, @FechaVencimiento, @Estado, @RUC)";

                    SqlCommand cmd = new SqlCommand(query, oConexion);

                    cmd.Parameters.AddWithValue("@Cedula", objSocio.Cedula);
                    cmd.Parameters.AddWithValue("@Nombre", objSocio.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", objSocio.Apellido);
                    cmd.Parameters.AddWithValue("@Email", objSocio.Email);
                    cmd.Parameters.AddWithValue("@Telefono", objSocio.Telefono);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", objSocio.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@NombreContactoEmergencia", objSocio.NombreContactoEmergencia);
                    cmd.Parameters.AddWithValue("@TelefonoEmergencia", objSocio.TelefonoEmergencia);
                    cmd.Parameters.AddWithValue("@FechaRegistro", objSocio.FechaRegistro);
                    cmd.Parameters.AddWithValue("@IdPlan", objSocio.IdPlan);
                    cmd.Parameters.AddWithValue("@FechaVencimiento", objSocio.FechaVencimiento);
                    cmd.Parameters.AddWithValue("@Estado", objSocio.Estado);

                    // --- CORRECCIÓN: Faltaba enviar el parámetro del RUC ---
                    cmd.Parameters.AddWithValue("@RUC", objSocio.Ruc ?? (object)DBNull.Value);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return respuesta;
        }

        // Método para registrar socio con el primer pago (Devuelve el ID)
        public int InsertarSocioYObtenerId(Socio objSocio, out string mensaje)
        {
            mensaje = string.Empty;
            int idGenerado = 0;

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    // --- CORRECCIÓN: Agregamos el RUC al INSERT ---
                    string query = @"INSERT INTO Socios 
                            (Cedula, Nombre, Apellido, Email, Telefono, FechaNacimiento, NombreContactoEmergencia, TelefonoEmergencia, FechaRegistro, IdPlan, FechaVencimiento, Estado, RUC) 
                            VALUES 
                            (@Cedula, @Nombre, @Apellido, @Email, @Telefono, @FechaNacimiento, @NombreContactoEmergencia, @TelefonoEmergencia, @FechaRegistro, @IdPlan, @FechaVencimiento, @Estado, @RUC);
                            SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, conexion);

                    cmd.Parameters.AddWithValue("@Cedula", objSocio.Cedula);
                    cmd.Parameters.AddWithValue("@Nombre", objSocio.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", objSocio.Apellido);
                    cmd.Parameters.AddWithValue("@Email", objSocio.Email);
                    cmd.Parameters.AddWithValue("@Telefono", objSocio.Telefono);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", objSocio.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@NombreContactoEmergencia", objSocio.NombreContactoEmergencia);
                    cmd.Parameters.AddWithValue("@TelefonoEmergencia", objSocio.TelefonoEmergencia);
                    cmd.Parameters.AddWithValue("@FechaRegistro", objSocio.FechaRegistro);
                    cmd.Parameters.AddWithValue("@IdPlan", objSocio.IdPlan);
                    cmd.Parameters.AddWithValue("@FechaVencimiento", objSocio.FechaVencimiento);
                    cmd.Parameters.AddWithValue("@Estado", objSocio.Estado);

                    // --- CORRECCIÓN: Faltaba enviar el parámetro del RUC ---
                    cmd.Parameters.AddWithValue("@RUC", objSocio.Ruc ?? (object)DBNull.Value);

                    conexion.Open();
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && int.TryParse(resultado.ToString(), out idGenerado))
                    {
                        return idGenerado;
                    }
                    else
                    {
                        mensaje = "No se pudo obtener el ID del nuevo socio generado por la base de datos.";
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al intentar registrar el socio en la base de datos: " + ex.Message;
                return 0;
            }
        }

        public bool CambiarEstadoSocio(int idSocio, string nuevoEstado)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "UPDATE Socios SET Estado = @Estado WHERE IdSocio = @IdSocio";
                    SqlCommand cmd = new SqlCommand(query, oConexion);

                    cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@IdSocio", idSocio);

                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public DataTable ListarSocios(string estado)
        {
            DataTable dtSocios = new DataTable();
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    // --- CORRECCIÓN: Agregamos s.RUC al SELECT para que la grilla lo tenga en memoria ---
                    string query = @"SELECT s.IdSocio, s.Cedula, s.Nombre, s.Apellido, s.Email, s.RUC, s.Telefono, 
                                     s.FechaNacimiento, s.NombreContactoEmergencia, s.TelefonoEmergencia, 
                                     s.FechaRegistro, p.NombrePlan AS TipoPlan, 
                                     p.Precio, s.IdPlan, s.FechaVencimiento, s.Estado
                                     FROM Socios s
                                     INNER JOIN Planes p ON s.IdPlan = p.IdPlan
                                     WHERE s.Estado = @estado";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
                    adaptador.Fill(dtSocios);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return dtSocios;
        }

        public bool EditarSocio(Socio obj)
        {
            using (System.Data.SqlClient.SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"UPDATE Socios SET 
                                    Cedula = @Cedula, 
                                    Nombre = @Nombre, 
                                    Apellido = @Apellido, 
                                    Email = @Email, 
                                    RUC = @Ruc, 
                                    Telefono = @Telefono, 
                                    FechaNacimiento = @FechaNacimiento, 
                                    NombreContactoEmergencia = @NombreContactoEmergencia, 
                                    TelefonoEmergencia = @TelefonoEmergencia, 
                                    IdPlan = @IdPlan 
                                    WHERE IdSocio = @IdSocio";

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, oConexion);

                    cmd.Parameters.AddWithValue("@Cedula", obj.Cedula);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Ruc", obj.Ruc ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", obj.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@NombreContactoEmergencia", obj.NombreContactoEmergencia);
                    cmd.Parameters.AddWithValue("@TelefonoEmergencia", obj.TelefonoEmergencia);
                    cmd.Parameters.AddWithValue("@IdPlan", obj.IdPlan);
                    cmd.Parameters.AddWithValue("@IdSocio", obj.IdSocio);

                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public bool EliminarSocio(int idSocio)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Socios WHERE IdSocio = @IdSocio", oConexion);
                    cmd.Parameters.AddWithValue("@IdSocio", idSocio);

                    oConexion.Open();
                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0) respuesta = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return respuesta;
        }

        public bool ExisteCedula(string cedula, int idSocioActual)
        {
            bool existe = false;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Socios WHERE Cedula = @Cedula AND IdSocio != @IdSocio";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Cedula", cedula);
                    cmd.Parameters.AddWithValue("@IdSocio", idSocioActual);

                    oConexion.Open();
                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    if (cantidad > 0)
                    {
                        existe = true;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return existe;
        }

        public Socio BuscarSocioPorCedula(string documento)
        {
            Socio socioEncontrado = null;
            using (System.Data.SqlClient.SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"
                        SELECT 
                            s.IdSocio, s.Nombre, s.Apellido, s.FechaVencimiento, s.Estado, 
                            s.Email, s.RUC, p.NombrePlan 
                        FROM Socios s
                        LEFT JOIN Planes p ON s.IdPlan = p.IdPlan 
                        WHERE s.Cedula = @Documento OR s.RUC = @Documento";

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Documento", documento);

                    oConexion.Open();
                    using (System.Data.SqlClient.SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            socioEncontrado = new Socio();
                            socioEncontrado.IdSocio = Convert.ToInt32(dr["IdSocio"]);
                            socioEncontrado.Nombre = dr["Nombre"].ToString();
                            socioEncontrado.Apellido = dr["Apellido"].ToString();
                            socioEncontrado.FechaVencimiento = dr["FechaVencimiento"] != DBNull.Value ? Convert.ToDateTime(dr["FechaVencimiento"]) : (DateTime?)null;
                            socioEncontrado.Estado = dr["Estado"].ToString();
                            socioEncontrado.NombrePlan = dr["NombrePlan"] != DBNull.Value ? dr["NombrePlan"].ToString() : "Plan no asignado";
                            socioEncontrado.Email = dr["Email"].ToString();
                            socioEncontrado.Ruc = dr["RUC"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return socioEncontrado;
        }

        public Socio BuscarSocioPorId(int idSocio)
        {
            Socio socioEncontrado = null;
            using (System.Data.SqlClient.SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"
                        SELECT 
                            s.IdSocio, s.Cedula, s.Nombre, s.Apellido, s.FechaVencimiento, s.Estado, 
                            s.Email, s.RUC, p.NombrePlan 
                        FROM Socios s
                        LEFT JOIN Planes p ON s.IdPlan = p.IdPlan 
                        WHERE s.IdSocio = @IdSocio";

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@IdSocio", idSocio);

                    oConexion.Open();
                    using (System.Data.SqlClient.SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            socioEncontrado = new Socio();
                            socioEncontrado.IdSocio = Convert.ToInt32(dr["IdSocio"]);
                            socioEncontrado.Cedula = dr["Cedula"].ToString();
                            socioEncontrado.Nombre = dr["Nombre"].ToString();
                            socioEncontrado.Apellido = dr["Apellido"].ToString();
                            socioEncontrado.FechaVencimiento = dr["FechaVencimiento"] != DBNull.Value ? Convert.ToDateTime(dr["FechaVencimiento"]) : (DateTime?)null;
                            socioEncontrado.Estado = dr["Estado"].ToString();
                            socioEncontrado.Email = dr["Email"].ToString();
                            socioEncontrado.Ruc = dr["RUC"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return socioEncontrado;
        }

        public void RegistrarAsistencia(int idSocio)
        {
            using (System.Data.SqlClient.SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "INSERT INTO Asistencias (IdSocio, FechaHora) VALUES (@IdSocio, GETDATE())";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@IdSocio", idSocio);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al guardar historial: " + ex.Message);
                }
            }
        }

        public List<Socio> ListarVencidos()
        {
            List<Socio> lista = new List<Socio>();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"SELECT Nombre, Apellido, FechaVencimiento 
                             FROM Socios 
                             WHERE FechaVencimiento < GETDATE() AND Estado = 'Activo'
                             ORDER BY FechaVencimiento DESC";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Socio()
                            {
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                FechaVencimiento = Convert.ToDateTime(dr["FechaVencimiento"])
                            });
                        }
                    }
                }
                catch (Exception) { throw; }
            }
            return lista;
        }

        public bool RenovarMembresiaSocio(int idSocio, int diasPlan)
        {
            using (System.Data.SqlClient.SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"
                        UPDATE Socios 
                        SET Estado = 'Activo', 
                            FechaVencimiento = CASE 
                                WHEN FechaVencimiento < GETDATE() THEN DATEADD(day, @Dias, GETDATE()) 
                                ELSE DATEADD(day, @Dias, FechaVencimiento) 
                            END 
                        WHERE IdSocio = @IdSocio";

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Dias", diasPlan);
                    cmd.Parameters.AddWithValue("@IdSocio", idSocio);

                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}