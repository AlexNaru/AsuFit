using AsuFit.Datos;
using System.Data;

namespace AsuFit.Negocio
{
    // Orquesta la recopilación de indicadores y métricas para el panel de control.
    public class DashboardNegocio
    {
        private DashboardDatos objDatos = new DashboardDatos();

        #region MÉTRICAS Y ALERTAS
        // Procesa la obtención de los KPIs principales del sistema.
        public void ObtenerMeticasPrincipales(out int activos, out decimal ingresos, out decimal egresos, out int vencimientos)
        {
            objDatos.ObtenerMeticasPrincipales(out activos, out ingresos, out egresos, out vencimientos);
        }

        // Recupera el listado de socios con suscripciones próximas a vencer.
        public DataTable ListarVencimientosProximos()
        {
            return objDatos.ListarVencimientosProximos();
        }

        // Llama a la capa de datos para obtener los números de las notificaciones
        public void ObtenerContadoresNotificaciones(out int porVencer, out int vencidos, out int stockBajo, out int sinStock)
        {
            objDatos.ObtenerContadoresNotificaciones(out porVencer, out vencidos, out stockBajo, out sinStock);
        }
        #endregion
    }
}