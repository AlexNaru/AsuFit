using AsuFit.Datos;
using AsuFit.Entidades;
using System.Data;

namespace AsuFit.Negocio
{
    public class ProveedorNegocio
    {
        private ProveedorDatos datos = new ProveedorDatos();

        public DataTable ListarProveedores()
        {
            return datos.Listar();
        }

        public bool GuardarProveedor(Proveedor obj)
        {
            return datos.Guardar(obj);
        }

        public bool CambiarEstado(int idProveedor, string nuevoEstado)
        {
            return datos.CambiarEstado(idProveedor, nuevoEstado);
        }
    }
}