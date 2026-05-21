using AsuFit.Datos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmAuditoria : Form
    {
        private DataTable dtAuditoria = new DataTable();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmAuditoria()
        {
            InitializeComponent();

            // Bloqueamos las columnas automáticas
            dgvAuditoria.AutoGenerateColumns = false;
        }

        private void frmAuditoria_Load(object sender, EventArgs e)
        {
            if (cmbFiltroModulo.Items.Count > 0)
                cmbFiltroModulo.SelectedIndex = 0;

            CargarAuditoria();

            SendMessage(txtBuscar.Handle, EM_SETCUEBANNER, 1, "Buscar por usuario, acción o detalle...");
        }

        private void btnAbrirHistorial_Click(object sender, EventArgs e)
        {
            frmHistorialArqueos frm = new frmHistorialArqueos();
            frm.ShowDialog();
        }

        private void CargarAuditoria()
        {
            try
            {
                AsuFit.Negocio.AuditoriaNegocio negocio = new AsuFit.Negocio.AuditoriaNegocio();
                dtAuditoria = negocio.ListarAuditoria();

                dgvAuditoria.DataSource = dtAuditoria;
                dgvAuditoria.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la auditoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltros()
        {
            if (dtAuditoria == null || dtAuditoria.Rows.Count == 0) return;

            string modulo = cmbFiltroModulo.Text;
            string busqueda = txtBuscar.Text.Trim();
            string filtro = "1=1";

            // Buscamos 'Modulo' sin tilde, tal como está en SQL
            if (modulo != "Todos" && !string.IsNullOrEmpty(modulo))
            {
                filtro += $" AND Modulo = '{modulo}'";
            }

            // Buscamos 'Accion' sin tilde, tal como está en SQL
            if (!string.IsNullOrEmpty(busqueda))
            {
                filtro += $" AND (Usuario LIKE '%{busqueda}%' OR Accion LIKE '%{busqueda}%' OR Detalle LIKE '%{busqueda}%')";
            }

            dtAuditoria.DefaultView.RowFilter = filtro;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void cmbFiltroModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void dgvAuditoria_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvAuditoria.ClearSelection();
        }
    }
}