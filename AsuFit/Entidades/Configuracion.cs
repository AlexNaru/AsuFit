using System;

namespace AsuFit.Entidades
{
    public class Configuracion
    {
        public int IdConfiguracion { get; set; }
        public string NombreGimnasio { get; set; }
        public string Ruc { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public byte[] Logo { get; set; }
        public string CorreoEmisor { get; set; }
        public string ContrasenaCorreo { get; set; }
        public int DiasAviso1 { get; set; }
        public int DiasAviso2 { get; set; }
        public string RutaBackup { get; set; }
    }
}