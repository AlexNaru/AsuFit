using AsuFit.Datos;
using AsuFit.Entidades;
using System.Collections.Generic;

namespace AsuFit.Negocio
{
    // Centraliza las reglas de negocio para la gestión de suscripciones y membresías.
    public class PlanNegocio
    {
        private PlanDatos objDatos = new PlanDatos();

        #region CONSULTAS
        // Obtiene el listado de planes filtrados por su estado actual.
        public List<Plan> ListarPlanes(string estado)
        {
            return objDatos.ListarPlanes(estado);
        }

        // Busca y recupera la información de un plan utilizando su nombre exacto.
        public Plan ObtenerPlanPorNombre(string nombrePlan)
        {
            PlanDatos datos = new PlanDatos();
            return datos.ObtenerPlanPorNombre(nombrePlan);
        }
        #endregion

        #region OPERACIONES Y VALIDACIONES
        // Valida la integridad de los datos antes de registrar un nuevo plan.
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

        // Verifica las reglas de negocio antes de actualizar la información de un plan.
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

        // Gestiona la activación o desactivación de un plan en el sistema.
        public bool CambiarEstadoPlan(int idPlan, string nuevoEstado, out string mensaje)
        {
            return objDatos.CambiarEstadoPlan(idPlan, nuevoEstado, out mensaje);
        }
        #endregion
    }
}