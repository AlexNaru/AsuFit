using System.Data;

namespace AsuFit.Entidades
{
    public static class CarritoGlobal
    {
        public static DataTable Detalles { get; set; }
        public static decimal TotalAPagar { get; set; }

        public static int? IdSocioPagara { get; set; }

        static CarritoGlobal()
        {
            Detalles = new DataTable();
            Detalles.Columns.Add("IdProducto", typeof(int));
            Detalles.Columns.Add("CodigoBarras", typeof(string)); // NUEVA COLUMNA
            Detalles.Columns.Add("Concepto", typeof(string));
            Detalles.Columns.Add("Cantidad", typeof(int));
            Detalles.Columns.Add("PrecioUnitario", typeof(decimal));
            Detalles.Columns.Add("SubTotal", typeof(decimal));
            TotalAPagar = 0;
            IdSocioPagara = null;
        }

        // Modificamos para pedir el código de barras
        public static void AgregarItem(int idProducto, string codigoBarras, string concepto, int cantidad, decimal precio)
        {
            decimal subtotal = cantidad * precio;
            Detalles.Rows.Add(idProducto, codigoBarras, concepto, cantidad, precio, subtotal);
            TotalAPagar += subtotal;
        }

        public static void LimpiarCarrito()
        {
            Detalles.Rows.Clear();
            TotalAPagar = 0;
            IdSocioPagara = null;
        }
    }
}