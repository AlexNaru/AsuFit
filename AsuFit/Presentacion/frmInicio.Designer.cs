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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblUtilidad = new System.Windows.Forms.Label();
            this.lblEgresos = new System.Windows.Forms.Label();
            this.lblIngresos = new System.Windows.Forms.Label();
            this.lblTituloVencimientos = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvVencimientos = new System.Windows.Forms.DataGridView();
            this.colProxNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProxApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProxVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblProximosVencimientos = new System.Windows.Forms.Label();
            this.dgvVencidos = new System.Windows.Forms.DataGridView();
            this.colVencidoNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVencidoApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVencidoVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl = new System.Windows.Forms.Label();
            this.lblVencimientos = new System.Windows.Forms.Label();
            this.lblActivos = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.chartFinanzas = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel8 = new System.Windows.Forms.Panel();
            this.dgvProductosStock = new System.Windows.Forms.DataGridView();
            this.colStockNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStockActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.dgvProductosStockBajo = new System.Windows.Forms.DataGridView();
            this.colBajoNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBajoActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.lblUtilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUtilidad.ForeColor = System.Drawing.Color.White;
            this.lblUtilidad.Location = new System.Drawing.Point(9, 34);
            this.lblUtilidad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUtilidad.Name = "lblUtilidad";
            this.lblUtilidad.Size = new System.Drawing.Size(14, 13);
            this.lblUtilidad.TabIndex = 1;
            this.lblUtilidad.Text = "0";
            // 
            // lblEgresos
            // 
            this.lblEgresos.AutoSize = true;
            this.lblEgresos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEgresos.ForeColor = System.Drawing.Color.White;
            this.lblEgresos.Location = new System.Drawing.Point(10, 34);
            this.lblEgresos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEgresos.Name = "lblEgresos";
            this.lblEgresos.Size = new System.Drawing.Size(14, 13);
            this.lblEgresos.TabIndex = 2;
            this.lblEgresos.Text = "0";
            // 
            // lblIngresos
            // 
            this.lblIngresos.AutoSize = true;
            this.lblIngresos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIngresos.ForeColor = System.Drawing.Color.White;
            this.lblIngresos.Location = new System.Drawing.Point(11, 34);
            this.lblIngresos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIngresos.Name = "lblIngresos";
            this.lblIngresos.Size = new System.Drawing.Size(14, 13);
            this.lblIngresos.TabIndex = 3;
            this.lblIngresos.Text = "0";
            // 
            // lblTituloVencimientos
            // 
            this.lblTituloVencimientos.AutoSize = true;
            this.lblTituloVencimientos.ForeColor = System.Drawing.Color.White;
            this.lblTituloVencimientos.Location = new System.Drawing.Point(15, 8);
            this.lblTituloVencimientos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTituloVencimientos.Name = "lblTituloVencimientos";
            this.lblTituloVencimientos.Size = new System.Drawing.Size(120, 13);
            this.lblTituloVencimientos.TabIndex = 4;
            this.lblTituloVencimientos.Text = "Proximos vencimientos: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Ingresos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Egresos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(9, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Utilidad";
            // 
            // dgvVencimientos
            // 
            this.dgvVencimientos.AllowUserToAddRows = false;
            this.dgvVencimientos.AllowUserToDeleteRows = false;
            this.dgvVencimientos.AllowUserToResizeColumns = false;
            this.dgvVencimientos.AllowUserToResizeRows = false;
            this.dgvVencimientos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVencimientos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVencimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVencimientos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProxNombre,
            this.colProxApellido,
            this.colProxVencimiento});
            this.dgvVencimientos.Location = new System.Drawing.Point(18, 28);
            this.dgvVencimientos.Margin = new System.Windows.Forms.Padding(2);
            this.dgvVencimientos.MultiSelect = false;
            this.dgvVencimientos.Name = "dgvVencimientos";
            this.dgvVencimientos.ReadOnly = true;
            this.dgvVencimientos.RowHeadersVisible = false;
            this.dgvVencimientos.RowHeadersWidth = 62;
            this.dgvVencimientos.RowTemplate.Height = 35;
            this.dgvVencimientos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVencimientos.Size = new System.Drawing.Size(299, 98);
            this.dgvVencimientos.TabIndex = 10;
            this.dgvVencimientos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvVencimientos_CellFormatting);
            this.dgvVencimientos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvVencimientos_DataBindingComplete);
            // 
            // colProxNombre
            // 
            this.colProxNombre.DataPropertyName = "Nombre";
            this.colProxNombre.HeaderText = "Nombre";
            this.colProxNombre.MinimumWidth = 8;
            this.colProxNombre.Name = "colProxNombre";
            this.colProxNombre.ReadOnly = true;
            // 
            // colProxApellido
            // 
            this.colProxApellido.DataPropertyName = "Apellido";
            this.colProxApellido.HeaderText = "Apellido";
            this.colProxApellido.MinimumWidth = 8;
            this.colProxApellido.Name = "colProxApellido";
            this.colProxApellido.ReadOnly = true;
            // 
            // colProxVencimiento
            // 
            this.colProxVencimiento.DataPropertyName = "FechaVencimiento";
            this.colProxVencimiento.HeaderText = "Vencimiento";
            this.colProxVencimiento.MinimumWidth = 8;
            this.colProxVencimiento.Name = "colProxVencimiento";
            this.colProxVencimiento.ReadOnly = true;
            // 
            // lblProximosVencimientos
            // 
            this.lblProximosVencimientos.AutoSize = true;
            this.lblProximosVencimientos.ForeColor = System.Drawing.Color.White;
            this.lblProximosVencimientos.Location = new System.Drawing.Point(137, 8);
            this.lblProximosVencimientos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProximosVencimientos.Name = "lblProximosVencimientos";
            this.lblProximosVencimientos.Size = new System.Drawing.Size(13, 13);
            this.lblProximosVencimientos.TabIndex = 11;
            this.lblProximosVencimientos.Text = "0";
            // 
            // dgvVencidos
            // 
            this.dgvVencidos.AllowUserToAddRows = false;
            this.dgvVencidos.AllowUserToDeleteRows = false;
            this.dgvVencidos.AllowUserToResizeColumns = false;
            this.dgvVencidos.AllowUserToResizeRows = false;
            this.dgvVencidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVencidos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvVencidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVencidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVencidoNombre,
            this.colVencidoApellido,
            this.colVencidoVencimiento});
            this.dgvVencidos.Location = new System.Drawing.Point(16, 28);
            this.dgvVencidos.Margin = new System.Windows.Forms.Padding(2);
            this.dgvVencidos.MultiSelect = false;
            this.dgvVencidos.Name = "dgvVencidos";
            this.dgvVencidos.ReadOnly = true;
            this.dgvVencidos.RowHeadersVisible = false;
            this.dgvVencidos.RowHeadersWidth = 62;
            this.dgvVencidos.RowTemplate.Height = 35;
            this.dgvVencidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVencidos.Size = new System.Drawing.Size(301, 98);
            this.dgvVencidos.TabIndex = 12;
            this.dgvVencidos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvVencidos_CellFormatting);
            this.dgvVencidos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvVencidos_DataBindingComplete);
            // 
            // colVencidoNombre
            // 
            this.colVencidoNombre.DataPropertyName = "Nombre";
            this.colVencidoNombre.HeaderText = "Nombre";
            this.colVencidoNombre.MinimumWidth = 8;
            this.colVencidoNombre.Name = "colVencidoNombre";
            this.colVencidoNombre.ReadOnly = true;
            // 
            // colVencidoApellido
            // 
            this.colVencidoApellido.DataPropertyName = "Apellido";
            this.colVencidoApellido.HeaderText = "Apellido";
            this.colVencidoApellido.MinimumWidth = 8;
            this.colVencidoApellido.Name = "colVencidoApellido";
            this.colVencidoApellido.ReadOnly = true;
            // 
            // colVencidoVencimiento
            // 
            this.colVencidoVencimiento.DataPropertyName = "FechaVencimiento";
            this.colVencidoVencimiento.HeaderText = "FechaVencimiento";
            this.colVencidoVencimiento.MinimumWidth = 8;
            this.colVencidoVencimiento.Name = "colVencidoVencimiento";
            this.colVencidoVencimiento.ReadOnly = true;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.ForeColor = System.Drawing.Color.White;
            this.lbl.Location = new System.Drawing.Point(13, 8);
            this.lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(54, 13);
            this.lbl.TabIndex = 13;
            this.lbl.Text = "Vencidos:";
            // 
            // lblVencimientos
            // 
            this.lblVencimientos.AutoSize = true;
            this.lblVencimientos.ForeColor = System.Drawing.Color.White;
            this.lblVencimientos.Location = new System.Drawing.Point(70, 8);
            this.lblVencimientos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVencimientos.Name = "lblVencimientos";
            this.lblVencimientos.Size = new System.Drawing.Size(13, 13);
            this.lblVencimientos.TabIndex = 14;
            this.lblVencimientos.Text = "0";
            // 
            // lblActivos
            // 
            this.lblActivos.AutoSize = true;
            this.lblActivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActivos.ForeColor = System.Drawing.Color.White;
            this.lblActivos.Location = new System.Drawing.Point(15, 34);
            this.lblActivos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblActivos.Name = "lblActivos";
            this.lblActivos.Size = new System.Drawing.Size(14, 13);
            this.lblActivos.TabIndex = 0;
            this.lblActivos.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(15, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Asistencias de hoy";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblActivos);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(124, 54);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblIngresos);
            this.panel2.Location = new System.Drawing.Point(129, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(111, 54);
            this.panel2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(81, 10);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "▲";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.lblEgresos);
            this.panel3.Location = new System.Drawing.Point(244, 2);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(111, 54);
            this.panel3.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(80, 10);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "▼";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.lblUtilidad);
            this.panel4.Location = new System.Drawing.Point(359, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(111, 54);
            this.panel4.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.panel5.Controls.Add(this.dgvVencimientos);
            this.panel5.Controls.Add(this.lblTituloVencimientos);
            this.panel5.Controls.Add(this.lblProximosVencimientos);
            this.panel5.Location = new System.Drawing.Point(471, 287);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(327, 137);
            this.panel5.TabIndex = 7;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.panel6.Controls.Add(this.dgvVencidos);
            this.panel6.Controls.Add(this.lbl);
            this.panel6.Controls.Add(this.lblVencimientos);
            this.panel6.Location = new System.Drawing.Point(471, 429);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(327, 137);
            this.panel6.TabIndex = 8;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.chartFinanzas);
            this.panel7.Location = new System.Drawing.Point(32, 144);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(413, 280);
            this.panel7.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "BALANCE MENSUAL";
            // 
            // chartFinanzas
            // 
            chartArea1.Name = "ChartArea1";
            this.chartFinanzas.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chartFinanzas.Legends.Add(legend1);
            this.chartFinanzas.Location = new System.Drawing.Point(21, 38);
            this.chartFinanzas.Margin = new System.Windows.Forms.Padding(2);
            this.chartFinanzas.Name = "chartFinanzas";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartFinanzas.Series.Add(series1);
            this.chartFinanzas.Size = new System.Drawing.Size(377, 234);
            this.chartFinanzas.TabIndex = 6;
            this.chartFinanzas.Text = "chart1";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.panel8.Controls.Add(this.dgvProductosStock);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Location = new System.Drawing.Point(473, 2);
            this.panel8.Margin = new System.Windows.Forms.Padding(2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(327, 137);
            this.panel8.TabIndex = 5;
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
            this.dgvProductosStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStockNombre,
            this.colStockActual});
            this.dgvProductosStock.Location = new System.Drawing.Point(16, 28);
            this.dgvProductosStock.Margin = new System.Windows.Forms.Padding(2);
            this.dgvProductosStock.MultiSelect = false;
            this.dgvProductosStock.Name = "dgvProductosStock";
            this.dgvProductosStock.ReadOnly = true;
            this.dgvProductosStock.RowHeadersVisible = false;
            this.dgvProductosStock.RowHeadersWidth = 62;
            this.dgvProductosStock.RowTemplate.Height = 35;
            this.dgvProductosStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductosStock.Size = new System.Drawing.Size(301, 98);
            this.dgvProductosStock.TabIndex = 12;
            this.dgvProductosStock.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductosStock_DataBindingComplete);
            // 
            // colStockNombre
            // 
            this.colStockNombre.DataPropertyName = "Nombre";
            this.colStockNombre.HeaderText = "Nombre";
            this.colStockNombre.MinimumWidth = 8;
            this.colStockNombre.Name = "colStockNombre";
            this.colStockNombre.ReadOnly = true;
            // 
            // colStockActual
            // 
            this.colStockActual.DataPropertyName = "StockActual";
            this.colStockActual.HeaderText = "Stock Actual";
            this.colStockActual.MinimumWidth = 8;
            this.colStockActual.Name = "colStockActual";
            this.colStockActual.ReadOnly = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(13, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Todos los Productos:";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.panel9.Controls.Add(this.dgvProductosStockBajo);
            this.panel9.Controls.Add(this.label8);
            this.panel9.Location = new System.Drawing.Point(473, 144);
            this.panel9.Margin = new System.Windows.Forms.Padding(2);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(327, 137);
            this.panel9.TabIndex = 6;
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
            this.dgvProductosStockBajo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBajoNombre,
            this.colBajoActual});
            this.dgvProductosStockBajo.Location = new System.Drawing.Point(16, 27);
            this.dgvProductosStockBajo.Margin = new System.Windows.Forms.Padding(2);
            this.dgvProductosStockBajo.MultiSelect = false;
            this.dgvProductosStockBajo.Name = "dgvProductosStockBajo";
            this.dgvProductosStockBajo.ReadOnly = true;
            this.dgvProductosStockBajo.RowHeadersVisible = false;
            this.dgvProductosStockBajo.RowHeadersWidth = 62;
            this.dgvProductosStockBajo.RowTemplate.Height = 35;
            this.dgvProductosStockBajo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductosStockBajo.Size = new System.Drawing.Size(301, 98);
            this.dgvProductosStockBajo.TabIndex = 12;
            this.dgvProductosStockBajo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvProductosStockBajo_CellFormatting);
            this.dgvProductosStockBajo.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductosStockBajo_DataBindingComplete);
            // 
            // colBajoNombre
            // 
            this.colBajoNombre.DataPropertyName = "Nombre";
            this.colBajoNombre.HeaderText = "Nombre";
            this.colBajoNombre.MinimumWidth = 8;
            this.colBajoNombre.Name = "colBajoNombre";
            this.colBajoNombre.ReadOnly = true;
            // 
            // colBajoActual
            // 
            this.colBajoActual.DataPropertyName = "StockActual";
            this.colBajoActual.HeaderText = "Stock Actual";
            this.colBajoActual.MinimumWidth = 8;
            this.colBajoActual.Name = "colBajoActual";
            this.colBajoActual.ReadOnly = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(13, 8);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Productos con Stock Bajo:";
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(28)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(805, 571);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmInicio";
            this.Text = "Inicio";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colProxNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProxApellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProxVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVencidoNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVencidoApellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVencidoVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBajoNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBajoActual;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
    }
}