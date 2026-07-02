using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmGestionPlanes : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private int idPlanSeleccionado = 0;
        private Usuario usuarioActual;

        public frmGestionPlanes(Usuario userLogueado)
        {
            InitializeComponent();

            this.Load += new EventHandler(frmGestionPlanes_Load);

            usuarioActual = userLogueado;
            dgvPlanes.AutoGenerateColumns = false;
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmGestionPlanes_Load(object sender, EventArgs e)
        {
            ConfigurarTemaOscuro();
            CargarGrilla();
        }

        private void chkMostrarInactivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            try
            {
                PlanNegocio negocio = new PlanNegocio();
                string estadoFiltro = chkMostrarInactivos.Checked ? "Inactivo" : "Activo";

                dgvPlanes.DataSource = negocio.ListarPlanes(estadoFiltro);

                dgvPlanes.ClearSelection();
                idPlanSeleccionado = 0;

                int cantidad = dgvPlanes.Rows.Count;
                lblTotal.Text = "Planes encontrados: " + cantidad.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de planes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 3. ESTILOS VISUALES (UI)
        private void ConfigurarTemaOscuro()
        {
            // Fondo general del formulario
            this.BackColor = Color.FromArgb(25, 28, 35);

            // Recorremos los controles para pintar paneles, textos y checkboxes
            foreach (Control c in this.Controls)
            {
                if (c is Panel || c is GroupBox)
                {
                    c.BackColor = Color.FromArgb(35, 39, 47); // Gris panel elevado
                    c.ForeColor = Color.White;

                    foreach (Control subC in c.Controls)
                    {
                        if (subC is Label lbl) lbl.ForeColor = Color.White;
                        if (subC is CheckBox chk) chk.ForeColor = Color.White;
                    }
                }
                else if (c is Label lbl) lbl.ForeColor = Color.White;
                else if (c is CheckBox chk) chk.ForeColor = Color.White;
            }

            // Estilos de los botones de acción
            Button[] botones = { btnNuevo, btnEditar, btnEstado };
            foreach (Button btn in botones)
            {
                if (btn != null)
                {
                    btn.BackColor = Color.FromArgb(0, 229, 255); // Cian AsuFit
                    btn.ForeColor = Color.Black;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                }
            }

            ConfigurarTemaOscuroGrilla(dgvPlanes);
        }

        private void ConfigurarTemaOscuroGrilla(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(25, 28, 35);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(50, 55, 65);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(25, 28, 35);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 229, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 35;
        }
        #endregion

        #region 4. SECCIÓN CENTRAL: GRILLA DE PLANES
        private void dgvPlanes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idPlanSeleccionado = Convert.ToInt32(dgvPlanes.Rows[e.RowIndex].Cells["colPlanId"].Value);
            }
        }

        private void dgvPlanes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvPlanes.ClearSelection();
        }

        private void frmGestionPlanes_Click(object sender, EventArgs e)
        {
            dgvPlanes.ClearSelection();
            idPlanSeleccionado = 0;
        }
        #endregion

        #region 5. MÉTODOS AUXILIARES DE FORMULARIO EMERGENTE
        // Configura la escala, fuente y posición del formulario emergente (Registrar/Editar Plan)
        private void PrepararFormularioComoDashboard(Form frm)
        {
            float escalaActual = Properties.Settings.Default.EscalaInterfaz;

            // Aplica la escala elegida por el usuario
            frm.Scale(new SizeF(escalaActual, escalaActual));
            AjustarFuentes(frm);

            frm.StartPosition = FormStartPosition.Manual;

            // Calcula la posición relativa al panel contenedor para un centrado exacto
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

        private void AjustarFuentes(Control contenedor)
        {
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            foreach (Control c in contenedor.Controls)
            {
                if (c is TextBox || c is ComboBox || c is Label || c is NumericUpDown || c is Button)
                {
                    c.Font = new Font("Segoe UI", fuenteActual, c.Font.Style);
                }
                else if (c.HasChildren)
                {
                    AjustarFuentes(c);
                }
            }
        }
        #endregion

        #region 6. SECCIÓN INFERIOR: ACCIONES
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmRegistrarPlan ventanaRegistro = new frmRegistrarPlan(usuarioActual);
            
            PrepararFormularioComoDashboard(ventanaRegistro);
            
            ventanaRegistro.ShowDialog();
            CargarGrilla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idPlanSeleccionado > 0)
            {
                DataGridViewRow fila = dgvPlanes.CurrentRow;

                Plan planSeleccionado = new Plan
                {
                    IdPlan = idPlanSeleccionado,
                    NombrePlan = fila.Cells["colPlanNombre"].Value.ToString(),
                    Precio = Convert.ToDecimal(fila.Cells["colPlanPrecio"].Value),
                    DuracionDias = Convert.ToInt32(fila.Cells["colPlanDuracion"].Value)
                };

                frmRegistrarPlan ventanaRegistro = new frmRegistrarPlan(planSeleccionado, usuarioActual);
                
                PrepararFormularioComoDashboard(ventanaRegistro);
                
                ventanaRegistro.ShowDialog();
                CargarGrilla();
            }
            else
            {
                MessageBox.Show("Por favor, seleccioná un plan de la tabla haciendo clic en la fila.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            if (idPlanSeleccionado == 0)
            {
                MessageBox.Show("Por favor, seleccioná un plan de la tabla primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nuevoEstado = chkMostrarInactivos.Checked ? "Activo" : "Inactivo";

            DialogResult pregunta = MessageBox.Show($"¿Está seguro que desea cambiar el estado de este plan a {nuevoEstado}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (pregunta == DialogResult.Yes)
            {
                PlanNegocio negocio = new PlanNegocio();
                string mensaje;

                if (negocio.CambiarEstadoPlan(idPlanSeleccionado, nuevoEstado, out mensaje))
                {
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Planes", "Cambio de Estado", $"Se cambió el estado del plan ID {idPlanSeleccionado} a {nuevoEstado}.");
                    MessageBox.Show("El estado del plan fue actualizado.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion
    }
}