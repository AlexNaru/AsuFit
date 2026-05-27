using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmHistorialTransacciones : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private VentaNegocio negocioVenta = new VentaNegocio(); // Instanciamos la capa de negocio

        public frmHistorialTransacciones()
        {
            InitializeComponent();
            dgvVentas.AutoGenerateColumns = false;

            ConfigurarTemaOscuroGrilla(dgvVentas);
            ConfigurarTemaOscuroCalendarios();
        }
        #endregion

        #region 2. ESTILOS VISUALES Y COMPORTAMIENTO UI
        // Aplica el estilo visual del sistema a la grilla
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
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(35, 39, 47);

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(25, 28, 35);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 229, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 35;
        }

        // Configura los colores del calendario desplegable
        private void ConfigurarTemaOscuroCalendarios()
        {
            dtpDesde.CalendarMonthBackground = Color.FromArgb(35, 39, 47);
            dtpDesde.CalendarTitleBackColor = Color.FromArgb(0, 229, 255);
            dtpDesde.CalendarTitleForeColor = Color.Black;
            dtpDesde.CalendarTrailingForeColor = Color.Gray;
            dtpDesde.CalendarForeColor = Color.White;

            dtpHasta.CalendarMonthBackground = Color.FromArgb(35, 39, 47);
            dtpHasta.CalendarTitleBackColor = Color.FromArgb(0, 229, 255);
            dtpHasta.CalendarTitleForeColor = Color.Black;
            dtpHasta.CalendarTrailingForeColor = Color.Gray;
            dtpHasta.CalendarForeColor = Color.White;
        }

        // Gestiona el comportamiento de las marcas de agua en el TextBox de búsqueda
        private void AplicarPlaceholder(TextBox txt, string textoAyuda)
        {
            txt.Tag = textoAyuda;

            if (string.IsNullOrWhiteSpace(txt.Text) || txt.Text == textoAyuda)
            {
                txt.Text = textoAyuda;
                txt.ForeColor = Color.Silver;
            }
            else
            {
                txt.ForeColor = Color.White;
            }

            txt.Enter += delegate
            {
                if (txt.Text == textoAyuda)
                {
                    txt.Text = "";
                    txt.ForeColor = Color.White;
                }
            };

            txt.Leave += delegate
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = textoAyuda;
                    txt.ForeColor = Color.Silver;
                }
            };
        }

        private void QuitarFocoCombo_DropDownClosed(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        // Vincula el evento clic a todo el fondo y sus paneles para asegurar la deselección
        private void VincularClicDeseleccion(Control contenedor)
        {
            contenedor.Click += new EventHandler(Fondo_Click);
            foreach (Control c in contenedor.Controls)
            {
                // Solo vinculamos a contenedores o etiquetas, NO a botones, grillas o inputs
                if (c is Panel || c is GroupBox || c is Label)
                {
                    c.Click += new EventHandler(Fondo_Click);
                    VincularClicDeseleccion(c); // Recursividad para paneles anidados
                }
            }
        }

        private void Fondo_Click(object sender, EventArgs e)
        {
            dgvVentas.ClearSelection();
            dgvVentas.CurrentCell = null;
            this.ActiveControl = null;
        }
        #endregion

        #region 3. INICIALIZACIÓN
        private void frmHistorialTransacciones_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpHasta.Value = DateTime.Now.Date;

            // Sincroniza la fecha inicial a los TextBox oscuros creados en el diseñador
            if (txtDesde != null) txtDesde.Text = dtpDesde.Value.ToShortDateString();
            if (txtHasta != null) txtHasta.Text = dtpHasta.Value.ToShortDateString();

            AplicarPlaceholder(txtBuscar, "Buscar por N° de Transacción, Cliente o Cédula...");

            cmbFiltroTipo.SelectedIndex = 0;
            cmbFiltroTipo.DropDownClosed += QuitarFocoCombo_DropDownClosed;

            BuscarVentas();

            // Activa la deselección haciendo clic en cualquier parte vacía
            VincularClicDeseleccion(this);

            this.ActiveControl = null;
        }
        #endregion

        #region 4. SECCIÓN SUPERIOR: FILTROS Y BÚSQUEDA
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarVentas();
        }

        // Sincroniza el DTP con el TextBox oscuro y lanza la búsqueda
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            if (txtDesde != null) txtDesde.Text = dtpDesde.Value.ToShortDateString();
            BuscarVentas();
        }

        // Sincroniza el DTP con el TextBox oscuro y lanza la búsqueda
        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            if (txtHasta != null) txtHasta.Text = dtpHasta.Value.ToShortDateString();
            BuscarVentas();
        }

        private void cmbFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscarVentas();
        }
        #endregion

        #region 5. SECCIÓN CENTRAL: PROCESAMIENTO DE DATOS Y GRILLA
        private void BuscarVentas()
        {
            if (cmbFiltroTipo.SelectedItem == null) return;
            string tipoFiltro = cmbFiltroTipo.SelectedItem.ToString();

            string textoBusqueda = txtBuscar.Text;
            if (textoBusqueda == (string)txtBuscar.Tag || string.IsNullOrWhiteSpace(textoBusqueda))
            {
                textoBusqueda = "";
            }

            try
            {
                // Ahora el formulario solo llama a la capa de Negocio, delegando toda la responsabilidad de SQL.
                DataTable dtVentas = negocioVenta.ObtenerHistorialVentas(dtpDesde.Value.Date, dtpHasta.Value.Date, textoBusqueda.Trim(), tipoFiltro);

                dgvVentas.DataSource = dtVentas;

                dgvVentas.ClearSelection();
                dgvVentas.CurrentCell = null;

                CalcularTotales(dtVentas);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVentas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvVentas.ClearSelection();
            dgvVentas.CurrentCell = null;
        }
        #endregion

        #region 6. MÉTODOS AUXILIARES DE FORMULARIO EMERGENTE
        private void PrepararFormularioComoDashboard(Form frm)
        {
            frm.Scale(new SizeF(1.4f, 1.4f));
            AjustarFuentes(frm);

            frm.StartPosition = FormStartPosition.Manual;

            if (this.Parent != null)
            {
                Point posicionPanelAbsoluta = this.Parent.PointToScreen(Point.Empty);
                int x = posicionPanelAbsoluta.X + (this.Parent.Width - frm.Width) / 2;
                int y = posicionPanelAbsoluta.Y + (this.Parent.Height - frm.Height) / 2;

                frm.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterParent;
            }
        }

        private void AjustarFuentes(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is TextBox || c is ComboBox || c is Label || c is DataGridView)
                {
                    c.Font = new Font("Segoe UI", 10f, c.Font.Style);
                }
                else if (c.HasChildren)
                {
                    AjustarFuentes(c);
                }
            }
        }
        #endregion

        #region 7. SECCIÓN INFERIOR: TOTALES Y ACCIONES
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

                PrepararFormularioComoDashboard(ventanaDetalle);

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