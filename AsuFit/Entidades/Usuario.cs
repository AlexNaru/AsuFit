namespace AsuFit.Entidades
{
    // Entidad para la gestión de credenciales, roles y acceso al sistema.
    public class Usuario
    {
        #region PROPIEDADES
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }
        public string Email { get; set; }
        public string PreguntaSeguridad { get; set; }
        public string RespuestaSeguridad { get; set; }
        #endregion
    }
}