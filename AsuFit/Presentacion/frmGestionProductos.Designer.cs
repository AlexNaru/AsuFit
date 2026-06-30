namespace AsuFit.Presentacion
{
    partial class frmGestionProductos
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbIva = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnSubirFoto = new System.Windows.Forms.Button();
            this.picFoto = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkMostrarInactivos = new System.Windows.Forms.CheckBox();
            this.btnCambiarEstado = new System.Windows.Forms.Button();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.colProductoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductoCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductoNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductoCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductoPrecioCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductoPrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductoStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductoStockMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductoProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductoIva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductoEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductoIdProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cmbIva);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cmbProveedor);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtId);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnGuardar);
            this.panel1.Controls.Add(this.btnLimpiar);
            this.panel1.Controls.Add(this.txtStock);
            this.panel1.Controls.Add(this.txtPrecio);
            this.panel1.Controls.Add(this.txtNombre);
            this.panel1.Controls.Add(this.cmbCategoria);
            this.panel1.Controls.Add(this.txtCodigo);
            this.panel1.Controls.Add(this.btnSubirFoto);
            this.panel1.Controls.Add(this.picFoto);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(11, 34);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 455);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.frmGestionProductos_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(137, 245);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Iva %:";
            // 
            // cmbIva
            // 
            this.cmbIva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.cmbIva.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbIva.ForeColor = System.Drawing.Color.White;
            this.cmbIva.FormattingEnabled = true;
            this.cmbIva.Items.AddRange(new object[] {
            "10",
            "5",
            "0"});
            this.cmbIva.Location = new System.Drawing.Point(140, 260);
            this.cmbIva.Margin = new System.Windows.Forms.Padding(2);
            this.cmbIva.Name = "cmbIva";
            this.cmbIva.Size = new System.Drawing.Size(82, 21);
            this.cmbIva.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(137, 200);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Proveedor:";
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.cmbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProveedor.ForeColor = System.Drawing.Color.White;
            this.cmbProveedor.FormattingEnabled = true;
            this.cmbProveedor.Location = new System.Drawing.Point(140, 215);
            this.cmbProveedor.Margin = new System.Windows.Forms.Padding(2);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(82, 21);
            this.cmbProveedor.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(137, 146);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Id:";
            this.label6.Visible = false;
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.ForeColor = System.Drawing.Color.White;
            this.txtId.Location = new System.Drawing.Point(140, 161);
            this.txtId.Margin = new System.Windows.Forms.Padding(2);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(68, 20);
            this.txtId.TabIndex = 2;
            this.txtId.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(8, 341);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Stock Actual:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 294);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Precio Venta (Gs.):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(8, 245);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Categoría:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 200);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Nombre del Producto:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 146);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Código de Barras:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Location = new System.Drawing.Point(127, 393);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(93, 25);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.Location = new System.Drawing.Point(11, 393);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(93, 25);
            this.btnLimpiar.TabIndex = 11;
            this.btnLimpiar.Text = "LIMPIAR";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtStock
            // 
            this.txtStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStock.Enabled = false;
            this.txtStock.ForeColor = System.Drawing.Color.White;
            this.txtStock.Location = new System.Drawing.Point(11, 356);
            this.txtStock.Margin = new System.Windows.Forms.Padding(2);
            this.txtStock.MaxLength = 6;
            this.txtStock.Name = "txtStock";
            this.txtStock.ReadOnly = true;
            this.txtStock.ShortcutsEnabled = false;
            this.txtStock.Size = new System.Drawing.Size(68, 20);
            this.txtStock.TabIndex = 8;
            // 
            // txtPrecio
            // 
            this.txtPrecio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtPrecio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrecio.ForeColor = System.Drawing.Color.White;
            this.txtPrecio.Location = new System.Drawing.Point(11, 309);
            this.txtPrecio.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrecio.MaxLength = 10;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.ShortcutsEnabled = false;
            this.txtPrecio.Size = new System.Drawing.Size(68, 20);
            this.txtPrecio.TabIndex = 7;
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombre.ForeColor = System.Drawing.Color.White;
            this.txtNombre.Location = new System.Drawing.Point(11, 215);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ShortcutsEnabled = false;
            this.txtNombre.Size = new System.Drawing.Size(68, 20);
            this.txtNombre.TabIndex = 3;
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategoria.ForeColor = System.Drawing.Color.White;
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Items.AddRange(new object[] {
            "Suplementos",
            "Bebidas",
            "Snacks"});
            this.cmbCategoria.Location = new System.Drawing.Point(11, 260);
            this.cmbCategoria.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(82, 21);
            this.cmbCategoria.TabIndex = 5;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.ForeColor = System.Drawing.Color.White;
            this.txtCodigo.Location = new System.Drawing.Point(11, 161);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(2);
            this.txtCodigo.MaxLength = 50;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ShortcutsEnabled = false;
            this.txtCodigo.Size = new System.Drawing.Size(68, 20);
            this.txtCodigo.TabIndex = 1;
            // 
            // btnSubirFoto
            // 
            this.btnSubirFoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubirFoto.ForeColor = System.Drawing.Color.White;
            this.btnSubirFoto.Location = new System.Drawing.Point(11, 109);
            this.btnSubirFoto.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubirFoto.Name = "btnSubirFoto";
            this.btnSubirFoto.Size = new System.Drawing.Size(93, 25);
            this.btnSubirFoto.TabIndex = 0;
            this.btnSubirFoto.Text = "SUBIR FOTO";
            this.btnSubirFoto.UseVisualStyleBackColor = true;
            this.btnSubirFoto.Click += new System.EventHandler(this.btnSubirFoto_Click);
            // 
            // picFoto
            // 
            this.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFoto.Location = new System.Drawing.Point(11, 17);
            this.picFoto.Margin = new System.Windows.Forms.Padding(2);
            this.picFoto.Name = "picFoto";
            this.picFoto.Size = new System.Drawing.Size(94, 72);
            this.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFoto.TabIndex = 22;
            this.picFoto.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chkMostrarInactivos);
            this.panel2.Controls.Add(this.btnCambiarEstado);
            this.panel2.Controls.Add(this.txtBuscarProducto);
            this.panel2.Controls.Add(this.dgvProductos);
            this.panel2.Location = new System.Drawing.Point(253, 34);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(552, 455);
            this.panel2.TabIndex = 1;
            this.panel2.Click += new System.EventHandler(this.frmGestionProductos_Click);
            // 
            // chkMostrarInactivos
            // 
            this.chkMostrarInactivos.AutoSize = true;
            this.chkMostrarInactivos.ForeColor = System.Drawing.Color.White;
            this.chkMostrarInactivos.Location = new System.Drawing.Point(371, 18);
            this.chkMostrarInactivos.Margin = new System.Windows.Forms.Padding(2);
            this.chkMostrarInactivos.Name = "chkMostrarInactivos";
            this.chkMostrarInactivos.Size = new System.Drawing.Size(158, 17);
            this.chkMostrarInactivos.TabIndex = 1;
            this.chkMostrarInactivos.Text = "Mostrar Productos Inactivos";
            this.chkMostrarInactivos.UseVisualStyleBackColor = true;
            this.chkMostrarInactivos.CheckedChanged += new System.EventHandler(this.chkMostrarInactivos_CheckedChanged);
            // 
            // btnCambiarEstado
            // 
            this.btnCambiarEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarEstado.ForeColor = System.Drawing.Color.White;
            this.btnCambiarEstado.Location = new System.Drawing.Point(231, 393);
            this.btnCambiarEstado.Margin = new System.Windows.Forms.Padding(2);
            this.btnCambiarEstado.Name = "btnCambiarEstado";
            this.btnCambiarEstado.Size = new System.Drawing.Size(120, 25);
            this.btnCambiarEstado.TabIndex = 3;
            this.btnCambiarEstado.Text = "CAMBIAR ESTADO";
            this.btnCambiarEstado.UseVisualStyleBackColor = true;
            this.btnCambiarEstado.Click += new System.EventHandler(this.btnCambiarEstado_Click);
            // 
            // txtBuscarProducto
            // 
            this.txtBuscarProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtBuscarProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscarProducto.Location = new System.Drawing.Point(25, 17);
            this.txtBuscarProducto.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscarProducto.MaxLength = 100;
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.ShortcutsEnabled = false;
            this.txtBuscarProducto.Size = new System.Drawing.Size(328, 20);
            this.txtBuscarProducto.TabIndex = 0;
            this.txtBuscarProducto.Click += new System.EventHandler(this.txtBuscarProducto_TextChanged);
            this.txtBuscarProducto.TextChanged += new System.EventHandler(this.txtBuscarProducto_TextChanged);
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToResizeColumns = false;
            this.dgvProductos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductoId,
            this.colProductoCodigo,
            this.colProductoNombre,
            this.colProductoCategoria,
            this.colProductoPrecioCompra,
            this.colProductoPrecioVenta,
            this.colProductoStock,
            this.colProductoStockMin,
            this.colProductoProveedor,
            this.colProductoIva,
            this.colProductoEstado,
            this.colProductoIdProveedor});
            this.dgvProductos.Location = new System.Drawing.Point(25, 45);
            this.dgvProductos.Margin = new System.Windows.Forms.Padding(2);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.RowHeadersWidth = 62;
            this.dgvProductos.RowTemplate.Height = 28;
            this.dgvProductos.Size = new System.Drawing.Size(501, 340);
            this.dgvProductos.TabIndex = 2;
            this.dgvProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellClick);
            this.dgvProductos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductos_DataBindingComplete);
            // 
            // colProductoId
            // 
            this.colProductoId.DataPropertyName = "IdProducto";
            this.colProductoId.HeaderText = "Id Producto";
            this.colProductoId.MinimumWidth = 8;
            this.colProductoId.Name = "colProductoId";
            this.colProductoId.Visible = false;
            this.colProductoId.Width = 150;
            // 
            // colProductoCodigo
            // 
            this.colProductoCodigo.DataPropertyName = "CodigoBarras";
            this.colProductoCodigo.HeaderText = "Código";
            this.colProductoCodigo.MinimumWidth = 8;
            this.colProductoCodigo.Name = "colProductoCodigo";
            this.colProductoCodigo.Width = 150;
            // 
            // colProductoNombre
            // 
            this.colProductoNombre.DataPropertyName = "Nombre";
            this.colProductoNombre.HeaderText = "Nombre";
            this.colProductoNombre.MinimumWidth = 8;
            this.colProductoNombre.Name = "colProductoNombre";
            this.colProductoNombre.Width = 150;
            // 
            // colProductoCategoria
            // 
            this.colProductoCategoria.DataPropertyName = "Categoria";
            this.colProductoCategoria.HeaderText = "Categoría";
            this.colProductoCategoria.MinimumWidth = 8;
            this.colProductoCategoria.Name = "colProductoCategoria";
            this.colProductoCategoria.Width = 150;
            // 
            // colProductoPrecioCompra
            // 
            this.colProductoPrecioCompra.DataPropertyName = "PrecioCompra";
            dataGridViewCellStyle2.Format = "N0";
            this.colProductoPrecioCompra.DefaultCellStyle = dataGridViewCellStyle2;
            this.colProductoPrecioCompra.HeaderText = "Precio Compra";
            this.colProductoPrecioCompra.MinimumWidth = 8;
            this.colProductoPrecioCompra.Name = "colProductoPrecioCompra";
            this.colProductoPrecioCompra.Width = 150;
            // 
            // colProductoPrecioVenta
            // 
            this.colProductoPrecioVenta.DataPropertyName = "PrecioVenta";
            dataGridViewCellStyle3.Format = "N0";
            this.colProductoPrecioVenta.DefaultCellStyle = dataGridViewCellStyle3;
            this.colProductoPrecioVenta.HeaderText = "Precio Venta";
            this.colProductoPrecioVenta.MinimumWidth = 8;
            this.colProductoPrecioVenta.Name = "colProductoPrecioVenta";
            this.colProductoPrecioVenta.Width = 150;
            // 
            // colProductoStock
            // 
            this.colProductoStock.DataPropertyName = "StockActual";
            this.colProductoStock.HeaderText = "Stock Actual";
            this.colProductoStock.MinimumWidth = 8;
            this.colProductoStock.Name = "colProductoStock";
            this.colProductoStock.Width = 150;
            // 
            // colProductoStockMin
            // 
            this.colProductoStockMin.DataPropertyName = "StockMinimo";
            this.colProductoStockMin.HeaderText = "Stock Mínimo";
            this.colProductoStockMin.MinimumWidth = 8;
            this.colProductoStockMin.Name = "colProductoStockMin";
            this.colProductoStockMin.Width = 150;
            // 
            // colProductoProveedor
            // 
            this.colProductoProveedor.DataPropertyName = "Proveedor";
            this.colProductoProveedor.HeaderText = "Proveedor";
            this.colProductoProveedor.MinimumWidth = 8;
            this.colProductoProveedor.Name = "colProductoProveedor";
            this.colProductoProveedor.Width = 150;
            // 
            // colProductoIva
            // 
            this.colProductoIva.DataPropertyName = "PorcentajeIva";
            this.colProductoIva.HeaderText = "% IVA";
            this.colProductoIva.MinimumWidth = 8;
            this.colProductoIva.Name = "colProductoIva";
            this.colProductoIva.Width = 150;
            // 
            // colProductoEstado
            // 
            this.colProductoEstado.DataPropertyName = "Estado";
            this.colProductoEstado.HeaderText = "Estado";
            this.colProductoEstado.MinimumWidth = 8;
            this.colProductoEstado.Name = "colProductoEstado";
            this.colProductoEstado.Width = 150;
            // 
            // colProductoIdProveedor
            // 
            this.colProductoIdProveedor.DataPropertyName = "IdProveedor";
            this.colProductoIdProveedor.HeaderText = "Id Proveedor";
            this.colProductoIdProveedor.MinimumWidth = 8;
            this.colProductoIdProveedor.Name = "colProductoIdProveedor";
            this.colProductoIdProveedor.Visible = false;
            this.colProductoIdProveedor.Width = 150;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(9, 8);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "GESTIÓN DE PRODUCTOS";
            // 
            // frmGestionProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(28)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(816, 497);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmGestionProductos";
            this.Text = "frmGestionProductos";
            this.Load += new System.EventHandler(this.frmGestionProductos_Load);
            this.Click += new System.EventHandler(this.frmGestionProductos_Click);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Button btnSubirFoto;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkMostrarInactivos;
        private System.Windows.Forms.Button btnCambiarEstado;
        private System.Windows.Forms.TextBox txtBuscarProducto;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbIva;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoPrecioCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoPrecioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoStockMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoIva;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoIdProveedor;
    }
}