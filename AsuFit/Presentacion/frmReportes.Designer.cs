namespace AsuFit.Presentacion
{
    partial class frmReportes
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
            this.tabControlReportes = new System.Windows.Forms.TabControl();
            this.tabIngresos = new System.Windows.Forms.TabPage();
            this.lblTotalIngresos = new System.Windows.Forms.Label();
            this.dgvIngresos = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.tabTopProductos = new System.Windows.Forms.TabPage();
            this.dgvTopProductos = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpHastaTop = new System.Windows.Forms.DateTimePicker();
            this.dtpDesdeTop = new System.Windows.Forms.DateTimePicker();
            this.tabControlReportes.SuspendLayout();
            this.tabIngresos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngresos)).BeginInit();
            this.tabTopProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlReportes
            // 
            this.tabControlReportes.Controls.Add(this.tabIngresos);
            this.tabControlReportes.Controls.Add(this.tabTopProductos);
            this.tabControlReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlReportes.Location = new System.Drawing.Point(0, 0);
            this.tabControlReportes.Name = "tabControlReportes";
            this.tabControlReportes.SelectedIndex = 0;
            this.tabControlReportes.Size = new System.Drawing.Size(797, 503);
            this.tabControlReportes.TabIndex = 0;
            // 
            // tabIngresos
            // 
            this.tabIngresos.Controls.Add(this.lblTotalIngresos);
            this.tabIngresos.Controls.Add(this.dgvIngresos);
            this.tabIngresos.Controls.Add(this.label2);
            this.tabIngresos.Controls.Add(this.label1);
            this.tabIngresos.Controls.Add(this.dtpHasta);
            this.tabIngresos.Controls.Add(this.dtpDesde);
            this.tabIngresos.Location = new System.Drawing.Point(4, 29);
            this.tabIngresos.Name = "tabIngresos";
            this.tabIngresos.Padding = new System.Windows.Forms.Padding(3);
            this.tabIngresos.Size = new System.Drawing.Size(789, 470);
            this.tabIngresos.TabIndex = 0;
            this.tabIngresos.Text = "Ingresos por Fechas";
            this.tabIngresos.UseVisualStyleBackColor = true;
            // 
            // lblTotalIngresos
            // 
            this.lblTotalIngresos.AutoSize = true;
            this.lblTotalIngresos.Location = new System.Drawing.Point(436, 323);
            this.lblTotalIngresos.Name = "lblTotalIngresos";
            this.lblTotalIngresos.Size = new System.Drawing.Size(213, 20);
            this.lblTotalIngresos.TabIndex = 12;
            this.lblTotalIngresos.Text = "TOTAL RECAUDADO: Gs. 0";
            // 
            // dgvIngresos
            // 
            this.dgvIngresos.AllowUserToAddRows = false;
            this.dgvIngresos.AllowUserToDeleteRows = false;
            this.dgvIngresos.AllowUserToResizeColumns = false;
            this.dgvIngresos.AllowUserToResizeRows = false;
            this.dgvIngresos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIngresos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIngresos.Location = new System.Drawing.Point(74, 136);
            this.dgvIngresos.Name = "dgvIngresos";
            this.dgvIngresos.ReadOnly = true;
            this.dgvIngresos.RowHeadersVisible = false;
            this.dgvIngresos.RowHeadersWidth = 62;
            this.dgvIngresos.RowTemplate.Height = 28;
            this.dgvIngresos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIngresos.Size = new System.Drawing.Size(575, 150);
            this.dgvIngresos.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(439, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Hasta:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Desde:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(496, 24);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(153, 26);
            this.dtpHasta.TabIndex = 7;
            this.dtpHasta.ValueChanged += new System.EventHandler(this.dtpHasta_ValueChanged);
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(127, 24);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(155, 26);
            this.dtpDesde.TabIndex = 6;
            this.dtpDesde.ValueChanged += new System.EventHandler(this.dtpDesde_ValueChanged);
            // 
            // tabTopProductos
            // 
            this.tabTopProductos.Controls.Add(this.dgvTopProductos);
            this.tabTopProductos.Controls.Add(this.label3);
            this.tabTopProductos.Controls.Add(this.label4);
            this.tabTopProductos.Controls.Add(this.dtpHastaTop);
            this.tabTopProductos.Controls.Add(this.dtpDesdeTop);
            this.tabTopProductos.Location = new System.Drawing.Point(4, 29);
            this.tabTopProductos.Name = "tabTopProductos";
            this.tabTopProductos.Padding = new System.Windows.Forms.Padding(3);
            this.tabTopProductos.Size = new System.Drawing.Size(789, 470);
            this.tabTopProductos.TabIndex = 1;
            this.tabTopProductos.Text = "Top 5 Productos Más Vendidos";
            this.tabTopProductos.UseVisualStyleBackColor = true;
            // 
            // dgvTopProductos
            // 
            this.dgvTopProductos.AllowUserToAddRows = false;
            this.dgvTopProductos.AllowUserToDeleteRows = false;
            this.dgvTopProductos.AllowUserToResizeColumns = false;
            this.dgvTopProductos.AllowUserToResizeRows = false;
            this.dgvTopProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopProductos.Location = new System.Drawing.Point(71, 128);
            this.dgvTopProductos.Name = "dgvTopProductos";
            this.dgvTopProductos.ReadOnly = true;
            this.dgvTopProductos.RowHeadersVisible = false;
            this.dgvTopProductos.RowHeadersWidth = 62;
            this.dgvTopProductos.RowTemplate.Height = 28;
            this.dgvTopProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopProductos.Size = new System.Drawing.Size(575, 150);
            this.dgvTopProductos.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(436, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Hasta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Desde:";
            // 
            // dtpHastaTop
            // 
            this.dtpHastaTop.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHastaTop.Location = new System.Drawing.Point(493, 16);
            this.dtpHastaTop.Name = "dtpHastaTop";
            this.dtpHastaTop.Size = new System.Drawing.Size(153, 26);
            this.dtpHastaTop.TabIndex = 12;
            this.dtpHastaTop.ValueChanged += new System.EventHandler(this.dtpHastaTop_ValueChanged);
            // 
            // dtpDesdeTop
            // 
            this.dtpDesdeTop.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesdeTop.Location = new System.Drawing.Point(124, 16);
            this.dtpDesdeTop.Name = "dtpDesdeTop";
            this.dtpDesdeTop.Size = new System.Drawing.Size(155, 26);
            this.dtpDesdeTop.TabIndex = 11;
            this.dtpDesdeTop.ValueChanged += new System.EventHandler(this.dtpDesdeTop_ValueChanged);
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 503);
            this.Controls.Add(this.tabControlReportes);
            this.Name = "frmReportes";
            this.Text = "frmReportes";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            this.tabControlReportes.ResumeLayout(false);
            this.tabIngresos.ResumeLayout(false);
            this.tabIngresos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngresos)).EndInit();
            this.tabTopProductos.ResumeLayout(false);
            this.tabTopProductos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProductos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlReportes;
        private System.Windows.Forms.TabPage tabIngresos;
        private System.Windows.Forms.TabPage tabTopProductos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DataGridView dgvIngresos;
        private System.Windows.Forms.Label lblTotalIngresos;
        private System.Windows.Forms.DataGridView dgvTopProductos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpHastaTop;
        private System.Windows.Forms.DateTimePicker dtpDesdeTop;
    }
}