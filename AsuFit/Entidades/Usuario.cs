using System;

namespace AsuFit.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }
        public string Email { get; set; }
        public string PreguntaSeguridad { get; set; }
        public string RespuestaSeguridad { get; set; }
    }
}