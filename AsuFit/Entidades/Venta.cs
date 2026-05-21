using System;
using System.Collections.Generic;

namespace AsuFit.Entidades
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int? IdSocio { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string MetodoPago { get; set; }
        public string TipoComprobante { get; set; }
        public int? IdUsuario { get; set; }

        // ¡LA MAGIA DE OBJETOS! Una venta contiene una lista de sus propios detalles
        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}