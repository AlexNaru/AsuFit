using AsuFit.Datos;
using System;
using System.Data;

namespace AsuFit.Negocio
{
    // Coordina la generación de indicadores analíticos y cruce de datos para reportes.
    public class ReportesNegocio
    {
        private ReportesDatos datos = new ReportesDatos();

        #region GENERACIÓN DE REPORTES
        // Procesa la solicitud de ingresos y facturación dentro de un rango de fechas.
        public DataTable ListarIngresosPorFechas(DateTime desde, DateTime hasta)
        {
            return datos.ObtenerIngresosPorFechas(desde, hasta);
        }

        // Consolida la información para obtener los productos de mayor rotación.
        public DataTable ListarTopProductos(DateTime desde, DateTime hasta)
        {
            return datos.ObtenerTopProductos(desde, hasta);
        }
        #endregion
    }
}