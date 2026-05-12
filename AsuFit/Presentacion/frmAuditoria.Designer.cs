namespace AsuFit.Presentacion
{
    partial class frmAuditoria
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnAbrirHistorial = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvAuditoria = new System.Windows.Forms.DataGridView();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.cmbFiltroModulo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditoria)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnAbrirHistorial);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1383, 419);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cierres de Caja";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnAbrirHistorial
            // 
            this.btnAbrirHistorial.Location = new System.Drawing.Point(138, 194);
            this.btnAbrirHistorial.Name = "btnAbrirHistorial";
            this.btnAbrirHistorial.Size = new System.Drawing.Size(306, 35);
            this.btnAbrirHistorial.TabIndex = 13;
            this.btnAbrirHistorial.Text = "📊 ABRIR HISTORIAL DE ARQUEOS";
            this.btnAbrirHistorial.UseVisualStyleBackColor = true;
            this.btnAbrirHistorial.Click += new System.EventHandler(this.btnAbrirHistorial_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(518, 40);
            this.label2.TabIndex = 12;
            this.label2.Text = "Revisá el historial completo de las aperturas y cierres de caja, incluyendo\n los " +
    "montos declarados, faltantes y el cajero responsable.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "1. CONTROL DE CIERRES DIARIOS";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1391, 452);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvAuditoria);
            this.tabPage4.Controls.Add(this.txtBuscar);
            this.tabPage4.Controls.Add(this.cmbFiltroModulo);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1383, 419);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Log del Sistema";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvAuditoria
            // 
            this.dgvAuditoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAuditoria.Location = new System.Drawing.Point(53, 109);
            this.dgvAuditoria.Name = "dgvAuditoria";
            this.dgvAuditoria.RowHeadersWidth = 62;
            this.dgvAuditoria.RowTemplate.Height = 28;
            this.dgvAuditoria.Size = new System.Drawing.Size(1249, 224);
            this.dgvAuditoria.TabIndex = 3;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(374, 32);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(289, 26);
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // cmbFiltroModulo
            // 
            this.cmbFiltroModulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroModulo.FormattingEnabled = true;
            this.cmbFiltroModulo.Items.AddRange(new object[] {
            "Todos",
            "Seguridad",
            "Caja",
            "Cobros",
            "Gastos",
            "Socios",
            "Usuarios",
            "Planes",
            "Inventario",
            "Proveedores",
            "Configuración",
            "Sistema"});
            this.cmbFiltroModulo.Location = new System.Drawing.Point(201, 31);
            this.cmbFiltroModulo.Name = "cmbFiltroModulo";
            this.cmbFiltroModulo.Size = new System.Drawing.Size(121, 28);
            this.cmbFiltroModulo.TabIndex = 1;
            this.cmbFiltroModulo.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroModulo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Filtrar por Módulo:";
            // 
            // frmAuditoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1391, 452);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmAuditoria";
            this.Text = "frmAuditoriaRespaldos";
            this.Load += new System.EventHandler(this.frmAuditoria_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditoria)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnAbrirHistorial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvAuditoria;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.ComboBox cmbFiltroModulo;
        private System.Windows.Forms.Label label3;
    }
}