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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.btnVerDetalle = new System.Windows.Forms.Button();
            this.cmbFiltroTipo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.txtHasta = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "HISTORIAL DE TRANSACCIONES Y CAJA";
            // 
            // lblTotalRecaudado
            // 
            this.lblTotalRecaudado.AutoSize = true;
            this.lblTotalRecaudado.ForeColor = System.Drawing.Color.White;
            this.lblTotalRecaudado.Location = new System.Drawing.Point(376, 276);
            this.lblTotalRecaudado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalRecaudado.Name = "lblTotalRecaudado";
            this.lblTotalRecaudado.Size = new System.Drawing.Size(116, 13);
            this.lblTotalRecaudado.TabIndex = 18;
            this.lblTotalRecaudado.Text = "TOTAL RECAUDADO:";
            // 
            // lblCantidadVentas
            // 
            this.lblCantidadVentas.AutoSize = true;
            this.lblCantidadVentas.ForeColor = System.Drawing.Color.White;
            this.lblCantidadVentas.Location = new System.Drawing.Point(8, 276);
            this.lblCantidadVentas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCantidadVentas.Name = "lblCantidadVentas";
            this.lblCantidadVentas.Size = new System.Drawing.Size(143, 13);
            this.lblCantidadVentas.TabIndex = 16;
            this.lblCantidadVentas.Text = "Transacciones Encontradas:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(296, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Hasta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
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
            this.dgvVentas.Location = new System.Drawing.Point(11, 103);
            this.dgvVentas.Margin = new System.Windows.Forms.Padding(2);
            this.dgvVentas.Name = "dgvVentas";
            this.dgvVentas.ReadOnly = true;
            this.dgvVentas.RowHeadersVisible = false;
            this.dgvVentas.RowHeadersWidth = 62;
            this.dgvVentas.RowTemplate.Height = 28;
            this.dgvVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVentas.Size = new System.Drawing.Size(546, 150);
            this.dgvVentas.TabIndex = 5;
            this.dgvVentas.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvVentas_DataBindingComplete);
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
            dataGridViewCellStyle3.Format = "dd/MM/yyyy HH:mm";
            this.colHistorialFecha.DefaultCellStyle = dataGridViewCellStyle3;
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
            dataGridViewCellStyle4.Format = "N0";
            this.colHistorialTotal.DefaultCellStyle = dataGridViewCellStyle4;
            this.colHistorialTotal.FillWeight = 80F;
            this.colHistorialTotal.HeaderText = "Total Cobrado";
            this.colHistorialTotal.MinimumWidth = 8;
            this.colHistorialTotal.Name = "colHistorialTotal";
            this.colHistorialTotal.ReadOnly = true;
            // 
            // btnVerDetalle
            // 
            this.btnVerDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.btnVerDetalle.FlatAppearance.BorderSize = 0;
            this.btnVerDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerDetalle.ForeColor = System.Drawing.Color.Black;
            this.btnVerDetalle.Location = new System.Drawing.Point(190, 317);
            this.btnVerDetalle.Margin = new System.Windows.Forms.Padding(2);
            this.btnVerDetalle.Name = "btnVerDetalle";
            this.btnVerDetalle.Size = new System.Drawing.Size(187, 25);
            this.btnVerDetalle.TabIndex = 4;
            this.btnVerDetalle.Text = "VER DETALLE DE VENTA";
            this.btnVerDetalle.UseVisualStyleBackColor = false;
            this.btnVerDetalle.Click += new System.EventHandler(this.btnVerDetalle_Click);
            // 
            // cmbFiltroTipo
            // 
            this.cmbFiltroTipo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.cmbFiltroTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroTipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFiltroTipo.ForeColor = System.Drawing.Color.White;
            this.cmbFiltroTipo.FormattingEnabled = true;
            this.cmbFiltroTipo.Items.AddRange(new object[] {
            "Todos",
            "Solo Productos",
            "Solo Mensualidades",
            "Mixtos"});
            this.cmbFiltroTipo.Location = new System.Drawing.Point(339, 69);
            this.cmbFiltroTipo.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFiltroTipo.Name = "cmbFiltroTipo";
            this.cmbFiltroTipo.Size = new System.Drawing.Size(103, 21);
            this.cmbFiltroTipo.TabIndex = 3;
            this.cmbFiltroTipo.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroTipo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(296, 73);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Tipo:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Location = new System.Drawing.Point(12, 70);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(269, 20);
            this.txtBuscar.TabIndex = 24;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(141, 35);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(15, 20);
            this.dtpDesde.TabIndex = 29;
            // 
            // txtDesde
            // 
            this.txtDesde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtDesde.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesde.ForeColor = System.Drawing.Color.White;
            this.txtDesde.Location = new System.Drawing.Point(54, 35);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.ReadOnly = true;
            this.txtDesde.Size = new System.Drawing.Size(102, 20);
            this.txtDesde.TabIndex = 30;
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(426, 35);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(2);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(15, 20);
            this.dtpHasta.TabIndex = 31;
            // 
            // txtHasta
            // 
            this.txtHasta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtHasta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHasta.ForeColor = System.Drawing.Color.White;
            this.txtHasta.Location = new System.Drawing.Point(339, 35);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.ReadOnly = true;
            this.txtHasta.Size = new System.Drawing.Size(102, 20);
            this.txtHasta.TabIndex = 32;
            // 
            // frmHistorialTransacciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(28)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(565, 353);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.txtHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.txtDesde);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbFiltroTipo);
            this.Controls.Add(this.btnVerDetalle);
            this.Controls.Add(this.lblTotalRecaudado);
            this.Controls.Add(this.lblCantidadVentas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvVentas);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.Button btnVerDetalle;
        private System.Windows.Forms.ComboBox cmbFiltroTipo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorialId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorialFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorialCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorialMetodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorialTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistorialTotal;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.TextBox txtHasta;
    }
}