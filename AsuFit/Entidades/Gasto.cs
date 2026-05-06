using System;

namespace AsuFit.Entidades
{
    public class Gasto
    {
        public int IdGasto { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaGasto { get; set; }
        public string UsuarioRegistra { get; set; }
    }
}