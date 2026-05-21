using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmRegistrarCobro : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private int idSocioSeleccionado = 0;
        private int diasPlanSeleccionado = 0;
        private Usuario usuarioActual;

        // Inyección de librería nativa para el placeholder del buscador
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmRegistrarCobro(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;

            dgvSocios.AutoGenerateColumns = false;

            SendMessage(txtBuscar.Handle, EM_SETCUEBANNER, 1, "Buscar por Cédula, Nombre o Apellido...");

            CargarGrillaSocios();
            CargarPlanes();
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void CargarGrillaSocios()
        {
            SocioNegocio negocio = new SocioNegocio();
            dgvSocios.DataSource = negocio.ListarSocios("Activo");
            dgvSocios.ClearSelection();
            idSocioSeleccionado = 0;
        }

        private void CargarPlanes()
        {
            PlanNegocio negocio = new PlanNegocio();
            cmbPlanes.DataSource = negocio.ListarPlanes("Activo");
            cmbPlanes.DisplayMember = "NombrePlan";
            cmbPlanes.ValueMember = "IdPlan";
            cmbPlanes.SelectedIndex = -1;
        }
        #endregion

        #region 3. SECCIÓN SUPERIOR: BÚSQUEDA
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvSocios.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = $"Cedula LIKE '%{txtBuscar.Text}%' OR Apellido LIKE '%{txtBuscar.Text}%' OR Nombre LIKE '%{txtBuscar.Text}%'";
            }
        }
        #endregion

        #region 4. SECCIÓN CENTRAL: GRILLA Y SELECCIÓN
        private void dgvSocios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvSocios.ClearSelection();
        }

        private void dgvSocios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSocios.Columns.Contains("colCobroVencimiento"))
            {
                var celdaFecha = dgvSocios.Rows[e.RowIndex].Cells["colCobroVencimiento"].Value;

                if (celdaFecha != null && celdaFecha != DBNull.Value)
                {
                    DateTime fechaVencimiento = Convert.ToDateTime(celdaFecha);
                    TimeSpan diferencia = fechaVencimiento.Date - DateTime.Now.Date;

                    if (fechaVencimiento.Date < DateTime.Now.Date)
                    {
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if (diferencia.TotalDays >= 0 && diferencia.TotalDays <= 7)
                    {
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void dgvSocios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSocios.Columns.Contains("colCobroId"))
            {
                idSocioSeleccionado = Convert.ToInt32(dgvSocios.Rows[e.RowIndex].Cells["colCobroId"].Value);
            }
        }

        private void frmRegistrarCobro_Click(object sender, EventArgs e)
        {
            // Libera la selección al hacer clic en un área vacía del formulario
            dgvSocios.ClearSelection();
            idSocioSeleccionado = 0;
        }
        #endregion

        #region 5. SECCIÓN INFERIOR: SELECCIÓN DE PLAN Y COBRO
        private void cmbPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlanes.SelectedIndex != -1 && cmbPlanes.SelectedItem is Plan planSel)
            {
                txtMonto.Text = planSel.Precio.ToString("N0");
                diasPlanSeleccionado = planSel.DuracionDias;
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (idSocioSeleccionado == 0)
            {
                MessageBox.Show("Por favor, seleccione un socio de la tabla para registrar el cobro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbPlanes.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                MessageBox.Show("Por favor, seleccione el Plan que desea cobrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prevención de colisiones en el Carrito Global
            if (CarritoGlobal.Detalles.Rows.Count > 0 && CarritoGlobal.IdSocioPagara != null && CarritoGlobal.IdSocioPagara != idSocioSeleccionado)
            {
                DialogResult respuesta = MessageBox.Show(
                    "Ya hay conceptos en la caja a nombre de otro socio.\n\n¿Deseas agregar esta mensualidad para cobrar ambos planes juntos en el mismo ticket?\n(La factura saldrá a nombre del primer socio, pero ambos serán renovados en el sistema).",
                    "Cobro Múltiple Detectado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.No) return;
            }
            else
            {
                CarritoGlobal.IdSocioPagara = idSocioSeleccionado;
            }

            PlanNegocio negocioPlan = new PlanNegocio();
            Plan planInfo = negocioPlan.ObtenerPlanPorNombre(cmbPlanes.Text);
            if (planInfo == null) return;

            decimal monto = Convert.ToDecimal(txtMonto.Text.Replace(".", ""));
            string conceptoPlan = "Renovación: " + cmbPlanes.Text;

            // Integración con el Módulo de Caja
            string codigoPlanArtificial = $"PLAN-{planInfo.DuracionDias}-{idSocioSeleccionado}-{planInfo.IdPlan}";
            CarritoGlobal.AgregarItem(0, codigoPlanArtificial, conceptoPlan, 1, monto, 10);

            frmCajaCobro cajaAbierta = Application.OpenForms["frmCajaCobro"] as frmCajaCobro;
            if (cajaAbierta != null)
            {
                cajaAbierta.WindowState = FormWindowState.Normal;
                cajaAbierta.BringToFront();
                cajaAbierta.ActualizarPantallaDesdeCarrito();
            }
            else
            {
                frmCajaCobro nuevaCaja = new frmCajaCobro(usuarioActual);
                nuevaCaja.Show();
            }

            // Limpieza post-envío a caja
            idSocioSeleccionado = 0;
            txtMonto.Clear();
            cmbPlanes.SelectedIndex = -1;
            txtBuscar.Clear();
            dgvSocios.ClearSelection();
        }
        #endregion
    }
}