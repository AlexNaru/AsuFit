using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class InventarioDatos
    {
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

        public bool GuardarProducto(int id, string codigo, string nombre, string categoria, decimal precio, int stock, int idProveedor, int porcentajeIva)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                string query = "";

                if (id == 0)
                {
                    // SOLUCIÓN AL ERROR CRÍTICO: PONEMOS EL COSTO EN 0 AL NACER EL PRODUCTO
                    query = @"INSERT INTO Productos (CodigoBarras, Nombre, IdCategoria, PrecioVenta, PrecioCompra, StockActual, Estado, IdProveedor, PorcentajeIva) 
                      VALUES (@Codigo, @Nombre, (SELECT IdCategoria FROM CategoriasProducto WHERE Descripcion = @Categoria), @Precio, 0, @Stock, 'Activo', @IdProveedor, @PorcentajeIva)";
                }
                else
                {
                    query = @"UPDATE Productos SET 
                      CodigoBarras = @Codigo, Nombre = @Nombre, 
                      IdCategoria = (SELECT IdCategoria FROM CategoriasProducto WHERE Descripcion = @Categoria), 
                      PrecioVenta = @Precio, StockActual = @Stock,
                      IdProveedor = @IdProveedor, PorcentajeIva = @PorcentajeIva
                      WHERE IdProducto = @Id";
                }

                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Categoria", categoria);
                cmd.Parameters.AddWithValue("@Precio", precio);
                cmd.Parameters.AddWithValue("@Stock", stock);
                cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);
                cmd.Parameters.AddWithValue("@PorcentajeIva", porcentajeIva);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // --- NUEVO: AHORA GUARDA EL STOCK Y EL PRECIO DE COMPRA UNITARIO ---
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
    }
}