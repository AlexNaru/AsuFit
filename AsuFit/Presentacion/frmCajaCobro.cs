using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net;        // <-- NUEVO: Para red
using System.Net.Mail;   // <-- NUEVO: Para correos

namespace AsuFit.Presentacion
{
    public partial class frmCajaCobro : Form
    {
        private decimal totalAPagar = 0;
        private string correoClienteActual = "";
        private int? idClienteActual = null;

        private DataTable carritoDetalles;
        private Usuario cajeroActual;

        public frmCajaCobro(Usuario usuarioCajero)
        {
            InitializeComponent();

            totalAPagar = CarritoGlobal.TotalAPagar;
            carritoDetalles = CarritoGlobal.Detalles;
            cajeroActual = usuarioCajero;

            lblTotalCobrar.Text = "Gs. " + totalAPagar.ToString("N0");

            if (cajeroActual != null)
            {
                txtCajero.Text = $" {cajeroActual.Rol} - {cajeroActual.NombreCompleto}";
            }
            else
            {
                txtCajero.Text = "Cajero: Administrador (No detectado)";
            }

            cmbTipoComprobante.SelectedItem = "Ticket";
            cmbMetodoPago.SelectedItem = "Efectivo";

            if (CarritoGlobal.IdSocioPagara != null)
            {
                SocioNegocio negocio = new SocioNegocio();
                Socio socioInfo = negocio.BuscarSocioPorId(CarritoGlobal.IdSocioPagara.Value);

                if (socioInfo != null)
                {
                    txtBusquedaCliente.Text = socioInfo.Cedula;
                    txtNombreCliente.Text = socioInfo.Nombre + " " + socioInfo.Apellido;
                    txtRucCliente.Text = string.IsNullOrWhiteSpace(socioInfo.Ruc) ? "Sin RUC" : socioInfo.Ruc;

                    correoClienteActual = socioInfo.Email;
                    idClienteActual = socioInfo.IdSocio;
                }
            }
        }

