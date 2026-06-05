using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmNuevoProducto : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private Usuario usuarioActual;
        private InventarioNegocio negocio = new InventarioNegocio();
        private ProveedorNegocio negocioProveedor = new ProveedorNegocio();

        private string rutaFotoOrigen = "";
        public string ProductoRecienCreado = "";

        public frmNuevoProducto(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;
            this.Text = "Nuevo Producto";
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmNuevoProducto_Load(object sender, EventArgs e)
        {
            ConfigurarTemaOscuro();

            if (txtId != null) txtId.Enabled = false;

            CargarProveedores();
            ConfigurarValoresPorDefecto();

            // Desvincular el foco para evitar el resaltado azul nativo de Windows
            cmbProveedor.DropDownClosed += QuitarFocoCombo_DropDownClosed;
            cmbIva.DropDownClosed += QuitarFocoCombo_DropDownClosed;
            cmbCategoria.DropDownClosed += QuitarFocoCombo_DropDownClosed;

            this.ActiveControl = null;
        }

        private void CargarProveedores()
        {
            try
            {
                DataTable dtProveedores = negocioProveedor.ListarProveedores();
                DataView dv = new DataView(dtProveedores);
                dv.RowFilter = "Estado = 'Activo'";

                cmbProveedor.DisplayMember = "Nombre";
                cmbProveedor.ValueMember = "IdProveedor";
                cmbProveedor.DataSource = dv.ToTable();
                cmbProveedor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Excepción de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarValoresPorDefecto()
        {
            txtStock.Text = "0";

            if (cmbIva.Items.Count > 0)
            {
                cmbIva.SelectedIndex = 0;
            }
        }
        #endregion

        #region 3. ESTILOS VISUALES (TEMA OSCURO)
        private void ConfigurarTemaOscuro()
        {
            float fuenteGlobal = Properties.Settings.Default.TamanoFuente;

            // Fondo general del formulario emergente
            this.BackColor = Color.FromArgb(25, 28, 35);

            AplicarTemaOscuroRecursivo(this, fuenteGlobal);
        }

        private void AplicarTemaOscuroRecursivo(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Label lbl)
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

                    // El botón secundario hereda color gris, el principal el color corporativo Cian
                    if (btn.Name.Contains("Cancelar"))
                    {
                        btn.BackColor = Color.FromArgb(50, 55, 65);
                        btn.ForeColor = Color.White;
                    }
                    else
                    {
                        btn.BackColor = Color.FromArgb(0, 229, 255);
                        btn.ForeColor = Color.Black;
                    }
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c, fuente);
            }
        }

        private void QuitarFocoCombo_DropDownClosed(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        #endregion

        #region 4. ACCIONES DEL FORMULARIO
        private void btnSubirFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccionar Foto del Producto";
                ofd.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    rutaFotoOrigen = ofd.FileName;
                    picProducto.Image = Image.FromFile(rutaFotoOrigen);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                cmbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Complete el Código, Nombre, Precio y seleccione un Proveedor.", "Aviso de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Mapeo de la entidad
                Producto objProducto = new Producto();
                objProducto.IdProducto = 0;
                objProducto.CodigoBarras = txtCodigoBarras.Text.Trim();
                objProducto.Nombre = txtNombre.Text.Trim();
                objProducto.Categoria = cmbCategoria.Text;
                objProducto.PrecioVenta = Convert.ToDecimal(txtPrecio.Text);
                objProducto.StockActual = string.IsNullOrWhiteSpace(txtStock.Text) ? 0 : Convert.ToInt32(txtStock.Text);
                objProducto.IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                objProducto.PorcentajeIva = string.IsNullOrWhiteSpace(cmbIva.Text) ? 10 : Convert.ToInt32(cmbIva.Text);

                // Delegación a la capa de negocio
                bool exito = negocio.GuardarProducto(objProducto);

                if (exito)
                {
                    // Almacenamiento físico de la imagen si se seleccionó una
                    if (picProducto.Image != null && !string.IsNullOrEmpty(rutaFotoOrigen))
                    {
                        string carpetaDestino = @"C:\AsuFit_Fotos\";
                        if (!Directory.Exists(carpetaDestino)) Directory.CreateDirectory(carpetaDestino);

                        string rutaDestinoFinal = Path.Combine(carpetaDestino, objProducto.CodigoBarras + ".jpg");
                        if (File.Exists(rutaDestinoFinal)) File.Delete(rutaDestinoFinal);

                        File.Copy(rutaFotoOrigen, rutaDestinoFinal);
                    }

                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Inventario", "Alta Rápida", $"Se registró el producto '{objProducto.Nombre}'.");
                    MessageBox.Show("¡Producto registrado con éxito!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ProductoRecienCreado = objProducto.Nombre;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el producto: " + ex.Message, "Excepción Crítica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 5. VALIDACIONES DE ENTRADA (KEYPRESS)
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite números y teclas de control (como Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite números y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }
        #endregion
    }
}