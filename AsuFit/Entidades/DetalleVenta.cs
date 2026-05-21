using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsuFit.Entidades
{
    public class DetalleVenta
    {
        public int IdProducto { get; set; }
        public string CodigoBarras { get; set; }
        public string Concepto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}