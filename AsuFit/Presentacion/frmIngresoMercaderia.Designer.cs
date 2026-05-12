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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.label1.Location = new System.Drawing.Point(30, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "INGRESO DE MERCADERÍA / COMPRAS A PROVEEDORES";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(381, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "1. SELECCIÓN DE PRODUCTTOS Y PROVEEDOR";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Proveedor:";
            // 
            // cmbProveedores
            // 
            this.cmbProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedores.FormattingEnabled = true;
            this.cmbProveedores.Location = new System.Drawing.Point(15, 93);
            this.cmbProveedores.Name = "cmbProveedores";
            this.cmbProveedores.Size = new System.Drawing.Size(377, 28);
            this.cmbProveedores.TabIndex = 3;
            this.cmbProveedores.SelectedIndexChanged += new System.EventHandler(this.cmbProveedores_SelectedIndexChanged);
            // 
            // btnNuevoProveedor
            // 
            this.btnNuevoProveedor.Location = new System.Drawing.Point(15, 127);
            this.btnNuevoProveedor.Name = "btnNuevoProveedor";
            this.btnNuevoProveedor.Size = new System.Drawing.Size(196, 34);
            this.btnNuevoProveedor.TabIndex = 4;
            this.btnNuevoProveedor.Text = "NUEVO PROVEEDOR";
            this.btnNuevoProveedor.UseVisualStyleBackColor = true;
            this.btnNuevoProveedor.Click += new System.EventHandler(this.btnNuevoProveedor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(355, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "2. DETALLES DE INGRESO DE MERCADERIA";
            // 
            // txtIdProductoSeleccionado
            // 
            this.txtIdProductoSeleccionado.Location = new System.Drawing.Point(47, 64);
            this.txtIdProductoSeleccionado.Name = "txtIdProductoSeleccionado";
            this.txtIdProductoSeleccionado.Size = new System.Drawing.Size(53, 26);
            this.txtIdProductoSeleccionado.TabIndex = 8;
            this.txtIdProductoSeleccionado.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Id:";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 278);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Cantidad (+):";
            // 
            // txtCantidadIngreso
            // 
            this.txtCantidadIngreso.Location = new System.Drawing.Point(18, 311);
            this.txtCantidadIngreso.Name = "txtCantidadIngreso";
            this.txtCantidadIngreso.Size = new System.Drawing.Size(100, 26);
            this.txtCantidadIngreso.TabIndex = 1;
            this.txtCantidadIngreso.TextChanged += new System.EventHandler(this.txtCantidadIngreso_TextChanged);
            this.txtCantidadIngreso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadIngreso_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(309, 278);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(153, 20);
            this.label8.TabIndex = 12;
            this.label8.Text = "Costo Unitario (Gs.):";
            // 
            // txtCostoUnitario
            // 
            this.txtCostoUnitario.Location = new System.Drawing.Point(313, 311);
            this.txtCostoUnitario.Name = "txtCostoUnitario";
            this.txtCostoUnitario.ReadOnly = true;
            this.txtCostoUnitario.Size = new System.Drawing.Size(149, 26);
            this.txtCostoUnitario.TabIndex = 13;
            // 
            // btnConfirmarIngreso
            // 
            this.btnConfirmarIngreso.Location = new System.Drawing.Point(37, 511);
            this.btnConfirmarIngreso.Name = "btnConfirmarIngreso";
            this.btnConfirmarIngreso.Size = new System.Drawing.Size(196, 34);
            this.btnConfirmarIngreso.TabIndex = 3;
            this.btnConfirmarIngreso.Text = "CONFIRMAR INGRESO";
            this.btnConfirmarIngreso.UseVisualStyleBackColor = true;
            this.btnConfirmarIngreso.Click += new System.EventHandler(this.btnConfirmarIngreso_Click);
            // 
            // txtBuscarProducto
            // 
            this.txtBuscarProducto.Location = new System.Drawing.Point(226, 197);
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.Size = new System.Drawing.Size(332, 26);
            this.txtBuscarProducto.TabIndex = 16;
            this.txtBuscarProducto.TextChanged += new System.EventHandler(this.txtBuscarProducto_TextChanged);
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AllowUserToOrderColumns = true;
            this.dgvProductos.AllowUserToResizeColumns = false;
            this.dgvProductos.AllowUserToResizeRows = false;
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle40.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle40.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle40.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle40.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle40.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle40.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle40;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle41.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle41.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle41.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle41.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle41.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductos.DefaultCellStyle = dataGridViewCellStyle41;
            this.dgvProductos.Location = new System.Drawing.Point(15, 243);
            this.dgvProductos.Name = "dgvProductos";
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle42.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle42.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle42.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle42.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle42.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle42.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductos.RowHeadersDefaultCellStyle = dataGridViewCellStyle42;
            this.dgvProductos.RowHeadersWidth = 62;
            this.dgvProductos.RowTemplate.Height = 28;
            this.dgvProductos.Size = new System.Drawing.Size(543, 361);
            this.dgvProductos.TabIndex = 17;
            this.dgvProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellClick);
            this.dgvProductos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductos_DataBindingComplete);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(240, 20);
            this.label10.TabIndex = 18;
            this.label10.Text = "RESUMEN DE LA OPERACIÓN";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 20);
            this.label11.TabIndex = 19;
            this.label11.Text = "Proveedor:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 151);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 20);
            this.label12.TabIndex = 20;
            this.label12.Text = "Producto:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 234);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 20);
            this.label13.TabIndex = 21;
            this.label13.Text = "Cantidad:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 424);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(174, 20);
            this.label14.TabIndex = 25;
            this.label14.Text = "Nuevo Stock Estimado:";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(167, 570);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(87, 34);
            this.btnLimpiar.TabIndex = 27;
            this.btnLimpiar.Text = "LIMPIAR";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // lblResumenCostoTotal
            // 
            this.lblResumenCostoTotal.AutoSize = true;
            this.lblResumenCostoTotal.Location = new System.Drawing.Point(14, 333);
            this.lblResumenCostoTotal.Name = "lblResumenCostoTotal";
            this.lblResumenCostoTotal.Size = new System.Drawing.Size(108, 20);
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
            this.panel1.Location = new System.Drawing.Point(34, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 631);
            this.panel1.TabIndex = 32;
            this.panel1.Click += new System.EventHandler(this.LimpiarSeleccion_Click);
            // 
            // btnNuevoProducto
            // 
            this.btnNuevoProducto.Location = new System.Drawing.Point(15, 186);
            this.btnNuevoProducto.Name = "btnNuevoProducto";
            this.btnNuevoProducto.Size = new System.Drawing.Size(196, 37);
            this.btnNuevoProducto.TabIndex = 19;
            this.btnNuevoProducto.Text = "NUEVO PRODUCTO";
            this.btnNuevoProducto.UseVisualStyleBackColor = true;
            this.btnNuevoProducto.Click += new System.EventHandler(this.btnNuevoProducto_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 197);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(0, 20);
            this.label15.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.panel2.Location = new System.Drawing.Point(627, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(488, 631);
            this.panel2.TabIndex = 33;
            this.panel2.Click += new System.EventHandler(this.LimpiarSeleccion_Click);
            // 
            // lblProveedorProducto
            // 
            this.lblProveedorProducto.AutoSize = true;
            this.lblProveedorProducto.Location = new System.Drawing.Point(205, 178);
            this.lblProveedorProducto.Name = "lblProveedorProducto";
            this.lblProveedorProducto.Size = new System.Drawing.Size(85, 20);
            this.lblProveedorProducto.TabIndex = 22;
            this.lblProveedorProducto.Text = "Proveedor:";
            // 
            // lblDetalleStockMinimo
            // 
            this.lblDetalleStockMinimo.AutoSize = true;
            this.lblDetalleStockMinimo.Location = new System.Drawing.Point(205, 234);
            this.lblDetalleStockMinimo.Name = "lblDetalleStockMinimo";
            this.lblDetalleStockMinimo.Size = new System.Drawing.Size(108, 20);
            this.lblDetalleStockMinimo.TabIndex = 21;
            this.lblDetalleStockMinimo.Text = "Stock Minimo:";
            // 
            // lblDetalleProducto
            // 
            this.lblDetalleProducto.AutoSize = true;
            this.lblDetalleProducto.Location = new System.Drawing.Point(205, 93);
            this.lblDetalleProducto.Name = "lblDetalleProducto";
            this.lblDetalleProducto.Size = new System.Drawing.Size(77, 20);
            this.lblDetalleProducto.TabIndex = 20;
            this.lblDetalleProducto.Text = "Producto:";
            // 
            // lblDetalleCodigo
            // 
            this.lblDetalleCodigo.AutoSize = true;
            this.lblDetalleCodigo.Location = new System.Drawing.Point(205, 119);
            this.lblDetalleCodigo.Name = "lblDetalleCodigo";
            this.lblDetalleCodigo.Size = new System.Drawing.Size(63, 20);
            this.lblDetalleCodigo.TabIndex = 19;
            this.lblDetalleCodigo.Text = "Código:";
            // 
            // lblDetalleCategoria
            // 
            this.lblDetalleCategoria.AutoSize = true;
            this.lblDetalleCategoria.Location = new System.Drawing.Point(205, 148);
            this.lblDetalleCategoria.Name = "lblDetalleCategoria";
            this.lblDetalleCategoria.Size = new System.Drawing.Size(82, 20);
            this.lblDetalleCategoria.TabIndex = 18;
            this.lblDetalleCategoria.Text = "Categoria:";
            // 
            // lblDetalleStock
            // 
            this.lblDetalleStock.AutoSize = true;
            this.lblDetalleStock.Location = new System.Drawing.Point(205, 206);
            this.lblDetalleStock.Name = "lblDetalleStock";
            this.lblDetalleStock.Size = new System.Drawing.Size(103, 20);
            this.lblDetalleStock.TabIndex = 17;
            this.lblDetalleStock.Text = "Stock Actual:";
            // 
            // txtCostoTotal
            // 
            this.txtCostoTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCostoTotal.Location = new System.Drawing.Point(156, 311);
            this.txtCostoTotal.Name = "txtCostoTotal";
            this.txtCostoTotal.Size = new System.Drawing.Size(129, 26);
            this.txtCostoTotal.TabIndex = 2;
            this.txtCostoTotal.TextChanged += new System.EventHandler(this.txtCostoTotal_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(152, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Costo Total (Gs.):";
            // 
            // picProducto
            // 
            this.picProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picProducto.Location = new System.Drawing.Point(18, 93);
            this.picProducto.Name = "picProducto";
            this.picProducto.Size = new System.Drawing.Size(170, 161);
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
            this.panel3.Location = new System.Drawing.Point(1129, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(277, 631);
            this.panel3.TabIndex = 33;
            this.panel3.Click += new System.EventHandler(this.LimpiarSeleccion_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(18, 570);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(118, 34);
            this.btnCancelar.TabIndex = 36;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtResumenNuevoStock
            // 
            this.txtResumenNuevoStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResumenNuevoStock.Location = new System.Drawing.Point(18, 463);
            this.txtResumenNuevoStock.Name = "txtResumenNuevoStock";
            this.txtResumenNuevoStock.ReadOnly = true;
            this.txtResumenNuevoStock.Size = new System.Drawing.Size(236, 26);
            this.txtResumenNuevoStock.TabIndex = 35;
            // 
            // txtResumenTotal
            // 
            this.txtResumenTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResumenTotal.Location = new System.Drawing.Point(18, 366);
            this.txtResumenTotal.Name = "txtResumenTotal";
            this.txtResumenTotal.ReadOnly = true;
            this.txtResumenTotal.Size = new System.Drawing.Size(236, 26);
            this.txtResumenTotal.TabIndex = 34;
            // 
            // txtResumenCantidad
            // 
            this.txtResumenCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResumenCantidad.Location = new System.Drawing.Point(18, 276);
            this.txtResumenCantidad.Name = "txtResumenCantidad";
            this.txtResumenCantidad.ReadOnly = true;
            this.txtResumenCantidad.Size = new System.Drawing.Size(236, 26);
            this.txtResumenCantidad.TabIndex = 33;
            // 
            // txtResumenProducto
            // 
            this.txtResumenProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResumenProducto.Location = new System.Drawing.Point(18, 184);
            this.txtResumenProducto.Name = "txtResumenProducto";
            this.txtResumenProducto.ReadOnly = true;
            this.txtResumenProducto.Size = new System.Drawing.Size(236, 26);
            this.txtResumenProducto.TabIndex = 32;
            // 
            // txtResumenProveedor
            // 
            this.txtResumenProveedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResumenProveedor.Location = new System.Drawing.Point(18, 99);
            this.txtResumenProveedor.Name = "txtResumenProveedor";
            this.txtResumenProveedor.ReadOnly = true;
            this.txtResumenProveedor.Size = new System.Drawing.Size(236, 26);
            this.txtResumenProveedor.TabIndex = 21;
            // 
            // frmIngresoMercaderia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1449, 941);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
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
    }
}