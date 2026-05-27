using AsuFit.Datos;
using AsuFit.Entidades;
using System;
using System.Data;

namespace AsuFit.Negocio
{
    // Encapsula las reglas de negocio requeridas para el procesamiento de transacciones de venta.
    public class VentaNegocio
    {
        private VentaDatos datos = new VentaDatos();

        #region 1. PROCESAMIENTO DE VENTAS (ESCRITURA)
        // Aplica las validaciones de negocio antes de registrar la venta y sus detalles.
        public int RegistrarVentaCompleta(Venta objVenta, out string mensajeError)
        {
            mensajeError = string.Empty;

            if (objVenta.Detalles == null || objVenta.Detalles.Count == 0)
            {
                mensajeError = "La venta no tiene productos ni mensualidades asignadas.";
                return 0;
            }

            return datos.RegistrarVentaCompleta(objVenta, out mensajeError);
        }
        #endregion

        #region 2. CONSULTAS E HISTORIALES (LECTURA)
        // Intermediario para recuperar el historial de ventas
        public DataTable ObtenerHistorialVentas(DateTime desde, DateTime hasta, string filtro, string tipoFiltro)
        {
            return datos.ObtenerHistorialVentas(desde, hasta, filtro, tipoFiltro);
        }
        #endregion
    }
}