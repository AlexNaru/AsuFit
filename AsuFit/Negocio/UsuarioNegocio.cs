using AsuFit.Datos;
using AsuFit.Entidades;
using System.Data;

namespace AsuFit.Negocio
{
    public class UsuarioNegocio
    {
        private UsuarioDatos objUsuarioDatos = new UsuarioDatos();

        public Usuario Loguear(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            return objUsuarioDatos.ValidarLogin(username, password);
        }

        public string BuscarPregunta(string username)
        {
            if (string.IsNullOrEmpty(username)) return "";
            return objUsuarioDatos.ObtenerPregunta(username);
        }

        public bool CambiarPassword(string username, string respuesta, string nuevaPass)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(respuesta) || string.IsNullOrEmpty(nuevaPass))
                return false;
            return objUsuarioDatos.ActualizarPassword(username, respuesta, nuevaPass);
        }

        public bool RegistrarUsuario(Usuario objUsuario, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(objUsuario.NombreCompleto) ||
                string.IsNullOrWhiteSpace(objUsuario.Username) ||
                string.IsNullOrWhiteSpace(objUsuario.Password))
            {
                mensaje = "El nombre completo, usuario y contraseña son obligatorios.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(objUsuario.Rol))
            {
                mensaje = "Debe seleccionar un nivel de acceso (Rol).";
                return false;
            }

            bool respuestaBD = objUsuarioDatos.RegistrarUsuario(objUsuario);
            if (!respuestaBD)
            {
                mensaje = "Error al guardar el usuario en la base de datos.";
            }

            return respuestaBD;
        }

        public DataTable ListarUsuarios(string estado)
        {
            return objUsuarioDatos.ListarUsuarios(estado);
        }

        public bool EditarUsuario(Usuario objUsuario, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(objUsuario.NombreCompleto) ||
                string.IsNullOrWhiteSpace(objUsuario.Username))
            {
                mensaje = "El nombre completo y usuario son obligatorios.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(objUsuario.Rol))
            {
                mensaje = "Debe seleccionar un nivel de acceso (Rol).";
                return false;
            }

            bool respuestaBD = objUsuarioDatos.EditarUsuario(objUsuario);
            if (!respuestaBD)
            {
                mensaje = "Error al actualizar el usuario en la base de datos.";
            }

            return respuestaBD;
        }

        public bool CambiarEstado(int idUsuario, string nuevoEstado)
        {
            return objUsuarioDatos.CambiarEstado(idUsuario, nuevoEstado);
        }

        public bool ResetearClave(int idUsuario)
        {
            return objUsuarioDatos.ResetearClave(idUsuario, "12345");
        }

        // --- NUEVA VALIDACIÓN DE DUPLICADOS (EL PUENTE QUE FALTABA) ---
        public bool ExisteUsername(string username, int idUsuarioActual)
        {
            // Delegamos la tarea a la capa de Datos para que consulte en SQL
            return objUsuarioDatos.ExisteUsername(username, idUsuarioActual);
        }
    }
}