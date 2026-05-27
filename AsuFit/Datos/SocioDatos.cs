using AsuFit.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    // Gestiona las operaciones de persistencia para la entidad Socios.
    public class SocioDatos
    {
        #region OPERACIONES DE REGISTRO
        // Inserta un nuevo registro de socio en la base de datos.
        public bool RegistrarSocio(Socio objSocio)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"INSERT INTO Socios (Cedula, Nombre, Apellido, Email, Telefono, FechaNacimiento, NombreContactoEmergencia, TelefonoEmergencia, FechaRegistro, IdPlan, FechaVencimiento, Estado, RUC) 
                                    VALUES (@Cedula, @Nombre, @Apellido, @Email, @Telefono, @FechaNacimiento, @NombreContactoEmergencia, @TelefonoEmergencia, @FechaRegistro, @IdPlan, @FechaVencimiento, @Estado, @RUC)";

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
                    cmd.Parameters.AddWithValue("@RUC", objSocio.Ruc ?? (object)DBNull.Value);

                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
        }

        // Inserta un socio y devuelve el identificador generado automáticamente.
        public int InsertarSocioYObtenerId(Socio objSocio, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    string query = @"INSERT INTO Socios (Cedula, Nombre, Apellido, Email, Telefono, FechaNacimiento, NombreContactoEmergencia, TelefonoEmergencia, FechaRegistro, IdPlan, FechaVencimiento, Estado, RUC) 
                            VALUES (@Cedula, @Nombre, @Apellido, @Email, @Telefono, @FechaNacimiento, @NombreContactoEmergencia, @TelefonoEmergencia, @FechaRegistro, @IdPlan, @FechaVencimiento, @Estado, @RUC);
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
                    cmd.Parameters.AddWithValue("@RUC", objSocio.Ruc ?? (object)DBNull.Value);

                    conexion.Open();
                    object resultado = cmd.ExecuteScalar();
                    return (resultado != null) ? Convert.ToInt32(resultado) : 0;
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error en base de datos: " + ex.Message;
                return 0;
            }
        }
        #endregion

        #region OPERACIONES DE EDICIÓN Y ESTADO
        // Actualiza los datos generales de un socio existente.
        public bool EditarSocio(Socio obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"UPDATE Socios SET Cedula = @Cedula, Nombre = @Nombre, Apellido = @Apellido, Email = @Email, 
                                    RUC = @Ruc, Telefono = @Telefono, FechaNacimiento = @FechaNacimiento, 
                                    NombreContactoEmergencia = @NombreContactoEmergencia, TelefonoEmergencia = @TelefonoEmergencia, IdPlan = @IdPlan 
                                    WHERE IdSocio = @IdSocio";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
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
                catch (Exception) { throw; }
            }
        }

        // Cambia el estado de un socio (Activo/Inactivo).
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
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { return false; }
            }
        }

        // Elimina físicamente el registro de un socio de la base de datos.
        public bool EliminarSocio(int idSocio)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Socios WHERE IdSocio = @IdSocio", oConexion);
                    cmd.Parameters.AddWithValue("@IdSocio", idSocio);
                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
        }
        #endregion

        #region CONSULTAS DE SOCIOS
        // Lista socios según estado actual.
        public DataTable ListarSocios(string estado)
        {
            DataTable dtSocios = new DataTable();
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
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
            catch (Exception) { throw; }
            return dtSocios;
        }

        // Verifica si un número de cédula ya existe en otro registro.
        public bool ExisteCedula(string cedula, int idSocioActual)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Socios WHERE Cedula = @Cedula AND IdSocio != @IdSocio";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Cedula", cedula);
                    cmd.Parameters.AddWithValue("@IdSocio", idSocioActual);

                    oConexion.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
                catch (Exception) { throw; }
            }
        }

        // Busca datos básicos de un socio por documento.
        public Socio BuscarSocioPorCedula(string documento)
        {
            Socio socioEncontrado = null;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"SELECT s.IdSocio, s.Nombre, s.Apellido, s.FechaVencimiento, s.Estado, 
                                     s.Email, s.RUC, p.NombrePlan 
                                     FROM Socios s
                                     LEFT JOIN Planes p ON s.IdPlan = p.IdPlan 
                                     WHERE s.Cedula = @Documento OR s.RUC = @Documento";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Documento", documento);

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            socioEncontrado = new Socio()
                            {
                                IdSocio = Convert.ToInt32(dr["IdSocio"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                FechaVencimiento = dr["FechaVencimiento"] != DBNull.Value ? Convert.ToDateTime(dr["FechaVencimiento"]) : (DateTime?)null,
                                Estado = dr["Estado"].ToString(),
                                NombrePlan = dr["NombrePlan"] != DBNull.Value ? dr["NombrePlan"].ToString() : "Plan no asignado",
                                Email = dr["Email"].ToString(),
                                Ruc = dr["RUC"].ToString()
                            };
                        }
                    }
                }
                catch (Exception) { throw; }
            }
            return socioEncontrado;
        }

        // Busca un socio utilizando su identificador interno.
        public Socio BuscarSocioPorId(int idSocio)
        {
            Socio socioEncontrado = null;
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"SELECT s.IdSocio, s.Cedula, s.Nombre, s.Apellido, s.FechaVencimiento, s.Estado, 
                                     s.Email, s.RUC, p.NombrePlan 
                                     FROM Socios s
                                     LEFT JOIN Planes p ON s.IdPlan = p.IdPlan 
                                     WHERE s.IdSocio = @IdSocio";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@IdSocio", idSocio);

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            socioEncontrado = new Socio()
                            {
                                IdSocio = Convert.ToInt32(dr["IdSocio"]),
                                Cedula = dr["Cedula"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                FechaVencimiento = dr["FechaVencimiento"] != DBNull.Value ? Convert.ToDateTime(dr["FechaVencimiento"]) : (DateTime?)null,
                                Estado = dr["Estado"].ToString(),
                                Email = dr["Email"].ToString(),
                                Ruc = dr["RUC"].ToString()
                            };
                        }
                    }
                }
                catch (Exception) { throw; }
            }
            return socioEncontrado;
        }

        // Lista socios cuyas membresías se encuentran vencidas.
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
        #endregion

        #region OPERACIONES ADICIONALES
        // Registra de forma silenciosa la asistencia física de un socio.
        public void RegistrarAsistencia(int idSocio)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "INSERT INTO Asistencias (IdSocio, FechaHora) VALUES (@IdSocio, GETDATE())";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@IdSocio", idSocio);
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception) { /* Fallo silencioso permitido */ }
            }
        }

        // Renueva el vencimiento de un plan.
        public bool RenovarMembresiaSocio(int idSocio, int diasPlan)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"UPDATE Socios SET Estado = 'Activo', 
                                    FechaVencimiento = CASE 
                                        WHEN FechaVencimiento < GETDATE() THEN DATEADD(day, @Dias, GETDATE()) 
                                        ELSE DATEADD(day, @Dias, FechaVencimiento) 
                                    END 
                                    WHERE IdSocio = @IdSocio";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Dias", diasPlan);
                    cmd.Parameters.AddWithValue("@IdSocio", idSocio);

                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
        }
        #endregion

        #region NOTIFICACIONES Y ALERTAS (LECTURA/ESCRITURA)
        // Recupera la configuración del correo emisor desde la base de datos.
        public DataTable ObtenerConfiguracionCorreo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                string query = "SELECT CorreoEmisor, ContrasenaCorreo, DiasAviso1, DiasAviso2 FROM Configuracion WHERE IdConfiguracion = 1";
                SqlCommand cmd = new SqlCommand(query, oConexion);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // Obtiene la lista de socios que requieren notificación por vencimiento inminente.
        public DataTable ObtenerSociosParaAvisoCorreo(int avisoCercano, int avisoLejano)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                string query = $@"
                    SELECT IdSocio, Nombre, Apellido, Email, FechaVencimiento, 
                           DATEDIFF(day, GETDATE(), FechaVencimiento) AS DiasRestantes
                    FROM Socios
                    WHERE Estado = 'Activo' 
                    AND Email IS NOT NULL AND Email LIKE '%@%'
                    AND DATEDIFF(day, GETDATE(), FechaVencimiento) IN (0, {avisoCercano}, {avisoCercano + 1}, {avisoLejano - 1}, {avisoLejano}, {avisoLejano + 1})
                    AND (FechaUltimoAviso IS NULL OR DATEDIFF(day, FechaUltimoAviso, GETDATE()) >= 4)";

                SqlCommand cmd = new SqlCommand(query, oConexion);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // Actualiza la fecha de última notificación enviada a un socio.
        public void RegistrarAvisoEnviado(int idSocio)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                string query = "UPDATE Socios SET FechaUltimoAviso = GETDATE() WHERE IdSocio = @IdSocio";
                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.Parameters.AddWithValue("@IdSocio", idSocio);
                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}