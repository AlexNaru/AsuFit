using AsuFit.Datos;
using AsuFit.Entidades;
using System.Data;

namespace AsuFit.Negocio
{
    // Centraliza las reglas de negocio, seguridad y validación para los usuarios del sistema.
    public class UsuarioNegocio
    {
        private UsuarioDatos objUsuarioDatos = new UsuarioDatos();

        #region AUTENTICACIÓN Y SEGURIDAD
        // Valida las credenciales ingresadas para permitir el acceso al sistema.
        public Usuario Loguear(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            return objUsuarioDatos.ValidarLogin(username, password);
        }

        // Obtiene la pregunta de seguridad configurada para la recuperación de contraseña.
        public string BuscarPregunta(string username)
        {
            if (string.IsNullOrEmpty(username)) return "";
            return objUsuarioDatos.ObtenerPregunta(username);
        }

        // Procesa la actualización de la contraseña tras validar la respuesta de seguridad.
        public bool CambiarPassword(string username, string respuesta, string nuevaPass)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(respuesta) || string.IsNullOrEmpty(nuevaPass))
                return false;
            return objUsuarioDatos.ActualizarPassword(username, respuesta, nuevaPass);
        }

        // Restablece la contraseña del usuario a un valor por defecto.
        public bool ResetearClave(int idUsuario)
        {
            return objUsuarioDatos.ResetearClave(idUsuario, "12345");
        }
        #endregion

        #region GESTIÓN DE USUARIOS
        // Lista los usuarios filtrados por su estado actual.
        public DataTable ListarUsuarios(string estado)
        {
            return objUsuarioDatos.ListarUsuarios(estado);
        }

        // Valida los datos y procesa el registro de un nuevo usuario.
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

        // Valida y aplica los cambios sobre la información de un usuario existente.
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

        // Modifica el estado lógico (Activo/Inactivo) de un usuario en el sistema.
        public bool CambiarEstado(int idUsuario, string nuevoEstado)
        {
            return objUsuarioDatos.CambiarEstado(idUsuario, nuevoEstado);
        }

        // Verifica si el nombre de usuario ya está asignado a otro registro.
        public bool ExisteUsername(string username, int idUsuarioActual)
        {
            return objUsuarioDatos.ExisteUsername(username, idUsuarioActual);
        }
        #endregion
    }
}