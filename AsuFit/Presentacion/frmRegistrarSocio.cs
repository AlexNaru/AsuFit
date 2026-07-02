using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmRegistrarSocio : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTORES
        private Socio socioEdicion = null;
        private Usuario usuarioActual;

        // Inicializa el formulario en contexto de inserción de un nuevo registro.
        public frmRegistrarSocio(Usuario userLogueado)
        {
            InitializeComponent();
            usuarioActual = userLogueado;

            // FIX ARQUITECTÓNICO: Garantiza el enlace absoluto del evento Load programáticamente
            this.Load += new EventHandler(frmRegistrarSocio_Load);
        }

        // Inicializa el formulario en contexto de modificación, bloqueando campos inmutables y centrando controles de acción.
        public frmRegistrarSocio(Socio socioParaEditar, Usuario userLogueado)
        {
            InitializeComponent();
            this.socioEdicion = socioParaEditar;
            usuarioActual = userLogueado;

            cmbPlanes.Enabled = false;

            btnGuardar.Text = "ACTUALIZAR DATOS";
            btnGuardar.Size = new Size(145, 30);

            int separacion = 15;
            int anchoTotal = btnGuardar.Width + separacion + btnCancelar.Width;
            int posX = (this.ClientSize.Width - anchoTotal) / 2;

            btnGuardar.Location = new Point(posX, btnGuardar.Location.Y);
            btnCancelar.Location = new Point(posX + btnGuardar.Width + separacion, btnCancelar.Location.Y);

            // FIX ARQUITECTÓNICO: Garantiza el enlace absoluto del evento Load programáticamente
            this.Load += new EventHandler(frmRegistrarSocio_Load);

            CargarDatosEnPantalla();
        }
        #endregion

        #region 2. INICIALIZACIÓN Y SISTEMA DE PLACEHOLDERS
        // Orquesta la configuración geométrica, límites lógicos de entrada y directrices de UI al renderizar la vista.
        private void frmRegistrarSocio_Load(object sender, EventArgs e)
        {
            txtCedula.MaxLength = 8;
            txtRuc.MaxLength = 15;
            txtTelefono.MaxLength = 20;
            txtTelefonoEmergencia.MaxLength = 20;

            dtpFechaNacimiento.MaxDate = DateTime.Now.AddYears(-14);
            dtpFechaNacimiento.MinDate = DateTime.Now.AddYears(-100);

            if (socioEdicion == null)
            {
                dtpFechaNacimiento.Value = dtpFechaNacimiento.MaxDate;
            }

            txtFechaNacimiento.Text = dtpFechaNacimiento.Value.ToShortDateString();

            ConfigurarTextosDeAyuda();
            SuscribirFiltrosDeSeguridad();

            cmbPlanes.SelectedIndex = 0;

            this.ActiveControl = txtCedula;
        }

        // Centraliza la asignación de descriptores visuales interactivos para los campos de captura de datos.
        private void ConfigurarTextosDeAyuda()
        {
            AplicarPlaceholder(txtCedula, "Ej: 5123456");
            AplicarPlaceholder(txtRuc, "Ej: 5123456-7");
            AplicarPlaceholder(txtNombre, "Ej: Juan");
            AplicarPlaceholder(txtApellido, "Ej: Perez");
            AplicarPlaceholder(txtEmail, "ejemplo@correo.com");
            AplicarPlaceholder(txtTelefono, "09XX XXX XXX");
            AplicarPlaceholder(txtContactoEmergencia, "Ej: Juana Perez");
            AplicarPlaceholder(txtTelefonoEmergencia, "09XX XXX XXX");
        }

        // Gestiona la mutación de estado visual, colorimetría y posicionamiento del cursor simulando un atributo nativo.
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

            txt.Enter += (s, e) =>
            {
                if (txt.Text == textoAyuda)
                {
                    this.BeginInvoke((MethodInvoker)delegate ()
                    {
                        txt.SelectionStart = 0;
                        txt.SelectionLength = 0;
                    });
                }
            };

            txt.KeyDown += (s, e) =>
            {
                if (txt.Text == textoAyuda && e.KeyCode != Keys.Tab && e.KeyCode != Keys.Enter)
                {
                    txt.Text = "";
                    txt.ForeColor = Color.White;
                }
            };

            txt.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = textoAyuda;
                    txt.ForeColor = Color.Silver;
                }
            };
        }

        // Discrimina cadenas enlazadas al metadato del control para retornar valores absolutos listos para persistencia.
        private string ObtenerTextoReal(TextBox txt)
        {
            if (txt.Text == (string)txt.Tag) return "";
            return txt.Text.Trim();
        }

        // Vuelca la estructura de la entidad en memoria hacia los controles correspondientes, aplicando contraste de edición.
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

        #region 3. RESTRICCIONES FÍSICAS DE TECLADO Y SEGURIDAD
        // Suscribe programáticamente todos los controles a sus filtros de sanitización y bloqueos físicos
        private void SuscribirFiltrosDeSeguridad()
        {
            txtCedula.KeyPress += txtSoloNumeros_KeyPress;
            txtTelefono.KeyPress += txtTelefono_KeyPress;
            txtTelefonoEmergencia.KeyPress += txtTelefono_KeyPress;

            txtNombre.KeyPress += txtAlfabetico_KeyPress;
            txtApellido.KeyPress += txtAlfabetico_KeyPress;
            txtContactoEmergencia.KeyPress += txtAlfabetico_KeyPress;

            txtRuc.KeyPress += txtRuc_KeyPress;
            txtEmail.KeyPress += txtEmail_KeyPress; // Blindaje estricto de sintaxis de correo

            // Anulación del menú contextual nativo de Windows (Mitiga pegado por clic derecho)
            ContextMenuStrip menuVacio = new ContextMenuStrip();

            foreach (Control contenedor in this.Controls)
            {
                AsignarBloqueosRecursivo(contenedor, menuVacio);
            }
        }

        // Inspecciona la jerarquía de la vista capturando TextBoxes en cualquier nivel de anidamiento
        private void AsignarBloqueosRecursivo(Control contenedor, ContextMenuStrip menuVacio)
        {
            if (contenedor is TextBox txt)
            {
                txt.KeyDown += BloquearPegado_KeyDown;
                txt.ContextMenuStrip = menuVacio; // Neutraliza el clic derecho
            }

            foreach (Control hijo in contenedor.Controls)
            {
                AsignarBloqueosRecursivo(hijo, menuVacio);
            }
        }

        // Invalida combinaciones de teclado orientadas a la inserción masiva desde el portapapeles
        private void BloquearPegado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
            }
        }

        // Limita el ingreso de datos exclusivamente a secuencias numéricas y retroceso
        private void txtSoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Aplica exclusión a caracteres no pertenecientes al alfabeto, permitiendo separadores de espacio
        private void txtAlfabetico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Autoriza la digitación de formatos telefónicos que requieran el uso del prefijo internacional (+)
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }
        }

        // Permite estructuras conformadas por dígitos y el guion delimitador estándar tributario
        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        // Filtra caracteres ilegales en tiempo real según especificaciones de formato RFC 5322
        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '@' && e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != '_')
            {
                e.Handled = true;
            }
        }
        #endregion

        #region 4. EVENTOS DE INTERFAZ Y NAVEGACIÓN
        // Intercepta la pulsación de retorno del carro (Enter) para validar obligatoriedad antes de transferir el foco al control adyacente.
        private void NavegacionEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TextBox txtActivo = sender as TextBox;

                if (txtActivo != null && string.IsNullOrWhiteSpace(ObtenerTextoReal(txtActivo)))
                {
                    if (txtActivo.Name != "txtEmail" && txtActivo.Name != "txtContactoEmergencia" &&
                        txtActivo.Name != "txtTelefonoEmergencia" && txtActivo.Name != "txtRuc")
                    {
                        MessageBox.Show("Este campo no puede estar vacío.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (txtActivo != null && txtActivo.Name == "txtTelefonoEmergencia")
                {
                    cmbPlanes.Focus();
                    cmbPlanes.DroppedDown = true;
                    return;
                }

                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        // Ejecuta el traslado de la atención de entrada hacia el control de transacción principal.
        private void cmbPlanes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnGuardar.Focus();
        }

        // Extrapola el valor DateTime interno hacia el componente visual de exposición textual.
        private void dtpFechaNacimiento_ValueChanged(object sender, EventArgs e)
        {
            txtFechaNacimiento.Text = dtpFechaNacimiento.Value.ToShortDateString();
        }

        // Revoca el foco residual post-despliegue mitigando anomalías visuales en el renderizado de selección.
        private void cmbPlanes_DropDownClosed(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        #endregion

        #region 5. LÓGICA TRANSACCIONAL, VALIDACIÓN Y AUDITORÍA
        // Efectúa comprobaciones paramétricas, evalúa unicidad, ejecuta el procedimiento almacenado y despacha al módulo de facturación.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (socioEdicion == null && cmbPlanes.SelectedIndex <= 0)
            {
                MessageBox.Show("Por favor, seleccioná el Plan para el nuevo socio.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Socio nuevoSocio = new Socio();
            nuevoSocio.Cedula = ObtenerTextoReal(txtCedula);
            nuevoSocio.Nombre = ObtenerTextoReal(txtNombre);
            nuevoSocio.Apellido = ObtenerTextoReal(txtApellido);
            nuevoSocio.Email = string.IsNullOrWhiteSpace(ObtenerTextoReal(txtEmail)) ? "No especificado" : ObtenerTextoReal(txtEmail);
            nuevoSocio.Ruc = ObtenerTextoReal(txtRuc);
            nuevoSocio.Telefono = ObtenerTextoReal(txtTelefono);
            nuevoSocio.FechaNacimiento = dtpFechaNacimiento.Value;
            nuevoSocio.NombreContactoEmergencia = ObtenerTextoReal(txtContactoEmergencia);
            nuevoSocio.TelefonoEmergencia = ObtenerTextoReal(txtTelefonoEmergencia);
            nuevoSocio.FechaRegistro = DateTime.Now;
            nuevoSocio.Estado = "Activo";

            PlanNegocio negocioPlan = new PlanNegocio();
            SocioNegocio negocioSocio = new SocioNegocio();
            string mensajeError = "";

            if (socioEdicion != null)
            {
                nuevoSocio.IdPlan = negocioPlan.ObtenerPlanPorNombre("Plan Mensual")?.IdPlan ?? 1;
                nuevoSocio.IdSocio = socioEdicion.IdSocio;

                // Delegación con parámetro out
                if (negocioSocio.EditarSocio(nuevoSocio, out mensajeError))
                {
                    AsuFit.Datos.GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Socios", "Edición", $"Se actualizaron los datos: {nuevoSocio.Nombre} ({nuevoSocio.Cedula}).");
                    MessageBox.Show("Actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else MessageBox.Show(mensajeError, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Plan planInfo = negocioPlan.ObtenerPlanPorNombre(cmbPlanes.Text);
                if (planInfo == null) return;

                nuevoSocio.IdPlan = planInfo.IdPlan;
                nuevoSocio.FechaVencimiento = DateTime.Now.AddDays(planInfo.DuracionDias);

                // Delegación pura
                int nuevoId = negocioSocio.InsertarSocioYObtenerId(nuevoSocio, out mensajeError);

                if (nuevoId > 0)
                {
                    AsuFit.Datos.GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Socios", "Registro", $"Registrado: {nuevoSocio.Nombre} ({nuevoSocio.Cedula}).");

                    CarritoGlobal.AgregarItem(0, $"PLAN-{planInfo.DuracionDias}-{nuevoId}-{planInfo.IdPlan}", "Inscripción y " + planInfo.NombrePlan, 1, planInfo.Precio, 10);
                    if (CarritoGlobal.IdSocioPagara == null) CarritoGlobal.IdSocioPagara = nuevoId;

                    // FIX DE CAJA: Cerramos si está abierta en fondo y abrimos limpia
                    if (Application.OpenForms["frmCajaCobro"] is frmCajaCobro cajaAbierta) cajaAbierta.Close();

                    new frmCajaCobro(usuarioActual).ShowDialog();
                    LimpiarCampos();
                }
                else MessageBox.Show(mensajeError, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Finaliza el ciclo de vida del proceso de diálogo liberando recursos de instancia.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Revierte el estado de los componentes visuales a su configuración nominal de arranque.
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
            dtpFechaNacimiento.Value = dtpFechaNacimiento.MaxDate;
            cmbPlanes.SelectedIndex = 0;

            ConfigurarTextosDeAyuda();
            this.ActiveControl = txtCedula;
        }
        #endregion
    }
}