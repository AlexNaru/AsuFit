using System;

namespace AsuFit.Entidades
{
    public class Plan
    {
        public int IdPlan { get; set; }
        public string NombrePlan { get; set; }
        public decimal Precio { get; set; }
        public int DuracionDias { get; set; }
    }
}