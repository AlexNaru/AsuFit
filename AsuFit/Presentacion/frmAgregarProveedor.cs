using AsuFit.Negocio; // Conectamos con tu capa de negocio ya existente
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

        // 2. Agrega este método en cualquier parte dentro de la clase
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
                // 2. Por defecto, un proveedor nuevo entra como "Activo"
                string estadoTexto = "Activo";
                string categoria = cmbCategoria.SelectedItem != null ? cmbCategoria.SelectedItem.ToString() : "";

                // 3. Enviamos los datos a SQL
                bool exito = negocioProveedor.InsertarProveedor(
                    txtNombre.Text,
                    txtRuc.Text,
                    categoria,
                    txtContacto.Text,
                    txtTelefono.Text,
                    txtCorreo.Text,
                    txtDireccion.Text,
                    txtCiudad.Text,
                    estadoTexto
                );

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