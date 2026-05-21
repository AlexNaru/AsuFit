using AsuFit.Datos;
using AsuFit.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmConfiguracion : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private Usuario usuarioActual;

        public frmConfiguracion(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
            CargarConfiguracion();
        }

        private void CargarConfiguracion()
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = "SELECT * FROM Configuracion WHERE IdConfiguracion = 1";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    oConexion.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtNombreGimnasio.Text = reader["NombreGimnasio"].ToString();
                        txtRUC.Text = reader["RUC"].ToString();
                        txtDireccion.Text = reader["Direccion"].ToString();
                        txtTelefono.Text = reader["Telefono"].ToString();

                        if (reader["Logo"] != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])reader["Logo"];
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                picLogo.Image = Image.FromStream(ms);
                            }
                        }

                        txtCorreoEmisor.Text = reader["CorreoEmisor"].ToString();
                        txtContrasenaCorreo.Text = reader["ContrasenaCorreo"].ToString();
                        nudDiasAviso1.Value = Convert.ToDecimal(reader["DiasAviso1"]);
                        nudDiasAviso2.Value = Convert.ToDecimal(reader["DiasAviso2"]);

                        if (reader["RutaBackup"] != DBNull.Value)
                        {
                            txtRutaDestino.Text = reader["RutaBackup"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la configuración: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region 3. PESTAÑA: EMPRESA Y DATOS GENERALES
        private void btnSubirLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleccionar Logo del Gimnasio";
            ofd.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picLogo.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"UPDATE Configuracion SET 
                             NombreGimnasio = @Nombre, 
                             RUC = @RUC, 
                             Direccion = @Direccion, 
                             Telefono = @Telefono, 
                             CorreoEmisor = @Correo, 
                             ContrasenaCorreo = @Contrasena, 
                             DiasAviso1 = @Dias1, 
                             DiasAviso2 = @Dias2,
                             Logo = @Logo,
                             RutaBackup = @RutaBackup
                             WHERE IdConfiguracion = 1";

                    SqlCommand cmd = new SqlCommand(query, oConexion);

                    cmd.Parameters.AddWithValue("@Nombre", txtNombreGimnasio.Text);
                    cmd.Parameters.AddWithValue("@RUC", txtRUC.Text);
                    cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    cmd.Parameters.AddWithValue("@Correo", txtCorreoEmisor.Text);
                    cmd.Parameters.AddWithValue("@Contrasena", txtContrasenaCorreo.Text);
                    cmd.Parameters.AddWithValue("@Dias1", nudDiasAviso1.Value);
                    cmd.Parameters.AddWithValue("@Dias2", nudDiasAviso2.Value);
                    cmd.Parameters.AddWithValue("@RutaBackup", txtRutaDestino.Text);

                    if (picLogo.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            picLogo.Image.Save(ms, picLogo.Image.RawFormat);
                            cmd.Parameters.AddWithValue("@Logo", ms.ToArray());
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("@Logo", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Configuración", "Actualización General", "Se modificaron los datos generales de la empresa o el logo.");

                    MessageBox.Show("¡Configuración guardada correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region 4. PESTAÑA: NOTIFICACIONES Y ALERTAS
        private void btnPruebaCorreo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCorreoEmisor.Text) || string.IsNullOrWhiteSpace(txtContrasenaCorreo.Text))
            {
                MessageBox.Show("Por favor, completá el correo y la contraseña antes de hacer la prueba.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(txtCorreoEmisor.Text.Trim(), txtContrasenaCorreo.Text.Trim());

                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(txtCorreoEmisor.Text.Trim(), "Sistema AsuFit");
                correo.To.Add(txtCorreoEmisor.Text.Trim());
                correo.Subject = "✅ Correo de Prueba - Configuración AsuFit";
                correo.Body = "¡Felicidades!\n\nSi estás leyendo esto, significa que el motor de correos de tu sistema AsuFit está configurado correctamente y listo para avisar automáticamente a los socios sobre sus vencimientos.\n\nSaludos.";
                correo.IsBodyHtml = false;

                smtp.Send(correo);
                Cursor.Current = Cursors.Default;

                MessageBox.Show("¡Conexión exitosa!\n\nTe hemos enviado un correo de prueba a tu bandeja de entrada. Por favor, revisalo para confirmar.", "Prueba Superada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Error al conectar con el correo. Verifica que tu contraseña de aplicación sea correcta y no tenga espacios de más.\n\nDetalle técnico: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarNotificaciones_Click(object sender, EventArgs e)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"UPDATE Configuracion SET 
                                     CorreoEmisor = @Correo, 
                                     ContrasenaCorreo = @Contrasena, 
                                     DiasAviso1 = @Dias1, 
                                     DiasAviso2 = @Dias2
                                     WHERE IdConfiguracion = 1";

                    SqlCommand cmd = new SqlCommand(query, oConexion);

                    cmd.Parameters.AddWithValue("@Correo", txtCorreoEmisor.Text.Trim());
                    cmd.Parameters.AddWithValue("@Contrasena", txtContrasenaCorreo.Text.Trim());
                    cmd.Parameters.AddWithValue("@Dias1", nudDiasAviso1.Value);
                    cmd.Parameters.AddWithValue("@Dias2", nudDiasAviso2.Value);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Configuración", "Actualización", "Se cambiaron los parámetros de envío de correos.");
                    MessageBox.Show("¡Configuración de correos y avisos guardada correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar las notificaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelarNotificaciones_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas cancelar? Se perderán los cambios no guardados.", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                CargarConfiguracion();
            }
        }
        #endregion

        #region 5. PESTAÑA: SISTEMA Y RESPALDOS (BACKUP)
        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Seleccioná la carpeta donde se guardará la copia de seguridad.";
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtRutaDestino.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnGenerarBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaDestino.Text))
            {
                MessageBox.Show("Por favor, seleccioná una carpeta de destino usando el botón Examinar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fechaHoy = DateTime.Now.ToString("yyyyMMdd_HHmm");
            string nombreArchivo = $"AsuFit_Backup_{fechaHoy}.bak";
            string rutaCompleta = Path.Combine(txtRutaDestino.Text, nombreArchivo);

            try
            {
                string query = $"BACKUP DATABASE AsuFitDB TO DISK = '{rutaCompleta}' WITH FORMAT, MEDIANAME = 'AsuFit_Backups', NAME = 'Respaldo Completo AsuFit'";

                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }

                GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Sistema", "Backup de Base de Datos", $"Se generó una copia de seguridad en: {rutaCompleta}");

                lblUltimoRespaldo.Text = "Último respaldo realizado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " hs";
                MessageBox.Show($"¡Copia de seguridad generada con éxito!\n\nSe guardó en:\n{rutaCompleta}", "Respaldo Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar generar la copia de seguridad:\n\n" + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}