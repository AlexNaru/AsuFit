using AsuFit.Datos; // O el namespace donde tengas tu Conexion
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmAuditoria : Form
    {
        // Variable global para guardar los datos en memoria y buscar súper rápido
        private DataTable dtAuditoria = new DataTable();

        public frmAuditoria()
        {
            InitializeComponent();
        }

        // EVENTO LOAD: Se ejecuta al abrir la pantalla
        private void frmAuditoria_Load(object sender, EventArgs e)
        {
            // Ponemos el combo en "Todos" por defecto
            if (cmbFiltroModulo.Items.Count > 0)
                cmbFiltroModulo.SelectedIndex = 0;

            CargarAuditoria();

            // Evita que la primera celda se seleccione de color azul por defecto al abrir
            dgvAuditoria.ClearSelection();
        }

        private void btnAbrirHistorial_Click(object sender, EventArgs e)
        {
            // Abrimos la pantalla que ya tenés lista y blindada
            frmHistorialArqueos frm = new frmHistorialArqueos();
            frm.ShowDialog();
        }

        // MÉTODO PARA TRAER LA TABLA DE SQL
        private void CargarAuditoria()
        {
            try
            {
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    string query = "SELECT FechaHora AS [Fecha y Hora], Usuario, Modulo AS Módulo, Accion AS Acción, Detalle FROM LogAuditoria ORDER BY FechaHora DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, oConexion);

                    dtAuditoria.Clear();
                    da.Fill(dtAuditoria);

                    dgvAuditoria.DataSource = dtAuditoria;
                    dgvAuditoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // --- AJUSTES VISUALES ---
                    // 1. Quita la primera columna vacía de la izquierda (la de la flechita)
                    dgvAuditoria.RowHeadersVisible = false;

                    // 2. Quita la última fila vacía de abajo (la del asterisco)
                    dgvAuditoria.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la auditoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MOTOR DE BÚSQUEDA EN TIEMPO REAL
        private void AplicarFiltros()
        {
            if (dtAuditoria == null || dtAuditoria.Rows.Count == 0) return;

            string modulo = cmbFiltroModulo.Text;
            string busqueda = txtBuscar.Text.Trim();
            string filtro = "1=1";

            if (modulo != "Todos" && !string.IsNullOrEmpty(modulo))
            {
                filtro += $" AND Módulo = '{modulo}'"; // Usamos el alias de la consulta SQL
            }

            if (!string.IsNullOrEmpty(busqueda))
            {
                filtro += $" AND (Usuario LIKE '%{busqueda}%' OR Acción LIKE '%{busqueda}%' OR Detalle LIKE '%{busqueda}%')";
            }

            dtAuditoria.DefaultView.RowFilter = filtro;
        }

        // EVENTO: Cuando el usuario teclea en el cuadro de búsqueda
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        // EVENTO: Cuando el usuario cambia la opción del ComboBox
        private void cmbFiltroModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }
    }
}