using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmGestionSocios : Form
    {
        private int idSocioSeleccionado = 0;
        private Usuario usuarioActual;

        // 1. Traemos la función nativa de Windows
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public frmGestionSocios(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;

            // Le decimos que cargue los datos apenas nace el formulario
            CargarGrilla();

            // 2. Aplicamos el placeholder nativo. El número "1" es clave para que no se borre al hacer clic.
            SendMessage(txtBuscar.Handle, EM_SETCUEBANNER, 1, "Buscar por Cédula, Nombre o Apellido...");
        }

        // Para la barra de busqueda.
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvSocios.DataSource is DataTable dt)
            {
                // 1. Filtramos la tabla
                dt.DefaultView.RowFilter = $"Cedula LIKE '%{txtBuscar.Text}%' OR Apellido LIKE '%{txtBuscar.Text}%' OR Nombre LIKE '%{txtBuscar.Text}%'";

                // 2. ACTUALIZAMOS EL CONTADOR (Ignorando la fila fantasma si la hubiera)
                int cantidad = 0;
                foreach (DataGridViewRow fila in dgvSocios.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        cantidad++;
                    }
                }
                lblTotal.Text = "Registros encontrados: " + cantidad.ToString();
            }
        }

        private void chkActivo_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        // Método privado para buscar los datos y pegarlos en la tabla
        private void CargarGrilla()
        {
            SocioNegocio negocio = new SocioNegocio();

            // Leemos el CheckBox para saber qué lista traer
            string filtroEstado = chkActivo.Checked ? "Inactivo" : "Activo";

            // Le pasamos un texto vacío para que traiga TODOS los socios (Activos e Inactivos)
            dgvSocios.DataSource = negocio.ListarSocios(filtroEstado);

            // Ocultamos los datos técnicos y de emergencia
            if (dgvSocios.Columns.Contains("IdSocio")) dgvSocios.Columns["IdSocio"].Visible = false;
            if (dgvSocios.Columns.Contains("NombreContactoEmergencia")) dgvSocios.Columns["NombreContactoEmergencia"].Visible = false;
            if (dgvSocios.Columns.Contains("TelefonoEmergencia")) dgvSocios.Columns["TelefonoEmergencia"].Visible = false;
            if (dgvSocios.Columns.Contains("IdPlan")) dgvSocios.Columns["IdPlan"].Visible = false;

            dgvSocios.ClearSelection();
            idSocioSeleccionado = 0;

            // Actualizamos el contador de registros
            int total = dgvSocios.Rows.Count;
            lblTotal.Text = "Registros encontrados: " + total.ToString();

        }

        private void dgvSocios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvSocios.ClearSelection();
        }

        private void dgvSocios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Recordamos el ID
                idSocioSeleccionado = Convert.ToInt32(dgvSocios.Rows[e.RowIndex].Cells["IdSocio"].Value);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Creamos la instancia del registro (constructor vacío = nuevo)
            frmRegistrarSocio frm = new frmRegistrarSocio();

            // MODO FÁCIL: Encendemos el botón que ya acomodaste en el diseño
            frm.btnCancelar.Visible = true;

            // Lo mostramos como diálogo (bloquea la lista hasta que termine)
            frm.ShowDialog();

            // Al cerrar la ventana, refrescamos la grilla para ver al nuevo socio
            CargarGrilla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                // Usamos CurrentRow que es más directo que SelectedRows
                if (idSocioSeleccionado > 0)
                {
                    // Capturamos la fila actual
                    DataGridViewRow fila = dgvSocios.CurrentRow;

                    Socio socioSeleccionado = new Socio
                    {
                        // Usamos el nombre exacto de la columna en SQL.
                        // El ?.ToString() evita errores si la celda está vacía (NULL)
                        IdSocio = Convert.ToInt32(fila.Cells["IdSocio"].Value),
                        Cedula = fila.Cells["Cedula"].Value?.ToString() ?? "",
                        Nombre = fila.Cells["Nombre"].Value?.ToString() ?? "",
                        Apellido = fila.Cells["Apellido"].Value?.ToString() ?? "",
                        Email = fila.Cells["Email"].Value?.ToString() ?? "",

                        // --- AQUÍ ESTÁ LA LÍNEA AGREGADA PARA EL RUC ---
                        Ruc = fila.Cells["RUC"].Value?.ToString() ?? "",

                        Telefono = fila.Cells["Telefono"].Value?.ToString() ?? "",
                        FechaNacimiento = Convert.ToDateTime(fila.Cells["FechaNacimiento"].Value),
                        NombreContactoEmergencia = fila.Cells["NombreContactoEmergencia"].Value?.ToString() ?? "",
                        TelefonoEmergencia = fila.Cells["TelefonoEmergencia"].Value?.ToString() ?? ""
                    };

                    // Abrimos el formulario pasándole el objeto socio
                    frmRegistrarSocio frm = new frmRegistrarSocio(socioSeleccionado);

                    // MODO FÁCIL: Encendemos el botón que ya acomodaste en el diseño
                    frm.btnCancelar.Visible = true;

                    frm.ShowDialog();

                    // Refrescamos la lista al volver
                    CargarGrilla();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccioná un socio de la tabla haciendo clic en la fila.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Este mensaje te dirá exactamente qué nombre de columna está mal escrito
                MessageBox.Show("Error al capturar el socio: " + ex.Message);
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            // 1. Verificamos selección
            if (idSocioSeleccionado == 0)
            {
                MessageBox.Show("Por favor, seleccioná un socio de la tabla primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Determinamos el nuevo estado (el opuesto al actual en la fila)
            string estadoActual = dgvSocios.CurrentRow.Cells["Estado"].Value?.ToString();
            string nuevoEstado = (estadoActual == "Activo") ? "Inactivo" : "Activo";
            string nombre = dgvSocios.CurrentRow.Cells["Nombre"].Value?.ToString();

            // 3. Confirmación y Acción
            DialogResult pregunta = MessageBox.Show($"¿Cambiar el estado de {nombre} a {nuevoEstado}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pregunta == DialogResult.Yes)
            {
                SocioNegocio negocio = new SocioNegocio();
                if (negocio.CambiarEstadoSocio(idSocioSeleccionado, nuevoEstado))
                {
                    GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Socios", "Cambio de Estado", $"Se cambió el estado del socio '{nombre}' a {nuevoEstado}.");
                    MessageBox.Show("Estado actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla(); // Refresca según el filtro del CheckBox 
                }
            }
        }

        private void dgvSocios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSocios.Columns.Contains("FechaVencimiento"))
            {
                var celdaFecha = dgvSocios.Rows[e.RowIndex].Cells["FechaVencimiento"].Value;

                if (celdaFecha != null && celdaFecha != DBNull.Value)
                {
                    DateTime fechaVencimiento = Convert.ToDateTime(celdaFecha);
                    TimeSpan diferencia = fechaVencimiento.Date - DateTime.Now.Date; // Calculamos la diferencia en días

                    // 1. CONDICIÓN ROJA (Vencidos: La fecha ya pasó)
                    if (fechaVencimiento.Date < DateTime.Now.Date)
                    {
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                    }
                    // 2. CONDICIÓN AMARILLA (Alerta: Faltan 7 días o menos, incluyendo HOY)
                    else if (diferencia.TotalDays >= 0 && diferencia.TotalDays <= 7)
                    {
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Gold;
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
        }

        private void frmGestionSocios_Click(object sender, EventArgs e)
        {
            // 1. Limpiamos la selección visual de la tabla
            dgvSocios.ClearSelection();

            // 2. Olvidamos el ID seleccionado para que los botones "Editar" o "Cambiar Estado" no hagan nada por accidente
            idSocioSeleccionado = 0;
        }
    }
}