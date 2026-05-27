using System;

namespace AsuFit.Entidades
{
    // Estructura de datos para el registro individual de pagos realizados por los socios.
    public class Pago
    {
        #region PROPIEDADES
        public int IdPago { get; set; }
        public int IdSocio { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }

        // Indica la vía de transacción (Ej: Efectivo, Transferencia, Tarjeta).
        public string MetodoPago { get; set; }

        // Describe el motivo del pago (Ej: Renovación de Plan Mensual).
        public string Concepto { get; set; }
        #endregion
    }
}