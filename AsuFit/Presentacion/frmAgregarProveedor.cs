using AsuFit.Entidades; // <-- Agregado para poder usar el "molde" Proveedor
using AsuFit.Negocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmAgregarProveedor : Form
    {
        // Instanciamos el negocio (Reutilizamos la lógica que ya creaste)
        private ProveedorNegocio negocioProveedor = new ProveedorNegocio();

        public frmAgregarProveedor()
        {
            InitializeComponent();
        }

        private void frmAgregarProveedor_Load(object sender, EventArgs e)
        {
            // Selecciona la primera categoría por defecto (opcional)
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;

            // Llamamos al cálculo apenas se abre
            CargarResumen();
        }

        private void CargarResumen()
        {
            try
            {
                // Usamos el negocio para traer la lista completa desde SQL
                DataTable dt = negocioProveedor.ListarProveedores();

                if (dt != null)
                {
                    int total = dt.Rows.Count;
                    int activos = 0;
                    int inactivos = 0;

                    // Contamos uno por uno
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Estado"].ToString() == "Activo") activos++;
                        else inactivos++;
                    }

                    // Actualizamos los labels (asegúrate de que los nombres coincidan con tu diseño)
                    lblTotal.Text = total.ToString();
                    lblActivos.Text = activos.ToString();
                    lblInactivos.Text = inactivos.ToString();
                }
            }
            catch (Exception ex)
            {
                // Si falla, los dejamos en 0 silenciosamente
                Console.WriteLine("Error al cargar resumen: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Validación rápida
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtRuc.Text))
            {
                MessageBox.Show("El Nombre y el RUC son campos obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. EMPAQUETAMOS LOS DATOS EN LA ENTIDAD
                Proveedor objProveedor = new Proveedor();
                objProveedor.IdProveedor = 0; // Es 0 porque es uno nuevo
                objProveedor.Nombre = txtNombre.Text.Trim();
                objProveedor.RUC = txtRuc.Text.Trim();
                objProveedor.Categoria = cmbCategoria.SelectedItem != null ? cmbCategoria.SelectedItem.ToString() : "";
                objProveedor.Contacto = txtContacto.Text.Trim();
                objProveedor.Telefono = txtTelefono.Text.Trim();
                objProveedor.Correo = txtCorreo.Text.Trim();
                objProveedor.Direccion = txtDireccion.Text.Trim();
                objProveedor.Ciudad = txtCiudad.Text.Trim();
                objProveedor.Estado = "Activo"; // Por defecto, un proveedor nuevo entra como "Activo"

                // 3. Enviamos el PAQUETE a SQL usando la nueva arquitectura
                bool exito = negocioProveedor.GuardarProveedor(objProveedor);

                if (exito)
                {
                    MessageBox.Show("Proveedor registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ¡EL TOQUE FINAL! 
                    // Cerramos este mini-formulario para volver a la pantalla de compras
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}