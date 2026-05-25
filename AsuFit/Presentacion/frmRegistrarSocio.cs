using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmRegistrarSocio : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTORES
        private Socio socioEdicion = null;
        private Usuario usuarioActual;

        public frmRegistrarSocio(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;
        }

        public frmRegistrarSocio(Socio socioParaEditar, Usuario userLogueado)
        {
            InitializeComponent();
            this.socioEdicion = socioParaEditar;
            usuarioActual = userLogueado;

            // Bloquea la edición del tipo de plan durante la actualización de datos
            cmbPlanes.Enabled = false;

            btnGuardar.Text = "ACTUALIZAR DATOS";
            btnGuardar.Size = new Size(145, 30);

            // Centrado dinámico de los botones de acción principal
            int separacion = 15;
            int anchoTotal = btnGuardar.Width + separacion + btnCancelar.Width;
            int posX = (this.ClientSize.Width - anchoTotal) / 2;

            btnGuardar.Location = new Point(posX, btnGuardar.Location.Y);
            btnCancelar.Location = new Point(posX + btnGuardar.Width + separacion, btnCancelar.Location.Y);

            CargarDatosEnPantalla();
        }
        #endregion

        #region 2. INICIALIZACIÓN Y SISTEMA DE PLACEHOLDERS
        private void frmRegistrarSocio_Load(object sender, EventArgs e)
        {
            ConfigurarTextosDeAyuda();

            txtCedula.MaxLength = 7;
            txtFechaNacimiento.Text = dtpFechaNacimiento.Value.ToShortDateString();

            // Fuerza la selección de la instrucción predeterminada
            cmbPlanes.SelectedIndex = 0;

            // Elimina el foco inicial para permitir la correcta visualización de los placeholders
            this.ActiveControl = null;
        }

        private void ConfigurarTextosDeAyuda()
        {
            AplicarPlaceholder(txtCedula, "Ej: 4588999");
            AplicarPlaceholder(txtRuc, "Ej: 4588999-5");
            AplicarPlaceholder(txtNombre, "Ej: Carlos Miguel");
            AplicarPlaceholder(txtApellido, "Ej: Benítez Rojas");
            AplicarPlaceholder(txtEmail, "ejemplo@correo.com");
            AplicarPlaceholder(txtTelefono, "09XX XXX XXX");
            AplicarPlaceholder(txtContactoEmergencia, "Ej: Laura Rojas");
            AplicarPlaceholder(txtTelefonoEmergencia, "09XX XXX XXX");
        }

        // Gestiona el comportamiento visual de los textos de sugerencia
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
                    txt.Text = "";
                    txt.ForeColor = Color.White;
                }
            };

            txt.Leave += delegate
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = textoAyuda;
                    txt.ForeColor = Color.Silver;
                }
            };
        }

        // Filtra el contenido para evitar el envío de placeholders a la Base de Datos
        private string ObtenerTextoReal(TextBox txt)
        {
            if (txt.Text == (string)txt.Tag) return "";
            return txt.Text;
        }
        #endregion

        #region 3. EVENTOS DE INTERFAZ Y VALIDACIONES
        // Restringe el ingreso de caracteres permitiendo únicamente valores numéricos
        private void ValidarNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Facilita la navegación del usuario entre campos utilizando la tecla Enter
        private void NavegacionEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TextBox txtActivo = sender as TextBox;

                // Validación de campos vacíos antes de ceder el foco
                if (txtActivo != null && string.IsNullOrWhiteSpace(ObtenerTextoReal(txtActivo)))
                {
                    if (txtActivo.Name != "txtEmail" && txtActivo.Name != "txtContactoEmergencia" &&
                        txtActivo.Name != "txtTelefonoEmergencia" && txtActivo.Name != "txtRuc")
                    {
                        MessageBox.Show("Este campo no puede estar vacío.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Transferencia de foco al control desplegable al finalizar la sección de texto
                if (txtActivo != null && txtActivo.Name == "txtTelefonoEmergencia")
                {
                    cmbPlanes.Focus();
                    cmbPlanes.DroppedDown = true;
                    return;
                }

                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        // Mueve el foco de atención al botón de guardado tras la selección de plan
        private void cmbPlanes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnGuardar.Focus();
        }

        // Sincroniza la visualización de la caja de texto con el valor del calendario subyacente
        private void dtpFechaNacimiento_ValueChanged(object sender, EventArgs e)
        {
            txtFechaNacimiento.Text = dtpFechaNacimiento.Value.ToShortDateString();
        }

        // Libera el foco del componente para evitar el remanente de color de selección (Azul nativo)
        private void cmbPlanes_DropDownClosed(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        // Carga los datos de un socio existente en modo edición
        private void CargarDatosEnPantalla()
        {
            txtCedula.Text = socioEdicion.Cedula;
            txtCedula.ForeColor = Color.White;

            txtNombre.Text = socioEdicion.Nombre;
            txtNombre.ForeColor = Color.White;

            txtApellido.Text = socioEdicion.Apellido;
            txtApellido.ForeColor = Color.White;

            txtEmail.Text = socioEdicion.Email;
            txtEmail.ForeColor = Color.White;

            txtRuc.Text = socioEdicion.Ruc;
            txtRuc.ForeColor = Color.White;

            txtTelefono.Text = socioEdicion.Telefono;
            txtTelefono.ForeColor = Color.White;

            dtpFechaNacimiento.Value = socioEdicion.FechaNacimiento;

            txtContactoEmergencia.Text = socioEdicion.NombreContactoEmergencia;
            txtContactoEmergencia.ForeColor = Color.White;

            txtTelefonoEmergencia.Text = socioEdicion.TelefonoEmergencia;
            txtTelefonoEmergencia.ForeColor = Color.White;

            cmbPlanes.Text = "Plan Mensual";
        }
        #endregion

        #region 4. ACCIONES DE GUARDADO Y LIMPIEZA
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Verificación estricta de campos obligatorios
            if (string.IsNullOrWhiteSpace(ObtenerTextoReal(txtCedula)) ||
                string.IsNullOrWhiteSpace(ObtenerTextoReal(txtNombre)) ||
                string.IsNullOrWhiteSpace(ObtenerTextoReal(txtApellido)) ||
                cmbPlanes.SelectedIndex == 0) // Índice 0 = Instrucción de selección
            {
                MessageBox.Show("Por favor, completá los campos obligatorios: Cédula, Nombre, Apellido y Plan.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SocioNegocio negocioSocio = new SocioNegocio();
            int idActual = socioEdicion != null ? socioEdicion.IdSocio : 0;

            // 2. Control de duplicidad de Cédula de Identidad
            if (negocioSocio.ExisteCedula(ObtenerTextoReal(txtCedula), idActual))
            {
                MessageBox.Show("Este número de cédula ya está registrado con otro socio.",
                                "Cédula Duplicada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCedula.Focus();
                return;
            }

            // 3. Mapeo de valores a la entidad Socio
            Socio nuevoSocio = new Socio();
            nuevoSocio.Cedula = ObtenerTextoReal(txtCedula);
            nuevoSocio.Nombre = ObtenerTextoReal(txtNombre);
            nuevoSocio.Apellido = ObtenerTextoReal(txtApellido);
            nuevoSocio.Email = string.IsNullOrWhiteSpace(ObtenerTextoReal(txtEmail)) ? "No especificado" : ObtenerTextoReal(txtEmail);
            nuevoSocio.Ruc = string.IsNullOrWhiteSpace(ObtenerTextoReal(txtRuc)) ? "" : ObtenerTextoReal(txtRuc);
            nuevoSocio.Telefono = ObtenerTextoReal(txtTelefono);
            nuevoSocio.FechaNacimiento = dtpFechaNacimiento.Value;
            nuevoSocio.NombreContactoEmergencia = ObtenerTextoReal(txtContactoEmergencia);
            nuevoSocio.TelefonoEmergencia = ObtenerTextoReal(txtTelefonoEmergencia);
            nuevoSocio.FechaRegistro = DateTime.Now;
            nuevoSocio.Estado = "Activo";

            // 4. Procesamiento de la información del Plan
            string nombrePlanSeleccionado = cmbPlanes.Text;
            PlanNegocio negocioPlan = new PlanNegocio();
            Plan planInfo = negocioPlan.ObtenerPlanPorNombre(nombrePlanSeleccionado);

            if (planInfo == null)
            {
                MessageBox.Show("No se pudo encontrar la información del plan.", "Error interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            nuevoSocio.IdPlan = planInfo.IdPlan;
            nuevoSocio.FechaVencimiento = DateTime.Now.AddDays(planInfo.DuracionDias);

            // 5. Ejecución en Base de Datos (Flujo de Edición o Nuevo Ingreso)
            if (socioEdicion != null)
            {
                nuevoSocio.IdSocio = socioEdicion.IdSocio;
                if (negocioSocio.EditarSocio(nuevoSocio))
                {
                    MessageBox.Show("Los datos se actualizaron correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                string mensajeError = "";
                int nuevoIdSocio = negocioSocio.InsertarSocioYObtenerId(nuevoSocio, out mensajeError);

                if (nuevoIdSocio > 0)
                {
                    // 6. Vinculación automática y apertura del módulo de Caja (Cobro de inscripción)
                    string codigoPlanArtificial = $"PLAN-{planInfo.DuracionDias}-{nuevoIdSocio}-{planInfo.IdPlan}";
                    CarritoGlobal.AgregarItem(0, codigoPlanArtificial, "Inscripción y " + planInfo.NombrePlan, 1, planInfo.Precio, 10);

                    if (CarritoGlobal.IdSocioPagara == null) CarritoGlobal.IdSocioPagara = nuevoIdSocio;

                    frmCajaCobro cajaAbierta = Application.OpenForms["frmCajaCobro"] as frmCajaCobro;
                    if (cajaAbierta != null)
                    {
                        cajaAbierta.WindowState = FormWindowState.Normal;
                        cajaAbierta.BringToFront();
                        cajaAbierta.ActualizarPantallaDesdeCarrito();
                    }
                    else
                    {
                        frmCajaCobro nuevaCaja = new frmCajaCobro(usuarioActual);
                        nuevaCaja.Show();
                    }
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al registrar el socio: " + mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LimpiarCampos()
        {
            txtCedula.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtRuc.Clear();
            txtTelefono.Clear();
            txtContactoEmergencia.Clear();
            txtTelefonoEmergencia.Clear();
            dtpFechaNacimiento.Value = DateTime.Now;
            cmbPlanes.SelectedIndex = 0;

            ConfigurarTextosDeAyuda();
            this.ActiveControl = null;
        }
        #endregion
    }
}