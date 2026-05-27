using System;
using System.Drawing;
using System.Windows.Forms;
using AsuFit.Entidades;
using AsuFit.Negocio;

namespace AsuFit.Presentacion
{
    public partial class frmGestionGastos : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        public frmGestionGastos()
        {
            InitializeComponent();
            dgvGastos.AutoGenerateColumns = false;

            ConfigurarTemaOscuroGrilla(dgvGastos);
        }
        #endregion

        #region 2. ESTILOS VISUALES Y COMPORTAMIENTO UI
        // Aplica el estilo visual del sistema a la grilla y oculta el resaltado nativo de los encabezados
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

        // Gestiona el comportamiento de las marcas de agua en los TextBox
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

        // Evita que el placeholder se procese como un valor real al guardar
        private string ObtenerTextoReal(TextBox txt)
        {
            if (txt.Text == (string)txt.Tag) return "";
            return txt.Text;
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
                if (c is Panel || c is GroupBox || c is Label)
                {
                    c.Click += new EventHandler(Fondo_Click);
                    VincularClicDeseleccion(c);
                }
            }
        }

        private void Fondo_Click(object sender, EventArgs e)
        {
            dgvGastos.ClearSelection();
            dgvGastos.CurrentCell = null;
            this.ActiveControl = null;
        }
        #endregion

        #region 3. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmGestionGastos_Load(object sender, EventArgs e)
        {
            AplicarPlaceholder(txtDescripcion, "Ej: Pago de internet, insumos...");
            AplicarPlaceholder(txtMonto, "Ej: 150000");

            if (cmbCategoria != null) cmbCategoria.DropDownClosed += QuitarFocoCombo_DropDownClosed;

            CargarGrillaGastos();
            VincularClicDeseleccion(this);

            this.ActiveControl = null;
        }

        private void CargarGrillaGastos()
        {
            try
            {
                GastoNegocio negocio = new GastoNegocio();
                dgvGastos.DataSource = negocio.ListarGastos();

                dgvGastos.ClearSelection();
                dgvGastos.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar gastos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 4. SECCIÓN SUPERIOR: GRILLA DE GASTOS
        private void dgvGastos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvGastos.ClearSelection();
            dgvGastos.CurrentCell = null;
        }

        private void frmGestionGastos_Click(object sender, EventArgs e)
        {
            dgvGastos.ClearSelection();
            dgvGastos.CurrentCell = null;
        }
        #endregion

        #region 5. SECCIÓN INFERIOR: REGISTRO DE NUEVO GASTO
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string descripcionReal = ObtenerTextoReal(txtDescripcion);
            string montoReal = ObtenerTextoReal(txtMonto);

            if (string.IsNullOrWhiteSpace(descripcionReal) || string.IsNullOrWhiteSpace(montoReal) || cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, complete todos los campos antes de guardar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Gasto nuevoGasto = new Gasto();
                nuevoGasto.Descripcion = descripcionReal;
                nuevoGasto.Categoria = cmbCategoria.Text;
                nuevoGasto.Monto = Convert.ToDecimal(montoReal.Replace(".", ""));

                // Asignación estática temporal del usuario que registra el movimiento
                nuevoGasto.UsuarioRegistra = "Admin";

                GastoNegocio negocio = new GastoNegocio();
                string mensaje;

                if (negocio.RegistrarGasto(nuevoGasto, out mensaje))
                {
                    // Invocación a la auditoría especificando la ruta completa para evitar importar la capa de Datos
                    AsuFit.Datos.GestorAuditoria.Registrar("Admin", "Gastos", "Registro", $"Gasto de Gs. {nuevoGasto.Monto:N0} en {nuevoGasto.Categoria}.");

                    MessageBox.Show("Gasto registrado correctamente en la caja.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtDescripcion.Clear();
                    txtMonto.Clear();
                    cmbCategoria.SelectedIndex = -1;

                    AplicarPlaceholder(txtDescripcion, "Ej: Pago de internet, insumos...");
                    AplicarPlaceholder(txtMonto, "Ej: 150000");

                    CargarGrillaGastos();
                    this.ActiveControl = null;
                }
                else
                {
                    MessageBox.Show(mensaje, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingresá un monto numérico válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}