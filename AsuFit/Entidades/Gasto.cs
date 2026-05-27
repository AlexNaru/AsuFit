using System;

namespace AsuFit.Entidades
{
    // Modelo de datos que representa un egreso operativo o administrativo del sistema.
    public class Gasto
    {
        #region PROPIEDADES
        public int IdGasto { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaGasto { get; set; }
        public string UsuarioRegistra { get; set; }
        #endregion
    }
}