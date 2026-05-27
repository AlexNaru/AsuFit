using AsuFit.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    // Gestiona las operaciones de persistencia para la entidad Productos e Inventario.
    public class InventarioDatos
    {
        #region CONSULTAS DE INVENTARIO
        // Recupera el listado de categorías activas.
        public DataTable ListarCategorias()
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "SELECT IdCategoria, Descripcion FROM CategoriasProducto WHERE Estado = 'Activo'";
                    SqlDataAdapter da = new SqlDataAdapter(query, oConexion);
                    da.Fill(dt);
                }
                catch (Exception) { throw; }
            }
            return dt;
        }

        // Recupera el listado completo de productos con datos de proveedores y categorías.
        public DataTable ListarProductos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"SELECT p.IdProducto, p.CodigoBarras, p.Nombre, c.Descripcion AS Categoria, 
                                            p.PrecioCompra, p.PrecioVenta, p.StockActual, p.StockMinimo, p.Estado,
                                            p.IdProveedor, pr.Nombre AS Proveedor, p.PorcentajeIva 
                                     FROM Productos p
                                     INNER JOIN CategoriasProducto c ON p.IdCategoria = c.IdCategoria
                                     LEFT JOIN Proveedores pr ON p.IdProveedor = pr.IdProveedor";
                    SqlDataAdapter da = new SqlDataAdapter(query, oConexion);
                    da.Fill(dt);
                }
                catch (Exception) { throw; }
            }
            return dt;
        }
        #endregion

        #region OPERACIONES DE STOCK Y PRODUCTOS
        // Modifica el estado lógico de un producto.
        public bool CambiarEstado(int idProducto, string nuevoEstado)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                string query = "UPDATE Productos SET Estado = @Estado WHERE IdProducto = @Id";
                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                cmd.Parameters.AddWithValue("@Id", idProducto);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Persiste los datos de un producto (Nuevo o Edición).
        public bool GuardarProducto(Producto obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                string query = (obj.IdProducto == 0)
                    ? @"INSERT INTO Productos (CodigoBarras, Nombre, IdCategoria, PrecioVenta, PrecioCompra, StockActual, Estado, IdProveedor, PorcentajeIva) 
                      VALUES (@Codigo, @Nombre, (SELECT IdCategoria FROM CategoriasProducto WHERE Descripcion = @Categoria), @Precio, 0, @Stock, 'Activo', @IdProveedor, @PorcentajeIva)"
                    : @"UPDATE Productos SET CodigoBarras = @Codigo, Nombre = @Nombre, 
                      IdCategoria = (SELECT IdCategoria FROM CategoriasProducto WHERE Descripcion = @Categoria), 
                      PrecioVenta = @Precio, StockActual = @Stock, IdProveedor = @IdProveedor, PorcentajeIva = @PorcentajeIva
                      WHERE IdProducto = @IdProducto";

                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.Parameters.AddWithValue("@IdProducto", obj.IdProducto);
                cmd.Parameters.AddWithValue("@Codigo", obj.CodigoBarras);
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@Categoria", obj.Categoria);
                cmd.Parameters.AddWithValue("@Precio", obj.PrecioVenta);
                cmd.Parameters.AddWithValue("@Stock", obj.StockActual);
                cmd.Parameters.AddWithValue("@IdProveedor", obj.IdProveedor);
                cmd.Parameters.AddWithValue("@PorcentajeIva", obj.PorcentajeIva);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Incrementa stock y actualiza el costo promedio de compra.
        public bool SumarStock(int idProducto, int cantidadAumentar, decimal nuevoPrecioCompra)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                string query = "UPDATE Productos SET StockActual = StockActual + @Cantidad, PrecioCompra = @PrecioCompra WHERE IdProducto = @Id";
                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.Parameters.AddWithValue("@Cantidad", cantidadAumentar);
                cmd.Parameters.AddWithValue("@PrecioCompra", nuevoPrecioCompra);
                cmd.Parameters.AddWithValue("@Id", idProducto);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        #endregion

        #region TRANSACCIONES DE VENTA
        // Ejecuta el registro de una venta y descuenta el stock de manera transaccional.
        public bool RegistrarVenta(decimal total, DataTable detalleVenta)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                SqlTransaction transaccion = oConexion.BeginTransaction();
                try
                {
                    string queryVenta = "INSERT INTO Ventas (Total) OUTPUT INSERTED.IdVenta VALUES (@Total)";
                    SqlCommand cmdVenta = new SqlCommand(queryVenta, oConexion, transaccion);
                    cmdVenta.Parameters.AddWithValue("@Total", total);
                    int idVentaGenerado = (int)cmdVenta.ExecuteScalar();

                    foreach (DataRow row in detalleVenta.Rows)
                    {
                        string queryDetalle = @"INSERT INTO VentasDetalle (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
                                        VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, @Subtotal)";
                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle, oConexion, transaccion);
                        cmdDetalle.Parameters.AddWithValue("@IdVenta", idVentaGenerado);
                        cmdDetalle.Parameters.AddWithValue("@IdProducto", row["IdProducto"]);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", row["Cantidad"]);
                        cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", row["PrecioUnitario"]);
                        cmdDetalle.Parameters.AddWithValue("@Subtotal", row["Subtotal"]);
                        cmdDetalle.ExecuteNonQuery();

                        string queryStock = "UPDATE Productos SET StockActual = StockActual - @Cantidad WHERE IdProducto = @IdProducto";
                        SqlCommand cmdStock = new SqlCommand(queryStock, oConexion, transaccion);
                        cmdStock.Parameters.AddWithValue("@Cantidad", row["Cantidad"]);
                        cmdStock.Parameters.AddWithValue("@IdProducto", row["IdProducto"]);
                        cmdStock.ExecuteNonQuery();
                    }
                    transaccion.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }
        #endregion
    }
}