using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmGestionPlanes : Form
    {
        private int idPlanSeleccionado = 0;
        private Usuario usuarioActual;

        public frmGestionPlanes(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;

            // --- EL CAMBIO CLAVE: Bloqueamos las columnas automáticas ---
            dgvPlanes.AutoGenerateColumns = false;
        }

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

                // Al asignar el DataSource, los datos se acomodarán solos según el DataPropertyName
                dgvPlanes.DataSource = negocio.ListarPlanes(estadoFiltro);

                dgvPlanes.ClearSelection();
                idPlanSeleccionado = 0;

                // Actualizamos la etiqueta con el total de planes encontrados
                int cantidad = dgvPlanes.Rows.Count;
                lblTotal.Text = "Planes encontrados: " + cantidad.ToString();
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
                // Capturamos el ID usando el Name de la columna
                idPlanSeleccionado = Convert.ToInt32(dgvPlanes.Rows[e.RowIndex].Cells["colPlanId"].Value);
            }
        }

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

                // --- CÓDIGO LIMPIO RESTAURADO ---
                // Como los DataPropertyName ya están correctos, podemos convertir directamente
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

        private void dgvPlanes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvPlanes.ClearSelection();
        }

        private void frmGestionPlanes_Click(object sender, EventArgs e)
        {
            dgvPlanes.ClearSelection();
            idPlanSeleccionado = 0;
        }
    }
}