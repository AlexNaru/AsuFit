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

        // Procesa y delega el registro o actualización de un proveedor.
        public bool GuardarProveedor(Proveedor obj)
        {
            return datos.Guardar(obj);
        }

        // Modifica el estado lógico de un proveedor en la base de datos.
        public bool CambiarEstado(int idProveedor, string nuevoEstado)
        {
            return datos.CambiarEstado(idProveedor, nuevoEstado);
        }
        #endregion
    }
}