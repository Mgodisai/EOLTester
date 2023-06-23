namespace AlberEOL.UI.GraphicalComponents
{
    partial class BimControl
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
            this.BimGroupBox = new System.Windows.Forms.GroupBox();
            this.ImpedanceTextBox = new AlberEOL.DesignElements.InterfaceTextBox();
            this.VoltageTextBox = new AlberEOL.DesignElements.InterfaceTextBox();
            this.ImpedanceLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BimGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // BimGroupBox
            // 
            this.BimGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.BimGroupBox.Controls.Add(this.label1);
            this.BimGroupBox.Controls.Add(this.ImpedanceLabel);
            this.BimGroupBox.Controls.Add(this.VoltageTextBox);
            this.BimGroupBox.Controls.Add(this.ImpedanceTextBox);
            this.BimGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BimGroupBox.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.BimGroupBox.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.BimGroupBox.Location = new System.Drawing.Point(0, 0);
            this.BimGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.BimGroupBox.Name = "BimGroupBox";
            this.BimGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.BimGroupBox.Size = new System.Drawing.Size(110, 212);
            this.BimGroupBox.TabIndex = 0;
            this.BimGroupBox.TabStop = false;
            this.BimGroupBox.Text = "Bim";
            // 
            // ImpedanceTextBox
            // 
            this.ImpedanceTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.ImpedanceTextBox.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.ImpedanceTextBox.BorderFocusColor = System.Drawing.Color.HotPink;
            this.ImpedanceTextBox.BorderRadius = 0;
            this.ImpedanceTextBox.BorderSize = 2;
            this.ImpedanceTextBox.Font = new System.Drawing.Font("Oswald", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImpedanceTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ImpedanceTextBox.Location = new System.Drawing.Point(16, 58);
            this.ImpedanceTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.ImpedanceTextBox.Multiline = false;
            this.ImpedanceTextBox.Name = "ImpedanceTextBox";
            this.ImpedanceTextBox.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.ImpedanceTextBox.PasswordChar = false;
            this.ImpedanceTextBox.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.ImpedanceTextBox.PlaceholderText = "";
            this.ImpedanceTextBox.ReadOnly = false;
            this.ImpedanceTextBox.Size = new System.Drawing.Size(82, 42);
            this.ImpedanceTextBox.TabIndex = 0;
            this.ImpedanceTextBox.Texts = "";
            this.ImpedanceTextBox.UnderlinedStyle = false;
            // 
            // VoltageTextBox
            // 
            this.VoltageTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.VoltageTextBox.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.VoltageTextBox.BorderFocusColor = System.Drawing.Color.HotPink;
            this.VoltageTextBox.BorderRadius = 0;
            this.VoltageTextBox.BorderSize = 2;
            this.VoltageTextBox.Font = new System.Drawing.Font("Oswald", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoltageTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.VoltageTextBox.Location = new System.Drawing.Point(16, 149);
            this.VoltageTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.VoltageTextBox.Multiline = false;
            this.VoltageTextBox.Name = "VoltageTextBox";
            this.VoltageTextBox.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.VoltageTextBox.PasswordChar = false;
            this.VoltageTextBox.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.VoltageTextBox.PlaceholderText = "";
            this.VoltageTextBox.ReadOnly = false;
            this.VoltageTextBox.Size = new System.Drawing.Size(82, 42);
            this.VoltageTextBox.TabIndex = 1;
            this.VoltageTextBox.Texts = "";
            this.VoltageTextBox.UnderlinedStyle = false;
            // 
            // ImpedanceLabel
            // 
            this.ImpedanceLabel.AutoSize = true;
            this.ImpedanceLabel.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.ImpedanceLabel.Location = new System.Drawing.Point(20, 30);
            this.ImpedanceLabel.Name = "ImpedanceLabel";
            this.ImpedanceLabel.Size = new System.Drawing.Size(78, 24);
            this.ImpedanceLabel.TabIndex = 2;
            this.ImpedanceLabel.Text = "Imp. (mΩ)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Voltage (V)";
            // 
            // BimControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.BimGroupBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BimControl";
            this.Size = new System.Drawing.Size(110, 212);
            this.BimGroupBox.ResumeLayout(false);
            this.BimGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox BimGroupBox;
        private DesignElements.InterfaceTextBox ImpedanceTextBox;
        private DesignElements.InterfaceTextBox VoltageTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ImpedanceLabel;
    }
}
