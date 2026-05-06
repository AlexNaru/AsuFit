using AsuFit.Datos;
using System.Data;

namespace AsuFit.Negocio
{
    public class DashboardNegocio
    {
        private DashboardDatos objDatos = new DashboardDatos();

        public void ObtenerMeticasPrincipales(out int activos, out decimal ingresos, out decimal egresos, out int vencimientos)
        {
            objDatos.ObtenerMeticasPrincipales(out activos, out ingresos, out egresos, out vencimientos);
        }

        public DataTable ListarVencimientosProximos()
        {
            return objDatos.ListarVencimientosProximos();
        }
    }
}