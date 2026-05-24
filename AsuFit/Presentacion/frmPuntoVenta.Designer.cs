namespace AsuFit.Presentacion
{
    partial class frmPuntoVenta
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFinalizarVenta = new System.Windows.Forms.Button();
            this.lblTotalPagar = new System.Windows.Forms.Label();
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.colCarritoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarritoCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarritoNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarritoRestar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colCarritoCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarritoSumar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colCarritoPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarritoSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarritoEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colCarritoIva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.flpCatalogo = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbFiltroCategoria = new System.Windows.Forms.ComboBox();
            this.cmbOrdenar = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnFinalizarVenta);
            this.panel1.Controls.Add(this.lblTotalPagar);
            this.panel1.Controls.Add(this.dgvCarrito);
            this.panel1.Location = new System.Drawing.Point(632, 42);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 513);
            this.panel1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(28)))), ((int)(((byte)(35)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(102, 424);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "LIMPIAR CARRITO";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnLimpiarCarrito_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 384);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Total a pagar:";
            // 
            // btnFinalizarVenta
            // 
            this.btnFinalizarVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnFinalizarVenta.FlatAppearance.BorderSize = 0;
            this.btnFinalizarVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizarVenta.ForeColor = System.Drawing.Color.Black;
            this.btnFinalizarVenta.Location = new System.Drawing.Point(102, 460);
            this.btnFinalizarVenta.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFinalizarVenta.Name = "btnFinalizarVenta";
            this.btnFinalizarVenta.Size = new System.Drawing.Size(117, 23);
            this.btnFinalizarVenta.TabIndex = 1;
            this.btnFinalizarVenta.Text = "FINALIZAR VENTA";
            this.btnFinalizarVenta.UseVisualStyleBackColor = false;
            this.btnFinalizarVenta.Click += new System.EventHandler(this.btnFinalizarVenta_Click);
            // 
            // lblTotalPagar
            // 
            this.lblTotalPagar.AutoSize = true;
            this.lblTotalPagar.ForeColor = System.Drawing.Color.White;
            this.lblTotalPagar.Location = new System.Drawing.Point(249, 384);
            this.lblTotalPagar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalPagar.Name = "lblTotalPagar";
            this.lblTotalPagar.Size = new System.Drawing.Size(20, 13);
            this.lblTotalPagar.TabIndex = 1;
            this.lblTotalPagar.Text = "Gs";
            // 
            // dgvCarrito
            // 
            this.dgvCarrito.AllowUserToResizeColumns = false;
            this.dgvCarrito.AllowUserToResizeRows = false;
            this.dgvCarrito.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.dgvCarrito.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(24)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCarrito.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvCarrito.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarrito.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCarritoId,
            this.colCarritoCodigo,
            this.colCarritoNombre,
            this.colCarritoRestar,
            this.colCarritoCantidad,
            this.colCarritoSumar,
            this.colCarritoPrecio,
            this.colCarritoSubtotal,
            this.colCarritoEliminar,
            this.colCarritoIva});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCarrito.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvCarrito.EnableHeadersVisualStyles = false;
            this.dgvCarrito.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.dgvCarrito.Location = new System.Drawing.Point(10, 10);
            this.dgvCarrito.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvCarrito.Name = "dgvCarrito";
            this.dgvCarrito.RowHeadersWidth = 62;
            this.dgvCarrito.RowTemplate.Height = 28;
            this.dgvCarrito.Size = new System.Drawing.Size(303, 316);
            this.dgvCarrito.TabIndex = 0;
            this.dgvCarrito.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCarrito_CellContentClick);
            this.dgvCarrito.SelectionChanged += new System.EventHandler(this.dgvCarrito_SelectionChanged);
            // 
            // colCarritoId
            // 
            this.colCarritoId.HeaderText = "ID Producto";
            this.colCarritoId.MinimumWidth = 8;
            this.colCarritoId.Name = "colCarritoId";
            this.colCarritoId.Visible = false;
            this.colCarritoId.Width = 150;
            // 
            // colCarritoCodigo
            // 
            this.colCarritoCodigo.HeaderText = "Código Barras";
            this.colCarritoCodigo.MinimumWidth = 8;
            this.colCarritoCodigo.Name = "colCarritoCodigo";
            this.colCarritoCodigo.Visible = false;
            this.colCarritoCodigo.Width = 150;
            // 
            // colCarritoNombre
            // 
            this.colCarritoNombre.FillWeight = 150F;
            this.colCarritoNombre.HeaderText = "Producto";
            this.colCarritoNombre.MinimumWidth = 8;
            this.colCarritoNombre.Name = "colCarritoNombre";
            this.colCarritoNombre.Width = 150;
            // 
            // colCarritoRestar
            // 
            this.colCarritoRestar.FillWeight = 25F;
            this.colCarritoRestar.HeaderText = "-";
            this.colCarritoRestar.MinimumWidth = 8;
            this.colCarritoRestar.Name = "colCarritoRestar";
            this.colCarritoRestar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCarritoRestar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCarritoRestar.Text = "-";
            this.colCarritoRestar.UseColumnTextForButtonValue = true;
            this.colCarritoRestar.Width = 150;
            // 
            // colCarritoCantidad
            // 
            this.colCarritoCantidad.FillWeight = 60F;
            this.colCarritoCantidad.HeaderText = "Cant.";
            this.colCarritoCantidad.MinimumWidth = 8;
            this.colCarritoCantidad.Name = "colCarritoCantidad";
            this.colCarritoCantidad.Width = 150;
            // 
            // colCarritoSumar
            // 
            this.colCarritoSumar.FillWeight = 25F;
            this.colCarritoSumar.HeaderText = "+";
            this.colCarritoSumar.MinimumWidth = 8;
            this.colCarritoSumar.Name = "colCarritoSumar";
            this.colCarritoSumar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCarritoSumar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCarritoSumar.Text = "+";
            this.colCarritoSumar.UseColumnTextForButtonValue = true;
            this.colCarritoSumar.Width = 150;
            // 
            // colCarritoPrecio
            // 
            dataGridViewCellStyle10.Format = "N0";
            this.colCarritoPrecio.DefaultCellStyle = dataGridViewCellStyle10;
            this.colCarritoPrecio.FillWeight = 95F;
            this.colCarritoPrecio.HeaderText = "Precio U.";
            this.colCarritoPrecio.MinimumWidth = 8;
            this.colCarritoPrecio.Name = "colCarritoPrecio";
            this.colCarritoPrecio.Width = 150;
            // 
            // colCarritoSubtotal
            // 
            dataGridViewCellStyle11.Format = "N0";
            this.colCarritoSubtotal.DefaultCellStyle = dataGridViewCellStyle11;
            this.colCarritoSubtotal.FillWeight = 85F;
            this.colCarritoSubtotal.HeaderText = "Subtotal";
            this.colCarritoSubtotal.MinimumWidth = 8;
            this.colCarritoSubtotal.Name = "colCarritoSubtotal";
            this.colCarritoSubtotal.Width = 150;
            // 
            // colCarritoEliminar
            // 
            this.colCarritoEliminar.FillWeight = 30F;
            this.colCarritoEliminar.HeaderText = "X";
            this.colCarritoEliminar.MinimumWidth = 8;
            this.colCarritoEliminar.Name = "colCarritoEliminar";
            this.colCarritoEliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCarritoEliminar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCarritoEliminar.Text = "X";
            this.colCarritoEliminar.UseColumnTextForButtonValue = true;
            this.colCarritoEliminar.Width = 150;
            // 
            // colCarritoIva
            // 
            this.colCarritoIva.HeaderText = "% IVA";
            this.colCarritoIva.MinimumWidth = 8;
            this.colCarritoIva.Name = "colCarritoIva";
            this.colCarritoIva.Visible = false;
            this.colCarritoIva.Width = 150;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(739, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "RESUMEN DE VENTA";
            // 
            // flpCatalogo
            // 
            this.flpCatalogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flpCatalogo.AutoScroll = true;
            this.flpCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.flpCatalogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpCatalogo.Location = new System.Drawing.Point(9, 42);
            this.flpCatalogo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flpCatalogo.Name = "flpCatalogo";
            this.flpCatalogo.Size = new System.Drawing.Size(620, 514);
            this.flpCatalogo.TabIndex = 3;
            // 
            // cmbFiltroCategoria
            // 
            this.cmbFiltroCategoria.BackColor = System.Drawing.Color.White;
            this.cmbFiltroCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFiltroCategoria.ForeColor = System.Drawing.Color.Black;
            this.cmbFiltroCategoria.FormattingEnabled = true;
            this.cmbFiltroCategoria.Items.AddRange(new object[] {
            "Todas",
            "Suplementos",
            "Bebidas",
            "Snacks"});
            this.cmbFiltroCategoria.Location = new System.Drawing.Point(327, 9);
            this.cmbFiltroCategoria.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbFiltroCategoria.Name = "cmbFiltroCategoria";
            this.cmbFiltroCategoria.Size = new System.Drawing.Size(82, 21);
            this.cmbFiltroCategoria.TabIndex = 1;
            this.cmbFiltroCategoria.SelectedIndexChanged += new System.EventHandler(this.CombosFiltro_SelectedIndexChanged);
            // 
            // cmbOrdenar
            // 
            this.cmbOrdenar.BackColor = System.Drawing.Color.White;
            this.cmbOrdenar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrdenar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOrdenar.ForeColor = System.Drawing.Color.Black;
            this.cmbOrdenar.FormattingEnabled = true;
            this.cmbOrdenar.Items.AddRange(new object[] {
            "Nombre (A-Z)",
            "Nombre (Z-A)",
            "Precio (Menor a Mayor)",
            "Precio (Mayor a Menor)"});
            this.cmbOrdenar.Location = new System.Drawing.Point(509, 9);
            this.cmbOrdenar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbOrdenar.Name = "cmbOrdenar";
            this.cmbOrdenar.Size = new System.Drawing.Size(120, 21);
            this.cmbOrdenar.TabIndex = 2;
            this.cmbOrdenar.Click += new System.EventHandler(this.CombosFiltro_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(271, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Categoria";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(443, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Ordenar por";
            // 
            // txtBuscarProducto
            // 
            this.txtBuscarProducto.BackColor = System.Drawing.Color.White;
            this.txtBuscarProducto.ForeColor = System.Drawing.Color.Black;
            this.txtBuscarProducto.Location = new System.Drawing.Point(9, 10);
            this.txtBuscarProducto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.Size = new System.Drawing.Size(235, 20);
            this.txtBuscarProducto.TabIndex = 0;
            this.txtBuscarProducto.TextChanged += new System.EventHandler(this.txtBuscarProducto_TextChanged);
            // 
            // frmPuntoVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(28)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(966, 568);
            this.Controls.Add(this.txtBuscarProducto);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbOrdenar);
            this.Controls.Add(this.cmbFiltroCategoria);
            this.Controls.Add(this.flpCatalogo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmPuntoVenta";
            this.Text = "frmPuntoVenta";
            this.Load += new System.EventHandler(this.frmPuntoVenta_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvCarrito;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFinalizarVenta;
        private System.Windows.Forms.Label lblTotalPagar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel flpCatalogo;
        private System.Windows.Forms.ComboBox cmbFiltroCategoria;
        private System.Windows.Forms.ComboBox cmbOrdenar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBuscarProducto;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarritoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarritoCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarritoNombre;
        private System.Windows.Forms.DataGridViewButtonColumn colCarritoRestar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarritoCantidad;
        private System.Windows.Forms.DataGridViewButtonColumn colCarritoSumar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarritoPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarritoSubtotal;
        private System.Windows.Forms.DataGridViewButtonColumn colCarritoEliminar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarritoIva;
    }
}