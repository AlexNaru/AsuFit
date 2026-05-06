using AsuFit.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            
            // 3. LA CLAVE: No usamos DockStyle.Fill para que el formulario mantenga
            // exactamente el tamaño que le diste en el modo diseño.
            //formularioHijo.Dock = DockStyle.Fill; 

            // 4. Lo agregamos al panel gris y lo mostramos
            pnlContenedor.Controls.Add(formularioHijo);
            formularioHijo.Show();
        }


        // --- EVENTOS DE LOS BOTONES (Ahora mucho más limpios) ---

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
            AbrirFormularioHijo(new frmGestionSocios());
        }

        private void btnRegistrarUsuario_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmRegistrarUsuario());
        }

        private void btnGestionUsuarios_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);
            AbrirFormularioHijo(new frmGestionUsuarios());
        }

        private void btnRegistroAsistencia_Click(object sender, EventArgs e)
        {
            ResaltarBoton(sender);

            // 1. Verificamos si la ventana ya está abierta
            foreach (Form formulario in Application.OpenForms)
            {
                if (formulario is frmAsistencia)
                {
                    formulario.BringToFront(); // La trae al frente
                    formulario.Focus();        // Pone el cursor activo
                    return;                    // CORTAMOS la función acá
                }
            }

            // 2. Si el código llega acá, es porque no estaba abierta. Creamos una nueva.
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

        private void btnHistorialPagos_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmHistorialPagos());
            ResaltarBoton(sender);
        }

        private void btnGestionGastos_Click(object sender, EventArgs e)
        {
            // 1. Creamos la "instancia" de tu nuevo formulario de gastos
            frmGestionGastos ventanaGastos = new frmGestionGastos();

            // 2. Llamamos al método que tenés en tu Menú para incrustar el formulario en el panel central
            // NOTA: Cambiá 'AbrirFormularioHijo' por el nombre real del método que usás en tu sistema
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
            AbrirFormularioHijo(new frmGestionPlanes());
            ResaltarBoton(sender);
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmProveedores());
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
            AbrirFormularioHijo(new frmConfiguracion());
            ResaltarBoton(sender);
        }

        // --- EVENTOS DEL SISTEMA Y LOGOUT ---

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea cerrar la sesión actual?",
                "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // --- NUEVO: Buscar y cerrar frmAsistencia si quedó abierta ---
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
                // --------------------------------------------------------------

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
                Application.ExitThread();
            }
        }

        private void btnGestionProductos_Click(object sender, EventArgs e)
        {
            // Abrimos la pantalla de gestión adentro del panel central
            AbrirFormularioHijo(new frmGestionProductos());

            // Iluminamos el botón en el menú (si tenés esta función)
            ResaltarBoton(sender);
        }

        private void btnIngresoMercaderia_Click(object sender, EventArgs e)
        {
            // Abre la nueva pantalla de re-stock
            AbrirFormularioHijo(new frmIngresoMercaderia());

            // Ilumina el botón en el menú
            ResaltarBoton(sender);
        }

        private void btnHistorialVentas_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmHistorialVentas());
            ResaltarBoton(sender);
        }
    }
}