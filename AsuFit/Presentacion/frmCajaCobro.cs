using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmCajaCobro : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private decimal totalAPagar = 0;
        private string correoClienteActual = "";
        private int? idClienteActual = null;
        private bool procesandoPago = false; // Bandera de seguridad transaccional

        private DataTable carritoDetalles;
        private Usuario cajeroActual;

        // Inicializa el contexto de cobro absorbiendo los datos del carrito global.
        public frmCajaCobro(Usuario usuarioCajero)
        {
            InitializeComponent();

            float escala = Properties.Settings.Default.EscalaInterfaz;
            this.Scale(new SizeF(escala, escala));

            this.StartPosition = FormStartPosition.CenterScreen;

            ConfigurarTemaOscuro();

            totalAPagar = CarritoGlobal.TotalAPagar;
            carritoDetalles = CarritoGlobal.Detalles;
            cajeroActual = usuarioCajero;

            lblTotalCobrar.Text = "Total a cobrar: Gs. " + totalAPagar.ToString("N0");

            if (cajeroActual != null)
            {
                txtCajero.Text = $" {cajeroActual.Rol} - {cajeroActual.NombreCompleto}";
            }
            else
            {
                txtCajero.Text = "Cajero: Administrador (No detectado)";
            }

            // El cursor rebota hacia el buscador de clientes
            txtCajero.Enter += delegate { this.ActiveControl = txtBusquedaCliente; };

            cmbTipoComprobante.SelectedItem = "Ticket";
            cmbMetodoPago.SelectedItem = "Efectivo";

            // Si la venta viene desde el Registro de Socios, precarga los datos.
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

            // Enlace absoluto de inicialización tardía para blindaje financiero
            this.Load += new EventHandler(frmCajaCobro_Load);
        }
        #endregion

        #region 2. ESTILOS VISUALES Y SEGURIDAD UI
        private void frmCajaCobro_Load(object sender, EventArgs e)
        {
            // Mitigación visual: Quita el sombreado azul nativo de Windows al interactuar
            cmbMetodoPago.DropDownClosed += DesmarcarCombo_Interaccion;
            cmbMetodoPago.SelectedIndexChanged += DesmarcarCombo_Interaccion;
            cmbTipoComprobante.DropDownClosed += DesmarcarCombo_Interaccion;
            cmbTipoComprobante.SelectedIndexChanged += DesmarcarCombo_Interaccion;

            SuscribirFiltrosDeSeguridad();
        }

        // Libera el foco del control activo de forma asíncrona para eliminar selecciones residuales de la interfaz.
        private void DesmarcarCombo_Interaccion(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => this.ActiveControl = null));
        }

        // Aplica la paleta corporativa y rescata la legibilidad de controles deshabilitados.
        private void ConfigurarTemaOscuro()
        {
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            this.BackColor = Color.FromArgb(25, 28, 35);
            AplicarTemaOscuroRecursivo(this, fuenteActual);

            if (lblTotalCobrar != null)
            {
                lblTotalCobrar.Font = new Font("Segoe UI", fuenteActual + 4f, FontStyle.Bold);
                lblTotalCobrar.ForeColor = Color.FromArgb(0, 229, 255);
            }
        }

        private void AplicarTemaOscuroRecursivo(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Panel || c is GroupBox)
                {
                    c.BackColor = Color.FromArgb(35, 39, 47);
                    c.ForeColor = Color.White;
                }
                else if (c is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                    lbl.Font = new Font("Segoe UI", fuente, lbl.Font.Style);
                }
                else if (c is TextBox txt)
                {
                    if (!txt.Enabled)
                    {
                        txt.Enabled = true;
                        txt.ReadOnly = true;
                    }

                    if (txt.ReadOnly)
                    {
                        txt.BackColor = Color.FromArgb(35, 39, 47);
                        txt.ForeColor = Color.White;
                    }
                    else
                    {
                        txt.BackColor = Color.FromArgb(50, 55, 65);
                        txt.ForeColor = Color.White;
                    }

                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
                }
                else if (c is ComboBox cmb)
                {
                    cmb.BackColor = Color.FromArgb(50, 55, 65);
                    cmb.ForeColor = Color.White;
                    cmb.FlatStyle = FlatStyle.Flat;
                    cmb.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
                }
                else if (c is Button btn)
                {
                    btn.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;

                    if (btn.Name.Contains("Cancelar"))
                    {
                        btn.BackColor = Color.FromArgb(50, 55, 65);
                        btn.ForeColor = Color.White;
                    }
                    else
                    {
                        btn.BackColor = Color.FromArgb(0, 229, 255);
                        btn.ForeColor = Color.Black;
                    }
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c, fuente);
            }
        }

        private void SuscribirFiltrosDeSeguridad()
        {
            ContextMenuStrip menuVacio = new ContextMenuStrip();

            foreach (Control contenedor in this.Controls)
            {
                AsignarBloqueosRecursivo(contenedor, menuVacio);
            }
        }

        private void AsignarBloqueosRecursivo(Control contenedor, ContextMenuStrip menuVacio)
        {
            if (contenedor is TextBox txt)
            {
                txt.KeyDown += BloquearPegado_KeyDown;
                txt.ContextMenuStrip = menuVacio;
                txt.ShortcutsEnabled = false; // Desactiva Ctrl+V nativo en toda la pantalla
            }

            foreach (Control hijo in contenedor.Controls)
            {
                AsignarBloqueosRecursivo(hijo, menuVacio);
            }
        }

        private void BloquearPegado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
            }
        }
        #endregion

        #region 3. EVENTOS DE INTERFAZ Y BÚSQUEDA
        // Conmuta la captura manual de efectivo si el método de pago es digital.
        private void cmbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMetodoPago.SelectedItem != null)
            {
                string metodo = cmbMetodoPago.SelectedItem.ToString();

                if (metodo != "Efectivo")
                {
                    txtMontoRecibido.Enabled = true;
                    txtMontoRecibido.ReadOnly = true;
                    txtMontoRecibido.BackColor = Color.FromArgb(35, 39, 47);
                    txtMontoRecibido.ForeColor = Color.White;
                    txtMontoRecibido.Text = totalAPagar.ToString();
                    txtVuelto.Text = "Gs. 0";
                }
                else
                {
                    txtMontoRecibido.Enabled = true;
                    txtMontoRecibido.ReadOnly = false;
                    txtMontoRecibido.BackColor = Color.FromArgb(50, 55, 65);
                    txtMontoRecibido.ForeColor = Color.White;
                    txtMontoRecibido.Text = "";
                    txtVuelto.Text = "Gs. 0";
                }
            }
        }

        // Ejecuta la búsqueda de cliente al presionar Enter y bloquea la entrada de caracteres alfabéticos.
        private void txtBusquedaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnBuscarCliente.PerformClick();
            }
            else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        // Consulta en la base de datos la existencia del cliente y formatea los resultados.
        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusquedaCliente.Text)) return;

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

        // Calcula el cambio a devolver en tiempo real según la captura de efectivo.
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

        // Descarta la venta en curso y limpia el estado global del sistema.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Estás seguro de que deseas cancelar esta operación y vaciar el carrito?", "Cancelar Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                CarritoGlobal.LimpiarCarrito();

                frmPuntoVenta inventario = Application.OpenForms["frmPuntoVenta"] as frmPuntoVenta;
                if (inventario != null) inventario.LimpiarGrillaVisual();

                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        public void ActualizarPantallaDesdeCarrito()
        {
            totalAPagar = CarritoGlobal.TotalAPagar;
            lblTotalCobrar.Text = "Gs. " + totalAPagar.ToString("N0");

            txtMontoRecibido_TextChanged(null, null);
        }

        // Minimiza la caja y redirige al usuario al catálogo para seguir añadiendo ítems.
        private void btnAgregarMasCosas_Click(object sender, EventArgs e)
        {
            this.Close();

            
        }
        #endregion

        #region 4. PROCESAMIENTO DE VENTA Y CORREO
        // Consolida la transacción, actualiza inventario, genera PDFs y envía los correos pertinentes.
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (procesandoPago) return; // Prevención contra el síndrome de múltiple clic

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

            // Bloqueo de UI para evitar cobros duplicados en caso de lentitud de red
            procesandoPago = true;
            btnConfirmar.Enabled = false;
            btnConfirmar.Text = "PROCESANDO...";
            Application.DoEvents();

            try
            {
                Venta objVenta = new Venta();
                objVenta.Total = totalAPagar;
                objVenta.MetodoPago = metodoPago;
                objVenta.TipoComprobante = tipoComprobante;
                objVenta.IdUsuario = cajeroActual?.IdUsuario;
                objVenta.IdSocio = idClienteActual;

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

                VentaNegocio negocioVenta = new VentaNegocio();
                string mensajeError;

                int idNuevaVenta = negocioVenta.RegistrarVentaCompleta(objVenta, out mensajeError);

                if (idNuevaVenta > 0)
                {
                    string nombreCajero = cajeroActual != null ? cajeroActual.NombreCompleto : "Admin";
                    GestorAuditoria.Registrar(nombreCajero, "Caja", "Venta Confirmada", $"Se registró la operación N° {idNuevaVenta} por Gs. {totalAPagar:N0}.");

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

                    CarritoGlobal.LimpiarCarrito();

                    frmPuntoVenta inventario = Application.OpenForms["frmPuntoVenta"] as frmPuntoVenta;
                    if (inventario != null) inventario.LimpiarGrillaVisual();

                    MessageBox.Show($"¡Venta N° {idNuevaVenta} registrada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error crítico al procesar la venta: \n" + mensajeError, "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RevertirBotonConfirmar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RevertirBotonConfirmar();
            }
        }

        // Restaura el botón de confirmación en caso de fallo transaccional.
        private void RevertirBotonConfirmar()
        {
            procesandoPago = false;
            btnConfirmar.Enabled = true;
            btnConfirmar.Text = "CONFIRMAR E IMPRIMIR";
        }

        private void EnviarCorreoConAdjunto(string correoDestino, string rutaArchivo, string tipoComprobante, string nroComprobante)
        {
            try
            {
                ConfiguracionNegocio negocioConfig = new ConfiguracionNegocio();
                string nombreGym = "AsuFit GYM";

                Configuracion config = negocioConfig.ObtenerConfiguracion();
                if (config != null && !string.IsNullOrWhiteSpace(config.NombreGimnasio))
                {
                    nombreGym = config.NombreGimnasio;
                }

                string asunto = $"Tu {tipoComprobante} N° {nroComprobante} - {nombreGym}";

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

                negocioConfig.EnviarCorreoConAdjunto(correoDestino, asunto, cuerpoHtml, rutaArchivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("La venta se registró correctamente, pero hubo un problema al enviar el correo automático: " + ex.Message, "Aviso de Correo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}