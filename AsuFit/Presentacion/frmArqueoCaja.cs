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
            usuarioActual = user;
        }
        #endregion

        #region 2. INICIALIZACIÓN
        private void frmArqueoCaja_Load(object sender, EventArgs e)
        {
            lblCajeroActual.Text = "Cajero: " + usuarioActual.NombreCompleto;
            RevisarEstadoDeCaja();
        }
        #endregion

        #region 3. SECCIÓN SUPERIOR Y CENTRAL: ESTADO DE CAJA Y RESUMEN
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

                lblFondoInicial.Text = "Gs. 0";
                lblIngresosEfectivo.Text = "Gs. 0";
                lblIngresosTransferencia.Text = "Gs. 0";
                lblTotalIngresos.Text = "Gs. 0";
                lblGastos.Text = "Gs. 0";
                lblTotalEsperado.Text = "Gs. 0";
            }
        }
        #endregion

        #region 4. SECCIÓN INFERIOR: ACCIONES DE CAJA
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
            frm.ShowDialog();
        }
        #endregion
    }
}