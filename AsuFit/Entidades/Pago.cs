using System;

namespace AsuFit.Entidades
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdSocio { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; } // Efectivo, Transferencia, Tarjeta
        public string Concepto { get; set; }   // Ej: "Renovación Plan Mensual"
    }
}