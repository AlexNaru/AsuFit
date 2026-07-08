namespace AsuFit.Presentacion
{
    partial class frmIngresoMercaderiaHistorial
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
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.txtHasta = new System.Windows.Forms.TextBox();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblTotalGastado = new System.Windows.Forms.Label();
            this.lblCantidadIngresos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvIngresoMercaderia = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.colNroOperacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCostoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngresoMercaderia)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(429, 38);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(2);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(15, 20);
            this.dtpHasta.TabIndex = 44;
            this.dtpHasta.ValueChanged += new System.EventHandler(this.dtpHasta_ValueChanged);
            // 
            // txtHasta
            // 
            this.txtHasta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtHasta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHasta.ForeColor = System.Drawing.Color.White;
            this.txtHasta.Location = new System.Drawing.Point(342, 38);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.ReadOnly = true;
            this.txtHasta.ShortcutsEnabled = false;
            this.txtHasta.Size = new System.Drawing.Size(102, 20);
            this.txtHasta.TabIndex = 45;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(144, 38);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(15, 20);
            this.dtpDesde.TabIndex = 42;
            this.dtpDesde.ValueChanged += new System.EventHandler(this.dtpDesde_ValueChanged);
            // 
            // txtDesde
            // 
            this.txtDesde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtDesde.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesde.ForeColor = System.Drawing.Color.White;
            this.txtDesde.Location = new System.Drawing.Point(57, 38);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.ReadOnly = true;
            this.txtDesde.ShortcutsEnabled = false;
            this.txtDesde.Size = new System.Drawing.Size(102, 20);
            this.txtDesde.TabIndex = 43;
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Location = new System.Drawing.Point(15, 73);
            this.txtBuscar.MaxLength = 100;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.ShortcutsEnabled = false;
            this.txtBuscar.Size = new System.Drawing.Size(269, 20);
            this.txtBuscar.TabIndex = 41;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // lblTotalGastado
            // 
            this.lblTotalGastado.AutoSize = true;
            this.lblTotalGastado.ForeColor = System.Drawing.Color.White;
            this.lblTotalGastado.Location = new System.Drawing.Point(379, 279);
            this.lblTotalGastado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalGastado.Name = "lblTotalGastado";
            this.lblTotalGastado.Size = new System.Drawing.Size(100, 13);
            this.lblTotalGastado.TabIndex = 39;
            this.lblTotalGastado.Text = "TOTAL GASTADO:";
            // 
            // lblCantidadIngresos
            // 
            this.lblCantidadIngresos.AutoSize = true;
            this.lblCantidadIngresos.ForeColor = System.Drawing.Color.White;
            this.lblCantidadIngresos.Location = new System.Drawing.Point(11, 279);
            this.lblCantidadIngresos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCantidadIngresos.Name = "lblCantidadIngresos";
            this.lblCantidadIngresos.Size = new System.Drawing.Size(113, 13);
            this.lblCantidadIngresos.TabIndex = 38;
            this.lblCantidadIngresos.Text = "Ingresos Encontrados:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(299, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Hasta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(11, 40);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Desde:";
            // 
            // dgvIngresoMercaderia
            // 
            this.dgvIngresoMercaderia.AllowUserToAddRows = false;
            this.dgvIngresoMercaderia.AllowUserToDeleteRows = false;
            this.dgvIngresoMercaderia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIngresoMercaderia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIngresoMercaderia.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNroOperacion,
            this.colFecha,
            this.colProveedor,
            this.colCostoTotal});
            this.dgvIngresoMercaderia.Location = new System.Drawing.Point(14, 106);
            this.dgvIngresoMercaderia.Margin = new System.Windows.Forms.Padding(2);
            this.dgvIngresoMercaderia.Name = "dgvIngresoMercaderia";
            this.dgvIngresoMercaderia.ReadOnly = true;
            this.dgvIngresoMercaderia.RowHeadersVisible = false;
            this.dgvIngresoMercaderia.RowHeadersWidth = 62;
            this.dgvIngresoMercaderia.RowTemplate.Height = 28;
            this.dgvIngresoMercaderia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIngresoMercaderia.Size = new System.Drawing.Size(546, 150);
            this.dgvIngresoMercaderia.TabIndex = 35;
            this.dgvIngresoMercaderia.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvIngresoMercaderia_DataBindingComplete);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "HISTORIAL DE INGRESOS DE MERCADERIA";
            // 
            // colNroOperacion
            // 
            this.colNroOperacion.DataPropertyName = "N° Operación";
            this.colNroOperacion.HeaderText = "N° Operación";
            this.colNroOperacion.Name = "colNroOperacion";
            this.colNroOperacion.ReadOnly = true;
            // 
            // colFecha
            // 
            this.colFecha.DataPropertyName = "Fecha";
            this.colFecha.HeaderText = "Fecha";
            this.colFecha.Name = "colFecha";
            this.colFecha.ReadOnly = true;
            // 
            // colProveedor
            // 
            this.colProveedor.DataPropertyName = "Proveedor";
            this.colProveedor.HeaderText = "Proveedor";
            this.colProveedor.Name = "colProveedor";
            this.colProveedor.ReadOnly = true;
            // 
            // colCostoTotal
            // 
            this.colCostoTotal.DataPropertyName = "CostoTotal";
            this.colCostoTotal.HeaderText = "Costo Total";
            this.colCostoTotal.Name = "colCostoTotal";
            this.colCostoTotal.ReadOnly = true;
            // 
            // frmIngresoMercaderiaHistorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(28)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(565, 308);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.txtHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.txtDesde);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblTotalGastado);
            this.Controls.Add(this.lblCantidadIngresos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvIngresoMercaderia);
            this.Controls.Add(this.label1);
            this.Name = "frmIngresoMercaderiaHistorial";
            this.Text = "frmIngresoMercaderiaHistorial";
            this.Load += new System.EventHandler(this.frmIngresoMercaderiaHistorial_Load);
            this.Shown += new System.EventHandler(this.frmIngresoMercaderiaHistorial_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BloquearPegado_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAntiInyeccion_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngresoMercaderia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.TextBox txtHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblTotalGastado;
        private System.Windows.Forms.Label lblCantidadIngresos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvIngresoMercaderia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNroOperacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCostoTotal;
    }
}