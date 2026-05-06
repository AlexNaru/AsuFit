using AsuFit.Entidades; // Usamos tu carpeta Entidades
using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class UsuarioDatos
    {

        // Para validar el login, buscamos un usuario activo que coincida con username y password.
        public Usuario ValidarLogin(string username, string password)
        {
            Usuario objetoUsuario = null;
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    string query = "SELECT IdUsuario, NombreCompleto, Rol FROM Usuarios " +
                                   "WHERE Username = @user AND Password = @pass AND Estado = 'Activo'";

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
                                Rol = dr["Rol"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Datos: " + ex.Message);
            }
            return objetoUsuario;
        }

        // Para recuperar la contraseña, primero obtenemos la pregunta de seguridad del usuario (si existe y está activo).
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

        // Para actualizar la contraseña, verificamos que el username y la respuesta de seguridad coincidan.
        public bool ActualizarPassword(string username, string respuesta, string nuevaPass)
        {
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    // Solo actualiza si coinciden Username Y RespuestaSeguridad
                    string query = "UPDATE Usuarios SET Password = @newPass " +
                                   "WHERE Username = @user AND RespuestaSeguridad = @resp";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@newPass", nuevaPass);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@resp", respuesta);
                    oConexion.Open();
                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0; // Si es > 0, la respuesta fue correcta y se cambió
                }
            }
            catch (Exception) { return false; }
        }

        // Método para insertar un nuevo usuario
        public bool RegistrarUsuario(Usuario obj)
        {
            bool respuesta = false;
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
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado); // "Activo" por defecto
                    cmd.Parameters.AddWithValue("@Email", obj.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Pregunta", obj.PreguntaSeguridad ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Respuesta", obj.RespuestaSeguridad ?? (object)DBNull.Value);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception)
                {
                    throw; // <-- CAMBIO APLICADO AQUÍ
                }
            }
            return respuesta;
        }

        // Método para listar los usuarios en la grilla
        public DataTable ListarUsuarios(string estado)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    string query = @"SELECT IdUsuario, NombreCompleto, Username, Rol, Email, Estado, FechaRegistro 
                             FROM Usuarios WHERE Estado = @estado";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@estado", estado);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception)
            {
                throw; // <-- CAMBIO APLICADO AQUÍ
            }
            return dt;
        }

        // Método para actualizar los datos de un usuario existente
        public bool EditarUsuario(Usuario obj)
        {
            bool respuesta = false;
            using (System.Data.SqlClient.SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"UPDATE Usuarios SET 
                             NombreCompleto = @Nombre, 
                             Username = @User, 
                             Rol = @Rol, 
                             Estado = @Estado, 
                             Email = @Email, 
                             PreguntaSeguridad = @Pregunta, 
                             RespuestaSeguridad = @Respuesta 
                             WHERE IdUsuario = @Id";

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Id", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("@User", obj.Username);
                    cmd.Parameters.AddWithValue("@Rol", obj.Rol);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@Email", obj.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Pregunta", obj.PreguntaSeguridad ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Respuesta", obj.RespuestaSeguridad ?? (object)DBNull.Value);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception)
                {
                    throw; // <-- CAMBIO APLICADO AQUÍ
                }
            }
            return respuesta;
        }

        // Método para dar de baja o reactivar a un usuario
        public bool CambiarEstado(int idUsuario, string nuevoEstado)
        {
            bool respuesta = false;
            using (System.Data.SqlClient.SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "UPDATE Usuarios SET Estado = @estado WHERE IdUsuario = @id";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@id", idUsuario);

                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception)
                {
                    throw; // <-- CAMBIO APLICADO AQUÍ
                }
            }
            return respuesta;
        }

        // Método para resetear la clave a una por defecto
        public bool ResetearClave(int idUsuario, string clavePorDefecto)
        {
            bool respuesta = false;
            using (System.Data.SqlClient.SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "UPDATE Usuarios SET Password = @clave WHERE IdUsuario = @id";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@clave", clavePorDefecto);
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    oConexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception)
                {
                    throw; // <-- CAMBIO APLICADO AQUÍ
                }
            }
            return respuesta;
        }

        // --- NUEVA VALIDACIÓN DE DUPLICADOS ---
        public bool ExisteUsername(string username, int idUsuarioActual)
        {
            bool existe = false;
            using (System.Data.SqlClient.SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Username = @Username AND IdUsuario != @IdUsuario";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuarioActual);

                    oConexion.Open();
                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                    if (cantidad > 0)
                    {
                        existe = true;
                    }
                }
                catch (Exception)
                {
                    throw; // <-- CAMBIO APLICADO AQUÍ
                }
            }
            return existe;
        }
    }
}