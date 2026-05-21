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

            // --- EL CAMBIO CLAVE: Bloqueamos las columnas automáticas ---
            dgvGastos.AutoGenerateColumns = false;
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

                // Al asignar el DataSource, los datos buscarán los DataPropertyName configurados visualmente
                dgvGastos.DataSource = negocio.ListarGastos();

                // --- CÓDIGO LIMPIO: Ya no ocultamos columnas ni damos formato desde aquí ---

                dgvGastos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar gastos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || string.IsNullOrWhiteSpace(txtMonto.Text) || cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, complete todos los campos antes de guardar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Gasto nuevoGasto = new Gasto();
                nuevoGasto.Descripcion = txtDescripcion.Text.Trim();
                nuevoGasto.Categoria = cmbCategoria.Text;
                nuevoGasto.Monto = Convert.ToDecimal(txtMonto.Text.Trim().Replace(".", ""));

                // Asumimos que el usuario actual se llama "Admin" (luego lo conectaremos al Login)
                nuevoGasto.UsuarioRegistra = "Admin";

                GastoNegocio negocio = new GastoNegocio();
                string mensaje;

                if (negocio.RegistrarGasto(nuevoGasto, out mensaje))
                {
                    GestorAuditoria.Registrar("Admin", "Gastos", "Registro", $"Gasto de Gs. {nuevoGasto.Monto:N0} en {nuevoGasto.Categoria}.");

                    MessageBox.Show("Gasto registrado correctamente en la caja.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiamos los campos
                    txtDescripcion.Clear();
                    txtMonto.Clear();
                    cmbCategoria.SelectedIndex = -1;

                    // Recargamos la tabla
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

        private void dgvGastos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvGastos.ClearSelection();
        }

        private void frmGestionGastos_Click(object sender, EventArgs e)
        {
            dgvGastos.ClearSelection();
        }
    }
}