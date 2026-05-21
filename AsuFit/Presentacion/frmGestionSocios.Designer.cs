namespace AsuFit.Presentacion
{
    partial class frmGestionSocios
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
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvSocios = new System.Windows.Forms.DataGridView();
            this.colSocioId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioCedula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioRuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioTelefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioFechaNacim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioFechaReg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioContEmerg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocioTelEmerg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.btnEstado = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSocios)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.Color.White;
            this.txtBuscar.ForeColor = System.Drawing.Color.Black;
            this.txtBuscar.Location = new System.Drawing.Point(390, 63);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(457, 26);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
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
            this.colSocioId,
            this.colSocioCedula,
            this.colSocioNombre,
            this.colSocioApellido,
            this.colSocioEmail,
            this.colSocioRuc,
            this.colSocioTelefono,
            this.colSocioFechaNacim,
            this.colSocioFechaReg,
            this.colSocioPlan,
            this.colSocioPrecio,
            this.colSocioVencimiento,
            this.colSocioEstado,
            this.colSocioContEmerg,
            this.colSocioTelEmerg});
            this.dgvSocios.Location = new System.Drawing.Point(12, 105);
            this.dgvSocios.Name = "dgvSocios";
            this.dgvSocios.ReadOnly = true;
            this.dgvSocios.RowHeadersVisible = false;
            this.dgvSocios.RowHeadersWidth = 62;
            this.dgvSocios.RowTemplate.Height = 28;
            this.dgvSocios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSocios.Size = new System.Drawing.Size(1273, 230);
            this.dgvSocios.TabIndex = 2;
            this.dgvSocios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSocios_CellClick);
            this.dgvSocios.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSocios_CellFormatting);
            this.dgvSocios.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvSocios_DataBindingComplete);
            // 
            // colSocioId
            // 
            this.colSocioId.DataPropertyName = "IdSocio";
            this.colSocioId.HeaderText = "ID";
            this.colSocioId.MinimumWidth = 8;
            this.colSocioId.Name = "colSocioId";
            this.colSocioId.ReadOnly = true;
            this.colSocioId.Visible = false;
            // 
            // colSocioCedula
            // 
            this.colSocioCedula.DataPropertyName = "Cedula";
            this.colSocioCedula.HeaderText = "Cédula";
            this.colSocioCedula.MinimumWidth = 8;
            this.colSocioCedula.Name = "colSocioCedula";
            this.colSocioCedula.ReadOnly = true;
            // 
            // colSocioNombre
            // 
            this.colSocioNombre.DataPropertyName = "Nombre";
            this.colSocioNombre.HeaderText = "Nombre";
            this.colSocioNombre.MinimumWidth = 8;
            this.colSocioNombre.Name = "colSocioNombre";
            this.colSocioNombre.ReadOnly = true;
            // 
            // colSocioApellido
            // 
            this.colSocioApellido.DataPropertyName = "Apellido";
            this.colSocioApellido.HeaderText = "Apellido";
            this.colSocioApellido.MinimumWidth = 8;
            this.colSocioApellido.Name = "colSocioApellido";
            this.colSocioApellido.ReadOnly = true;
            // 
            // colSocioEmail
            // 
            this.colSocioEmail.DataPropertyName = "Email";
            this.colSocioEmail.HeaderText = "Email";
            this.colSocioEmail.MinimumWidth = 8;
            this.colSocioEmail.Name = "colSocioEmail";
            this.colSocioEmail.ReadOnly = true;
            // 
            // colSocioRuc
            // 
            this.colSocioRuc.DataPropertyName = "RUC";
            this.colSocioRuc.HeaderText = "RUC";
            this.colSocioRuc.MinimumWidth = 8;
            this.colSocioRuc.Name = "colSocioRuc";
            this.colSocioRuc.ReadOnly = true;
            // 
            // colSocioTelefono
            // 
            this.colSocioTelefono.DataPropertyName = "Telefono";
            this.colSocioTelefono.HeaderText = "Teléfono";
            this.colSocioTelefono.MinimumWidth = 8;
            this.colSocioTelefono.Name = "colSocioTelefono";
            this.colSocioTelefono.ReadOnly = true;
            // 
            // colSocioFechaNacim
            // 
            this.colSocioFechaNacim.DataPropertyName = "FechaNacimiento";
            this.colSocioFechaNacim.HeaderText = "Fecha Nacimiento";
            this.colSocioFechaNacim.MinimumWidth = 8;
            this.colSocioFechaNacim.Name = "colSocioFechaNacim";
            this.colSocioFechaNacim.ReadOnly = true;
            // 
            // colSocioFechaReg
            // 
            this.colSocioFechaReg.DataPropertyName = "FechaRegistro";
            this.colSocioFechaReg.HeaderText = "Fecha Registro";
            this.colSocioFechaReg.MinimumWidth = 8;
            this.colSocioFechaReg.Name = "colSocioFechaReg";
            this.colSocioFechaReg.ReadOnly = true;
            // 
            // colSocioPlan
            // 
            this.colSocioPlan.DataPropertyName = "TipoPlan";
            this.colSocioPlan.HeaderText = "Plan";
            this.colSocioPlan.MinimumWidth = 8;
            this.colSocioPlan.Name = "colSocioPlan";
            this.colSocioPlan.ReadOnly = true;
            // 
            // colSocioPrecio
            // 
            this.colSocioPrecio.DataPropertyName = "Precio";
            dataGridViewCellStyle1.Format = "N0";
            this.colSocioPrecio.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSocioPrecio.HeaderText = "Precio";
            this.colSocioPrecio.MinimumWidth = 8;
            this.colSocioPrecio.Name = "colSocioPrecio";
            this.colSocioPrecio.ReadOnly = true;
            // 
            // colSocioVencimiento
            // 
            this.colSocioVencimiento.DataPropertyName = "FechaVencimiento";
            this.colSocioVencimiento.FillWeight = 120F;
            this.colSocioVencimiento.HeaderText = "Vencimiento";
            this.colSocioVencimiento.MinimumWidth = 8;
            this.colSocioVencimiento.Name = "colSocioVencimiento";
            this.colSocioVencimiento.ReadOnly = true;
            // 
            // colSocioEstado
            // 
            this.colSocioEstado.DataPropertyName = "Estado";
            this.colSocioEstado.FillWeight = 80F;
            this.colSocioEstado.HeaderText = "Estado";
            this.colSocioEstado.MinimumWidth = 8;
            this.colSocioEstado.Name = "colSocioEstado";
            this.colSocioEstado.ReadOnly = true;
            // 
            // colSocioContEmerg
            // 
            this.colSocioContEmerg.DataPropertyName = "NombreContactoEmergencia";
            this.colSocioContEmerg.HeaderText = "Contacto Emergencia";
            this.colSocioContEmerg.MinimumWidth = 8;
            this.colSocioContEmerg.Name = "colSocioContEmerg";
            this.colSocioContEmerg.ReadOnly = true;
            this.colSocioContEmerg.Visible = false;
            // 
            // colSocioTelEmerg
            // 
            this.colSocioTelEmerg.DataPropertyName = "TelefonoEmergencia";
            this.colSocioTelEmerg.HeaderText = "Telefono Emergencia";
            this.colSocioTelEmerg.MinimumWidth = 8;
            this.colSocioTelEmerg.Name = "colSocioTelEmerg";
            this.colSocioTelEmerg.ReadOnly = true;
            this.colSocioTelEmerg.Visible = false;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.White;
            this.btnNuevo.ForeColor = System.Drawing.Color.Black;
            this.btnNuevo.Location = new System.Drawing.Point(839, 357);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(109, 49);
            this.btnNuevo.TabIndex = 3;
            this.btnNuevo.Text = "NUEVO";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.White;
            this.btnEditar.ForeColor = System.Drawing.Color.Black;
            this.btnEditar.Location = new System.Drawing.Point(980, 357);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(96, 49);
            this.btnEditar.TabIndex = 4;
            this.btnEditar.Text = "EDITAR";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(12, 69);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(174, 20);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Registros encontrados:";
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.Location = new System.Drawing.Point(1080, 65);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(208, 24);
            this.chkActivo.TabIndex = 1;
            this.chkActivo.Text = "Mostrar Socios Inactivos";
            this.chkActivo.UseVisualStyleBackColor = true;
            this.chkActivo.Click += new System.EventHandler(this.chkActivo_Click);
            // 
            // btnEstado
            // 
            this.btnEstado.BackColor = System.Drawing.Color.White;
            this.btnEstado.ForeColor = System.Drawing.Color.Black;
            this.btnEstado.Location = new System.Drawing.Point(1105, 357);
            this.btnEstado.Name = "btnEstado";
            this.btnEstado.Size = new System.Drawing.Size(180, 49);
            this.btnEstado.TabIndex = 5;
            this.btnEstado.Text = "CAMBIAR ESTADO";
            this.btnEstado.UseVisualStyleBackColor = false;
            this.btnEstado.Click += new System.EventHandler(this.btnEstado_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "GESTIÓN DE SOCIOS";
            // 
            // frmGestionSocios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1300, 419);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEstado);
            this.Controls.Add(this.chkActivo);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.dgvSocios);
            this.Controls.Add(this.txtBuscar);
            this.Name = "frmGestionSocios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGestionSocios";
            this.Click += new System.EventHandler(this.frmGestionSocios_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSocios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvSocios;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.Button btnEstado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioCedula;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioApellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioRuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioTelefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioFechaNacim;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioFechaReg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioPlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioContEmerg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocioTelEmerg;
    }
}