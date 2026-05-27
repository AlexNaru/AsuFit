using AsuFit.Datos;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmAuditoria : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private DataTable dtAuditoria = new DataTable();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmAuditoria()
        {
            InitializeComponent();
            dgvAuditoria.AutoGenerateColumns = false;
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmAuditoria_Load(object sender, EventArgs e)
        {
            if (cmbFiltroModulo.Items.Count > 0)
                cmbFiltroModulo.SelectedIndex = 0;

            // 1. Configuramos los selectores para que por defecto muestren solo el día de hoy
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;

            // 2. Conectamos el evento manualmente para evitar múltiples recargas al abrir la ventana
            dtpDesde.ValueChanged += new EventHandler(FiltrosFecha_ValueChanged);
            dtpHasta.ValueChanged += new EventHandler(FiltrosFecha_ValueChanged);

            CargarAuditoria();

            SendMessage(txtBuscar.Handle, EM_SETCUEBANNER, 1, "Buscar por usuario, acción o detalle...");

            // Forzamos el foco a la barra de búsqueda para que la grilla no se auto-seleccione
            txtBuscar.Focus();
        }

        private void CargarAuditoria()
        {
            try
            {
                AsuFit.Negocio.AuditoriaNegocio negocio = new AsuFit.Negocio.AuditoriaNegocio();

                // Ejecutamos la consulta a la base de datos enviando el rango de fechas exacto
                dtAuditoria = negocio.ListarAuditoria(dtpDesde.Value, dtpHasta.Value);

                dgvAuditoria.DataSource = dtAuditoria;

                // Aplicamos los filtros locales de texto y módulo si los hubiera
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la auditoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 3. SECCIÓN SUPERIOR: FILTROS Y BÚSQUEDA
        private void FiltrosFecha_ValueChanged(object sender, EventArgs e)
        {
            // Si el usuario cambia los días, vamos a la BD a buscar la nueva información
            CargarAuditoria();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void cmbFiltroModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            if (dtAuditoria == null || dtAuditoria.Rows.Count == 0) return;

            string modulo = cmbFiltroModulo.Text;
            string busqueda = txtBuscar.Text.Trim();
            string filtro = "1=1";

            if (modulo != "Todos" && !string.IsNullOrEmpty(modulo))
            {
                filtro += $" AND Modulo = '{modulo}'";
            }

            if (!string.IsNullOrEmpty(busqueda))
            {
                filtro += $" AND (Usuario LIKE '%{busqueda}%' OR Accion LIKE '%{busqueda}%' OR Detalle LIKE '%{busqueda}%')";
            }

            dtAuditoria.DefaultView.RowFilter = filtro;
        }
        #endregion

        #region 4. SECCIÓN CENTRAL Y ACCIONES: GRILLA
        private void dgvAuditoria_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvAuditoria.ClearSelection();
            dgvAuditoria.CurrentCell = null; // Obliga a la grilla a no tener ninguna celda activa
        }

        private void frmAuditoria_Click(object sender, EventArgs e)
        {
            dgvAuditoria.ClearSelection();
            dgvAuditoria.CurrentCell = null;
        }

        private void btnAbrirHistorial_Click(object sender, EventArgs e)
        {
            frmHistorialArqueos frm = new frmHistorialArqueos();
            frm.ShowDialog();
        }
        #endregion
    }
}