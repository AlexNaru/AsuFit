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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFinalizarVenta = new System.Windows.Forms.Button();
            this.lblTotalPagar = new System.Windows.Forms.Label();
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.flpCatalogo = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
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
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnFinalizarVenta);
            this.panel1.Controls.Add(this.lblTotalPagar);
            this.panel1.Controls.Add(this.dgvCarrito);
            this.panel1.Location = new System.Drawing.Point(997, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(487, 789);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 591);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Total a pagar:";
            // 
            // btnFinalizarVenta
            // 
            this.btnFinalizarVenta.BackColor = System.Drawing.Color.SteelBlue;
            this.btnFinalizarVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizarVenta.ForeColor = System.Drawing.Color.White;
            this.btnFinalizarVenta.Location = new System.Drawing.Point(166, 640);
            this.btnFinalizarVenta.Name = "btnFinalizarVenta";
            this.btnFinalizarVenta.Size = new System.Drawing.Size(176, 36);
            this.btnFinalizarVenta.TabIndex = 2;
            this.btnFinalizarVenta.Text = "FINALIZAR VENTA";
            this.btnFinalizarVenta.UseVisualStyleBackColor = true;
            this.btnFinalizarVenta.Click += new System.EventHandler(this.btnFinalizarVenta_Click);
            // 
            // lblTotalPagar
            // 
            this.lblTotalPagar.AutoSize = true;
            this.lblTotalPagar.Location = new System.Drawing.Point(374, 591);
            this.lblTotalPagar.Name = "lblTotalPagar";
            this.lblTotalPagar.Size = new System.Drawing.Size(30, 20);
            this.lblTotalPagar.TabIndex = 1;
            this.lblTotalPagar.Text = "Gs";
            // 
            // dgvCarrito
            // 
            this.dgvCarrito.AllowUserToResizeColumns = false;
            this.dgvCarrito.AllowUserToResizeRows = false;
            this.dgvCarrito.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarrito.Location = new System.Drawing.Point(15, 3);
            this.dgvCarrito.Name = "dgvCarrito";
            this.dgvCarrito.RowHeadersWidth = 62;
            this.dgvCarrito.RowTemplate.Height = 28;
            this.dgvCarrito.Size = new System.Drawing.Size(454, 486);
            this.dgvCarrito.TabIndex = 0;
            this.dgvCarrito.SelectionChanged += new System.EventHandler(this.dgvCarrito_SelectionChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1165, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "RESUMEN DE VENTA";
            // 
            // flpCatalogo
            // 
            this.flpCatalogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpCatalogo.AutoScroll = true;
            this.flpCatalogo.Location = new System.Drawing.Point(34, 49);
            this.flpCatalogo.Name = "flpCatalogo";
            this.flpCatalogo.Size = new System.Drawing.Size(957, 789);
            this.flpCatalogo.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "PRODUCTOS:";
            // 
            // cmbFiltroCategoria
            // 
            this.cmbFiltroCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroCategoria.FormattingEnabled = true;
            this.cmbFiltroCategoria.Location = new System.Drawing.Point(520, 9);
            this.cmbFiltroCategoria.Name = "cmbFiltroCategoria";
            this.cmbFiltroCategoria.Size = new System.Drawing.Size(121, 28);
            this.cmbFiltroCategoria.TabIndex = 7;
            // 
            // cmbOrdenar
            // 
            this.cmbOrdenar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrdenar.FormattingEnabled = true;
            this.cmbOrdenar.Location = new System.Drawing.Point(794, 9);
            this.cmbOrdenar.Name = "cmbOrdenar";
            this.cmbOrdenar.Size = new System.Drawing.Size(178, 28);
            this.cmbOrdenar.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(436, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Categoria";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(694, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Ordenar por";
            // 
            // txtBuscarProducto
            // 
            this.txtBuscarProducto.Location = new System.Drawing.Point(151, 9);
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.Size = new System.Drawing.Size(246, 26);
            this.txtBuscarProducto.TabIndex = 12;
            this.txtBuscarProducto.TextChanged += new System.EventHandler(this.txtBuscarProducto_TextChanged);
            // 
            // frmPuntoVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(1530, 883);
            this.Controls.Add(this.txtBuscarProducto);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbOrdenar);
            this.Controls.Add(this.cmbFiltroCategoria);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flpCatalogo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFiltroCategoria;
        private System.Windows.Forms.ComboBox cmbOrdenar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBuscarProducto;
    }
}