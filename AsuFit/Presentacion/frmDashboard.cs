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
            AbrirFormularioHijo(new frmInicio());
            ResaltarBoton(btnInicio);
        }
        #endregion

        #region MÉTODOS DE LA INTERFAZ (UI)
        // --- MÉTODO PARA LOS COLORES DEL MENÚ ---
        private void ResaltarBoton(object btnSender)
        {
            if (btnSender != null)
            {
                if (botonActivo != null)
                {
                    botonActivo.BackColor = Color.FromArgb(30, 30, 30);
                    botonActivo.ForeColor = Color.White;
                }

                botonActivo = (Button)btnSender;
                botonActivo.BackColor = Color.White;
                botonActivo.ForeColor = Color.Black;
            }
        }

        // --- MÉTODO CENTRALIZADO PARA ABRIR VENTANAS ---
        private void AbrirFormularioHijo(Form formularioHijo)
        {
            // 1. Limpiamos el panel central por si ya había otra ventana abierta
            pnlContenedor.Controls.Clear();

            // 2. Quitamos bordes y comportamiento de ventana independiente
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;

            // 3. Calculamos el centro exacto del contenedor gris
            int x = (pnlContenedor.Width - formularioHijo.Width) / 2;
            int y = (pnlContenedor.Height - formularioHijo.Height) / 2;

            // Evitamos que se corte si algún formulario llega a ser más grande que el panel
            if (x < 0) x = 0;
            if (y < 0) y = 0;

            // 4. Lo posicionamos en el centro y le quitamos los anclajes para que flote
            formularioHijo.Location = new Point(x, y);
            formularioHijo.Anchor = AnchorStyles.None;

            // 5. Lo agregamos al panel gris y lo mostramos
            pnlContenedor.Controls.Add(formularioHijo);
            formularioHijo.Show();
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