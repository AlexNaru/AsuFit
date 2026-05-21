namespace AsuFit.Presentacion
{
    partial class frmGestionUsuarios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnEstado = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnResetearClave = new System.Windows.Forms.Button();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.colUsuarioId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarioNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarioUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarioRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarioEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarioEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuarioFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.chkMostrarInactivos = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEstado
            // 
            this.btnEstado.BackColor = System.Drawing.Color.White;
            this.btnEstado.ForeColor = System.Drawing.Color.Black;
            this.btnEstado.Location = new System.Drawing.Point(811, 362);
            this.btnEstado.Name = "btnEstado";
            this.btnEstado.Size = new System.Drawing.Size(180, 49);
            this.btnEstado.TabIndex = 9;
            this.btnEstado.Text = "CAMBIAR ESTADO";
            this.btnEstado.UseVisualStyleBackColor = false;
            this.btnEstado.Click += new System.EventHandler(this.btnEstado_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.White;
            this.btnEditar.ForeColor = System.Drawing.Color.Black;
            this.btnEditar.Location = new System.Drawing.Point(658, 362);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(86, 49);
            this.btnEditar.TabIndex = 8;
            this.btnEditar.Text = "EDITAR";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.White;
            this.btnNuevo.ForeColor = System.Drawing.Color.Black;
            this.btnNuevo.Location = new System.Drawing.Point(496, 362);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(91, 49);
            this.btnNuevo.TabIndex = 7;
            this.btnNuevo.Text = "NUEVO";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnResetearClave
            // 
            this.btnResetearClave.BackColor = System.Drawing.Color.White;
            this.btnResetearClave.ForeColor = System.Drawing.Color.Black;
            this.btnResetearClave.Location = new System.Drawing.Point(1048, 362);
            this.btnResetearClave.Name = "btnResetearClave";
            this.btnResetearClave.Size = new System.Drawing.Size(168, 49);
            this.btnResetearClave.TabIndex = 10;
            this.btnResetearClave.Text = "RESETEAR CLAVE";
            this.btnResetearClave.UseVisualStyleBackColor = false;
            this.btnResetearClave.Click += new System.EventHandler(this.btnResetearClave_Click);
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToResizeColumns = false;
            this.dgvUsuarios.AllowUserToResizeRows = false;
            this.dgvUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colUsuarioId,
            this.colUsuarioNombre,
            this.colUsuarioUsername,
            this.colUsuarioRol,
            this.colUsuarioEmail,
            this.colUsuarioEstado,
            this.colUsuarioFecha});
            this.dgvUsuarios.Location = new System.Drawing.Point(18, 101);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.RowHeadersWidth = 62;
            this.dgvUsuarios.RowTemplate.Height = 28;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.Size = new System.Drawing.Size(1198, 246);
            this.dgvUsuarios.TabIndex = 12;
            this.dgvUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellClick);
            this.dgvUsuarios.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvUsuarios_DataBindingComplete);
            // 
            // colUsuarioId
            // 
            this.colUsuarioId.DataPropertyName = "IdUsuario";
            this.colUsuarioId.HeaderText = "Id Usuario";
            this.colUsuarioId.MinimumWidth = 8;
            this.colUsuarioId.Name = "colUsuarioId";
            this.colUsuarioId.ReadOnly = true;
            this.colUsuarioId.Visible = false;
            // 
            // colUsuarioNombre
            // 
            this.colUsuarioNombre.DataPropertyName = "NombreCompleto";
            this.colUsuarioNombre.HeaderText = "Nombre Completo";
            this.colUsuarioNombre.MinimumWidth = 8;
            this.colUsuarioNombre.Name = "colUsuarioNombre";
            this.colUsuarioNombre.ReadOnly = true;
            // 
            // colUsuarioUsername
            // 
            this.colUsuarioUsername.DataPropertyName = "Username";
            this.colUsuarioUsername.HeaderText = "Username";
            this.colUsuarioUsername.MinimumWidth = 8;
            this.colUsuarioUsername.Name = "colUsuarioUsername";
            this.colUsuarioUsername.ReadOnly = true;
            // 
            // colUsuarioRol
            // 
            this.colUsuarioRol.DataPropertyName = "Rol";
            this.colUsuarioRol.HeaderText = "Rol";
            this.colUsuarioRol.MinimumWidth = 8;
            this.colUsuarioRol.Name = "colUsuarioRol";
            this.colUsuarioRol.ReadOnly = true;
            // 
            // colUsuarioEmail
            // 
            this.colUsuarioEmail.DataPropertyName = "Email";
            this.colUsuarioEmail.HeaderText = "Email";
            this.colUsuarioEmail.MinimumWidth = 8;
            this.colUsuarioEmail.Name = "colUsuarioEmail";
            this.colUsuarioEmail.ReadOnly = true;
            // 
            // colUsuarioEstado
            // 
            this.colUsuarioEstado.DataPropertyName = "Estado";
            this.colUsuarioEstado.HeaderText = "Estado";
            this.colUsuarioEstado.MinimumWidth = 8;
            this.colUsuarioEstado.Name = "colUsuarioEstado";
            this.colUsuarioEstado.ReadOnly = true;
            // 
            // colUsuarioFecha
            // 
            this.colUsuarioFecha.DataPropertyName = "FechaRegistro";
            dataGridViewCellStyle4.Format = "dd/MM/yyyy HH:mm";
            this.colUsuarioFecha.DefaultCellStyle = dataGridViewCellStyle4;
            this.colUsuarioFecha.HeaderText = "Fecha de Registro";
            this.colUsuarioFecha.MinimumWidth = 8;
            this.colUsuarioFecha.Name = "colUsuarioFecha";
            this.colUsuarioFecha.ReadOnly = true;
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.Color.White;
            this.txtBuscar.ForeColor = System.Drawing.Color.Black;
            this.txtBuscar.Location = new System.Drawing.Point(367, 61);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(386, 26);
            this.txtBuscar.TabIndex = 11;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(14, 67);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(174, 20);
            this.lblTotal.TabIndex = 13;
            this.lblTotal.Text = "Registros encontrados:";
            // 
            // chkMostrarInactivos
            // 
            this.chkMostrarInactivos.AutoSize = true;
            this.chkMostrarInactivos.Location = new System.Drawing.Point(993, 63);
            this.chkMostrarInactivos.Name = "chkMostrarInactivos";
            this.chkMostrarInactivos.Size = new System.Drawing.Size(223, 24);
            this.chkMostrarInactivos.TabIndex = 14;
            this.chkMostrarInactivos.Text = "Mostrar Usuarios Inactivos";
            this.chkMostrarInactivos.UseVisualStyleBackColor = true;
            this.chkMostrarInactivos.CheckedChanged += new System.EventHandler(this.chkMostrarInactivos_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "GESTIÓN DE USUARIOS";
            // 
            // frmGestionUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 427);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkMostrarInactivos);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.btnResetearClave);
            this.Controls.Add(this.btnEstado);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnNuevo);
            this.Name = "frmGestionUsuarios";
            this.Text = "frmGestionUsuarios";
            this.Load += new System.EventHandler(this.frmGestionUsuarios_Load);
            this.Click += new System.EventHandler(this.frmGestionUsuarios_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEstado;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnResetearClave;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.CheckBox chkMostrarInactivos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarioId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarioNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarioUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarioRol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarioEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarioEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuarioFecha;
        private System.Windows.Forms.Label label1;
    }
}