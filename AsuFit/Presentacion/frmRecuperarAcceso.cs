using System;
using System.Drawing;
using System.Windows.Forms;
using AsuFit.Negocio;

namespace AsuFit.Presentacion
{
    public partial class frmRecuperarAcceso : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private UsuarioNegocio _negocio = new UsuarioNegocio();
        private bool usuarioVerificado = false;

        // Inicializa los componentes de la interfaz, habilita el doble búfer gráfico y suscribe los controladores de eventos para el ciclo de vida del formulario.
        public frmRecuperarAcceso()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.Opacity = 0;
            this.Load += new EventHandler(frmRecuperarAcceso_Load);
            this.Shown += new EventHandler(frmRecuperarAcceso_Shown);
        }
        #endregion

        #region 2. CONFIGURACIÓN VISUAL Y ESCALADO
        // Establece la posición inicial centrada, aplica el redimensionamiento estructural estandarizado y define los enmascaramientos.
        private void frmRecuperarAcceso_Load(object sender, EventArgs e)
        {
            this.SuspendLayout();

            this.Scale(new SizeF(1.2f, 1.2f));
            AjustarFuentesEstandar(this);
            this.CenterToScreen();

            ConfigurarTemaOscuro();

            txtRespuesta.UseSystemPasswordChar = true;
            txtNuevaClave.UseSystemPasswordChar = true;
            txtConfirmarClave.UseSystemPasswordChar = true;

            this.ActiveControl = txtUsuarioRecuperar;

            // Activamos las barreras físicas contra pegado e inyecciones
            SuscribirFiltrosDeSeguridad();

            this.ResumeLayout(false);
        }

        // Modifica la opacidad al valor nominal unitario una vez completada la secuencia de renderizado.
        private void frmRecuperarAcceso_Shown(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        // Replica el estándar estructural del módulo de Socios: aplica una tipografía legible a campos y etiquetas, protegiendo los botones de desbordamiento.
        private void AjustarFuentesEstandar(Control contenedor)
        {
            float fuenteEstandar = 10f;

            foreach (Control c in contenedor.Controls)
            {
                if (c is TextBox || c is ComboBox || c is Label || c is GroupBox)
                {
                    c.Font = new Font("Segoe UI", fuenteEstandar, c.Font.Style);
                }

                if (c.HasChildren)
                {
                    AjustarFuentesEstandar(c);
                }
            }
        }

        // Asigna el color de fondo base e inicia la propagación recursiva de propiedades estéticas sobre los controles secundarios.
        private void ConfigurarTemaOscuro()
        {
            this.BackColor = Color.FromArgb(25, 28, 35);
            AplicarTemaOscuroRecursivo(this);
        }

        // Evalúa y aplica propiedades cromáticas y de estilo de manera selectiva según el tipo de objeto de control detectado.
        private void AplicarTemaOscuroRecursivo(Control contenedor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is GroupBox grp)
                {
                    grp.ForeColor = Color.White;
                }
                else if (c is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                }
                else if (c is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(50, 55, 65);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (c is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;

                    if (btn.Name.Contains("Cancelar"))
                    {
                        btn.BackColor = Color.FromArgb(50, 55, 65);
                        btn.ForeColor = Color.White;
                    }
                    else if (btn.Name.Contains("Buscar"))
                    {
                        btn.BackColor = Color.White;
                        btn.ForeColor = Color.Black;
                    }
                    else
                    {
                        btn.BackColor = Color.FromArgb(0, 229, 255);
                        btn.ForeColor = Color.Black;
                    }
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c);
            }
        }
        #endregion

        #region 3. NAVEGACIÓN Y RESTRICCIONES DE FOCO
        // Captura la pulsación de la tecla Enter en la entrada de usuario para direccionar la ejecución al método de búsqueda de registros.
        private void txtUsuarioRecuperar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnBuscar.PerformClick();
            }
        }

        // Redirecciona el foco de entrada hacia la definición de la nueva clave tras confirmar la pulsación de Enter y la verificación de identidad.
        private void txtRespuesta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && usuarioVerificado)
            {
                e.SuppressKeyPress = true;
                txtNuevaClave.Focus();
            }
        }

        // Desplaza el cursor de inserción hacia el campo de confirmación de credenciales al procesar la entrada de la tecla Enter.
        private void txtNuevaClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && usuarioVerificado)
            {
                e.SuppressKeyPress = true;
                txtConfirmarClave.Focus();
            }
        }

        // Evalúa el estado de verificación y desencadena de forma automatizada la acción del botón de confirmación de cambios.
        private void txtConfirmarClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && usuarioVerificado)
            {
                e.SuppressKeyPress = true;
                btnConfirmar.PerformClick();
            }
        }

        // Evalúa el estado transaccional de verificación antes de admitir la edición del campo de respuesta de seguridad.
        private void txtRespuesta_Enter(object sender, EventArgs e)
        {
            if (!usuarioVerificado)
            {
                MessageBox.Show("Por favor, ingresá primero un nombre de usuario válido.", "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuarioRecuperar.Focus();
            }
        }

        // Valida los privilegios de navegación del contexto actual para restringir la entrada al campo de nueva contraseña.
        private void txtNuevaClave_Enter(object sender, EventArgs e)
        {
            if (!usuarioVerificado)
            {
                MessageBox.Show("Por favor, ingresá primero un nombre de usuario válido.", "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuarioRecuperar.Focus();
            }
        }

        // Restringe la inserción de datos en el campo de confirmación si la identidad del usuario no ha sido previamente autenticada.
        private void txtConfirmarClave_Enter(object sender, EventArgs e)
        {
            if (!usuarioVerificado)
            {
                MessageBox.Show("Por favor, ingresá primero un nombre de usuario válido.", "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuarioRecuperar.Focus();
            }
        }
        #endregion

        #region 4. GESTIÓN DE VISIBILIDAD (OJITOS)
        // Conmuta el enmascaramiento de caracteres del campo de respuesta y actualiza la referencia del recurso gráfico correspondiente.
        private void picOjoRespuesta_Click(object sender, EventArgs e)
        {
            txtRespuesta.UseSystemPasswordChar = !txtRespuesta.UseSystemPasswordChar;
            picOjoRespuesta.Image = txtRespuesta.UseSystemPasswordChar ? Properties.Resources.ojo_cerrado : Properties.Resources.ojo_abierto;
        }

        // Sincroniza la acción del método de selección simple para mitigar anomalías por doble pulsación consecutiva en el control gráfico.
        private void picOjoRespuesta_DoubleClick(object sender, EventArgs e)
        {
            picOjoRespuesta_Click(sender, e);
        }

        // Conmuta el enmascaramiento de caracteres del campo de nueva clave y actualiza la referencia del recurso gráfico correspondiente.
        private void picOjoNueva_Click(object sender, EventArgs e)
        {
            txtNuevaClave.UseSystemPasswordChar = !txtNuevaClave.UseSystemPasswordChar;
            picOjoNueva.Image = txtNuevaClave.UseSystemPasswordChar ? Properties.Resources.ojo_cerrado : Properties.Resources.ojo_abierto;
        }

        // Sincroniza la acción del método de selección simple para mitigar anomalías por doble pulsación consecutiva en el control gráfico.
        private void picOjoNueva_DoubleClick(object sender, EventArgs e)
        {
            picOjoNueva_Click(sender, e);
        }

        // Conmuta el enmascaramiento de caracteres del campo de confirmación y actualiza la referencia del recurso gráfico correspondiente.
        private void picOjoConfirmar_Click(object sender, EventArgs e)
        {
            txtConfirmarClave.UseSystemPasswordChar = !txtConfirmarClave.UseSystemPasswordChar;
            picOjoConfirmar.Image = txtConfirmarClave.UseSystemPasswordChar ? Properties.Resources.ojo_cerrado : Properties.Resources.ojo_abierto;
        }

        // Sincroniza la acción del método de selección simple para mitigar anomalías por doble pulsación consecutiva en el control gráfico.
        private void picOjoConfirmar_DoubleClick(object sender, EventArgs e)
        {
            picOjoConfirmar_Click(sender, e);
        }
        #endregion

        #region 5. LÓGICA DE NEGOCIO Y PERSISTENCIA
        // Realiza la petición síncrona a la capa de negocio para comprobar la existencia del identificador y recuperar la pregunta asociada.
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string username = txtUsuarioRecuperar.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Debes ingresar el nombre de usuario.", "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuarioRecuperar.Focus();
                return;
            }

            string pregunta = _negocio.BuscarPregunta(username);

            if (!string.IsNullOrEmpty(pregunta))
            {
                usuarioVerificado = true;
                lblPreguntaSeguridad.Text = pregunta;
                txtRespuesta.Focus();
            }
            else
            {
                usuarioVerificado = false;
                lblPreguntaSeguridad.Text = "¿Palabra o número de seguridad?";
                MessageBox.Show("El usuario no existe o se encuentra inactivo.", "Error de Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuarioRecuperar.Focus();
            }
        }

        // Valida la integridad de las entradas, comprueba la simetría de las cadenas, aplica el algoritmo criptográfico SHA-256 y confirma los cambios.
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!usuarioVerificado)
            {
                MessageBox.Show("Debes identificar un usuario antes de continuar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtUsuarioRecuperar.Focus();
                return;
            }

            string username = txtUsuarioRecuperar.Text.Trim();
            string respuesta = txtRespuesta.Text.Trim();
            string nuevaClave = txtNuevaClave.Text.Trim();
            string confirmarClave = txtConfirmarClave.Text.Trim();

            if (string.IsNullOrWhiteSpace(respuesta) || string.IsNullOrWhiteSpace(nuevaClave) || string.IsNullOrWhiteSpace(confirmarClave))
            {
                MessageBox.Show("Todos los campos de seguridad son obligatorios.", "Datos faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nuevaClave != confirmarClave)
            {
                MessageBox.Show("Las nuevas contraseñas no coinciden entre sí.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarClave.Focus();
                return;
            }

            string respuestaHasheada = SeguridadHelper.HashearContrasena(respuesta);
            string nuevaClaveHasheada = SeguridadHelper.HashearContrasena(nuevaClave);

            bool exito = _negocio.CambiarPassword(username, respuestaHasheada, nuevaClaveHasheada);

            if (exito)
            {
                MessageBox.Show("La contraseña ha sido actualizada con éxito.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("La respuesta de seguridad proporcionada es incorrecta.", "Fallo de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRespuesta.Clear();
                txtRespuesta.Focus();
            }
        }

        // Intercepta la operación actual y efectúa la destrucción controlada de la ventana sin alterar el estado de persistencia.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 6. GESTIÓN DE SEGURIDAD Y RESTRICCIONES DE ENTRADA
        // Suscribe programáticamente las barreras de contención física para silenciar accesos no autorizados
        private void SuscribirFiltrosDeSeguridad()
        {
            // 1. Sanitización estricta para el nombre de usuario (Lista Blanca alfanumérica)
            txtUsuarioRecuperar.KeyPress += txtAlfanumericoSinEspacios_KeyPress;

            // 2. Anulación del menú contextual nativo de Windows
            ContextMenuStrip menuVacio = new ContextMenuStrip();

            // 3. Inspección profunda recursiva para silenciar el portapapeles en todos los TextBox
            foreach (Control contenedor in this.Controls)
            {
                AsignarBloqueosRecursivo(contenedor, menuVacio);
            }
        }

        // Inspecciona la jerarquía visual neutralizando menús contextuales y comandos de pegado
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

        // Restringe caracteres permitidos exclusivamente a caracteres alfanuméricos, eliminando espacios y símbolos de inyección
        private void txtAlfanumericoSinEspacios_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}