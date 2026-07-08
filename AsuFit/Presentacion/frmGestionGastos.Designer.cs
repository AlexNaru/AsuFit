namespace AsuFit.Presentacion
{
    partial class frmGestionGastos
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
            this.dgvGastos = new System.Windows.Forms.DataGridView();
            this.colGastoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGastoDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGastoCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGastoMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGastoFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGastoUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.lblTotalGastado = new System.Windows.Forms.Label();
            this.lblGastosEncontrados = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFiltroTipo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.txtHasta = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGastos
            // 
            this.dgvGastos.AllowUserToAddRows = false;
            this.dgvGastos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGastos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGastos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colGastoId,
            this.colGastoDescripcion,
            this.colGastoCategoria,
            this.colGastoMonto,
            this.colGastoFecha,
            this.colGastoUsuario});
            this.dgvGastos.Location = new System.Drawing.Point(11, 100);
            this.dgvGastos.Margin = new System.Windows.Forms.Padding(2);
            this.dgvGastos.Name = "dgvGastos";
            this.dgvGastos.ReadOnly = true;
            this.dgvGastos.RowHeadersVisible = false;
            this.dgvGastos.RowHeadersWidth = 62;
            this.dgvGastos.RowTemplate.Height = 28;
            this.dgvGastos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGastos.Size = new System.Drawing.Size(565, 150);
            this.dgvGastos.TabIndex = 4;
            this.dgvGastos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvGastos_DataBindingComplete);
            // 
            // colGastoId
            // 
            this.colGastoId.DataPropertyName = "IdGasto";
            this.colGastoId.HeaderText = "Id Gasto";
            this.colGastoId.MinimumWidth = 8;
            this.colGastoId.Name = "colGastoId";
            this.colGastoId.ReadOnly = true;
            this.colGastoId.Visible = false;
            // 
            // colGastoDescripcion
            // 
            this.colGastoDescripcion.DataPropertyName = "Descripcion";
            this.colGastoDescripcion.HeaderText = "Descripción";
            this.colGastoDescripcion.MinimumWidth = 8;
            this.colGastoDescripcion.Name = "colGastoDescripcion";
            this.colGastoDescripcion.ReadOnly = true;
            // 
            // colGastoCategoria
            // 
            this.colGastoCategoria.DataPropertyName = "Categoria";
            this.colGastoCategoria.HeaderText = "Categoría";
            this.colGastoCategoria.MinimumWidth = 8;
            this.colGastoCategoria.Name = "colGastoCategoria";
            this.colGastoCategoria.ReadOnly = true;
            // 
            // colGastoMonto
            // 
            this.colGastoMonto.DataPropertyName = "Monto";
            dataGridViewCellStyle3.Format = "N0";
            this.colGastoMonto.DefaultCellStyle = dataGridViewCellStyle3;
            this.colGastoMonto.HeaderText = "Monto";
            this.colGastoMonto.MinimumWidth = 8;
            this.colGastoMonto.Name = "colGastoMonto";
            this.colGastoMonto.ReadOnly = true;
            // 
            // colGastoFecha
            // 
            this.colGastoFecha.DataPropertyName = "FechaGasto";
            dataGridViewCellStyle4.Format = "dd/MM/yyyy HH:mm";
            this.colGastoFecha.DefaultCellStyle = dataGridViewCellStyle4;
            this.colGastoFecha.HeaderText = "Fecha";
            this.colGastoFecha.MinimumWidth = 8;
            this.colGastoFecha.Name = "colGastoFecha";
            this.colGastoFecha.ReadOnly = true;
            // 
            // colGastoUsuario
            // 
            this.colGastoUsuario.DataPropertyName = "UsuarioRegistra";
            this.colGastoUsuario.HeaderText = "Usuario";
            this.colGastoUsuario.MinimumWidth = 8;
            this.colGastoUsuario.Name = "colGastoUsuario";
            this.colGastoUsuario.ReadOnly = true;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescripcion.ForeColor = System.Drawing.Color.White;
            this.txtDescripcion.Location = new System.Drawing.Point(184, 273);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescripcion.MaxLength = 150;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ShortcutsEnabled = false;
            this.txtDescripcion.Size = new System.Drawing.Size(85, 20);
            this.txtDescripcion.TabIndex = 1;
            // 
            // txtMonto
            // 
            this.txtMonto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtMonto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonto.ForeColor = System.Drawing.Color.White;
            this.txtMonto.Location = new System.Drawing.Point(323, 273);
            this.txtMonto.Margin = new System.Windows.Forms.Padding(2);
            this.txtMonto.MaxLength = 20;
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(91, 20);
            this.txtMonto.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(181, 258);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Descripcion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(320, 258);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Monto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(8, 258);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Categorias";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(495, 270);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(80, 24);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "GESTIÓN DE GASTOS";
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategoria.ForeColor = System.Drawing.Color.White;
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Items.AddRange(new object[] {
            "--- Seleccionar ---",
            "Servicios (Luz/Agua)",
            "Mantenimiento/Limpieza",
            "Insumos",
            "Otros"});
            this.cmbCategoria.Location = new System.Drawing.Point(11, 273);
            this.cmbCategoria.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(143, 21);
            this.cmbCategoria.TabIndex = 9;
            // 
            // lblTotalGastado
            // 
            this.lblTotalGastado.AutoSize = true;
            this.lblTotalGastado.ForeColor = System.Drawing.Color.White;
            this.lblTotalGastado.Location = new System.Drawing.Point(379, 324);
            this.lblTotalGastado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalGastado.Name = "lblTotalGastado";
            this.lblTotalGastado.Size = new System.Drawing.Size(100, 13);
            this.lblTotalGastado.TabIndex = 20;
            this.lblTotalGastado.Text = "TOTAL GASTADO:";
            // 
            // lblGastosEncontrados
            // 
            this.lblGastosEncontrados.AutoSize = true;
            this.lblGastosEncontrados.ForeColor = System.Drawing.Color.White;
            this.lblGastosEncontrados.Location = new System.Drawing.Point(11, 324);
            this.lblGastosEncontrados.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGastosEncontrados.Name = "lblGastosEncontrados";
            this.lblGastosEncontrados.Size = new System.Drawing.Size(106, 13);
            this.lblGastosEncontrados.TabIndex = 19;
            this.lblGastosEncontrados.Text = "Gastos Encontradas:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(144, 35);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(15, 20);
            this.dtpDesde.TabIndex = 38;
            this.dtpDesde.ValueChanged += new System.EventHandler(this.dtpDesde_ValueChanged);
            // 
            // txtDesde
            // 
            this.txtDesde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtDesde.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesde.ForeColor = System.Drawing.Color.White;
            this.txtDesde.Location = new System.Drawing.Point(57, 35);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.ReadOnly = true;
            this.txtDesde.ShortcutsEnabled = false;
            this.txtDesde.Size = new System.Drawing.Size(102, 20);
            this.txtDesde.TabIndex = 39;
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Location = new System.Drawing.Point(15, 70);
            this.txtBuscar.MaxLength = 100;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.ShortcutsEnabled = false;
            this.txtBuscar.Size = new System.Drawing.Size(269, 20);
            this.txtBuscar.TabIndex = 37;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(299, 73);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Tipo:";
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
            "Servicios (Luz/Agua)",
            "Mantenimiento/Limpieza",
            "Insumos",
            "Otros"});
            this.cmbFiltroTipo.Location = new System.Drawing.Point(342, 69);
            this.cmbFiltroTipo.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFiltroTipo.Name = "cmbFiltroTipo";
            this.cmbFiltroTipo.Size = new System.Drawing.Size(103, 21);
            this.cmbFiltroTipo.TabIndex = 33;
            this.cmbFiltroTipo.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroTipo_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(11, 37);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Desde:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(429, 34);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(2);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(15, 20);
            this.dtpHasta.TabIndex = 41;
            this.dtpHasta.ValueChanged += new System.EventHandler(this.dtpHasta_ValueChanged);
            // 
            // txtHasta
            // 
            this.txtHasta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtHasta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHasta.ForeColor = System.Drawing.Color.White;
            this.txtHasta.Location = new System.Drawing.Point(342, 34);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.ReadOnly = true;
            this.txtHasta.ShortcutsEnabled = false;
            this.txtHasta.Size = new System.Drawing.Size(102, 20);
            this.txtHasta.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(299, 37);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Hasta:";
            // 
            // frmGestionGastos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(586, 353);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.txtHasta);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.txtDesde);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbFiltroTipo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblTotalGastado);
            this.Controls.Add(this.lblGastosEncontrados);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.dgvGastos);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmGestionGastos";
            this.Text = "frmGestionGastos";
            this.Load += new System.EventHandler(this.frmGestionGastos_Load);
            this.Click += new System.EventHandler(this.frmGestionGastos_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGastos;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGastoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGastoDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGastoCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGastoMonto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGastoFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGastoUsuario;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Label lblTotalGastado;
        private System.Windows.Forms.Label lblGastosEncontrados;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbFiltroTipo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.TextBox txtHasta;
        private System.Windows.Forms.Label label5;
    }
}