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
            this.btnConfirmarCierre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarCierre.Location = new System.Drawing.Point(11, 175);
            this.btnConfirmarCierre.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnConfirmarCierre.Name = "btnConfirmarCierre";
            this.btnConfirmarCierre.Size = new System.Drawing.Size(152, 24);
            this.btnConfirmarCierre.TabIndex = 28;
            this.btnConfirmarCierre.Text = "CONFIRMAR CIERRE";
            this.btnConfirmarCierre.UseVisualStyleBackColor = true;
            this.btnConfirmarCierre.Click += new System.EventHandler(this.btnConfirmarCierre_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(34, 143);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(107, 24);
            this.btnCancelar.TabIndex = 27;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtMontoContado
            // 
            this.txtMontoContado.Location = new System.Drawing.Point(11, 101);
            this.txtMontoContado.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMontoContado.MaxLength = 10;
            this.txtMontoContado.Name = "txtMontoContado";
            this.txtMontoContado.ShortcutsEnabled = false;
            this.txtMontoContado.Size = new System.Drawing.Size(109, 20);
            this.txtMontoContado.TabIndex = 26;
            this.txtMontoContado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMontoContado_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "¿Cuánto dinero tienes en efectivo?";
            // 
            // txtCajeroCierre
            // 
            this.txtCajeroCierre.Location = new System.Drawing.Point(11, 51);
            this.txtCajeroCierre.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCajeroCierre.Name = "txtCajeroCierre";
            this.txtCajeroCierre.ReadOnly = true;
            this.txtCajeroCierre.Size = new System.Drawing.Size(109, 20);
            this.txtCajeroCierre.TabIndex = 24;
            // 
            // lblEstadoCaja
            // 
            this.lblEstadoCaja.AutoSize = true;
            this.lblEstadoCaja.Location = new System.Drawing.Point(8, 35);
            this.lblEstadoCaja.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEstadoCaja.Name = "lblEstadoCaja";
            this.lblEstadoCaja.Size = new System.Drawing.Size(111, 13);
            this.lblEstadoCaja.TabIndex = 23;
            this.lblEstadoCaja.Text = "Empleado encargado:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 6);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "CERRAR CAJA Y TURNO";
            // 
            // frmCerrarCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 210);
            this.Controls.Add(this.btnConfirmarCierre);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtMontoContado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCajeroCierre);
            this.Controls.Add(this.lblEstadoCaja);
            this.Controls.Add(this.label10);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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