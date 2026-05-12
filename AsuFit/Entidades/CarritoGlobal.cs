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
            Detalles.Columns.Add("CodigoBarras", typeof(string));
            Detalles.Columns.Add("Concepto", typeof(string));
            Detalles.Columns.Add("Cantidad", typeof(int));
            Detalles.Columns.Add("PrecioUnitario", typeof(decimal));
            Detalles.Columns.Add("SubTotal", typeof(decimal));

            // --- NUEVA COLUMNA PARA EL IVA ---
            Detalles.Columns.Add("PorcentajeIva", typeof(int));

            TotalAPagar = 0;
            IdSocioPagara = null;
        }

        // Modificamos para pedir el PorcentajeIva al final
        public static void AgregarItem(int idProducto, string codigoBarras, string concepto, int cantidad, decimal precio, int porcentajeIva)
        {
            decimal subtotal = cantidad * precio;

            // Guardamos la fila completa con el IVA incluido
            Detalles.Rows.Add(idProducto, codigoBarras, concepto, cantidad, precio, subtotal, porcentajeIva);

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