namespace WindowsFormsApplication1
{
    partial class Cliente
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUserLog = new System.Windows.Forms.TextBox();
            this.textBoxPasswordLog1 = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPasswordLog2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonLogIn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelNombre = new System.Windows.Forms.Label();
            this.buttonSingUp = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonDesconectar = new System.Windows.Forms.Button();
            this.labelInv = new System.Windows.Forms.Label();
            this.buttonDeny = new System.Windows.Forms.Button();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.textBoxInv = new System.Windows.Forms.TextBox();
            this.buttonInv = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(168, 27);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(100, 22);
            this.textBoxUsername.TabIndex = 7;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(168, 54);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(100, 22);
            this.textBoxPassword.TabIndex = 8;
            // 
            // textBoxUserLog
            // 
            this.textBoxUserLog.Location = new System.Drawing.Point(168, 60);
            this.textBoxUserLog.Name = "textBoxUserLog";
            this.textBoxUserLog.Size = new System.Drawing.Size(100, 22);
            this.textBoxUserLog.TabIndex = 9;
            // 
            // textBoxPasswordLog1
            // 
            this.textBoxPasswordLog1.Location = new System.Drawing.Point(168, 88);
            this.textBoxPasswordLog1.Name = "textBoxPasswordLog1";
            this.textBoxPasswordLog1.Size = new System.Drawing.Size(100, 22);
            this.textBoxPasswordLog1.TabIndex = 10;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(29, 30);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(125, 16);
            this.labelUsername.TabIndex = 11;
            this.labelUsername.Text = "Nombre de usuario:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Contraseña:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Nombre de usuario:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(77, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Contraseña:";
            // 
            // textBoxPasswordLog2
            // 
            this.textBoxPasswordLog2.Location = new System.Drawing.Point(168, 116);
            this.textBoxPasswordLog2.Name = "textBoxPasswordLog2";
            this.textBoxPasswordLog2.Size = new System.Drawing.Size(100, 22);
            this.textBoxPasswordLog2.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Confirmar contraseña:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonLogIn);
            this.groupBox2.Controls.Add(this.textBoxUsername);
            this.groupBox2.Controls.Add(this.labelUsername);
            this.groupBox2.Controls.Add(this.textBoxPassword);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 100);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Iniciar Sesión";
            // 
            // buttonLogIn
            // 
            this.buttonLogIn.Location = new System.Drawing.Point(274, 31);
            this.buttonLogIn.Name = "buttonLogIn";
            this.buttonLogIn.Size = new System.Drawing.Size(106, 45);
            this.buttonLogIn.TabIndex = 19;
            this.buttonLogIn.Text = "Iniciar Sesión";
            this.buttonLogIn.UseVisualStyleBackColor = true;
            this.buttonLogIn.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxName);
            this.groupBox3.Controls.Add(this.labelNombre);
            this.groupBox3.Controls.Add(this.buttonSingUp);
            this.groupBox3.Controls.Add(this.textBoxPasswordLog1);
            this.groupBox3.Controls.Add(this.textBoxUserLog);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.textBoxPasswordLog2);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(12, 118);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(386, 149);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Registrarse";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(168, 32);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 22);
            this.textBoxName.TabIndex = 22;
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(81, 35);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(75, 16);
            this.labelNombre.TabIndex = 21;
            this.labelNombre.Text = "Tú nombre:";
            // 
            // buttonSingUp
            // 
            this.buttonSingUp.Location = new System.Drawing.Point(274, 60);
            this.buttonSingUp.Name = "buttonSingUp";
            this.buttonSingUp.Size = new System.Drawing.Size(106, 45);
            this.buttonSingUp.TabIndex = 20;
            this.buttonSingUp.Text = "Registrarse";
            this.buttonSingUp.UseVisualStyleBackColor = true;
            this.buttonSingUp.Click += new System.EventHandler(this.buttonSingUp_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(416, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 57;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(370, 251);
            this.dataGridView1.TabIndex = 23;
            // 
            // buttonDesconectar
            // 
            this.buttonDesconectar.Enabled = false;
            this.buttonDesconectar.Location = new System.Drawing.Point(666, 285);
            this.buttonDesconectar.Name = "buttonDesconectar";
            this.buttonDesconectar.Size = new System.Drawing.Size(120, 40);
            this.buttonDesconectar.TabIndex = 24;
            this.buttonDesconectar.Text = "Desconectar";
            this.buttonDesconectar.UseVisualStyleBackColor = true;
            this.buttonDesconectar.Click += new System.EventHandler(this.buttonDesconectar_Click);
            // 
            // labelInv
            // 
            this.labelInv.AutoSize = true;
            this.labelInv.Location = new System.Drawing.Point(120, 270);
            this.labelInv.Name = "labelInv";
            this.labelInv.Size = new System.Drawing.Size(63, 16);
            this.labelInv.TabIndex = 25;
            this.labelInv.Text = "Invitación";
            // 
            // buttonDeny
            // 
            this.buttonDeny.Enabled = false;
            this.buttonDeny.Location = new System.Drawing.Point(44, 289);
            this.buttonDeny.Name = "buttonDeny";
            this.buttonDeny.Size = new System.Drawing.Size(99, 36);
            this.buttonDeny.TabIndex = 26;
            this.buttonDeny.Text = "Rechazar";
            this.buttonDeny.UseVisualStyleBackColor = true;
            this.buttonDeny.Click += new System.EventHandler(this.buttonDeny_Click);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Enabled = false;
            this.buttonAccept.Location = new System.Drawing.Point(149, 289);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(99, 36);
            this.buttonAccept.TabIndex = 27;
            this.buttonAccept.Text = "Aceptar";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // textBoxInv
            // 
            this.textBoxInv.Location = new System.Drawing.Point(372, 287);
            this.textBoxInv.Name = "textBoxInv";
            this.textBoxInv.Size = new System.Drawing.Size(146, 22);
            this.textBoxInv.TabIndex = 28;
            // 
            // buttonInv
            // 
            this.buttonInv.Enabled = false;
            this.buttonInv.Location = new System.Drawing.Point(407, 315);
            this.buttonInv.Name = "buttonInv";
            this.buttonInv.Size = new System.Drawing.Size(75, 23);
            this.buttonInv.TabIndex = 29;
            this.buttonInv.Text = "Invitar";
            this.buttonInv.UseVisualStyleBackColor = true;
            this.buttonInv.Click += new System.EventHandler(this.buttonInv_Click);
            // 
            // Cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 350);
            this.Controls.Add(this.buttonInv);
            this.Controls.Add(this.textBoxInv);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.buttonDeny);
            this.Controls.Add(this.labelInv);
            this.Controls.Add(this.buttonDesconectar);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Cliente";
            this.Text = "Cliente";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUserLog;
        private System.Windows.Forms.TextBox textBoxPasswordLog1;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxPasswordLog2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonLogIn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonSingUp;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonDesconectar;
        private System.Windows.Forms.Label labelInv;
        private System.Windows.Forms.Button buttonDeny;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.TextBox textBoxInv;
        private System.Windows.Forms.Button buttonInv;
    }
}

