using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmRegistrarSocio : Form
    {
        private Socio socioEdicion = null;
        private bool puedeGuardar = false;

        public frmRegistrarSocio()
        {
            InitializeComponent();
        }

        public frmRegistrarSocio(Socio socioParaEditar)
        {
            InitializeComponent();
            this.socioEdicion = socioParaEditar;

            cmbPlanes.Enabled = false;
            cmbMetodoPago.Enabled = false;

            btnGuardar.Text = "ACTUALIZAR DATOS";
            btnGuardar.Size = new System.Drawing.Size(130, 30);

            int centroX = (this.ClientSize.Width - btnGuardar.Width) / 2;
            int posicionY = btnGuardar.Location.Y;
            btnGuardar.Location = new System.Drawing.Point(centroX, posicionY);

            CargarDatosEnPantalla();
        }

        private void frmRegistrarSocio_Load(object sender, EventArgs e)
        {
            txtCedula.Focus();
            txtCedula.MaxLength = 7;
            CambiarEstadoBoton(false);
        }

        private void CambiarEstadoBoton(bool activo)
        {
            puedeGuardar = activo;
            if (activo)
            {
                btnGuardar.ForeColor = Color.White;
                btnGuardar.Cursor = Cursors.Hand;
                btnGuardar.FlatAppearance.BorderColor = Color.White;
            }
            else
            {
                btnGuardar.ForeColor = Color.Silver;
                btnGuardar.Cursor = Cursors.Default;
                btnGuardar.FlatAppearance.BorderColor = Color.Silver;
            }
        }

        private void ValidarNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void NavegacionEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TextBox txtActivo = sender as TextBox;

                if (txtActivo != null && string.IsNullOrWhiteSpace(txtActivo.Text))
                {
                    // Email, Contacto, Teléfono de Emergencia y RUC son opcionales
                    if (txtActivo.Name != "txtEmail" && txtActivo.Name != "txtContactoEmergencia" &&
                        txtActivo.Name != "txtTelefonoEmergencia" && txtActivo.Name != "txtRuc")
                    {
                        MessageBox.Show("Este campo no puede estar vacío.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Forzar el salto al ComboBox y abrir la lista automáticamente
                if (txtActivo != null && txtActivo.Name == "txtTelefonoEmergencia")
                {
                    cmbPlanes.Focus();
                    cmbPlanes.DroppedDown = true;
                    return;
                }

                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbPlanes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnGuardar.Focus();
        }

        private void VerificarCamposObligatorios(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCedula.Text) &&
                !string.IsNullOrWhiteSpace(txtNombre.Text) &&
                !string.IsNullOrWhiteSpace(txtApellido.Text) &&
                !string.IsNullOrWhiteSpace(txtTelefono.Text) &&
                cmbPlanes.SelectedIndex != -1)
            {
                CambiarEstadoBoton(true);
            }
            else
            {
                CambiarEstadoBoton(false);
            }
        }

        private void CargarDatosEnPantalla()
        {
            txtCedula.Text = socioEdicion.Cedula;
            txtNombre.Text = socioEdicion.Nombre;
            txtApellido.Text = socioEdicion.Apellido;
            txtEmail.Text = socioEdicion.Email;

            // --- AQUÍ ESTIRAMOS EL RUC AL EDITAR ---
            txtRuc.Text = socioEdicion.Ruc;

            txtTelefono.Text = socioEdicion.Telefono;
            dtpFechaNacimiento.Value = socioEdicion.FechaNacimiento;
            txtContactoEmergencia.Text = socioEdicion.NombreContactoEmergencia;
            txtTelefonoEmergencia.Text = socioEdicion.TelefonoEmergencia;

            cmbPlanes.Text = "Plan Mensual";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCedula.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) || cmbPlanes.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, completá los campos obligatorios: Cédula, Nombre, Apellido y Plan.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SocioNegocio negocioSocio = new SocioNegocio();
            int idActual = socioEdicion != null ? socioEdicion.IdSocio : 0;

            if (negocioSocio.ExisteCedula(txtCedula.Text, idActual))
            {
                MessageBox.Show("Este número de cédula ya está registrado con otro socio.",
                                "Cédula Duplicada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCedula.Focus();
                return;
            }

            Socio nuevoSocio = new Socio();
            nuevoSocio.Cedula = txtCedula.Text;
            nuevoSocio.Nombre = txtNombre.Text;
            nuevoSocio.Apellido = txtApellido.Text;
            nuevoSocio.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? "No especificado" : txtEmail.Text;

            // --- AQUÍ GUARDAMOS EL RUC (SI ESTÁ VACÍO MANDA UN TEXTO VACÍO) ---
            nuevoSocio.Ruc = string.IsNullOrWhiteSpace(txtRuc.Text) ? "" : txtRuc.Text;

            nuevoSocio.Telefono = txtTelefono.Text;
            nuevoSocio.FechaNacimiento = dtpFechaNacimiento.Value;
            nuevoSocio.NombreContactoEmergencia = txtContactoEmergencia.Text;
            nuevoSocio.TelefonoEmergencia = txtTelefonoEmergencia.Text;
            nuevoSocio.FechaRegistro = DateTime.Now;
            nuevoSocio.Estado = "Activo";

            string nombrePlanSeleccionado = cmbPlanes.Text;
            PlanNegocio negocioPlan = new PlanNegocio();
            Plan planInfo = negocioPlan.ObtenerPlanPorNombre(nombrePlanSeleccionado);

            if (planInfo == null)
            {
                MessageBox.Show("No se pudo encontrar la información del plan en la base de datos.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            nuevoSocio.IdPlan = planInfo.IdPlan;
            nuevoSocio.FechaVencimiento = DateTime.Now.AddDays(planInfo.DuracionDias);

            if (socioEdicion != null)
            {
                nuevoSocio.IdSocio = socioEdicion.IdSocio;
                if (negocioSocio.EditarSocio(nuevoSocio))
                {
                    MessageBox.Show("Los datos del socio se actualizaron correctamente.",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                if (cmbMetodoPago.SelectedIndex == -1)
                {
                    MessageBox.Show("Para un socio nuevo, debes seleccionar el Método de Pago para asentar el ingreso.",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbMetodoPago.Focus();
                    return;
                }

                Pago objPago = new Pago()
                {
                    Monto = planInfo.Precio,
                    MetodoPago = cmbMetodoPago.Text,
                    Concepto = "Inscripción y " + planInfo.NombrePlan
                };

                string mensajeError = "";
                bool exito = negocioSocio.RegistrarSocioYPrimerPago(nuevoSocio, objPago, planInfo.DuracionDias, out mensajeError);

                if (exito)
                {
                    MessageBox.Show("¡Socio registrado y primer pago asentado correctamente!",
                                    "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (this.Modal) this.Close();
                    else
                        LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al registrar: " + mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LimpiarCampos()
        {
            txtCedula.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();

            // --- LIMPIAMOS EL RUC AL TERMINAR ---
            txtRuc.Clear();

            txtTelefono.Clear();
            txtContactoEmergencia.Clear();
            txtTelefonoEmergencia.Clear();
            dtpFechaNacimiento.Value = DateTime.Now;
            cmbPlanes.SelectedIndex = -1;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}