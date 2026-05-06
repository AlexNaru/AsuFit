namespace AsuFit.Presentacion
{
    partial class frmAsistencia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.pnlEstado = new System.Windows.Forms.Panel();
            this.lblTipoPlan = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.timerReloj = new System.Windows.Forms.Timer(this.components);
            this.pnlEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(447, 238);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(100, 26);
            this.txtCedula.TabIndex = 0;
            this.txtCedula.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCedula_KeyDown);
            this.txtCedula.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCedula_KeyPress);
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Location = new System.Drawing.Point(443, 54);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(132, 20);
            this.lblMensaje.TabIndex = 1;
            this.lblMensaje.Text = "¡BIENVENIDO/A!";
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Location = new System.Drawing.Point(443, 136);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(346, 20);
            this.lblDetalle.TabIndex = 2;
            this.lblDetalle.Text = "Ingresá tu número de cédula y presioná ENTER";
            // 
            // pnlEstado
            // 
            this.pnlEstado.BackColor = System.Drawing.Color.Silver;
            this.pnlEstado.Controls.Add(this.lblTipoPlan);
            this.pnlEstado.Controls.Add(this.lblTitulo);
            this.pnlEstado.Controls.Add(this.lblFecha);
            this.pnlEstado.Controls.Add(this.lblHora);
            this.pnlEstado.Controls.Add(this.txtCedula);
            this.pnlEstado.Controls.Add(this.lblDetalle);
            this.pnlEstado.Controls.Add(this.lblMensaje);
            this.pnlEstado.Location = new System.Drawing.Point(75, 67);
            this.pnlEstado.Name = "pnlEstado";
            this.pnlEstado.Size = new System.Drawing.Size(812, 324);
            this.pnlEstado.TabIndex = 3;
            // 
            // lblTipoPlan
            // 
            this.lblTipoPlan.AutoSize = true;
            this.lblTipoPlan.Location = new System.Drawing.Point(443, 186);
            this.lblTipoPlan.Name = "lblTipoPlan";
            this.lblTipoPlan.Size = new System.Drawing.Size(70, 20);
            this.lblTipoPlan.TabIndex = 6;
            this.lblTipoPlan.Text = "TipoPlan";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(18, 19);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(55, 20);
            this.lblTitulo.TabIndex = 5;
            this.lblTitulo.Text = "AsuFit";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(18, 133);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(62, 20);
            this.lblFecha.TabIndex = 4;
            this.lblFecha.Text = "Fecha: ";
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Location = new System.Drawing.Point(18, 75);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(48, 20);
            this.lblHora.TabIndex = 3;
            this.lblHora.Text = "Hora:";
            // 
            // timerReloj
            // 
            this.timerReloj.Enabled = true;
            this.timerReloj.Tick += new System.EventHandler(this.timerReloj_Tick);
            // 
            // frmAsistencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(993, 647);
            this.Controls.Add(this.pnlEstado);
            this.Name = "frmAsistencia";
            this.Text = "frmAsistencia";
            this.pnlEstado.ResumeLayout(false);
            this.pnlEstado.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.Panel pnlEstado;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Timer timerReloj;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblTipoPlan;
    }
}