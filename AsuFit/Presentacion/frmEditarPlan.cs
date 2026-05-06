using AsuFit.Entidades;
using System;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmEditarPlan : Form
    {
        // Esta variable va a guardar el plan que nos manden desde la pantalla principal
        public Plan PlanAEditar { get; set; }

        // Modificamos el constructor para que reciba el Plan cuando se abre la ventana
        public frmEditarPlan(Plan plan)
        {
            InitializeComponent();
            PlanAEditar = plan;
        }

        // EVENTO: Al abrir la ventanita, cargamos los datos en los cuadros de texto
        private void frmEditarPlan_Load(object sender, EventArgs e)
        {
            txtNombrePlan.Text = PlanAEditar.NombrePlan;
            txtPrecio.Text = Math.Round(PlanAEditar.Precio, 0).ToString();
            txtDuracionDias.Text = PlanAEditar.DuracionDias.ToString();
        }

        // BOTÓN: GUARDAR
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Actualizamos el objeto con los datos nuevos que escribió el usuario
                PlanAEditar.NombrePlan = txtNombrePlan.Text.Trim();
                PlanAEditar.Precio = Convert.ToDecimal(txtPrecio.Text.Trim());
                PlanAEditar.DuracionDias = Convert.ToInt32(txtDuracionDias.Text.Trim());

                // Le avisamos a la pantalla principal que todo salió bien (OK) y cerramos
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor, verificá que el precio y la duración sean números válidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // BOTÓN: CANCELAR
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Le avisamos a la pantalla principal que cancelamos y cerramos
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}