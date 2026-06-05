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

        // Constructor utilizado para Alta de nuevos planes
        public frmRegistrarPlan(Usuario user)
        {
            InitializeComponent();
            usuarioActual = user;
            this.Text = "Nuevo Plan";
        }

        // Constructor sobrecargado utilizado para Edición de planes existentes
        public frmRegistrarPlan(Plan planCargado, Usuario user)
        {
            InitializeComponent();
            planAEditar = planCargado;
            usuarioActual = user;
            this.Text = "Editar Plan";
        }
        #endregion

        #region 2. INICIALIZACIÓN Y CARGA DE DATOS
        private void frmRegistrarPlan_Load(object sender, EventArgs e)
        {
            ConfigurarTemaOscuro();

            // Despliegue de datos en caso de recibir una entidad existente
            if (planAEditar != null)
            {
                txtNombrePlan.Text = planAEditar.NombrePlan;
                txtPrecio.Text = Math.Round(planAEditar.Precio, 0).ToString();
                txtDuracionDias.Text = planAEditar.DuracionDias.ToString();
            }
        }
        #endregion

        #region 3. ESTILOS VISUALES (TEMA OSCURO)
        private void ConfigurarTemaOscuro()
        {
            float fuenteGlobal = Properties.Settings.Default.TamanoFuente;

            // Fondo general del formulario popup
            this.BackColor = Color.FromArgb(25, 28, 35);

            AplicarTemaOscuroRecursivo(this, fuenteGlobal);
        }

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

                    // El botón secundario hereda color gris, el principal el color corporativo Cian
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

                // Recursividad en caso de usar paneles o groupboxes
                if (c.HasChildren) AplicarTemaOscuroRecursivo(c, fuente);
            }
        }
        #endregion

        #region 4. ACCIONES DEL FORMULARIO (CRUD)
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
                    // Operación: ALTA
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
                    // Operación: MODIFICACIÓN
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

                // Retorno de reglas de negocio fallidas (ej. Nombre duplicado)
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}