using AsuFit.Datos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmDetalleTransaccion : Form
    {
        private int idTransaccion;

        public frmDetalleTransaccion(int id, string cliente)
        {
            InitializeComponent();
            this.idTransaccion = id;

            // Le ponemos el título a la ventana automáticamente
            this.Text = $"Detalle Transacción N° {id} - Cliente: {cliente}";

            ConfigurarTemaOscuroGrilla(dgvDetalle);
        }

        // Aplica el estilo visual del sistema a la grilla de detalles
        private void ConfigurarTemaOscuroGrilla(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(25, 28, 35);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(50, 55, 65);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(25, 28, 35);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(25, 28, 35);

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 35;
        }

        private void frmDetalleTransaccion_Load(object sender, EventArgs e)
        {

            CargarDetalles();
        }

        private void CargarDetalles()
        {
            try
            {
                DataTable dtDetalle = new DataTable();
                using (SqlConnection oConexion = Conexion.ObtenerConexion())
                {
                    string query = "SELECT Concepto, Cantidad, PrecioUnitario AS Precio, SubTotal FROM VentasDetalle WHERE IdVenta = @IdVenta";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@IdVenta", idTransaccion);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtDetalle);
                }

                dgvDetalle.DataSource = dtDetalle;

                // Aplicamos el formato visual a los números (los puntos de miles)
                if (dgvDetalle.Columns.Contains("Precio")) dgvDetalle.Columns["Precio"].DefaultCellStyle.Format = "N0";
                if (dgvDetalle.Columns.Contains("SubTotal")) dgvDetalle.Columns["SubTotal"].DefaultCellStyle.Format = "N0";
                if (dgvDetalle.Columns.Contains("Cantidad")) dgvDetalle.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvDetalle.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvDetalle.ClearSelection();
        }
    }
}