using AsuFit.Datos;
using AsuFit.Entidades;
using System;
using System.Net;
using System.Net.Mail;

namespace AsuFit.Negocio
{
    public class ConfiguracionNegocio
    {
        private ConfiguracionDatos datos = new ConfiguracionDatos();

        public Configuracion ObtenerConfiguracion() { return datos.ObtenerConfiguracion(); }

        public bool ActualizarDatosGenerales(Configuracion obj) { return datos.ActualizarDatosGenerales(obj); }

        public bool ActualizarNotificaciones(Configuracion obj) { return datos.ActualizarNotificaciones(obj); }

        public void GenerarBackup(string rutaCompleta) { datos.GenerarBackup(rutaCompleta); }

        // Motor centralizado de prueba de correos (Usado por frmConfiguracion)
        public void ProbarConexionCorreo(string correoEmisor, string contrasena)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(correoEmisor, contrasena);

                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(correoEmisor, "Sistema AsuFit");
                correo.To.Add(correoEmisor);
                correo.Subject = "✅ Correo de Prueba - Configuración AsuFit";
                correo.Body = "¡Felicidades!\n\nSi estás leyendo esto, significa que el motor de correos de tu sistema AsuFit está configurado correctamente.";
                correo.IsBodyHtml = false;

                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // EL MÉTODO QUE FALTABA: Motor para enviar correos con PDF (Usado por frmResumenArqueo)
        public bool EnviarCorreoConAdjunto(string destinatario, string asunto, string cuerpoHtml, string rutaAdjunto)
        {
            try
            {
                Configuracion config = ObtenerConfiguracion();

                if (string.IsNullOrWhiteSpace(config.CorreoEmisor) || string.IsNullOrWhiteSpace(config.ContrasenaCorreo))
                {
                    throw new Exception("Las credenciales del correo emisor no están configuradas en el sistema.");
                }

                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(config.CorreoEmisor, config.NombreGimnasio);
                correo.To.Add(destinatario);
                correo.Subject = asunto;
                correo.Body = cuerpoHtml;
                correo.IsBodyHtml = true;

                if (!string.IsNullOrWhiteSpace(rutaAdjunto) && System.IO.File.Exists(rutaAdjunto))
                {
                    correo.Attachments.Add(new Attachment(rutaAdjunto));
                }

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(config.CorreoEmisor, config.ContrasenaCorreo);

                smtp.Send(correo);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}