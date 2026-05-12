using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class VentaDatos
    {
        public int RegistrarVentaCompleta(decimal total, string metodoPago, string tipoComprobante, int? idUsuario, int? idSocio, DataTable carrito, out string mensajeError)
        {
            mensajeError = string.Empty;
            int idNuevaVenta = 0;

            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                using (SqlTransaction transaccion = oConexion.BeginTransaction())
                {
                    try
                    {
                        // 1. CREAR LA CABECERA (La Venta)
                        string queryVenta = @"INSERT INTO Ventas (IdSocio, Fecha, Total, MetodoPago, TipoComprobante, IdUsuario) 
                                              OUTPUT INSERTED.IdVenta 
                                              VALUES (@IdSocio, GETDATE(), @Total, @Metodo, @TipoComp, @Usuario)";

                        SqlCommand cmdVenta = new SqlCommand(queryVenta, oConexion, transaccion);
                        cmdVenta.Parameters.AddWithValue("@IdSocio", idSocio ?? (object)DBNull.Value);
                        cmdVenta.Parameters.AddWithValue("@Total", total);
                        cmdVenta.Parameters.AddWithValue("@Metodo", metodoPago);
                        cmdVenta.Parameters.AddWithValue("@TipoComp", tipoComprobante);
                        cmdVenta.Parameters.AddWithValue("@Usuario", idUsuario ?? (object)DBNull.Value);

                        idNuevaVenta = (int)cmdVenta.ExecuteScalar();

                        // 2. CREAR LOS DETALLES, DESCONTAR STOCK Y ACTUALIZAR PLANES
                        foreach (DataRow fila in carrito.Rows)
                        {
                            int idProducto = Convert.ToInt32(fila["IdProducto"]);
                            string concepto = fila["Concepto"].ToString();
                            int cantidad = Convert.ToInt32(fila["Cantidad"]);
                            decimal precio = Convert.ToDecimal(fila["PrecioUnitario"]);
                            decimal subtotal = Convert.ToDecimal(fila["SubTotal"]);
                            string codBarras = fila["CodigoBarras"].ToString();

                            // Insertamos el detalle
                            string queryDetalle = @"INSERT INTO VentasDetalle (IdVenta, IdProducto, Concepto, Cantidad, PrecioUnitario, SubTotal) 
                                                    VALUES (@IdVenta, @IdProd, @Concepto, @Cant, @Precio, @Sub)";
                            SqlCommand cmdDet = new SqlCommand(queryDetalle, oConexion, transaccion);
                            cmdDet.Parameters.AddWithValue("@IdVenta", idNuevaVenta);
                            cmdDet.Parameters.AddWithValue("@IdProd", idProducto > 0 ? idProducto : (object)DBNull.Value);
                            cmdDet.Parameters.AddWithValue("@Concepto", concepto);
                            cmdDet.Parameters.AddWithValue("@Cant", cantidad);
                            cmdDet.Parameters.AddWithValue("@Precio", precio);
                            cmdDet.Parameters.AddWithValue("@Sub", subtotal);
                            cmdDet.ExecuteNonQuery();

                            // Descontar Stock si es producto físico
                            if (idProducto > 0)
                            {
                                string queryStock = "UPDATE Productos SET StockActual = StockActual - @cantidadVendida WHERE IdProducto = @idProducto";
                                SqlCommand cmdStock = new SqlCommand(queryStock, oConexion, transaccion);
                                cmdStock.Parameters.AddWithValue("@cantidadVendida", cantidad);
                                cmdStock.Parameters.AddWithValue("@idProducto", idProducto);
                                cmdStock.ExecuteNonQuery();
                            }

                            // 3. LA MAGIA MEJORADA: Actualizamos al socio inmediatamente
                            if (codBarras.StartsWith("PLAN-"))
                            {
                                string[] partes = codBarras.Split('-');
                                if (partes.Length >= 4) // PLAN - Dias - IdSocio - IdPlan
                                {
                                    int dias = Convert.ToInt32(partes[1]);
                                    int idSocioPlan = Convert.ToInt32(partes[2]);
                                    int idPlanNuevo = Convert.ToInt32(partes[3]);

                                    string querySocio = @"
                                        UPDATE Socios SET 
                                            IdPlan = @NuevoPlan, 
                                            Estado = 'Activo',
                                            FechaVencimiento = CASE 
                                                WHEN FechaVencimiento > GETDATE() THEN DATEADD(day, @Dias, FechaVencimiento) 
                                                ELSE DATEADD(day, @Dias, GETDATE()) 
                                            END
                                        WHERE IdSocio = @Socio";

                                    SqlCommand cmdSocio = new SqlCommand(querySocio, oConexion, transaccion);
                                    cmdSocio.Parameters.AddWithValue("@NuevoPlan", idPlanNuevo);
                                    cmdSocio.Parameters.AddWithValue("@Dias", dias);
                                    cmdSocio.Parameters.AddWithValue("@Socio", idSocioPlan);
                                    cmdSocio.ExecuteNonQuery();
                                }
                            }
                        }

                        transaccion.Commit();
                        return idNuevaVenta;
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        mensajeError = ex.Message;
                        return 0;
                    }
                }
            }
        }
    }
}