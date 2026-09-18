using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AsuFit.Presentacion
{
    public static class MensajeAsuFit
    {
        #region 1. MÉTODOS DE EXPOSICIÓN (API)

        // Expone la sobrecarga principal que permite anclar el mensaje a un componente propietario.
        public static DialogResult Mostrar(IWin32Window propietario, string mensaje, string titulo = "AsuFit", MessageBoxButtons botones = MessageBoxButtons.OK, MessageBoxIcon icono = MessageBoxIcon.Information)
        {
            return Mostrar(mensaje, titulo, botones, icono);
        }

        // Construye, renderiza y despliega un cuadro de diálogo modal personalizado utilizando parámetros estándar de Windows Forms.
        // Implementa limpieza automática de recursos mediante la directiva using y escalado dinámico de geometría.
        public static DialogResult Mostrar(string mensaje, string titulo = "AsuFit", MessageBoxButtons botones = MessageBoxButtons.OK, MessageBoxIcon icono = MessageBoxIcon.Information)
        {
            float escalaActual = Properties.Settings.Default.EscalaInterfaz;

            using (Form frm = new Form())
            {
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
                lblMensaje.MaximumSize = new Size(550, 0);
                lblMensaje.AutoSize = true;
                lblMensaje.Padding = new Padding(20, 20, 20, 20);

                int anchoFormulario = Math.Max(340, lblMensaje.PreferredWidth);
                int altoFormulario = Math.Max(125, lblMensaje.PreferredHeight + 43);

                frm.ClientSize = new Size(anchoFormulario, altoFormulario);

                lblMensaje.AutoSize = false;
                lblMensaje.Dock = DockStyle.Fill;

                Panel pnlBotones = new Panel();
                pnlBotones.Dock = DockStyle.Bottom;
                pnlBotones.Height = 40;
                pnlBotones.BackColor = Color.FromArgb(35, 39, 47);

                Panel pnlAcento = new Panel();
                pnlAcento.Dock = DockStyle.Top;
                pnlAcento.Height = 3;
                pnlAcento.BackColor = colorAcento;

                frm.Controls.Add(lblMensaje);
                frm.Controls.Add(pnlBotones);
                frm.Controls.Add(pnlAcento);

                if (botones == MessageBoxButtons.YesNo)
                {
                    int inicioX = (anchoFormulario - 191) / 2;

                    Button btnSi = CrearBoton("SÍ", colorAcento, colorAcento == Color.LightCoral ? Color.White : Color.Black);
                    btnSi.Location = new Point(inicioX, 7);
                    btnSi.Click += delegate { frm.DialogResult = DialogResult.Yes; frm.Close(); };
                    pnlBotones.Controls.Add(btnSi);

                    Button btnNo = CrearBoton("NO", Color.FromArgb(50, 55, 65), Color.White);
                    btnNo.Location = new Point(inicioX + 103, 7);
                    btnNo.Click += delegate { frm.DialogResult = DialogResult.No; frm.Close(); };
                    pnlBotones.Controls.Add(btnNo);

                    frm.AcceptButton = btnSi;
                    frm.CancelButton = btnNo;
                }
                else
                {
                    int inicioX = (anchoFormulario - 88) / 2;

                    Button btnOk = CrearBoton("ACEPTAR", colorAcento, colorAcento == Color.LightCoral ? Color.White : Color.Black);
                    btnOk.Location = new Point(inicioX, 7);
                    btnOk.Click += delegate { frm.DialogResult = DialogResult.OK; frm.Close(); };
                    pnlBotones.Controls.Add(btnOk);

                    frm.AcceptButton = btnOk;
                    frm.CancelButton = btnOk;
                }

                frm.Scale(new SizeF(escalaActual, escalaActual));
                CentrarEnContenedor(frm);

                // Emite el estímulo auditivo nativo del sistema operativo correspondiente al nivel de severidad del mensaje.
                switch (icono)
                {
                    case MessageBoxIcon.Error:
                        System.Media.SystemSounds.Hand.Play();
                        break;
                    case MessageBoxIcon.Question:
                        System.Media.SystemSounds.Question.Play();
                        break;
                    case MessageBoxIcon.Warning:
                        System.Media.SystemSounds.Exclamation.Play();
                        break;
                    case MessageBoxIcon.Information:
                        System.Media.SystemSounds.Asterisk.Play();
                        break;
                    case MessageBoxIcon.None:
                        break;
                }

                return frm.ShowDialog();
            }
        }
        #endregion

        #region 2. MÉTODOS AUXILIARES DE MAQUETACIÓN Y POSICIÓN

        // Instancia un control de tipo botón aplicando la paleta de colores corporativa y deshabilitando los bordes nativos.
        private static Button CrearBoton(string texto, Color bg, Color txt)
        {
            Button btn = new Button();
            btn.Text = texto;
            btn.Size = new Size(88, 25);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bg;
            btn.ForeColor = txt;
            btn.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        // Calcula las coordenadas absolutas de pantalla para renderizar el cuadro de diálogo sobre el área de trabajo activa.
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