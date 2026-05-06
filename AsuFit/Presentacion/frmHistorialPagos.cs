using System;
using System.Data;
using System.Windows.Forms;
using AsuFit.Negocio;
using System.Runtime.InteropServices;

namespace AsuFit.Presentacion
{
    public partial class frmHistorialPagos : Form
    {
        // --- MAGIA DEL PLACEHOLDER (CUE BANNER) ---
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmHistorialPagos()
        {
            InitializeComponent();
            // Aplicamos el texto fantasma
            SendMessage(txtBuscar.Handle, EM_SETCUEBANNER, 1, "Buscar por Cédula, Nombre o Apellido...");
        }

        private void frmHistorialPagos_Load(object sender, EventArgs e)
        {
            // Por defecto, cargamos desde el día 1 del mes actual hasta hoy
            dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpHasta.Value = DateTime.Now;

            CargarHistorialBase();
        }

        // --- 1. CARGA DESDE LA BASE DE DATOS (Solo por fechas) ---
        private void CargarHistorialBase()
        {
            try
            {
                PagoNegocio negocio = new PagoNegocio();
                DataTable dt = negocio.ListarHistorialPagos(dtpDesde.Value, dtpHasta.Value, "");

                dgvHistorial.DataSource = dt;

                if (dgvHistorial.Columns.Count > 0)
                {
                    dgvHistorial.Columns["IdPago"].Visible = false;
                    dgvHistorial.Columns["Monto"].DefaultCellStyle.Format = "N0";
                    dgvHistorial.Columns["FechaPago"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }

                dgvHistorial.ClearSelection();

                // --- AGREGAMOS ESTO AQUÍ ---
                CalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvHistorial.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = $"Cedula LIKE '%{txtBuscar.Text}%' OR Socio LIKE '%{txtBuscar.Text}%' OR Concepto LIKE '%{txtBuscar.Text}%'";

                // --- Y AGREGAMOS ESTO AQUÍ TAMBIÉN ---
                CalcularTotales();
            }
        }

        // --- 3. ACTUALIZAR SI CAMBIAN LAS FECHAS ---
        // (Enlazá estos eventos en el rayito amarillo de tus dos DateTimePicker)
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            CargarHistorialBase();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            CargarHistorialBase();
        }

        private void CalcularTotales()
        {
            int cantidad = 0;
            decimal totalDinero = 0;

            foreach (DataGridViewRow fila in dgvHistorial.Rows)
            {
                // ¡LA CONDICIÓN MÁGICA! Solo contamos si NO es la fila fantasma
                if (!fila.IsNewRow)
                {
                    cantidad++;
                    if (fila.Cells["Monto"].Value != null && fila.Cells["Monto"].Value != DBNull.Value)
                    {
                        totalDinero += Convert.ToDecimal(fila.Cells["Monto"].Value);
                    }
                }
            }

            lblTotalRegistros.Text = cantidad.ToString();
            lblTotalDinero.Text = totalDinero.ToString("N0");
        }

        private void dgvHistorial_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvHistorial.ClearSelection();
        }

        private void frmHistorialPagos_Click(object sender, EventArgs e)
        {
            dgvHistorial.ClearSelection();
        }
    }
}