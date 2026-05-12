using AsuFit.Reportes;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmResumenArqueo : Form
    {
        private int idTurnoActual;
        private DateTime aperturaActual; // Variable para enviar al PDF

        // 1. CONSTRUCTOR ACTUALIZADO
        public frmResumenArqueo(int idTurno, string cajero, DateTime apertura, decimal trans, decimal efvo, decimal fondo, decimal gastos, decimal esperado, decimal contado, decimal diferencia)
        {
            InitializeComponent();
            idTurnoActual = idTurno;
            aperturaActual = apertura;

            // Rellenamos las fechas y el cajero
            lblCajeroEncargado.Text = $"Cajero encargado: {cajero}";
            lblDatosApertura.Text = $"Apertura: {apertura.ToString("dd MMM yyyy, hh:mm tt")}";
            lblDatosCierre.Text = $"Cierre: {DateTime.Now.ToString("dd MMM yyyy, hh:mm tt")}";

            // Rellenamos agregando "Gs. "
            lblResumenTransferencia.Text = "Gs. " + trans.ToString("N0");
            lblResumenEfectivo.Text = "Gs. " + efvo.ToString("N0");
            lblResumenTotalIngresos.Text = "Gs. " + (trans + efvo).ToString("N0");

            lblResumenFondo.Text = "Gs. " + fondo.ToString("N0");
            lblResumenIngresosEfvo.Text = "Gs. " + efvo.ToString("N0");
            lblResumenGastosEfvo.Text = "Gs. " + gastos.ToString("N0");
            lblResumenEsperado.Text = "Gs. " + esperado.ToString("N0");
            lblResumenContado.Text = "Gs. " + contado.ToString("N0");
            lblResumenDiferencia.Text = "Gs. " + diferencia.ToString("N0");

            // Si falta plata, pintamos de rojo. Si sobra o cuadra perfecto a 0, de verde.
            if (diferencia < 0)
            {
                lblResumenDiferencia.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblResumenDiferencia.ForeColor = System.Drawing.Color.MediumSeaGreen;
            }
        }

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

                // Generamos el PDF pasando también la fecha de apertura
                GeneradorPDF generador = new GeneradorPDF();
                string rutaPDFGenerado = generador.GenerarTicketArqueo(
                    idTurnoActual,
                    lblCajeroEncargado.Text.Replace("Cajero encargado: ", ""),
                    aperturaActual,
                    trans, efvo, fondo, gastos, esperado, contado, diferencia
                );

                // Enviar por correo corporativo
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

        private void EnviarCorreoArqueo(string rutaArchivoAdjunto)
        {
            string miCorreo = "";
            string miContrasenaApp = "";
            string nombreGym = "AsuFit GYM";

            // LEER DESDE LA BASE DE DATOS
            using (SqlConnection oConexion = AsuFit.Datos.Conexion.ObtenerConexion())
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

            if (string.IsNullOrWhiteSpace(miCorreo) || string.IsNullOrWhiteSpace(miContrasenaApp)) return;

            MailMessage correo = new MailMessage();
            correo.From = new MailAddress(miCorreo, nombreGym);
            correo.To.Add(miCorreo); // Se auto-envía al correo del gimnasio
            correo.Subject = $"Cierre de Caja N° {idTurnoActual} - {nombreGym}";

            string cuerpoHtml = $@"
            <div style='font-family: Arial; color: #333; padding: 20px; border: 1px solid #eaeaea;'>
                <h2 style='color: #2E86C1;'>Reporte de Cierre de Caja</h2>
                <p>Se ha registrado un nuevo cierre de turno. Adjuntamos el reporte detallado en PDF.</p>
                <p><strong>Cajero:</strong> {lblCajeroEncargado.Text.Replace("Cajero encargado: ", "")}</p>
                <p><strong>Diferencia declarada:</strong> {lblResumenDiferencia.Text}</p>
                <hr>
                <p style='font-size: 12px;'>Sistema de Gestión AsuFit</p>
            </div>";

            correo.Body = cuerpoHtml;
            correo.IsBodyHtml = true;

            if (System.IO.File.Exists(rutaArchivoAdjunto))
            {
                correo.Attachments.Add(new Attachment(rutaArchivoAdjunto));
            }

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(miCorreo, miContrasenaApp);

            smtp.Send(correo);
        }
    }
}