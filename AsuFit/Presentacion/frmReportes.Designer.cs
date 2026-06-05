namespace AsuFit.Presentacion
{
    partial class frmReportes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControlReportes = new System.Windows.Forms.TabControl();
            this.tabIngresos = new System.Windows.Forms.TabPage();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.txtHasta = new System.Windows.Forms.TextBox();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.lblTotalIngresos = new System.Windows.Forms.Label();
            this.dgvIngresos = new System.Windows.Forms.DataGridView();
            this.colRepIngresoFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRepIngresoComprobante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRepIngresoMetodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRepIngresoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabTopProductos = new System.Windows.Forms.TabPage();
            this.dtpHastaTop = new System.Windows.Forms.DateTimePicker();
            this.txtHastaTop = new System.Windows.Forms.TextBox();
            this.dtpDesdeTop = new System.Windows.Forms.DateTimePicker();
            this.txtDesdeTop = new System.Windows.Forms.TextBox();
            this.dgvTopProductos = new System.Windows.Forms.DataGridView();
            this.colTopProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTopCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTopIngresos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControlReportes.SuspendLayout();
            this.tabIngresos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngresos)).BeginInit();
            this.tabTopProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlReportes
            // 
            this.tabControlReportes.Controls.Add(this.tabIngresos);
            this.tabControlReportes.Controls.Add(this.tabTopProductos);
            this.tabControlReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlReportes.Location = new System.Drawing.Point(0, 0);
            this.tabControlReportes.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlReportes.Name = "tabControlReportes";
            this.tabControlReportes.SelectedIndex = 0;
            this.tabControlReportes.Size = new System.Drawing.Size(409, 262);
            this.tabControlReportes.TabIndex = 0;
            // 
            // tabIngresos
            // 
            this.tabIngresos.Controls.Add(this.dtpHasta);
            this.tabIngresos.Controls.Add(this.txtHasta);
            this.tabIngresos.Controls.Add(this.dtpDesde);
            this.tabIngresos.Controls.Add(this.txtDesde);
            this.tabIngresos.Controls.Add(this.lblTotalIngresos);
            this.tabIngresos.Controls.Add(this.dgvIngresos);
            this.tabIngresos.Controls.Add(this.label2);
            this.tabIngresos.Controls.Add(this.label1);
            this.tabIngresos.Location = new System.Drawing.Point(4, 22);
            this.tabIngresos.Margin = new System.Windows.Forms.Padding(2);
            this.tabIngresos.Name = "tabIngresos";
            this.tabIngresos.Padding = new System.Windows.Forms.Padding(2);
            this.tabIngresos.Size = new System.Drawing.Size(401, 236);
            this.tabIngresos.TabIndex = 0;
            this.tabIngresos.Text = "Ingresos por Fechas";
            this.tabIngresos.UseVisualStyleBackColor = true;
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(376, 11);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(2);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(15, 20);
            this.dtpHasta.TabIndex = 33;
            this.dtpHasta.ValueChanged += new System.EventHandler(this.dtpHasta_ValueChanged);
            // 
            // txtHasta
            // 
            this.txtHasta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtHasta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHasta.ForeColor = System.Drawing.Color.White;
            this.txtHasta.Location = new System.Drawing.Point(289, 11);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.ReadOnly = true;
            this.txtHasta.Size = new System.Drawing.Size(102, 20);
            this.txtHasta.TabIndex = 34;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(138, 11);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(15, 20);
            this.dtpDesde.TabIndex = 31;
            this.dtpDesde.ValueChanged += new System.EventHandler(this.dtpDesde_ValueChanged);
            // 
            // txtDesde
            // 
            this.txtDesde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtDesde.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesde.ForeColor = System.Drawing.Color.White;
            this.txtDesde.Location = new System.Drawing.Point(51, 11);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.ReadOnly = true;
            this.txtDesde.Size = new System.Drawing.Size(102, 20);
            this.txtDesde.TabIndex = 32;
            // 
            // lblTotalIngresos
            // 
            this.lblTotalIngresos.AutoSize = true;
            this.lblTotalIngresos.Location = new System.Drawing.Point(5, 209);
            this.lblTotalIngresos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalIngresos.Name = "lblTotalIngresos";
            this.lblTotalIngresos.Size = new System.Drawing.Size(144, 13);
            this.lblTotalIngresos.TabIndex = 12;
            this.lblTotalIngresos.Text = "TOTAL RECAUDADO: Gs. 0";
            // 
            // dgvIngresos
            // 
            this.dgvIngresos.AllowUserToAddRows = false;
            this.dgvIngresos.AllowUserToDeleteRows = false;
            this.dgvIngresos.AllowUserToResizeColumns = false;
            this.dgvIngresos.AllowUserToResizeRows = false;
            this.dgvIngresos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIngresos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIngresos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRepIngresoFecha,
            this.colRepIngresoComprobante,
            this.colRepIngresoMetodo,
            this.colRepIngresoTotal});
            this.dgvIngresos.Location = new System.Drawing.Point(8, 45);
            this.dgvIngresos.Margin = new System.Windows.Forms.Padding(2);
            this.dgvIngresos.Name = "dgvIngresos";
            this.dgvIngresos.ReadOnly = true;
            this.dgvIngresos.RowHeadersVisible = false;
            this.dgvIngresos.RowHeadersWidth = 62;
            this.dgvIngresos.RowTemplate.Height = 28;
            this.dgvIngresos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIngresos.Size = new System.Drawing.Size(383, 150);
            this.dgvIngresos.TabIndex = 2;
            // 
            // colRepIngresoFecha
            // 
            this.colRepIngresoFecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy HH:mm";
            this.colRepIngresoFecha.DefaultCellStyle = dataGridViewCellStyle1;
            this.colRepIngresoFecha.HeaderText = "Fecha";
            this.colRepIngresoFecha.MinimumWidth = 8;
            this.colRepIngresoFecha.Name = "colRepIngresoFecha";
            this.colRepIngresoFecha.ReadOnly = true;
            // 
            // colRepIngresoComprobante
            // 
            this.colRepIngresoComprobante.DataPropertyName = "Comprobante";
            this.colRepIngresoComprobante.HeaderText = "Comprobante";
            this.colRepIngresoComprobante.MinimumWidth = 8;
            this.colRepIngresoComprobante.Name = "colRepIngresoComprobante";
            this.colRepIngresoComprobante.ReadOnly = true;
            // 
            // colRepIngresoMetodo
            // 
            this.colRepIngresoMetodo.DataPropertyName = "MetodoPago";
            this.colRepIngresoMetodo.HeaderText = "Método de Pago";
            this.colRepIngresoMetodo.MinimumWidth = 8;
            this.colRepIngresoMetodo.Name = "colRepIngresoMetodo";
            this.colRepIngresoMetodo.ReadOnly = true;
            // 
            // colRepIngresoTotal
            // 
            this.colRepIngresoTotal.DataPropertyName = "Total";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            this.colRepIngresoTotal.DefaultCellStyle = dataGridViewCellStyle2;
            this.colRepIngresoTotal.HeaderText = "Total";
            this.colRepIngresoTotal.MinimumWidth = 8;
            this.colRepIngresoTotal.Name = "colRepIngresoTotal";
            this.colRepIngresoTotal.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Hasta:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Desde:";
            // 
            // tabTopProductos
            // 
            this.tabTopProductos.Controls.Add(this.dtpHastaTop);
            this.tabTopProductos.Controls.Add(this.txtHastaTop);
            this.tabTopProductos.Controls.Add(this.dtpDesdeTop);
            this.tabTopProductos.Controls.Add(this.txtDesdeTop);
            this.tabTopProductos.Controls.Add(this.dgvTopProductos);
            this.tabTopProductos.Controls.Add(this.label3);
            this.tabTopProductos.Controls.Add(this.label4);
            this.tabTopProductos.Location = new System.Drawing.Point(4, 22);
            this.tabTopProductos.Margin = new System.Windows.Forms.Padding(2);
            this.tabTopProductos.Name = "tabTopProductos";
            this.tabTopProductos.Padding = new System.Windows.Forms.Padding(2);
            this.tabTopProductos.Size = new System.Drawing.Size(401, 236);
            this.tabTopProductos.TabIndex = 1;
            this.tabTopProductos.Text = "Top 5 Productos Más Vendidos";
            this.tabTopProductos.UseVisualStyleBackColor = true;
            // 
            // dtpHastaTop
            // 
            this.dtpHastaTop.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHastaTop.Location = new System.Drawing.Point(376, 10);
            this.dtpHastaTop.Margin = new System.Windows.Forms.Padding(2);
            this.dtpHastaTop.Name = "dtpHastaTop";
            this.dtpHastaTop.Size = new System.Drawing.Size(15, 20);
            this.dtpHastaTop.TabIndex = 35;
            this.dtpHastaTop.ValueChanged += new System.EventHandler(this.dtpHastaTop_ValueChanged);
            // 
            // txtHastaTop
            // 
            this.txtHastaTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtHastaTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHastaTop.ForeColor = System.Drawing.Color.White;
            this.txtHastaTop.Location = new System.Drawing.Point(289, 10);
            this.txtHastaTop.Name = "txtHastaTop";
            this.txtHastaTop.ReadOnly = true;
            this.txtHastaTop.Size = new System.Drawing.Size(102, 20);
            this.txtHastaTop.TabIndex = 36;
            // 
            // dtpDesdeTop
            // 
            this.dtpDesdeTop.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesdeTop.Location = new System.Drawing.Point(138, 10);
            this.dtpDesdeTop.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDesdeTop.Name = "dtpDesdeTop";
            this.dtpDesdeTop.Size = new System.Drawing.Size(15, 20);
            this.dtpDesdeTop.TabIndex = 33;
            this.dtpDesdeTop.ValueChanged += new System.EventHandler(this.dtpDesdeTop_ValueChanged);
            // 
            // txtDesdeTop
            // 
            this.txtDesdeTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtDesdeTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesdeTop.ForeColor = System.Drawing.Color.White;
            this.txtDesdeTop.Location = new System.Drawing.Point(51, 10);
            this.txtDesdeTop.Name = "txtDesdeTop";
            this.txtDesdeTop.ReadOnly = true;
            this.txtDesdeTop.Size = new System.Drawing.Size(102, 20);
            this.txtDesdeTop.TabIndex = 34;
            // 
            // dgvTopProductos
            // 
            this.dgvTopProductos.AllowUserToAddRows = false;
            this.dgvTopProductos.AllowUserToDeleteRows = false;
            this.dgvTopProductos.AllowUserToResizeColumns = false;
            this.dgvTopProductos.AllowUserToResizeRows = false;
            this.dgvTopProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTopProducto,
            this.colTopCantidad,
            this.colTopIngresos});
            this.dgvTopProductos.Location = new System.Drawing.Point(8, 44);
            this.dgvTopProductos.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTopProductos.Name = "dgvTopProductos";
            this.dgvTopProductos.ReadOnly = true;
            this.dgvTopProductos.RowHeadersVisible = false;
            this.dgvTopProductos.RowHeadersWidth = 62;
            this.dgvTopProductos.RowTemplate.Height = 28;
            this.dgvTopProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopProductos.Size = new System.Drawing.Size(383, 150);
            this.dgvTopProductos.TabIndex = 15;
            // 
            // colTopProducto
            // 
            this.colTopProducto.DataPropertyName = "Producto";
            this.colTopProducto.FillWeight = 130F;
            this.colTopProducto.HeaderText = "Producto";
            this.colTopProducto.MinimumWidth = 8;
            this.colTopProducto.Name = "colTopProducto";
            this.colTopProducto.ReadOnly = true;
            // 
            // colTopCantidad
            // 
            this.colTopCantidad.DataPropertyName = "CantidadVendida";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colTopCantidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.colTopCantidad.HeaderText = "Cantidad Vendida";
            this.colTopCantidad.MinimumWidth = 8;
            this.colTopCantidad.Name = "colTopCantidad";
            this.colTopCantidad.ReadOnly = true;
            // 
            // colTopIngresos
            // 
            this.colTopIngresos.DataPropertyName = "Ingresos";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            this.colTopIngresos.DefaultCellStyle = dataGridViewCellStyle4;
            this.colTopIngresos.FillWeight = 60F;
            this.colTopIngresos.HeaderText = "Ingresos";
            this.colTopIngresos.MinimumWidth = 8;
            this.colTopIngresos.Name = "colTopIngresos";
            this.colTopIngresos.ReadOnly = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(246, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Hasta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Desde:";
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 262);
            this.Controls.Add(this.tabControlReportes);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmReportes";
            this.Text = "frmReportes";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            this.tabControlReportes.ResumeLayout(false);
            this.tabIngresos.ResumeLayout(false);
            this.tabIngresos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngresos)).EndInit();
            this.tabTopProductos.ResumeLayout(false);
            this.tabTopProductos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProductos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlReportes;
        private System.Windows.Forms.TabPage tabIngresos;
        private System.Windows.Forms.TabPage tabTopProductos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvIngresos;
        private System.Windows.Forms.Label lblTotalIngresos;
        private System.Windows.Forms.DataGridView dgvTopProductos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRepIngresoFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRepIngresoComprobante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRepIngresoMetodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRepIngresoTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTopProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTopCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTopIngresos;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.TextBox txtHasta;
        private System.Windows.Forms.DateTimePicker dtpDesdeTop;
        private System.Windows.Forms.TextBox txtDesdeTop;
        private System.Windows.Forms.DateTimePicker dtpHastaTop;
        private System.Windows.Forms.TextBox txtHastaTop;
    }
}