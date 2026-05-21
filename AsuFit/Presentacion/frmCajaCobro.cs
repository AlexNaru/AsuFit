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

                // Limpiamos la pantalla de productos
                frmPuntoVenta inventario = Application.OpenForms["frmPuntoVenta"] as frmPuntoVenta;
                if (inventario != null) inventario.LimpiarGrillaVisual();

                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // =================================================================
        // NUEVO MÉTODO PÚBLICO: El inventario lo usará para "despertar" a la caja
        // =================================================================
        public void ActualizarPantallaDesdeCarrito()
        {
            totalAPagar = CarritoGlobal.TotalAPagar;
            lblTotalCobrar.Text = "Gs. " + totalAPagar.ToString("N0");

            // Forzamos el recálculo del vuelto
            txtMontoRecibido_TextChanged(null, null);
        }

        private void btnAgregarMasCosas_Click(object sender, EventArgs e)
        {
            // 1. Minimizamos la caja
            this.WindowState = FormWindowState.Minimized;

            // 2. Buscamos tu Dashboard principal abierto
            Form dashboardPrincipal = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "frmDashboard")
                {
                    dashboardPrincipal = f;
                    break;
                }
            }

            if (dashboardPrincipal != null)
            {
                // 3. Buscamos tu botón del menú lateral
                Control[] botonesMenu = dashboardPrincipal.Controls.Find("btnInventarioVentas", true);

                if (botonesMenu.Length > 0 && botonesMenu[0] is Button)
                {
                    Button btnMenuVentas = (Button)botonesMenu[0];

                    // El sistema hace clic en el menú lateral por el usuario
                    btnMenuVentas.PerformClick();
                }
                else
                {
                    MessageBox.Show("No se encontró el botón 'btnInventarioVentas' en el Dashboard.", "Aviso");
                }
            }
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
            string metodoPago = cmbMetodoPago.SelectedItem.ToString();

            if (tipoComprobante == "Factura" && (txtRucCliente.Text == "Sin RUC" || string.IsNullOrWhiteSpace(txtRucCliente.Text)))
            {
                MessageBox.Show("Para emitir una Factura legal, debe buscar un cliente que tenga un RUC registrado.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBusquedaCliente.Focus();
                return;
            }

            if (metodoPago == "Efectivo")
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

            // =============================================================
            // LLAMAMOS A NUESTRA NUEVA CAPA DE DATOS (ARQUITECTURA DE 3 CAPAS)
            // =============================================================
            Venta objVenta = new Venta();
            objVenta.Total = totalAPagar;
            objVenta.MetodoPago = metodoPago;
            objVenta.TipoComprobante = tipoComprobante;
            objVenta.IdUsuario = cajeroActual?.IdUsuario;
            objVenta.IdSocio = idClienteActual;

            // Transformamos el DataTable en la Lista de Objetos (Detalles)
            foreach (DataRow fila in carritoDetalles.Rows)
            {
                DetalleVenta item = new DetalleVenta();
                item.IdProducto = Convert.ToInt32(fila["IdProducto"]);
                item.Concepto = fila["Concepto"].ToString();
                item.Cantidad = Convert.ToInt32(fila["Cantidad"]);
                item.PrecioUnitario = Convert.ToDecimal(fila["PrecioUnitario"]);
                item.SubTotal = Convert.ToDecimal(fila["SubTotal"]);
                item.CodigoBarras = fila["CodigoBarras"].ToString();

                objVenta.Detalles.Add(item);
            }

            // LLAMAMOS A LA NUEVA CAPA DE NEGOCIO
            VentaNegocio negocioVenta = new VentaNegocio();
            string mensajeError;

            int idNuevaVenta = negocioVenta.RegistrarVentaCompleta(objVenta, out mensajeError);

            if (idNuevaVenta > 0)
            {
                // AUDITORÍA Y TICKETS
                string nombreCajero = cajeroActual != null ? cajeroActual.NombreCompleto : "Admin";
                GestorAuditoria.Registrar(nombreCajero, "Caja", "Venta/Cobro Confirmado", $"Se registró la operación N° {idNuevaVenta} por Gs. {totalAPagar:N0}.");

                MessageBox.Show($"¡Venta N° {idNuevaVenta} registrada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // --- GENERACIÓN DE PDF Y CORREO (SE MANTIENE INTACTO) ---
                AsuFit.Reportes.GeneradorPDF generador = new AsuFit.Reportes.GeneradorPDF();
                string nomCliente = string.IsNullOrWhiteSpace(txtNombreCliente.Text) ? "Cliente Ocasional" : txtNombreCliente.Text;
                string rucCliente = string.IsNullOrWhiteSpace(txtRucCliente.Text) ? "Sin RUC" : txtRucCliente.Text;
                string ciCliente = string.IsNullOrWhiteSpace(txtBusquedaCliente.Text) ? "Sin CI" : txtBusquedaCliente.Text;
                string numTicket = idNuevaVenta.ToString();

                decimal dineroRecibido = 0;
                decimal dineroVuelto = 0;

                if (metodoPago == "Efectivo")
                {
                    decimal.TryParse(txtMontoRecibido.Text, out dineroRecibido);
                    dineroVuelto = dineroRecibido - CarritoGlobal.TotalAPagar;
                    if (dineroVuelto < 0) dineroVuelto = 0;
                }

                string correoFiltro = string.IsNullOrWhiteSpace(correoClienteActual) ? "Sin correo" : correoClienteActual;

                if (tipoComprobante == "Factura")
                {
                    generador.GenerarFacturaLegalA4(CarritoGlobal.Detalles, CarritoGlobal.TotalAPagar, nomCliente, ciCliente, rucCliente, correoFiltro, metodoPago, dineroRecibido, dineroVuelto, nombreCajero, numTicket);
                }
                else
                {
                    generador.GenerarTicketTermico(CarritoGlobal.Detalles, CarritoGlobal.TotalAPagar, nomCliente, ciCliente, rucCliente, metodoPago, dineroRecibido, dineroVuelto, nombreCajero, numTicket);
                }

                string rutaDescargas = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string nombreArchivo = tipoComprobante == "Factura" ? $"Factura_Legal_{numTicket}.pdf" : $"Comprobante_Venta_{numTicket}.pdf";
                string rutaCompletaPdf = System.IO.Path.Combine(rutaDescargas, nombreArchivo);

                if (correoFiltro != "Sin correo" && correoFiltro.Contains("@"))
                {
                    EnviarCorreoConAdjunto(correoFiltro, rutaCompletaPdf, tipoComprobante ?? "Ticket", numTicket);
                }

                // --- LIMPIEZA FINAL DE LA NUBE Y DE LA PANTALLA DE INVENTARIO ---
                CarritoGlobal.LimpiarCarrito();

                frmPuntoVenta inventario = Application.OpenForms["frmPuntoVenta"] as frmPuntoVenta;
                if (inventario != null) inventario.LimpiarGrillaVisual();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error crítico al procesar la venta: \n" + mensajeError, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            txtBusquedaCliente_KeyPress(txtBusquedaCliente, new KeyPressEventArgs((char)Keys.Enter));
        }

        // =========================================================================================
        // --- NUEVO MÉTODO: ENVÍO DE CORREO AUTOMÁTICO DESDE LA CONFIGURACIÓN ---
        // =========================================================================================
        private void EnviarCorreoConAdjunto(string correoDestino, string rutaArchivo, string tipoComprobante, string nroComprobante)
        {
            try
            {
                string miCorreo = "";
                string miContrasenaApp = "";
                string nombreGym = "AsuFit GYM";

                // --- 1. LEER EL CORREO CORPORATIVO DESDE LA BASE DE DATOS ---
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    string query = "SELECT CorreoEmisor, ContrasenaCorreo, NombreGimnasio FROM Configuracion WHERE IdConfiguracion = 1";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    oConexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            miCorreo = reader["CorreoEmisor"].ToString();
                            miContrasenaApp = reader["ContrasenaCorreo"].ToString();
                            nombreGym = reader["NombreGimnasio"].ToString();
                        }
                    }
                }

                // Si no hay correo configurado, cancelamos el envío en silencio para no trabar la venta
                if (string.IsNullOrWhiteSpace(miCorreo) || string.IsNullOrWhiteSpace(miContrasenaApp))
                {
                    return;
                }

                // --- 2. ARMAMOS EL CORREO CON LOS DATOS DE LA EMPRESA ---
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(miCorreo, nombreGym);
                correo.To.Add(correoDestino);
                correo.Subject = $"Tu {tipoComprobante} N° {nroComprobante} - {nombreGym}";

                // --- 3. CÓDIGO ANTI-SPAM: USAMOS HTML EN LUGAR DE TEXTO PLANO ---
                string cuerpoHtml = $@"
                <div style='font-family: Arial, Helvetica, sans-serif; color: #333333; padding: 20px; border: 1px solid #eaeaea; border-radius: 10px; max-width: 600px;'>
                    <h2 style='color: #2E86C1;'>¡Hola!</h2>
                    <p>Adjuntamos tu comprobante (<strong>{tipoComprobante} N° {nroComprobante}</strong>) correspondiente a tu última transacción en <strong>{nombreGym}</strong>.</p>
                    <p>Si tienes alguna consulta sobre este documento, no dudes en responder directamente a este correo.</p>
                    <br>
                    <p>¡Gracias por tu preferencia y por seguir entrenando con nosotros!</p>
                    <hr style='border: none; border-top: 1px solid #eaeaea; margin: 20px 0;'>
                    <p style='font-size: 12px; color: #888888;'>
                        Saludos cordiales,<br>
                        <strong>El equipo de {nombreGym}</strong>
                    </p>
                </div>";

                correo.Body = cuerpoHtml;
                correo.IsBodyHtml = true; // Fundamental para que Google lo vea como un correo profesional

                // --- 4. ADJUNTAMOS EL PDF ---
                if (System.IO.File.Exists(rutaArchivo))
                {
                    Attachment adjunto = new Attachment(rutaArchivo);
                    correo.Attachments.Add(adjunto);
                }

                // --- 5. CONFIGURACIÓN DEL SERVIDOR Y ENVÍO ---
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;

                // Las dos líneas clave para evitar el bloqueo de red
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

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