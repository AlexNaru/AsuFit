using AsuFit.Datos;
using System;
using System.Data;

namespace AsuFit.Negocio
{
    public class ArqueoNegocio
    {
        private ArqueoDatos datos = new ArqueoDatos();

        public bool AbrirCaja(int idUsuario, string cajeroNombre, decimal fondoInicial)
        {
            return datos.AbrirCaja(idUsuario, cajeroNombre, fondoInicial);
        }

        public DataTable ObtenerTurnoAbierto(int idUsuario)
        {
            return datos.ObtenerTurnoAbierto(idUsuario);
        }

        public DataTable ObtenerTotalesEnVivo(int idUsuario, DateTime fechaApertura)
        {
            return datos.ObtenerTotalesEnVivo(idUsuario, fechaApertura);
        }

        // El puente que le faltaba a tu formulario
        public DataTable ListarHistorialArqueos(DateTime desde, DateTime hasta)
        {
            return datos.ListarHistorialArqueos(desde, hasta);
        }

        public bool CerrarCaja(int idTurno, decimal ingresosEfectivo, decimal ingresosTransferencia, decimal gastosEfectivo, decimal montoEsperado, decimal montoContado, decimal diferencia)
        {
            return datos.CerrarCaja(idTurno, ingresosEfectivo, ingresosTransferencia, gastosEfectivo, montoEsperado, montoContado, diferencia);
        }
    }
}