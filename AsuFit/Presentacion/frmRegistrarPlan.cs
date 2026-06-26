using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmRegistrarPlan : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTORES
        private Plan planAEditar = null;
        private Usuario usuarioActual;

        // Inicializa el formulario en contexto de alta transaccional, garantizando el enlace de inicialización tardía.
        public frmRegistrarPlan(Usuario user)
        {
            InitializeComponent();
            usuarioActual = user;
            this.Text = "Nuevo Plan";

            this.Load += new EventHandler(frmRegistrarPlan_Load);
        }

        // Inicializa el formulario en contexto de modificación de registro, garantizando el enlace de inicialización tardía.
        public frmRegistrarPlan(Plan planCargado, Usuario user)
        {
            InitializeComponent();
            planAEditar = planCargado;
            usuarioActual = user;
            this.Text = "Editar Plan";

            this.Load += new EventHandler(frmRegistrarPlan_Load);
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        // Orquesta la propagación de estilos cromáticos, límites físicos de memoria, volcados de edición y foco inicial al renderizar la vista.
        private void frmRegistrarPlan_Load(object sender, EventArgs e)
        {
            ConfigurarTemaOscuro();

            txtNombrePlan.MaxLength = 100;
            txtPrecio.MaxLength = 9;
            txtDuracionDias.MaxLength = 4;

            txtNombrePlan.ShortcutsEnabled = false;
            txtPrecio.ShortcutsEnabled = false;
            txtDuracionDias.ShortcutsEnabled = false;

            SuscribirFiltrosDeSeguridad();

            if (planAEditar != null)
            {
                txtNombrePlan.Text = planAEditar.NombrePlan;
                txtPrecio.Text = Math.Round(planAEditar.Precio, 0).ToString();
                txtDuracionDias.Text = planAEditar.DuracionDias.ToString();
            }

            // Asigna programáticamente el foco operativo de entrada al primer control superior
            this.ActiveControl = txtNombrePlan;
        }
        #endregion

        #region 3. ESTILOS VISUALES (TEMA OSCURO)
        // Establece la colorimetría base del lienzo e inicia el escaneo recursivo de propagación visual.
        private void ConfigurarTemaOscuro()
        {
            float fuenteGlobal = Properties.Settings.Default.TamanoFuente;
            this.BackColor = Color.FromArgb(25, 28, 35);

            AplicarTemaOscuroRecursivo(this, fuenteGlobal);
        }

        // Ejecuta un recorrido en profundidad sobre la jerarquía del contenedor para inyectar tipografías y paletas de contraste.
        private void AplicarTemaOscuroRecursivo(Control contenedor, float fuente)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                    lbl.Font = new Font("Segoe UI", fuente, lbl.Font.Style);
                }
                else if (c is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(50, 55, 65);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.Font = new Font("Segoe UI", fuente, FontStyle.Regular);
                }
                else if (c is Button btn)
                {
                    btn.Font = new Font("Segoe UI", fuente, FontStyle.Bold);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;

                    if (btn.Name.Contains("Cancelar"))
                    {
                        btn.BackColor = Color.FromArgb(50, 55, 65);
                        btn.ForeColor = Color.White;
                    }
                    else
                    {
                        btn.BackColor = Color.FromArgb(0, 229, 255);
                        btn.ForeColor = Color.Black;
                    }
                }

                if (c.HasChildren) AplicarTemaOscuroRecursivo(c, fuente);
            }
        }
        #endregion

        #region 4. ACCIONES DEL FORMULARIO (CRUD)
        // Evalúa la integridad de los campos obligatorios y despacha la transacción de inserción o actualización hacia la capa de negocio.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombrePlan.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtDuracionDias.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Aviso de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PlanNegocio negocio = new PlanNegocio();
                string mensaje = "";
                bool exito = false;

                if (planAEditar == null)
                {
                    Plan nuevoPlan = new Plan
                    {
                        NombrePlan = txtNombrePlan.Text.Trim(),
                        Precio = Convert.ToDecimal(txtPrecio.Text.Trim()),
                        DuracionDias = Convert.ToInt32(txtDuracionDias.Text.Trim())
                    };

                    exito = negocio.RegistrarPlan(nuevoPlan, out mensaje);

                    if (exito)
                    {
                        GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Planes", "Nuevo Plan", $"Creó el plan '{nuevoPlan.NombrePlan}' por Gs. {nuevoPlan.Precio:N0}.");
                        MessageBox.Show("¡Plan guardado con éxito!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    planAEditar.NombrePlan = txtNombrePlan.Text.Trim();
                    planAEditar.Precio = Convert.ToDecimal(txtPrecio.Text.Trim());
                    planAEditar.DuracionDias = Convert.ToInt32(txtDuracionDias.Text.Trim());

                    exito = negocio.EditarPlan(planAEditar, out mensaje);

                    if (exito)
                    {
                        GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Planes", "Edición", $"Modificó el plan '{planAEditar.NombrePlan}'.");
                        MessageBox.Show("¡Plan editado con éxito!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }

                if (!exito)
                {
                    MessageBox.Show(mensaje, "Conflicto de Regla de Negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Los campos 'Precio' y 'Duración' admiten únicamente valores numéricos enteros.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado en el sistema: " + ex.Message, "Excepción Crítica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Finaliza el ciclo de vida del cuadro de diálogo descartando la operación en curso.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 5. GESTIÓN DE SEGURIDAD Y RESTRICCIONES DE ENTRADA
        // Enlaza los delegados de inspección de caracteres en tiempo de digitación y neutraliza el renderizado del menú contextual.
        private void SuscribirFiltrosDeSeguridad()
        {
            txtPrecio.KeyPress += txtSoloNumeros_KeyPress;
            txtDuracionDias.KeyPress += txtSoloNumeros_KeyPress;
            txtNombrePlan.KeyPress += txtComercialSeguro_KeyPress;

            ContextMenuStrip menuVacio = new ContextMenuStrip();

            foreach (Control contenedor in this.Controls)
            {
                AsignarBloqueosRecursivo(contenedor, menuVacio);
            }
        }

        // Escanea la vista capturando controles TextBox estándar para suprimir accesos rápidos e inyectar menús contextuales nulos.
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

        // Intercepta e invalida comandos de inserción masiva disparados mediante combinaciones de teclado.
        private void BloquearPegado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
            }
        }

        // Restringe el campo para admitir exclusivamente caracteres numéricos enteros y comandos de retroceso.
        private void txtSoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Autoriza caracteres alfanuméricos, espacios y símbolos comerciales paraguayos estándar, destruyendo delimitadores SQL.
        private void txtComercialSeguro_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (!char.IsControl(c) && !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c) &&
                c != '-' && c != '(' && c != ')' && c != '%' && c != '#' && c != '.' && c != '/' && c != '+')
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}