namespace AlberEOL.UI.GraphicalComponents
{
    partial class PowerSupplyDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SettingsGroup = new System.Windows.Forms.GroupBox();
            this.CurrentSettingsLabel = new System.Windows.Forms.Label();
            this.VoltageSettingsTextBox = new System.Windows.Forms.TextBox();
            this.CurrentSettingsTextBox = new System.Windows.Forms.TextBox();
            this.VoltageSettingsLabel = new System.Windows.Forms.Label();
            this.ReadBackDataGroup = new System.Windows.Forms.GroupBox();
            this.CurrentReadBackLabel = new System.Windows.Forms.Label();
            this.CurrentReadBackTextBox = new System.Windows.Forms.TextBox();
            this.VoltageReadBackLabel = new System.Windows.Forms.Label();
            this.VoltageReadBackTextBox = new System.Windows.Forms.TextBox();
            this.DeviceDataGroup = new System.Windows.Forms.GroupBox();
            this.OnStatePictureBox = new System.Windows.Forms.PictureBox();
            this.StatusPictureBox = new System.Windows.Forms.PictureBox();
            this.DeviceStatusTextBox = new System.Windows.Forms.TextBox();
            this.DeviceStatusLabel = new System.Windows.Forms.Label();
            this.DeviceAddressTextBox = new System.Windows.Forms.TextBox();
            this.DeviceAddressLabel = new System.Windows.Forms.Label();
            this.ReadBackDataWorker = new System.ComponentModel.BackgroundWorker();
            this.SettingsGroup.SuspendLayout();
            this.ReadBackDataGroup.SuspendLayout();
            this.DeviceDataGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OnStatePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SettingsGroup
            // 
            this.SettingsGroup.Controls.Add(this.CurrentSettingsLabel);
            this.SettingsGroup.Controls.Add(this.VoltageSettingsTextBox);
            this.SettingsGroup.Controls.Add(this.CurrentSettingsTextBox);
            this.SettingsGroup.Controls.Add(this.VoltageSettingsLabel);
            this.SettingsGroup.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.SettingsGroup.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.SettingsGroup.Location = new System.Drawing.Point(4, 73);
            this.SettingsGroup.Margin = new System.Windows.Forms.Padding(2);
            this.SettingsGroup.Name = "SettingsGroup";
            this.SettingsGroup.Padding = new System.Windows.Forms.Padding(2);
            this.SettingsGroup.Size = new System.Drawing.Size(120, 90);
            this.SettingsGroup.TabIndex = 0;
            this.SettingsGroup.TabStop = false;
            this.SettingsGroup.Text = "Settings";
            // 
            // CurrentSettingsLabel
            // 
            this.CurrentSettingsLabel.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.CurrentSettingsLabel.Location = new System.Drawing.Point(95, 58);
            this.CurrentSettingsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CurrentSettingsLabel.Name = "CurrentSettingsLabel";
            this.CurrentSettingsLabel.Size = new System.Drawing.Size(21, 24);
            this.CurrentSettingsLabel.TabIndex = 5;
            this.CurrentSettingsLabel.Text = "A";
            this.CurrentSettingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VoltageSettingsTextBox
            // 
            this.VoltageSettingsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.VoltageSettingsTextBox.Enabled = false;
            this.VoltageSettingsTextBox.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Bold);
            this.VoltageSettingsTextBox.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.VoltageSettingsTextBox.Location = new System.Drawing.Point(11, 25);
            this.VoltageSettingsTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.VoltageSettingsTextBox.Name = "VoltageSettingsTextBox";
            this.VoltageSettingsTextBox.Size = new System.Drawing.Size(80, 24);
            this.VoltageSettingsTextBox.TabIndex = 3;
            this.VoltageSettingsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CurrentSettingsTextBox
            // 
            this.CurrentSettingsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CurrentSettingsTextBox.Enabled = false;
            this.CurrentSettingsTextBox.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Bold);
            this.CurrentSettingsTextBox.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.CurrentSettingsTextBox.Location = new System.Drawing.Point(11, 58);
            this.CurrentSettingsTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.CurrentSettingsTextBox.Name = "CurrentSettingsTextBox";
            this.CurrentSettingsTextBox.Size = new System.Drawing.Size(80, 24);
            this.CurrentSettingsTextBox.TabIndex = 6;
            this.CurrentSettingsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // VoltageSettingsLabel
            // 
            this.VoltageSettingsLabel.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.VoltageSettingsLabel.Location = new System.Drawing.Point(95, 26);
            this.VoltageSettingsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VoltageSettingsLabel.Name = "VoltageSettingsLabel";
            this.VoltageSettingsLabel.Size = new System.Drawing.Size(21, 24);
            this.VoltageSettingsLabel.TabIndex = 4;
            this.VoltageSettingsLabel.Text = "V";
            this.VoltageSettingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ReadBackDataGroup
            // 
            this.ReadBackDataGroup.Controls.Add(this.CurrentReadBackLabel);
            this.ReadBackDataGroup.Controls.Add(this.CurrentReadBackTextBox);
            this.ReadBackDataGroup.Controls.Add(this.VoltageReadBackLabel);
            this.ReadBackDataGroup.Controls.Add(this.VoltageReadBackTextBox);
            this.ReadBackDataGroup.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.ReadBackDataGroup.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.ReadBackDataGroup.Location = new System.Drawing.Point(192, 73);
            this.ReadBackDataGroup.Margin = new System.Windows.Forms.Padding(2);
            this.ReadBackDataGroup.Name = "ReadBackDataGroup";
            this.ReadBackDataGroup.Padding = new System.Windows.Forms.Padding(2);
            this.ReadBackDataGroup.Size = new System.Drawing.Size(120, 90);
            this.ReadBackDataGroup.TabIndex = 1;
            this.ReadBackDataGroup.TabStop = false;
            this.ReadBackDataGroup.Text = "Readback";
            // 
            // CurrentReadBackLabel
            // 
            this.CurrentReadBackLabel.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.CurrentReadBackLabel.Location = new System.Drawing.Point(96, 60);
            this.CurrentReadBackLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CurrentReadBackLabel.Name = "CurrentReadBackLabel";
            this.CurrentReadBackLabel.Size = new System.Drawing.Size(20, 24);
            this.CurrentReadBackLabel.TabIndex = 2;
            this.CurrentReadBackLabel.Text = "A";
            this.CurrentReadBackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CurrentReadBackTextBox
            // 
            this.CurrentReadBackTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CurrentReadBackTextBox.Enabled = false;
            this.CurrentReadBackTextBox.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Bold);
            this.CurrentReadBackTextBox.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.CurrentReadBackTextBox.Location = new System.Drawing.Point(13, 58);
            this.CurrentReadBackTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.CurrentReadBackTextBox.Name = "CurrentReadBackTextBox";
            this.CurrentReadBackTextBox.Size = new System.Drawing.Size(80, 24);
            this.CurrentReadBackTextBox.TabIndex = 2;
            this.CurrentReadBackTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // VoltageReadBackLabel
            // 
            this.VoltageReadBackLabel.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.VoltageReadBackLabel.Location = new System.Drawing.Point(96, 26);
            this.VoltageReadBackLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VoltageReadBackLabel.Name = "VoltageReadBackLabel";
            this.VoltageReadBackLabel.Size = new System.Drawing.Size(20, 24);
            this.VoltageReadBackLabel.TabIndex = 1;
            this.VoltageReadBackLabel.Text = "V";
            this.VoltageReadBackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VoltageReadBackTextBox
            // 
            this.VoltageReadBackTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.VoltageReadBackTextBox.Enabled = false;
            this.VoltageReadBackTextBox.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Bold);
            this.VoltageReadBackTextBox.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.VoltageReadBackTextBox.Location = new System.Drawing.Point(13, 25);
            this.VoltageReadBackTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.VoltageReadBackTextBox.Name = "VoltageReadBackTextBox";
            this.VoltageReadBackTextBox.Size = new System.Drawing.Size(80, 24);
            this.VoltageReadBackTextBox.TabIndex = 0;
            this.VoltageReadBackTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // DeviceDataGroup
            // 
            this.DeviceDataGroup.Controls.Add(this.SettingsGroup);
            this.DeviceDataGroup.Controls.Add(this.OnStatePictureBox);
            this.DeviceDataGroup.Controls.Add(this.ReadBackDataGroup);
            this.DeviceDataGroup.Controls.Add(this.StatusPictureBox);
            this.DeviceDataGroup.Controls.Add(this.DeviceStatusTextBox);
            this.DeviceDataGroup.Controls.Add(this.DeviceStatusLabel);
            this.DeviceDataGroup.Controls.Add(this.DeviceAddressTextBox);
            this.DeviceDataGroup.Controls.Add(this.DeviceAddressLabel);
            this.DeviceDataGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeviceDataGroup.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.DeviceDataGroup.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.DeviceDataGroup.Location = new System.Drawing.Point(0, 0);
            this.DeviceDataGroup.Margin = new System.Windows.Forms.Padding(2);
            this.DeviceDataGroup.Name = "DeviceDataGroup";
            this.DeviceDataGroup.Padding = new System.Windows.Forms.Padding(2);
            this.DeviceDataGroup.Size = new System.Drawing.Size(322, 170);
            this.DeviceDataGroup.TabIndex = 2;
            this.DeviceDataGroup.TabStop = false;
            this.DeviceDataGroup.Text = "PowerSupply";
            // 
            // OnStatePictureBox
            // 
            this.OnStatePictureBox.BackgroundImage = global::AlberEOL.Properties.Resources.switch_off;
            this.OnStatePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.OnStatePictureBox.Location = new System.Drawing.Point(136, 127);
            this.OnStatePictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.OnStatePictureBox.Name = "OnStatePictureBox";
            this.OnStatePictureBox.Size = new System.Drawing.Size(45, 30);
            this.OnStatePictureBox.TabIndex = 9;
            this.OnStatePictureBox.TabStop = false;
            // 
            // StatusPictureBox
            // 
            this.StatusPictureBox.BackgroundImage = global::AlberEOL.Properties.Resources.usb_disconn;
            this.StatusPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.StatusPictureBox.Location = new System.Drawing.Point(143, 91);
            this.StatusPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.StatusPictureBox.Name = "StatusPictureBox";
            this.StatusPictureBox.Size = new System.Drawing.Size(30, 32);
            this.StatusPictureBox.TabIndex = 8;
            this.StatusPictureBox.TabStop = false;
            // 
            // DeviceStatusTextBox
            // 
            this.DeviceStatusTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DeviceStatusTextBox.Enabled = false;
            this.DeviceStatusTextBox.Font = new System.Drawing.Font("Oswald", 10F, System.Drawing.FontStyle.Bold);
            this.DeviceStatusTextBox.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.DeviceStatusTextBox.Location = new System.Drawing.Point(156, 47);
            this.DeviceStatusTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.DeviceStatusTextBox.Name = "DeviceStatusTextBox";
            this.DeviceStatusTextBox.Size = new System.Drawing.Size(156, 20);
            this.DeviceStatusTextBox.TabIndex = 0;
            // 
            // DeviceStatusLabel
            // 
            this.DeviceStatusLabel.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.DeviceStatusLabel.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.DeviceStatusLabel.Location = new System.Drawing.Point(5, 47);
            this.DeviceStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DeviceStatusLabel.Name = "DeviceStatusLabel";
            this.DeviceStatusLabel.Size = new System.Drawing.Size(147, 24);
            this.DeviceStatusLabel.TabIndex = 7;
            this.DeviceStatusLabel.Text = "Status";
            this.DeviceStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DeviceAddressTextBox
            // 
            this.DeviceAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DeviceAddressTextBox.Enabled = false;
            this.DeviceAddressTextBox.Font = new System.Drawing.Font("Oswald", 10F, System.Drawing.FontStyle.Bold);
            this.DeviceAddressTextBox.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.DeviceAddressTextBox.Location = new System.Drawing.Point(156, 23);
            this.DeviceAddressTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.DeviceAddressTextBox.Name = "DeviceAddressTextBox";
            this.DeviceAddressTextBox.Size = new System.Drawing.Size(156, 20);
            this.DeviceAddressTextBox.TabIndex = 3;
            // 
            // DeviceAddressLabel
            // 
            this.DeviceAddressLabel.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.DeviceAddressLabel.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.DeviceAddressLabel.Location = new System.Drawing.Point(5, 23);
            this.DeviceAddressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DeviceAddressLabel.Name = "DeviceAddressLabel";
            this.DeviceAddressLabel.Size = new System.Drawing.Size(147, 24);
            this.DeviceAddressLabel.TabIndex = 4;
            this.DeviceAddressLabel.Text = "Device Address";
            this.DeviceAddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ReadBackDataWorker
            // 
            this.ReadBackDataWorker.WorkerReportsProgress = true;
            this.ReadBackDataWorker.WorkerSupportsCancellation = true;
            this.ReadBackDataWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ReadBackDataWorker_DoWork);
            // 
            // PowerSupplyDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DeviceDataGroup);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PowerSupplyDisplay";
            this.Size = new System.Drawing.Size(322, 170);
            this.SettingsGroup.ResumeLayout(false);
            this.SettingsGroup.PerformLayout();
            this.ReadBackDataGroup.ResumeLayout(false);
            this.ReadBackDataGroup.PerformLayout();
            this.DeviceDataGroup.ResumeLayout(false);
            this.DeviceDataGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OnStatePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SettingsGroup;
        private System.Windows.Forms.GroupBox ReadBackDataGroup;
        private System.Windows.Forms.Label CurrentReadBackLabel;
        private System.Windows.Forms.TextBox CurrentReadBackTextBox;
        private System.Windows.Forms.Label VoltageReadBackLabel;
        private System.Windows.Forms.TextBox VoltageReadBackTextBox;
        private System.Windows.Forms.Label CurrentSettingsLabel;
        private System.Windows.Forms.TextBox VoltageSettingsTextBox;
        private System.Windows.Forms.TextBox CurrentSettingsTextBox;
        private System.Windows.Forms.Label VoltageSettingsLabel;
        private System.Windows.Forms.GroupBox DeviceDataGroup;
        private System.Windows.Forms.TextBox DeviceStatusTextBox;
        private System.Windows.Forms.Label DeviceStatusLabel;
        private System.Windows.Forms.TextBox DeviceAddressTextBox;
        private System.Windows.Forms.Label DeviceAddressLabel;
        private System.Windows.Forms.PictureBox StatusPictureBox;
        private System.Windows.Forms.PictureBox OnStatePictureBox;
        public System.ComponentModel.BackgroundWorker ReadBackDataWorker;
    }
}
