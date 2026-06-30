using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmGestionSocios : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private int idSocioSeleccionado = 0;
        private Usuario usuarioActual;

        public frmGestionSocios(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;

            // Bloquea la autogeneración de columnas para mantener la estructura del diseñador
            dgvSocios.AutoGenerateColumns = false;

            // Aplica la paleta de colores del sistema a la grilla
            ConfigurarTemaOscuroGrilla(dgvSocios);

            CargarGrilla();

            SuscribirFiltrosDeSeguridad();

            // Configura el texto de sugerencia en el buscador
            AplicarPlaceholder(txtBuscar, "Buscar por Cédula, Nombre o Apellido...");

            // Libera el foco del cuadro de texto al iniciar
            this.ActiveControl = null;
        }
        #endregion

        #region 2. ESTILOS VISUALES Y COMPORTAMIENTO UI
        // Gestiona el comportamiento de la marca de agua con desvanecimiento dinámico estilo AsuFit
        private void AplicarPlaceholder(TextBox txt, string textoAyuda)
        {
            txt.Tag = textoAyuda;

            if (string.IsNullOrWhiteSpace(txt.Text) || txt.Text == textoAyuda)
            {
                txt.Text = textoAyuda;
                txt.ForeColor = Color.Silver;
            }
            else
            {
                txt.ForeColor = Color.White;
            }

            txt.Enter += delegate
            {
                if (txt.Text == textoAyuda)
                {
                    this.BeginInvoke(new Action(() => txt.SelectionStart = 0));
                }
            };

            // Intercepta el clic y el arrastre del mouse para impedir que pinten de azul la ayuda
            txt.MouseDown += delegate
            {
                if (txt.Text == textoAyuda)
                {
                    txt.SelectionStart = 0;
                    txt.SelectionLength = 0;
                }
            };

            txt.MouseMove += delegate
            {
                if (txt.Text == textoAyuda && txt.SelectionLength > 0)
                {
                    txt.SelectionStart = 0;
                    txt.SelectionLength = 0;
                }
            };

            txt.TextChanged += delegate
            {
                if (txt.Text != textoAyuda && txt.ForeColor == Color.Silver)
                {
                    string entradaUsuario = txt.Text.Replace(textoAyuda, "");
                    txt.ForeColor = Color.White;
                    txt.Text = entradaUsuario;
                    txt.SelectionStart = txt.Text.Length;
                }
                else if (string.IsNullOrEmpty(txt.Text))
                {
                    txt.ForeColor = Color.Silver;
                    txt.Text = textoAyuda;
                    txt.SelectionStart = 0;
                }
            };

            txt.KeyDown += delegate (object sender, KeyEventArgs e)
            {
                if (txt.Text == textoAyuda && (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
                {
                    e.SuppressKeyPress = true;
                }
            };
        }

        // Aplica el estilo visual premium (Modo Oscuro) al DataGridView
        private void ConfigurarTemaOscuroGrilla(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(25, 28, 35);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(50, 55, 65);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 39, 47);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(25, 28, 35);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 229, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 35;
        }
        #endregion

        #region 3. BÚSQUEDA Y CARGA DE DATOS
        // Carga los registros de socios desde la base de datos según el estado seleccionado
        private void CargarGrilla()
        {
            SocioNegocio negocio = new SocioNegocio();
            string filtroEstado = chkActivo.Checked ? "Inactivo" : "Activo";

            dgvSocios.DataSource = negocio.ListarSocios(filtroEstado);

            idSocioSeleccionado = 0;
            txtBuscar_TextChanged(null, null);
        }

        // Filtra los datos en memoria sin requerir consultas adicionales a la BD
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string textoBusqueda = txtBuscar.Text;
            if (textoBusqueda == (string)txtBuscar.Tag) textoBusqueda = "";

            if (dgvSocios.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = $"Cedula LIKE '%{textoBusqueda}%' OR Apellido LIKE '%{textoBusqueda}%' OR Nombre LIKE '%{textoBusqueda}%'";

                int cantidad = 0;
                foreach (DataGridViewRow fila in dgvSocios.Rows)
                {
                    if (!fila.IsNewRow) cantidad++;
                }
                lblTotal.Text = "Registros encontrados: " + cantidad.ToString();
            }
        }

        // Alterna la visualización entre socios activos e inactivos
        private void chkActivo_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }
        #endregion

        #region 4. GESTIÓN DE GRILLA Y FORMATO CONDICIONAL
        // Configura propiedades de la grilla posteriores al enlace de datos
        private void dgvSocios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvSocios.ClearSelection();

            // Oculta columnas de identificadores internos para evitar su visualización
            foreach (DataGridViewColumn col in dgvSocios.Columns)
            {
                if (col.Name == "colSocioId" ||
                    col.DataPropertyName == "IdSocio" ||
                    col.HeaderText.Trim().ToUpper() == "ID")
                {
                    col.Visible = false;
                }
            }
        }

        // Aplica alertas de color basadas en la fecha de vencimiento de los planes
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
                        // Membresía vencida
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if (diferencia.TotalDays >= 0 && diferencia.TotalDays <= 7)
                    {
                        // Próximo a vencer
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                        dgvSocios.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        // Captura el ID del registro seleccionado
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

        // Libera la selección de la grilla al hacer clic en el panel
        private void frmGestionSocios_Click(object sender, EventArgs e)
        {
            dgvSocios.ClearSelection();
            idSocioSeleccionado = 0;
        }
        #endregion

        #region 5. MÉTODOS AUXILIARES DE FORMULARIO EMERGENTE
        // Configura la escala, fuente y posición del formulario emergente DENTRO DE LA ESCALA ACTUAL
        private void PrepararFormularioComoDashboard(Form frm)
        {
            float escalaActual = Properties.Settings.Default.EscalaInterfaz;

            // Aplica la escala elegida por el usuario
            frm.Scale(new SizeF(escalaActual, escalaActual));
            AjustarFuentes(frm);

            frm.StartPosition = FormStartPosition.Manual;

            // Calcula la posición relativa al panel contenedor para un centrado exacto
            if (this.Parent != null)
            {
                Point posicionPanelAbsoluta = this.Parent.PointToScreen(Point.Empty);
                int x = posicionPanelAbsoluta.X + (this.Parent.Width - frm.Width) / 2;
                int y = posicionPanelAbsoluta.Y + (this.Parent.Height - frm.Height) / 2;

                frm.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterParent;
            }
        }

        // Ajusta recursivamente el tamaño de fuente utilizando la configuración de usuario
        private void AjustarFuentes(Control contenedor)
        {
            float fuenteActual = Properties.Settings.Default.TamanoFuente;

            foreach (Control c in contenedor.Controls)
            {
                if (c is TextBox || c is ComboBox || c is Label)
                {
                    c.Font = new Font("Segoe UI", fuenteActual, c.Font.Style);
                }
                else if (c.HasChildren)
                {
                    AjustarFuentes(c);
                }
            }
        }
        #endregion

        #region 6. BOTONES DE ACCIÓN (CRUD)
        // Instancia el formulario para registrar un nuevo socio
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmRegistrarSocio frm = new frmRegistrarSocio(usuarioActual);
            frm.btnCancelar.Visible = true;

            PrepararFormularioComoDashboard(frm);

            frm.ShowDialog();
            CargarGrilla();
        }

        // Instancia el formulario en modo edición con los datos del registro seleccionado
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

                    PrepararFormularioComoDashboard(frm);

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
                MessageBox.Show("Error al capturar el socio: " + ex.Message, "Error interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Alterna el estado del socio e inserta el registro en el log de auditoría
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
                    AsuFit.Datos.GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Socios", "Cambio de Estado", $"Se cambió el estado del socio '{nombre}' a {nuevoEstado}.");
                    MessageBox.Show("Estado actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrilla();
                }
            }
        }
        #endregion

        #region 7. GESTIÓN DE SEGURIDAD Y RESTRICCIONES DE ENTRADA
        // Suscribe programáticamente todos los controles interactivos a sus barreras de contención
        private void SuscribirFiltrosDeSeguridad()
        {
            // 1. Sanitización en tiempo real contra caracteres que corrompen expresiones RowFilter
            if (txtBuscar != null)
            {
                txtBuscar.KeyPress += txtAntiInyeccion_KeyPress;
            }

            // 2. Neutralización del menú contextual nativo de Windows (Inhabilita pegado por clic derecho)
            ContextMenuStrip menuVacio = new ContextMenuStrip();

            // 3. Inspección profunda recursiva para silenciar el portapapeles (Ctrl+V / Shift+Insert)
            foreach (Control contenedor in this.Controls)
            {
                AsignarBloqueosRecursivo(contenedor, menuVacio);
            }
        }

        // Escanea la vista capturando cuadros de texto en cualquier nivel de anidamiento visual
        private void AsignarBloqueosRecursivo(Control contenedor, ContextMenuStrip menuVacio)
        {
            if (contenedor is TextBox txt)
            {
                txt.KeyDown += BloquearPegado_KeyDown;
                txt.ContextMenuStrip = menuVacio;
            }

            foreach (Control hijo in contenedor.Controls)
            {
                AsignarBloqueosRecursivo(hijo, menuVacio);
            }
        }

        // Intercepta e invalida accesos rápidos de inserción masiva desde el portapapeles
        private void BloquearPegado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
            }
        }

        // Suprime cualquier símbolo ajeno a identidades aplicando el patrón de contención de Lista Blanca (Whitelist)
        private void txtAntiInyeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Autoriza exclusivamente: retroceso, letras, dígitos, espacios y guion. Todo lo demás muere aquí.
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}