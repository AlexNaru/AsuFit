using System.Data;

namespace AsuFit.Entidades
{
    // Gestor estático de estado para la manipulación temporal de productos y servicios antes del cobro.
    public static class CarritoGlobal
    {
        #region PROPIEDADES GLOBALES
        public static DataTable Detalles { get; set; }
        public static decimal TotalAPagar { get; set; }
        public static int? IdSocioPagara { get; set; }
        #endregion

        #region CONSTRUCTOR ESTÁTICO
        // Inicializa la estructura del carrito de compras en memoria al arrancar la aplicación.
        static CarritoGlobal()
        {
            Detalles = new DataTable();
            Detalles.Columns.Add("IdProducto", typeof(int));
            Detalles.Columns.Add("CodigoBarras", typeof(string));
            Detalles.Columns.Add("Concepto", typeof(string));
            Detalles.Columns.Add("Cantidad", typeof(int));
            Detalles.Columns.Add("PrecioUnitario", typeof(decimal));
            Detalles.Columns.Add("SubTotal", typeof(decimal));
            Detalles.Columns.Add("PorcentajeIva", typeof(int));

            TotalAPagar = 0;
            IdSocioPagara = null;
        }
        #endregion

        #region OPERACIONES DEL CARRITO
        // Añade un nuevo ítem a la tabla temporal y recalcula el monto total acumulado.
        public static void AgregarItem(int idProducto, string codigoBarras, string concepto, int cantidad, decimal precio, int porcentajeIva)
        {
            decimal subtotal = cantidad * precio;

            Detalles.Rows.Add(idProducto, codigoBarras, concepto, cantidad, precio, subtotal, porcentajeIva);

            TotalAPagar += subtotal;
        }

        // Restablece el carrito de compras y limpia los objetos vinculados.
        public static void LimpiarCarrito()
        {
            Detalles.Rows.Clear();
            TotalAPagar = 0;
            IdSocioPagara = null;
        }
        #endregion
    }
}