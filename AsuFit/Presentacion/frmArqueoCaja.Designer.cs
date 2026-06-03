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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnHistorial = new System.Windows.Forms.Button();
            this.lblEstadoCaja = new System.Windows.Forms.Label();
            this.lblCajeroActual = new System.Windows.Forms.Label();
            this.btnAbrirCaja = new System.Windows.Forms.Button();
            this.btnCerrarCaja = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblFondoInicial = new System.Windows.Forms.Label();
            this.lblIngresosEfectivo = new System.Windows.Forms.Label();
            this.lblIngresosTransferencia = new System.Windows.Forms.Label();
            this.lblTotalIngresos = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblGastos = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblTotalEsperado = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 148);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Fondo Inicial (Dinero Base):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 118);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "RESUMEN DEL TURNO ACTUAL:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 6);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(159, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "ARQUEO DE CAJA Y TURNOS";
            // 
            // btnHistorial
            // 
            this.btnHistorial.Location = new System.Drawing.Point(77, 407);
            this.btnHistorial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.Size = new System.Drawing.Size(175, 30);
            this.btnHistorial.TabIndex = 14;
            this.btnHistorial.Text = "HISTORIAL DE ARQUEOS";
            this.btnHistorial.UseVisualStyleBackColor = true;
            this.btnHistorial.Click += new System.EventHandler(this.btnHistorialArqueos_Click);
            // 
            // lblEstadoCaja
            // 
            this.lblEstadoCaja.AutoSize = true;
            this.lblEstadoCaja.Location = new System.Drawing.Point(8, 37);
            this.lblEstadoCaja.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEstadoCaja.Name = "lblEstadoCaja";
            this.lblEstadoCaja.Size = new System.Drawing.Size(75, 13);
            this.lblEstadoCaja.TabIndex = 15;
            this.lblEstadoCaja.Text = "Estado actual:";
            // 
            // lblCajeroActual
            // 
            this.lblCajeroActual.AutoSize = true;
            this.lblCajeroActual.Location = new System.Drawing.Point(212, 37);
            this.lblCajeroActual.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCajeroActual.Name = "lblCajeroActual";
            this.lblCajeroActual.Size = new System.Drawing.Size(40, 13);
            this.lblCajeroActual.TabIndex = 16;
            this.lblCajeroActual.Text = "Cajero:";
            // 
            // btnAbrirCaja
            // 
            this.btnAbrirCaja.Location = new System.Drawing.Point(129, 75);
            this.btnAbrirCaja.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAbrirCaja.Name = "btnAbrirCaja";
            this.btnAbrirCaja.Size = new System.Drawing.Size(107, 24);
            this.btnAbrirCaja.TabIndex = 17;
            this.btnAbrirCaja.Text = "ABRIR CAJA";
            this.btnAbrirCaja.UseVisualStyleBackColor = true;
            this.btnAbrirCaja.Click += new System.EventHandler(this.btnAbrirCaja_Click);
            // 
            // btnCerrarCaja
            // 
            this.btnCerrarCaja.Location = new System.Drawing.Point(93, 367);
            this.btnCerrarCaja.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCerrarCaja.Name = "btnCerrarCaja";
            this.btnCerrarCaja.Size = new System.Drawing.Size(143, 24);
            this.btnCerrarCaja.TabIndex = 18;
            this.btnCerrarCaja.Text = "CERRAR CAJA Y TURNO";
            this.btnCerrarCaja.UseVisualStyleBackColor = true;
            this.btnCerrarCaja.Click += new System.EventHandler(this.btnCerrarCaja_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 188);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "INGRESOS DEL TURNO:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 207);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "> Pagos en Efectivo:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 226);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "> Pagos por Transferencia:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 248);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "Total Ingresos (Sistema):";
            // 
            // lblFondoInicial
            // 
            this.lblFondoInicial.AutoSize = true;
            this.lblFondoInicial.Location = new System.Drawing.Point(196, 148);
            this.lblFondoInicial.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFondoInicial.Name = "lblFondoInicial";
            this.lblFondoInicial.Size = new System.Drawing.Size(13, 13);
            this.lblFondoInicial.TabIndex = 23;
            this.lblFondoInicial.Text = "0";
            // 
            // lblIngresosEfectivo
            // 
            this.lblIngresosEfectivo.AutoSize = true;
            this.lblIngresosEfectivo.Location = new System.Drawing.Point(196, 207);
            this.lblIngresosEfectivo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIngresosEfectivo.Name = "lblIngresosEfectivo";
            this.lblIngresosEfectivo.Size = new System.Drawing.Size(13, 13);
            this.lblIngresosEfectivo.TabIndex = 24;
            this.lblIngresosEfectivo.Text = "0";
            // 
            // lblIngresosTransferencia
            // 
            this.lblIngresosTransferencia.AutoSize = true;
            this.lblIngresosTransferencia.Location = new System.Drawing.Point(196, 226);
            this.lblIngresosTransferencia.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIngresosTransferencia.Name = "lblIngresosTransferencia";
            this.lblIngresosTransferencia.Size = new System.Drawing.Size(13, 13);
            this.lblIngresosTransferencia.TabIndex = 25;
            this.lblIngresosTransferencia.Text = "0";
            // 
            // lblTotalIngresos
            // 
            this.lblTotalIngresos.AutoSize = true;
            this.lblTotalIngresos.Location = new System.Drawing.Point(196, 248);
            this.lblTotalIngresos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalIngresos.Name = "lblTotalIngresos";
            this.lblTotalIngresos.Size = new System.Drawing.Size(13, 13);
            this.lblTotalIngresos.TabIndex = 26;
            this.lblTotalIngresos.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 281);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(128, 13);
            this.label18.TabIndex = 27;
            this.label18.Text = "EGRESOS DEL TURNO:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 302);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(109, 13);
            this.label19.TabIndex = 28;
            this.label19.Text = "> Gastos en Efectivo:";
            // 
            // lblGastos
            // 
            this.lblGastos.AutoSize = true;
            this.lblGastos.Location = new System.Drawing.Point(196, 302);
            this.lblGastos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGastos.Name = "lblGastos";
            this.lblGastos.Size = new System.Drawing.Size(13, 13);
            this.lblGastos.TabIndex = 29;
            this.lblGastos.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 334);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(263, 13);
            this.label21.TabIndex = 30;
            this.label21.Text = "TOTAL ESPERADO EN CAJA (Base + Efvo - Gastos):";
            // 
            // lblTotalEsperado
            // 
            this.lblTotalEsperado.AutoSize = true;
            this.lblTotalEsperado.Location = new System.Drawing.Point(274, 334);
            this.lblTotalEsperado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalEsperado.Name = "lblTotalEsperado";
            this.lblTotalEsperado.Size = new System.Drawing.Size(13, 13);
            this.lblTotalEsperado.TabIndex = 31;
            this.lblTotalEsperado.Text = "0";
            // 
            // frmArqueoCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 453);
            this.Controls.Add(this.lblTotalEsperado);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.lblGastos);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.lblTotalIngresos);
            this.Controls.Add(this.lblIngresosTransferencia);
            this.Controls.Add(this.lblIngresosEfectivo);
            this.Controls.Add(this.lblFondoInicial);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCerrarCaja);
            this.Controls.Add(this.btnAbrirCaja);
            this.Controls.Add(this.lblCajeroActual);
            this.Controls.Add(this.lblEstadoCaja);
            this.Controls.Add(this.btnHistorial);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmArqueoCaja";
            this.Text = "frmArqueoCaja";
            this.Load += new System.EventHandler(this.frmArqueoCaja_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnHistorial;
        private System.Windows.Forms.Label lblEstadoCaja;
        private System.Windows.Forms.Label lblCajeroActual;
        private System.Windows.Forms.Button btnAbrirCaja;
        private System.Windows.Forms.Button btnCerrarCaja;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblFondoInicial;
        private System.Windows.Forms.Label lblIngresosEfectivo;
        private System.Windows.Forms.Label lblIngresosTransferencia;
        private System.Windows.Forms.Label lblTotalIngresos;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblGastos;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblTotalEsperado;
    }
}