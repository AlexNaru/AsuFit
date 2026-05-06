namespace AsuFit.Presentacion
{
    partial class frmConfiguracion
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
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txtRutaDestino = new System.Windows.Forms.TextBox();
            this.btnGenerarBackup = new System.Windows.Forms.Button();
            this.lblUltimoRespaldo = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.nudDiasAviso2 = new System.Windows.Forms.NumericUpDown();
            this.nudDiasAviso1 = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnPruebaCorreo = new System.Windows.Forms.Button();
            this.txtContrasenaCorreo = new System.Windows.Forms.TextBox();
            this.txtCorreoEmisor = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnGuardarCambios = new System.Windows.Forms.Button();
            this.btnSubirLogo = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtRUC = new System.Windows.Forms.TextBox();
            this.txtNombreGimnasio = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnGuardarNotificaciones = new System.Windows.Forms.Button();
            this.btnCancelarNotificaciones = new System.Windows.Forms.Button();
            this.tabPage6.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiasAviso2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiasAviso1)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.txtRutaDestino);
            this.tabPage6.Controls.Add(this.btnGenerarBackup);
            this.tabPage6.Controls.Add(this.lblUltimoRespaldo);
            this.tabPage6.Controls.Add(this.label9);
            this.tabPage6.Controls.Add(this.btnExaminar);
            this.tabPage6.Controls.Add(this.label7);
            this.tabPage6.Controls.Add(this.label6);
            this.tabPage6.Controls.Add(this.label5);
            this.tabPage6.Location = new System.Drawing.Point(4, 29);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(592, 425);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Sistema y Respaldos";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // txtRutaDestino
            // 
            this.txtRutaDestino.Location = new System.Drawing.Point(55, 230);
            this.txtRutaDestino.Name = "txtRutaDestino";
            this.txtRutaDestino.ReadOnly = true;
            this.txtRutaDestino.Size = new System.Drawing.Size(287, 26);
            this.txtRutaDestino.TabIndex = 9;
            // 
            // btnGenerarBackup
            // 
            this.btnGenerarBackup.Location = new System.Drawing.Point(124, 370);
            this.btnGenerarBackup.Name = "btnGenerarBackup";
            this.btnGenerarBackup.Size = new System.Drawing.Size(295, 37);
            this.btnGenerarBackup.TabIndex = 8;
            this.btnGenerarBackup.Text = "💾 GENERAR BACKUP AHORA";
            this.btnGenerarBackup.UseVisualStyleBackColor = true;
            this.btnGenerarBackup.Click += new System.EventHandler(this.btnGenerarBackup_Click);
            // 
            // lblUltimoRespaldo
            // 
            this.lblUltimoRespaldo.AutoSize = true;
            this.lblUltimoRespaldo.Location = new System.Drawing.Point(248, 291);
            this.lblUltimoRespaldo.Name = "lblUltimoRespaldo";
            this.lblUltimoRespaldo.Size = new System.Drawing.Size(160, 20);
            this.lblUltimoRespaldo.TabIndex = 7;
            this.lblUltimoRespaldo.Text = "Nunca / Desconocido";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(51, 291);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(191, 20);
            this.label9.TabIndex = 6;
            this.label9.Text = "Último respaldo realizado:";
            // 
            // btnExaminar
            // 
            this.btnExaminar.Location = new System.Drawing.Point(358, 226);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(130, 35);
            this.btnExaminar.TabIndex = 5;
            this.btnExaminar.Text = "📁 EXAMINAR";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Ruta de destino:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(530, 40);
            this.label6.TabIndex = 2;
            this.label6.Text = "Seleccioná la carpeta donde deseás guardar el archivo de respaldo (.bak).\nSe reco" +
    "mienda usar un pendrive o un disco externo.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(329, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "GENERAR COPIA DE SEGURIDAD LOCAL";
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(592, 425);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Operativo";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnCancelarNotificaciones);
            this.tabPage3.Controls.Add(this.btnGuardarNotificaciones);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.label16);
            this.tabPage3.Controls.Add(this.nudDiasAviso2);
            this.tabPage3.Controls.Add(this.nudDiasAviso1);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.btnPruebaCorreo);
            this.tabPage3.Controls.Add(this.txtContrasenaCorreo);
            this.tabPage3.Controls.Add(this.txtCorreoEmisor);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(592, 451);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Notificaciones";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(49, 244);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(396, 20);
            this.label17.TabIndex = 14;
            this.label17.Text = "2. PROGRAMACIÓN DE AVISO DE VENCIMIENTOS:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(45, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(353, 20);
            this.label16.TabIndex = 13;
            this.label16.Text = "1. CONFIGURACIÓN DE CORREO SALIENTE: ";
            // 
            // nudDiasAviso2
            // 
            this.nudDiasAviso2.Location = new System.Drawing.Point(356, 348);
            this.nudDiasAviso2.Name = "nudDiasAviso2";
            this.nudDiasAviso2.Size = new System.Drawing.Size(44, 26);
            this.nudDiasAviso2.TabIndex = 12;
            this.nudDiasAviso2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudDiasAviso1
            // 
            this.nudDiasAviso1.Location = new System.Drawing.Point(356, 298);
            this.nudDiasAviso1.Name = "nudDiasAviso1";
            this.nudDiasAviso1.Size = new System.Drawing.Size(44, 26);
            this.nudDiasAviso1.TabIndex = 11;
            this.nudDiasAviso1.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(433, 348);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(143, 20);
            this.label15.TabIndex = 10;
            this.label15.Text = "día antes del corte.";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(433, 305);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(151, 20);
            this.label14.TabIndex = 9;
            this.label14.Text = "días antes del corte.";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(45, 348);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(235, 20);
            this.label13.TabIndex = 6;
            this.label13.Text = "Días para el 2° Aviso (Urgencia):";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(45, 305);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(256, 20);
            this.label12.TabIndex = 5;
            this.label12.Text = "Días para el 1° Aviso (Preparación):";
            // 
            // btnPruebaCorreo
            // 
            this.btnPruebaCorreo.Location = new System.Drawing.Point(129, 179);
            this.btnPruebaCorreo.Name = "btnPruebaCorreo";
            this.btnPruebaCorreo.Size = new System.Drawing.Size(271, 35);
            this.btnPruebaCorreo.TabIndex = 4;
            this.btnPruebaCorreo.Text = "ENVIAR CORREO DE PRUEBA";
            this.btnPruebaCorreo.UseVisualStyleBackColor = true;
            this.btnPruebaCorreo.Click += new System.EventHandler(this.btnPruebaCorreo_Click);
            // 
            // txtContrasenaCorreo
            // 
            this.txtContrasenaCorreo.Location = new System.Drawing.Point(241, 110);
            this.txtContrasenaCorreo.Name = "txtContrasenaCorreo";
            this.txtContrasenaCorreo.PasswordChar = '*';
            this.txtContrasenaCorreo.Size = new System.Drawing.Size(264, 26);
            this.txtContrasenaCorreo.TabIndex = 3;
            // 
            // txtCorreoEmisor
            // 
            this.txtCorreoEmisor.Location = new System.Drawing.Point(241, 69);
            this.txtCorreoEmisor.Name = "txtCorreoEmisor";
            this.txtCorreoEmisor.Size = new System.Drawing.Size(264, 26);
            this.txtCorreoEmisor.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(45, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(155, 20);
            this.label11.TabIndex = 1;
            this.label11.Text = "Contraseña de App: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(45, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "Correo Emisor:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(592, 425);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Facturación";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnGuardarCambios);
            this.tabPage1.Controls.Add(this.btnSubirLogo);
            this.tabPage1.Controls.Add(this.picLogo);
            this.tabPage1.Controls.Add(this.txtDireccion);
            this.tabPage1.Controls.Add(this.txtTelefono);
            this.tabPage1.Controls.Add(this.txtRUC);
            this.tabPage1.Controls.Add(this.txtNombreGimnasio);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(592, 451);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Empresa";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnGuardarCambios
            // 
            this.btnGuardarCambios.Location = new System.Drawing.Point(180, 378);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(189, 41);
            this.btnGuardarCambios.TabIndex = 11;
            this.btnGuardarCambios.Text = "GUARDAR CAMBIOS";
            this.btnGuardarCambios.UseVisualStyleBackColor = true;
            this.btnGuardarCambios.Click += new System.EventHandler(this.btnGuardarCambios_Click);
            // 
            // btnSubirLogo
            // 
            this.btnSubirLogo.Location = new System.Drawing.Point(340, 248);
            this.btnSubirLogo.Name = "btnSubirLogo";
            this.btnSubirLogo.Size = new System.Drawing.Size(132, 46);
            this.btnSubirLogo.TabIndex = 10;
            this.btnSubirLogo.Text = "SUBIR LOGO";
            this.btnSubirLogo.UseVisualStyleBackColor = true;
            this.btnSubirLogo.Click += new System.EventHandler(this.btnSubirLogo_Click);
            // 
            // picLogo
            // 
            this.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLogo.Location = new System.Drawing.Point(340, 100);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(132, 117);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 9;
            this.picLogo.TabStop = false;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(51, 297);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(180, 26);
            this.txtDireccion.TabIndex = 8;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(51, 220);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(180, 26);
            this.txtTelefono.TabIndex = 7;
            // 
            // txtRUC
            // 
            this.txtRUC.Location = new System.Drawing.Point(51, 146);
            this.txtRUC.Name = "txtRUC";
            this.txtRUC.Size = new System.Drawing.Size(180, 26);
            this.txtRUC.TabIndex = 6;
            // 
            // txtNombreGimnasio
            // 
            this.txtNombreGimnasio.Location = new System.Drawing.Point(51, 77);
            this.txtNombreGimnasio.Name = "txtNombreGimnasio";
            this.txtNombreGimnasio.Size = new System.Drawing.Size(180, 26);
            this.txtNombreGimnasio.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(47, 123);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "RUC:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Teléfono / WhatsApp:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 274);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Dirección:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Logo del Sistema:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre del Gimnasio:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(600, 484);
            this.tabControl1.TabIndex = 0;
            // 
            // btnGuardarNotificaciones
            // 
            this.btnGuardarNotificaciones.Location = new System.Drawing.Point(84, 401);
            this.btnGuardarNotificaciones.Name = "btnGuardarNotificaciones";
            this.btnGuardarNotificaciones.Size = new System.Drawing.Size(116, 42);
            this.btnGuardarNotificaciones.TabIndex = 15;
            this.btnGuardarNotificaciones.Text = "GUARDAR";
            this.btnGuardarNotificaciones.UseVisualStyleBackColor = true;
            this.btnGuardarNotificaciones.Click += new System.EventHandler(this.btnGuardarNotificaciones_Click);
            // 
            // btnCancelarNotificaciones
            // 
            this.btnCancelarNotificaciones.Location = new System.Drawing.Point(313, 406);
            this.btnCancelarNotificaciones.Name = "btnCancelarNotificaciones";
            this.btnCancelarNotificaciones.Size = new System.Drawing.Size(116, 42);
            this.btnCancelarNotificaciones.TabIndex = 16;
            this.btnCancelarNotificaciones.Text = "CANCELAR";
            this.btnCancelarNotificaciones.UseVisualStyleBackColor = true;
            this.btnCancelarNotificaciones.Click += new System.EventHandler(this.btnCancelarNotificaciones_Click);
            // 
            // frmConfiguracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 484);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmConfiguracion";
            this.Text = "frmConfiguracion";
            this.Load += new System.EventHandler(this.frmConfiguracion_Load);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiasAviso2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiasAviso1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TextBox txtRutaDestino;
        private System.Windows.Forms.Button btnGenerarBackup;
        private System.Windows.Forms.Label lblUltimoRespaldo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubirLogo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtRUC;
        private System.Windows.Forms.TextBox txtNombreGimnasio;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnPruebaCorreo;
        private System.Windows.Forms.TextBox txtContrasenaCorreo;
        private System.Windows.Forms.TextBox txtCorreoEmisor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudDiasAviso2;
        private System.Windows.Forms.NumericUpDown nudDiasAviso1;
        private System.Windows.Forms.Button btnGuardarCambios;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnCancelarNotificaciones;
        private System.Windows.Forms.Button btnGuardarNotificaciones;
    }
}