using AsuFit.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class VentaDatos
    {
        public int RegistrarVentaCompleta(Venta objVenta, out string mensajeError)
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
                        // 1. CREAR LA CABECERA
                        string queryVenta = @"INSERT INTO Ventas (IdSocio, Fecha, Total, MetodoPago, TipoComprobante, IdUsuario) 
                                              OUTPUT INSERTED.IdVenta 
                                              VALUES (@IdSocio, GETDATE(), @Total, @Metodo, @TipoComp, @Usuario)";

                        SqlCommand cmdVenta = new SqlCommand(queryVenta, oConexion, transaccion);
                        cmdVenta.Parameters.AddWithValue("@IdSocio", objVenta.IdSocio ?? (object)DBNull.Value);
                        cmdVenta.Parameters.AddWithValue("@Total", objVenta.Total);
                        cmdVenta.Parameters.AddWithValue("@Metodo", objVenta.MetodoPago);
                        cmdVenta.Parameters.AddWithValue("@TipoComp", objVenta.TipoComprobante);
                        cmdVenta.Parameters.AddWithValue("@Usuario", objVenta.IdUsuario ?? (object)DBNull.Value);

                        idNuevaVenta = (int)cmdVenta.ExecuteScalar();

                        // 2. CREAR LOS DETALLES LEYENDO LA LISTA DE LA ENTIDAD
                        foreach (DetalleVenta item in objVenta.Detalles)
                        {
                            string queryDetalle = @"INSERT INTO VentasDetalle (IdVenta, IdProducto, Concepto, Cantidad, PrecioUnitario, SubTotal) 
                                                    VALUES (@IdVenta, @IdProd, @Concepto, @Cant, @Precio, @Sub)";
                            SqlCommand cmdDet = new SqlCommand(queryDetalle, oConexion, transaccion);
                            cmdDet.Parameters.AddWithValue("@IdVenta", idNuevaVenta);
                            cmdDet.Parameters.AddWithValue("@IdProd", item.IdProducto > 0 ? item.IdProducto : (object)DBNull.Value);
                            cmdDet.Parameters.AddWithValue("@Concepto", item.Concepto);
                            cmdDet.Parameters.AddWithValue("@Cant", item.Cantidad);
                            cmdDet.Parameters.AddWithValue("@Precio", item.PrecioUnitario);
                            cmdDet.Parameters.AddWithValue("@Sub", item.SubTotal);
                            cmdDet.ExecuteNonQuery();

                            if (item.IdProducto > 0)
                            {
                                string queryStock = "UPDATE Productos SET StockActual = StockActual - @cantidadVendida WHERE IdProducto = @idProducto";
                                SqlCommand cmdStock = new SqlCommand(queryStock, oConexion, transaccion);
                                cmdStock.Parameters.AddWithValue("@cantidadVendida", item.Cantidad);
                                cmdStock.Parameters.AddWithValue("@idProducto", item.IdProducto);
                                cmdStock.ExecuteNonQuery();
                            }

                            if (item.CodigoBarras.StartsWith("PLAN-"))
                            {
                                string[] partes = item.CodigoBarras.Split('-');
                                if (partes.Length >= 4)
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