using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsuFit.Entidades
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string CodigoBarras { get; set; }
        public string Nombre { get; set; }

        // Relación con Categorías
        public int IdCategoria { get; set; }
        public string Categoria { get; set; } // Nos sirve para mostrar el texto en la grilla

        // Precios y Stock
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }

        // Relación con Proveedores
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; } // Nos sirve para mostrar el texto en la grilla

        // Impuestos y Control
        public int PorcentajeIva { get; set; }
        public string Estado { get; set; }
    }
}