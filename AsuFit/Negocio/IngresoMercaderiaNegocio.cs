using System;
using AsuFit.Datos;

namespace AsuFit.Negocio
{
    // Administra las reglas de negocio para el abastecimiento y compra de productos.
    public class IngresoMercaderiaNegocio
    {
        private IngresoMercaderiaDatos datos = new IngresoMercaderiaDatos();

        #region OPERACIONES Y VALIDACIONES
        // Valida los montos y cantidades antes de procesar el ingreso de mercadería.
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
        #endregion
    }
}