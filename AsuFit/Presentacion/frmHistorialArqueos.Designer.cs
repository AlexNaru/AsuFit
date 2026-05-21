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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.dgvArqueos = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.btnVerPDF = new System.Windows.Forms.Button();
            this.colArqueoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArqueoFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArqueoIngresos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArqueoEfectivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArqueoDiferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArqueoCajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArqueoEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArqueos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "HISTORIAL DE ARQUEOS Y TURNOS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(381, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Hasta:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Desde:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(438, 48);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(153, 26);
            this.dtpHasta.TabIndex = 7;
            this.dtpHasta.ValueChanged += new System.EventHandler(this.dtpHasta_ValueChanged);
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(69, 48);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(155, 26);
            this.dtpDesde.TabIndex = 6;
            this.dtpDesde.ValueChanged += new System.EventHandler(this.dtpDesde_ValueChanged);
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
            this.dgvArqueos.Location = new System.Drawing.Point(16, 103);
            this.dgvArqueos.MultiSelect = false;
            this.dgvArqueos.Name = "dgvArqueos";
            this.dgvArqueos.ReadOnly = true;
            this.dgvArqueos.RowHeadersVisible = false;
            this.dgvArqueos.RowHeadersWidth = 62;
            this.dgvArqueos.RowTemplate.Height = 28;
            this.dgvArqueos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArqueos.Size = new System.Drawing.Size(575, 230);
            this.dgvArqueos.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 350);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(491, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Selecciona un arqueo cerrado y presiona el botón para ver su detalle";
            // 
            // btnVerPDF
            // 
            this.btnVerPDF.Location = new System.Drawing.Point(196, 401);
            this.btnVerPDF.Name = "btnVerPDF";
            this.btnVerPDF.Size = new System.Drawing.Size(221, 39);
            this.btnVerPDF.TabIndex = 48;
            this.btnVerPDF.Text = "VER PDF DEL TURNO";
            this.btnVerPDF.UseVisualStyleBackColor = true;
            this.btnVerPDF.Click += new System.EventHandler(this.btnVerPDF_Click);
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
            dataGridViewCellStyle1.Format = "dd/MM/yyyy HH:mm";
            this.colArqueoFecha.DefaultCellStyle = dataGridViewCellStyle1;
            this.colArqueoFecha.HeaderText = "Fecha y Hora";
            this.colArqueoFecha.MinimumWidth = 8;
            this.colArqueoFecha.Name = "colArqueoFecha";
            this.colArqueoFecha.ReadOnly = true;
            // 
            // colArqueoIngresos
            // 
            this.colArqueoIngresos.DataPropertyName = "TotalIngresosSistema";
            dataGridViewCellStyle2.Format = "N0";
            this.colArqueoIngresos.DefaultCellStyle = dataGridViewCellStyle2;
            this.colArqueoIngresos.HeaderText = "Total Ingresos";
            this.colArqueoIngresos.MinimumWidth = 8;
            this.colArqueoIngresos.Name = "colArqueoIngresos";
            this.colArqueoIngresos.ReadOnly = true;
            // 
            // colArqueoEfectivo
            // 
            this.colArqueoEfectivo.DataPropertyName = "EfectivoDeclarado";
            dataGridViewCellStyle3.Format = "N0";
            this.colArqueoEfectivo.DefaultCellStyle = dataGridViewCellStyle3;
            this.colArqueoEfectivo.HeaderText = "Efectivo Declarado";
            this.colArqueoEfectivo.MinimumWidth = 8;
            this.colArqueoEfectivo.Name = "colArqueoEfectivo";
            this.colArqueoEfectivo.ReadOnly = true;
            // 
            // colArqueoDiferencia
            // 
            this.colArqueoDiferencia.DataPropertyName = "Diferencia";
            dataGridViewCellStyle4.Format = "N0";
            this.colArqueoDiferencia.DefaultCellStyle = dataGridViewCellStyle4;
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
            // frmHistorialArqueos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 463);
            this.Controls.Add(this.btnVerPDF);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvArqueos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
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
    }
}