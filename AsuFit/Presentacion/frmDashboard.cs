using AsuFit.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmDashboard : Form
    {
        #region VARIABLES GLOBALES Y CONSTRUCTOR
        // Variable para recordar cuál es el botón que está seleccionado actualmente
        private Button botonActivo = null;

        // Variable para guardar los datos del usuario logueado
        private Usuario usuarioActual;

        // Nos dice si cerramos para salir (True) o para loguear de nuevo (False)
        private bool _cerrandoParaLogOut = false;

        public frmDashboard(Usuario user)
        {
            InitializeComponent();
            usuarioActual = user;
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            this.Scale(new SizeF(1.4f, 1.4f));
            this.CenterToScreen();

            // Agrandamos la letra de los botones del menú lateral
            AjustarLetraMenu(this);

            // Enlazamos el evento para cuando la ventana termine de cargar
            this.Shown += (s, ev) =>
            {
                // 1. Sale el cartel con el fondo limpio detrás
                MessageBox.Show($"¡Bienvenido a AsuFit, {usuarioActual.NombreCompleto}!",
                                "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 2. Cuando el usuario le da "Aceptar" al cartel, simulamos el clic en Inicio
                btnInicio.PerformClick();
            };
        }

        // --- MÉTODO GLOBAL PARA ESCUCHAR LA TECLA ESCAPE ---
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Si el usuario presiona Escape
            if (keyData == Keys.Escape)
            {
                // Verificamos si hay algún formulario abierto tapando el fondo
                if (pnlContenedor.Controls.Count > 0)
                {
                    // Lo destruimos y limpiamos el panel para que se vea el fondo del gym
                    pnlContenedor.Controls[0].Dispose();
                    pnlContenedor.Controls.Clear();

                    // Regresamos el color del botón activo en el menú lateral a la normalidad
                    if (botonActivo != null)
                    {
                        botonActivo.BackColor = botonActivo.Parent.BackColor;
                        botonActivo.ForeColor = Color.White;
                        botonActivo = null; // Reiniciamos la variable
                    }

                    return true; // Le decimos a Windows que ya procesamos la tecla
                }
            }

            // Para cualquier otra tecla, que siga su comportamiento normal
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region MÉTODOS DE LA INTERFAZ (UI)
        // --- MÉTODO PARA LOS COLORES DEL MENÚ ---
        private void ResaltarBoton(object btnSender)
        {
            if (btnSender != null)
            {
                // 1. Si había un botón seleccionado antes, lo regresamos a la normalidad
                if (botonActivo != null)
                {
                    // EL FIX: Hereda el color exacto del panel lateral para camuflarse de nuevo
                    botonActivo.BackColor = botonActivo.Parent.BackColor;
                    botonActivo.ForeColor = Color.White;
                }

                // 2. Pintamos el NUEVO botón que el usuario acaba de clickear
                botonActivo = (Button)btnSender;
                botonActivo.BackColor = Color.FromArgb(35, 39, 47); // Gris resaltado (como las tarjetas)
                botonActivo.ForeColor = Color.FromArgb(0, 229, 255); // Tu color Cian AsuFit
            }
        }

        // --- MÉTODO INTELIGENTE PARA PULIR TEXTOS SIN ROMPER PANELES ---
        private void PulirTextosYGrillas(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                // 1. Agrandar solo textos legibles (TextBox, ComboBox, Labels de datos)
                // Usamos 10f para lectura cómoda. No tocamos GroupBox ni Panels.
                if (c is TextBox || c is ComboBox || c is Label)
                {
                    c.Font = new Font("Segoe UI", 10f, c.Font.Style);
                }

                // 2. Tablas bajo control absoluto
                else if (c is DataGridView dgv)
                {
                    // Forzamos un tamaño legible
                    dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);

                    // Auto-ajuste de altura de filas para que la letra no se corte
                    dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }

                // 3. Revisar dentro de contenedores
                if (c.HasChildren)
                {
                    PulirTextosYGrillas(c);
                }
            }
        }

        // --- MÉTODO PARA AGRANDAR LA LETRA DEL MENÚ LATERAL ---
        private void AjustarLetraMenu(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Button)
                {
                    // Le forzamos un tamaño base de 10f o 11f para que resalte
                    c.Font = new Font("Segoe UI", 10f, c.Font.Style);
                }
                else if (c.HasChildren)
                {
                    // Entra al panel oscuro a buscar los botones
                    AjustarLetraMenu(c);
                }
            }
        }

        // --- MÉTODO CENTRALIZADO PARA ABRIR VENTANAS (VERSIÓN NATIVA) ---
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
            formularioHijo.Anchor = AnchorStyles.None;

            // 1. Escalar el formulario base
            formularioHijo.Scale(new SizeF(1.4f, 1.4f));

            // 2. Pulir los textos y grillas individualmente sin desbordar los paneles
            PulirTextosYGrillas(formularioHijo);

            // Centrado
            int x = (pnlContenedor.Width - formularioHijo.Width) / 2;
            int y = (pnlContenedor.Height - formularioHijo.Height) / 2;
            formularioHijo.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);

            pnlContenedor.Controls.Add(formularioHijo);
            formularioHijo.Show();

            pnlContenedor.ResumeLayout();
        }
        #endregion

        #region EVENTOS: MÓDULO PRINCIPAL Y SOCIOS
        private void btnInicio_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmInicio());
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
        #endregion

        #region EVENTOS: MÓDULO CAJA Y VENTAS
        private void btnRegistrarCobro_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmRegistrarCobro(usuarioActual));
        }

        private void btnInventarioVentas_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmPuntoVenta(usuarioActual));
        }

        private void btnHistorialVentas_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmHistorialTransacciones());
        }

        private void btnArqueoCaja_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmArqueoCaja(usuarioActual));
        }
        #endregion

        #region EVENTOS: MÓDULO INVENTARIO Y GASTOS
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

        private void btnGestionGastos_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmGestionGastos());
        }

        private void btnGestionPlanes_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmGestionPlanes(usuarioActual));
        }
        #endregion

        #region EVENTOS: MÓDULO ADMINISTRACIÓN Y SEGURIDAD
        private void btnReportesEstadísticas_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmReportes());
        }

        private void btnRespaldosAuditoría_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmAuditoria());
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

        private void btnConfiguración_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmConfiguracion(usuarioActual));
        }
        #endregion

        #region EVENTOS DEL SISTEMA Y LOGOUT
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea cerrar la sesión actual?",
                "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                AsuFit.Datos.GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Seguridad", "Cierre de Sesión", "El usuario cerró sesión normalmente.");

                Form ventanaAsistencia = null;
                foreach (Form formulario in Application.OpenForms)
                {
                    if (formulario is frmAsistencia)
                    {
                        ventanaAsistencia = formulario;
                        break;
                    }
                }
                if (ventanaAsistencia != null)
                {
                    ventanaAsistencia.Close();
                }

                _cerrandoParaLogOut = true;
                this.Close();
                frmLogin login = new frmLogin();
                login.Show();
            }
        }

        private void frmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_cerrandoParaLogOut)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea salir del sistema AsuFit? Se perderán los cambios no guardados.",
                    "Confirmar Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    AsuFit.Datos.GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Seguridad", "Cierre de Sistema", "El usuario cerró la aplicación usando la 'X' o Alt+F4.");
                    Application.ExitThread();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion
    }
}