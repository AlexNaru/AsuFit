using AsuFit.Entidades;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmDashboard : Form
    {
        #region VARIABLES GLOBALES Y CONSTRUCTOR
        private Button botonActivo = null;
        private Usuario usuarioActual;
        private bool _cerrandoParaLogOut = false;

        public frmDashboard(Usuario user)
        {
            InitializeComponent();
            usuarioActual = user;
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            // Aplica la escala global definida en la configuración del usuario
            float escala = Properties.Settings.Default.EscalaInterfaz;
            this.Scale(new SizeF(escala, escala));

            this.CenterToScreen();

            AjustarLetraMenu(this);

            this.Shown += (s, ev) =>
            {
                MessageBox.Show($"¡Bienvenido a AsuFit, {usuarioActual.NombreCompleto}!",
                                "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.None);

                btnInicio.PerformClick();
            };
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

        #region MÉTODOS DE LA INTERFAZ (UI)
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

        // Adapta los textos y grillas utilizando el tamaño de fuente configurado en el sistema
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

        // Ajusta la fuente del menú lateral según las preferencias del usuario
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

        // Renderiza el formulario hijo aplicando la escala y las fuentes configuradas
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

            // Centrado inicial
            int x = (pnlContenedor.Width - formularioHijo.Width) / 2;
            int y = (pnlContenedor.Height - formularioHijo.Height) / 2;
            formularioHijo.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);

            pnlContenedor.Controls.Add(formularioHijo);
            formularioHijo.Show();

            pnlContenedor.ResumeLayout();
        }

        // Recibe una orden externa para re-escalar la interfaz en tiempo real
        public void AplicarNuevaEscala(float nuevaEscala)
        {
            float escalaActual = Properties.Settings.Default.EscalaInterfaz;

            if (escalaActual == nuevaEscala) return;

            float factor = nuevaEscala / escalaActual;

            // 1. Pausamos el renderizado visual
            this.SuspendLayout();

            // 2. Aplicamos el escalado físico general a todo el Dashboard
            this.Scale(new SizeF(factor, factor));

            // 3. Calculamos el tamaño de letra dinámicamente
            float nuevaFuente = 8f + ((nuevaEscala - 1.0f) * 5f);

            // 4. Guardamos en la memoria del sistema operativo
            Properties.Settings.Default.EscalaInterfaz = nuevaEscala;
            Properties.Settings.Default.TamanoFuente = nuevaFuente;
            Properties.Settings.Default.Save();

            // 5. Forzamos la actualización de textos y grillas
            AjustarLetraMenu(this);
            PulirTextosYGrillas(this);

            // 6. EL FIX MAGISTRAL: Calculamos y asignamos la nueva posición MIENTRAS la pantalla está "congelada"
            Rectangle areaTrabajo = Screen.FromControl(this).WorkingArea;
            this.Location = new Point(
                areaTrabajo.X + (areaTrabajo.Width - this.Width) / 2,
                areaTrabajo.Y + (areaTrabajo.Height - this.Height) / 2
            );

            // 7. También centramos el formulario hijo abierto estando congelado
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

            // 8. AHORA SÍ: Le decimos a Windows que dibuje todo de un solo golpe (ya posicionado)
            this.ResumeLayout(true);
            this.PerformLayout();
        }
        #endregion

        #region EVENTOS: INICIO Y MÓDULO 1 (SOCIOS Y ACCESOS)
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
        #endregion

        #region EVENTOS: MÓDULO 2 (COMERCIAL E INVENTARIO)
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
        #endregion

        #region EVENTOS: MÓDULO 3 (CAJA Y FINANZAS)
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
        #endregion

        #region EVENTOS: MÓDULO 4 (SEGURIDAD Y ADMINISTRACIÓN)
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