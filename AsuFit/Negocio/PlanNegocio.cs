using AsuFit.Datos;
using AsuFit.Entidades;
using System.Collections.Generic;

namespace AsuFit.Negocio
{
    public class PlanNegocio
    {
        private PlanDatos objDatos = new PlanDatos();

        // --- CAMBIO APLICADO: Ahora recibe el string 'estado' ---
        public List<Plan> ListarPlanes(string estado)
        {
            return objDatos.ListarPlanes(estado);
        }

        public bool RegistrarPlan(Plan obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(obj.NombrePlan))
            {
                mensaje = "El nombre del plan no puede estar vacío.";
                return false;
            }
            if (obj.Precio <= 0 || obj.DuracionDias <= 0)
            {
                mensaje = "El precio y los días deben ser mayores a 0.";
                return false;
            }
            return objDatos.RegistrarPlan(obj, out mensaje);
        }

        public bool EditarPlan(Plan obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(obj.NombrePlan))
            {
                mensaje = "El nombre del plan no puede estar vacío.";
                return false;
            }
            if (obj.Precio <= 0 || obj.DuracionDias <= 0)
            {
                mensaje = "El precio y los días deben ser mayores a 0.";
                return false;
            }
            return objDatos.EditarPlan(obj, out mensaje);
        }

        public bool CambiarEstadoPlan(int idPlan, string nuevoEstado, out string mensaje)
        {
            return objDatos.CambiarEstadoPlan(idPlan, nuevoEstado, out mensaje);
        }

        public Plan ObtenerPlanPorNombre(string nombrePlan)
        {
            PlanDatos datos = new PlanDatos();
            return datos.ObtenerPlanPorNombre(nombrePlan);
        }
    }
}