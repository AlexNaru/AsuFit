using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
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
            usuarioActual = userLogueado;
            dgvPlanes.AutoGenerateColumns = false;
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmGestionPlanes_Load(object sender, EventArgs e)
        {
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

        #region 3. SECCIÓN CENTRAL: GRILLA DE PLANES
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

        #region 4. SECCIÓN INFERIOR: ACCIONES
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmRegistrarPlan ventanaRegistro = new frmRegistrarPlan(usuarioActual);
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