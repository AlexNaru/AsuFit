using AsuFit.Datos;
using AsuFit.Entidades;

namespace AsuFit.Negocio
{
    public class VentaNegocio
    {
        private VentaDatos datos = new VentaDatos();

        public int RegistrarVentaCompleta(Venta objVenta, out string mensajeError)
        {
            mensajeError = string.Empty;

            // Regla de Negocio: No se puede guardar una venta vacía
            if (objVenta.Detalles == null || objVenta.Detalles.Count == 0)
            {
                mensajeError = "La venta no tiene productos ni mensualidades asignadas.";
                return 0;
            }

            return datos.RegistrarVentaCompleta(objVenta, out mensajeError);
        }
    }
}