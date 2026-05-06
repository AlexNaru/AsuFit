using System.Data;
using AsuFit.Datos;

namespace AsuFit.Negocio
{
    public class ProveedorNegocio
    {
        private ProveedorDatos datos = new ProveedorDatos();

        public DataTable ListarProveedores()
        {
            return datos.Listar();
        }

        public bool InsertarProveedor(string nombre, string ruc, string categoria, string contacto, string telefono, string correo, string direccion, string ciudad, string estado)
        {
            return datos.Insertar(nombre, ruc, categoria, contacto, telefono, correo, direccion, ciudad, estado);
        }

        public bool EditarProveedor(int idProveedor, string nombre, string ruc, string categoria, string contacto, string telefono, string correo, string direccion, string ciudad, string estado)
        {
            return datos.Editar(idProveedor, nombre, ruc, categoria, contacto, telefono, correo, direccion, ciudad, estado);
        }

        public bool CambiarEstado(int idProveedor, string nuevoEstado)
        {
            return datos.CambiarEstado(idProveedor, nuevoEstado);
        }
    }
}