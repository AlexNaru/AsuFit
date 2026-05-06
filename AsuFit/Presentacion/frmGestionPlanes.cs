using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmGestionPlanes : Form
    {
        private int idPlanSeleccionado = 0;

        public frmGestionPlanes()
        {
            InitializeComponent();
        }

        private void frmGestionPlanes_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        // --- NUEVO: EVENTO DEL CHECKBOX ---
        private void chkMostrarInactivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarGrilla(); // Cada vez que tildas/destildas, se recarga la tabla
        }

        private void CargarGrilla()
        {
            try
            {
                PlanNegocio negocio = new PlanNegocio();

                // --- CAMBIO APLICADO: Filtro por estado ---
                string estadoFiltro = chkMostrarInactivos.Checked ? "Inactivo" : "Activo";
                dgvPlanes.DataSource = negocio.ListarPlanes(estadoFiltro);

                if (dgvPlanes.Columns.Count > 0)
                {
                    dgvPlanes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    // Ocultamos el ID para limpiar la vista
                    dgvPlanes.Columns["IdPlan"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de planes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPlanes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvPlanes.Rows[e.RowIndex];
                idPlanSeleccionado = Convert.ToInt32(fila.Cells["IdPlan"].Value);
                txtNombrePlan.Text = fila.Cells["NombrePlan"].Value.ToString();
                txtPrecio.Text = Math.Round(Convert.ToDecimal(fila.Cells["Precio"].Value), 0).ToString();
                txtDuracionDias.Text = fila.Cells["DuracionDias"].Value.ToString();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Plan nuevoPlan = new Plan();
                nuevoPlan.NombrePlan = txtNombrePlan.Text.Trim();
                nuevoPlan.Precio = Convert.ToDecimal(txtPrecio.Text.Trim());
                nuevoPlan.DuracionDias = Convert.ToInt32(txtDuracionDias.Text.Trim());

                PlanNegocio negocio = new PlanNegocio();
                string mensaje;
                if (negocio.RegistrarPlan(nuevoPlan, out mensaje))
                {
                    MessageBox.Show("¡Plan guardado con éxito!", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarGrilla();
                }
                else
                {
                    MessageBox.Show(mensaje, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingresá solo números en Precio y Días.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idPlanSeleccionado == 0)
            {
                MessageBox.Show("Por favor, seleccioná un plan de la lista con un clic para editarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Plan planActual = new Plan();
            planActual.IdPlan = idPlanSeleccionado;
            planActual.NombrePlan = txtNombrePlan.Text.Trim();
            planActual.Precio = Convert.ToDecimal(txtPrecio.Text.Trim());
            planActual.DuracionDias = Convert.ToInt32(txtDuracionDias.Text.Trim());

            frmEditarPlan ventanaEdicion = new frmEditarPlan(planActual);
            if (ventanaEdicion.ShowDialog() == DialogResult.OK)
            {
                PlanNegocio negocio = new PlanNegocio();
                string mensaje;

                if (negocio.EditarPlan(ventanaEdicion.PlanAEditar, out mensaje))
                {
                    MessageBox.Show("¡Plan editado con éxito!", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarGrilla();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idPlanSeleccionado == 0)
            {
                MessageBox.Show("Por favor, seleccioná un plan de la lista con un clic para eliminarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult advertencia = MessageBox.Show("¿Estás seguro de que querés eliminar este plan?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (advertencia == DialogResult.Yes)
            {
                PlanNegocio negocio = new PlanNegocio();
                string mensaje;

                if (negocio.EliminarPlan(idPlanSeleccionado, out mensaje))
                {
                    MessageBox.Show("El plan fue eliminado del sistema.", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarGrilla();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtNombrePlan.Clear();
            txtPrecio.Clear();
            txtDuracionDias.Clear();
            idPlanSeleccionado = 0;
            txtNombrePlan.Focus();
        }

        private void dgvPlanes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvPlanes.ClearSelection();
        }

        private void frmGestionPlanes_Click(object sender, EventArgs e)
        {
            // 1. Desmarcamos la fila
            dgvPlanes.ClearSelection();
            idPlanSeleccionado = 0;

            // 2. Limpiamos los textos (ajustá los nombres a tus TextBoxes)
            txtNombrePlan.Clear();
            txtPrecio.Clear();
            txtDuracionDias.Clear();
        }
    }
}