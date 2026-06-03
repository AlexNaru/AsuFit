namespace AsuFit.Presentacion
{
    partial class frmHistorialArqueos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvArqueos = new System.Windows.Forms.DataGridView();
            this.colArqueoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArqueoFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArqueoIngresos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArqueoEfectivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArqueoDiferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArqueoCajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArqueoEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.btnVerPDF = new System.Windows.Forms.Button();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.txtHasta = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArqueos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "HISTORIAL DE ARQUEOS Y TURNOS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Hasta:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Desde:";
            // 
            // dgvArqueos
            // 
            this.dgvArqueos.AllowUserToAddRows = false;
            this.dgvArqueos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvArqueos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArqueos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colArqueoId,
            this.colArqueoFecha,
            this.colArqueoIngresos,
            this.colArqueoEfectivo,
            this.colArqueoDiferencia,
            this.colArqueoCajero,
            this.colArqueoEstado});
            this.dgvArqueos.Location = new System.Drawing.Point(11, 67);
            this.dgvArqueos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvArqueos.MultiSelect = false;
            this.dgvArqueos.Name = "dgvArqueos";
            this.dgvArqueos.ReadOnly = true;
            this.dgvArqueos.RowHeadersVisible = false;
            this.dgvArqueos.RowHeadersWidth = 62;
            this.dgvArqueos.RowTemplate.Height = 28;
            this.dgvArqueos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArqueos.Size = new System.Drawing.Size(383, 150);
            this.dgvArqueos.TabIndex = 10;
            // 
            // colArqueoId
            // 
            this.colArqueoId.DataPropertyName = "IdArqueo";
            this.colArqueoId.HeaderText = "Id Arqueo";
            this.colArqueoId.MinimumWidth = 8;
            this.colArqueoId.Name = "colArqueoId";
            this.colArqueoId.ReadOnly = true;
            this.colArqueoId.Visible = false;
            // 
            // colArqueoFecha
            // 
            this.colArqueoFecha.DataPropertyName = "FechaHora";
            dataGridViewCellStyle13.Format = "dd/MM/yyyy HH:mm";
            this.colArqueoFecha.DefaultCellStyle = dataGridViewCellStyle13;
            this.colArqueoFecha.HeaderText = "Fecha y Hora";
            this.colArqueoFecha.MinimumWidth = 8;
            this.colArqueoFecha.Name = "colArqueoFecha";
            this.colArqueoFecha.ReadOnly = true;
            // 
            // colArqueoIngresos
            // 
            this.colArqueoIngresos.DataPropertyName = "TotalIngresosSistema";
            dataGridViewCellStyle14.Format = "N0";
            this.colArqueoIngresos.DefaultCellStyle = dataGridViewCellStyle14;
            this.colArqueoIngresos.HeaderText = "Total Ingresos";
            this.colArqueoIngresos.MinimumWidth = 8;
            this.colArqueoIngresos.Name = "colArqueoIngresos";
            this.colArqueoIngresos.ReadOnly = true;
            // 
            // colArqueoEfectivo
            // 
            this.colArqueoEfectivo.DataPropertyName = "EfectivoDeclarado";
            dataGridViewCellStyle15.Format = "N0";
            this.colArqueoEfectivo.DefaultCellStyle = dataGridViewCellStyle15;
            this.colArqueoEfectivo.HeaderText = "Efectivo Declarado";
            this.colArqueoEfectivo.MinimumWidth = 8;
            this.colArqueoEfectivo.Name = "colArqueoEfectivo";
            this.colArqueoEfectivo.ReadOnly = true;
            // 
            // colArqueoDiferencia
            // 
            this.colArqueoDiferencia.DataPropertyName = "Diferencia";
            dataGridViewCellStyle16.Format = "N0";
            this.colArqueoDiferencia.DefaultCellStyle = dataGridViewCellStyle16;
            this.colArqueoDiferencia.HeaderText = "Diferencia";
            this.colArqueoDiferencia.MinimumWidth = 8;
            this.colArqueoDiferencia.Name = "colArqueoDiferencia";
            this.colArqueoDiferencia.ReadOnly = true;
            // 
            // colArqueoCajero
            // 
            this.colArqueoCajero.DataPropertyName = "UsuarioRegistra";
            this.colArqueoCajero.HeaderText = "Cajero";
            this.colArqueoCajero.MinimumWidth = 8;
            this.colArqueoCajero.Name = "colArqueoCajero";
            this.colArqueoCajero.ReadOnly = true;
            // 
            // colArqueoEstado
            // 
            this.colArqueoEstado.DataPropertyName = "Estado";
            this.colArqueoEstado.HeaderText = "Estado";
            this.colArqueoEstado.MinimumWidth = 8;
            this.colArqueoEstado.Name = "colArqueoEstado";
            this.colArqueoEstado.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 227);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(332, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Selecciona un arqueo cerrado y presiona el botón para ver su detalle";
            // 
            // btnVerPDF
            // 
            this.btnVerPDF.Location = new System.Drawing.Point(131, 261);
            this.btnVerPDF.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnVerPDF.Name = "btnVerPDF";
            this.btnVerPDF.Size = new System.Drawing.Size(147, 25);
            this.btnVerPDF.TabIndex = 48;
            this.btnVerPDF.Text = "VER PDF DEL TURNO";
            this.btnVerPDF.UseVisualStyleBackColor = true;
            this.btnVerPDF.Click += new System.EventHandler(this.btnVerPDF_Click);
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(141, 32);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(15, 20);
            this.dtpDesde.TabIndex = 49;
            // 
            // txtDesde
            // 
            this.txtDesde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtDesde.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesde.ForeColor = System.Drawing.Color.White;
            this.txtDesde.Location = new System.Drawing.Point(54, 32);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.ReadOnly = true;
            this.txtDesde.Size = new System.Drawing.Size(102, 20);
            this.txtDesde.TabIndex = 50;
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(370, 32);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(2);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(15, 20);
            this.dtpHasta.TabIndex = 51;
            // 
            // txtHasta
            // 
            this.txtHasta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.txtHasta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHasta.ForeColor = System.Drawing.Color.White;
            this.txtHasta.Location = new System.Drawing.Point(283, 32);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.ReadOnly = true;
            this.txtHasta.Size = new System.Drawing.Size(102, 20);
            this.txtHasta.TabIndex = 52;
            // 
            // frmHistorialArqueos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 301);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.txtHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.txtDesde);
            this.Controls.Add(this.btnVerPDF);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvArqueos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmHistorialArqueos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmHistorialArqueos";
            this.Load += new System.EventHandler(this.frmHistorialArqueos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArqueos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvArqueos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnVerPDF;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArqueoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArqueoFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArqueoIngresos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArqueoEfectivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArqueoDiferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArqueoCajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArqueoEstado;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.TextBox txtHasta;
    }
}