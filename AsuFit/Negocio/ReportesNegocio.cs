using AsuFit.Datos;
using System;
using System.Data;

namespace AsuFit.Negocio
{
    public class ReportesNegocio
    {
        private ReportesDatos datos = new ReportesDatos();

        public DataTable ListarIngresosPorFechas(DateTime desde, DateTime hasta)
        {
            return datos.ObtenerIngresosPorFechas(desde, hasta);
        }

        public DataTable ListarTopProductos(DateTime desde, DateTime hasta)
        {
            return datos.ObtenerTopProductos(desde, hasta);
        }
    }
}