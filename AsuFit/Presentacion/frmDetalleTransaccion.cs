using AsuFit.Datos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmDetalleTransaccion : Form
    {
        private int idTransaccion;

        // Modificamos el constructor para que exija el ID y el Nombre del cliente al abrirse
        public frmDetalleTransaccion(int id, string cliente)
        {
            InitializeComponent();
            this.idTransaccion = id;

            // Le ponemos el título a la ventana automáticamente
            this.Text = $"Detalle Transacción N° {id} - Cliente: {cliente}";
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}