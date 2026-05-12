using System;
using System.Windows.Forms;
using AsuFit.Entidades;
using AsuFit.Negocio;
using AsuFit.Datos;

namespace AsuFit.Presentacion
{
    public partial class frmGestionGastos : Form
    {
        public frmGestionGastos()
        {
            InitializeComponent();
        }

        private void frmGestionGastos_Load(object sender, EventArgs e)
        {
            CargarGrillaGastos();
        }

        private void CargarGrillaGastos()
        {
            try
            {
                GastoNegocio negocio = new GastoNegocio();
                dgvGastos.DataSource = negocio.ListarGastos();

                if (dgvGastos.Columns.Count > 0)
                {
                    // Ocultamos datos técnicos o redundantes
                    if (dgvGastos.Columns.Contains("IdGasto")) dgvGastos.Columns["IdGasto"].Visible = false;

                    dgvGastos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar gastos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- EL EVENTO PARA EVITAR LA SELECCIÓN AUTOMÁTICA QUE VIMOS HOY ---
        private void dgvGastos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvGastos.ClearSelection();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Gasto nuevoGasto = new Gasto();
                nuevoGasto.Descripcion = txtDescripcion.Text.Trim();
                nuevoGasto.Categoria = cmbCategoria.Text;
                nuevoGasto.Monto = Convert.ToDecimal(txtMonto.Text.Trim().Replace(".", ""));
                // Asumimos que el usuario actual se llama "Admin" (esto luego lo conectamos con tu Login real)
                nuevoGasto.UsuarioRegistra = "Admin";

                GastoNegocio negocio = new GastoNegocio();
                string mensaje;

                if (negocio.RegistrarGasto(nuevoGasto, out mensaje))
                {
                    GestorAuditoria.Registrar("Admin", "Gastos", "Registro", $"Gasto de Gs. {nuevoGasto.Monto:N0} en {nuevoGasto.Categoria}.");

                    MessageBox.Show("Gasto registrado correctamente en la caja.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiamos los campos para el siguiente gasto
                    txtDescripcion.Clear();
                    txtMonto.Clear();
                    cmbCategoria.SelectedIndex = -1;

                    // Recargamos la tabla para ver el nuevo registro
                    CargarGrillaGastos();
                }
                else
                {
                    MessageBox.Show(mensaje, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingresá un monto numérico válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvGastos_DataBindingComplete_1(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvGastos.ClearSelection();
        }

        private void frmGestionGastos_Click(object sender, EventArgs e)
        {
            // Cambiá el nombre si tu grilla se llama distinto
            dgvGastos.ClearSelection();
        }
    }
}