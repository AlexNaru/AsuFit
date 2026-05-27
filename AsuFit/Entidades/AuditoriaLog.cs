using System;

namespace AsuFit.Entidades
{
    // Modelo de datos para el seguimiento y trazabilidad de las acciones críticas en el sistema.
    public class AuditoriaLog
    {
        #region PROPIEDADES
        public int IdLog { get; set; }
        public DateTime FechaHora { get; set; }
        public string Usuario { get; set; }
        public string Modulo { get; set; }
        public string Accion { get; set; }
        public string Detalle { get; set; }
        #endregion
    }
}