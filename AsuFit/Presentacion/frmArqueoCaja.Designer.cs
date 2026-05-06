namespace AsuFit.Presentacion
{
    partial class frmArqueoCaja
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
            this.dtpFechaArqueo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalSistema = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEfectivoCaja = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDiferencia = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnHistorialArqueos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpFechaArqueo
            // 
            this.dtpFechaArqueo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaArqueo.Location = new System.Drawing.Point(232, 90);
            this.dtpFechaArqueo.Name = "dtpFechaArqueo";
            this.dtpFechaArqueo.Size = new System.Drawing.Size(155, 26);
            this.dtpFechaArqueo.TabIndex = 0;
            this.dtpFechaArqueo.ValueChanged += new System.EventHandler(this.dtpFechaArqueo_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fecha del Arqueo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "1. INGRESOS DEL DÍA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Total cobrado en el sistema:";
            // 
            // lblTotalSistema
            // 
            this.lblTotalSistema.AutoSize = true;
            this.lblTotalSistema.Location = new System.Drawing.Point(300, 215);
            this.lblTotalSistema.Name = "lblTotalSistema";
            this.lblTotalSistema.Size = new System.Drawing.Size(18, 20);
            this.lblTotalSistema.TabIndex = 4;
            this.lblTotalSistema.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(87, 288);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(247, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "2. DECLARACIÓN DEL CAJERO";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 333);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Efectivo contado a mano:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(300, 333);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 20);
            this.label6.TabIndex = 7;
            // 
            // txtEfectivoCaja
            // 
            this.txtEfectivoCaja.Location = new System.Drawing.Point(304, 330);
            this.txtEfectivoCaja.Name = "txtEfectivoCaja";
            this.txtEfectivoCaja.Size = new System.Drawing.Size(100, 26);
            this.txtEfectivoCaja.TabIndex = 8;
            this.txtEfectivoCaja.TextChanged += new System.EventHandler(this.txtEfectivoCaja_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(87, 397);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "3. RESULTADO";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(87, 444);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(168, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "Diferencia de Efectivo:";
            // 
            // lblDiferencia
            // 
            this.lblDiferencia.AutoSize = true;
            this.lblDiferencia.Location = new System.Drawing.Point(300, 444);
            this.lblDiferencia.Name = "lblDiferencia";
            this.lblDiferencia.Size = new System.Drawing.Size(18, 20);
            this.lblDiferencia.TabIndex = 11;
            this.lblDiferencia.Text = "0";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(170, 531);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(263, 46);
            this.btnCerrar.TabIndex = 12;
            this.btnCerrar.Text = "🔒 CERRAR CAJA Y GUARDAR";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(249, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 20);
            this.label10.TabIndex = 13;
            this.label10.Text = "ARQUEO";
            // 
            // btnHistorialArqueos
            // 
            this.btnHistorialArqueos.Location = new System.Drawing.Point(170, 591);
            this.btnHistorialArqueos.Name = "btnHistorialArqueos";
            this.btnHistorialArqueos.Size = new System.Drawing.Size(263, 46);
            this.btnHistorialArqueos.TabIndex = 14;
            this.btnHistorialArqueos.Text = "HISTORIAL DE ARQUEOS";
            this.btnHistorialArqueos.UseVisualStyleBackColor = true;
            this.btnHistorialArqueos.Click += new System.EventHandler(this.btnHistorialArqueos_Click);
            // 
            // frmArqueoCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 649);
            this.Controls.Add(this.btnHistorialArqueos);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.lblDiferencia);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEfectivoCaja);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTotalSistema);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFechaArqueo);
            this.Name = "frmArqueoCaja";
            this.Text = "frmArqueoCaja";
            this.Load += new System.EventHandler(this.frmArqueoCaja_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpFechaArqueo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalSistema;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEfectivoCaja;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDiferencia;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnHistorialArqueos;
    }
}