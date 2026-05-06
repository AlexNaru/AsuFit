using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Runtime.InteropServices; // <-- Fundamental para el Cue Banner
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmRegistrarCobro : Form
    {
        private int idSocioSeleccionado = 0;
        private int diasPlanSeleccionado = 0;
        private Usuario usuarioActual;

        // --- MAGIA DEL PLACEHOLDER (CUE BANNER) ---
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmRegistrarCobro(Usuario userLogueado)
        {
            InitializeComponent();

            // 1. Aplicamos el placeholder nativo a tu barra de búsqueda
            SendMessage(txtBuscar.Handle, EM_SETCUEBANNER, 1, "Buscar por Cédula, Nombre o Apellido...");

            // 2. SOLUCIÓN ANTI-PANTALLA BLANCA: Cargamos todo directo al nacer
            CargarGrillaSocios();
            CargarPlanes();
        }

        // --- 1. CARGA DE DATOS ---
        private void CargarGrillaSocios()
        {
            SocioNegocio negocio = new SocioNegocio();
            // Traemos a todos (activos e inactivos)
            dgvSocios.DataSource = negocio.ListarSocios("Activo");

            // Ocultamos columnas técnicas
            if (dgvSocios.Columns["IdSocio"] != null) dgvSocios.Columns["IdSocio"].Visible = false;
            if (dgvSocios.Columns["IdPlan"] != null) dgvSocios.Columns["IdPlan"].Visible = false;

            dgvSocios.Columns["Email"].Visible = false;
            dgvSocios.Columns["Telefono"].Visible = false;
            dgvSocios.Columns["FechaNacimiento"].Visible = false;
            dgvSocios.Columns["NombreContactoEmergencia"].Visible = false;
            dgvSocios.Columns["TelefonoEmergencia"].Visible = false;
            dgvSocios.Columns["FechaRegistro"].Visible = false;

            dgvSocios.ClearSelection();
            idSocioSeleccionado = 0;
        }

        private void CargarPlanes()
        {
            PlanNegocio negocio = new PlanNegocio();
            cmbPlanes.DataSource = negocio.ListarPlanes("Activo");
            cmbPlanes.DisplayMember = "NombrePlan";
            cmbPlanes.ValueMember = "IdPlan";
            cmbPlanes.SelectedIndex = -1; // -1 significa que arranca vacío
        }

        // --- 2. EVENTOS DE INTERACCIÓN ---
        private void dgvSocios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idSocioSeleccionado = Convert.ToInt32(dgvSocios.Rows[e.RowIndex].Cells["IdSocio"].Value);
            }
        }

        private void cmbPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlanes.SelectedIndex != -1 && cmbPlanes.SelectedItem is Plan planSel)
            {
                txtMonto.Text = planSel.Precio.ToString("N0");
                diasPlanSeleccionado = planSel.DuracionDias;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvSocios.DataSource is DataTable dt)
            {
                // Filtro en tiempo real (idéntico al de tu gestión de socios)
                dt.DefaultView.RowFilter = $"Cedula LIKE '%{txtBuscar.Text}%' OR Apellido LIKE '%{txtBuscar.Text}%' OR Nombre LIKE '%{txtBuscar.Text}%'";
            }
        }

        // --- 3. PROCESAR EL COBRO ---
        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (idSocioSeleccionado == 0)
            {
                MessageBox.Show("Por favor, selecciona un socio de la tabla.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbPlanes.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                MessageBox.Show("Debes seleccionar un plan para realizar el cobro.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Cargamos los datos en el Carrito Global (La Nube)
            decimal monto = Convert.ToDecimal(txtMonto.Text.Replace(".", ""));
            string conceptoPlan = "Renovación: " + cmbPlanes.Text;

            // Extraemos el ID del Plan para crear su código artificial
            int idDelPlan = Convert.ToInt32(cmbPlanes.SelectedValue);
            string codigoPlanArtificial = "PLAN-" + idDelPlan;

            // ¡Usamos el nuevo parámetro!
            CarritoGlobal.AgregarItem(0, codigoPlanArtificial, conceptoPlan, 1, monto);
            CarritoGlobal.IdSocioPagara = idSocioSeleccionado;

            // 2. Cerramos cualquier caja que haya quedado abierta para refrescarla
            Form cajaAbierta = Application.OpenForms["frmCajaCobro"];
            if (cajaAbierta != null) cajaAbierta.Close();

            // 3. Abrimos la nueva Caja de forma independiente
            frmCajaCobro nuevaCaja = new frmCajaCobro(usuarioActual);

            // 4. SENSOR: Solo si la venta se confirma, actualizamos los días en SQL
            nuevaCaja.FormClosed += (s, args) =>
            {
                if (nuevaCaja.DialogResult == DialogResult.OK)
                {
                    PlanNegocio negocioPlan = new PlanNegocio();
                    Plan planInfo = negocioPlan.ObtenerPlanPorNombre(cmbPlanes.Text);

                    if (planInfo != null)
                    {
                        SocioNegocio negocioSocio = new SocioNegocio();
                        negocioSocio.RenovarMembresiaSocio(idSocioSeleccionado, planInfo.DuracionDias);
                    }

                    // Limpieza visual de la pantalla de socios
                    idSocioSeleccionado = 0;
                    txtMonto.Clear();
                    cmbPlanes.SelectedIndex = -1;
                    txtBuscar.Clear();
                    dgvSocios.ClearSelection();
                    // Si tienes un método para refrescar la tabla de socios, llámalo aquí
                }
            };

            nuevaCaja.Show();
        }

        private void dgvSocios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSocios.Columns.Contains("FechaVencimiento"))
            {
                var celdaFecha = dgvSocios.Rows[e.RowIndex].Cells["FechaVencimiento"].Value;

                if (celdaFecha != null && celdaFecha != DBNull.Value)
                {
                    DateTime fechaVencimiento = Convert.ToDateTime(celdaFecha);
                    TimeSpan diferencia = fechaVencimiento.Date - DateTime.Now.Date; // Calculamos la diferencia en días

                    // 1. CONDICIÓN ROJA (Vencidos: La fecha ya pasó)
                    if (fechaVencimiento.Date < DateTime.Now.Date)
                    {
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                    }
                    // 2. CONDICIÓN AMARILLA (Alerta: Faltan 7 días o menos, incluyendo HOY)
                    else if (diferencia.TotalDays >= 0 && diferencia.TotalDays <= 7)
                    {
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Gold;
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
        }

        private void dgvSocios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvSocios.ClearSelection();
        }

        private void frmRegistrarCobro_Click(object sender, EventArgs e)
        {
            // Asegurate de poner el nombre correcto de tu grilla si no es dgvSocios
            dgvSocios.ClearSelection();
            idSocioSeleccionado = 0;
        }
    }
}