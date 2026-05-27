using AsuFit.Datos;
using AsuFit.Entidades;
using System;
using System.Data;

namespace AsuFit.Negocio
{
    // Encapsula las reglas de negocio y validaciones para la gestión de turnos de caja.
    public class ArqueoNegocio
    {
        private ArqueoDatos datos = new ArqueoDatos();

        #region GESTIÓN DE TURNOS
        // Gestiona la apertura de un nuevo turno de caja.
        public bool AbrirCaja(TurnoCaja obj)
        {
            return datos.AbrirCaja(obj);
        }

        // Gestiona el cierre del turno de caja actual.
        public bool CerrarCaja(TurnoCaja obj)
        {
            return datos.CerrarCaja(obj);
        }
        #endregion

        #region CONSULTAS Y REPORTES
        // Verifica y recupera el turno de caja abierto para un usuario.
        public DataTable ObtenerTurnoAbierto(int idUsuario)
        {
            return datos.ObtenerTurnoAbierto(idUsuario);
        }

        // Calcula los totales en tiempo real de ingresos y egresos.
        public DataTable ObtenerTotalesEnVivo(int idUsuario, DateTime fechaApertura)
        {
            return datos.ObtenerTotalesEnVivo(idUsuario, fechaApertura);
        }

        // Genera el historial de arqueos cerrados en un rango de fechas.
        public DataTable ListarHistorialArqueos(DateTime desde, DateTime hasta)
        {
            return datos.ListarHistorialArqueos(desde, hasta);
        }
        #endregion
    }
}