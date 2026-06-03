using AsuFit.Datos;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmAuditoria : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private DataTable dtAuditoria = new DataTable();

        public frmAuditoria()
        {
            InitializeComponent();
            dgvAuditoria.AutoGenerateColumns = false;
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmAuditoria_Load(object sender, EventArgs e)
        {
            ConfigurarTemaOscuro();

            if (cmbFiltroModulo.Items.Count > 0)
                cmbFiltroModulo.SelectedIndex = 0;

            // 1. Configuramos los selectores para que por defecto muestren solo el día de hoy
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;

            // FIX: Sincronizamos las fechas iniciales con los TextBoxes oscuros
            SincronizarTextosFecha();

            // 2. Conectamos el evento manualmente para evitar múltiples recargas al abrir la ventana
            dtpDesde.ValueChanged += new EventHandler(FiltrosFecha_ValueChanged);
            dtpHasta.ValueChanged += new EventHandler(FiltrosFecha_ValueChanged);

            CargarAuditoria();

            // Aplicamos el placeholder interactivo estilo AsuFit (Color Plata)
            AplicarPlaceholder(txtBuscar, "Buscar por usuario, acción o detalle...");

            // Forzamos el foco nulo para que el placeholder se dibuje correctamente y la grilla no se auto-seleccione
            this.ActiveControl = null;
        }

        private void CargarAuditoria()
        {
            try
            {
                AuditoriaNegocio negocio = new AuditoriaNegocio();

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

        #region 3. ESTILOS VISUALES (TEMA OSCURO)
        private void ConfigurarTemaOscuro()
        {
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            // Fondo general del formulario
            this.BackColor = Color.FromArgb(25, 28, 35);

            AplicarTemaOscuroRecursivo(this, fuenteActual);
            ConfigurarTemaOscuroGrilla(dgvAuditoria, fuenteActual);
        }

        private void AplicarTemaOscuroRecursivo(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Panel || c is GroupBox || c is TabPage)
                {
                    c.BackColor = Color.FromArgb(25, 28, 35); // Fondo oscuro general
                    c.ForeColor = Color.White;
                }
                else if (c is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                    lbl.Font = new Font("Segoe UI", fuente, lbl.Font.Style);
                }
                else if (c is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(50, 55, 65);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.Font = new Font("Segoe UI", fuente, FontStyle.Regular);

                    // FIX: Solo hacemos de lectura a los falsos calendarios, para no bloquear el txtBuscar
                    if (txt.Name == "txtDesde" || txt.Name == "txtHasta")
                    {
                        txt.ReadOnly = true;
                    }
                }
                else if (c is ComboBox cmb)
                {
                    cmb.BackColor = Color.FromArgb(50, 55, 65);
                    cmb.ForeColor = Color.White;
                    cmb.FlatStyle = FlatStyle.Flat;
                    cmb.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
                }
                else if (c is Button btn)
                {
                    btn.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                    btn.Height = 35; // Estándar de altura AsuFit
                    btn.BackColor = Color.FromArgb(0, 229, 255); // Cian AsuFit
                    btn.ForeColor = Color.Black;
                }
                else if (c is TabControl tab)
                {
                    tab.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
                    // Dejamos que Windows controle las pestañas para no tener el borde blanco grueso
                    tab.DrawMode = TabDrawMode.Normal;
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c, fuente);
            }
        }

        private void ConfigurarTemaOscuroGrilla(DataGridView dgv, float fuente)
        {
            if (dgv == null) return;

            dgv.BackgroundColor = Color.FromArgb(25, 28, 35);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(50, 55, 65);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(25, 28, 35);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 229, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 35;
        }

        // --- MÉTODO INTELIGENTE DE PLACEHOLDER ---
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

        // Sincroniza los valores de los calendarios con los TextBox decorativos
        private void SincronizarTextosFecha()
        {
            if (txtDesde != null) txtDesde.Text = dtpDesde.Value.ToShortDateString();
            if (txtHasta != null) txtHasta.Text = dtpHasta.Value.ToShortDateString();
        }
        #endregion

        #region 4. SECCIÓN SUPERIOR: FILTROS Y BÚSQUEDA
        private void FiltrosFecha_ValueChanged(object sender, EventArgs e)
        {
            SincronizarTextosFecha(); // Se actualiza el texto oscuro al cambiar de fecha
            CargarAuditoria();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void cmbFiltroModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();

            this.ActiveControl = null;
        }

        private void cmbFiltroModulo_DropDownClosed(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void AplicarFiltros()
        {
            if (dtAuditoria == null || dtAuditoria.Rows.Count == 0) return;

            string modulo = cmbFiltroModulo.Text;
            string busqueda = txtBuscar.Text.Trim();

            // Evitamos buscar el texto del placeholder
            if (busqueda == "Buscar por usuario, acción o detalle...") busqueda = "";

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

        #region 5. SECCIÓN CENTRAL Y ACCIONES: GRILLA
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

            // Si quieres que este pop-up también se adapte a la escala, puedes aplicarlo aquí
            float escalaActual = Properties.Settings.Default.EscalaInterfaz;
            frm.Scale(new SizeF(escalaActual, escalaActual));
            frm.StartPosition = FormStartPosition.CenterParent;

            frm.ShowDialog();
        }
        #endregion
    }
}