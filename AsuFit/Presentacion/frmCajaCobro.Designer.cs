namespace AsuFit.Presentacion
{
    partial class frmCajaCobro
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalCobrar = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBusquedaCliente = new System.Windows.Forms.TextBox();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.txtRucCliente = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
            this.cmbTipoComprobante = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMontoRecibido = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtVuelto = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.txtCajero = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "CAJA Y FACTURACIÓN";
            // 
            // lblTotalCobrar
            // 
            this.lblTotalCobrar.AutoSize = true;
            this.lblTotalCobrar.Location = new System.Drawing.Point(147, 83);
            this.lblTotalCobrar.Name = "lblTotalCobrar";
            this.lblTotalCobrar.Size = new System.Drawing.Size(54, 20);
            this.lblTotalCobrar.TabIndex = 1;
            this.lblTotalCobrar.Text = "Gs: ....";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "1. DATOS DEL CLIENTE:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Cédula / RUC:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 247);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Cliente:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 286);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "RUC Factura:  ";
            // 
            // txtBusquedaCliente
            // 
            this.txtBusquedaCliente.Location = new System.Drawing.Point(170, 196);
            this.txtBusquedaCliente.Name = "txtBusquedaCliente";
            this.txtBusquedaCliente.Size = new System.Drawing.Size(171, 26);
            this.txtBusquedaCliente.TabIndex = 7;
            this.txtBusquedaCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBusquedaCliente_KeyPress);
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.BackColor = System.Drawing.Color.White;
            this.txtNombreCliente.Enabled = false;
            this.txtNombreCliente.Location = new System.Drawing.Point(169, 235);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.ReadOnly = true;
            this.txtNombreCliente.Size = new System.Drawing.Size(171, 26);
            this.txtNombreCliente.TabIndex = 8;
            this.txtNombreCliente.TabStop = false;
            // 
            // txtRucCliente
            // 
            this.txtRucCliente.BackColor = System.Drawing.Color.White;
            this.txtRucCliente.Enabled = false;
            this.txtRucCliente.Location = new System.Drawing.Point(170, 280);
            this.txtRucCliente.Name = "txtRucCliente";
            this.txtRucCliente.ReadOnly = true;
            this.txtRucCliente.Size = new System.Drawing.Size(171, 26);
            this.txtRucCliente.TabIndex = 9;
            this.txtRucCliente.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 368);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(199, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "2. DETALLES DEL PAGO:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 418);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(130, 20);
            this.label9.TabIndex = 11;
            this.label9.Text = "Método de Pago:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 460);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "Comprobante: ";
            // 
            // cmbMetodoPago
            // 
            this.cmbMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMetodoPago.FormattingEnabled = true;
            this.cmbMetodoPago.Items.AddRange(new object[] {
            "Efectivo",
            "Tarjeta",
            "Transferencia"});
            this.cmbMetodoPago.Location = new System.Drawing.Point(170, 415);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(121, 28);
            this.cmbMetodoPago.TabIndex = 13;
            this.cmbMetodoPago.SelectedIndexChanged += new System.EventHandler(this.cmbMetodoPago_SelectedIndexChanged);
            // 
            // cmbTipoComprobante
            // 
            this.cmbTipoComprobante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoComprobante.FormattingEnabled = true;
            this.cmbTipoComprobante.Items.AddRange(new object[] {
            "Ticket",
            "Factura"});
            this.cmbTipoComprobante.Location = new System.Drawing.Point(170, 457);
            this.cmbTipoComprobante.Name = "cmbTipoComprobante";
            this.cmbTipoComprobante.Size = new System.Drawing.Size(121, 28);
            this.cmbTipoComprobante.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(34, 530);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(246, 20);
            this.label11.TabIndex = 15;
            this.label11.Text = "3. CALCULADORA DE VUELTO:";
            // 
            // txtMontoRecibido
            // 
            this.txtMontoRecibido.Location = new System.Drawing.Point(170, 574);
            this.txtMontoRecibido.Name = "txtMontoRecibido";
            this.txtMontoRecibido.Size = new System.Drawing.Size(171, 26);
            this.txtMontoRecibido.TabIndex = 17;
            this.txtMontoRecibido.TextChanged += new System.EventHandler(this.txtMontoRecibido_TextChanged);
            this.txtMontoRecibido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMontoRecibido_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(34, 580);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(124, 20);
            this.label12.TabIndex = 16;
            this.label12.Text = "Monto Recibido:";
            // 
            // txtVuelto
            // 
            this.txtVuelto.BackColor = System.Drawing.Color.White;
            this.txtVuelto.Enabled = false;
            this.txtVuelto.Location = new System.Drawing.Point(170, 617);
            this.txtVuelto.Name = "txtVuelto";
            this.txtVuelto.ReadOnly = true;
            this.txtVuelto.Size = new System.Drawing.Size(171, 26);
            this.txtVuelto.TabIndex = 19;
            this.txtVuelto.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(34, 623);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 20);
            this.label13.TabIndex = 18;
            this.label13.Text = "Vuelto:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(34, 704);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 20);
            this.label14.TabIndex = 20;
            this.label14.Text = "Cajero:";
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Location = new System.Drawing.Point(38, 763);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(219, 36);
            this.btnMinimizar.TabIndex = 21;
            this.btnMinimizar.Text = "+ AGREGAR MÁS COSAS";
            this.btnMinimizar.UseVisualStyleBackColor = true;
            this.btnMinimizar.Click += new System.EventHandler(this.btnAgregarMasCosas_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(216, 830);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(125, 36);
            this.btnCancelar.TabIndex = 22;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(277, 763);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(269, 36);
            this.btnConfirmar.TabIndex = 23;
            this.btnConfirmar.Text = "$$$ CONFIRMAR E IMPRIMIR";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtCajero
            // 
            this.txtCajero.BackColor = System.Drawing.Color.White;
            this.txtCajero.Enabled = false;
            this.txtCajero.Location = new System.Drawing.Point(169, 698);
            this.txtCajero.Name = "txtCajero";
            this.txtCajero.ReadOnly = true;
            this.txtCajero.Size = new System.Drawing.Size(171, 26);
            this.txtCajero.TabIndex = 24;
            this.txtCajero.TabStop = false;
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.Location = new System.Drawing.Point(347, 191);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(113, 36);
            this.btnBuscarCliente.TabIndex = 25;
            this.btnBuscarCliente.Text = "BUSCAR";
            this.btnBuscarCliente.UseVisualStyleBackColor = true;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "Total a cobrar:";
            // 
            // frmCajaCobro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 896);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBuscarCliente);
            this.Controls.Add(this.txtCajero);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnMinimizar);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtVuelto);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtMontoRecibido);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbTipoComprobante);
            this.Controls.Add(this.cmbMetodoPago);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtRucCliente);
            this.Controls.Add(this.txtNombreCliente);
            this.Controls.Add(this.txtBusquedaCliente);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTotalCobrar);
            this.Controls.Add(this.label1);
            this.Name = "frmCajaCobro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCajaCobro";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalCobrar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBusquedaCliente;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.TextBox txtRucCliente;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.ComboBox cmbTipoComprobante;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMontoRecibido;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtVuelto;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.TextBox txtCajero;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Label label2;
    }
}