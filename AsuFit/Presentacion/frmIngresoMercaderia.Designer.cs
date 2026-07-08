namespace AsuFit.Presentacion
{
    partial class frmIngresoMercaderia
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbProveedores = new System.Windows.Forms.ComboBox();
            this.btnNuevoProveedor = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIdProductoSeleccionado = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCantidadIngreso = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCostoUnitario = new System.Windows.Forms.TextBox();
            this.btnConfirmarIngreso = new System.Windows.Forms.Button();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.colIngresoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIngresoCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIngresoNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIngresoCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIngresoPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIngresoStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIngresoStockMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIngresoProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIngresoIva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblResumenCostoTotal = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNuevoProducto = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblProveedorProducto = new System.Windows.Forms.Label();
            this.lblDetalleStockMinimo = new System.Windows.Forms.Label();
            this.lblDetalleProducto = new System.Windows.Forms.Label();
            this.lblDetalleCodigo = new System.Windows.Forms.Label();
            this.lblDetalleCategoria = new System.Windows.Forms.Label();
            this.lblDetalleStock = new System.Windows.Forms.Label();
            this.txtCostoTotal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.picProducto = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtResumenNuevoStock = new System.Windows.Forms.TextBox();
            this.txtResumenTotal = new System.Windows.Forms.TextBox();
            this.txtResumenCantidad = new System.Windows.Forms.TextBox();
            this.txtResumenProducto = new System.Windows.Forms.TextBox();
            this.txtResumenProveedor = new System.Windows.Forms.TextBox();
            this.btnHistorialIngresos = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProducto)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "INGRESO DE MERCADERÍA / COMPRAS A PROVEEDORES";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "1. SELECCIÓN DE PRODUCTTOS Y PROVEEDOR";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Proveedor:";
            // 
            // cmbProveedores
            // 
            this.cmbProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedores.FormattingEnabled = true;
            this.cmbProveedores.Location = new System.Drawing.Point(10, 60);
            this.cmbProveedores.Margin = new System.Windows.Forms.Padding(2);
            this.cmbProveedores.Name = "cmbProveedores";
            this.cmbProveedores.Size = new System.Drawing.Size(253, 21);
            this.cmbProveedores.TabIndex = 0;
            this.cmbProveedores.SelectedIndexChanged += new System.EventHandler(this.cmbProveedores_SelectedIndexChanged);
            this.cmbProveedores.DropDownClosed += new System.EventHandler(this.cmbProveedores_DropDownClosed);
            // 
            // btnNuevoProveedor
            // 
            this.btnNuevoProveedor.Location = new System.Drawing.Point(10, 83);
            this.btnNuevoProveedor.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevoProveedor.Name = "btnNuevoProveedor";
            this.btnNuevoProveedor.Size = new System.Drawing.Size(131, 22);
            this.btnNuevoProveedor.TabIndex = 1;
            this.btnNuevoProveedor.Text = "NUEVO PROVEEDOR";
            this.btnNuevoProveedor.UseVisualStyleBackColor = true;
            this.btnNuevoProveedor.Click += new System.EventHandler(this.btnNuevoProveedor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(236, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "2. DETALLES DE INGRESO DE MERCADERIA";
            // 
            // txtIdProductoSeleccionado
            // 
            this.txtIdProductoSeleccionado.Location = new System.Drawing.Point(31, 42);
            this.txtIdProductoSeleccionado.Margin = new System.Windows.Forms.Padding(2);
            this.txtIdProductoSeleccionado.Name = "txtIdProductoSeleccionado";
            this.txtIdProductoSeleccionado.Size = new System.Drawing.Size(37, 20);
            this.txtIdProductoSeleccionado.TabIndex = 8;
            this.txtIdProductoSeleccionado.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 46);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Id:";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 181);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Cantidad (+):";
            // 
            // txtCantidadIngreso
            // 
            this.txtCantidadIngreso.Location = new System.Drawing.Point(12, 202);
            this.txtCantidadIngreso.Margin = new System.Windows.Forms.Padding(2);
            this.txtCantidadIngreso.MaxLength = 6;
            this.txtCantidadIngreso.Name = "txtCantidadIngreso";
            this.txtCantidadIngreso.Size = new System.Drawing.Size(68, 20);
            this.txtCantidadIngreso.TabIndex = 0;
            this.txtCantidadIngreso.TextChanged += new System.EventHandler(this.txtCantidadIngreso_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(206, 181);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Costo Unitario (Gs.):";
            // 
            // txtCostoUnitario
            // 
            this.txtCostoUnitario.Location = new System.Drawing.Point(209, 202);
            this.txtCostoUnitario.Margin = new System.Windows.Forms.Padding(2);
            this.txtCostoUnitario.Name = "txtCostoUnitario";
            this.txtCostoUnitario.ReadOnly = true;
            this.txtCostoUnitario.ShortcutsEnabled = false;
            this.txtCostoUnitario.Size = new System.Drawing.Size(101, 20);
            this.txtCostoUnitario.TabIndex = 2;
            // 
            // btnConfirmarIngreso
            // 
            this.btnConfirmarIngreso.Location = new System.Drawing.Point(25, 332);
            this.btnConfirmarIngreso.Margin = new System.Windows.Forms.Padding(2);
            this.btnConfirmarIngreso.Name = "btnConfirmarIngreso";
            this.btnConfirmarIngreso.Size = new System.Drawing.Size(131, 22);
            this.btnConfirmarIngreso.TabIndex = 0;
            this.btnConfirmarIngreso.Text = "CONFIRMAR INGRESO";
            this.btnConfirmarIngreso.UseVisualStyleBackColor = true;
            this.btnConfirmarIngreso.Click += new System.EventHandler(this.btnConfirmarIngreso_Click);
            // 
            // txtBuscarProducto
            // 
            this.txtBuscarProducto.Location = new System.Drawing.Point(151, 128);
            this.txtBuscarProducto.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscarProducto.MaxLength = 100;
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.Size = new System.Drawing.Size(223, 20);
            this.txtBuscarProducto.TabIndex = 3;
            this.txtBuscarProducto.TextChanged += new System.EventHandler(this.txtBuscarProducto_TextChanged);
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AllowUserToResizeColumns = false;
            this.dgvProductos.AllowUserToResizeRows = false;
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIngresoId,
            this.colIngresoCodigo,
            this.colIngresoNombre,
            this.colIngresoCategoria,
            this.colIngresoPrecio,
            this.colIngresoStock,
            this.colIngresoStockMin,
            this.colIngresoProveedor,
            this.colIngresoIva});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductos.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvProductos.Location = new System.Drawing.Point(10, 158);
            this.dgvProductos.Margin = new System.Windows.Forms.Padding(2);
            this.dgvProductos.Name = "dgvProductos";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductos.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.RowHeadersWidth = 62;
            this.dgvProductos.RowTemplate.Height = 28;
            this.dgvProductos.Size = new System.Drawing.Size(362, 235);
            this.dgvProductos.TabIndex = 17;
            this.dgvProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellClick);
            this.dgvProductos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductos_DataBindingComplete);
            // 
            // colIngresoId
            // 
            this.colIngresoId.DataPropertyName = "IdProducto";
            this.colIngresoId.HeaderText = "Id Producto";
            this.colIngresoId.MinimumWidth = 8;
            this.colIngresoId.Name = "colIngresoId";
            this.colIngresoId.Visible = false;
            // 
            // colIngresoCodigo
            // 
            this.colIngresoCodigo.DataPropertyName = "CodigoBarras";
            this.colIngresoCodigo.HeaderText = "Código";
            this.colIngresoCodigo.MinimumWidth = 8;
            this.colIngresoCodigo.Name = "colIngresoCodigo";
            // 
            // colIngresoNombre
            // 
            this.colIngresoNombre.DataPropertyName = "Nombre";
            this.colIngresoNombre.HeaderText = "Nombre";
            this.colIngresoNombre.MinimumWidth = 8;
            this.colIngresoNombre.Name = "colIngresoNombre";
            // 
            // colIngresoCategoria
            // 
            this.colIngresoCategoria.DataPropertyName = "Categoria";
            this.colIngresoCategoria.HeaderText = "Categoría";
            this.colIngresoCategoria.MinimumWidth = 8;
            this.colIngresoCategoria.Name = "colIngresoCategoria";
            // 
            // colIngresoPrecio
            // 
            this.colIngresoPrecio.DataPropertyName = "PrecioVenta";
            this.colIngresoPrecio.HeaderText = "Precio Venta";
            this.colIngresoPrecio.MinimumWidth = 8;
            this.colIngresoPrecio.Name = "colIngresoPrecio";
            // 
            // colIngresoStock
            // 
            this.colIngresoStock.DataPropertyName = "StockActual";
            dataGridViewCellStyle2.Format = "N0";
            this.colIngresoStock.DefaultCellStyle = dataGridViewCellStyle2;
            this.colIngresoStock.HeaderText = "Stock Actual";
            this.colIngresoStock.MinimumWidth = 8;
            this.colIngresoStock.Name = "colIngresoStock";
            // 
            // colIngresoStockMin
            // 
            this.colIngresoStockMin.DataPropertyName = "StockMinimo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colIngresoStockMin.DefaultCellStyle = dataGridViewCellStyle3;
            this.colIngresoStockMin.HeaderText = "Stock Mínimo";
            this.colIngresoStockMin.MinimumWidth = 8;
            this.colIngresoStockMin.Name = "colIngresoStockMin";
            // 
            // colIngresoProveedor
            // 
            this.colIngresoProveedor.DataPropertyName = "Proveedor";
            this.colIngresoProveedor.HeaderText = "Proveedor";
            this.colIngresoProveedor.MinimumWidth = 8;
            this.colIngresoProveedor.Name = "colIngresoProveedor";
            // 
            // colIngresoIva
            // 
            this.colIngresoIva.DataPropertyName = "PorcentajeIva";
            this.colIngresoIva.HeaderText = "% IVA";
            this.colIngresoIva.MinimumWidth = 8;
            this.colIngresoIva.Name = "colIngresoIva";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 21);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(161, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "RESUMEN DE LA OPERACIÓN";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 46);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Proveedor:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 98);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Producto:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 152);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Cantidad:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 276);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(119, 13);
            this.label14.TabIndex = 25;
            this.label14.Text = "Nuevo Stock Estimado:";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(96, 370);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(74, 22);
            this.btnLimpiar.TabIndex = 27;
            this.btnLimpiar.Text = "LIMPIAR";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // lblResumenCostoTotal
            // 
            this.lblResumenCostoTotal.AutoSize = true;
            this.lblResumenCostoTotal.Location = new System.Drawing.Point(9, 216);
            this.lblResumenCostoTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResumenCostoTotal.Name = "lblResumenCostoTotal";
            this.lblResumenCostoTotal.Size = new System.Drawing.Size(73, 13);
            this.lblResumenCostoTotal.TabIndex = 29;
            this.lblResumenCostoTotal.Text = "Total Compra:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnNuevoProducto);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnNuevoProveedor);
            this.panel1.Controls.Add(this.cmbProveedores);
            this.panel1.Controls.Add(this.dgvProductos);
            this.panel1.Controls.Add(this.txtBuscarProducto);
            this.panel1.Location = new System.Drawing.Point(11, 34);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 411);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.LimpiarSeleccion_Click);
            // 
            // btnNuevoProducto
            // 
            this.btnNuevoProducto.Location = new System.Drawing.Point(10, 121);
            this.btnNuevoProducto.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevoProducto.Name = "btnNuevoProducto";
            this.btnNuevoProducto.Size = new System.Drawing.Size(131, 24);
            this.btnNuevoProducto.TabIndex = 2;
            this.btnNuevoProducto.Text = "NUEVO PRODUCTO";
            this.btnNuevoProducto.UseVisualStyleBackColor = true;
            this.btnNuevoProducto.Click += new System.EventHandler(this.btnNuevoProducto_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 128);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(0, 13);
            this.label15.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnHistorialIngresos);
            this.panel2.Controls.Add(this.lblProveedorProducto);
            this.panel2.Controls.Add(this.lblDetalleStockMinimo);
            this.panel2.Controls.Add(this.lblDetalleProducto);
            this.panel2.Controls.Add(this.lblDetalleCodigo);
            this.panel2.Controls.Add(this.lblDetalleCategoria);
            this.panel2.Controls.Add(this.lblDetalleStock);
            this.panel2.Controls.Add(this.txtCostoTotal);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.picProducto);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtCostoUnitario);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtCantidadIngreso);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtIdProductoSeleccionado);
            this.panel2.Location = new System.Drawing.Point(406, 34);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(326, 411);
            this.panel2.TabIndex = 1;
            this.panel2.Click += new System.EventHandler(this.LimpiarSeleccion_Click);
            // 
            // lblProveedorProducto
            // 
            this.lblProveedorProducto.AutoSize = true;
            this.lblProveedorProducto.Location = new System.Drawing.Point(137, 116);
            this.lblProveedorProducto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProveedorProducto.Name = "lblProveedorProducto";
            this.lblProveedorProducto.Size = new System.Drawing.Size(59, 13);
            this.lblProveedorProducto.TabIndex = 22;
            this.lblProveedorProducto.Text = "Proveedor:";
            // 
            // lblDetalleStockMinimo
            // 
            this.lblDetalleStockMinimo.AutoSize = true;
            this.lblDetalleStockMinimo.Location = new System.Drawing.Point(137, 152);
            this.lblDetalleStockMinimo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDetalleStockMinimo.Name = "lblDetalleStockMinimo";
            this.lblDetalleStockMinimo.Size = new System.Drawing.Size(74, 13);
            this.lblDetalleStockMinimo.TabIndex = 21;
            this.lblDetalleStockMinimo.Text = "Stock Minimo:";
            // 
            // lblDetalleProducto
            // 
            this.lblDetalleProducto.AutoSize = true;
            this.lblDetalleProducto.Location = new System.Drawing.Point(137, 60);
            this.lblDetalleProducto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDetalleProducto.Name = "lblDetalleProducto";
            this.lblDetalleProducto.Size = new System.Drawing.Size(53, 13);
            this.lblDetalleProducto.TabIndex = 20;
            this.lblDetalleProducto.Text = "Producto:";
            // 
            // lblDetalleCodigo
            // 
            this.lblDetalleCodigo.AutoSize = true;
            this.lblDetalleCodigo.Location = new System.Drawing.Point(137, 77);
            this.lblDetalleCodigo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDetalleCodigo.Name = "lblDetalleCodigo";
            this.lblDetalleCodigo.Size = new System.Drawing.Size(43, 13);
            this.lblDetalleCodigo.TabIndex = 19;
            this.lblDetalleCodigo.Text = "Código:";
            // 
            // lblDetalleCategoria
            // 
            this.lblDetalleCategoria.AutoSize = true;
            this.lblDetalleCategoria.Location = new System.Drawing.Point(137, 96);
            this.lblDetalleCategoria.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDetalleCategoria.Name = "lblDetalleCategoria";
            this.lblDetalleCategoria.Size = new System.Drawing.Size(55, 13);
            this.lblDetalleCategoria.TabIndex = 18;
            this.lblDetalleCategoria.Text = "Categoria:";
            // 
            // lblDetalleStock
            // 
            this.lblDetalleStock.AutoSize = true;
            this.lblDetalleStock.Location = new System.Drawing.Point(137, 134);
            this.lblDetalleStock.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDetalleStock.Name = "lblDetalleStock";
            this.lblDetalleStock.Size = new System.Drawing.Size(71, 13);
            this.lblDetalleStock.TabIndex = 17;
            this.lblDetalleStock.Text = "Stock Actual:";
            // 
            // txtCostoTotal
            // 
            this.txtCostoTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCostoTotal.Location = new System.Drawing.Point(104, 202);
            this.txtCostoTotal.Margin = new System.Windows.Forms.Padding(2);
            this.txtCostoTotal.MaxLength = 10;
            this.txtCostoTotal.Name = "txtCostoTotal";
            this.txtCostoTotal.Size = new System.Drawing.Size(87, 20);
            this.txtCostoTotal.TabIndex = 1;
            this.txtCostoTotal.TextChanged += new System.EventHandler(this.txtCostoTotal_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 181);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Costo Total (Gs.):";
            // 
            // picProducto
            // 
            this.picProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picProducto.Location = new System.Drawing.Point(12, 60);
            this.picProducto.Margin = new System.Windows.Forms.Padding(2);
            this.picProducto.Name = "picProducto";
            this.picProducto.Size = new System.Drawing.Size(114, 105);
            this.picProducto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picProducto.TabIndex = 14;
            this.picProducto.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnCancelar);
            this.panel3.Controls.Add(this.txtResumenNuevoStock);
            this.panel3.Controls.Add(this.txtResumenTotal);
            this.panel3.Controls.Add(this.txtResumenCantidad);
            this.panel3.Controls.Add(this.txtResumenProducto);
            this.panel3.Controls.Add(this.btnLimpiar);
            this.panel3.Controls.Add(this.txtResumenProveedor);
            this.panel3.Controls.Add(this.btnConfirmarIngreso);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.lblResumenCostoTotal);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Location = new System.Drawing.Point(741, 34);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(185, 411);
            this.panel3.TabIndex = 2;
            this.panel3.Click += new System.EventHandler(this.LimpiarSeleccion_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(12, 370);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(79, 22);
            this.btnCancelar.TabIndex = 36;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtResumenNuevoStock
            // 
            this.txtResumenNuevoStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResumenNuevoStock.Location = new System.Drawing.Point(12, 301);
            this.txtResumenNuevoStock.Margin = new System.Windows.Forms.Padding(2);
            this.txtResumenNuevoStock.Name = "txtResumenNuevoStock";
            this.txtResumenNuevoStock.ReadOnly = true;
            this.txtResumenNuevoStock.ShortcutsEnabled = false;
            this.txtResumenNuevoStock.Size = new System.Drawing.Size(158, 20);
            this.txtResumenNuevoStock.TabIndex = 35;
            // 
            // txtResumenTotal
            // 
            this.txtResumenTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResumenTotal.Location = new System.Drawing.Point(12, 238);
            this.txtResumenTotal.Margin = new System.Windows.Forms.Padding(2);
            this.txtResumenTotal.Name = "txtResumenTotal";
            this.txtResumenTotal.ReadOnly = true;
            this.txtResumenTotal.ShortcutsEnabled = false;
            this.txtResumenTotal.Size = new System.Drawing.Size(158, 20);
            this.txtResumenTotal.TabIndex = 34;
            // 
            // txtResumenCantidad
            // 
            this.txtResumenCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResumenCantidad.Location = new System.Drawing.Point(12, 179);
            this.txtResumenCantidad.Margin = new System.Windows.Forms.Padding(2);
            this.txtResumenCantidad.Name = "txtResumenCantidad";
            this.txtResumenCantidad.ReadOnly = true;
            this.txtResumenCantidad.ShortcutsEnabled = false;
            this.txtResumenCantidad.Size = new System.Drawing.Size(158, 20);
            this.txtResumenCantidad.TabIndex = 33;
            // 
            // txtResumenProducto
            // 
            this.txtResumenProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResumenProducto.Location = new System.Drawing.Point(12, 120);
            this.txtResumenProducto.Margin = new System.Windows.Forms.Padding(2);
            this.txtResumenProducto.Name = "txtResumenProducto";
            this.txtResumenProducto.ReadOnly = true;
            this.txtResumenProducto.ShortcutsEnabled = false;
            this.txtResumenProducto.Size = new System.Drawing.Size(158, 20);
            this.txtResumenProducto.TabIndex = 32;
            // 
            // txtResumenProveedor
            // 
            this.txtResumenProveedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResumenProveedor.Location = new System.Drawing.Point(12, 64);
            this.txtResumenProveedor.Margin = new System.Windows.Forms.Padding(2);
            this.txtResumenProveedor.Name = "txtResumenProveedor";
            this.txtResumenProveedor.ReadOnly = true;
            this.txtResumenProveedor.ShortcutsEnabled = false;
            this.txtResumenProveedor.Size = new System.Drawing.Size(158, 20);
            this.txtResumenProveedor.TabIndex = 21;
            // 
            // btnHistorialIngresos
            // 
            this.btnHistorialIngresos.Location = new System.Drawing.Point(80, 370);
            this.btnHistorialIngresos.Margin = new System.Windows.Forms.Padding(2);
            this.btnHistorialIngresos.Name = "btnHistorialIngresos";
            this.btnHistorialIngresos.Size = new System.Drawing.Size(176, 22);
            this.btnHistorialIngresos.TabIndex = 23;
            this.btnHistorialIngresos.Text = "HISTORIAL DE INGRESOS";
            this.btnHistorialIngresos.UseVisualStyleBackColor = true;
            this.btnHistorialIngresos.Click += new System.EventHandler(this.btnHistorialIngresos_Click);
            // 
            // frmIngresoMercaderia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 452);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmIngresoMercaderia";
            this.Text = "frmIngresoMercaderia";
            this.Load += new System.EventHandler(this.frmIngresoMercaderia_Load);
            this.Click += new System.EventHandler(this.LimpiarSeleccion_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProducto)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbProveedores;
        private System.Windows.Forms.Button btnNuevoProveedor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIdProductoSeleccionado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCantidadIngreso;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCostoUnitario;
        private System.Windows.Forms.Button btnConfirmarIngreso;
        private System.Windows.Forms.TextBox txtBuscarProducto;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblResumenCostoTotal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox picProducto;
        private System.Windows.Forms.Label lblDetalleProducto;
        private System.Windows.Forms.Label lblDetalleCodigo;
        private System.Windows.Forms.Label lblDetalleCategoria;
        private System.Windows.Forms.Label lblDetalleStock;
        private System.Windows.Forms.TextBox txtCostoTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtResumenProducto;
        private System.Windows.Forms.TextBox txtResumenProveedor;
        private System.Windows.Forms.TextBox txtResumenNuevoStock;
        private System.Windows.Forms.TextBox txtResumenTotal;
        private System.Windows.Forms.TextBox txtResumenCantidad;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblDetalleStockMinimo;
        private System.Windows.Forms.Label lblProveedorProducto;
        private System.Windows.Forms.Button btnNuevoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIngresoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIngresoCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIngresoNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIngresoCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIngresoPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIngresoStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIngresoStockMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIngresoProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIngresoIva;
        private System.Windows.Forms.Button btnHistorialIngresos;
    }
}