using System;

namespace AsuFit.Entidades
{
    // Representa los distintos tipos de suscripciones o membresías disponibles en el gimnasio.
    public class Plan
    {
        #region PROPIEDADES
        public int IdPlan { get; set; }
        public string NombrePlan { get; set; }
        public decimal Precio { get; set; }
        public int DuracionDias { get; set; }
        #endregion
    }
}