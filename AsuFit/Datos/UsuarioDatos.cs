using AsuFit.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    // Gestiona las operaciones de persistencia para la entidad Usuarios.
    public class UsuarioDatos
    {
        #region AUTENTICACIÓN Y SEGURIDAD
        // Valida las credenciales de acceso de un usuario y descarga TODOS sus datos.
        public Usuario ValidarLogin(string username, string password)
        {
            Usuario objetoUsuario = null;
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    // 1. SOLUCIÓN: Agregamos Email, Estado, Pregunta y Respuesta a la consulta SQL
                    string query = "SELECT IdUsuario, NombreCompleto, Rol, Email, Estado, PreguntaSeguridad, RespuestaSeguridad FROM Usuarios WHERE Username = @user AND Password = @pass AND Estado = 'Activo'";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            objetoUsuario = new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Username = username,
                                Rol = dr["Rol"].ToString(),

                                // 2. SOLUCIÓN: Rescatamos los valores de la base de datos (y verificamos que no sean nulos)
                                Email = dr["Email"] != DBNull.Value ? dr["Email"].ToString() : "",
                                Estado = dr["Estado"].ToString(),
                                PreguntaSeguridad = dr["PreguntaSeguridad"] != DBNull.Value ? dr["PreguntaSeguridad"].ToString() : "",
                                RespuestaSeguridad = dr["RespuestaSeguridad"] != DBNull.Value ? dr["RespuestaSeguridad"].ToString() : ""
                            };
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Error en Datos: " + ex.Message); }
            return objetoUsuario;
        }

        // Recupera la pregunta de seguridad configurada para el usuario.
        public string ObtenerPregunta(string username)
        {
            string pregunta = "";
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    string query = "SELECT PreguntaSeguridad FROM Usuarios WHERE Username = @user AND Estado = 'Activo'";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@user", username);
                    oConexion.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null) pregunta = result.ToString();
                }
            }
            catch (Exception) { pregunta = ""; }
            return pregunta;
        }

        // Actualiza la contraseña si los parámetros de seguridad coinciden.
        public bool ActualizarPassword(string username, string respuesta, string nuevaPass)
        {
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    string query = "UPDATE Usuarios SET Password = @newPass WHERE Username = @user AND RespuestaSeguridad = @resp";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@newPass", nuevaPass);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@resp", respuesta);
                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception) { return false; }
        }

        // Restablece la contraseña a una clave temporal.
        public bool ResetearClave(int idUsuario, string clavePorDefecto)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "UPDATE Usuarios SET Password = @clave WHERE IdUsuario = @id";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@clave", clavePorDefecto);
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
        }
        #endregion

        #region GESTIÓN DE USUARIOS
        // Registra un nuevo usuario en el sistema.
        public bool RegistrarUsuario(Usuario obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"INSERT INTO Usuarios (NombreCompleto, Username, Password, Rol, Estado, Email, PreguntaSeguridad, RespuestaSeguridad) 
                             VALUES (@Nombre, @User, @Pass, @Rol, @Estado, @Email, @Pregunta, @Respuesta)";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Nombre", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("@User", obj.Username);
                    cmd.Parameters.AddWithValue("@Pass", obj.Password);
                    cmd.Parameters.AddWithValue("@Rol", obj.Rol);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@Email", obj.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Pregunta", obj.PreguntaSeguridad ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Respuesta", obj.RespuestaSeguridad ?? (object)DBNull.Value);

                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
        }

        // Lista los usuarios filtrados por estado.
        public DataTable ListarUsuarios(string estado)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    string query = "SELECT IdUsuario, NombreCompleto, Username, Rol, Email, Estado, FechaRegistro FROM Usuarios WHERE Estado = @estado";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@estado", estado);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception) { throw; }
            return dt;
        }

        // Modifica los datos de un usuario.
        public bool EditarUsuario(Usuario obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"UPDATE Usuarios SET
                                     NombreCompleto = @Nombre,
                                     Username = @User,
                                     Password = @Pass,
                                     Rol = @Rol,
                                     Estado = @Estado,
                                     Email = @Email,
                                     PreguntaSeguridad = @Pregunta,
                                     RespuestaSeguridad = @Respuesta
                                     WHERE IdUsuario = @Id";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Id", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("@User", obj.Username);
                    cmd.Parameters.AddWithValue("@Pass", obj.Password);
                    cmd.Parameters.AddWithValue("@Rol", obj.Rol);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@Email", obj.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Pregunta", obj.PreguntaSeguridad ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Respuesta", obj.RespuestaSeguridad ?? (object)DBNull.Value);

                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
        }

        // Modifica el estado de un usuario (Alta/Baja).
        public bool CambiarEstado(int idUsuario, string nuevoEstado)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "UPDATE Usuarios SET Estado = @estado WHERE IdUsuario = @id";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@id", idUsuario);

                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
        }

        // Verifica si un nombre de usuario ya está en uso.
        public bool ExisteUsername(string username, int idUsuarioActual)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Username = @Username AND IdUsuario != @IdUsuario";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuarioActual);

                    oConexion.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
                catch (Exception) { throw; }
            }
        }
        #endregion
    }
}