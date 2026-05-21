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
            this.dgvSocios = new System.Windows.Forms.DataGridView();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.cmbPlanes = new System.Windows.Forms.ComboBox();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCobrar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.colCobroId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroCedula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCobroIdPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.dgvSocios.Location = new System.Drawing.Point(12, 93);
            this.dgvSocios.Name = "dgvSocios";
            this.dgvSocios.ReadOnly = true;
            this.dgvSocios.RowHeadersVisible = false;
            this.dgvSocios.RowHeadersWidth = 62;
            this.dgvSocios.RowTemplate.Height = 28;
            this.dgvSocios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSocios.Size = new System.Drawing.Size(1273, 211);
            this.dgvSocios.TabIndex = 3;
            this.dgvSocios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSocios_CellClick);
            this.dgvSocios.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSocios_CellFormatting);
            this.dgvSocios.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvSocios_DataBindingComplete);
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.Color.White;
            this.txtBuscar.ForeColor = System.Drawing.Color.Black;
            this.txtBuscar.Location = new System.Drawing.Point(417, 61);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(457, 26);
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // cmbPlanes
            // 
            this.cmbPlanes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlanes.FormattingEnabled = true;
            this.cmbPlanes.Location = new System.Drawing.Point(816, 323);
            this.cmbPlanes.Name = "cmbPlanes";
            this.cmbPlanes.Size = new System.Drawing.Size(121, 28);
            this.cmbPlanes.TabIndex = 4;
            this.cmbPlanes.SelectedIndexChanged += new System.EventHandler(this.cmbPlanes_SelectedIndexChanged);
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(1043, 326);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.ReadOnly = true;
            this.txtMonto.Size = new System.Drawing.Size(100, 26);
            this.txtMonto.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(983, 331);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Monto";
            // 
            // btnCobrar
            // 
            this.btnCobrar.Location = new System.Drawing.Point(1188, 324);
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.Size = new System.Drawing.Size(99, 30);
            this.btnCobrar.TabIndex = 8;
            this.btnCobrar.Text = "COBRAR";
            this.btnCobrar.UseVisualStyleBackColor = true;
            this.btnCobrar.Click += new System.EventHandler(this.btnCobrar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(753, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Planes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "REGISTRO DE COBROS";
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
            // frmRegistrarCobro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1299, 363);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCobrar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.cmbPlanes);
            this.Controls.Add(this.dgvSocios);
            this.Controls.Add(this.txtBuscar);
            this.Name = "frmRegistrarCobro";
            this.Text = "frmRegistrarCobro";
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