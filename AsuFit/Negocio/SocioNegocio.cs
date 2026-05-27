using AsuFit.Datos;
using AsuFit.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace AsuFit.Negocio
{
    // Orquesta las reglas de negocio para la gestión y control de los socios.
    public class SocioNegocio
    {
        private SocioDatos objSocioDatos = new SocioDatos();

        #region CONSULTAS Y BÚSQUEDAS
        // Obtiene la lista de socios filtrada por su estado actual.
        public DataTable ListarSocios(string estado)
        {
            return objSocioDatos.ListarSocios(estado);
        }

        // Recupera el listado de socios cuyas membresías se encuentran vencidas.
        public List<Socio> ListarVencidos()
        {
            SocioDatos datos = new SocioDatos();
            return datos.ListarVencidos();
        }

        // Verifica la existencia de un número de documento para prevenir duplicados.
        public bool ExisteCedula(string cedula, int idSocioActual)
        {
            return objSocioDatos.ExisteCedula(cedula, idSocioActual);
        }

        // Busca y retorna la información de un socio utilizando su documento de identidad.
        public Socio BuscarSocioPorCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula)) return null;
            return objSocioDatos.BuscarSocioPorCedula(cedula);
        }

        // Busca y retorna la información de un socio utilizando su identificador interno.
        public Socio BuscarSocioPorId(int idSocio)
        {
            return objSocioDatos.BuscarSocioPorId(idSocio);
        }
        #endregion

        #region OPERACIONES Y VALIDACIONES
        // Valida y procesa el registro de un nuevo socio en el sistema.
        public bool RegistrarSocio(Socio objSocio, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(objSocio.Cedula))
            {
                mensaje = "El número de Cédula es obligatorio para registrar al socio.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(objSocio.Nombre) || string.IsNullOrWhiteSpace(objSocio.Apellido))
            {
                mensaje = "El nombre y el apellido no pueden estar vacíos.";
                return false;
            }

            if (objSocio.IdPlan <= 0)
            {
                mensaje = "Debe seleccionar un plan válido para el socio.";
                return false;
            }

            bool respuestaBD = objSocioDatos.RegistrarSocio(objSocio);
            if (!respuestaBD)
            {
                mensaje = "Error al conectar o guardar en la base de datos.";
            }

            return respuestaBD;
        }

        // Procesa el registro inicial del socio y retorna el identificador generado.
        public int InsertarSocioYObtenerId(Socio nuevoSocio, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(nuevoSocio.Cedula) || string.IsNullOrWhiteSpace(nuevoSocio.Nombre))
            {
                mensaje = "La Cédula y el Nombre son obligatorios.";
                return 0;
            }

            return objSocioDatos.InsertarSocioYObtenerId(nuevoSocio, out mensaje);
        }

        // Valida y procesa la actualización de datos de un socio existente.
        public bool EditarSocio(Socio obj)
        {
            return objSocioDatos.EditarSocio(obj);
        }

        // Modifica el estado lógico de un socio en el sistema.
        public bool CambiarEstadoSocio(int idSocio, string nuevoEstado)
        {
            return objSocioDatos.CambiarEstadoSocio(idSocio, nuevoEstado);
        }

        // Elimina físicamente el registro de un socio de la base de datos.
        public bool EliminarSocio(int idSocio)
        {
            return objSocioDatos.EliminarSocio(idSocio);
        }

        // Extiende la vigencia del plan asociado al socio.
        public bool RenovarMembresiaSocio(int idSocio, int diasPlan)
        {
            return objSocioDatos.RenovarMembresiaSocio(idSocio, diasPlan);
        }

        // Registra una nueva marca de asistencia para el socio indicado.
        public void RegistrarAsistencia(int idSocio)
        {
            objSocioDatos.RegistrarAsistencia(idSocio);
        }
        #endregion

        #region NOTIFICACIONES AUTOMATIZADAS
        // Ejecuta el proceso en segundo plano para enviar alertas de vencimiento por correo electrónico.
        public void ProcesarEnvioCorreosVencimiento()
        {
            try
            {
                DataTable dtConfig = objSocioDatos.ObtenerConfiguracionCorreo();
                if (dtConfig.Rows.Count == 0) return;

                string correoGym = dtConfig.Rows[0]["CorreoEmisor"].ToString();
                string passGym = dtConfig.Rows[0]["ContrasenaCorreo"].ToString();

                if (string.IsNullOrEmpty(correoGym) || string.IsNullOrEmpty(passGym)) return;

                int avisoLejano = 7;
                int avisoCercano = 1;
                if (dtConfig.Rows[0]["DiasAviso1"] != DBNull.Value) avisoLejano = Convert.ToInt32(dtConfig.Rows[0]["DiasAviso1"]);
                if (dtConfig.Rows[0]["DiasAviso2"] != DBNull.Value) avisoCercano = Convert.ToInt32(dtConfig.Rows[0]["DiasAviso2"]);

                DataTable dtAvisos = objSocioDatos.ObtenerSociosParaAvisoCorreo(avisoCercano, avisoLejano);

                if (dtAvisos.Rows.Count > 0)
                {
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                    {
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(correoGym, passGym);

                        foreach (DataRow row in dtAvisos.Rows)
                        {
                            string emailDestino = row["Email"].ToString();
                            string nombre = row["Nombre"].ToString();
                            int dias = Convert.ToInt32(row["DiasRestantes"]);
                            int idSocio = Convert.ToInt32(row["IdSocio"]);
                            DateTime fechaVence = Convert.ToDateTime(row["FechaVencimiento"]);

                            MailMessage correo = new MailMessage();
                            correo.From = new MailAddress(correoGym, "AsuFit GYM");
                            correo.To.Add(emailDestino);

                            string diaTexto = (dias == 0) ? "HOY" : ((dias == avisoCercano) ? "MAÑANA" : $"en {dias} días");
                            correo.Subject = $"Aviso de Vencimiento AsuFit - Tu plan vence {diaTexto}";

                            System.Globalization.CultureInfo idiomaEspanol = new System.Globalization.CultureInfo("es-ES");
                            string fechaNatural = fechaVence.ToString("dddd d 'de' MMMM", idiomaEspanol);

                            correo.Body = $"Hola {nombre},\n\nTe recordamos que tu membresía en AsuFit Gym vence el {fechaNatural}.\n\n¡Te esperamos en la recepción para renovar y seguir entrenando con todo!\n\nSaludos,\nEl equipo de AsuFit Gym";
                            correo.IsBodyHtml = false;

                            try
                            {
                                smtp.Send(correo);
                                objSocioDatos.RegistrarAvisoEnviado(idSocio); // Llamada limpia a Datos
                            }
                            catch
                            {
                                continue; // Omite errores de envío individuales silenciosamente
                            }
                        }
                    }
                }
            }
            catch
            {
                // Silenciado intencionalmente para evitar interrupciones de UI en procesos de segundo plano
            }
        }
        #endregion
    }
}