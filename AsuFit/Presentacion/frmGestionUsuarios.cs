using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmGestionUsuarios : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private int idUsuarioSeleccionado = 0;
        private Usuario usuarioActual;

        public frmGestionUsuarios(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;
            dgvUsuarios.AutoGenerateColumns = false;
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmGestionUsuarios_Load(object sender, EventArgs e)
        {
            ConfigurarTemaOscuro();

            // Aplicamos el placeholder interactivo estilo AsuFit (Color Plata)
            AplicarPlaceholder(txtBuscar, "Buscar por Usuario, Nombre o Rol...");

            CargarGrilla("Activo");

            // Liberamos el foco para que el placeholder se dibuje correctamente
            this.ActiveControl = null;
        }

        private void CargarGrilla(string estado)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvUsuarios.DataSource = negocio.ListarUsuarios(estado);

            lblTotal.Text = "Registros encontrados: " + dgvUsuarios.Rows.Count.ToString();

            dgvUsuarios.ClearSelection();
            idUsuarioSeleccionado = 0;
        }

        private void RecargarGrilla()
        {
            if (chkMostrarInactivos.Checked)
                CargarGrilla("Inactivo");
            else
                CargarGrilla("Activo");
        }
        #endregion

        #region 3. ESTILOS VISUALES (UI)
        private void ConfigurarTemaOscuro()
        {
            // El Dashboard ya maneja la escala física del contenedor.
            // Aquí solo recuperamos el tamaño de la fuente para los textos.
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            // Fondo general del formulario
            this.BackColor = Color.FromArgb(25, 28, 35);

            AplicarTemaOscuroRecursivo(this, fuenteActual);
            ConfigurarTemaOscuroGrilla(dgvUsuarios, fuenteActual);
        }

        private void AplicarTemaOscuroRecursivo(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Panel || c is GroupBox)
                {
                    c.BackColor = Color.FromArgb(35, 39, 47); // Gris panel elevado
                    c.ForeColor = Color.White;
                }
                else if (c is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                    lbl.Font = new Font("Segoe UI", fuente, lbl.Font.Style);
                }
                else if (c is CheckBox chk)
                {
                    chk.ForeColor = Color.White;
                    chk.Font = new Font("Segoe UI", fuente, chk.Font.Style);
                }
                else if (c is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(50, 55, 65);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
                }
                else if (c is Button btn)
                {
                    btn.Font = new Font("Segoe UI", fuente, FontStyle.Bold); // NEGRITA
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;

                    // FIX: Forzamos la altura a 25 píxeles exactos
                    btn.Height = 25;

                    // Diferenciamos los botones por tipo de acción
                    if (btn.Name.Contains("Resetear"))
                    {
                        btn.BackColor = Color.IndianRed; // Rojo suave para acción crítica
                        btn.ForeColor = Color.White;
                    }
                    else // Nuevo, Editar, Estado...
                    {
                        btn.BackColor = Color.FromArgb(0, 229, 255); // Cian AsuFit
                        btn.ForeColor = Color.Black;
                    }
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c, fuente);
            }
        }

        private void ConfigurarTemaOscuroGrilla(DataGridView dgv, float fuente)
        {
            dgv.BackgroundColor = Color.FromArgb(25, 28, 35);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(50, 55, 65);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", fuente, FontStyle.Bold);

            // FIX: Quitar el azul nativo de Windows al tocar la cabecera
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(25, 28, 35);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 229, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 35;
        }

        // --- MÉTODO INTELIGENTE DE PLACEHOLDER ---
        private void AplicarPlaceholder(TextBox txt, string textoAyuda)
        {
            txt.Tag = textoAyuda;

            if (string.IsNullOrWhiteSpace(txt.Text) || txt.Text == textoAyuda)
            {
                txt.Text = textoAyuda;
                txt.ForeColor = Color.Silver;
            }
            else
            {
                txt.ForeColor = Color.White;
            }

            txt.Enter += delegate
            {
                if (txt.Text == textoAyuda)
                {
                    txt.Text = "";
                    txt.ForeColor = Color.White;
                }
            };

            txt.Leave += delegate
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = textoAyuda;
                    txt.ForeColor = Color.Silver;
                }
            };
        }
        #endregion

        #region 4. MÉTODOS AUXILIARES DE FORMULARIO EMERGENTE
        private void PrepararFormularioComoDashboard(Form frm)
        {
            float escalaActual = Properties.Settings.Default.EscalaInterfaz;
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            frm.Scale(new SizeF(escalaActual, escalaActual));
            AjustarFuentesPopup(frm, fuenteActual);

            frm.StartPosition = FormStartPosition.Manual;

            if (this.Parent != null)
            {
                Point posicionPanelAbsoluta = this.Parent.PointToScreen(Point.Empty);
                int x = posicionPanelAbsoluta.X + (this.Parent.Width - frm.Width) / 2;
                int y = posicionPanelAbsoluta.Y + (this.Parent.Height - frm.Height) / 2;

                frm.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterParent;
            }
        }

        private void AjustarFuentesPopup(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is TextBox || c is ComboBox || c is Label || c is NumericUpDown || c is Button)
                {
                    if (c is Button) c.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
                    else c.Font = new Font("Segoe UI", fuente, c.Font.Style);
                }
                if (c.HasChildren) AjustarFuentesPopup(c, fuente);
            }
        }
        #endregion

        #region 5. BÚSQUEDA Y FILTROS
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.DataSource is DataTable dt)
            {
                string textoBusqueda = txtBuscar.Text;

                // Evitamos buscar el texto del placeholder
                if (textoBusqueda == "Buscar por Usuario, Nombre o Rol...") textoBusqueda = "";

                dt.DefaultView.RowFilter = $"NombreCompleto LIKE '%{textoBusqueda}%' OR Username LIKE '%{textoBusqueda}%'";

                // FIX: Usamos 'dt' en lugar de 'dgvUsuarios' para contar los registros
                lblTotal.Text = "Registros encontrados: " + dt.DefaultView.Count.ToString();
            }
        }

        private void chkMostrarInactivos_CheckedChanged(object sender, EventArgs e)
        {
            RecargarGrilla();
        }
        #endregion

        #region 6. SECCIÓN CENTRAL: GRILLA DE USUARIOS
        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idUsuarioSeleccionado = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells["colUsuarioId"].Value);
            }
        }

        private void dgvUsuarios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvUsuarios.ClearSelection();
        }

        private void frmGestionUsuarios_Click(object sender, EventArgs e)
        {
            dgvUsuarios.ClearSelection();
            idUsuarioSeleccionado = 0;
        }
        #endregion

        #region 7. SECCIÓN INFERIOR: ACCIONES DE GESTIÓN
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmRegistrarUsuario frm = new frmRegistrarUsuario(true);

            // Centrado y escalado perfecto
            PrepararFormularioComoDashboard(frm);

            frm.ShowDialog();
            RecargarGrilla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado > 0)
            {
                Usuario userSeleccionado = new Usuario()
                {
                    IdUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["colUsuarioId"].Value),
                    NombreCompleto = dgvUsuarios.CurrentRow.Cells["colUsuarioNombre"].Value.ToString(),
                    Username = dgvUsuarios.CurrentRow.Cells["colUsuarioUsername"].Value.ToString(),
                    Rol = dgvUsuarios.CurrentRow.Cells["colUsuarioRol"].Value.ToString(),
                    Email = dgvUsuarios.CurrentRow.Cells["colUsuarioEmail"].Value.ToString(),
                    Estado = dgvUsuarios.CurrentRow.Cells["colUsuarioEstado"].Value.ToString()
                };

                frmRegistrarUsuario frm = new frmRegistrarUsuario(userSeleccionado);

                // Centrado y escalado perfecto
                PrepararFormularioComoDashboard(frm);

                frm.ShowDialog();
                RecargarGrilla();
            }
            else
            {
                MessageBox.Show("Por favor, seleccioná el usuario que querés editar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado > 0)
            {
                int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["colUsuarioId"].Value);
                string estadoActual = dgvUsuarios.CurrentRow.Cells["colUsuarioEstado"].Value.ToString();
                string nombreUsuario = dgvUsuarios.CurrentRow.Cells["colUsuarioUsername"].Value.ToString();

                string nuevoEstado = estadoActual == "Activo" ? "Inactivo" : "Activo";

                DialogResult pregunta = MessageBox.Show($"¿Estás seguro que querés cambiar el estado del usuario '{nombreUsuario}' a {nuevoEstado}?",
                                                        "Confirmar Cambio de Estado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (pregunta == DialogResult.Yes)
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    if (negocio.CambiarEstado(idUsuario, nuevoEstado))
                    {
                        GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Usuarios", "Cambio de Estado", $"Cambió el estado de '{nombreUsuario}' a {nuevoEstado}.");
                        MessageBox.Show($"El usuario ahora está {nuevoEstado}.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RecargarGrilla();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccioná un usuario de la tabla haciendo clic en la fila.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnResetearClave_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado > 0)
            {
                int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["colUsuarioId"].Value);
                string nombreUsuario = dgvUsuarios.CurrentRow.Cells["colUsuarioUsername"].Value.ToString();

                DialogResult pregunta = MessageBox.Show($"¿Deseás restablecer la contraseña del usuario '{nombreUsuario}' a '12345'?",
                                                        "Confirmar Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (pregunta == DialogResult.Yes)
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    if (negocio.ResetearClave(idUsuario))
                    {
                        GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Usuarios", "Reset de Clave", $"Restableció la contraseña de '{nombreUsuario}'.");
                        MessageBox.Show("Contraseña restablecida con éxito a: 12345", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccioná un usuario de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}