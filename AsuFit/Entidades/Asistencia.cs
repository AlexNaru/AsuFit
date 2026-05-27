using System;

namespace AsuFit.Entidades
{
    // Entidad que almacena los registros de entrada y salida de los socios al establecimiento.
    public class Asistencia
    {
        #region PROPIEDADES
        public int IdAsistencia { get; set; }
        public int IdSocio { get; set; }
        public DateTime FechaHora { get; set; }
        #endregion
    }
}