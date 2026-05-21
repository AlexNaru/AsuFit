using System;

namespace AsuFit.Entidades
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }
        public int IdSocio { get; set; }
        public DateTime FechaHora { get; set; }
    }
}