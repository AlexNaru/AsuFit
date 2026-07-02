using AsuFit.Negocio;
using AsuFit.Entidades;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public partial class frmArqueoCaja : Form
    {
        #region 1. VARIABLES GLOBALES Y CONSTRUCTOR
        private Usuario usuarioActual;
        private int idTurnoActivo = 0;
        private DateTime fechaAperturaActiva;

        public frmArqueoCaja(Usuario user)
        {
            InitializeComponent();

            this.Load += new EventHandler(frmArqueoCaja_Load);

            usuarioActual = user;
        }
        #endregion

        #region 2. INICIALIZACIÓN
        private void frmArqueoCaja_Load(object sender, EventArgs e)
        {
            ConfigurarTemaOscuro();
            lblCajeroActual.Text = "Cajero: " + usuarioActual.NombreCompleto;
            RevisarEstadoDeCaja();

            // Libera el foco inicial para un despliegue visual completamente limpio
            this.ActiveControl = null;
        }
        #endregion

        #region 3. ESTILOS VISUALES (UI)
        private void ConfigurarTemaOscuro()
        {
            // Leemos la fuente dinámica de la configuración
            float fuenteGlobal = Properties.Settings.Default.TamanoFuente;

            // Fondo general del formulario
            this.BackColor = Color.FromArgb(25, 28, 35);

            // Recorremos los controles para pintar el panel central y los textos
            foreach (Control c in this.Controls)
            {
                if (c is Panel || c is GroupBox)
                {
                    c.BackColor = Color.FromArgb(35, 39, 47); // Gris panel elevado
                    c.ForeColor = Color.White;

                    foreach (Control subC in c.Controls)
                    {
                        if (subC is Label lbl)
                        {
                            lbl.ForeColor = Color.White;
                        }
                    }
                }
                else if (c is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                }
            }

            // Estilos estáticos de los valores monetarios para que resalten
            lblFondoInicial.ForeColor = Color.LightGray;
            lblIngresosEfectivo.ForeColor = Color.MediumSeaGreen;
            lblIngresosTransferencia.ForeColor = Color.MediumSeaGreen;
            lblTotalIngresos.ForeColor = Color.MediumSeaGreen;
            lblGastos.ForeColor = Color.IndianRed;
            lblTotalEsperado.ForeColor = Color.FromArgb(0, 229, 255); // Cian AsuFit
            lblTotalEsperado.Font = new Font(lblTotalEsperado.Font, FontStyle.Bold);

            // Estilo base del botón de Historial
            btnHistorial.FlatStyle = FlatStyle.Flat;
            btnHistorial.FlatAppearance.BorderColor = Color.FromArgb(0, 229, 255);
            btnHistorial.FlatAppearance.BorderSize = 1;
            btnHistorial.BackColor = Color.FromArgb(35, 39, 47);
            btnHistorial.ForeColor = Color.White;

            // FIX: Usamos la fuente global dinámica
            btnHistorial.Font = new Font("Segoe UI", fuenteGlobal, FontStyle.Bold);
            btnHistorial.Cursor = Cursors.Hand;
        }

        // Modifica dinámicamente los botones principales según el estado
        private void ActualizarEstiloBotonesCaja(bool cajaAbierta)
        {
            // Leemos la fuente dinámica de la configuración
            float fuenteGlobal = Properties.Settings.Default.TamanoFuente;

            btnAbrirCaja.FlatStyle = FlatStyle.Flat;
            btnCerrarCaja.FlatStyle = FlatStyle.Flat;

            // FIX: Usamos la fuente global dinámica
            btnAbrirCaja.Font = new Font("Segoe UI", fuenteGlobal, FontStyle.Bold);
            btnCerrarCaja.Font = new Font("Segoe UI", fuenteGlobal, FontStyle.Bold);

            if (cajaAbierta)
            {
                // Botón Abrir apagado
                btnAbrirCaja.BackColor = Color.FromArgb(50, 55, 65);
                btnAbrirCaja.ForeColor = Color.Gray;
                btnAbrirCaja.FlatAppearance.BorderSize = 0;
                btnAbrirCaja.Cursor = Cursors.Default;

                // Botón Cerrar encendido en Rojo/Coral
                btnCerrarCaja.BackColor = Color.IndianRed;
                btnCerrarCaja.ForeColor = Color.White;
                btnCerrarCaja.FlatAppearance.BorderSize = 0;
                btnCerrarCaja.Cursor = Cursors.Hand;
            }
            else
            {
                // Botón Abrir encendido en Cian
                btnAbrirCaja.BackColor = Color.FromArgb(0, 229, 255);
                btnAbrirCaja.ForeColor = Color.Black;
                btnAbrirCaja.FlatAppearance.BorderSize = 0;
                btnAbrirCaja.Cursor = Cursors.Hand;

                // Botón Cerrar apagado
                btnCerrarCaja.BackColor = Color.FromArgb(50, 55, 65);
                btnCerrarCaja.ForeColor = Color.Gray;
                btnCerrarCaja.FlatAppearance.BorderSize = 0;
                btnCerrarCaja.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region 4. SECCIÓN SUPERIOR Y CENTRAL: ESTADO DE CAJA Y RESUMEN
        private void RevisarEstadoDeCaja()
        {
            ArqueoNegocio negocio = new ArqueoNegocio();
            DataTable dtTurno = negocio.ObtenerTurnoAbierto(usuarioActual.IdUsuario);

            if (dtTurno.Rows.Count > 0)
            {
                DataRow turno = dtTurno.Rows[0];
                idTurnoActivo = Convert.ToInt32(turno["IdTurno"]);
                fechaAperturaActiva = Convert.ToDateTime(turno["FechaApertura"]);
                decimal fondoInicial = Convert.ToDecimal(turno["FondoInicial"]);

                lblEstadoCaja.Text = "🟢 CAJA ABIERTA";
                lblEstadoCaja.ForeColor = Color.MediumSeaGreen;

                btnAbrirCaja.Enabled = false;
                btnCerrarCaja.Enabled = true;
                ActualizarEstiloBotonesCaja(true);

                lblFondoInicial.Text = "Gs. " + fondoInicial.ToString("N0");

                DataTable dtTotales = negocio.ObtenerTotalesEnVivo(usuarioActual.IdUsuario, fechaAperturaActiva);

                if (dtTotales.Rows.Count > 0)
                {
                    DataRow totales = dtTotales.Rows[0];
                    decimal efvo = Convert.ToDecimal(totales["TotalEfectivo"]);
                    decimal trans = Convert.ToDecimal(totales["TotalTransferencia"]);
                    decimal gastos = Convert.ToDecimal(totales["TotalGastos"]);

                    lblIngresosEfectivo.Text = "Gs. " + efvo.ToString("N0");
                    lblIngresosTransferencia.Text = "Gs. " + trans.ToString("N0");
                    lblTotalIngresos.Text = "Gs. " + (efvo + trans).ToString("N0");
                    lblGastos.Text = "Gs. " + gastos.ToString("N0");

                    decimal esperado = fondoInicial + efvo - gastos;
                    lblTotalEsperado.Text = "Gs. " + esperado.ToString("N0");
                }
            }
            else
            {
                idTurnoActivo = 0;
                lblEstadoCaja.Text = "🔴 CAJA CERRADA";
                lblEstadoCaja.ForeColor = Color.IndianRed;

                btnAbrirCaja.Enabled = true;
                btnCerrarCaja.Enabled = false;
                ActualizarEstiloBotonesCaja(false);

                lblFondoInicial.Text = "Gs. 0";
                lblIngresosEfectivo.Text = "Gs. 0";
                lblIngresosTransferencia.Text = "Gs. 0";
                lblTotalIngresos.Text = "Gs. 0";
                lblGastos.Text = "Gs. 0";
                lblTotalEsperado.Text = "Gs. 0";
            }
        }
        #endregion

        #region 5. SECCIÓN INFERIOR: ACCIONES DE CAJA
        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            frmAbrirCaja frm = new frmAbrirCaja(usuarioActual);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                RevisarEstadoDeCaja();
            }
        }

        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            decimal fondo = Convert.ToDecimal(lblFondoInicial.Text.Replace("Gs. ", "").Replace(".", ""));
            decimal ingEfvo = Convert.ToDecimal(lblIngresosEfectivo.Text.Replace("Gs. ", "").Replace(".", ""));
            decimal ingTrans = Convert.ToDecimal(lblIngresosTransferencia.Text.Replace("Gs. ", "").Replace(".", ""));
            decimal gastos = Convert.ToDecimal(lblGastos.Text.Replace("Gs. ", "").Replace(".", ""));
            decimal esperado = Convert.ToDecimal(lblTotalEsperado.Text.Replace("Gs. ", "").Replace(".", ""));

            frmCerrarCaja frm = new frmCerrarCaja(idTurnoActivo, usuarioActual.NombreCompleto, fechaAperturaActiva, fondo, ingEfvo, ingTrans, gastos, esperado);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                RevisarEstadoDeCaja();
            }
        }

        private void btnHistorialArqueos_Click(object sender, EventArgs e)
        {
            frmHistorialArqueos frm = new frmHistorialArqueos();

            // Aplicamos la misma escala dinámica heredada de la configuración global
            float escalaActual = Properties.Settings.Default.EscalaInterfaz;
            frm.Scale(new SizeF(escalaActual, escalaActual));

            // Centramos el pop-up respecto a la ventana padre
            frm.StartPosition = FormStartPosition.CenterParent;

            // Al pasarle 'this', le aseguramos a Windows quién es el padre exacto para el centrado
            frm.ShowDialog(this);
        }
        #endregion
    }
}