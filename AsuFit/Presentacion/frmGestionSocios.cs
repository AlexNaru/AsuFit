using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmGestionSocios : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private int idSocioSeleccionado = 0;
        private Usuario usuarioActual;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmGestionSocios(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;

            dgvSocios.AutoGenerateColumns = false;
            CargarGrilla();

            SendMessage(txtBuscar.Handle, EM_SETCUEBANNER, 1, "Buscar por Cédula, Nombre o Apellido...");
        }
        #endregion

        #region 2. SECCIÓN SUPERIOR: FILTROS Y BÚSQUEDA
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvSocios.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = $"Cedula LIKE '%{txtBuscar.Text}%' OR Apellido LIKE '%{txtBuscar.Text}%' OR Nombre LIKE '%{txtBuscar.Text}%'";

                int cantidad = 0;
                foreach (DataGridViewRow fila in dgvSocios.Rows)
                {
                    if (!fila.IsNewRow) cantidad++;
                }
                lblTotal.Text = "Registros encontrados: " + cantidad.ToString();
            }
        }

        private void chkActivo_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }
        #endregion

        #region 3. SECCIÓN CENTRAL: GRILLA Y SELECCIÓN
        private void CargarGrilla()
        {
            SocioNegocio negocio = new SocioNegocio();
            string filtroEstado = chkActivo.Checked ? "Inactivo" : "Activo";

            dgvSocios.DataSource = negocio.ListarSocios(filtroEstado);

            idSocioSeleccionado = 0;
            lblTotal.Text = "Registros encontrados: " + dgvSocios.Rows.Count.ToString();
        }

        private void dgvSocios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvSocios.ClearSelection();
        }

        private void dgvSocios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int colIndex = dgvSocios.Columns.Contains("colSocioVencimiento") ? dgvSocios.Columns["colSocioVencimiento"].Index :
                               dgvSocios.Columns.Contains("Vencimiento") ? dgvSocios.Columns["Vencimiento"].Index : -1;

                if (colIndex != -1 && dgvSocios.Rows[e.RowIndex].Cells[colIndex].Value != null && dgvSocios.Rows[e.RowIndex].Cells[colIndex].Value != DBNull.Value)
                {
                    DateTime fechaVencimiento = Convert.ToDateTime(dgvSocios.Rows[e.RowIndex].Cells[colIndex].Value);
                    TimeSpan diferencia = fechaVencimiento.Date - DateTime.Now.Date;

                    if (fechaVencimiento.Date < DateTime.Now.Date)
                    {
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if (diferencia.TotalDays >= 0 && diferencia.TotalDays <= 7)
                    {
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void dgvSocios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvSocios.Columns.Contains("colSocioId"))
                {
                    idSocioSeleccionado = Convert.ToInt32(dgvSocios.Rows[e.RowIndex].Cells["colSocioId"].Value);
                }
                else if (dgvSocios.Columns.Contains("IdSocio"))
                {
                    idSocioSeleccionado = Convert.ToInt32(dgvSocios.Rows[e.RowIndex].Cells["IdSocio"].Value);
                }
            }
        }

        private void frmGestionSocios_Click(object sender, EventArgs e)
        {
            dgvSocios.ClearSelection();
            idSocioSeleccionado = 0;
        }
        #endregion

        #region 4. SECCIÓN INFERIOR: BOTONES DE ACCIÓN
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmRegistrarSocio frm = new frmRegistrarSocio(usuarioActual);
            frm.btnCancelar.Visible = true;
            frm.ShowDialog();
            CargarGrilla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSocioSeleccionado > 0)
                {
                    DataGridViewRow fila = dgvSocios.CurrentRow;

                    Socio socioSeleccionado = new Socio
                    {
                        IdSocio = idSocioSeleccionado,
                        Cedula = fila.Cells[dgvSocios.Columns.Contains("colSocioCedula") ? "colSocioCedula" : "Cedula"].Value?.ToString() ?? "",
                        Nombre = fila.Cells[dgvSocios.Columns.Contains("colSocioNombre") ? "colSocioNombre" : "Nombre"].Value?.ToString() ?? "",
                        Apellido = fila.Cells[dgvSocios.Columns.Contains("colSocioApellido") ? "colSocioApellido" : "Apellido"].Value?.ToString() ?? "",
                        Email = fila.Cells[dgvSocios.Columns.Contains("colSocioEmail") ? "colSocioEmail" : "Email"].Value?.ToString() ?? "",
                        Ruc = fila.Cells[dgvSocios.Columns.Contains("colSocioRuc") ? "colSocioRuc" : "Ruc"].Value?.ToString() ?? "",
                        Telefono = fila.Cells[dgvSocios.Columns.Contains("colSocioTelefono") ? "colSocioTelefono" : "Telefono"].Value?.ToString() ?? "",
                        FechaNacimiento = Convert.ToDateTime(fila.Cells[dgvSocios.Columns.Contains("colSocioFechaNacim") ? "colSocioFechaNacim" : "FechaNacimiento"].Value),
                        NombreContactoEmergencia = fila.Cells[dgvSocios.Columns.Contains("colSocioContEmerg") ? "colSocioContEmerg" : "NombreContactoEmergencia"].Value?.ToString() ?? "",
                        TelefonoEmergencia = fila.Cells[dgvSocios.Columns.Contains("colSocioTelEmerg") ? "colSocioTelEmerg" : "TelefonoEmergencia"].Value?.ToString() ?? ""
                    };

                    frmRegistrarSocio frm = new frmRegistrarSocio(socioSeleccionado, usuarioActual);
                    frm.btnCancelar.Visible = true;
                    frm.ShowDialog();
                    CargarGrilla();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccioná un socio de la tabla haciendo clic en la fila.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al capturar el socio. Asegurate de que los nombres de las columnas coincidan: " + ex.Message, "Error interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            if (idSocioSeleccionado == 0)
            {
                MessageBox.Show("Por favor, seleccioná un socio de la tabla primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string colEstado = dgvSocios.Columns.Contains("colSocioEstado") ? "colSocioEstado" : "Estado";
            string colNombre = dgvSocios.Columns.Contains("colSocioNombre") ? "colSocioNombre" : "Nombre";

            string estadoActual = dgvSocios.CurrentRow.Cells[colEstado].Value?.ToString();
            string nombre = dgvSocios.CurrentRow.Cells[colNombre].Value?.ToString();
            string nuevoEstado = (estadoActual == "Activo") ? "Inactivo" : "Activo";

            DialogResult pregunta = MessageBox.Show($"¿Cambiar el estado de {nombre} a {nuevoEstado}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pregunta == DialogResult.Yes)
            {
                SocioNegocio negocio = new SocioNegocio();
                if (negocio.CambiarEstadoSocio(idSocioSeleccionado, nuevoEstado))
                {
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Socios", "Cambio de Estado", $"Se cambió el estado del socio '{nombre}' a {nuevoEstado}.");
                    MessageBox.Show("Estado actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                }
            }
        }
        #endregion
    }
}