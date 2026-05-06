using AsuFit.Datos;
using System.Data;

namespace AsuFit.Negocio
{
    public class InventarioNegocio
    {
        // Instanciamos la capa de datos que lee de SQL
        private InventarioDatos objDatos = new InventarioDatos();

        // Método puente para las categorías
        public DataTable ListarCategorias()
        {
            return objDatos.ListarCategorias();
        }

        // Método puente para los productos
        public DataTable ListarProductos()
        {
            return objDatos.ListarProductos();
        }

        public bool RegistrarVenta(decimal total, DataTable detalleVenta)
        {
            return objDatos.RegistrarVenta(total, detalleVenta);
        }

        // --- ESTOS SON LOS DOS MÉTODOS QUE FALTABAN ---
        public bool GuardarProducto(int id, string codigo, string nombre, string categoria, decimal precio, int stock, int idProveedor)
        {
            return objDatos.GuardarProducto(id, codigo, nombre, categoria, precio, stock, idProveedor);
        }

        public bool CambiarEstado(int id, string nuevoEstado)
        {
            return objDatos.CambiarEstado(id, nuevoEstado);
        }

        // Método puente para sumar stock desde la pantalla de compras
        public bool SumarStock(int idProducto, int cantidadAumentar)
        {
            return objDatos.SumarStock(idProducto, cantidadAumentar);
        }

        // --- NUEVOS MÉTODOS PARA EL DASHBOARD ---
        public DataTable ListarProductosBasico()
        {
            return objDatos.ListarProductos();
        }

        public DataTable ListarProductosStockBajo()
        {
            DataTable dt = objDatos.ListarProductos();
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                // Filtramos por stock bajo y que estén activos
                dv.RowFilter = "StockActual <= StockMinimo AND Estado = 'Activo'";
                return dv.ToTable();
            }
            return null;
        }
    }
}