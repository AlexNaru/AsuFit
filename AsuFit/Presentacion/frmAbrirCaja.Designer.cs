namespace AsuFit.Presentacion
{
    partial class frmAbrirCaja
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
            this.label10 = new System.Windows.Forms.Label();
            this.lblEstadoCaja = new System.Windows.Forms.Label();
            this.txtCajero = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMontoInicial = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEmpezar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 6);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(176, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "ABRIR CAJA Y EMPEZAR TURNO";
            // 
            // lblEstadoCaja
            // 
            this.lblEstadoCaja.AutoSize = true;
            this.lblEstadoCaja.Location = new System.Drawing.Point(8, 35);
            this.lblEstadoCaja.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEstadoCaja.Name = "lblEstadoCaja";
            this.lblEstadoCaja.Size = new System.Drawing.Size(111, 13);
            this.lblEstadoCaja.TabIndex = 16;
            this.lblEstadoCaja.Text = "Empleado encargado:";
            // 
            // txtCajero
            // 
            this.txtCajero.Location = new System.Drawing.Point(11, 51);
            this.txtCajero.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCajero.Name = "txtCajero";
            this.txtCajero.ReadOnly = true;
            this.txtCajero.Size = new System.Drawing.Size(109, 20);
            this.txtCajero.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "¿Con cuánto dinero empiezas el turno?";
            // 
            // txtMontoInicial
            // 
            this.txtMontoInicial.Location = new System.Drawing.Point(11, 101);
            this.txtMontoInicial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMontoInicial.MaxLength = 10;
            this.txtMontoInicial.Name = "txtMontoInicial";
            this.txtMontoInicial.ShortcutsEnabled = false;
            this.txtMontoInicial.Size = new System.Drawing.Size(109, 20);
            this.txtMontoInicial.TabIndex = 19;
            this.txtMontoInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMontoInicial_KeyPress);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(47, 140);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(107, 24);
            this.btnCancelar.TabIndex = 20;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEmpezar
            // 
            this.btnEmpezar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpezar.Location = new System.Drawing.Point(25, 176);
            this.btnEmpezar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEmpezar.Name = "btnEmpezar";
            this.btnEmpezar.Size = new System.Drawing.Size(147, 24);
            this.btnEmpezar.TabIndex = 21;
            this.btnEmpezar.Text = "EMPEZAR TURNO";
            this.btnEmpezar.UseVisualStyleBackColor = true;
            this.btnEmpezar.Click += new System.EventHandler(this.btnEmpezar_Click);
            // 
            // frmAbrirCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 212);
            this.Controls.Add(this.btnEmpezar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtMontoInicial);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCajero);
            this.Controls.Add(this.lblEstadoCaja);
            this.Controls.Add(this.label10);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmAbrirCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAbrirCaja";
            this.Load += new System.EventHandler(this.frmAbrirCaja_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblEstadoCaja;
        private System.Windows.Forms.TextBox txtCajero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMontoInicial;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEmpezar;
    }
}