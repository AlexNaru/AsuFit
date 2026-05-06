using AsuFit.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmRecuperarAcceso : Form
    {
        // Esto debe estar al principio de la clase
        private UsuarioNegocio _negocio = new UsuarioNegocio();

        public frmRecuperarAcceso()
        {
            InitializeComponent();

            // AGREGÁ ESTAS 3 LÍNEAS AQUÍ
            // Así nos aseguramos de que arranquen deshabilitados (visibles pero grises)
            txtRespuesta.Enabled = false;
            txtNuevaClave.Enabled = false;
            btnConfirmar.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string username = txtUsuarioRecuperar.Text.Trim();

            // Llamamos a la capa de negocio para buscar la pregunta
            string pregunta = _negocio.BuscarPregunta(username);

            if (!string.IsNullOrEmpty(pregunta))
            {
                // AGREGAMOS ESTA LÍNEA: Un aviso visual de que el sistema hizo su trabajo
                MessageBox.Show("Usuario encontrado. Por favor, respondé la pregunta de seguridad.", "Búsqueda Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Si el usuario existe, mostramos la pregunta
                lblPreguntaSeguridad.Text = pregunta;

                // Desbloqueamos los campos de abajo
                txtRespuesta.Enabled = true;
                txtNuevaClave.Enabled = true;
                btnConfirmar.Enabled = true;

                txtRespuesta.Focus(); // Ponemos el cursor ahí para que escriba directo
            }
            else
            {
                MessageBox.Show("El usuario no existe o está inactivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Volvemos a bloquear todo por seguridad
                lblPreguntaSeguridad.Text = "¿Palabra o número de seguridad?";
                txtRespuesta.Enabled = false;
                txtNuevaClave.Enabled = false;
                btnConfirmar.Enabled = false;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string username = txtUsuarioRecuperar.Text.Trim();
            string respuesta = txtRespuesta.Text.Trim();
            string nuevaClave = txtNuevaClave.Text.Trim();

            // Verificamos que no deje campos vacíos
            if (string.IsNullOrEmpty(respuesta) || string.IsNullOrEmpty(nuevaClave))
            {
                MessageBox.Show("Por favor, completá la respuesta y la nueva contraseña.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Intentamos cambiar la clave en la base de datos
            bool exito = _negocio.CambiarPassword(username, respuesta, nuevaClave);

            if (exito)
            {
                MessageBox.Show("¡Contraseña actualizada con éxito! Ya podés iniciar sesión.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Cierra la ventana y te deja en el Login
            }
            else
            {
                MessageBox.Show("La respuesta de seguridad es incorrecta. Intentá de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRespuesta.Clear();
                txtRespuesta.Focus();
            }
        }

        // Para el botón de cancelar, simplemente cerramos esta ventana sin hacer nada más.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra la ventana de recuperación sin hacer cambios
        }
    }
}
