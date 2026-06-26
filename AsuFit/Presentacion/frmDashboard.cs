using AsuFit.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmDashboard : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private Button botonActivo = null;
        private Usuario usuarioActual;
        private bool _cerrandoParaLogOut = false;

        private ContextMenuStrip menuDropdownUsuario;
        private ContextMenuStrip menuDropdownNotificaciones;

        public frmDashboard(Usuario user)
        {
            InitializeComponent();
            usuarioActual = user;
        }
        #endregion

        #region 2. EVENTOS PRINCIPALES DEL FORMULARIO (LOAD Y CIERRE)
        private void frmDashboard_Load(object sender, EventArgs e)
        {
            float escala = Properties.Settings.Default.EscalaInterfaz;
            this.Scale(new SizeF(escala, escala));
            AjustarLetraMenu(this);

            // 1. Mostramos el ROL, forzamos letra blanca y agregamos el ÍCONO 👤
            btnUsuario.ForeColor = Color.White;
            // Hace que el reloj tome la fuente dinámica del sistema y le suma 2 puntos para que destaque en negrita
            lblFechaHora.Font = new Font("Segoe UI", Properties.Settings.Default.TamanoFuente + 2f, FontStyle.Bold);
            btnUsuario.Text = $"👤 {usuarioActual.NombreCompleto}";

            if (lblFechaHora != null)
            {
                lblFechaHora.Font = new Font("Segoe UI", Properties.Settings.Default.TamanoFuente, FontStyle.Regular);
            }

            btnNotificaciones.ForeColor = Color.White;

            // 2. Construir los menús invisibles con datos de la BD
            GenerarMenusDesplegables();

            // 3. Forzar la primera actualización del reloj para que arranque al instante
            timerReloj_Tick(null, null);
            if (timerReloj != null) timerReloj.Start();

            this.CenterToScreen();

            // =====================================================================
            // 4. RESTRICCIÓN DE ACCESOS POR ROLES (RBAC)
            // =====================================================================
            if (usuarioActual.Rol == "Recepcionista")
            {
                // GESTIÓN DE SOCIOS Y ACCESOS
                btnGestionPlanes.Visible = false; // No puede alterar precios de cuotas

                // COMERCIAL E INVENTARIO
                btnGestionProductos.Visible = false; // No altera el catálogo
                btnIngresoMercaderia.Visible = false; // No registra compras a proveedores
                btnProveedores.Visible = false; // No ve datos de proveedores

                // CAJA Y FINANZAS
                btnHistorialTransacciones.Visible = false; // No ve el historial de otros días
                btnGestionGastos.Visible = false; // No ve gastos de luz, alquiler, etc.
                btnReportesEstadísticas.Visible = false; // No ve las ganancias totales

                // SEGURIDAD Y CONTROL (No pueden crear ni auditar usuarios)
                btnRegistrarUsuario.Visible = false;
                btnGestionUsuarios.Visible = false;
                btnAuditoría.Visible = false;

                // Ocultar título del módulo 4 (Seguridad)
                Control[] lblMod4 = this.Controls.Find("lblModulo4", true);
                if (lblMod4.Length > 0) lblMod4[0].Visible = false;

                // BARRA SUPERIOR (Configuración general del gimnasio)
                Control[] btnConfig = this.Controls.Find("btnConfiguración", true);
                if (btnConfig.Length > 0) btnConfig[0].Visible = false;

                Control[] btnConfigSinTilde = this.Controls.Find("btnConfiguracion", true);
                if (btnConfigSinTilde.Length > 0) btnConfigSinTilde[0].Visible = false;
            }
            // =====================================================================

            this.Shown += (s, ev) =>
            {
                MessageBox.Show($"¡Bienvenido a AsuFit, {usuarioActual.NombreCompleto}!",
                                "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnInicio.PerformClick();
            };
        }

        private void frmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_cerrandoParaLogOut)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea salir del sistema AsuFit?",
                    "Confirmar Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    AsuFit.Datos.GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Seguridad", "Cierre de Sistema", "Cierre desde la X.");
                    Application.ExitThread();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (pnlContenedor.Controls.Count > 0)
                {
                    pnlContenedor.Controls[0].Dispose();
                    pnlContenedor.Controls.Clear();

                    if (botonActivo != null)
                    {
                        botonActivo.BackColor = botonActivo.Parent.BackColor;
                        botonActivo.ForeColor = Color.White;
                        botonActivo = null;
                    }
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region 3. LÓGICA DE BARRA SUPERIOR, BD Y MENÚS DESPLEGABLES

        // Evento que actualiza la fecha y hora en tiempo real
        private void timerReloj_Tick(object sender, EventArgs e)
        {
            // Obtenemos la fecha en español
            string fecha = DateTime.Now.ToString("dddd dd/MM/yyyy", new System.Globalization.CultureInfo("es-ES"));

            // Capitalizamos la primera letra
            if (!string.IsNullOrEmpty(fecha))
                fecha = char.ToUpper(fecha[0]) + fecha.Substring(1);

            // Obtenemos la hora con segundos incluidos
            string hora = DateTime.Now.ToString("HH:mm:ss");

            // Imprimimos todo en el Label con sus respectivos íconos
            if (lblFechaHora != null)
                lblFechaHora.Text = $"📅 {fecha}   |   🕒 {hora}";
        }

        private void ConsultarNotificacionesBD(out int porVencer, out int vencidos, out int stockBajo, out int sinStock)
        {
            porVencer = 0; vencidos = 0; stockBajo = 0; sinStock = 0;
            try
            {
                AsuFit.Negocio.DashboardNegocio negocio = new AsuFit.Negocio.DashboardNegocio();

                // Le pedimos los datos
                negocio.ObtenerContadoresNotificaciones(out porVencer, out vencidos, out stockBajo, out sinStock);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Notificaciones: " + ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarMenusDesplegables()
        {
            TemaOscuroRenderer temaOscuro = new TemaOscuroRenderer();

            // --- MENÚ DE USUARIO ---
            menuDropdownUsuario = new ContextMenuStrip();
            menuDropdownUsuario.Cursor = Cursors.Hand;
            menuDropdownUsuario.Renderer = temaOscuro;
            menuDropdownUsuario.ShowImageMargin = false;

            string correoMostrar = string.IsNullOrWhiteSpace(usuarioActual.Email) ? "Correo no registrado" : usuarioActual.Email;

            menuDropdownUsuario.Items.Add(CrearItemMenu($"👤 Nombre: {usuarioActual.NombreCompleto}"));
            menuDropdownUsuario.Items.Add(CrearItemMenu($"📧 Correo: {correoMostrar}"));
            menuDropdownUsuario.Items.Add(CrearItemMenu($"🆔 Username: {usuarioActual.Username}"));
            menuDropdownUsuario.Items.Add(CrearItemMenu($"🛡️ Rol: {usuarioActual.Rol}"));
            menuDropdownUsuario.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem btnCambiarPass = CrearItemMenu("🔑 Cambiar Contraseña / Perfil");
            btnCambiarPass.Click += (s, e) => {

                frmRegistrarUsuario frmEdicion = new frmRegistrarUsuario(usuarioActual);
                float escalaActual = Properties.Settings.Default.EscalaInterfaz;
                frmEdicion.Scale(new SizeF(escalaActual, escalaActual));
                frmEdicion.StartPosition = FormStartPosition.CenterParent;

                // --- NUEVO CANDADO DE SEGURIDAD ---
                if (usuarioActual.Rol != "Administrador")
                {
                    frmEdicion.BloquearPermisosParaEmpleado();
                }

                frmEdicion.ShowDialog();

                // Actualizamos por si el usuario cambió su propio nombre completo
                btnUsuario.Text = $"👤 {usuarioActual.NombreCompleto}";
            };
            menuDropdownUsuario.Items.Add(btnCambiarPass);


            // --- MENÚ DE NOTIFICACIONES ---
            menuDropdownNotificaciones = new ContextMenuStrip();
            menuDropdownNotificaciones.Cursor = Cursors.Hand;
            menuDropdownNotificaciones.Renderer = temaOscuro;
            menuDropdownNotificaciones.ShowImageMargin = false;

            ConsultarNotificacionesBD(out int sociosPorVencer, out int sociosVencidos, out int prodStockBajo, out int prodSinStock);

            int totalAlertas = sociosPorVencer + sociosVencidos + prodStockBajo + prodSinStock;

            // Agregamos el ÍCONO 🔔 al texto del botón
            btnNotificaciones.Text = $"🔔 Notificaciones ({totalAlertas})";
            btnNotificaciones.Top = btnUsuario.Top;

            ToolStripMenuItem itemSociosVencer = CrearItemMenu($"🟡 Socios por vencer: {sociosPorVencer}");
            itemSociosVencer.Click += (s, e) => { btnGestionSocios.PerformClick(); };

            ToolStripMenuItem itemSociosVencidos = CrearItemMenu($"🔴 Socios vencidos: {sociosVencidos}");
            itemSociosVencidos.Click += (s, e) => { btnGestionSocios.PerformClick(); };

            ToolStripMenuItem itemProdBajo = CrearItemMenu($"🟠 Productos con stock bajo: {prodStockBajo}");
            itemProdBajo.Click += (s, e) => { btnGestionProductos.PerformClick(); };

            ToolStripMenuItem itemProdAgotados = CrearItemMenu($"🔴 Productos sin stock: {prodSinStock}");
            itemProdAgotados.Click += (s, e) => { btnGestionProductos.PerformClick(); };

            menuDropdownNotificaciones.Items.Add(itemSociosVencer);
            menuDropdownNotificaciones.Items.Add(itemSociosVencidos);
            menuDropdownNotificaciones.Items.Add(new ToolStripSeparator());
            menuDropdownNotificaciones.Items.Add(itemProdBajo);
            menuDropdownNotificaciones.Items.Add(itemProdAgotados);
        }

        private ToolStripMenuItem CrearItemMenu(string texto)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(texto);
            item.ForeColor = Color.White;
            return item;
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            menuDropdownUsuario.Show(btnUsuario, new Point(0, btnUsuario.Height));
        }

        private void btnNotificaciones_Click(object sender, EventArgs e)
        {
            GenerarMenusDesplegables();
            menuDropdownNotificaciones.Show(btnNotificaciones, new Point(0, btnNotificaciones.Height));
        }
        #endregion

        #region 4. MÉTODOS DE LA INTERFAZ (UI Y ESCALADO)
        private void ResaltarBoton(object btnSender)
        {
            if (btnSender != null)
            {
                if (botonActivo != null)
                {
                    botonActivo.BackColor = botonActivo.Parent.BackColor;
                    botonActivo.ForeColor = Color.White;
                }

                botonActivo = (Button)btnSender;
                botonActivo.BackColor = Color.FromArgb(35, 39, 47);
                botonActivo.ForeColor = Color.FromArgb(0, 229, 255);
            }
        }

        private void PulirTextosYGrillas(Control contenedor)
        {
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            foreach (Control c in contenedor.Controls)
            {
                if (c is TextBox || c is ComboBox || c is Label)
                {
                    c.Font = new Font("Segoe UI", fuenteActual, c.Font.Style);
                }
                else if (c is DataGridView dgv)
                {
                    dgv.DefaultCellStyle.Font = new Font("Segoe UI", fuenteActual, FontStyle.Regular);
                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", fuenteActual, FontStyle.Bold);
                    dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }

                if (c.HasChildren)
                {
                    PulirTextosYGrillas(c);
                }
            }
        }

        private void AjustarLetraMenu(Control contenedor)
        {
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            foreach (Control c in contenedor.Controls)
            {
                if (c is Button)
                {
                    c.Font = new Font("Segoe UI", fuenteActual, c.Font.Style);
                }
                else if (c.HasChildren)
                {
                    AjustarLetraMenu(c);
                }
            }
        }

        private void AbrirFormularioHijo(Form formularioHijo)
        {
            pnlContenedor.SuspendLayout();

            if (pnlContenedor.Controls.Count > 0)
            {
                pnlContenedor.Controls[0].Dispose();
                pnlContenedor.Controls.Clear();
            }

            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;

            float escala = Properties.Settings.Default.EscalaInterfaz;
            formularioHijo.Scale(new SizeF(escala, escala));

            PulirTextosYGrillas(formularioHijo);

            int x = (pnlContenedor.Width - formularioHijo.Width) / 2;
            int y = (pnlContenedor.Height - formularioHijo.Height) / 2;
            formularioHijo.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);

            pnlContenedor.Controls.Add(formularioHijo);
            formularioHijo.Show();

            pnlContenedor.ResumeLayout();
        }

        public void AplicarNuevaEscala(float nuevaEscala)
        {
            float escalaActual = Properties.Settings.Default.EscalaInterfaz;

            if (escalaActual == nuevaEscala) return;

            float factor = nuevaEscala / escalaActual;

            this.SuspendLayout();
            this.Scale(new SizeF(factor, factor));

            float nuevaFuente = 8f + ((nuevaEscala - 1.0f) * 5f);

            Properties.Settings.Default.EscalaInterfaz = nuevaEscala;
            Properties.Settings.Default.TamanoFuente = nuevaFuente;
            Properties.Settings.Default.Save();

            AjustarLetraMenu(this);
            PulirTextosYGrillas(this);

            Rectangle areaTrabajo = Screen.FromControl(this).WorkingArea;
            this.Location = new Point(
                areaTrabajo.X + (areaTrabajo.Width - this.Width) / 2,
                areaTrabajo.Y + (areaTrabajo.Height - this.Height) / 2
            );

            if (pnlContenedor.Controls.Count > 0)
            {
                Form hijoAbierto = pnlContenedor.Controls[0] as Form;
                if (hijoAbierto != null)
                {
                    int x = (pnlContenedor.Width - hijoAbierto.Width) / 2;
                    int y = (pnlContenedor.Height - hijoAbierto.Height) / 2;
                    hijoAbierto.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);
                }
            }

            this.ResumeLayout(true);
            this.PerformLayout();
        }
        #endregion

        #region 5. EVENTOS DEL MENÚ LATERAL (MÓDULOS DEL SISTEMA)
        private void btnInicio_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmInicio());
        }

        private void btnRegistroAsistencia_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);

            foreach (Form formulario in Application.OpenForms)
            {
                if (formulario is frmAsistencia)
                {
                    formulario.BringToFront();
                    formulario.Focus();
                    return;
                }
            }

            frmAsistencia frm = new frmAsistencia();
            Screen[] pantallas = Screen.AllScreens;

            if (pantallas.Length > 1)
            {
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = pantallas[1].WorkingArea.Location;
                frm.WindowState = FormWindowState.Maximized;
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
            }

            frm.Show();
        }

        private void btnRegistrarSocio_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmRegistrarSocio(usuarioActual));
        }

        private void btnGestionSocios_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmGestionSocios(usuarioActual));
        }

        private void btnGestionPlanes_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmGestionPlanes(usuarioActual));
        }

        private void btnInventarioVentas_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmPuntoVenta(usuarioActual));
        }

        private void btnGestionProductos_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmGestionProductos(usuarioActual));
        }

        private void btnIngresoMercaderia_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmIngresoMercaderia(usuarioActual));
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmProveedores(usuarioActual));
        }

        private void btnRegistrarCobro_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmRegistrarCobro(usuarioActual));
        }

        private void btnHistorialTransacciones_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmHistorialTransacciones());
        }

        private void btnGestionGastos_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmGestionGastos());
        }

        private void btnArqueoCaja_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmArqueoCaja(usuarioActual));
        }

        private void btnReportesEstadísticas_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmReportes());
        }

        private void btnRegistrarUsuario_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmRegistrarUsuario());
        }

        private void btnGestionUsuarios_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmGestionUsuarios(usuarioActual));
        }

        private void btnAuditoría_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmAuditoria());
        }

        private void btnConfiguración_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmConfiguracion(usuarioActual));
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea cerrar la sesión actual?",
                "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                AsuFit.Datos.GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Seguridad", "Cierre de Sesión", "Cierre normal.");

                Form ventanaAsistencia = null;
                foreach (Form formulario in Application.OpenForms)
                {
                    if (formulario is frmAsistencia)
                    {
                        ventanaAsistencia = formulario;
                        break;
                    }
                }
                if (ventanaAsistencia != null) ventanaAsistencia.Close();

                _cerrandoParaLogOut = true;
                this.Close();
                frmLogin login = new frmLogin();
                login.Show();
            }
        }
        #endregion
    }

    #region 6. CLASES ESPECIALES PARA EL TEMA OSCURO DE LOS MENÚS
    public class TemaOscuroRenderer : ToolStripProfessionalRenderer
    {
        public TemaOscuroRenderer() : base(new TemaOscuroColorTable()) { }
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }
    }

    public class TemaOscuroColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground => Color.FromArgb(25, 27, 33);
        public override Color ImageMarginGradientBegin => Color.FromArgb(25, 27, 33);
        public override Color ImageMarginGradientMiddle => Color.FromArgb(25, 27, 33);
        public override Color ImageMarginGradientEnd => Color.FromArgb(25, 27, 33);
        public override Color MenuItemSelected => Color.FromArgb(50, 55, 65);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(50, 55, 65);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(50, 55, 65);
        public override Color MenuItemBorder => Color.Transparent;
        public override Color MenuBorder => Color.FromArgb(20, 22, 27);
    }
    #endregion
}