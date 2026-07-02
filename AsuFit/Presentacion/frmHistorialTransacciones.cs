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
            this.Load += new EventHandler(frmHistorialTransacciones_Load);
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

        // Gestiona el comportamiento de la marca de agua con desvanecimiento dinámico estilo AsuFit
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
                    this.BeginInvoke(new Action(() => txt.SelectionStart = 0));
                }
            };

            txt.MouseDown += delegate
            {
                if (txt.Text == textoAyuda)
                {
                    txt.SelectionStart = 0;
                    txt.SelectionLength = 0;
                }
            };

            txt.MouseMove += delegate
            {
                if (txt.Text == textoAyuda && txt.SelectionLength > 0)
                {
                    txt.SelectionStart = 0;
                    txt.SelectionLength = 0;
                }
            };

            txt.TextChanged += delegate
            {
                if (txt.Text != textoAyuda && txt.ForeColor == Color.Silver)
                {
                    string entradaUsuario = txt.Text;

                    // Solo quitamos el bloque exacto del placeholder, sin destruir caracteres similares
                    if (entradaUsuario.StartsWith(textoAyuda))
                        entradaUsuario = entradaUsuario.Substring(textoAyuda.Length);
                    else if (entradaUsuario.EndsWith(textoAyuda))
                        entradaUsuario = entradaUsuario.Substring(0, entradaUsuario.Length - textoAyuda.Length);

                    txt.ForeColor = Color.White;
                    txt.Text = entradaUsuario;
                    txt.SelectionStart = txt.Text.Length;
                }
                else if (string.IsNullOrEmpty(txt.Text))
                {
                    txt.ForeColor = Color.Silver;
                    txt.Text = textoAyuda;
                    txt.SelectionStart = 0;
                }
            };

            txt.KeyDown += delegate (object sender, KeyEventArgs e)
            {
                if (txt.Text == textoAyuda && (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
                {
                    e.SuppressKeyPress = true;
                }
            };
        }

        // Libera el foco del componente de forma asíncrona mitigando remanentes visuales de selección del sistema.
        private void QuitarFocoCombo_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => this.ActiveControl = null));
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
            dtpDesde.Value = DateTime.Now.AddDays(-1).Date;
            dtpHasta.Value = DateTime.Now.Date;

            // Sincroniza la fecha inicial a los TextBox oscuros creados en el diseñador
            if (txtDesde != null) txtDesde.Text = dtpDesde.Value.ToShortDateString();
            if (txtHasta != null) txtHasta.Text = dtpHasta.Value.ToShortDateString();

            AplicarPlaceholder(txtBuscar, "Buscar por N° de Transacción, Cliente o Cédula...");

            cmbFiltroTipo.SelectedIndex = 0;
            cmbFiltroTipo.DropDownClosed += QuitarFocoCombo_DropDownClosed;
            cmbFiltroTipo.SelectedIndexChanged += QuitarFocoCombo_DropDownClosed;

            SuscribirFiltrosDeSeguridad();

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
        // Configura la escala, fuente y posición del formulario emergente DENTRO DE LA ESCALA ACTUAL
        private void PrepararFormularioComoDashboard(Form frm)
        {
            float escalaActual = Properties.Settings.Default.EscalaInterfaz;

            // Aplica la escala elegida por el usuario
            frm.Scale(new SizeF(escalaActual, escalaActual));
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

        // Ajusta recursivamente el tamaño de fuente utilizando la configuración de usuario
        private void AjustarFuentes(Control contenedor)
        {
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            foreach (Control c in contenedor.Controls)
            {
                if (c is TextBox || c is ComboBox || c is Label || c is DataGridView)
                {
                    c.Font = new Font("Segoe UI", fuenteActual, c.Font.Style);
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

        #region 8. GESTIÓN DE SEGURIDAD Y RESTRICCIONES DE ENTRADA
        // Suscribe programáticamente los vectores de captura a las directivas de sanitización de consultas.
        private void SuscribirFiltrosDeSeguridad()
        {
            txtBuscar.KeyPress += txtAntiInyeccion_KeyPress;

            ContextMenuStrip menuVacio = new ContextMenuStrip();
            foreach (Control contenedor in this.Controls)
            {
                AsignarBloqueosRecursivo(contenedor, menuVacio);
            }
        }

        private void AsignarBloqueosRecursivo(Control contenedor, ContextMenuStrip menuVacio)
        {
            if (contenedor is TextBox txt)
            {
                txt.KeyDown += BloquearPegado_KeyDown;
                txt.ContextMenuStrip = menuVacio;
            }

            foreach (Control hijo in contenedor.Controls)
            {
                AsignarBloqueosRecursivo(hijo, menuVacio);
            }
        }

        private void BloquearPegado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtAntiInyeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'' || e.KeyChar == '"' || e.KeyChar == ';')
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}