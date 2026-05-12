using AsuFit.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmDashboard : Form
    {
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

            lblNombreUsuario.Text = "Bienvenido: " + usuarioActual.NombreCompleto;
            lblRolUsuario.Text = "Nivel de acceso: " + usuarioActual.Rol.ToUpper();

            if (usuarioActual.Rol == "Administrador")
            {
                lblRolUsuario.ForeColor = Color.Yellow;
            }
            else
            {
                lblRolUsuario.ForeColor = Color.Green;
            }
        }

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

        // --- 👇 EL NUEVO MÉTODO CENTRALIZADO 👇 ---
        private void AbrirFormularioHijo(Form formularioHijo)
        {
            // 1. Limpiamos el panel central por si ya había otra ventana abierta
            pnlContenedor.Controls.Clear();

            // 2. Quitamos bordes y comportamiento de ventana independiente
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;

            // 3. Lo agregamos al panel gris y lo mostramos
            pnlContenedor.Controls.Add(formularioHijo);
            formularioHijo.Show();
        }


        // --- EVENTOS DE LOS BOTONES ---

        private void btnInicio_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmInicio());
        }

        private void btnRegistrarSocio_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmRegistrarSocio());
        }

        private void btnGestionSocios_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmGestionSocios(usuarioActual));
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

        private void btnRegistrarCobro_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmRegistrarCobro(usuarioActual));
            ResaltarBoton(sender);
        }

        private void btnInventarioVentas_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmPuntoVenta(usuarioActual));
            ResaltarBoton(sender);
        }

        private void btnGestionGastos_Click(object sender, EventArgs e)
        {
            frmGestionGastos ventanaGastos = new frmGestionGastos();
            AbrirFormularioHijo(ventanaGastos);
            ResaltarBoton(sender);
        }

        private void btnArqueoCaja_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmArqueoCaja(usuarioActual));
            ResaltarBoton(sender);
        }

        private void btnGestionPlanes_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmGestionPlanes(usuarioActual));
            ResaltarBoton(sender);
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmProveedores(usuarioActual));
            ResaltarBoton(sender);
        }

        private void btnReportesEstadísticas_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmReportes());
            ResaltarBoton(sender);
        }

        private void btnRespaldosAuditoría_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmAuditoria());
            ResaltarBoton(sender);
        }

        private void btnConfiguración_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmConfiguracion(usuarioActual));
            ResaltarBoton(sender);
        }

        // --- EVENTOS DEL SISTEMA Y LOGOUT ---

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

        private void btnGestionProductos_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmGestionProductos(usuarioActual));
            ResaltarBoton(sender);
        }

        private void btnIngresoMercaderia_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmIngresoMercaderia(usuarioActual));
            ResaltarBoton(sender);
        }

        // Este es tu botón unificado de Historial (antes llamado btnHistorialVentas)
        private void btnHistorialVentas_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmHistorialTransacciones());
            ResaltarBoton(sender);
        }
    }
}