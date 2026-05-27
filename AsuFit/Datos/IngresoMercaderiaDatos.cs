using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    // Gestiona las operaciones de acceso a datos relacionadas con el ingreso y reposición de mercadería.
    public class IngresoMercaderiaDatos
    {
        #region OPERACIONES TRANSACCIONALES
        // Registra un ingreso de mercadería ejecutando cabecera, detalle y actualización de stock bajo una transacción.
        public bool RegistrarIngreso(int idProveedor, int idProducto, int cantidad, decimal costoTotal, DateTime fechaIngreso, string observaciones)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlTransaction transaccion = null;

                try
                {
                    cn.Open();
                    transaccion = cn.BeginTransaction();

                    decimal precioCompraUnitario = 0;
                    if (cantidad > 0)
                    {
                        precioCompraUnitario = costoTotal / cantidad;
                    }

                    // 1. Inserción de Cabecera
                    string queryIngreso = @"INSERT INTO IngresosMercaderia (IdProveedor, CostoTotal, FechaIngreso, Observaciones) 
                                            OUTPUT INSERTED.IdIngreso 
                                            VALUES (@IdProveedor, @CostoTotal, @FechaIngreso, @Observaciones)";

                    SqlCommand cmdIngreso = new SqlCommand(queryIngreso, cn, transaccion);
                    cmdIngreso.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    cmdIngreso.Parameters.AddWithValue("@CostoTotal", costoTotal);
                    cmdIngreso.Parameters.AddWithValue("@FechaIngreso", fechaIngreso);
                    cmdIngreso.Parameters.AddWithValue("@Observaciones", string.IsNullOrEmpty(observaciones) ? (object)DBNull.Value : observaciones);

                    int idIngresoGenerado = (int)cmdIngreso.ExecuteScalar();

                    // 2. Inserción de Detalle
                    string queryDetalle = @"INSERT INTO IngresosMercaderiaDetalle (IdIngreso, IdProducto, Cantidad, PrecioCompra) 
                                            VALUES (@IdIngreso, @IdProducto, @Cantidad, @PrecioCompra)";

                    SqlCommand cmdDetalle = new SqlCommand(queryDetalle, cn, transaccion);
                    cmdDetalle.Parameters.AddWithValue("@IdIngreso", idIngresoGenerado);
                    cmdDetalle.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmdDetalle.Parameters.AddWithValue("@PrecioCompra", precioCompraUnitario);

                    cmdDetalle.ExecuteNonQuery();

                    // 3. Actualización de Inventario y Costos
                    string queryStock = @"UPDATE Productos 
                                          SET StockActual = StockActual + @CantidadIngresada, 
                                              PrecioCompra = @NuevoCosto 
                                          WHERE IdProducto = @IdProducto";

                    SqlCommand cmdStock = new SqlCommand(queryStock, cn, transaccion);
                    cmdStock.Parameters.AddWithValue("@CantidadIngresada", cantidad);
                    cmdStock.Parameters.AddWithValue("@NuevoCosto", precioCompraUnitario);
                    cmdStock.Parameters.AddWithValue("@IdProducto", idProducto);

                    cmdStock.ExecuteNonQuery();

                    transaccion.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    if (transaccion != null)
                    {
                        transaccion.Rollback();
                    }
                    throw new Exception("Error al registrar el ingreso transaccional: " + ex.Message);
                }
            }
        }
        #endregion
    }
}