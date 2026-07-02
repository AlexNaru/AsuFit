using AsuFit.Datos;
using AsuFit.Entidades;
using System.Data;

namespace AsuFit.Negocio
{
    // Administra la lógica de negocio aplicable al catálogo de proveedores.
    public class ProveedorNegocio
    {
        private ProveedorDatos datos = new ProveedorDatos();

        #region OPERACIONES Y CONSULTAS
        // Obtiene la lista completa de proveedores registrados.
        public DataTable ListarProveedores()
        {
            return datos.Listar();
        }

        // Procesa y delega el registro o actualización de un proveedor, emitiendo el diagnóstico de la operación mediante referencia.
        public bool GuardarProveedor(Proveedor obj, out string mensaje)
        {
            mensaje = string.Empty;
            bool exito = datos.Guardar(obj);

            if (!exito)
            {
                mensaje = "Ocurrió un error al intentar guardar el proveedor en la base de datos.";
            }

            return exito;
        }

        // Modifica el estado lógico de un proveedor en la base de datos.
        public bool CambiarEstado(int idProveedor, string nuevoEstado)
        {
            return datos.CambiarEstado(idProveedor, nuevoEstado);
        }
        #endregion
    }
}