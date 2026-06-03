using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmConfiguracion : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private Usuario usuarioActual;
        private ConfiguracionNegocio negocio = new ConfiguracionNegocio();

        // FIX CRÍTICO: Bandera de seguridad para evitar el bucle infinito al cargar la ventana
        private bool _esCargaInicial = true;

        public frmConfiguracion(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
            _esCargaInicial = true; // Aseguramos que esté activo al iniciar la carga

            ConfigurarTemaOscuro();
            CargarConfiguracion();
            CargarEscalaActual();
            this.ActiveControl = null; // Evita el borde azul en el combobox al cargar

            _esCargaInicial = false; // ¡Carga completada! A partir de aquí, el evento responderá al usuario
        }

        private void CargarConfiguracion()
        {
            try
            {
                Configuracion config = negocio.ObtenerConfiguracion();

                txtNombreGimnasio.Text = config.NombreGimnasio;
                txtRUC.Text = config.Ruc;
                txtDireccion.Text = config.Direccion;
                txtTelefono.Text = config.Telefono;
                txtCorreoEmisor.Text = config.CorreoEmisor;
                txtContrasenaCorreo.Text = config.ContrasenaCorreo;
                nudDiasAviso1.Value = config.DiasAviso1;
                nudDiasAviso2.Value = config.DiasAviso2;
                txtRutaDestino.Text = config.RutaBackup;

                if (config.Logo != null)
                {
                    using (MemoryStream ms = new MemoryStream(config.Logo))
                    {
                        picLogo.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la configuración: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 3. ESTILOS VISUALES (TEMA OSCURO)
        private void ConfigurarTemaOscuro()
        {
            float fuenteActual = Properties.Settings.Default.TamanoFuente;
            this.BackColor = Color.FromArgb(25, 28, 35);
            AplicarTemaOscuroRecursivo(this, fuenteActual);
        }

        private void AplicarTemaOscuroRecursivo(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Panel || c is GroupBox || c is TabPage)
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
                    txt.BackColor = Color.FromArgb(50, 55, 65);
                    txt.ForeColor = Color.White;
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
                else if (c is NumericUpDown nud)
                {
                    nud.BackColor = Color.FromArgb(50, 55, 65);
                    nud.ForeColor = Color.White;
                    nud.BorderStyle = BorderStyle.FixedSingle;
                    nud.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
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
                else if (c is TabControl tab)
                {
                    tab.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
                    tab.DrawMode = TabDrawMode.Normal;
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c, fuente);
            }
        }
        #endregion

        #region 4. PESTAÑA: EMPRESA Y DATOS GENERALES
        private void btnSubirLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccionar Logo del Gimnasio";
                ofd.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    picLogo.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                Configuracion obj = new Configuracion();
                obj.NombreGimnasio = txtNombreGimnasio.Text;
                obj.Ruc = txtRUC.Text;
                obj.Direccion = txtDireccion.Text;
                obj.Telefono = txtTelefono.Text;
                obj.RutaBackup = txtRutaDestino.Text;

                if (picLogo.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picLogo.Image.Save(ms, picLogo.Image.RawFormat);
                        obj.Logo = ms.ToArray();
                    }
                }

                if (negocio.ActualizarDatosGenerales(obj))
                {
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Configuración", "Actualización General", "Se modificaron los datos generales de la empresa o el logo.");
                    MessageBox.Show("¡Configuración guardada correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 5. PESTAÑA: NOTIFICACIONES Y ALERTAS
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
                negocio.ProbarConexionCorreo(txtCorreoEmisor.Text.Trim(), txtContrasenaCorreo.Text.Trim());
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
            try
            {
                Configuracion obj = new Configuracion();
                obj.CorreoEmisor = txtCorreoEmisor.Text.Trim();
                obj.ContrasenaCorreo = txtContrasenaCorreo.Text.Trim();
                obj.DiasAviso1 = (int)nudDiasAviso1.Value;
                obj.DiasAviso2 = (int)nudDiasAviso2.Value;

                if (negocio.ActualizarNotificaciones(obj))
                {
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Configuración", "Actualización", "Se cambiaron los parámetros de envío de correos.");
                    MessageBox.Show("¡Configuración de correos y avisos guardada correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar las notificaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region 6. PESTAÑA: SISTEMA Y RESPALDOS (BACKUP)
        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Entrá a la carpeta donde deseás guardar y hacé clic en Abrir";
                ofd.ValidateNames = false;
                ofd.CheckFileExists = false;
                ofd.CheckPathExists = true;
                ofd.FileName = "Selección_de_Carpeta";
                ofd.Filter = "Carpetas|*.ninguno";

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    txtRutaDestino.Text = Path.GetDirectoryName(ofd.FileName);
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
                negocio.GenerarBackup(rutaCompleta);

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

        #region 7. ACCESIBILIDAD Y ESCALADO
        private void CargarEscalaActual()
        {
            if (cmbEscala == null) return;

            float escalaActual = Properties.Settings.Default.EscalaInterfaz;

            if (escalaActual == 1.0f) cmbEscala.SelectedIndex = 0;
            else if (escalaActual == 1.4f) cmbEscala.SelectedIndex = 1;
            else cmbEscala.SelectedIndex = 1;
        }

        private void cmbEscala_DropDownClosed(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void cmbEscala_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveControl = null;

            // FIX CRÍTICO: Si es la carga inicial de la pantalla, salimos para evitar el bucle infinito
            if (_esCargaInicial) return;

            if (cmbEscala.SelectedIndex == -1) return;

            float escalaSeleccionada = 1.4f;

            switch (cmbEscala.SelectedIndex)
            {
                case 0: escalaSeleccionada = 1.0f; break;
                case 1: escalaSeleccionada = 1.4f; break;
            }

            frmDashboard dashboard = Application.OpenForms["frmDashboard"] as frmDashboard;
            if (dashboard != null)
            {
                this.Hide();

                dashboard.AplicarNuevaEscala(escalaSeleccionada);
                this.Close();

                Control[] botones = dashboard.Controls.Find("btnConfiguración", true);
                if (botones.Length > 0 && botones[0] is Button btn)
                {
                    btn.PerformClick();
                }
                else
                {
                    Control[] botonesSinAcento = dashboard.Controls.Find("btnConfiguracion", true);
                    if (botonesSinAcento.Length > 0 && botonesSinAcento[0] is Button btnSinAcento)
                    {
                        btnSinAcento.PerformClick();
                    }
                }
            }
        }
        #endregion
    }
}