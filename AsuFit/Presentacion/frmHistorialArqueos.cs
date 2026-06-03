using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmHistorialArqueos : Form
    {
        public frmHistorialArqueos()
        {
            InitializeComponent();
            dgvArqueos.AutoGenerateColumns = false;
        }

        private void frmHistorialArqueos_Load(object sender, EventArgs e)
        {
            ConfigurarTemaYEscala();
            CentrarFormulario();

            dtpDesde.Value = DateTime.Now.AddDays(-7);
            dtpHasta.Value = DateTime.Now;

            // Sincroniza la fecha inicial a los TextBox oscuros creados en el diseñador
            if (txtDesde != null) txtDesde.Text = dtpDesde.Value.ToShortDateString();
            if (txtHasta != null) txtHasta.Text = dtpHasta.Value.ToShortDateString();

            CargarHistorial();
        }

        #region ESTILOS VISUALES Y ESCALADO
        private void ConfigurarTemaYEscala()
        {
            // BLOQUEO DE REDIMENSIONAMIENTO Y PANTALLA COMPLETA
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            float escalaActual = Properties.Settings.Default.EscalaInterfaz;
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            this.Scale(new SizeF(escalaActual, escalaActual));
            AjustarFuentesRecursivo(this, fuenteActual);

            this.BackColor = Color.FromArgb(25, 28, 35);
            AplicarTemaOscuroRecursivo(this);

            ConfigurarTemaOscuroGrilla(dgvArqueos, fuenteActual);
            ConfigurarTemaOscuroCalendarios();

            if (btnVerPDF != null)
            {
                btnVerPDF.BackColor = Color.FromArgb(0, 229, 255);
                btnVerPDF.ForeColor = Color.Black;
                btnVerPDF.FlatStyle = FlatStyle.Flat;
                btnVerPDF.FlatAppearance.BorderSize = 0;

                // 1. CAMBIO APLICADO: Botón en negrita leyendo la fuente global
                btnVerPDF.Font = new Font("Segoe UI", fuenteActual, FontStyle.Bold);
            }
        }

        private void AplicarTemaOscuroRecursivo(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Panel || c is GroupBox)
                {
                    c.BackColor = Color.FromArgb(35, 39, 47);
                    c.ForeColor = Color.White;
                }
                else if (c is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                }
                // 2. CAMBIO APLICADO: Motor pinta de oscuro los nuevos TextBox
                else if (c is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(50, 55, 65);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.ReadOnly = true; // Evitamos que el usuario escriba encima del diseño
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c);
            }
        }

        private void AjustarFuentesRecursivo(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                // Se agregó TextBox y DateTimePicker para que escale parejo
                if (c is Label || c is Button || c is TextBox || c is DateTimePicker)
                {
                    c.Font = new Font("Segoe UI", fuente, c.Font.Style);
                }
                if (c.HasChildren) AjustarFuentesRecursivo(c, fuente);
            }
        }

        private void ConfigurarTemaOscuroGrilla(DataGridView dgv, float fuente)
        {
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

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(25, 28, 35);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 229, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 35;
        }

        private void ConfigurarTemaOscuroCalendarios()
        {
            dtpDesde.CalendarMonthBackground = Color.FromArgb(35, 39, 47);
            dtpHasta.CalendarMonthBackground = Color.FromArgb(35, 39, 47);
        }

        private void CentrarFormulario()
        {
            Form padre = Application.OpenForms["frmDashboard"];
            if (padre != null)
            {
                Control[] controles = padre.Controls.Find("pnlContenedor", true);
                if (controles.Length > 0)
                {
                    Control contenedor = controles[0];
                    Point posicionAbsoluta = contenedor.PointToScreen(Point.Empty);

                    this.StartPosition = FormStartPosition.Manual;
                    int x = posicionAbsoluta.X + (contenedor.Width - this.Width) / 2;
                    int y = posicionAbsoluta.Y + (contenedor.Height - this.Height) / 2;
                    this.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);
                    return;
                }
            }
            this.CenterToScreen();
        }
        #endregion

        #region EVENTOS Y LÓGICA
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            // Sincronizamos y buscamos
            if (txtDesde != null) txtDesde.Text = dtpDesde.Value.ToShortDateString();
            CargarHistorial();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            // Sincronizamos y buscamos
            if (txtHasta != null) txtHasta.Text = dtpHasta.Value.ToShortDateString();
            CargarHistorial();
        }

        private void CargarHistorial()
        {
            try
            {
                ArqueoNegocio negocio = new ArqueoNegocio();
                DataTable dt = negocio.ListarHistorialArqueos(dtpDesde.Value, dtpHasta.Value);
                dgvArqueos.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvArqueos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ColorearDiferencias();
            dgvArqueos.ClearSelection();
        }

        private void ColorearDiferencias()
        {
            foreach (DataGridViewRow fila in dgvArqueos.Rows)
            {
                if (fila.Cells["colArqueoDiferencia"].Value != null && fila.Cells["colArqueoDiferencia"].Value != DBNull.Value)
                {
                    decimal diferencia = Convert.ToDecimal(fila.Cells["colArqueoDiferencia"].Value);

                    if (diferencia == 0) fila.Cells["colArqueoDiferencia"].Style.ForeColor = Color.MediumSeaGreen;
                    else if (diferencia < 0) fila.Cells["colArqueoDiferencia"].Style.ForeColor = Color.LightCoral;
                    else fila.Cells["colArqueoDiferencia"].Style.ForeColor = Color.Gold;

                    fila.Cells["colArqueoDiferencia"].Style.Font = new Font(dgvArqueos.Font, FontStyle.Bold);
                }
            }
        }

        private void btnVerPDF_Click(object sender, EventArgs e)
        {
            if (dgvArqueos.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecciona un arqueo de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string estado = dgvArqueos.CurrentRow.Cells["colArqueoEstado"].Value.ToString();
            string idTurno = dgvArqueos.CurrentRow.Cells["colArqueoId"].Value.ToString();

            if (estado == "Cerrada")
            {
                string rutaDescargas = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string nombreArchivo = $"Ticket_Arqueo_Turno_{idTurno}.pdf";
                string rutaCompleta = System.IO.Path.Combine(rutaDescargas, nombreArchivo);

                if (System.IO.File.Exists(rutaCompleta)) System.Diagnostics.Process.Start(rutaCompleta);
                else MessageBox.Show($"No se encontró el archivo PDF para este arqueo en la ruta:\n{rutaCompleta}", "Archivo no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Este turno aún se encuentra ABIERTO.", "Turno en curso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}