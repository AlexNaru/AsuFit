using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public static class MensajeAsuFit
    {
        #region 1. MÉTODOS DE EXPOSICIÓN (API)

        public static DialogResult Mostrar(IWin32Window propietario, string mensaje, string titulo = "AsuFit", MessageBoxButtons botones = MessageBoxButtons.OK, MessageBoxIcon icono = MessageBoxIcon.Information)
        {
            return Mostrar(mensaje, titulo, botones, icono);
        }

        public static DialogResult Mostrar(string mensaje, string titulo = "AsuFit", MessageBoxButtons botones = MessageBoxButtons.OK, MessageBoxIcon icono = MessageBoxIcon.Information)
        {
            float escalaActual = Properties.Settings.Default.EscalaInterfaz;

            Form frm = new Form();
            frm.ClientSize = new Size(340, 125); // Geometría apaisada compacta
            frm.BackColor = Color.FromArgb(25, 28, 35);
            frm.ForeColor = Color.White;
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.ShowInTaskbar = false;
            frm.Text = "  " + titulo;

            Color colorAcento = Color.FromArgb(0, 229, 255);
            if (icono == MessageBoxIcon.Warning || icono == MessageBoxIcon.Exclamation) colorAcento = Color.Gold;
            else if (icono == MessageBoxIcon.Error || icono == MessageBoxIcon.Stop) colorAcento = Color.LightCoral;

            Label lblMensaje = new Label();
            lblMensaje.Text = mensaje;
            lblMensaje.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblMensaje.ForeColor = Color.White;
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            lblMensaje.Dock = DockStyle.Fill;
            lblMensaje.Padding = new Padding(20, 5, 20, 5);

            Panel pnlBotones = new Panel();
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Height = 40; // Panel inferior estilizado
            pnlBotones.BackColor = Color.FromArgb(35, 39, 47);

            Panel pnlAcento = new Panel();
            pnlAcento.Dock = DockStyle.Top;
            pnlAcento.Height = 3; // Línea superior fina de 3px
            pnlAcento.BackColor = colorAcento;

            frm.Controls.Add(lblMensaje);
            frm.Controls.Add(pnlBotones);
            frm.Controls.Add(pnlAcento);

            if (botones == MessageBoxButtons.YesNo)
            {
                // Distribución matemática exacta simétrica para 2 botones
                Button btnSi = CrearBoton("SÍ", colorAcento, colorAcento == Color.LightCoral ? Color.White : Color.Black);
                btnSi.Location = new Point(75, 7);
                btnSi.Click += delegate { frm.DialogResult = DialogResult.Yes; frm.Close(); };
                pnlBotones.Controls.Add(btnSi);

                Button btnNo = CrearBoton("NO", Color.FromArgb(50, 55, 65), Color.White);
                btnNo.Location = new Point(177, 7);
                btnNo.Click += delegate { frm.DialogResult = DialogResult.No; frm.Close(); };
                pnlBotones.Controls.Add(btnNo);
            }
            else
            {
                // Centrado geométrico absoluto para botón único (Ancho 340 - Botón 88) / 2 = 126
                Button btnOk = CrearBoton("ACEPTAR", colorAcento, colorAcento == Color.LightCoral ? Color.White : Color.Black);
                btnOk.Location = new Point(126, 7);
                btnOk.Click += delegate { frm.DialogResult = DialogResult.OK; frm.Close(); };
                pnlBotones.Controls.Add(btnOk);
            }

            frm.Scale(new SizeF(escalaActual, escalaActual));

            CentrarEnContenedor(frm);

            return frm.ShowDialog();
        }
        #endregion

        #region 2. MÉTODOS AUXILIARES DE MAQUETACIÓN Y POSICIÓN

        private static Button CrearBoton(string texto, Color bg, Color txt)
        {
            Button btn = new Button();
            btn.Text = texto;
            btn.Size = new Size(88, 25); // Proporción áurea de botón limpio
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bg;
            btn.ForeColor = txt;
            btn.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold); // Estándar nativo sobrio
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        private static void CentrarEnContenedor(Form frm)
        {
            Form dashboard = Application.OpenForms["frmDashboard"];
            if (dashboard != null)
            {
                Control contenedor = dashboard.Controls.Find("pnlContenedor", true).FirstOrDefault();
                if (contenedor != null)
                {
                    frm.StartPosition = FormStartPosition.Manual;
                    Point pos = contenedor.PointToScreen(Point.Empty);
                    int x = pos.X + (contenedor.Width - frm.Width) / 2;
                    int y = pos.Y + (contenedor.Height - frm.Height) / 2;
                    frm.Location = new Point(x > 0 ? x : 0, y > 0 ? y : 0);
                    return;
                }
            }
            frm.StartPosition = FormStartPosition.CenterScreen;
        }
        #endregion
    }
}