using AsuFit.Datos;
using AsuFit.Entidades;
using System;
using System.Data;

namespace AsuFit.Negocio
{
    public class PagoNegocio
    {
        private PagoDatos objDatos = new PagoDatos();

        public bool RegistrarCobro(Pago objPago, int diasPlan, out string mensaje)
        {
            // Pequeña validación de seguridad
            if (objPago.Monto <= 0)
            {
                mensaje = "El monto del pago debe ser mayor a cero.";
                return false;
            }

            return objDatos.RegistrarCobro(objPago, diasPlan, out mensaje);
        }

        public DataTable ListarHistorialPagos(DateTime desde, DateTime hasta, string busqueda)
        {
            PagoDatos datos = new PagoDatos();
            return datos.ListarHistorialPagos(desde, hasta, busqueda);
        }
    }
}