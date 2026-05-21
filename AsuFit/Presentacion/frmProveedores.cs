using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmProveedores : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        private DataTable dtProveedores;
        private ProveedorNegocio negocio = new ProveedorNegocio();
        private Usuario usuarioActual;

        public frmProveedores(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;
            dgvProveedores.AutoGenerateColumns = false;
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmProveedores_Load(object sender, EventArgs e)
        {
            ConfigurarBuscador();
            CargarGrilla();
            txtBuscarProveedor.Focus();
        }

        private void ConfigurarBuscador()
        {
            txtBuscarProveedor.ForeColor = System.Drawing.Color.Black;
            SendMessage(txtBuscarProveedor.Handle, EM_SETCUEBANNER, 1, "Buscar por Nombre o RUC...");
        }

        private void CargarGrilla()
        {
            try
            {
                dtProveedores = negocio.ListarProveedores();
                if (dtProveedores != null)
                {
                    FiltrarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Base de Datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 3. SECCIÓN IZQUIERDA: CATÁLOGO Y BÚSQUEDA
        private void FiltrarDatos()
        {
            if (dtProveedores == null) return;

            string filtroEstado = chkMostrarInactivos.Checked ? "Estado = 'Inactivo'" : "Estado = 'Activo'";
            string textoBusqueda = txtBuscarProveedor.Text == "Buscar por Nombre o RUC..." ? "" : txtBuscarProveedor.Text.Trim();

            string filtroFinal = filtroEstado;

            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                string busquedaSegura = textoBusqueda.Replace("'", "''");
                filtroFinal = $"{filtroEstado} AND (Nombre LIKE '%{busquedaSegura}%' OR RUC LIKE '%{busquedaSegura}%')";
            }

            DataView dv = dtProveedores.DefaultView;
            dv.RowFilter = filtroFinal;
            dgvProveedores.DataSource = dv;

            ActualizarResumen();
            dgvProveedores.ClearSelection();
        }

        private void txtBuscarProveedor_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarProveedor.Text != "Buscar por Nombre o RUC...")
                FiltrarDatos();
        }

        private void chkMostrarInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void dgvProveedores_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvProveedores.ClearSelection();
        }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProveedores.Rows[e.RowIndex];

                txtId.Text = fila.Cells["colProvId"].Value.ToString();
                txtRuc.Text = fila.Cells["colProvRuc"].Value.ToString();
                txtNombre.Text = fila.Cells["colProvNombre"].Value.ToString();
                cmbCategoria.Text = fila.Cells["colProvCategoria"].Value.ToString();
                txtContacto.Text = fila.Cells["colProvContacto"].Value.ToString();
                txtTelefono.Text = fila.Cells["colProvTelefono"].Value.ToString();
                txtCorreo.Text = fila.Cells["colProvCorreo"].Value.ToString();
                txtDireccion.Text = fila.Cells["colProvDireccion"].Value.ToString();
                txtCiudad.Text = fila.Cells["colProvCiudad"].Value.ToString();
                chkActivo.Checked = (fila.Cells["colProvEstado"].Value.ToString() == "Activo");
            }
        }
        #endregion

        #region 4. SECCIÓN DERECHA SUPERIOR: DETALLES DEL PROVEEDOR Y ACCIONES
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtRuc.Text))
            {
                MessageBox.Show("El Nombre y el RUC son campos obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Proveedor objProveedor = new Proveedor();
                objProveedor.IdProveedor = string.IsNullOrWhiteSpace(txtId.Text) ? 0 : Convert.ToInt32(txtId.Text);
                objProveedor.Nombre = txtNombre.Text.Trim();
                objProveedor.RUC = txtRuc.Text.Trim();
                objProveedor.Categoria = cmbCategoria.Text;
                objProveedor.Contacto = txtContacto.Text.Trim();
                objProveedor.Telefono = txtTelefono.Text.Trim();
                objProveedor.Correo = txtCorreo.Text.Trim();
                objProveedor.Direccion = txtDireccion.Text.Trim();
                objProveedor.Ciudad = txtCiudad.Text.Trim();
                objProveedor.Estado = chkActivo.Checked ? "Activo" : "Inactivo";

                bool exito = negocio.GuardarProveedor(objProveedor);

                if (exito)
                {
                    string accion = objProveedor.IdProveedor == 0 ? "Alta" : "Edición";
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Proveedores", accion, $"Se gestionó al proveedor '{objProveedor.Nombre}'.");
                    MessageBox.Show("Proveedor guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarFormulario();
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Por favor, seleccione un proveedor de la lista primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idProveedor = Convert.ToInt32(txtId.Text);
                string nuevoEstadoTexto = chkActivo.Checked ? "Inactivo" : "Activo";

                if (negocio.CambiarEstado(idProveedor, nuevoEstadoTexto))
                {
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Proveedores", "Cambio de Estado", $"Estado del proveedor ID {idProveedor} a {nuevoEstadoTexto}.");
                    MessageBox.Show($"El estado del proveedor ha sido cambiado a: {nuevoEstadoTexto}.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar el estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtId.Clear();
            txtRuc.Clear();
            txtNombre.Clear();
            cmbCategoria.SelectedIndex = 0;
            txtContacto.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            txtCiudad.Clear();
            chkActivo.Checked = true;

            dgvProveedores.ClearSelection();
            txtNombre.Focus();
        }

        private void LimpiarSeleccion_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }
        #endregion

        #region 5. SECCIÓN DERECHA INFERIOR: RESUMEN
        private void ActualizarResumen()
        {
            if (dtProveedores == null) return;

            int total = dtProveedores.Rows.Count;
            int activos = 0;
            int inactivos = 0;

            foreach (DataRow row in dtProveedores.Rows)
            {
                if (row["Estado"].ToString() == "Activo") activos++;
                else inactivos++;
            }

            lblTotal.Text = total.ToString();
            lblActivos.Text = activos.ToString();
            lblInactivos.Text = inactivos.ToString();
        }
        #endregion
    }
}