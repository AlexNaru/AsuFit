using System;

namespace AsuFit.Entidades
{
    // Representa a un cliente registrado en el sistema con sus datos personales y de suscripción.
    public class Socio
    {
        #region PROPIEDADES
        public int IdSocio { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NombreContactoEmergencia { get; set; }
        public string TelefonoEmergencia { get; set; }
        public DateTime FechaRegistro { get; set; }
        public byte[] Foto { get; set; }
        public int IdPlan { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Estado { get; set; }
        public string NombrePlan { get; set; }
        public string Ruc { get; set; }
        #endregion
    }
}