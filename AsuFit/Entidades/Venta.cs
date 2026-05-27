using System;
using System.Collections.Generic;

namespace AsuFit.Entidades
{
    // Estructura de cabecera para una transacción de facturación o cobro.
    public class Venta
    {
        #region PROPIEDADES
        public int IdVenta { get; set; }
        public int? IdSocio { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string MetodoPago { get; set; }
        public string TipoComprobante { get; set; }
        public int? IdUsuario { get; set; }

        // Colección de productos y/o mensualidades incluidas en la transacción.
        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
        #endregion
    }
}