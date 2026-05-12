using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class IngresoMercaderiaDatos
    {
        public bool RegistrarIngreso(int idProveedor, int idProducto, int cantidad, decimal costoTotal, DateTime fechaIngreso, string observaciones)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlTransaction transaccion = null;

                try
                {
                    cn.Open();
                    transaccion = cn.BeginTransaction();

                    // Calculamos el Precio de Compra Unitario automáticamente
                    decimal precioCompraUnitario = 0;
                    if (cantidad > 0)
                    {
                        precioCompraUnitario = costoTotal / cantidad;
                    }

                    // 1. CABECERA: Guardamos la "Factura" general y capturamos el ID generado
                    string queryIngreso = @"INSERT INTO IngresosMercaderia (IdProveedor, CostoTotal, FechaIngreso, Observaciones) 
                                            OUTPUT INSERTED.IdIngreso 
                                            VALUES (@IdProveedor, @CostoTotal, @FechaIngreso, @Observaciones)";

                    SqlCommand cmdIngreso = new SqlCommand(queryIngreso, cn, transaccion);
                    cmdIngreso.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    cmdIngreso.Parameters.AddWithValue("@CostoTotal", costoTotal);
                    cmdIngreso.Parameters.AddWithValue("@FechaIngreso", fechaIngreso);

                    // Si observaciones viene null, enviamos un DBNull
                    if (string.IsNullOrEmpty(observaciones))
                        cmdIngreso.Parameters.AddWithValue("@Observaciones", DBNull.Value);
                    else
                        cmdIngreso.Parameters.AddWithValue("@Observaciones", observaciones);

                    // Ejecutamos y guardamos el IdIngreso que SQL nos devuelve
                    int idIngresoGenerado = (int)cmdIngreso.ExecuteScalar();

                    // 2. DETALLE: Guardamos qué producto compramos y lo unimos a la cabecera
                    // SOLUCIÓN: Quitamos 'Subtotal' porque tu base de datos lo calcula automáticamente
                    string queryDetalle = @"INSERT INTO IngresosMercaderiaDetalle (IdIngreso, IdProducto, Cantidad, PrecioCompra) 
                                            VALUES (@IdIngreso, @IdProducto, @Cantidad, @PrecioCompra)";

                    SqlCommand cmdDetalle = new SqlCommand(queryDetalle, cn, transaccion);
                    cmdDetalle.Parameters.AddWithValue("@IdIngreso", idIngresoGenerado);
                    cmdDetalle.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmdDetalle.Parameters.AddWithValue("@PrecioCompra", precioCompraUnitario);

                    cmdDetalle.ExecuteNonQuery();

                    // 3. ACTUALIZAR PRODUCTO: Sumamos stock y actualizamos su nuevo costo unitario
                    string queryStock = @"UPDATE Productos 
                                          SET StockActual = StockActual + @CantidadIngresada, 
                                              PrecioCompra = @NuevoCosto 
                                          WHERE IdProducto = @IdProducto";

                    SqlCommand cmdStock = new SqlCommand(queryStock, cn, transaccion);
                    cmdStock.Parameters.AddWithValue("@CantidadIngresada", cantidad);
                    cmdStock.Parameters.AddWithValue("@NuevoCosto", precioCompraUnitario);
                    cmdStock.Parameters.AddWithValue("@IdProducto", idProducto);

                    cmdStock.ExecuteNonQuery();

                    // Si todo salió perfecto, confirmamos el guardado en bloque
                    transaccion.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    // Si falla cualquier paso, deshacemos todo
                    if (transaccion != null)
                    {
                        transaccion.Rollback();
                    }
                    throw new Exception("Error al registrar el ingreso: " + ex.Message);
                }
            }
        }
    }
}