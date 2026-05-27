using System;

namespace AsuFit.Entidades
{
    // Contiene la información de apertura, transacciones y cierre de un turno operativo de caja.
    public class TurnoCaja
    {
        #region PROPIEDADES
        public int IdTurno { get; set; }
        public int IdUsuario { get; set; }
        public string CajeroNombre { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }
        public decimal FondoInicial { get; set; }
        public decimal IngresosEfectivo { get; set; }
        public decimal IngresosTransferencia { get; set; }
        public decimal GastosEfectivo { get; set; }
        public decimal MontoEsperado { get; set; }
        public decimal MontoContado { get; set; }
        public decimal Diferencia { get; set; }
        public string Estado { get; set; }
        #endregion
    }
}