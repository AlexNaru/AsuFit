using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    public class IngresoMercaderiaDatos
    {
        // Método para registrar un nuevo ingreso de mercadería
        public bool RegistrarIngreso(int idProveedor, int idProducto, int cantidad, decimal costoTotal, DateTime fechaIngreso, string observaciones)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                // Usamos una transacción SQL porque haremos dos cosas: 
                // 1. Guardar el registro del ingreso
                // 2. Sumar el stock al producto
                SqlTransaction transaccion = null;

                try
                {
                    cn.Open();
                    transaccion = cn.BeginTransaction();

                    // 1. Registrar el movimiento de ingreso (Asegúrate de tener una tabla similar a esta)
                    string queryIngreso = @"INSERT INTO IngresosMercaderia (IdProveedor, IdProducto, Cantidad, CostoTotal, FechaIngreso, Observaciones) 
                                            VALUES (@IdProveedor, @IdProducto, @Cantidad, @CostoTotal, @FechaIngreso, @Observaciones)";

                    SqlCommand cmdIngreso = new SqlCommand(queryIngreso, cn, transaccion);
                    cmdIngreso.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    cmdIngreso.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmdIngreso.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmdIngreso.Parameters.AddWithValue("@CostoTotal", costoTotal);
                    cmdIngreso.Parameters.AddWithValue("@FechaIngreso", fechaIngreso);
                    cmdIngreso.Parameters.AddWithValue("@Observaciones", observaciones);

                    cmdIngreso.ExecuteNonQuery();

                    // 2. Actualizar el Stock del Producto
                    string queryStock = @"UPDATE Productos 
                                          SET StockActual = StockActual + @CantidadIngresada 
                                          WHERE IdProducto = @IdProducto";

                    SqlCommand cmdStock = new SqlCommand(queryStock, cn, transaccion);
                    cmdStock.Parameters.AddWithValue("@CantidadIngresada", cantidad);
                    cmdStock.Parameters.AddWithValue("@IdProducto", idProducto);

                    cmdStock.ExecuteNonQuery();

                    // Si ambas operaciones salen bien, confirmamos los cambios
                    transaccion.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    // Si algo falla, deshacemos todo para que no queden datos a medias
                    if (transaccion != null)
                    {
                        transaccion.Rollback();
                    }
                    throw new Exception("Error al registrar el ingreso de mercadería: " + ex.Message);
                }
            }
        }

        // Aquí podrías agregar un método Listar() si quieres ver un historial de compras
    }
}