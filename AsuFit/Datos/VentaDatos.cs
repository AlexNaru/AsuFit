using AsuFit.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AsuFit.Datos
{
    // Gestiona las operaciones transaccionales para el registro de ventas y consultas del historial.
    public class VentaDatos
    {
        #region 1. VENTA TRANSACCIONAL (ESCRITURA)
        // Registra una venta completa con sus detalles y aplica lógica de suscripción si corresponde.
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
                        // 1. Registro de Cabecera
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

                        // 2. Registro de Detalle y lógica de negocio
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

                            // Actualización de inventario
                            if (item.IdProducto > 0)
                            {
                                string queryStock = "UPDATE Productos SET StockActual = StockActual - @cantidadVendida WHERE IdProducto = @idProducto";
                                SqlCommand cmdStock = new SqlCommand(queryStock, oConexion, transaccion);
                                cmdStock.Parameters.AddWithValue("@cantidadVendida", item.Cantidad);
                                cmdStock.Parameters.AddWithValue("@idProducto", item.IdProducto);
                                cmdStock.ExecuteNonQuery();
                            }

                            // Procesamiento automático de planes
                            if (item.CodigoBarras.StartsWith("PLAN-"))
                            {
                                string[] partes = item.CodigoBarras.Split('-');
                                if (partes.Length >= 4)
                                {
                                    int dias = Convert.ToInt32(partes[1]);
                                    int idSocioPlan = Convert.ToInt32(partes[2]);
                                    int idPlanNuevo = Convert.ToInt32(partes[3]);

                                    string querySocio = @"UPDATE Socios SET IdPlan = @NuevoPlan, Estado = 'Activo',
                                            FechaVencimiento = CASE 
                                                WHEN FechaVencimiento > GETDATE() THEN DATEADD(day, @Dias, FechaVencimiento) 
                                                ELSE DATEADD(day, @Dias, GETDATE()) 
                                            END WHERE IdSocio = @Socio";

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
        #endregion

        #region 2. CONSULTAS E HISTORIALES (LECTURA)
        // Recupera el historial de transacciones con filtros dinámicos
        public DataTable ObtenerHistorialVentas(DateTime desde, DateTime hasta, string filtro, string tipoFiltro)
        {
            DataTable dtVentas = new DataTable();

            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                string query = @"
                    WITH Clasificacion AS (
                        SELECT 
                            v.IdVenta,
                            SUM(CASE WHEN vd.IdProducto > 0 THEN 1 ELSE 0 END) as CantProductos,
                            SUM(CASE WHEN vd.IdProducto = 0 OR vd.IdProducto IS NULL THEN 1 ELSE 0 END) as CantMensualidades
                        FROM Ventas v
                        INNER JOIN VentasDetalle vd ON v.IdVenta = vd.IdVenta
                        GROUP BY v.IdVenta
                    )
                    SELECT 
                        v.IdVenta AS [N° Transacción],
                        v.Fecha AS [Fecha],
                        ISNULL(s.Nombre + ' ' + s.Apellido, 'Cliente Ocasional') AS [Cliente],
                        v.MetodoPago AS [Método],
                        CASE 
                            WHEN c.CantProductos > 0 AND c.CantMensualidades > 0 THEN 'Mixto (Cuota+Producto)'
                            WHEN c.CantMensualidades > 0 THEN 'Mensualidad'
                            ELSE 'Producto'
                        END AS [Tipo Operación],
                        v.Total AS [Total Cobrado]
                    FROM Ventas v
                    LEFT JOIN Socios s ON v.IdSocio = s.IdSocio
                    INNER JOIN Clasificacion c ON v.IdVenta = c.IdVenta
                    WHERE CAST(v.Fecha AS DATE) BETWEEN @Desde AND @Hasta
                    AND (ISNULL(s.Nombre, '') LIKE '%' + @Filtro + '%' 
                         OR ISNULL(s.Apellido, '') LIKE '%' + @Filtro + '%'
                         OR s.Cedula LIKE '%' + @Filtro + '%'
                         OR CAST(v.IdVenta AS VARCHAR) LIKE '%' + @Filtro + '%')
                    AND (@TipoFiltro = 'Todos' OR @TipoFiltro = 'Todas'
                         OR (@TipoFiltro LIKE '%Producto%' AND c.CantProductos > 0 AND c.CantMensualidades = 0)
                         OR (@TipoFiltro LIKE '%Mensualidad%' AND c.CantMensualidades > 0 AND c.CantProductos = 0)
                         OR (@TipoFiltro LIKE '%Mixto%' AND c.CantProductos > 0 AND c.CantMensualidades > 0))
                    ORDER BY v.Fecha DESC";

                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.Parameters.AddWithValue("@Desde", desde);
                cmd.Parameters.AddWithValue("@Hasta", hasta);
                cmd.Parameters.AddWithValue("@Filtro", filtro);
                cmd.Parameters.AddWithValue("@TipoFiltro", tipoFiltro);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtVentas);
            }

            return dtVentas;
        }
        #endregion
    }
}