        private void cmbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMetodoPago.SelectedItem != null)
            {
                string metodo = cmbMetodoPago.SelectedItem.ToString();

                if (metodo != "Efectivo")
                {
                    txtMontoRecibido.Enabled = false;
                    txtMontoRecibido.Text = totalAPagar.ToString();
                    txtVuelto.Text = "Gs. 0";
                }
                else
                {
                    txtMontoRecibido.Enabled = true;
                    txtMontoRecibido.Text = "";
                    txtVuelto.Text = "Gs. 0";
                }
            }
        }

        private void txtBusquedaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SocioNegocio negocio = new SocioNegocio();
                Socio socio = negocio.BuscarSocioPorCedula(txtBusquedaCliente.Text.Trim());

                if (socio != null)
                {
                    txtNombreCliente.Text = socio.Nombre + " " + socio.Apellido;
                    txtRucCliente.Text = string.IsNullOrWhiteSpace(socio.Ruc) ? "Sin RUC" : socio.Ruc;
                    correoClienteActual = socio.Email;
                    idClienteActual = socio.IdSocio;
                }
                else
                {
                    MessageBox.Show("No se encontró ningún socio con ese documento. Se registrará como cliente ocasional.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNombreCliente.Text = "Cliente Ocasional";
                    txtRucCliente.Text = "Sin RUC";
                    correoClienteActual = "";
                    idClienteActual = null;
                }
            }
        }

        private void txtMontoRecibido_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtMontoRecibido.Text, out decimal montoRecibido))
            {
                decimal vuelto = montoRecibido - totalAPagar;
                if (vuelto < 0) vuelto = 0;
                txtVuelto.Text = "Gs. " + vuelto.ToString("N0");
            }
            else
            {
                txtVuelto.Text = "Gs. 0";
            }
        }

        private void txtMontoRecibido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Estás seguro de que deseas cancelar esta operación y vaciar los datos?", "Cancelar Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                CarritoGlobal.LimpiarCarrito();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnAgregarMasCosas_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (carritoDetalles == null || carritoDetalles.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos o mensualidades para cobrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbMetodoPago.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un Método de Pago.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMetodoPago.Focus();
                return;
            }

            string tipoComprobante = cmbTipoComprobante.SelectedItem?.ToString();
            if (tipoComprobante == "Factura" && (txtRucCliente.Text == "Sin RUC" || string.IsNullOrWhiteSpace(txtRucCliente.Text)))
            {
                MessageBox.Show("Para emitir una Factura legal, debe buscar un cliente que tenga un RUC registrado.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBusquedaCliente.Focus();
                return;
            }

            if (cmbMetodoPago.SelectedItem != null && cmbMetodoPago.SelectedItem.ToString() == "Efectivo")
            {
                if (decimal.TryParse(txtMontoRecibido.Text, out decimal recibido))
                {
                    if (recibido < totalAPagar)
                    {
                        MessageBox.Show("El monto recibido es menor al total a cobrar.", "Dinero Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMontoRecibido.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un monto válido en la casilla 'Monto Recibido'.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMontoRecibido.Focus();
                    return;
                }
            }

            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                SqlTransaction transaccion = oConexion.BeginTransaction();

                try
                {
                    string queryCabecera = @"INSERT INTO Ventas 
                                   (IdSocio, Fecha, Total, MetodoPago, TipoComprobante, IdUsuario) 
                                   VALUES 
                                   (@IdSocio, GETDATE(), @Total, @Metodo, @Comprobante, @IdUsuario);
                                   SELECT SCOPE_IDENTITY();";
                    SqlCommand cmdCabecera = new SqlCommand(queryCabecera, oConexion, transaccion);
                    cmdCabecera.Parameters.AddWithValue("@IdSocio", idClienteActual ?? (object)DBNull.Value);
                    cmdCabecera.Parameters.AddWithValue("@Total", totalAPagar);
                    cmdCabecera.Parameters.AddWithValue("@Metodo", cmbMetodoPago.SelectedItem.ToString());
                    cmdCabecera.Parameters.AddWithValue("@Comprobante", tipoComprobante ?? "Ticket");
                    cmdCabecera.Parameters.AddWithValue("@IdUsuario", cajeroActual != null ? cajeroActual.IdUsuario : (object)DBNull.Value);

                    int idNuevaVenta = Convert.ToInt32(cmdCabecera.ExecuteScalar());

                    foreach (DataRow fila in carritoDetalles.Rows)
                    {
                        int idProd = Convert.ToInt32(fila["IdProducto"]);
                        int cantidadVendida = Convert.ToInt32(fila["Cantidad"]);

                        string queryDetalle = @"INSERT INTO VentasDetalle 
                                      (IdVenta, IdProducto, Concepto, Cantidad, PrecioUnitario, SubTotal) 
                                      VALUES 
                                      (@IdVenta, @IdProducto, @Concepto, @Cantidad, @Precio, @SubTotal)";
                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle, oConexion, transaccion);
                        cmdDetalle.Parameters.AddWithValue("@IdVenta", idNuevaVenta);
                        cmdDetalle.Parameters.AddWithValue("@IdProducto", idProd > 0 ? idProd : (object)DBNull.Value);
                        cmdDetalle.Parameters.AddWithValue("@Concepto", fila["Concepto"].ToString());
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", cantidadVendida);
                        cmdDetalle.Parameters.AddWithValue("@Precio", Convert.ToDecimal(fila["PrecioUnitario"]));
                        cmdDetalle.Parameters.AddWithValue("@SubTotal", Convert.ToDecimal(fila["SubTotal"]));
                        cmdDetalle.ExecuteNonQuery();

                        if (idProd > 0)
                        {
                            string queryStock = "UPDATE Productos SET StockActual = StockActual - @cantidadVendida WHERE IdProducto = @idProducto";
                            SqlCommand cmdStock = new SqlCommand(queryStock, oConexion, transaccion);
                            cmdStock.Parameters.AddWithValue("@cantidadVendida", cantidadVendida);
                            cmdStock.Parameters.AddWithValue("@idProducto", idProd);
                            cmdStock.ExecuteNonQuery();
                        }
                    }

                    transaccion.Commit();

                    MessageBox.Show($"¡Venta N° {idNuevaVenta} registrada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AsuFit.Reportes.GeneradorPDF generador = new AsuFit.Reportes.GeneradorPDF();
                    string nomCliente = string.IsNullOrWhiteSpace(txtNombreCliente.Text) ? "Cliente Ocasional" : txtNombreCliente.Text;
                    string rucCliente = string.IsNullOrWhiteSpace(txtRucCliente.Text) ? "Sin RUC" : txtRucCliente.Text;
                    string ciCliente = string.IsNullOrWhiteSpace(txtBusquedaCliente.Text) ? "Sin CI" : txtBusquedaCliente.Text;
                    string metodo = cmbMetodoPago.SelectedItem.ToString();
                    string numTicket = idNuevaVenta.ToString();

                    decimal dineroRecibido = 0;
                    decimal dineroVuelto = 0;

                    if (metodo == "Efectivo")
                    {
                        decimal.TryParse(txtMontoRecibido.Text, out dineroRecibido);
                        dineroVuelto = dineroRecibido - CarritoGlobal.TotalAPagar;
                        if (dineroVuelto < 0) dineroVuelto = 0;
                    }

                    string nombreDelCajero = cajeroActual != null ? cajeroActual.NombreCompleto : "Admin (No detectado)";

                    string correoFiltro = string.IsNullOrWhiteSpace(correoClienteActual) ? "Sin correo" : correoClienteActual;

                    if (tipoComprobante == "Factura")
                    {
                        generador.GenerarFacturaLegalA4(
                            CarritoGlobal.Detalles,
                            CarritoGlobal.TotalAPagar,
                            nomCliente,
                            ciCliente,
                            rucCliente,
                            correoFiltro,
                            metodo,
                            dineroRecibido,
                            dineroVuelto,
                            nombreDelCajero,
                            numTicket
                        );
                    }
                    else
                    {
                        generador.GenerarTicketTermico(
                            CarritoGlobal.Detalles,
                            CarritoGlobal.TotalAPagar,
                            nomCliente,
                            ciCliente,
                            rucCliente,
                            metodo,
                            dineroRecibido,
                            dineroVuelto,
                            nombreDelCajero,
                            numTicket
                        );
                    }

                    string rutaDescargas = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                    string nombreArchivo = tipoComprobante == "Factura" ? $"Factura_Legal_{numTicket}.pdf" : $"Comprobante_Venta_{numTicket}.pdf";
                    string rutaCompletaPdf = System.IO.Path.Combine(rutaDescargas, nombreArchivo);

                    if (correoFiltro != "Sin correo" && correoFiltro.Contains("@"))
                    {
                        EnviarCorreoConAdjunto(correoFiltro, rutaCompletaPdf, tipoComprobante ?? "Ticket", numTicket);
                    }

                    CarritoGlobal.LimpiarCarrito();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    MessageBox.Show("Error al procesar la venta: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            txtBusquedaCliente_KeyPress(txtBusquedaCliente, new KeyPressEventArgs((char)Keys.Enter));
        }

        // =========================================================================================
        // --- NUEVO MÉTODO: ENVÍO DE CORREO AUTOMÁTICO ---
        // =========================================================================================
        private void EnviarCorreoConAdjunto(string correoDestino, string rutaArchivo, string tipoComprobante, string nroComprobante)
        {
            try
            {
                string miCorreo = "alexisaguilarpsn@gmail.com";
                string miContrasenaApp = "prtziaqqyegoxjmc";

                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(miCorreo, "AsuFit GYM");
                correo.To.Add(correoDestino);
                correo.Subject = $"Tu {tipoComprobante} N° {nroComprobante} - AsuFit Gym";

                string cuerpo = $"Hola,\n\nAdjuntamos tu {tipoComprobante} correspondiente a tu última transacción en AsuFit Gym.\n\n¡Gracias por tu preferencia!\n\nSaludos,\nEl equipo de AsuFit Gym";
                correo.Body = cuerpo;
                correo.IsBodyHtml = false;

                if (System.IO.File.Exists(rutaArchivo))
                {
                    Attachment adjunto = new Attachment(rutaArchivo);
                    correo.Attachments.Add(adjunto);
                }

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;

                // --- LAS DOS LÍNEAS CLAVE PARA EVITAR EL BLOQUEO DE GOOGLE ---
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                // -------------------------------------------------------------

                smtp.Credentials = new NetworkCredential(miCorreo, miContrasenaApp);

                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("La venta se registró correctamente, pero hubo un problema al enviar el correo automático: " + ex.Message, "Aviso de Correo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}