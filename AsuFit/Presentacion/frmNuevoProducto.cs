using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AsuFit.Entidades; // <-- Ya lo tenías, excelente
using AsuFit.Negocio;
using AsuFit.Datos;

namespace AsuFit.Presentacion
{
    public partial class frmNuevoProducto : Form
    {
        private Usuario usuarioActual;
        private InventarioNegocio negocio = new InventarioNegocio();
        private ProveedorNegocio negocioProveedor = new ProveedorNegocio(); // Para cargar el ComboBox
        private string rutaFotoOrigen = "";
        public string ProductoRecienCreado = "";

        public frmNuevoProducto(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;
        }

        // --- CARGAMOS LOS PROVEEDORES AL ABRIR LA VENTANA ---
        private void frmNuevoProducto_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false; // Bloqueamos el ID

            try
            {
                DataTable dtProveedores = negocioProveedor.ListarProveedores();
                DataView dv = new DataView(dtProveedores);
                dv.RowFilter = "Estado = 'Activo'";

                cmbProveedor.DisplayMember = "Nombre";
                cmbProveedor.ValueMember = "IdProveedor";
                cmbProveedor.DataSource = dv.ToTable();
                cmbProveedor.SelectedIndex = -1;

                // --- NUEVO: VALORES POR DEFECTO VÁLIDOS ---
                txtStock.Text = "0"; // Stock por defecto

                // Si el ComboBox tiene elementos (10, 5, 0), seleccionamos el primero (índice 0, que es el 10)
                if (cmbIva.Items.Count > 0)
                {
                    cmbIva.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
            if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text) || cmbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Complete el Código, Nombre, Precio y seleccione un Proveedor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. EMPAQUETAMOS LOS DATOS EN LA ENTIDAD
                Producto objProducto = new Producto();
                objProducto.IdProducto = 0; // Es 0 porque es un producto nuevo
                objProducto.CodigoBarras = txtCodigoBarras.Text.Trim();
                objProducto.Nombre = txtNombre.Text.Trim();
                objProducto.Categoria = cmbCategoria.Text;
                objProducto.PrecioVenta = Convert.ToDecimal(txtPrecio.Text);
                objProducto.StockActual = string.IsNullOrWhiteSpace(txtStock.Text) ? 0 : Convert.ToInt32(txtStock.Text);
                objProducto.IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                objProducto.PorcentajeIva = string.IsNullOrWhiteSpace(cmbIva.Text) ? 10 : Convert.ToInt32(cmbIva.Text);

                // 2. ENVIAMOS LA ENTIDAD ARMADA A LA CAPA DE NEGOCIO
                bool exito = negocio.GuardarProducto(objProducto);

                if (exito)
                {
                    if (picProducto.Image != null && rutaFotoOrigen != "")
                    {
                        string carpetaDestino = @"C:\AsuFit_Fotos\";
                        if (!Directory.Exists(carpetaDestino)) Directory.CreateDirectory(carpetaDestino);

                        string rutaDestinoFinal = Path.Combine(carpetaDestino, objProducto.CodigoBarras + ".jpg");
                        if (File.Exists(rutaDestinoFinal)) File.Delete(rutaDestinoFinal);
                        File.Copy(rutaFotoOrigen, rutaDestinoFinal);
                    }

                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Inventario", "Alta Rápida", $"Se registró el producto '{objProducto.Nombre}'.");
                    MessageBox.Show("¡Producto registrado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ProductoRecienCreado = objProducto.Nombre;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el producto: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }
    }
}