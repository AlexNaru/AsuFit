using AsuFit.Reportes;
using AsuFit.Negocio;
using AsuFit.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmResumenArqueo : Form
    {
        private int idTurnoActual;
        private DateTime aperturaActual;
        private decimal diferenciaActual;

        // 1. CONSTRUCTOR ACTUALIZADO (A prueba de fallos)
        public frmResumenArqueo(int idTurno, string cajero, DateTime apertura, decimal trans, decimal efvo, decimal fondo, decimal gastos, decimal esperado, decimal contado, decimal diferencia)
        {
            InitializeComponent();
            idTurnoActual = idTurno;
            aperturaActual = apertura;
            diferenciaActual = diferencia;

            lblCajeroEncargado.Text = $"Cajero encargado: {cajero}";
            lblDatosApertura.Text = $"Apertura: {apertura.ToString("dd MMM yyyy, hh:mm tt")}";
            lblDatosCierre.Text = $"Cierre: {DateTime.Now.ToString("dd MMM yyyy, hh:mm tt")}";

            lblResumenTransferencia.Text = "Gs. " + trans.ToString("N0");
            lblResumenEfectivo.Text = "Gs. " + efvo.ToString("N0");
            lblResumenTotalIngresos.Text = "Gs. " + (trans + efvo).ToString("N0");

            lblResumenFondo.Text = "Gs. " + fondo.ToString("N0");
            lblResumenIngresosEfvo.Text = "Gs. " + efvo.ToString("N0");
            lblResumenGastosEfvo.Text = "Gs. " + gastos.ToString("N0");
            lblResumenEsperado.Text = "Gs. " + esperado.ToString("N0");
            lblResumenContado.Text = "Gs. " + contado.ToString("N0");
            lblResumenDiferencia.Text = "Gs. " + diferencia.ToString("N0");

            // Llamamos a las configuraciones de interfaz directamente desde el constructor
            ConfigurarTemaYEscala();
            CentrarFormulario();
        }

        #region ESTILOS VISUALES Y ESCALADO
        private void ConfigurarTemaYEscala()
        {
            // BLOQUEO DE REDIMENSIONAMIENTO Y PANTALLA COMPLETA
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            float escalaActual = Properties.Settings.Default.EscalaInterfaz;
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            this.Scale(new SizeF(escalaActual, escalaActual));
            AjustarFuentesRecursivo(this, fuenteActual);

            this.BackColor = Color.FromArgb(25, 28, 35);
            AplicarTemaOscuroRecursivo(this);

            if (diferenciaActual < 0) lblResumenDiferencia.ForeColor = Color.LightCoral;
            else lblResumenDiferencia.ForeColor = Color.MediumSeaGreen;

            if (btnAceptarEnviar != null)
            {
                btnAceptarEnviar.BackColor = Color.FromArgb(0, 229, 255);
                btnAceptarEnviar.ForeColor = Color.Black;
                btnAceptarEnviar.FlatStyle = FlatStyle.Flat;
                btnAceptarEnviar.FlatAppearance.BorderSize = 0;
            }
        }

        private void AplicarTemaOscuroRecursivo(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Panel || c is GroupBox)
                {
                    c.BackColor = Color.FromArgb(35, 39, 47);
                    c.ForeColor = Color.White;
                }
                else if (c is Label lbl && lbl.Name != "lblResumenDiferencia")
                {
                    lbl.ForeColor = Color.White;
                    lbl.BackColor = Color.Transparent;
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c);
            }
        }

        private void AjustarFuentesRecursivo(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Label || c is Button)
                {
                    c.Font = new Font("Segoe UI", fuente, c.Font.Style);
                }
                if (c.HasChildren) AjustarFuentesRecursivo(c, fuente);
            }
        }

        private void CentrarFormulario()
        {
            Form padre = Application.OpenForms["frmDashboard"];
            if (padre != null)
            {
                Control[] controles = padre.Controls.Find("pnlContenedor", true);
                if (controles.Length > 0)
                {
                    Control contenedor = controles[0];
                    Point posicionAbsoluta = contenedor.PointToScreen(Point.Empty);

                    this.StartPosition = FormStartPosition.Manual;
                    int x = posicionAbsoluta.X + (contenedor.Width - this.Width) / 2;
                    int y = posicionAbsoluta.Y + (contenedor.Height - this.Height) / 2;
                    this.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);
                    return;
                }
            }
            this.CenterToScreen();
        }
        #endregion

        // 3. ACCIONES Y REPORTES
        private void btnAceptarEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal trans = Convert.ToDecimal(lblResumenTransferencia.Text.Replace("Gs. ", "").Replace(".", ""));
                decimal efvo = Convert.ToDecimal(lblResumenEfectivo.Text.Replace("Gs. ", "").Replace(".", ""));
                decimal fondo = Convert.ToDecimal(lblResumenFondo.Text.Replace("Gs. ", "").Replace(".", ""));
                decimal gastos = Convert.ToDecimal(lblResumenGastosEfvo.Text.Replace("Gs. ", "").Replace(".", ""));
                decimal esperado = Convert.ToDecimal(lblResumenEsperado.Text.Replace("Gs. ", "").Replace(".", ""));
                decimal contado = Convert.ToDecimal(lblResumenContado.Text.Replace("Gs. ", "").Replace(".", ""));
                decimal diferencia = Convert.ToDecimal(lblResumenDiferencia.Text.Replace("Gs. ", "").Replace(".", ""));

                // La capa de reportes hace el PDF
                GeneradorPDF generador = new GeneradorPDF();
                string rutaPDFGenerado = generador.GenerarTicketArqueo(
                    idTurnoActual,
                    lblCajeroEncargado.Text.Replace("Cajero encargado: ", ""),
                    aperturaActual,
                    trans, efvo, fondo, gastos, esperado, contado, diferencia
                );

                // Llamada completamente limpia
                EnviarCorreoArqueo(rutaPDFGenerado);

                MessageBox.Show("¡El turno se ha cerrado completamente y el reporte fue guardado y enviado!", "Cierre Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se cerró la caja, pero hubo un problema al generar/enviar el comprobante: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // --- SOLUCIÓN ARQUITECTÓNICA ---
        private void EnviarCorreoArqueo(string rutaArchivoAdjunto)
        {
            try
            {
                // 1. Instanciamos la capa de Negocio
                ConfiguracionNegocio negocioConfig = new ConfiguracionNegocio();

                // 2. Le pedimos a Negocio que nos traiga la configuración de la BD
                Configuracion config = negocioConfig.ObtenerConfiguracion();

                // Si no hay correo configurado, simplemente no enviamos nada
                if (string.IsNullOrWhiteSpace(config.CorreoEmisor)) return;

                // 3. Preparamos el diseño del correo (Esto sí es tarea de la capa de Presentación)
                string asunto = $"Cierre de Caja N° {idTurnoActual} - {config.NombreGimnasio}";
                string cuerpoHtml = $@"
                <div style='font-family: Arial; color: #333; padding: 20px; border: 1px solid #eaeaea;'>
                    <h2 style='color: #2E86C1;'>Reporte de Cierre de Caja</h2>
                    <p>Se ha registrado un nuevo cierre de turno. Adjuntamos el reporte detallado en PDF.</p>
                    <p><strong>Cajero:</strong> {lblCajeroEncargado.Text.Replace("Cajero encargado: ", "")}</p>
                    <p><strong>Diferencia declarada:</strong> {lblResumenDiferencia.Text}</p>
                    <hr>
                    <p style='font-size: 12px;'>Sistema de Gestión AsuFit</p>
                </div>";

                // 4. Le ordenamos a la capa de Negocio que envíe el correo (Delegamos la responsabilidad)
                negocioConfig.EnviarCorreoConAdjunto(config.CorreoEmisor, asunto, cuerpoHtml, rutaArchivoAdjunto);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo enviar el correo: " + ex.Message);
            }
        }
    }
}