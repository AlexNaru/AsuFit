using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AsuFit.Entidades;
using AsuFit.Negocio;
using AsuFit.Datos;

namespace AsuFit.Presentacion
{
    public partial class frmGestionGastos : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmGestionGastos()
        {
            InitializeComponent();
            dgvGastos.AutoGenerateColumns = false;
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmGestionGastos_Load(object sender, EventArgs e)
        {
            CargarGrillaGastos();

            // Textos de ayuda de fondo para los campos de entrada
            SendMessage(txtDescripcion.Handle, EM_SETCUEBANNER, 1, "Ej: Pago de internet, insumos...");
            SendMessage(txtMonto.Handle, EM_SETCUEBANNER, 1, "Ej: 150000");
        }

        private void CargarGrillaGastos()
        {
            try
            {
                GastoNegocio negocio = new GastoNegocio();
                dgvGastos.DataSource = negocio.ListarGastos();
                dgvGastos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar gastos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 3. SECCIÓN SUPERIOR: GRILLA DE GASTOS
        private void dgvGastos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvGastos.ClearSelection();
        }

        private void frmGestionGastos_Click(object sender, EventArgs e)
        {
            dgvGastos.ClearSelection();
        }
        #endregion

        #region 4. SECCIÓN INFERIOR: REGISTRO DE NUEVO GASTO
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

                // Asignación estática temporal del usuario que registra el movimiento
                nuevoGasto.UsuarioRegistra = "Admin";

                GastoNegocio negocio = new GastoNegocio();
                string mensaje;

                if (negocio.RegistrarGasto(nuevoGasto, out mensaje))
                {
                    GestorAuditoria.Registrar("Admin", "Gastos", "Registro", $"Gasto de Gs. {nuevoGasto.Monto:N0} en {nuevoGasto.Categoria}.");

                    MessageBox.Show("Gasto registrado correctamente en la caja.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtDescripcion.Clear();
                    txtMonto.Clear();
                    cmbCategoria.SelectedIndex = -1;

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
        #endregion
    }
}