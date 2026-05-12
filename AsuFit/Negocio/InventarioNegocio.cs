using AsuFit.Datos;
using System.Data;

namespace AsuFit.Negocio
{
    public class InventarioNegocio
    {
        private InventarioDatos objDatos = new InventarioDatos();

        public DataTable ListarCategorias() { return objDatos.ListarCategorias(); }
        public DataTable ListarProductos() { return objDatos.ListarProductos(); }
        public bool RegistrarVenta(decimal total, DataTable detalleVenta) { return objDatos.RegistrarVenta(total, detalleVenta); }

        public bool GuardarProducto(int id, string codigo, string nombre, string categoria, decimal precio, int stock, int idProveedor, int porcentajeIva)
        {
            return objDatos.GuardarProducto(id, codigo, nombre, categoria, precio, stock, idProveedor, porcentajeIva);
        }

        public bool CambiarEstado(int id, string nuevoEstado) { return objDatos.CambiarEstado(id, nuevoEstado); }

        // --- NUEVO: AHORA RECIBE EL PRECIO DE COMPRA DE TU PANTALLA Y LO TRANSPORTA ---
        public bool SumarStock(int idProducto, int cantidadAumentar, decimal nuevoPrecioCompra)
        {
            return objDatos.SumarStock(idProducto, cantidadAumentar, nuevoPrecioCompra);
        }

        public DataTable ListarProductosBasico() { return objDatos.ListarProductos(); }

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
    }
}