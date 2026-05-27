namespace AsuFit.Entidades
{
    // Almacena los datos de contacto y facturación de los proveedores de mercadería.
    public class Proveedor
    {
        #region PROPIEDADES
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string RUC { get; set; }
        public string Categoria { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        #endregion
    }
}