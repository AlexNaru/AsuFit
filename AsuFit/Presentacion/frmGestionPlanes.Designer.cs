namespace AsuFit.Presentacion
{
    partial class frmGestionPlanes
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
            this.label4 = new System.Windows.Forms.Label();
            this.dgvPlanes = new System.Windows.Forms.DataGridView();
            this.colPlanId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlanDuracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEstado = new System.Windows.Forms.Button();
            this.chkMostrarInactivos = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanes)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 304);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 20);
            this.label4.TabIndex = 3;
            // 
            // dgvPlanes
            // 
            this.dgvPlanes.AllowUserToAddRows = false;
            this.dgvPlanes.AllowUserToDeleteRows = false;
            this.dgvPlanes.AllowUserToResizeColumns = false;
            this.dgvPlanes.AllowUserToResizeRows = false;
            this.dgvPlanes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPlanes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlanes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPlanId,
            this.colPlanNombre,
            this.colPlanPrecio,
            this.colPlanDuracion});
            this.dgvPlanes.Location = new System.Drawing.Point(17, 98);
            this.dgvPlanes.Name = "dgvPlanes";
            this.dgvPlanes.ReadOnly = true;
            this.dgvPlanes.RowHeadersVisible = false;
            this.dgvPlanes.RowHeadersWidth = 62;
            this.dgvPlanes.RowTemplate.Height = 28;
            this.dgvPlanes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlanes.Size = new System.Drawing.Size(685, 212);
            this.dgvPlanes.TabIndex = 9;
            this.dgvPlanes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlanes_CellClick);
            this.dgvPlanes.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvPlanes_DataBindingComplete);
            // 
            // colPlanId
            // 
            this.colPlanId.DataPropertyName = "IdPlan";
            this.colPlanId.HeaderText = "Id Plan";
            this.colPlanId.MinimumWidth = 8;
            this.colPlanId.Name = "colPlanId";
            this.colPlanId.ReadOnly = true;
            this.colPlanId.Visible = false;
            // 
            // colPlanNombre
            // 
            this.colPlanNombre.DataPropertyName = "NombrePlan";
            this.colPlanNombre.HeaderText = "Nombre del Plan";
            this.colPlanNombre.MinimumWidth = 8;
            this.colPlanNombre.Name = "colPlanNombre";
            this.colPlanNombre.ReadOnly = true;
            // 
            // colPlanPrecio
            // 
            this.colPlanPrecio.DataPropertyName = "Precio";
            dataGridViewCellStyle1.Format = "N0";
            this.colPlanPrecio.DefaultCellStyle = dataGridViewCellStyle1;
            this.colPlanPrecio.HeaderText = "Precio";
            this.colPlanPrecio.MinimumWidth = 8;
            this.colPlanPrecio.Name = "colPlanPrecio";
            this.colPlanPrecio.ReadOnly = true;
            // 
            // colPlanDuracion
            // 
            this.colPlanDuracion.DataPropertyName = "DuracionDias";
            this.colPlanDuracion.HeaderText = "Duración (Días)";
            this.colPlanDuracion.MinimumWidth = 8;
            this.colPlanDuracion.Name = "colPlanDuracion";
            this.colPlanDuracion.ReadOnly = true;
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(291, 339);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(126, 41);
            this.btnEditar.TabIndex = 2;
            this.btnEditar.Text = "EDITAR";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEstado
            // 
            this.btnEstado.Location = new System.Drawing.Point(518, 339);
            this.btnEstado.Name = "btnEstado";
            this.btnEstado.Size = new System.Drawing.Size(184, 41);
            this.btnEstado.TabIndex = 3;
            this.btnEstado.Text = "CAMBIAR ESTADO";
            this.btnEstado.UseVisualStyleBackColor = true;
            this.btnEstado.Click += new System.EventHandler(this.btnEstado_Click);
            // 
            // chkMostrarInactivos
            // 
            this.chkMostrarInactivos.AutoSize = true;
            this.chkMostrarInactivos.Location = new System.Drawing.Point(418, 55);
            this.chkMostrarInactivos.Name = "chkMostrarInactivos";
            this.chkMostrarInactivos.Size = new System.Drawing.Size(284, 24);
            this.chkMostrarInactivos.TabIndex = 0;
            this.chkMostrarInactivos.Text = "Mostrar planes eliminados/inactivos";
            this.chkMostrarInactivos.UseVisualStyleBackColor = true;
            this.chkMostrarInactivos.CheckedChanged += new System.EventHandler(this.chkMostrarInactivos_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "GESTIÓN DE PLANES";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(17, 339);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(126, 41);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "NUEVO";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(13, 56);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(154, 20);
            this.lblTotal.TabIndex = 15;
            this.lblTotal.Text = "Planes encontrados:";
            // 
            // frmGestionPlanes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 395);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkMostrarInactivos);
            this.Controls.Add(this.btnEstado);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.dgvPlanes);
            this.Controls.Add(this.label4);
            this.Name = "frmGestionPlanes";
            this.Text = "frmGestionPlanes";
            this.Load += new System.EventHandler(this.frmGestionPlanes_Load);
            this.Click += new System.EventHandler(this.frmGestionPlanes_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvPlanes;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEstado;
        private System.Windows.Forms.CheckBox chkMostrarInactivos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlanDuracion;
    }
}