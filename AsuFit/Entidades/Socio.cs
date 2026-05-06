using System;

namespace AsuFit.Entidades
{
    public class Socio
    {
        public int IdSocio { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NombreContactoEmergencia { get; set; }
        public string TelefonoEmergencia { get; set; }
        // -------------------------------

        public DateTime FechaRegistro { get; set; }

        // La foto del socio se transporta como un arreglo de bytes
        public byte[] Foto { get; set; }

        public int IdPlan { get; set; }

        // Nullable por si recién se registra y todavía no abonó
        public DateTime? FechaVencimiento { get; set; }

        public string Estado { get; set; }

        public string NombrePlan { get; set; }

        public string Ruc { get; set; }
    }
}