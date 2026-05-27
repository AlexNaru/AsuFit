using System;
using System.Collections.Generic;
using AsuFit.Datos;
using AsuFit.Entidades;

namespace AsuFit.Negocio
{
    // Controla las validaciones de negocio para la administración de gastos operativos.
    public class GastoNegocio
    {
        private GastoDatos objDatos = new GastoDatos();

        #region CONSULTAS
        // Recupera el listado histórico de gastos registrados.
        public List<Gasto> ListarGastos()
        {
            return objDatos.ListarGastos();
        }
        #endregion

        #region OPERACIONES Y VALIDACIONES
        // Verifica las reglas de negocio antes de registrar un nuevo gasto.
        public bool RegistrarGasto(Gasto obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                mensaje = "La descripción del gasto no puede estar vacía.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(obj.Categoria))
            {
                mensaje = "Debés seleccionar una categoría para el gasto.";
                return false;
            }
            if (obj.Monto <= 0)
            {
                mensaje = "El monto del gasto debe ser mayor a 0 Gs.";
                return false;
            }

            return objDatos.RegistrarGasto(obj, out mensaje);
        }
        #endregion
    }
}