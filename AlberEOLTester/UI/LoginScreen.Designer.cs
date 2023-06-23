namespace AlberEOL.UI
{
    partial class LoginScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginScreen));
            this.SoftwareVersion = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LoginMSG = new System.Windows.Forms.Label();
            this.Header = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_PW1 = new AlberEOL.DesignElements.InterfaceTextBox();
            this.txt_UN1 = new AlberEOL.DesignElements.InterfaceTextBox();
            this.btn_Login = new AlberEOL.DesignElements.InterfaceButton();
            this.btn_Exit = new AlberEOL.DesignElements.InterfaceButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // SoftwareVersion
            // 
            this.SoftwareVersion.AutoSize = true;
            this.SoftwareVersion.BackColor = System.Drawing.Color.Transparent;
            this.SoftwareVersion.Font = new System.Drawing.Font("Mohave", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SoftwareVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.SoftwareVersion.Location = new System.Drawing.Point(3, 482);
            this.SoftwareVersion.Name = "SoftwareVersion";
            this.SoftwareVersion.Size = new System.Drawing.Size(60, 18);
            this.SoftwareVersion.TabIndex = 51;
            this.SoftwareVersion.Text = "ver.1.0.0.1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Mohave", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.label7.Location = new System.Drawing.Point(201, 482);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 18);
            this.label7.TabIndex = 52;
            this.label7.Text = "VIDEOTON Elektro-PLAST KFT.";
            // 
            // LoginMSG
            // 
            this.LoginMSG.BackColor = System.Drawing.Color.Transparent;
            this.LoginMSG.Font = new System.Drawing.Font("Oswald", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LoginMSG.ForeColor = System.Drawing.Color.Yellow;
            this.LoginMSG.Location = new System.Drawing.Point(51, 337);
            this.LoginMSG.Name = "LoginMSG";
            this.LoginMSG.Size = new System.Drawing.Size(250, 59);
            this.LoginMSG.TabIndex = 58;
            this.LoginMSG.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.Transparent;
            this.Header.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Header.Font = new System.Drawing.Font("Mohave", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.Header.Location = new System.Drawing.Point(0, 94);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(351, 34);
            this.Header.TabIndex = 57;
            this.Header.Text = "LOGIN PANEL";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::AlberEOL.Properties.Resources.vtep_logo_medium;
            this.pictureBox1.Location = new System.Drawing.Point(117, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(117, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 59;
            this.pictureBox1.TabStop = false;
            // 
            // txt_PW1
            // 
            this.txt_PW1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(20)))), ((int)(((byte)(26)))));
            this.txt_PW1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.txt_PW1.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(93)))), ((int)(((byte)(151)))));
            this.txt_PW1.BorderRadius = 0;
            this.txt_PW1.BorderSize = 2;
            this.txt_PW1.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txt_PW1.ForeColor = System.Drawing.Color.DimGray;
            this.txt_PW1.Location = new System.Drawing.Point(50, 218);
            this.txt_PW1.Margin = new System.Windows.Forms.Padding(4);
            this.txt_PW1.Multiline = false;
            this.txt_PW1.Name = "txt_PW1";
            this.txt_PW1.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txt_PW1.PasswordChar = true;
            this.txt_PW1.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(87)))), ((int)(((byte)(90)))));
            this.txt_PW1.PlaceholderText = "Jelszó";
            this.txt_PW1.ReadOnly = false;
            this.txt_PW1.Size = new System.Drawing.Size(250, 42);
            this.txt_PW1.TabIndex = 43;
            this.txt_PW1.Texts = "";
            this.txt_PW1.UnderlinedStyle = false;
            this.txt_PW1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_PW1_KeyDown);
            this.txt_PW1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_PW1_KeyPress);
            // 
            // txt_UN1
            // 
            this.txt_UN1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(20)))), ((int)(((byte)(26)))));
            this.txt_UN1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.txt_UN1.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(93)))), ((int)(((byte)(151)))));
            this.txt_UN1.BorderRadius = 0;
            this.txt_UN1.BorderSize = 2;
            this.txt_UN1.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txt_UN1.ForeColor = System.Drawing.Color.DimGray;
            this.txt_UN1.Location = new System.Drawing.Point(50, 168);
            this.txt_UN1.Margin = new System.Windows.Forms.Padding(4);
            this.txt_UN1.Multiline = false;
            this.txt_UN1.Name = "txt_UN1";
            this.txt_UN1.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txt_UN1.PasswordChar = false;
            this.txt_UN1.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(87)))), ((int)(((byte)(90)))));
            this.txt_UN1.PlaceholderText = "Felhasználónév";
            this.txt_UN1.ReadOnly = false;
            this.txt_UN1.Size = new System.Drawing.Size(250, 42);
            this.txt_UN1.TabIndex = 42;
            this.txt_UN1.Texts = "";
            this.txt_UN1.UnderlinedStyle = false;
            // 
            // btn_Login
            // 
            this.btn_Login.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btn_Login.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.btn_Login.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_Login.BorderRadius = 0;
            this.btn_Login.BorderSize = 0;
            this.btn_Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Login.FlatAppearance.BorderSize = 0;
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Login.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.btn_Login.ForeColor = System.Drawing.Color.White;
            this.btn_Login.Location = new System.Drawing.Point(50, 278);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(250, 40);
            this.btn_Login.TabIndex = 44;
            this.btn_Login.Text = "BEJELENTKEZÉS";
            this.btn_Login.TextColor = System.Drawing.Color.White;
            this.btn_Login.UseVisualStyleBackColor = false;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btn_Exit.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.btn_Exit.BackgroundImage = global::AlberEOL.Properties.Resources.exit_24;
            this.btn_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Exit.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_Exit.BorderRadius = 0;
            this.btn_Exit.BorderSize = 0;
            this.btn_Exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Exit.FlatAppearance.BorderSize = 0;
            this.btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Exit.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.btn_Exit.ForeColor = System.Drawing.Color.White;
            this.btn_Exit.Location = new System.Drawing.Point(165, 458);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(30, 30);
            this.btn_Exit.TabIndex = 60;
            this.btn_Exit.TextColor = System.Drawing.Color.White;
            this.btn_Exit.UseVisualStyleBackColor = false;
            // 
            // LoginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AlberEOL.Properties.Resources.loginBG;
            this.ClientSize = new System.Drawing.Size(350, 500);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.txt_PW1);
            this.Controls.Add(this.txt_UN1);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.LoginMSG);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SoftwareVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Panel";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SoftwareVersion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LoginMSG;
        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DesignElements.InterfaceButton btn_Login;
        private DesignElements.InterfaceTextBox txt_UN1;
        private DesignElements.InterfaceTextBox txt_PW1;
        private DesignElements.InterfaceButton btn_Exit;
    }
}