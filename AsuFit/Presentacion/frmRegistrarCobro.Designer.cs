namespace AsuFit.Presentacion
{
    partial class frmRegistrarCobro
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
            this.dgvSocios = new System.Windows.Forms.DataGridView();
            this.colCobroId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroCedula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroIdPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.cmbPlanes = new System.Windows.Forms.ComboBox();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCobrar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSocios)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSocios
            // 
            this.dgvSocios.AllowUserToAddRows = false;
            this.dgvSocios.AllowUserToResizeColumns = false;
            this.dgvSocios.AllowUserToResizeRows = false;
            this.dgvSocios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSocios.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSocios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSocios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSocios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCobroId,
            this.colCobroCedula,
            this.colCobroNombre,
            this.colCobroApellido,
            this.colCobroPlan,
            this.colCobroPrecio,
            this.colCobroVencimiento,
            this.colCobroEstado,
            this.colCobroIdPlan});
            this.dgvSocios.Location = new System.Drawing.Point(8, 67);
            this.dgvSocios.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSocios.Name = "dgvSocios";
            this.dgvSocios.ReadOnly = true;
            this.dgvSocios.RowHeadersVisible = false;
            this.dgvSocios.RowHeadersWidth = 62;
            this.dgvSocios.RowTemplate.Height = 28;
            this.dgvSocios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSocios.Size = new System.Drawing.Size(849, 137);
            this.dgvSocios.TabIndex = 1;
            this.dgvSocios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSocios_CellClick);
            this.dgvSocios.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSocios_CellFormatting);
            this.dgvSocios.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvSocios_DataBindingComplete);
            // 
            // colCobroId
            // 
            this.colCobroId.DataPropertyName = "IdSocio";
            this.colCobroId.HeaderText = "ID";
            this.colCobroId.MinimumWidth = 8;
            this.colCobroId.Name = "colCobroId";
            this.colCobroId.ReadOnly = true;
            this.colCobroId.Visible = false;
            // 
            // colCobroCedula
            // 
            this.colCobroCedula.DataPropertyName = "Cedula";
            this.colCobroCedula.HeaderText = "Cédula";
            this.colCobroCedula.MinimumWidth = 8;
            this.colCobroCedula.Name = "colCobroCedula";
            this.colCobroCedula.ReadOnly = true;
            // 
            // colCobroNombre
            // 
            this.colCobroNombre.DataPropertyName = "Nombre";
            this.colCobroNombre.HeaderText = "Nombre";
            this.colCobroNombre.MinimumWidth = 8;
            this.colCobroNombre.Name = "colCobroNombre";
            this.colCobroNombre.ReadOnly = true;
            // 
            // colCobroApellido
            // 
            this.colCobroApellido.DataPropertyName = "Apellido";
            this.colCobroApellido.HeaderText = "Apellido";
            this.colCobroApellido.MinimumWidth = 8;
            this.colCobroApellido.Name = "colCobroApellido";
            this.colCobroApellido.ReadOnly = true;
            // 
            // colCobroPlan
            // 
            this.colCobroPlan.DataPropertyName = "TipoPlan";
            this.colCobroPlan.HeaderText = "Plan Actual";
            this.colCobroPlan.MinimumWidth = 8;
            this.colCobroPlan.Name = "colCobroPlan";
            this.colCobroPlan.ReadOnly = true;
            // 
            // colCobroPrecio
            // 
            this.colCobroPrecio.DataPropertyName = "Precio";
            this.colCobroPrecio.HeaderText = "Precio";
            this.colCobroPrecio.MinimumWidth = 8;
            this.colCobroPrecio.Name = "colCobroPrecio";
            this.colCobroPrecio.ReadOnly = true;
            // 
            // colCobroVencimiento
            // 
            this.colCobroVencimiento.DataPropertyName = "FechaVencimiento";
            this.colCobroVencimiento.HeaderText = "Vencimiento";
            this.colCobroVencimiento.MinimumWidth = 8;
            this.colCobroVencimiento.Name = "colCobroVencimiento";
            this.colCobroVencimiento.ReadOnly = true;
            // 
            // colCobroEstado
            // 
            this.colCobroEstado.DataPropertyName = "Estado";
            this.colCobroEstado.HeaderText = "Estado";
            this.colCobroEstado.MinimumWidth = 8;
            this.colCobroEstado.Name = "colCobroEstado";
            this.colCobroEstado.ReadOnly = true;
            // 
            // colCobroIdPlan
            // 
            this.colCobroIdPlan.DataPropertyName = "IdPlan";
            this.colCobroIdPlan.HeaderText = "ID Plan";
            this.colCobroIdPlan.MinimumWidth = 8;
            this.colCobroIdPlan.Name = "colCobroIdPlan";
            this.colCobroIdPlan.ReadOnly = true;
            this.colCobroIdPlan.Visible = false;
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.ForeColor = System.Drawing.Color.White;
            this.txtBuscar.Location = new System.Drawing.Point(278, 40);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(306, 20);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // cmbPlanes
            // 
            this.cmbPlanes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.cmbPlanes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlanes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPlanes.ForeColor = System.Drawing.Color.White;
            this.cmbPlanes.FormattingEnabled = true;
            this.cmbPlanes.Items.AddRange(new object[] {
            "--- Seleccionar Plan ---",
            "Pase Diario",
            "Pase Semanal",
            "Plan Mensual",
            "Plan Anual"});
            this.cmbPlanes.Location = new System.Drawing.Point(503, 215);
            this.cmbPlanes.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPlanes.Name = "cmbPlanes";
            this.cmbPlanes.Size = new System.Drawing.Size(122, 21);
            this.cmbPlanes.TabIndex = 2;
            this.cmbPlanes.SelectedIndexChanged += new System.EventHandler(this.cmbPlanes_SelectedIndexChanged);
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(688, 217);
            this.txtMonto.Margin = new System.Windows.Forms.Padding(2);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.ReadOnly = true;
            this.txtMonto.Size = new System.Drawing.Size(68, 20);
            this.txtMonto.TabIndex = 3;
            this.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(648, 220);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Monto";
            // 
            // btnCobrar
            // 
            this.btnCobrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.btnCobrar.FlatAppearance.BorderSize = 0;
            this.btnCobrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCobrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCobrar.Location = new System.Drawing.Point(781, 214);
            this.btnCobrar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.Size = new System.Drawing.Size(76, 22);
            this.btnCobrar.TabIndex = 4;
            this.btnCobrar.Text = "COBRAR";
            this.btnCobrar.UseVisualStyleBackColor = false;
            this.btnCobrar.Click += new System.EventHandler(this.btnCobrar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(461, 218);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Planes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(5, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "REGISTRO DE COBROS";
            // 
            // frmRegistrarCobro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(28)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(866, 244);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCobrar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.cmbPlanes);
            this.Controls.Add(this.dgvSocios);
            this.Controls.Add(this.txtBuscar);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmRegistrarCobro";
            this.Text = "frmRegistrarCobro";
            this.Load += new System.EventHandler(this.frmRegistrarCobro_Load);
            this.Click += new System.EventHandler(this.frmRegistrarCobro_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSocios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSocios;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.ComboBox cmbPlanes;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCobrar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCobroId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCobroCedula;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCobroNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCobroApellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCobroPlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCobroPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCobroVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCobroEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCobroIdPlan;
    }
}