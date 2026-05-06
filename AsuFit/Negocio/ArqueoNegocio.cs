using AsuFit.Datos;
using System;
using System.Data;

namespace AsuFit.Negocio
{
    public class ArqueoNegocio
    {
        private ArqueoDatos datos = new ArqueoDatos();

        public decimal ObtenerTotalDelDia(DateTime fecha)
        {
            return datos.ObtenerTotalDelDia(fecha);
        }

        public bool RegistrarCierre(decimal totalSistema, decimal efectivoCaja, decimal diferencia, string usuario)
        {
            // Usamos 'datos' en lugar de 'objArqueoDatos'
            return datos.RegistrarCierre(totalSistema, efectivoCaja, diferencia, usuario);
        }

        public DataTable ListarHistorialArqueos(DateTime desde, DateTime hasta)
        {
            return datos.ListarHistorialArqueos(desde, hasta);
        }
    }
}