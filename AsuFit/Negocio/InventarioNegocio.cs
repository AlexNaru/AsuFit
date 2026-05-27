using AsuFit.Datos;
using AsuFit.Entidades;
using System.Data;

namespace AsuFit.Negocio
{
    // Gestiona las reglas y validaciones para productos, categorías y control de stock.
    public class InventarioNegocio
    {
        private InventarioDatos objDatos = new InventarioDatos();

        #region CONSULTAS Y LISTADOS
        // Obtiene la lista de categorías activas en el sistema.
        public DataTable ListarCategorias()
        {
            return objDatos.ListarCategorias();
        }

        // Obtiene el listado completo de productos detallados.
        public DataTable ListarProductos()
        {
            return objDatos.ListarProductos();
        }

        // Obtiene la lista básica de productos para operaciones simples.
        public DataTable ListarProductosBasico()
        {
            return objDatos.ListarProductos();
        }

        // Filtra y retorna los productos que han alcanzado o superado su stock mínimo.
        public DataTable ListarProductosStockBajo()
        {
            DataTable dt = objDatos.ListarProductos();
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "StockActual <= StockMinimo AND Estado = 'Activo'";
                return dv.ToTable();
            }
            return null;
        }
        #endregion

        #region GESTIÓN DE PRODUCTOS Y STOCK
        // Delega la persistencia de los datos generales de un producto.
        public bool GuardarProducto(Producto obj)
        {
            return objDatos.GuardarProducto(obj);
        }

        // Modifica el estado lógico de un producto en el inventario.
        public bool CambiarEstado(int id, string nuevoEstado)
        {
            return objDatos.CambiarEstado(id, nuevoEstado);
        }

        // Gestiona el incremento de existencias y actualización de costos de compra.
        public bool SumarStock(int idProducto, int cantidadAumentar, decimal nuevoPrecioCompra)
        {
            return objDatos.SumarStock(idProducto, cantidadAumentar, nuevoPrecioCompra);
        }
        #endregion

        #region OPERACIONES DE VENTA
        // Valida y delega el registro transaccional de una venta de productos.
        public bool RegistrarVenta(decimal total, DataTable detalleVenta)
        {
            return objDatos.RegistrarVenta(total, detalleVenta);
        }
        #endregion
    }
}