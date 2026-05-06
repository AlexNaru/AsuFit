namespace AsuFit.Presentacion
{
    partial class frmRegistrarUsuario
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
            this.txtNombreCompleto = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtRespuesta = new System.Windows.Forms.TextBox();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPreguntaSeguridad = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtConfirmarPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtNombreCompleto
            // 
            this.txtNombreCompleto.Location = new System.Drawing.Point(257, 35);
            this.txtNombreCompleto.Name = "txtNombreCompleto";
            this.txtNombreCompleto.Size = new System.Drawing.Size(100, 26);
            this.txtNombreCompleto.TabIndex = 0;
            this.txtNombreCompleto.TextChanged += new System.EventHandler(this.VerificarCamposObligatorios);
            this.txtNombreCompleto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NavegacionEnter_KeyDown);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(257, 321);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 26);
            this.txtEmail.TabIndex = 6;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NavegacionEnter_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(257, 129);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 26);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.TextChanged += new System.EventHandler(this.VerificarCamposObligatorios);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NavegacionEnter_KeyDown);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(257, 82);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 26);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.TextChanged += new System.EventHandler(this.VerificarCamposObligatorios);
            this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NavegacionEnter_KeyDown);
            // 
            // txtRespuesta
            // 
            this.txtRespuesta.Location = new System.Drawing.Point(257, 428);
            this.txtRespuesta.Name = "txtRespuesta";
            this.txtRespuesta.PasswordChar = '*';
            this.txtRespuesta.Size = new System.Drawing.Size(100, 26);
            this.txtRespuesta.TabIndex = 8;
            this.txtRespuesta.TextChanged += new System.EventHandler(this.VerificarCamposObligatorios);
            this.txtRespuesta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NavegacionEnter_KeyDown);
            // 
            // cmbRol
            // 
            this.cmbRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Items.AddRange(new object[] {
            "Administrador",
            "Recepcionista"});
            this.cmbRol.Location = new System.Drawing.Point(257, 224);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(121, 28);
            this.cmbRol.TabIndex = 4;
            this.cmbRol.SelectedIndexChanged += new System.EventHandler(this.VerificarCamposObligatorios);
            this.cmbRol.SelectionChangeCommitted += new System.EventHandler(this.cmbRol_SelectionChangeCommitted);
            this.cmbRol.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbRol_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(55, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nombre completo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(55, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Email";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(55, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(55, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Contraseña";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(55, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Rol";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(55, 428);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Respuesta de Seguridad";
            // 
            // lblPreguntaSeguridad
            // 
            this.lblPreguntaSeguridad.AutoSize = true;
            this.lblPreguntaSeguridad.ForeColor = System.Drawing.Color.White;
            this.lblPreguntaSeguridad.Location = new System.Drawing.Point(253, 377);
            this.lblPreguntaSeguridad.Name = "lblPreguntaSeguridad";
            this.lblPreguntaSeguridad.Size = new System.Drawing.Size(86, 20);
            this.lblPreguntaSeguridad.TabIndex = 7;
            this.lblPreguntaSeguridad.Text = "Pregunta...";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(55, 377);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "Pregunta de seguridad:";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnGuardar.Location = new System.Drawing.Point(349, 494);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 32);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(55, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(162, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Confirmar contraseña";
            // 
            // txtConfirmarPassword
            // 
            this.txtConfirmarPassword.Location = new System.Drawing.Point(257, 178);
            this.txtConfirmarPassword.Name = "txtConfirmarPassword";
            this.txtConfirmarPassword.PasswordChar = '*';
            this.txtConfirmarPassword.Size = new System.Drawing.Size(100, 26);
            this.txtConfirmarPassword.TabIndex = 3;
            this.txtConfirmarPassword.TextChanged += new System.EventHandler(this.VerificarCamposObligatorios);
            this.txtConfirmarPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NavegacionEnter_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(55, 277);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "Estado";
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.ForeColor = System.Drawing.Color.White;
            this.chkActivo.Location = new System.Drawing.Point(257, 272);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(78, 24);
            this.chkActivo.TabIndex = 5;
            this.chkActivo.Text = "Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            this.chkActivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NavegacionEnter_KeyDown);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancelar.Location = new System.Drawing.Point(339, 545);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(128, 32);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmRegistrarUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(800, 589);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.chkActivo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtConfirmarPassword);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblPreguntaSeguridad);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRol);
            this.Controls.Add(this.txtRespuesta);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtNombreCompleto);
            this.Name = "frmRegistrarUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRegistrarUsuario";
            this.Shown += new System.EventHandler(this.frmRegistrarUsuario_Shown);
            this.TextChanged += new System.EventHandler(this.VerificarCamposObligatorios);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NavegacionEnter_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombreCompleto;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtRespuesta;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPreguntaSeguridad;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtConfirmarPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkActivo;
        public System.Windows.Forms.Button btnCancelar;
    }
}