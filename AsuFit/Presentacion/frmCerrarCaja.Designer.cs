namespace AsuFit.Presentacion
{
    partial class frmCerrarCaja
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
            this.btnConfirmarCierre = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtMontoContado = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCajeroCierre = new System.Windows.Forms.TextBox();
            this.lblEstadoCaja = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConfirmarCierre
            // 
            this.btnConfirmarCierre.Location = new System.Drawing.Point(200, 219);
            this.btnConfirmarCierre.Name = "btnConfirmarCierre";
            this.btnConfirmarCierre.Size = new System.Drawing.Size(191, 37);
            this.btnConfirmarCierre.TabIndex = 28;
            this.btnConfirmarCierre.Text = "CONFIRMAR CIERRE";
            this.btnConfirmarCierre.UseVisualStyleBackColor = true;
            this.btnConfirmarCierre.Click += new System.EventHandler(this.btnConfirmarCierre_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(16, 219);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(160, 37);
            this.btnCancelar.TabIndex = 27;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtMontoContado
            // 
            this.txtMontoContado.Location = new System.Drawing.Point(16, 156);
            this.txtMontoContado.Name = "txtMontoContado";
            this.txtMontoContado.Size = new System.Drawing.Size(161, 26);
            this.txtMontoContado.TabIndex = 26;
            this.txtMontoContado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMontoContado_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 20);
            this.label1.TabIndex = 25;
            this.label1.Text = "¿Cuánto dinero tienes en efectivo?";
            // 
            // txtCajeroCierre
            // 
            this.txtCajeroCierre.Location = new System.Drawing.Point(16, 78);
            this.txtCajeroCierre.Name = "txtCajeroCierre";
            this.txtCajeroCierre.ReadOnly = true;
            this.txtCajeroCierre.Size = new System.Drawing.Size(161, 26);
            this.txtCajeroCierre.TabIndex = 24;
            // 
            // lblEstadoCaja
            // 
            this.lblEstadoCaja.AutoSize = true;
            this.lblEstadoCaja.Location = new System.Drawing.Point(12, 54);
            this.lblEstadoCaja.Name = "lblEstadoCaja";
            this.lblEstadoCaja.Size = new System.Drawing.Size(165, 20);
            this.lblEstadoCaja.TabIndex = 23;
            this.lblEstadoCaja.Text = "Empleado encargado:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(198, 20);
            this.label10.TabIndex = 22;
            this.label10.Text = "CERRAR CAJA Y TURNO";
            // 
            // frmCerrarCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 281);
            this.Controls.Add(this.btnConfirmarCierre);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtMontoContado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCajeroCierre);
            this.Controls.Add(this.lblEstadoCaja);
            this.Controls.Add(this.label10);
            this.Name = "frmCerrarCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCerrarCaja";
            this.Load += new System.EventHandler(this.frmCerrarCaja_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConfirmarCierre;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtMontoContado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCajeroCierre;
        private System.Windows.Forms.Label lblEstadoCaja;
        private System.Windows.Forms.Label label10;
    }
}