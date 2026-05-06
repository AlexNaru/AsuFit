using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class InventarioDatos
    {
        // 1. Método para traer las categorías (Bebidas, Suplementos, Snacks)
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
                catch (Exception)
                {
                    // Si hay error en la base de datos, lo pasamos hacia arriba
                    throw;
                }
            }
            return dt;
        }

        // 2. Método para traer todo el catálogo de productos a la pantalla de venta
        // 2. Método para traer todo el catálogo de productos
        public DataTable ListarProductos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    // Agregamos p.Estado a la consulta y ELIMINAMOS el WHERE p.Estado = 'Activo'
                    string query = @"SELECT p.IdProducto, 
                                            p.CodigoBarras, 
                                            p.Nombre, 
                                            c.Descripcion AS Categoria, 
                                            p.PrecioCompra, 
                                            p.PrecioVenta, 
                                            p.StockActual, 
                                            p.StockMinimo,
                                            p.Estado,
                                            p.IdProveedor,
                                            pr.Nombre AS Proveedor
                                     FROM Productos p
                                     INNER JOIN CategoriasProducto c ON p.IdCategoria = c.IdCategoria
                                     LEFT JOIN Proveedores pr ON p.IdProveedor = pr.IdProveedor";
                    SqlDataAdapter da = new SqlDataAdapter(query, oConexion);
                    da.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return dt;
        }

        // --- NUEVO MÉTODO (Reemplaza a EliminarProducto) ---
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

        // Asegurate de tener este using arriba del todo si no lo tenías:
        // using System.Data.SqlClient;

        public bool RegistrarVenta(decimal total, DataTable detalleVenta)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                // Iniciamos la Transacción (Todo o Nada)
                SqlTransaction transaccion = oConexion.BeginTransaction();
                try
                {
                    // 1. Insertamos la Cabecera (El Ticket general) y obtenemos su ID generado
                    string queryVenta = "INSERT INTO Ventas (Total) OUTPUT INSERTED.IdVenta VALUES (@Total)";
                    SqlCommand cmdVenta = new SqlCommand(queryVenta, oConexion, transaccion);
                    cmdVenta.Parameters.AddWithValue("@Total", total);

                    int idVentaGenerado = (int)cmdVenta.ExecuteScalar(); // Atrapamos el ID

                    // 2. Recorremos los productos del carrito para guardarlos y descontar stock
                    foreach (DataRow row in detalleVenta.Rows)
                    {
                        // A) Guardamos el detalle
                        string queryDetalle = @"INSERT INTO VentasDetalle (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
                                        VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, @Subtotal)";
                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle, oConexion, transaccion);
                        cmdDetalle.Parameters.AddWithValue("@IdVenta", idVentaGenerado);
                        cmdDetalle.Parameters.AddWithValue("@IdProducto", row["IdProducto"]);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", row["Cantidad"]);
                        cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", row["PrecioUnitario"]);
                        cmdDetalle.Parameters.AddWithValue("@Subtotal", row["Subtotal"]);
                        cmdDetalle.ExecuteNonQuery();

                        // B) Descontamos el stock del producto (Permite ir a negativos)
                        string queryStock = "UPDATE Productos SET StockActual = StockActual - @Cantidad WHERE IdProducto = @IdProducto";
                        SqlCommand cmdStock = new SqlCommand(queryStock, oConexion, transaccion);
                        cmdStock.Parameters.AddWithValue("@Cantidad", row["Cantidad"]);
                        cmdStock.Parameters.AddWithValue("@IdProducto", row["IdProducto"]);
                        cmdStock.ExecuteNonQuery();
                    }

                    // Si llegamos hasta acá sin errores, confirmamos el guardado definitivo
                    transaccion.Commit();
                    return true;
                }
                catch (Exception)
                {
                    // Si hubo cualquier error, cancelamos todo para que no queden datos corruptos
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        // --- MÉTODO PARA INSERTAR O ACTUALIZAR PRODUCTOS ---
        // --- MÉTODO PARA INSERTAR O ACTUALIZAR PRODUCTOS ---
        public bool GuardarProducto(int id, string codigo, string nombre, string categoria, decimal precio, int stock, int idProveedor)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                string query = "";

                // Si el ID es 0, es un producto NUEVO
                if (id == 0)
                {
                    query = @"INSERT INTO Productos (CodigoBarras, Nombre, IdCategoria, PrecioVenta, StockActual, Estado, IdProveedor) 
                              VALUES (@Codigo, @Nombre, (SELECT IdCategoria FROM CategoriasProducto WHERE Descripcion = @Categoria), @Precio, @Stock, 'Activo', @IdProveedor)";
                }
                else // Si tiene ID, estamos EDITANDO
                {
                    query = @"UPDATE Productos SET 
                              CodigoBarras = @Codigo, 
                              Nombre = @Nombre, 
                              IdCategoria = (SELECT IdCategoria FROM CategoriasProducto WHERE Descripcion = @Categoria), 
                              PrecioVenta = @Precio, 
                              StockActual = @Stock,
                              IdProveedor = @IdProveedor
                              WHERE IdProducto = @Id";
                }

                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Categoria", categoria);
                cmd.Parameters.AddWithValue("@Precio", precio);
                cmd.Parameters.AddWithValue("@Stock", stock);

                // El nuevo parámetro para SQL
                cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool SumarStock(int idProducto, int cantidadAumentar)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                // Le decimos a SQL que sume la cantidad nueva a lo que ya existe
                string query = "UPDATE Productos SET StockActual = StockActual + @Cantidad WHERE IdProducto = @Id";
                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.Parameters.AddWithValue("@Cantidad", cantidadAumentar);
                cmd.Parameters.AddWithValue("@Id", idProducto);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}