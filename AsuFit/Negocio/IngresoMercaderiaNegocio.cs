using System;
using AsuFit.Datos;

namespace AsuFit.Negocio
{
    public class IngresoMercaderiaNegocio
    {
        // ACÁ ESTABA EL ERROR: Tiene que instanciar a DATOS, no a NEGOCIO.
        private IngresoMercaderiaDatos datos = new IngresoMercaderiaDatos();

        public bool RegistrarIngreso(int idProveedor, int idProducto, int cantidad, decimal costoTotal, DateTime fechaIngreso, string observaciones)
        {
            if (cantidad <= 0)
            {
                throw new Exception("La cantidad a ingresar debe ser mayor a cero.");
            }

            if (costoTotal < 0)
            {
                throw new Exception("El costo total no puede ser negativo.");
            }

            return datos.RegistrarIngreso(idProveedor, idProducto, cantidad, costoTotal, fechaIngreso, observaciones);
        }
    }
}