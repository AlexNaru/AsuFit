using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmIngresoMercaderiaHistorial : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR

        // Instancia de la capa de negocio para delegar las operaciones de consulta al inventario.
        private InventarioNegocio negocioInventario = new InventarioNegocio();

        // Inicializa los componentes, bloquea la autogeneración de columnas y renderiza la paleta cromática.
        public frmIngresoMercaderiaHistorial()
        {
            InitializeComponent();

            dgvIngresoMercaderia.AutoGenerateColumns = false;

            ConfigurarTemaOscuroGrilla(dgvIngresoMercaderia);
            ConfigurarTemaOscuroCalendarios();
        }

        #endregion

        #region 2. CICLO DE VIDA E INICIALIZACIÓN

        // Orquesta el montaje inicial de componentes, inyección de dependencias y primera carga de datos.
        private void frmIngresoMercaderiaHistorial_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpHasta.Value = DateTime.Now.Date;

            if (txtDesde != null) txtDesde.Text = dtpDesde.Value.ToShortDateString();
            if (txtHasta != null) txtHasta.Text = dtpHasta.Value.ToShortDateString();

            AplicarPlaceholder(txtBuscar, "Buscar por nombre de Proveedor...");

            SuscribirFiltrosDeSeguridad();

            BuscarCompras();

            VincularClicDeseleccion(this);
            this.ActiveControl = null;

            this.Shown += new EventHandler(frmIngresoMercaderiaHistorial_Shown);
        }

        // Intercepta la finalización del renderizado visual para limpiar selecciones y focos automáticos de Windows.
        private void frmIngresoMercaderiaHistorial_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            dgvIngresoMercaderia.ClearSelection();
        }

        #endregion

        #region 3. SECCIÓN SUPERIOR: FILTROS DE BÚSQUEDA

        // Dispara la recarga del origen de datos ante mutaciones en el cuadro de búsqueda algorítmica.
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarCompras();
        }

        // Sincroniza la cota inferior del rango temporal y solicita la actualización de datos a la grilla.
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            if (txtDesde != null) txtDesde.Text = dtpDesde.Value.ToShortDateString();
            BuscarCompras();
        }

        // Sincroniza la cota superior del rango temporal y solicita la actualización de datos a la grilla.
        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            if (txtHasta != null) txtHasta.Text = dtpHasta.Value.ToShortDateString();
            BuscarCompras();
        }

        #endregion

        #region 4. SECCIÓN CENTRAL: PROCESAMIENTO Y GRILLA

        // Solicita a la capa de negocio la extracción del historial filtrado y refresca la vista de la tabla.
        private void BuscarCompras()
        {
            string textoBusqueda = txtBuscar.Text;
            if (textoBusqueda == (string)txtBuscar.Tag || string.IsNullOrWhiteSpace(textoBusqueda))
            {
                textoBusqueda = "";
            }

            try
            {
                DataTable dtCompras = negocioInventario.ObtenerHistorialCompras(dtpDesde.Value.Date, dtpHasta.Value.Date, textoBusqueda.Trim());

                dgvIngresoMercaderia.DataSource = dtCompras;
                dgvIngresoMercaderia.ClearSelection();
                dgvIngresoMercaderia.CurrentCell = null;

                CalcularTotales(dtCompras);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de compras: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Fuerza la limpieza de selecciones automáticas generadas por el motor de enlace de datos de Windows Forms.
        private void dgvIngresoMercaderia_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvIngresoMercaderia.ClearSelection();
            dgvIngresoMercaderia.CurrentCell = null;
        }

        #endregion

        #region 5. SECCIÓN INFERIOR: MÉTRICAS Y TOTALES

        // Itera sobre el conjunto de datos en memoria para totalizar la salida financiera y reflejarla en los indicadores.
        private void CalcularTotales(DataTable dt)
        {
            decimal totalInvertido = 0;

            if (dt.Columns.Contains("CostoTotal"))
            {
                foreach (DataRow row in dt.Rows)
                {
                    totalInvertido += Convert.ToDecimal(row["CostoTotal"]);
                }
            }

            if (lblCantidadIngresos != null) lblCantidadIngresos.Text = "Ingresos Encontrados: " + dt.Rows.Count.ToString();
            if (lblTotalGastado != null) lblTotalGastado.Text = "TOTAL GASTADO: Gs. " + totalInvertido.ToString("N0");
        }

        #endregion

        #region 6. COMPORTAMIENTO UI Y ESTILOS VISUALES

        // Inyecta las propiedades cromáticas corporativas sobre la estructura del DataGridView.
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

        // Sobrescribe los colores nativos de los controles DateTimePicker para adaptarlos al esquema visual oscuro.
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

        // Inyecta comportamiento de marca de agua dinámica en cuadros de texto para optimizar el espacio en la interfaz.
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

            txt.Enter += delegate { if (txt.Text == textoAyuda) this.BeginInvoke(new Action(() => txt.SelectionStart = 0)); };
            txt.MouseDown += delegate { if (txt.Text == textoAyuda) { txt.SelectionStart = 0; txt.SelectionLength = 0; } };
            txt.MouseMove += delegate { if (txt.Text == textoAyuda && txt.SelectionLength > 0) { txt.SelectionStart = 0; txt.SelectionLength = 0; } };

            txt.TextChanged += delegate
            {
                if (txt.Text != textoAyuda && txt.ForeColor == Color.Silver)
                {
                    string entradaUsuario = txt.Text;
                    if (entradaUsuario.StartsWith(textoAyuda)) entradaUsuario = entradaUsuario.Substring(textoAyuda.Length);
                    else if (entradaUsuario.EndsWith(textoAyuda)) entradaUsuario = entradaUsuario.Substring(0, entradaUsuario.Length - textoAyuda.Length);

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

        // Suscribe el evento de limpieza visual a todos los contenedores estáticos.
        private void VincularClicDeseleccion(Control contenedor)
        {
            contenedor.Click += new EventHandler(Fondo_Click);
            foreach (Control c in contenedor.Controls)
            {
                if (c is Panel || c is GroupBox || c is Label)
                {
                    c.Click += new EventHandler(Fondo_Click);
                    VincularClicDeseleccion(c);
                }
            }
        }

        // Libera de forma asíncrona las selecciones residuales de la grilla principal y el foco de entrada.
        private void Fondo_Click(object sender, EventArgs e)
        {
            dgvIngresoMercaderia.ClearSelection();
            dgvIngresoMercaderia.CurrentCell = null;
            this.ActiveControl = null;
        }

        #endregion

        #region 7. SEGURIDAD Y PREVENCIÓN DE INYECCIONES

        // Vincula de forma recursiva los bloqueos de teclado e inhabilitación de menús contextuales en entradas.
        private void SuscribirFiltrosDeSeguridad()
        {
            txtBuscar.KeyPress += txtAntiInyeccion_KeyPress;

            ContextMenuStrip menuVacio = new ContextMenuStrip();
            foreach (Control contenedor in this.Controls)
            {
                AsignarBloqueosRecursivo(contenedor, menuVacio);
            }
        }

        // Recorre los controles hijos para asignar restricciones de pegado de manera profunda.
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

        // Invalida comandos del sistema operativo (Ctrl+V / Shift+Insert) para evadir el bypasseo del filtro de teclado.
        private void BloquearPegado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert) e.SuppressKeyPress = true;
        }

        // Intercepta y destruye caracteres de escape utilizados frecuentemente en inyecciones de código SQL.
        private void txtAntiInyeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'' || e.KeyChar == '"' || e.KeyChar == ';') e.Handled = true;
        }

        #endregion
    }
}