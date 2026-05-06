using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices; // <-- Agregado para el Cue Banner nativo
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmGestionUsuarios : Form
    {
        private int idUsuarioSeleccionado = 0;

        // 1. Traemos la función nativa de Windows
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmGestionUsuarios()
        {
            InitializeComponent();

            // 2. Aseguramos que la letra sea negra para que resalte sobre tu nuevo fondo blanco
            txtBuscar.ForeColor = Color.Black;

            // 3. Aplicamos el placeholder nativo (el "1" evita que se borre al hacer clic)
            SendMessage(txtBuscar.Handle, EM_SETCUEBANNER, 1, "Buscar por Usuario, Nombre o Rol...");
        }

        private void frmGestionUsuarios_Load(object sender, EventArgs e)
        {
            // Por defecto, cargamos solo los usuarios Activos
            CargarGrilla("Activo");
        }

        // --- 1. BUSCADOR LIMPIO ---

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Verificamos que la grilla tenga datos cargados
            if (dgvUsuarios.DataSource is DataTable dt)
            {
                // 1. Filtramos dinámicamente por Nombre Completo o Username
                dt.DefaultView.RowFilter = $"NombreCompleto LIKE '%{txtBuscar.Text}%' OR Username LIKE '%{txtBuscar.Text}%'";

                // 2. ACTUALIZAMOS EL CONTADOR en base a las filas filtradas
                lblTotal.Text = "Registros encontrados: " + dgvUsuarios.Rows.Count.ToString();
            }
        }

        // --- 2. GRILLA Y FILTROS ---

        private void chkMostrarInactivos_CheckedChanged(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void CargarGrilla(string estado)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvUsuarios.DataSource = negocio.ListarUsuarios(estado);

            // 1. Primero ocultamos y configuramos todo el aspecto de la tabla
            if (dgvUsuarios.Columns["IdUsuario"] != null)
                dgvUsuarios.Columns["IdUsuario"].Visible = false;

            if (dgvUsuarios.Columns["Password"] != null)
                dgvUsuarios.Columns["Password"].Visible = false;

            dgvUsuarios.AllowUserToAddRows = false;
            lblTotal.Text = "Registros encontrados: " + dgvUsuarios.Rows.Count.ToString();

            // 2. AHORA SÍ, al final de todo, limpiamos la selección para que quede en blanco
            dgvUsuarios.ClearSelection();

            // Y reseteamos la variable interna por seguridad
            idUsuarioSeleccionado = 0;
        }

        // OPTIMIZACIÓN: Método para no repetir el "if/else"
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
                idUsuarioSeleccionado = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells["IdUsuario"].Value);
            }
        }

        // --- 3. BOTONES ---

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
                Usuario userSeleccionado = new Usuario()
                {
                    IdUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value),
                    NombreCompleto = dgvUsuarios.CurrentRow.Cells["NombreCompleto"].Value.ToString(),
                    Username = dgvUsuarios.CurrentRow.Cells["Username"].Value.ToString(),
                    Rol = dgvUsuarios.CurrentRow.Cells["Rol"].Value.ToString(),
                    Email = dgvUsuarios.CurrentRow.Cells["Email"].Value.ToString(),
                    Estado = dgvUsuarios.CurrentRow.Cells["Estado"].Value.ToString()
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
                int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value);
                string estadoActual = dgvUsuarios.CurrentRow.Cells["Estado"].Value.ToString();
                string nombreUsuario = dgvUsuarios.CurrentRow.Cells["Username"].Value.ToString();

                string nuevoEstado = estadoActual == "Activo" ? "Inactivo" : "Activo";

                DialogResult pregunta = MessageBox.Show($"¿Estás seguro que querés cambiar el estado del usuario '{nombreUsuario}' a {nuevoEstado}?",
                                                        "Confirmar Cambio de Estado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (pregunta == DialogResult.Yes)
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    if (negocio.CambiarEstado(idUsuario, nuevoEstado))
                    {
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
                int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value);
                string nombreUsuario = dgvUsuarios.CurrentRow.Cells["Username"].Value.ToString();

                DialogResult pregunta = MessageBox.Show($"¿Deseás restablecer la contraseña del usuario '{nombreUsuario}' a '12345'?",
                                                        "Confirmar Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (pregunta == DialogResult.Yes)
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    if (negocio.ResetearClave(idUsuario))
                    {
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