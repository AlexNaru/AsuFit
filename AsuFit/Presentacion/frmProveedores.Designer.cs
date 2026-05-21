namespace AsuFit.Presentacion
{
    partial class frmProveedores
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkMostrarInactivos = new System.Windows.Forms.CheckBox();
            this.dgvProveedores = new System.Windows.Forms.DataGridView();
            this.colProvId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvRuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvContacto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvTelefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvCorreo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvDireccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvCiudad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscarProveedor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.btnCambiarEstado = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCiudad = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtContacto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRuc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblInactivos = new System.Windows.Forms.Label();
            this.lblActivos = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkMostrarInactivos);
            this.panel1.Controls.Add(this.dgvProveedores);
            this.panel1.Controls.Add(this.txtBuscarProveedor);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(16, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(657, 667);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.LimpiarSeleccion_Click);
            // 
            // chkMostrarInactivos
            // 
            this.chkMostrarInactivos.AutoSize = true;
            this.chkMostrarInactivos.Location = new System.Drawing.Point(373, 63);
            this.chkMostrarInactivos.Name = "chkMostrarInactivos";
            this.chkMostrarInactivos.Size = new System.Drawing.Size(249, 24);
            this.chkMostrarInactivos.TabIndex = 1;
            this.chkMostrarInactivos.Text = "Mostrar Proveedores Inactivos";
            this.chkMostrarInactivos.UseVisualStyleBackColor = true;
            this.chkMostrarInactivos.CheckedChanged += new System.EventHandler(this.chkMostrarInactivos_CheckedChanged);
            // 
            // dgvProveedores
            // 
            this.dgvProveedores.AllowUserToAddRows = false;
            this.dgvProveedores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProveedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProveedores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProvId,
            this.colProvNombre,
            this.colProvRuc,
            this.colProvCategoria,
            this.colProvContacto,
            this.colProvTelefono,
            this.colProvCorreo,
            this.colProvDireccion,
            this.colProvCiudad,
            this.colProvEstado});
            this.dgvProveedores.Location = new System.Drawing.Point(23, 111);
            this.dgvProveedores.Name = "dgvProveedores";
            this.dgvProveedores.RowHeadersVisible = false;
            this.dgvProveedores.RowHeadersWidth = 62;
            this.dgvProveedores.RowTemplate.Height = 28;
            this.dgvProveedores.Size = new System.Drawing.Size(599, 525);
            this.dgvProveedores.TabIndex = 2;
            this.dgvProveedores.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProveedores_CellClick);
            this.dgvProveedores.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProveedores_DataBindingComplete);
            // 
            // colProvId
            // 
            this.colProvId.DataPropertyName = "IdProveedor";
            this.colProvId.HeaderText = "Id Proveedor";
            this.colProvId.MinimumWidth = 8;
            this.colProvId.Name = "colProvId";
            this.colProvId.Visible = false;
            // 
            // colProvNombre
            // 
            this.colProvNombre.DataPropertyName = "Nombre";
            this.colProvNombre.HeaderText = "Nombre";
            this.colProvNombre.MinimumWidth = 8;
            this.colProvNombre.Name = "colProvNombre";
            // 
            // colProvRuc
            // 
            this.colProvRuc.DataPropertyName = "RUC";
            this.colProvRuc.HeaderText = "RUC";
            this.colProvRuc.MinimumWidth = 8;
            this.colProvRuc.Name = "colProvRuc";
            // 
            // colProvCategoria
            // 
            this.colProvCategoria.DataPropertyName = "Categoria";
            this.colProvCategoria.HeaderText = "Categoría";
            this.colProvCategoria.MinimumWidth = 8;
            this.colProvCategoria.Name = "colProvCategoria";
            // 
            // colProvContacto
            // 
            this.colProvContacto.DataPropertyName = "Contacto";
            this.colProvContacto.HeaderText = "Contacto";
            this.colProvContacto.MinimumWidth = 8;
            this.colProvContacto.Name = "colProvContacto";
            // 
            // colProvTelefono
            // 
            this.colProvTelefono.DataPropertyName = "Telefono";
            this.colProvTelefono.HeaderText = "Teléfono";
            this.colProvTelefono.MinimumWidth = 8;
            this.colProvTelefono.Name = "colProvTelefono";
            // 
            // colProvCorreo
            // 
            this.colProvCorreo.DataPropertyName = "Correo";
            this.colProvCorreo.HeaderText = "Correo";
            this.colProvCorreo.MinimumWidth = 8;
            this.colProvCorreo.Name = "colProvCorreo";
            // 
            // colProvDireccion
            // 
            this.colProvDireccion.DataPropertyName = "Direccion";
            this.colProvDireccion.HeaderText = "Dirección";
            this.colProvDireccion.MinimumWidth = 8;
            this.colProvDireccion.Name = "colProvDireccion";
            // 
            // colProvCiudad
            // 
            this.colProvCiudad.DataPropertyName = "Ciudad";
            this.colProvCiudad.HeaderText = "Ciudad";
            this.colProvCiudad.MinimumWidth = 8;
            this.colProvCiudad.Name = "colProvCiudad";
            // 
            // colProvEstado
            // 
            this.colProvEstado.DataPropertyName = "Estado";
            this.colProvEstado.HeaderText = "Estado";
            this.colProvEstado.MinimumWidth = 8;
            this.colProvEstado.Name = "colProvEstado";
            // 
            // txtBuscarProveedor
            // 
            this.txtBuscarProveedor.Location = new System.Drawing.Point(23, 63);
            this.txtBuscarProveedor.Name = "txtBuscarProveedor";
            this.txtBuscarProveedor.Size = new System.Drawing.Size(321, 26);
            this.txtBuscarProveedor.TabIndex = 0;
            this.txtBuscarProveedor.TextChanged += new System.EventHandler(this.txtBuscarProveedor_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "1. CATÁLOGO DE PROVEEDORES";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtCorreo);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.txtTelefono);
            this.panel2.Controls.Add(this.btnCambiarEstado);
            this.panel2.Controls.Add(this.btnNuevo);
            this.panel2.Controls.Add(this.btnGuardar);
            this.panel2.Controls.Add(this.txtId);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.chkActivo);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.txtCiudad);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtDireccion);
            this.panel2.Controls.Add(this.txtContacto);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cmbCategoria);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtRuc);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtNombre);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(696, 61);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(572, 485);
            this.panel2.TabIndex = 1;
            this.panel2.Click += new System.EventHandler(this.LimpiarSeleccion_Click);
            // 
            // txtCorreo
            // 
            this.txtCorreo.Location = new System.Drawing.Point(336, 237);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(178, 26);
            this.txtCorreo.TabIndex = 5;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(332, 212);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 20);
            this.label18.TabIndex = 24;
            this.label18.Text = "Correo:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(24, 212);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(75, 20);
            this.label19.TabIndex = 23;
            this.label19.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(28, 237);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(266, 26);
            this.txtTelefono.TabIndex = 4;
            // 
            // btnCambiarEstado
            // 
            this.btnCambiarEstado.Location = new System.Drawing.Point(336, 434);
            this.btnCambiarEstado.Name = "btnCambiarEstado";
            this.btnCambiarEstado.Size = new System.Drawing.Size(178, 31);
            this.btnCambiarEstado.TabIndex = 11;
            this.btnCambiarEstado.Text = "CAMBIAR ESTADO";
            this.btnCambiarEstado.UseVisualStyleBackColor = true;
            this.btnCambiarEstado.Click += new System.EventHandler(this.btnCambiarEstado_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(48, 434);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(100, 31);
            this.btnNuevo.TabIndex = 9;
            this.btnNuevo.Text = "NUEVO";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(188, 434);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(105, 31);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "GUARDAR CAMBIOS";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(336, 383);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(178, 26);
            this.txtId.TabIndex = 18;
            this.txtId.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(332, 359);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 20);
            this.label10.TabIndex = 17;
            this.label10.Text = "Id:";
            this.label10.Visible = false;
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.Location = new System.Drawing.Point(31, 383);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(154, 24);
            this.chkActivo.TabIndex = 8;
            this.chkActivo.Text = "Proveedor Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 359);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 20);
            this.label9.TabIndex = 15;
            this.label9.Text = "Estado:";
            // 
            // txtCiudad
            // 
            this.txtCiudad.Location = new System.Drawing.Point(336, 311);
            this.txtCiudad.Name = "txtCiudad";
            this.txtCiudad.Size = new System.Drawing.Size(178, 26);
            this.txtCiudad.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(332, 286);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "Ciudad:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 286);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(28, 311);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(266, 26);
            this.txtDireccion.TabIndex = 6;
            // 
            // txtContacto
            // 
            this.txtContacto.Location = new System.Drawing.Point(336, 169);
            this.txtContacto.Name = "txtContacto";
            this.txtContacto.Size = new System.Drawing.Size(178, 26);
            this.txtContacto.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(332, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Contacto:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Categoria:";
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Items.AddRange(new object[] {
            "Suplementos",
            "Bebidas",
            "Snacks"});
            this.cmbCategoria.Location = new System.Drawing.Point(28, 167);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(121, 28);
            this.cmbCategoria.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(332, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "RUC:";
            // 
            // txtRuc
            // 
            this.txtRuc.Location = new System.Drawing.Point(336, 89);
            this.txtRuc.Name = "txtRuc";
            this.txtRuc.Size = new System.Drawing.Size(178, 26);
            this.txtRuc.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nombre del Proveedor:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(28, 89);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(266, 26);
            this.txtNombre.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(252, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "2. DETALLES DEL PROVEEDOR";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblInactivos);
            this.panel3.Controls.Add(this.lblActivos);
            this.panel3.Controls.Add(this.lblTotal);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Location = new System.Drawing.Point(696, 567);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(572, 161);
            this.panel3.TabIndex = 2;
            this.panel3.Click += new System.EventHandler(this.LimpiarSeleccion_Click);
            // 
            // lblInactivos
            // 
            this.lblInactivos.AutoSize = true;
            this.lblInactivos.Location = new System.Drawing.Point(429, 109);
            this.lblInactivos.Name = "lblInactivos";
            this.lblInactivos.Size = new System.Drawing.Size(18, 20);
            this.lblInactivos.TabIndex = 27;
            this.lblInactivos.Text = "0";
            // 
            // lblActivos
            // 
            this.lblActivos.AutoSize = true;
            this.lblActivos.Location = new System.Drawing.Point(248, 109);
            this.lblActivos.Name = "lblActivos";
            this.lblActivos.Size = new System.Drawing.Size(18, 20);
            this.lblActivos.TabIndex = 26;
            this.lblActivos.Text = "0";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(66, 109);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(18, 20);
            this.lblTotal.TabIndex = 25;
            this.lblTotal.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(402, 67);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 20);
            this.label14.TabIndex = 2;
            this.label14.Text = "Inactivos";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(230, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 20);
            this.label13.TabIndex = 1;
            this.label13.Text = "Activos:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(28, 67);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "Proveedores:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(263, 20);
            this.label11.TabIndex = 22;
            this.label11.Text = "3. RESUMEN DE PROVEEDORES";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(134, 20);
            this.label15.TabIndex = 29;
            this.label15.Text = "PROVEEDORES";
            // 
            // frmProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 740);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmProveedores";
            this.Text = "frmProveedores";
            this.Load += new System.EventHandler(this.frmProveedores_Load);
            this.Click += new System.EventHandler(this.LimpiarSeleccion_Click);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProveedores)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtBuscarProveedor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRuc;
        private System.Windows.Forms.TextBox txtCiudad;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtContacto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Button btnCambiarEstado;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.DataGridView dgvProveedores;
        private System.Windows.Forms.CheckBox chkMostrarInactivos;
        private System.Windows.Forms.Label lblInactivos;
        private System.Windows.Forms.Label lblActivos;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvRuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvContacto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvTelefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvCorreo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvDireccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvCiudad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvEstado;
    }
}