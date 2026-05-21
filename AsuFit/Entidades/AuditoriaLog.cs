using System;

namespace AsuFit.Entidades
{
    public class AuditoriaLog
    {
        public int IdLog { get; set; }
        public DateTime FechaHora { get; set; }
        public string Usuario { get; set; }
        public string Modulo { get; set; }
        public string Accion { get; set; }
        public string Detalle { get; set; }
    }
}