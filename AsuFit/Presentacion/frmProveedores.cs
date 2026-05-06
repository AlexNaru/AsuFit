using AsuFit.Negocio;
using System;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AsuFit.Presentacion
{
    public partial class frmProveedores : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        private DataTable dtProveedores;
        private ProveedorNegocio negocio = new ProveedorNegocio();

        public frmProveedores()
        {
            InitializeComponent();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            ConfigurarBuscador();
            CargarGrilla();
            txtBuscarProveedor.Focus();
        }

        private void ConfigurarBuscador()
        {
            txtBuscarProveedor.ForeColor = System.Drawing.Color.Black;

            // El "1" le dice al sistema que mantenga el texto aunque el control tenga el foco
            SendMessage(txtBuscarProveedor.Handle, EM_SETCUEBANNER, 1, "Buscar por Nombre o RUC...");
        }

        private void CargarGrilla()
        {
            try
            {
                // Traemos los datos de SQL
                dtProveedores = negocio.ListarProveedores();

                if (dtProveedores != null)
                {
                    dgvProveedores.AutoGenerateColumns = true;

                    // ¡EL CAMBIO CLAVE! 
                    // No asignamos el DataSource directamente aquí. 
                    // Llamamos a FiltrarDatos() para que aplique el filtro de "Activos" de entrada.
                    FiltrarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Base de Datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarDatos()
        {
            if (dtProveedores == null) return;

            // 1. EL CAMBIO CLAVE: O filtramos estrictamente por 'Inactivo', o estrictamente por 'Activo'
            string filtroEstado = chkMostrarInactivos.Checked ? "Estado = 'Inactivo'" : "Estado = 'Activo'";

            // 2. Buscador por texto
            string textoBusqueda = txtBuscarProveedor.Text == "Buscar por Nombre o RUC..." ? "" : txtBuscarProveedor.Text.Trim();

            string filtroFinal = filtroEstado;

            // 3. Si el usuario escribió algo en el buscador, lo combinamos con el estado actual
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                string busquedaSegura = textoBusqueda.Replace("'", "''");
                string filtroTexto = $"(Nombre LIKE '%{busquedaSegura}%' OR RUC LIKE '%{busquedaSegura}%')";

                // Como filtroEstado siempre tiene un valor ahora, simplemente los unimos con un AND
                filtroFinal = $"{filtroEstado} AND {filtroTexto}";
            }

            // 4. Aplicamos los filtros a la vista de la grilla
            DataView dv = dtProveedores.DefaultView;
            dv.RowFilter = filtroFinal;
            dgvProveedores.DataSource = dv;

            FormatearGrilla();
            ActualizarResumen();
            dgvProveedores.ClearSelection();
        }

        private void FormatearGrilla()
        {
            if (dgvProveedores.Columns.Contains("IdProveedor")) dgvProveedores.Columns["IdProveedor"].Visible = false;
            dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.AllowUserToAddRows = false;
            dgvProveedores.ReadOnly = true;
        }

        private void ActualizarResumen()
        {
            if (dtProveedores == null) return;

            // Contamos directamente desde el DataTable original para los totales globales
            int total = dtProveedores.Rows.Count;
            int activos = 0;
            int inactivos = 0;

            foreach (DataRow row in dtProveedores.Rows)
            {
                if (row["Estado"].ToString() == "Activo") activos++;
                else inactivos++;
            }

            // ASIGNACIÓN A LABELS: Asegurate de que los nombres coincidan exactamente
            lblTotal.Text = total.ToString();
            lblActivos.Text = activos.ToString();
            lblInactivos.Text = inactivos.ToString();

            // Forzamos el refresco visual de los labels
            lblTotal.Refresh();
            lblActivos.Refresh();
            lblInactivos.Refresh();
        }

        private void chkMostrarInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProveedores.Rows[e.RowIndex];

                txtId.Text = fila.Cells["IdProveedor"].Value.ToString();
                txtRuc.Text = fila.Cells["RUC"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                cmbCategoria.Text = fila.Cells["Categoria"].Value.ToString();
                txtContacto.Text = fila.Cells["Contacto"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
                txtCorreo.Text = fila.Cells["Correo"].Value.ToString();
                txtDireccion.Text = fila.Cells["Direccion"].Value.ToString();
                txtCiudad.Text = fila.Cells["Ciudad"].Value.ToString();
                chkActivo.Checked = (fila.Cells["Estado"].Value.ToString() == "Activo");
            }
        }

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
                // Convertimos el valor del CheckBox a texto para la Base de Datos
                string estadoTexto = chkActivo.Checked ? "Activo" : "Inactivo";

                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    // NUEVO PROVEEDOR
                    bool exito = negocio.InsertarProveedor(txtNombre.Text, txtRuc.Text, cmbCategoria.Text, txtContacto.Text, txtTelefono.Text, txtCorreo.Text, txtDireccion.Text, txtCiudad.Text, estadoTexto);
                    if (exito) MessageBox.Show("Proveedor registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // EDITAR EXISTENTE
                    int idProveedor = Convert.ToInt32(txtId.Text);
                    bool exito = negocio.EditarProveedor(idProveedor, txtNombre.Text, txtRuc.Text, cmbCategoria.Text, txtContacto.Text, txtTelefono.Text, txtCorreo.Text, txtDireccion.Text, txtCiudad.Text, estadoTexto);
                    if (exito) MessageBox.Show("Proveedor actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimpiarFormulario();
                CargarGrilla();
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

                // Si está marcado (Activo), el nuevo estado será "Inactivo", y viceversa
                string nuevoEstadoTexto = chkActivo.Checked ? "Inactivo" : "Activo";

                bool exito = negocio.CambiarEstado(idProveedor, nuevoEstadoTexto);
                if (exito)
                {
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

        private void txtBuscarProveedor_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarProveedor.Text != "Buscar por Nombre o RUC...")
                FiltrarDatos();
        }

        private void dgvProveedores_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvProveedores.ClearSelection();
        }

        private void LimpiarSeleccion_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }
    }
}