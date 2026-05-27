using AsuFit.Datos;
using System;
using System.Data;

namespace AsuFit.Negocio
{
    // Administra las reglas de negocio para la lectura de los registros de auditoría.
    public class AuditoriaNegocio
    {
        private AuditoriaDatos datos = new AuditoriaDatos();

        #region CONSULTAS DE AUDITORÍA
        // Obtiene el historial de eventos del sistema filtrado por fechas.
        public DataTable ListarAuditoria(DateTime desde, DateTime hasta)
        {
            return datos.ListarAuditoria(desde, hasta);
        }
        #endregion
    }
}