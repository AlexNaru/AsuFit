namespace AsuFit.Presentacion
{
    partial class frmInicio
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblUtilidad = new System.Windows.Forms.Label();
            this.lblEgresos = new System.Windows.Forms.Label();
            this.lblIngresos = new System.Windows.Forms.Label();
            this.lblTituloVencimientos = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvVencimientos = new System.Windows.Forms.DataGridView();
            this.lblProximosVencimientos = new System.Windows.Forms.Label();
            this.dgvVencidos = new System.Windows.Forms.DataGridView();
            this.lbl = new System.Windows.Forms.Label();
            this.lblVencimientos = new System.Windows.Forms.Label();
            this.lblActivos = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.chartFinanzas = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label5 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.dgvProductosStock = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.dgvProductosStockBajo = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVencimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVencidos)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartFinanzas)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosStock)).BeginInit();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosStockBajo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUtilidad
            // 
            this.lblUtilidad.AutoSize = true;
            this.lblUtilidad.Location = new System.Drawing.Point(13, 52);
            this.lblUtilidad.Name = "lblUtilidad";
            this.lblUtilidad.Size = new System.Drawing.Size(18, 20);
            this.lblUtilidad.TabIndex = 1;
            this.lblUtilidad.Text = "0";
            // 
            // lblEgresos
            // 
            this.lblEgresos.AutoSize = true;
            this.lblEgresos.Location = new System.Drawing.Point(15, 52);
            this.lblEgresos.Name = "lblEgresos";
            this.lblEgresos.Size = new System.Drawing.Size(18, 20);
            this.lblEgresos.TabIndex = 2;
            this.lblEgresos.Text = "0";
            // 
            // lblIngresos
            // 
            this.lblIngresos.AutoSize = true;
            this.lblIngresos.Location = new System.Drawing.Point(26, 52);
            this.lblIngresos.Name = "lblIngresos";
            this.lblIngresos.Size = new System.Drawing.Size(18, 20);
            this.lblIngresos.TabIndex = 3;
            this.lblIngresos.Text = "0";
            // 
            // lblTituloVencimientos
            // 
            this.lblTituloVencimientos.AutoSize = true;
            this.lblTituloVencimientos.Location = new System.Drawing.Point(23, 19);
            this.lblTituloVencimientos.Name = "lblTituloVencimientos";
            this.lblTituloVencimientos.Size = new System.Drawing.Size(177, 20);
            this.lblTituloVencimientos.TabIndex = 4;
            this.lblTituloVencimientos.Text = "Proximos vencimientos: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Ingresos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Egresos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Utilidad";
            // 
            // dgvVencimientos
            // 
            this.dgvVencimientos.AllowUserToAddRows = false;
            this.dgvVencimientos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVencimientos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVencimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVencimientos.Location = new System.Drawing.Point(27, 49);
            this.dgvVencimientos.Name = "dgvVencimientos";
            this.dgvVencimientos.ReadOnly = true;
            this.dgvVencimientos.RowHeadersVisible = false;
            this.dgvVencimientos.RowHeadersWidth = 62;
            this.dgvVencimientos.RowTemplate.Height = 28;
            this.dgvVencimientos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVencimientos.Size = new System.Drawing.Size(448, 150);
            this.dgvVencimientos.TabIndex = 10;
            this.dgvVencimientos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvVencimientos_CellFormatting);
            this.dgvVencimientos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvVencimientos_DataBindingComplete);
            // 
            // lblProximosVencimientos
            // 
            this.lblProximosVencimientos.AutoSize = true;
            this.lblProximosVencimientos.Location = new System.Drawing.Point(206, 19);
            this.lblProximosVencimientos.Name = "lblProximosVencimientos";
            this.lblProximosVencimientos.Size = new System.Drawing.Size(18, 20);
            this.lblProximosVencimientos.TabIndex = 11;
            this.lblProximosVencimientos.Text = "0";
            // 
            // dgvVencidos
            // 
            this.dgvVencidos.AllowUserToAddRows = false;
            this.dgvVencidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVencidos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVencidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVencidos.Location = new System.Drawing.Point(24, 49);
            this.dgvVencidos.Name = "dgvVencidos";
            this.dgvVencidos.ReadOnly = true;
            this.dgvVencidos.RowHeadersVisible = false;
            this.dgvVencidos.RowHeadersWidth = 62;
            this.dgvVencidos.RowTemplate.Height = 28;
            this.dgvVencidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVencidos.Size = new System.Drawing.Size(451, 150);
            this.dgvVencidos.TabIndex = 12;
            this.dgvVencidos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvVencidos_CellFormatting);
            this.dgvVencidos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvVencidos_DataBindingComplete);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(20, 19);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(79, 20);
            this.lbl.TabIndex = 13;
            this.lbl.Text = "Vencidos:";
            // 
            // lblVencimientos
            // 
            this.lblVencimientos.AutoSize = true;
            this.lblVencimientos.Location = new System.Drawing.Point(105, 19);
            this.lblVencimientos.Name = "lblVencimientos";
            this.lblVencimientos.Size = new System.Drawing.Size(18, 20);
            this.lblVencimientos.TabIndex = 14;
            this.lblVencimientos.Text = "0";
            // 
            // lblActivos
            // 
            this.lblActivos.AutoSize = true;
            this.lblActivos.Location = new System.Drawing.Point(22, 52);
            this.lblActivos.Name = "lblActivos";
            this.lblActivos.Size = new System.Drawing.Size(18, 20);
            this.lblActivos.TabIndex = 0;
            this.lblActivos.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Asistencias de hoy";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblActivos);
            this.panel1.Location = new System.Drawing.Point(60, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 82);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblIngresos);
            this.panel2.Location = new System.Drawing.Point(263, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(165, 82);
            this.panel2.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.lblEgresos);
            this.panel3.Location = new System.Drawing.Point(443, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(165, 82);
            this.panel3.TabIndex = 17;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.lblUtilidad);
            this.panel4.Location = new System.Drawing.Point(624, 24);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(165, 82);
            this.panel4.TabIndex = 18;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.dgvVencimientos);
            this.panel5.Controls.Add(this.lblTituloVencimientos);
            this.panel5.Controls.Add(this.lblProximosVencimientos);
            this.panel5.Location = new System.Drawing.Point(59, 623);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(490, 226);
            this.panel5.TabIndex = 19;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.dgvVencidos);
            this.panel6.Controls.Add(this.lbl);
            this.panel6.Controls.Add(this.lblVencimientos);
            this.panel6.Location = new System.Drawing.Point(568, 623);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(490, 226);
            this.panel6.TabIndex = 20;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.chartFinanzas);
            this.panel7.Location = new System.Drawing.Point(60, 145);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(729, 443);
            this.panel7.TabIndex = 21;
            // 
            // chartFinanzas
            // 
            chartArea2.Name = "ChartArea1";
            this.chartFinanzas.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chartFinanzas.Legends.Add(legend2);
            this.chartFinanzas.Location = new System.Drawing.Point(27, 58);
            this.chartFinanzas.Name = "chartFinanzas";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartFinanzas.Series.Add(series2);
            this.chartFinanzas.Size = new System.Drawing.Size(565, 360);
            this.chartFinanzas.TabIndex = 6;
            this.chartFinanzas.Text = "chart1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(166, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "BALANCE MENSUAL";
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.dgvProductosStock);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Location = new System.Drawing.Point(809, 24);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(490, 226);
            this.panel8.TabIndex = 21;
            // 
            // dgvProductosStock
            // 
            this.dgvProductosStock.AllowUserToAddRows = false;
            this.dgvProductosStock.AllowUserToDeleteRows = false;
            this.dgvProductosStock.AllowUserToOrderColumns = true;
            this.dgvProductosStock.AllowUserToResizeColumns = false;
            this.dgvProductosStock.AllowUserToResizeRows = false;
            this.dgvProductosStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductosStock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProductosStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductosStock.Location = new System.Drawing.Point(24, 49);
            this.dgvProductosStock.Name = "dgvProductosStock";
            this.dgvProductosStock.ReadOnly = true;
            this.dgvProductosStock.RowHeadersVisible = false;
            this.dgvProductosStock.RowHeadersWidth = 62;
            this.dgvProductosStock.RowTemplate.Height = 28;
            this.dgvProductosStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductosStock.Size = new System.Drawing.Size(451, 150);
            this.dgvProductosStock.TabIndex = 12;
            this.dgvProductosStock.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductosStock_DataBindingComplete);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(157, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Todos los Productos:";
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.dgvProductosStockBajo);
            this.panel9.Controls.Add(this.label8);
            this.panel9.Location = new System.Drawing.Point(809, 274);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(490, 226);
            this.panel9.TabIndex = 21;
            // 
            // dgvProductosStockBajo
            // 
            this.dgvProductosStockBajo.AllowUserToAddRows = false;
            this.dgvProductosStockBajo.AllowUserToDeleteRows = false;
            this.dgvProductosStockBajo.AllowUserToOrderColumns = true;
            this.dgvProductosStockBajo.AllowUserToResizeColumns = false;
            this.dgvProductosStockBajo.AllowUserToResizeRows = false;
            this.dgvProductosStockBajo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductosStockBajo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProductosStockBajo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductosStockBajo.Location = new System.Drawing.Point(24, 49);
            this.dgvProductosStockBajo.Name = "dgvProductosStockBajo";
            this.dgvProductosStockBajo.ReadOnly = true;
            this.dgvProductosStockBajo.RowHeadersVisible = false;
            this.dgvProductosStockBajo.RowHeadersWidth = 62;
            this.dgvProductosStockBajo.RowTemplate.Height = 28;
            this.dgvProductosStockBajo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductosStockBajo.Size = new System.Drawing.Size(451, 150);
            this.dgvProductosStockBajo.TabIndex = 12;
            this.dgvProductosStockBajo.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductosStockBajo_DataBindingComplete);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(196, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "Productos con Stock Bajo:";
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1715, 911);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmInicio";
            this.Text = "frmInicio";
            this.Load += new System.EventHandler(this.frmInicio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVencimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVencidos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartFinanzas)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosStock)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosStockBajo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblUtilidad;
        private System.Windows.Forms.Label lblEgresos;
        private System.Windows.Forms.Label lblIngresos;
        private System.Windows.Forms.Label lblTituloVencimientos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvVencimientos;
        private System.Windows.Forms.Label lblProximosVencimientos;
        private System.Windows.Forms.DataGridView dgvVencidos;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblVencimientos;
        private System.Windows.Forms.Label lblActivos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFinanzas;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.DataGridView dgvProductosStock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.DataGridView dgvProductosStockBajo;
        private System.Windows.Forms.Label label8;
    }
}