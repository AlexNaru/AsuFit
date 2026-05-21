namespace AsuFit.Presentacion
{
    partial class frmHistorialTransacciones
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalRecaudado = new System.Windows.Forms.Label();
            this.lblCantidadVentas = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvVentas = new System.Windows.Forms.DataGridView();
            this.colHistorialId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorialFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorialCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorialMetodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorialTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistorialTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscar = new System.Windows.Forms.MaskedTextBox();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.btnVerDetalle = new System.Windows.Forms.Button();
            this.cmbFiltroTipo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "HISTORIAL DE TRANSACCIONES Y CAJA";
            // 
            // lblTotalRecaudado
            // 
            this.lblTotalRecaudado.AutoSize = true;
            this.lblTotalRecaudado.Location = new System.Drawing.Point(564, 424);
            this.lblTotalRecaudado.Name = "lblTotalRecaudado";
            this.lblTotalRecaudado.Size = new System.Drawing.Size(171, 20);
            this.lblTotalRecaudado.TabIndex = 18;
            this.lblTotalRecaudado.Text = "TOTAL RECAUDADO:";
            // 
            // lblCantidadVentas
            // 
            this.lblCantidadVentas.AutoSize = true;
            this.lblCantidadVentas.Location = new System.Drawing.Point(12, 424);
            this.lblCantidadVentas.Name = "lblCantidadVentas";
            this.lblCantidadVentas.Size = new System.Drawing.Size(211, 20);
            this.lblCantidadVentas.TabIndex = 16;
            this.lblCantidadVentas.Text = "Transacciones Encontradas:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(444, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Hasta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Desde:";
            // 
            // dgvVentas
            // 
            this.dgvVentas.AllowUserToAddRows = false;
            this.dgvVentas.AllowUserToDeleteRows = false;
            this.dgvVentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVentas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHistorialId,
            this.colHistorialFecha,
            this.colHistorialCliente,
            this.colHistorialMetodo,
            this.colHistorialTipo,
            this.colHistorialTotal});
            this.dgvVentas.Location = new System.Drawing.Point(16, 158);
            this.dgvVentas.Name = "dgvVentas";
            this.dgvVentas.ReadOnly = true;
            this.dgvVentas.RowHeadersVisible = false;
            this.dgvVentas.RowHeadersWidth = 62;
            this.dgvVentas.RowTemplate.Height = 28;
            this.dgvVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVentas.Size = new System.Drawing.Size(819, 231);
            this.dgvVentas.TabIndex = 5;
            // 
            // colHistorialId
            // 
            this.colHistorialId.DataPropertyName = "N° Transacción";
            this.colHistorialId.HeaderText = "N° Transacción";
            this.colHistorialId.MinimumWidth = 8;
            this.colHistorialId.Name = "colHistorialId";
            this.colHistorialId.ReadOnly = true;
            // 
            // colHistorialFecha
            // 
            this.colHistorialFecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy HH:mm";
            this.colHistorialFecha.DefaultCellStyle = dataGridViewCellStyle1;
            this.colHistorialFecha.FillWeight = 120F;
            this.colHistorialFecha.HeaderText = "Fecha";
            this.colHistorialFecha.MinimumWidth = 8;
            this.colHistorialFecha.Name = "colHistorialFecha";
            this.colHistorialFecha.ReadOnly = true;
            // 
            // colHistorialCliente
            // 
            this.colHistorialCliente.DataPropertyName = "Cliente";
            this.colHistorialCliente.FillWeight = 110F;
            this.colHistorialCliente.HeaderText = "Cliente";
            this.colHistorialCliente.MinimumWidth = 8;
            this.colHistorialCliente.Name = "colHistorialCliente";
            this.colHistorialCliente.ReadOnly = true;
            // 
            // colHistorialMetodo
            // 
            this.colHistorialMetodo.DataPropertyName = "Método";
            this.colHistorialMetodo.FillWeight = 95F;
            this.colHistorialMetodo.HeaderText = "Método";
            this.colHistorialMetodo.MinimumWidth = 8;
            this.colHistorialMetodo.Name = "colHistorialMetodo";
            this.colHistorialMetodo.ReadOnly = true;
            // 
            // colHistorialTipo
            // 
            this.colHistorialTipo.DataPropertyName = "Tipo Operación";
            this.colHistorialTipo.FillWeight = 95F;
            this.colHistorialTipo.HeaderText = "Tipo Operación";
            this.colHistorialTipo.MinimumWidth = 8;
            this.colHistorialTipo.Name = "colHistorialTipo";
            this.colHistorialTipo.ReadOnly = true;
            // 
            // colHistorialTotal
            // 
            this.colHistorialTotal.DataPropertyName = "Total Cobrado";
            dataGridViewCellStyle2.Format = "N0";
            this.colHistorialTotal.DefaultCellStyle = dataGridViewCellStyle2;
            this.colHistorialTotal.FillWeight = 80F;
            this.colHistorialTotal.HeaderText = "Total Cobrado";
            this.colHistorialTotal.MinimumWidth = 8;
            this.colHistorialTotal.Name = "colHistorialTotal";
            this.colHistorialTotal.ReadOnly = true;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(16, 96);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(401, 26);
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(501, 52);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(153, 26);
            this.dtpHasta.TabIndex = 1;
            this.dtpHasta.ValueChanged += new System.EventHandler(this.dtpHasta_ValueChanged);
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(69, 52);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(155, 26);
            this.dtpDesde.TabIndex = 0;
            this.dtpDesde.ValueChanged += new System.EventHandler(this.dtpDesde_ValueChanged);
            // 
            // btnVerDetalle
            // 
            this.btnVerDetalle.Location = new System.Drawing.Point(302, 498);
            this.btnVerDetalle.Name = "btnVerDetalle";
            this.btnVerDetalle.Size = new System.Drawing.Size(240, 38);
            this.btnVerDetalle.TabIndex = 4;
            this.btnVerDetalle.Text = "VER DETALLE DE VENTA";
            this.btnVerDetalle.UseVisualStyleBackColor = true;
            this.btnVerDetalle.Click += new System.EventHandler(this.btnVerDetalle_Click);
            // 
            // cmbFiltroTipo
            // 
            this.cmbFiltroTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroTipo.FormattingEnabled = true;
            this.cmbFiltroTipo.Items.AddRange(new object[] {
            "Todos",
            "Solo Productos",
            "Solo Mensualidades",
            "Mixtos"});
            this.cmbFiltroTipo.Location = new System.Drawing.Point(501, 96);
            this.cmbFiltroTipo.Name = "cmbFiltroTipo";
            this.cmbFiltroTipo.Size = new System.Drawing.Size(153, 28);
            this.cmbFiltroTipo.TabIndex = 3;
            this.cmbFiltroTipo.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroTipo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(444, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 20);
            this.label6.TabIndex = 22;
            this.label6.Text = "Tipo:";
            // 
            // frmHistorialTransacciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 543);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbFiltroTipo);
            this.Controls.Add(this.btnVerDetalle);
            this.Controls.Add(this.lblTotalRecaudado);
            this.Controls.Add(this.lblCantidadVentas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvVentas);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.label1);
            this.Name = "frmHistorialTransacciones";
            this.Text = "frmHistorialVentas";
            this.Load += new System.EventHandler(this.frmHistorialTransacciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalRecaudado;
        private System.Windows.Forms.Label lblCantidadVentas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvVentas;
        private System.Windows.Forms.MaskedTextBox txtBuscar;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Button btnVerDetalle;
        private System.Windows.Forms.ComboBox cmbFiltroTipo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorialId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorialFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorialCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorialMetodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorialTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorialTotal;
    }
}