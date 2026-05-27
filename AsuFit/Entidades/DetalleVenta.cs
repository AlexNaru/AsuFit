using System;

namespace AsuFit.Entidades
{
    // Representa la estructura de los productos o servicios incluidos en una transacción de venta.
    public class DetalleVenta
    {
        #region PROPIEDADES
        public int IdProducto { get; set; }
        public string CodigoBarras { get; set; }
        public string Concepto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
        #endregion
    }
}