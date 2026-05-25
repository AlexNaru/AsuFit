using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmRegistrarCobro : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private int idSocioSeleccionado = 0;
        private int diasPlanSeleccionado = 0;
        private Usuario usuarioActual;

        public frmRegistrarCobro(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;

            // Bloquea la autogeneración de columnas para mantener la estructura del diseñador
            dgvSocios.AutoGenerateColumns = false;

            // Configura el renderizado manual del ComboBox para integrar el tema oscuro
            cmbPlanes.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlanes.DrawItem += CmbPlanes_DrawItem;
            cmbPlanes.DropDownClosed += cmbPlanes_DropDownClosed;
            cmbPlanes.BackColor = Color.FromArgb(35, 39, 47);
            cmbPlanes.ForeColor = Color.White;

            // Configura el campo de monto como elemento de solo lectura
            txtMonto.BackColor = Color.FromArgb(35, 39, 47);
            txtMonto.ForeColor = Color.White;
            txtMonto.ReadOnly = true;
            txtMonto.Enter += delegate { this.Focus(); };

            // Aplica la paleta de colores del sistema a la grilla
            ConfigurarTemaOscuroGrilla(dgvSocios);

            CargarGrillaSocios();

            // Configura el texto de sugerencia en el buscador
            AplicarPlaceholder(txtBuscar, "Buscar por Cédula, Nombre o Apellido...");

            // Establece el texto por defecto en el selector de planes
            if (cmbPlanes.Items.Count > 0) cmbPlanes.SelectedIndex = 0;

            // Libera el foco inicial de los controles
            this.ActiveControl = null;
        }
        #endregion

        #region 2. ESTILOS VISUALES Y COMPORTAMIENTO UI
        // Personaliza el dibujado de los elementos del ComboBox
        private void CmbPlanes_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ComboBox combo = sender as ComboBox;

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color bgColor = isSelected ? Color.FromArgb(0, 229, 255) : Color.FromArgb(35, 39, 47);
            Color txtColor = isSelected ? Color.Black : Color.White;

            e.Graphics.FillRectangle(new SolidBrush(bgColor), e.Bounds);
            e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font, new SolidBrush(txtColor), e.Bounds, StringFormat.GenericDefault);
        }

        // Gestiona el comportamiento del texto de ayuda interactivo (Placeholder)
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

        // Aplica el estilo visual premium (Modo Oscuro) al DataGridView
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
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 229, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 35;
        }
        #endregion

        #region 3. BÚSQUEDA Y CARGA DE DATOS
        // Carga los registros de socios activos desde la base de datos
        private void CargarGrillaSocios()
        {
            SocioNegocio negocio = new SocioNegocio();
            dgvSocios.DataSource = negocio.ListarSocios("Activo");

            dgvSocios.ClearSelection();
            idSocioSeleccionado = 0;
        }

        // Filtra los datos en memoria sin requerir consultas adicionales a la BD
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string textoBusqueda = txtBuscar.Text;
            if (textoBusqueda == (string)txtBuscar.Tag) textoBusqueda = "";

            if (dgvSocios.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = $"Cedula LIKE '%{textoBusqueda}%' OR Apellido LIKE '%{textoBusqueda}%' OR Nombre LIKE '%{textoBusqueda}%'";
            }
        }
        #endregion

        #region 4. GESTIÓN DE GRILLA Y FORMATO CONDICIONAL
        // Configura propiedades de la grilla posteriores al enlace de datos
        private void dgvSocios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvSocios.ClearSelection();

            // Oculta las columnas de identificadores internos tras la renderización
            foreach (DataGridViewColumn col in dgvSocios.Columns)
            {
                if (col.Name == "colCobroId" ||
                    col.DataPropertyName == "IdSocio" ||
                    col.HeaderText.Trim().ToUpper() == "ID")
                {
                    col.Visible = false;
                }
            }
        }

        // Aplica alertas de color basadas en la fecha de vencimiento de los cobros
        private void dgvSocios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSocios.Columns.Contains("colCobroVencimiento"))
            {
                var celdaFecha = dgvSocios.Rows[e.RowIndex].Cells["colCobroVencimiento"].Value;

                if (celdaFecha != null && celdaFecha != DBNull.Value)
                {
                    DateTime fechaVencimiento = Convert.ToDateTime(celdaFecha);
                    TimeSpan diferencia = fechaVencimiento.Date - DateTime.Now.Date;

                    if (fechaVencimiento.Date < DateTime.Now.Date)
                    {
                        // Membresía vencida
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if (diferencia.TotalDays >= 0 && diferencia.TotalDays <= 7)
                    {
                        // Próximo a vencer
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        // Captura el identificador único del socio seleccionado
        private void dgvSocios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSocios.Columns.Contains("colCobroId"))
            {
                idSocioSeleccionado = Convert.ToInt32(dgvSocios.Rows[e.RowIndex].Cells["colCobroId"].Value);
            }
            // Captura alternativa para variaciones en el DataGridView
            else if (e.RowIndex >= 0 && dgvSocios.Columns.Contains("ID"))
            {
                idSocioSeleccionado = Convert.ToInt32(dgvSocios.Rows[e.RowIndex].Cells["ID"].Value);
            }
        }

        // Libera la selección al interactuar con el área libre del formulario
        private void frmRegistrarCobro_Click(object sender, EventArgs e)
        {
            dgvSocios.ClearSelection();
            idSocioSeleccionado = 0;
            this.ActiveControl = null;
        }
        #endregion

        #region 5. PROCESAMIENTO DE COBRO
        // Actualiza el monto a cobrar en base al plan seleccionado
        private void cmbPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlanes.SelectedIndex <= 0)
            {
                txtMonto.Clear();
                diasPlanSeleccionado = 0;
                return;
            }

            PlanNegocio negocioPlan = new PlanNegocio();
            Plan planInfo = negocioPlan.ObtenerPlanPorNombre(cmbPlanes.Text);

            if (planInfo != null)
            {
                txtMonto.Text = planInfo.Precio.ToString("N0");
                diasPlanSeleccionado = planInfo.DuracionDias;
            }
        }

        // Libera el foco tras la selección para evitar el resaltado nativo de Windows
        private void cmbPlanes_DropDownClosed(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        // Integra la solicitud de cobro con el Carrito Global y transfiere al módulo de Caja
        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (idSocioSeleccionado == 0)
            {
                MessageBox.Show("Por favor, seleccione un socio de la tabla para registrar el cobro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbPlanes.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                MessageBox.Show("Por favor, seleccione un Plan válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificación de colisiones en el Carrito Global
            if (CarritoGlobal.Detalles.Rows.Count > 0 && CarritoGlobal.IdSocioPagara != null && CarritoGlobal.IdSocioPagara != idSocioSeleccionado)
            {
                DialogResult respuesta = MessageBox.Show(
                    "Ya hay conceptos en la caja a nombre de otro socio.\n\n¿Deseas agregar esta mensualidad para cobrar ambos planes juntos en el mismo ticket?\n(La factura saldrá a nombre del primer socio, pero ambos serán renovados en el sistema).",
                    "Cobro Múltiple Detectado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.No) return;
            }
            else
            {
                CarritoGlobal.IdSocioPagara = idSocioSeleccionado;
            }

            PlanNegocio negocioPlan = new PlanNegocio();
            Plan planInfo = negocioPlan.ObtenerPlanPorNombre(cmbPlanes.Text);
            if (planInfo == null) return;

            decimal monto = Convert.ToDecimal(txtMonto.Text.Replace(".", ""));
            string conceptoPlan = "Renovación: " + cmbPlanes.Text;

            // Formato estándar de codificación para procesamientos en caja
            string codigoPlanArtificial = $"PLAN-{planInfo.DuracionDias}-{idSocioSeleccionado}-{planInfo.IdPlan}";
            CarritoGlobal.AgregarItem(0, codigoPlanArtificial, conceptoPlan, 1, monto, 10);

            // Gestión de transición de interfaz hacia la Caja
            frmCajaCobro cajaAbierta = Application.OpenForms["frmCajaCobro"] as frmCajaCobro;
            if (cajaAbierta != null)
            {
                cajaAbierta.WindowState = FormWindowState.Normal;
                cajaAbierta.BringToFront();
                cajaAbierta.ActualizarPantallaDesdeCarrito();
            }
            else
            {
                frmCajaCobro nuevaCaja = new frmCajaCobro(usuarioActual);
                nuevaCaja.Show();
            }

            // Restablecimiento del estado actual
            idSocioSeleccionado = 0;
            txtMonto.Clear();
            cmbPlanes.SelectedIndex = 0;
            txtBuscar.Text = "";
            AplicarPlaceholder(txtBuscar, "Buscar por Cédula, Nombre o Apellido...");
            dgvSocios.ClearSelection();
        }
        #endregion

        private void frmRegistrarCobro_Load(object sender, EventArgs e)
        {
            // Evento reservado para futuras implementaciones
        }
    }
}