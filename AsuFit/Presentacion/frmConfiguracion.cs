using AsuFit.Datos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace AsuFit.Presentacion
{
    public partial class frmConfiguracion : Form
    {
        public frmConfiguracion()
        {
            InitializeComponent();
        }

        // 1. EVENTO LOAD: Se ejecuta al abrir la pantalla de configuración
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
                        // Cargar Empresa
                        txtNombreGimnasio.Text = reader["NombreGimnasio"].ToString();
                        txtRUC.Text = reader["RUC"].ToString();
                        txtDireccion.Text = reader["Direccion"].ToString();
                        txtTelefono.Text = reader["Telefono"].ToString();

                        // Cargar Logo si existe en la BD
                        if (reader["Logo"] != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])reader["Logo"];
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                picLogo.Image = Image.FromStream(ms);
                            }
                        }

                        // Cargar Notificaciones
                        txtCorreoEmisor.Text = reader["CorreoEmisor"].ToString();
                        txtContrasenaCorreo.Text = reader["ContrasenaCorreo"].ToString();
                        nudDiasAviso1.Value = Convert.ToDecimal(reader["DiasAviso1"]);
                        nudDiasAviso2.Value = Convert.ToDecimal(reader["DiasAviso2"]);

                        // Cargar Ruta de Backup para que el usuario no tenga que buscarla siempre
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

        // 2. EVENTO PARA SUBIR EL LOGO DESDE LA PC
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

        // 3. EVENTO DEL BOTÓN GUARDAR CAMBIOS
        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    // Actualizamos todos los datos, incluida la ruta del backup
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

                    // Lógica especial para guardar la imagen como binario
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

                    MessageBox.Show("¡Configuración guardada correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // EVENTO PARA EL BOTÓN DE PRUEBA
        private void btnPruebaCorreo_Click(object sender, EventArgs e)
        {
            // 1. Verificamos que no hayan dejado las cajas vacías
            if (string.IsNullOrWhiteSpace(txtCorreoEmisor.Text) || string.IsNullOrWhiteSpace(txtContrasenaCorreo.Text))
            {
                MessageBox.Show("Por favor, completá el correo y la contraseña antes de hacer la prueba.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Ponemos el cursor del mouse en modo "Cargando" porque el envío tarda unos segundos
                Cursor.Current = Cursors.WaitCursor;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                // Toma los datos escritos en las cajas de texto en ese mismo momento
                smtp.Credentials = new NetworkCredential(txtCorreoEmisor.Text.Trim(), txtContrasenaCorreo.Text.Trim());

                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(txtCorreoEmisor.Text.Trim(), "Sistema AsuFit");

                // Se envía un correo a sí mismo para comprobar que la bandeja funciona
                correo.To.Add(txtCorreoEmisor.Text.Trim());
                correo.Subject = "✅ Correo de Prueba - Configuración AsuFit";
                correo.Body = "¡Felicidades!\n\nSi estás leyendo esto, significa que el motor de correos de tu sistema AsuFit está configurado correctamente y listo para avisar automáticamente a los socios sobre sus vencimientos.\n\nSaludos.";
                correo.IsBodyHtml = false;

                // Disparamos el correo
                smtp.Send(correo);

                // Devolvemos el cursor a la normalidad
                Cursor.Current = Cursors.Default;

                MessageBox.Show("¡Conexión exitosa!\n\nTe hemos enviado un correo de prueba a tu bandeja de entrada. Por favor, revisalo para confirmar.", "Prueba Superada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Error al conectar con el correo. Verifica que tu contraseña de aplicación sea correcta y no tenga espacios de más.\n\nDetalle técnico: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // EVENTO PARA GUARDAR SOLO LA PESTAÑA DE NOTIFICACIONES
        private void btnGuardarNotificaciones_Click(object sender, EventArgs e)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    // Hacemos el UPDATE únicamente de los campos de correo y días
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

                    MessageBox.Show("¡Configuración de correos y avisos guardada correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AsuFit.Datos.GestorAuditoria.Registrar("Administrador", "Configuración", "Actualización", "Se cambiaron los parámetros de envío de correos.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar las notificaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // EVENTO PARA EL BOTÓN CANCELAR
        private void btnCancelarNotificaciones_Click(object sender, EventArgs e)
        {
            // Pedimos confirmación antes de borrar lo que estaba escribiendo
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas cancelar? Se perderán los cambios no guardados.", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Reutilizamos el método de carga inicial para sobreescribir las cajas 
                // con los datos que están seguros en la base de datos.
                CargarConfiguracion();
            }
        }

        // 4. EVENTO PARA GENERAR EL BACKUP
        private void btnGenerarBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaDestino.Text))
            {
                MessageBox.Show("Por favor, seleccioná una carpeta de destino usando el botón Examinar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fechaHoy = DateTime.Now.ToString("yyyyMMdd_HHmm");
            string nombreArchivo = $"AsuFit_Backup_{fechaHoy}.bak";
            string rutaCompleta = System.IO.Path.Combine(txtRutaDestino.Text, nombreArchivo);

            try
            {
                string query = $"BACKUP DATABASE AsuFitDB TO DISK = '{rutaCompleta}' WITH FORMAT, MEDIANAME = 'AsuFit_Backups', NAME = 'Respaldo Completo AsuFit'";

                using (System.Data.SqlClient.SqlConnection oConexion = AsuFit.Datos.Conexion.ObtenerConexion())
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, oConexion);
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }

                lblUltimoRespaldo.Text = "Último respaldo realizado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " hs";
                MessageBox.Show($"¡Copia de seguridad generada con éxito!\n\nSe guardó en:\n{rutaCompleta}", "Respaldo Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar generar la copia de seguridad:\n\n" + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 5. EVENTO PARA EXAMINAR Y BUSCAR LA CARPETA DE RESPALDO
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
    }
}