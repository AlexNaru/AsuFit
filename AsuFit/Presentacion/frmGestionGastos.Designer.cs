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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.dgvGastos.Location = new System.Drawing.Point(11, 29);
            this.dgvGastos.Margin = new System.Windows.Forms.Padding(2);
            this.dgvGastos.Name = "dgvGastos";
            this.dgvGastos.ReadOnly = true;
            this.dgvGastos.RowHeadersVisible = false;
            this.dgvGastos.RowHeadersWidth = 62;
            this.dgvGastos.RowTemplate.Height = 28;
            this.dgvGastos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGastos.Size = new System.Drawing.Size(565, 98);
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
            dataGridViewCellStyle1.Format = "N0";
            this.colGastoMonto.DefaultCellStyle = dataGridViewCellStyle1;
            this.colGastoMonto.HeaderText = "Monto";
            this.colGastoMonto.MinimumWidth = 8;
            this.colGastoMonto.Name = "colGastoMonto";
            this.colGastoMonto.ReadOnly = true;
            // 
            // colGastoFecha
            // 
            this.colGastoFecha.DataPropertyName = "FechaGasto";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm";
            this.colGastoFecha.DefaultCellStyle = dataGridViewCellStyle2;
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
            this.txtDescripcion.Location = new System.Drawing.Point(184, 157);
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
            this.txtMonto.Location = new System.Drawing.Point(323, 157);
            this.txtMonto.Margin = new System.Windows.Forms.Padding(2);
            this.txtMonto.MaxLength = 10;
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.ShortcutsEnabled = false;
            this.txtMonto.Size = new System.Drawing.Size(91, 20);
            this.txtMonto.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(181, 142);
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
            this.label2.Location = new System.Drawing.Point(320, 142);
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
            this.label3.Location = new System.Drawing.Point(8, 142);
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
            this.btnGuardar.Location = new System.Drawing.Point(495, 154);
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
            this.cmbCategoria.Location = new System.Drawing.Point(11, 157);
            this.cmbCategoria.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(143, 21);
            this.cmbCategoria.TabIndex = 9;
            // 
            // frmGestionGastos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(586, 185);
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
    }
}