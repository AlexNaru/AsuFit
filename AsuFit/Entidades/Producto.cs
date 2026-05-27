namespace AsuFit.Entidades
{
    // Representa un artículo físico comercializado dentro del inventario del establecimiento.
    public class Producto
    {
        #region PROPIEDADES
        public int IdProducto { get; set; }
        public string CodigoBarras { get; set; }
        public string Nombre { get; set; }

        // Relación con Categorías
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }

        // Precios y Stock
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }

        // Relación con Proveedores
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }

        // Impuestos y Control
        public int PorcentajeIva { get; set; }
        public string Estado { get; set; }
        #endregion
    }
}