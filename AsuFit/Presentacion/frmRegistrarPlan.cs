using AsuFit.Datos;
using AsuFit.Entidades;
using AsuFit.Negocio;
using System;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmRegistrarPlan : Form
    {
        // Esta variable guardará el plan si estamos en modo "Editar"
        private Plan planAEditar = null;
        private Usuario usuarioActual;

        // 1. CONSTRUCTOR PARA MODO "NUEVO"
        public frmRegistrarPlan(Usuario user)
        {
            InitializeComponent();
            usuarioActual = user;
            this.Text = "Nuevo Plan";
        }

        // 2. CONSTRUCTOR PARA MODO "EDITAR"
        public frmRegistrarPlan(Plan planCargado, Usuario user)
        {
            InitializeComponent();
            planAEditar = planCargado;
            usuarioActual = user;
            this.Text = "Editar Plan";
        }

        private void frmRegistrarPlan_Load(object sender, EventArgs e)
        {
            // Si hay un plan cargado, rellenamos los TextBoxes
            if (planAEditar != null)
            {
                txtNombrePlan.Text = planAEditar.NombrePlan;
                txtPrecio.Text = Math.Round(planAEditar.Precio, 0).ToString();
                txtDuracionDias.Text = planAEditar.DuracionDias.ToString();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación básica para evitar que dejen campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombrePlan.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtDuracionDias.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PlanNegocio negocio = new PlanNegocio();
                string mensaje = "";
                bool exito = false;

                // MODO NUEVO
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
                        MessageBox.Show("¡Plan guardado con éxito!", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Cerramos la ventanita
                    }
                }
                // MODO EDITAR
                else
                {
                    planAEditar.NombrePlan = txtNombrePlan.Text.Trim();
                    planAEditar.Precio = Convert.ToDecimal(txtPrecio.Text.Trim());
                    planAEditar.DuracionDias = Convert.ToInt32(txtDuracionDias.Text.Trim());

                    exito = negocio.EditarPlan(planAEditar, out mensaje);

                    if (exito)
                    {
                        GestorAuditoria.Registrar(usuarioActual.NombreCompleto, "Planes", "Edición", $"Modificó el plan '{planAEditar.NombrePlan}'.");
                        MessageBox.Show("¡Plan editado con éxito!", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Cerramos la ventanita
                    }
                }

                // Si ocurrió un error en la base de datos (Ej: Nombre duplicado)
                if (!exito)
                {
                    MessageBox.Show(mensaje, "No se pudo guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingresá solo números en Precio y Duración.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Simplemente cierra la ventana emergente sin guardar ningún cambio, 
            // sin importar si el usuario estaba creando un plan nuevo o editando uno.
            this.Close();
        }
    }
}