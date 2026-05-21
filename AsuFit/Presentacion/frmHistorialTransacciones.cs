using AsuFit.Datos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmHistorialTransacciones : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmHistorialTransacciones()
        {
            InitializeComponent();
            dgvVentas.AutoGenerateColumns = false;
            cmbFiltroTipo.SelectedIndex = 0;
        }
        #endregion

        #region 2. INICIALIZACIÓN
        private void frmHistorialTransacciones_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpHasta.Value = DateTime.Now.Date;

            SendMessage(txtBuscar.Handle, EM_SETCUEBANNER, 1, "Buscar por N° de Transacción, Cliente o Cédula...");

            BuscarVentas();
        }
        #endregion

        #region 3. SECCIÓN SUPERIOR: FILTROS Y BÚSQUEDA
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarVentas();
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            BuscarVentas();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            BuscarVentas();
        }

        private void cmbFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscarVentas();
        }
        #endregion

        #region 4. SECCIÓN CENTRAL: PROCESAMIENTO DE DATOS Y GRILLA
        private void BuscarVentas()
        {
            if (cmbFiltroTipo.SelectedItem == null) return;
            string tipoFiltro = cmbFiltroTipo.SelectedItem.ToString();

            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                try
                {
                    string query = @"
                        WITH Clasificacion AS (
                            SELECT 
                                v.IdVenta,
                                SUM(CASE WHEN vd.IdProducto > 0 THEN 1 ELSE 0 END) as CantProductos,
                                SUM(CASE WHEN vd.IdProducto = 0 OR vd.IdProducto IS NULL THEN 1 ELSE 0 END) as CantMensualidades
                            FROM Ventas v
                            INNER JOIN VentasDetalle vd ON v.IdVenta = vd.IdVenta
                            GROUP BY v.IdVenta
                        )
                        SELECT 
                            v.IdVenta AS [N° Transacción],
                            v.Fecha AS [Fecha],
                            ISNULL(s.Nombre + ' ' + s.Apellido, 'Cliente Ocasional') AS [Cliente],
                            v.MetodoPago AS [Método],
                            CASE 
                                WHEN c.CantProductos > 0 AND c.CantMensualidades > 0 THEN 'Mixto (Cuota+Producto)'
                                WHEN c.CantMensualidades > 0 THEN 'Mensualidad'
                                ELSE 'Producto'
                            END AS [Tipo Operación],
                            v.Total AS [Total Cobrado]
                        FROM Ventas v
                        LEFT JOIN Socios s ON v.IdSocio = s.IdSocio
                        INNER JOIN Clasificacion c ON v.IdVenta = c.IdVenta
                        WHERE CAST(v.Fecha AS DATE) BETWEEN @Desde AND @Hasta
                        AND (ISNULL(s.Nombre, '') LIKE '%' + @Filtro + '%' 
                             OR ISNULL(s.Apellido, '') LIKE '%' + @Filtro + '%'
                             OR s.Cedula LIKE '%' + @Filtro + '%'
                             OR CAST(v.IdVenta AS VARCHAR) LIKE '%' + @Filtro + '%')
                        AND (@TipoFiltro = 'Todos' OR @TipoFiltro = 'Todas'
                             OR (@TipoFiltro LIKE '%Producto%' AND c.CantProductos > 0 AND c.CantMensualidades = 0)
                             OR (@TipoFiltro LIKE '%Mensualidad%' AND c.CantMensualidades > 0 AND c.CantProductos = 0)
                             OR (@TipoFiltro LIKE '%Mixto%' AND c.CantProductos > 0 AND c.CantMensualidades > 0))
                        ORDER BY v.Fecha DESC";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@Desde", dtpDesde.Value.Date);
                    cmd.Parameters.AddWithValue("@Hasta", dtpHasta.Value.Date);
                    cmd.Parameters.AddWithValue("@Filtro", txtBuscar.Text.Trim());
                    cmd.Parameters.AddWithValue("@TipoFiltro", tipoFiltro);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dtVentas = new DataTable();
                    da.Fill(dtVentas);

                    dgvVentas.DataSource = dtVentas;

                    CalcularTotales(dtVentas);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el historial: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region 5. SECCIÓN INFERIOR: TOTALES Y ACCIONES
        private void CalcularTotales(DataTable dt)
        {
            decimal totalRecaudado = 0;
            foreach (DataRow row in dt.Rows)
            {
                totalRecaudado += Convert.ToDecimal(row["Total Cobrado"]);
            }

            lblCantidadVentas.Text = "Transacciones Encontradas: " + dt.Rows.Count.ToString();
            lblTotalRecaudado.Text = "TOTAL RECAUDADO: Gs. " + totalRecaudado.ToString("N0");
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (dgvVentas.SelectedRows.Count > 0)
            {
                int idVenta = Convert.ToInt32(dgvVentas.SelectedRows[0].Cells["colHistorialId"].Value);
                string cliente = dgvVentas.SelectedRows[0].Cells["colHistorialCliente"].Value.ToString();

                frmDetalleTransaccion ventanaDetalle = new frmDetalleTransaccion(idVenta, cliente);
                ventanaDetalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una transacción de la tabla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}