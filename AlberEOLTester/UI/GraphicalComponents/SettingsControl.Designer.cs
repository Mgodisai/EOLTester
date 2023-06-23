namespace AlberEOL.UI.GraphicalComponents
{
    partial class SettingsControl
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
            this.components = new System.ComponentModel.Container();
            this.TestStepsListBox = new System.Windows.Forms.CheckedListBox();
            this.RelayChargeButton = new System.Windows.Forms.Button();
            this.RelayTwoButton = new System.Windows.Forms.Button();
            this.RelayOneButton = new System.Windows.Forms.Button();
            this.RelayHalfButton = new System.Windows.Forms.Button();
            this.RelaysGroupBox = new System.Windows.Forms.GroupBox();
            this.TestStepsGroupBox = new System.Windows.Forms.GroupBox();
            this.AllFETsOFFButton = new System.Windows.Forms.Button();
            this.AllFETsOnButton = new System.Windows.Forms.Button();
            this.FETSGroupBox = new System.Windows.Forms.GroupBox();
            this.DischargeFETOffButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ServiceGroupBox = new System.Windows.Forms.GroupBox();
            this.DMGroupBox = new System.Windows.Forms.GroupBox();
            this.ShutdownButton = new System.Windows.Forms.Button();
            this.FDOffButton = new System.Windows.Forms.Button();
            this.DMResetButton = new System.Windows.Forms.Button();
            this.DMReadAllButton = new System.Windows.Forms.Button();
            this.StopCPXButton = new AlberEOL.DesignElements.InterfaceButton();
            this.Charge3AButton = new AlberEOL.DesignElements.InterfaceButton();
            this.TimerButton = new System.Windows.Forms.CheckBox();
            this.StandAloneButton = new AlberEOL.DesignElements.InterfaceButton();
            this.ManualButton = new AlberEOL.DesignElements.InterfaceButton();
            this.OperationModeTextBox = new AlberEOL.DesignElements.InterfaceTextBox();
            this.ServiceTimer = new System.Windows.Forms.Timer(this.components);
            this.RelaysGroupBox.SuspendLayout();
            this.TestStepsGroupBox.SuspendLayout();
            this.FETSGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ServiceGroupBox.SuspendLayout();
            this.DMGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TestStepsListBox
            // 
            this.TestStepsListBox.FormattingEnabled = true;
            this.TestStepsListBox.Location = new System.Drawing.Point(5, 24);
            this.TestStepsListBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TestStepsListBox.Name = "TestStepsListBox";
            this.TestStepsListBox.Size = new System.Drawing.Size(184, 361);
            this.TestStepsListBox.TabIndex = 60;
            this.TestStepsListBox.SelectedValueChanged += new System.EventHandler(this.TestStepsListBox_SelectedValueChanged);
            // 
            // RelayChargeButton
            // 
            this.RelayChargeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.RelayChargeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RelayChargeButton.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.RelayChargeButton.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.RelayChargeButton.Location = new System.Drawing.Point(5, 144);
            this.RelayChargeButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RelayChargeButton.Name = "RelayChargeButton";
            this.RelayChargeButton.Size = new System.Drawing.Size(114, 36);
            this.RelayChargeButton.TabIndex = 71;
            this.RelayChargeButton.Text = "Charge";
            this.RelayChargeButton.UseVisualStyleBackColor = false;
            this.RelayChargeButton.Click += new System.EventHandler(this.RelayChargeButton_Click);
            // 
            // RelayTwoButton
            // 
            this.RelayTwoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.RelayTwoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RelayTwoButton.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.RelayTwoButton.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.RelayTwoButton.Location = new System.Drawing.Point(5, 24);
            this.RelayTwoButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RelayTwoButton.Name = "RelayTwoButton";
            this.RelayTwoButton.Size = new System.Drawing.Size(114, 36);
            this.RelayTwoButton.TabIndex = 72;
            this.RelayTwoButton.Text = "2 Ω";
            this.RelayTwoButton.UseVisualStyleBackColor = false;
            this.RelayTwoButton.Click += new System.EventHandler(this.RelayTwoButton_Click);
            // 
            // RelayOneButton
            // 
            this.RelayOneButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.RelayOneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RelayOneButton.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.RelayOneButton.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.RelayOneButton.Location = new System.Drawing.Point(5, 64);
            this.RelayOneButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RelayOneButton.Name = "RelayOneButton";
            this.RelayOneButton.Size = new System.Drawing.Size(114, 36);
            this.RelayOneButton.TabIndex = 73;
            this.RelayOneButton.Text = "1 Ω";
            this.RelayOneButton.UseVisualStyleBackColor = false;
            this.RelayOneButton.Click += new System.EventHandler(this.RelayOneButton_Click);
            // 
            // RelayHalfButton
            // 
            this.RelayHalfButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.RelayHalfButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RelayHalfButton.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.RelayHalfButton.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.RelayHalfButton.Location = new System.Drawing.Point(5, 104);
            this.RelayHalfButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RelayHalfButton.Name = "RelayHalfButton";
            this.RelayHalfButton.Size = new System.Drawing.Size(114, 36);
            this.RelayHalfButton.TabIndex = 74;
            this.RelayHalfButton.Text = "0,5 Ω";
            this.RelayHalfButton.UseVisualStyleBackColor = false;
            this.RelayHalfButton.Click += new System.EventHandler(this.RelayHalfButton_Click);
            // 
            // RelaysGroupBox
            // 
            this.RelaysGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.RelaysGroupBox.Controls.Add(this.RelayTwoButton);
            this.RelaysGroupBox.Controls.Add(this.RelayChargeButton);
            this.RelaysGroupBox.Controls.Add(this.RelayHalfButton);
            this.RelaysGroupBox.Controls.Add(this.RelayOneButton);
            this.RelaysGroupBox.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.RelaysGroupBox.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.RelaysGroupBox.Location = new System.Drawing.Point(492, 29);
            this.RelaysGroupBox.Name = "RelaysGroupBox";
            this.RelaysGroupBox.Size = new System.Drawing.Size(125, 188);
            this.RelaysGroupBox.TabIndex = 75;
            this.RelaysGroupBox.TabStop = false;
            this.RelaysGroupBox.Text = "RELAYS";
            // 
            // TestStepsGroupBox
            // 
            this.TestStepsGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.TestStepsGroupBox.Controls.Add(this.TestStepsListBox);
            this.TestStepsGroupBox.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.TestStepsGroupBox.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.TestStepsGroupBox.Location = new System.Drawing.Point(21, 29);
            this.TestStepsGroupBox.Name = "TestStepsGroupBox";
            this.TestStepsGroupBox.Size = new System.Drawing.Size(194, 390);
            this.TestStepsGroupBox.TabIndex = 76;
            this.TestStepsGroupBox.TabStop = false;
            this.TestStepsGroupBox.Text = "TEST STEPS";
            // 
            // AllFETsOFFButton
            // 
            this.AllFETsOFFButton.Location = new System.Drawing.Point(6, 65);
            this.AllFETsOFFButton.Name = "AllFETsOFFButton";
            this.AllFETsOFFButton.Size = new System.Drawing.Size(101, 34);
            this.AllFETsOFFButton.TabIndex = 77;
            this.AllFETsOFFButton.Text = "ALL FETS OFF";
            this.AllFETsOFFButton.UseVisualStyleBackColor = true;
            this.AllFETsOFFButton.Click += new System.EventHandler(this.btnALL_FETS_OFF_Click);
            // 
            // AllFETsOnButton
            // 
            this.AllFETsOnButton.Location = new System.Drawing.Point(6, 25);
            this.AllFETsOnButton.Name = "AllFETsOnButton";
            this.AllFETsOnButton.Size = new System.Drawing.Size(101, 34);
            this.AllFETsOnButton.TabIndex = 78;
            this.AllFETsOnButton.Text = "ALL FETS ON";
            this.AllFETsOnButton.UseVisualStyleBackColor = true;
            this.AllFETsOnButton.Click += new System.EventHandler(this.btnALL_FETS_ON_Click);
            // 
            // FETSGroupBox
            // 
            this.FETSGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.FETSGroupBox.Controls.Add(this.DischargeFETOffButton);
            this.FETSGroupBox.Controls.Add(this.AllFETsOFFButton);
            this.FETSGroupBox.Controls.Add(this.AllFETsOnButton);
            this.FETSGroupBox.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.FETSGroupBox.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.FETSGroupBox.Location = new System.Drawing.Point(361, 29);
            this.FETSGroupBox.Name = "FETSGroupBox";
            this.FETSGroupBox.Size = new System.Drawing.Size(125, 188);
            this.FETSGroupBox.TabIndex = 79;
            this.FETSGroupBox.TabStop = false;
            this.FETSGroupBox.Text = "FETs";
            // 
            // DischargeFETOffButton
            // 
            this.DischargeFETOffButton.Location = new System.Drawing.Point(6, 106);
            this.DischargeFETOffButton.Name = "DischargeFETOffButton";
            this.DischargeFETOffButton.Size = new System.Drawing.Size(101, 34);
            this.DischargeFETOffButton.TabIndex = 80;
            this.DischargeFETOffButton.Text = "DSG FET OFF";
            this.DischargeFETOffButton.UseVisualStyleBackColor = true;
            this.DischargeFETOffButton.Click += new System.EventHandler(this.DischargeFETOffButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::AlberEOL.Properties.Resources.settings_white;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Location = new System.Drawing.Point(711, 494);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 80;
            this.pictureBox1.TabStop = false;
            // 
            // ServiceGroupBox
            // 
            this.ServiceGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.ServiceGroupBox.Controls.Add(this.DMGroupBox);
            this.ServiceGroupBox.Controls.Add(this.StopCPXButton);
            this.ServiceGroupBox.Controls.Add(this.Charge3AButton);
            this.ServiceGroupBox.Controls.Add(this.TimerButton);
            this.ServiceGroupBox.Controls.Add(this.FETSGroupBox);
            this.ServiceGroupBox.Controls.Add(this.TestStepsGroupBox);
            this.ServiceGroupBox.Controls.Add(this.RelaysGroupBox);
            this.ServiceGroupBox.Controls.Add(this.StandAloneButton);
            this.ServiceGroupBox.Controls.Add(this.ManualButton);
            this.ServiceGroupBox.Controls.Add(this.OperationModeTextBox);
            this.ServiceGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServiceGroupBox.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.ServiceGroupBox.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.ServiceGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ServiceGroupBox.Name = "ServiceGroupBox";
            this.ServiceGroupBox.Size = new System.Drawing.Size(660, 554);
            this.ServiceGroupBox.TabIndex = 81;
            this.ServiceGroupBox.TabStop = false;
            this.ServiceGroupBox.Text = "SERVICE";
            this.ServiceGroupBox.Leave += new System.EventHandler(this.ServiceGroupBox_Leave);
            // 
            // DMGroupBox
            // 
            this.DMGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.DMGroupBox.Controls.Add(this.ShutdownButton);
            this.DMGroupBox.Controls.Add(this.FDOffButton);
            this.DMGroupBox.Controls.Add(this.DMResetButton);
            this.DMGroupBox.Controls.Add(this.DMReadAllButton);
            this.DMGroupBox.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.DMGroupBox.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.DMGroupBox.Location = new System.Drawing.Point(221, 29);
            this.DMGroupBox.Name = "DMGroupBox";
            this.DMGroupBox.Size = new System.Drawing.Size(125, 188);
            this.DMGroupBox.TabIndex = 83;
            this.DMGroupBox.TabStop = false;
            this.DMGroupBox.Text = "DataMemory";
            // 
            // ShutdownButton
            // 
            this.ShutdownButton.Location = new System.Drawing.Point(6, 148);
            this.ShutdownButton.Name = "ShutdownButton";
            this.ShutdownButton.Size = new System.Drawing.Size(101, 34);
            this.ShutdownButton.TabIndex = 81;
            this.ShutdownButton.Text = "SHUTDOWN";
            this.ShutdownButton.UseVisualStyleBackColor = true;
            this.ShutdownButton.Click += new System.EventHandler(this.ShutdownButton_Click);
            // 
            // FDOffButton
            // 
            this.FDOffButton.Location = new System.Drawing.Point(6, 106);
            this.FDOffButton.Name = "FDOffButton";
            this.FDOffButton.Size = new System.Drawing.Size(101, 34);
            this.FDOffButton.TabIndex = 80;
            this.FDOffButton.Text = "FD Off";
            this.FDOffButton.UseVisualStyleBackColor = true;
            this.FDOffButton.Click += new System.EventHandler(this.FDOffButton_Click);
            // 
            // DMResetButton
            // 
            this.DMResetButton.Location = new System.Drawing.Point(6, 65);
            this.DMResetButton.Name = "DMResetButton";
            this.DMResetButton.Size = new System.Drawing.Size(101, 34);
            this.DMResetButton.TabIndex = 77;
            this.DMResetButton.Text = "Reset";
            this.DMResetButton.UseVisualStyleBackColor = true;
            this.DMResetButton.Click += new System.EventHandler(this.DMResetButton_Click);
            // 
            // DMReadAllButton
            // 
            this.DMReadAllButton.Location = new System.Drawing.Point(6, 25);
            this.DMReadAllButton.Name = "DMReadAllButton";
            this.DMReadAllButton.Size = new System.Drawing.Size(101, 34);
            this.DMReadAllButton.TabIndex = 78;
            this.DMReadAllButton.Text = "ReadAll";
            this.DMReadAllButton.UseVisualStyleBackColor = true;
            this.DMReadAllButton.Click += new System.EventHandler(this.DMReadAllButton_Click);
            // 
            // StopCPXButton
            // 
            this.StopCPXButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.StopCPXButton.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.StopCPXButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.StopCPXButton.BorderRadius = 0;
            this.StopCPXButton.BorderSize = 0;
            this.StopCPXButton.FlatAppearance.BorderSize = 0;
            this.StopCPXButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopCPXButton.ForeColor = System.Drawing.Color.White;
            this.StopCPXButton.Location = new System.Drawing.Point(273, 257);
            this.StopCPXButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StopCPXButton.Name = "StopCPXButton";
            this.StopCPXButton.Size = new System.Drawing.Size(166, 65);
            this.StopCPXButton.TabIndex = 82;
            this.StopCPXButton.Text = "STOP PS";
            this.StopCPXButton.TextColor = System.Drawing.Color.White;
            this.StopCPXButton.UseVisualStyleBackColor = false;
            this.StopCPXButton.Click += new System.EventHandler(this.StopCPXButton_Click);
            // 
            // Charge3AButton
            // 
            this.Charge3AButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.Charge3AButton.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.Charge3AButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.Charge3AButton.BorderRadius = 0;
            this.Charge3AButton.BorderSize = 0;
            this.Charge3AButton.FlatAppearance.BorderSize = 0;
            this.Charge3AButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Charge3AButton.ForeColor = System.Drawing.Color.White;
            this.Charge3AButton.Location = new System.Drawing.Point(450, 257);
            this.Charge3AButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Charge3AButton.Name = "Charge3AButton";
            this.Charge3AButton.Size = new System.Drawing.Size(166, 65);
            this.Charge3AButton.TabIndex = 81;
            this.Charge3AButton.Text = "PowerSupply 3A";
            this.Charge3AButton.TextColor = System.Drawing.Color.White;
            this.Charge3AButton.UseVisualStyleBackColor = false;
            this.Charge3AButton.Click += new System.EventHandler(this.Charge3AButton_Click);
            // 
            // TimerButton
            // 
            this.TimerButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.TimerButton.AutoSize = true;
            this.TimerButton.BackColor = System.Drawing.Color.IndianRed;
            this.TimerButton.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TimerButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TimerButton.FlatAppearance.BorderSize = 0;
            this.TimerButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.TimerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TimerButton.ForeColor = System.Drawing.Color.Black;
            this.TimerButton.Location = new System.Drawing.Point(525, 479);
            this.TimerButton.Name = "TimerButton";
            this.TimerButton.Size = new System.Drawing.Size(92, 32);
            this.TimerButton.TabIndex = 80;
            this.TimerButton.Text = "AutoRefresh";
            this.TimerButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TimerButton.UseVisualStyleBackColor = false;
            this.TimerButton.CheckedChanged += new System.EventHandler(this.TimerButton_CheckedChanged);
            // 
            // StandAloneButton
            // 
            this.StandAloneButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.StandAloneButton.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.StandAloneButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.StandAloneButton.BorderRadius = 0;
            this.StandAloneButton.BorderSize = 0;
            this.StandAloneButton.FlatAppearance.BorderSize = 0;
            this.StandAloneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StandAloneButton.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.StandAloneButton.ForeColor = System.Drawing.Color.White;
            this.StandAloneButton.Location = new System.Drawing.Point(141, 479);
            this.StandAloneButton.Name = "StandAloneButton";
            this.StandAloneButton.Size = new System.Drawing.Size(74, 29);
            this.StandAloneButton.TabIndex = 63;
            this.StandAloneButton.Text = "StandAlone";
            this.StandAloneButton.TextColor = System.Drawing.Color.White;
            this.StandAloneButton.UseVisualStyleBackColor = false;
            this.StandAloneButton.Click += new System.EventHandler(this.StandAloneButton_Click);
            // 
            // ManualButton
            // 
            this.ManualButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ManualButton.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.ManualButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ManualButton.BorderRadius = 0;
            this.ManualButton.BorderSize = 0;
            this.ManualButton.FlatAppearance.BorderSize = 0;
            this.ManualButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ManualButton.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.ManualButton.ForeColor = System.Drawing.Color.White;
            this.ManualButton.Location = new System.Drawing.Point(21, 479);
            this.ManualButton.Name = "ManualButton";
            this.ManualButton.Size = new System.Drawing.Size(72, 29);
            this.ManualButton.TabIndex = 62;
            this.ManualButton.Text = "Manual";
            this.ManualButton.TextColor = System.Drawing.Color.White;
            this.ManualButton.UseVisualStyleBackColor = false;
            this.ManualButton.Click += new System.EventHandler(this.ManualButton_Click);
            // 
            // OperationModeTextBox
            // 
            this.OperationModeTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.OperationModeTextBox.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.OperationModeTextBox.BorderFocusColor = System.Drawing.Color.HotPink;
            this.OperationModeTextBox.BorderRadius = 0;
            this.OperationModeTextBox.BorderSize = 2;
            this.OperationModeTextBox.Font = new System.Drawing.Font("Oswald", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OperationModeTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OperationModeTextBox.Location = new System.Drawing.Point(21, 436);
            this.OperationModeTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OperationModeTextBox.Multiline = false;
            this.OperationModeTextBox.Name = "OperationModeTextBox";
            this.OperationModeTextBox.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.OperationModeTextBox.PasswordChar = false;
            this.OperationModeTextBox.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.OperationModeTextBox.PlaceholderText = "";
            this.OperationModeTextBox.ReadOnly = false;
            this.OperationModeTextBox.Size = new System.Drawing.Size(194, 37);
            this.OperationModeTextBox.TabIndex = 61;
            this.OperationModeTextBox.Texts = "as";
            this.OperationModeTextBox.UnderlinedStyle = false;
            // 
            // ServiceTimer
            // 
            this.ServiceTimer.Tick += new System.EventHandler(this.ServiceTimer_Tick);
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.ServiceGroupBox);
            this.Controls.Add(this.pictureBox1);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(660, 554);
            this.RelaysGroupBox.ResumeLayout(false);
            this.TestStepsGroupBox.ResumeLayout(false);
            this.FETSGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ServiceGroupBox.ResumeLayout(false);
            this.ServiceGroupBox.PerformLayout();
            this.DMGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckedListBox TestStepsListBox;
        private DesignElements.InterfaceTextBox OperationModeTextBox;
        private DesignElements.InterfaceButton ManualButton;
        private DesignElements.InterfaceButton StandAloneButton;
        private System.Windows.Forms.Button RelayChargeButton;
        private System.Windows.Forms.Button RelayTwoButton;
        private System.Windows.Forms.Button RelayOneButton;
        private System.Windows.Forms.Button RelayHalfButton;
        private System.Windows.Forms.GroupBox RelaysGroupBox;
        private System.Windows.Forms.GroupBox TestStepsGroupBox;
        private System.Windows.Forms.Button AllFETsOFFButton;
        private System.Windows.Forms.Button AllFETsOnButton;
        private System.Windows.Forms.GroupBox FETSGroupBox;
        private System.Windows.Forms.Button DischargeFETOffButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox ServiceGroupBox;
        private System.Windows.Forms.Timer ServiceTimer;
        private System.Windows.Forms.CheckBox TimerButton;
        private DesignElements.InterfaceButton Charge3AButton;
        private DesignElements.InterfaceButton StopCPXButton;
        private System.Windows.Forms.GroupBox DMGroupBox;
        private System.Windows.Forms.Button FDOffButton;
        private System.Windows.Forms.Button DMResetButton;
        private System.Windows.Forms.Button DMReadAllButton;
        private System.Windows.Forms.Button ShutdownButton;
    }
}