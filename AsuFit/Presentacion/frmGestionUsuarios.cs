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
    public partial class frmGestionUsuarios : Form
    {
        private int idUsuarioSeleccionado = 0;
        private Usuario usuarioActual;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmGestionUsuarios(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;

            // --- EL CAMBIO CLAVE: Bloqueamos las columnas automáticas ---
            dgvUsuarios.AutoGenerateColumns = false;

            txtBuscar.ForeColor = Color.Black;
            SendMessage(txtBuscar.Handle, EM_SETCUEBANNER, 1, "Buscar por Usuario, Nombre o Rol...");
        }

        private void frmGestionUsuarios_Load(object sender, EventArgs e)
        {
            CargarGrilla("Activo");
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = $"NombreCompleto LIKE '%{txtBuscar.Text}%' OR Username LIKE '%{txtBuscar.Text}%'";
                lblTotal.Text = "Registros encontrados: " + dgvUsuarios.Rows.Count.ToString();
            }
        }

        private void chkMostrarInactivos_CheckedChanged(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void CargarGrilla(string estado)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            // Los datos se acomodarán solos según los DataPropertyName
            dgvUsuarios.DataSource = negocio.ListarUsuarios(estado);

            // --- CÓDIGO LIMPIO: Ya no ocultamos columnas por código ---

            lblTotal.Text = "Registros encontrados: " + dgvUsuarios.Rows.Count.ToString();

            dgvUsuarios.ClearSelection();
            idUsuarioSeleccionado = 0;
        }

        private void RecargarGrilla()
        {
            if (chkMostrarInactivos.Checked)
                CargarGrilla("Inactivo");
            else
                CargarGrilla("Activo");
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // ACTUALIZADO: Leemos desde el nuevo "Name" de la columna
                idUsuarioSeleccionado = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells["colUsuarioId"].Value);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmRegistrarUsuario frm = new frmRegistrarUsuario(true);
            frm.ShowDialog();
            RecargarGrilla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado > 0)
            {
                // ACTUALIZADO: Referenciando los nuevos nombres visuales
                Usuario userSeleccionado = new Usuario()
                {
                    IdUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["colUsuarioId"].Value),
                    NombreCompleto = dgvUsuarios.CurrentRow.Cells["colUsuarioNombre"].Value.ToString(),
                    Username = dgvUsuarios.CurrentRow.Cells["colUsuarioUsername"].Value.ToString(),
                    Rol = dgvUsuarios.CurrentRow.Cells["colUsuarioRol"].Value.ToString(),
                    Email = dgvUsuarios.CurrentRow.Cells["colUsuarioEmail"].Value.ToString(),
                    Estado = dgvUsuarios.CurrentRow.Cells["colUsuarioEstado"].Value.ToString()
                };

                frmRegistrarUsuario frm = new frmRegistrarUsuario(userSeleccionado);
                frm.ShowDialog();
                RecargarGrilla();
            }
            else
            {
                MessageBox.Show("Por favor, seleccioná el usuario que querés editar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado > 0)
            {
                // ACTUALIZADO: Referenciando los nuevos nombres visuales
                int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["colUsuarioId"].Value);
                string estadoActual = dgvUsuarios.CurrentRow.Cells["colUsuarioEstado"].Value.ToString();
                string nombreUsuario = dgvUsuarios.CurrentRow.Cells["colUsuarioUsername"].Value.ToString();

                string nuevoEstado = estadoActual == "Activo" ? "Inactivo" : "Activo";

                DialogResult pregunta = MessageBox.Show($"¿Estás seguro que querés cambiar el estado del usuario '{nombreUsuario}' a {nuevoEstado}?",
                                                        "Confirmar Cambio de Estado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (pregunta == DialogResult.Yes)
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    if (negocio.CambiarEstado(idUsuario, nuevoEstado))
                    {
                        GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Usuarios", "Cambio de Estado", $"Cambió el estado de '{nombreUsuario}' a {nuevoEstado}.");
                        MessageBox.Show($"El usuario ahora está {nuevoEstado}.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RecargarGrilla();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccioná un usuario de la tabla haciendo clic en la fila.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnResetearClave_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado > 0)
            {
                // ACTUALIZADO: Referenciando los nuevos nombres visuales
                int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["colUsuarioId"].Value);
                string nombreUsuario = dgvUsuarios.CurrentRow.Cells["colUsuarioUsername"].Value.ToString();

                DialogResult pregunta = MessageBox.Show($"¿Deseás restablecer la contraseña del usuario '{nombreUsuario}' a '12345'?",
                                                        "Confirmar Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (pregunta == DialogResult.Yes)
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    if (negocio.ResetearClave(idUsuario))
                    {
                        GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Usuarios", "Reset de Clave", $"Restableció la contraseña de '{nombreUsuario}'.");
                        MessageBox.Show("Contraseña restablecida con éxito a: 12345", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccioná un usuario de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvUsuarios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvUsuarios.ClearSelection();
        }

        private void frmGestionUsuarios_Click(object sender, EventArgs e)
        {
            dgvUsuarios.ClearSelection();
            idUsuarioSeleccionado = 0;
        }
    }
}