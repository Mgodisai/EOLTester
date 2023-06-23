namespace AlberEOL.UI.GraphicalComponents
{
    partial class RelayControlContainer
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
            this.RelaysGroupBox = new System.Windows.Forms.GroupBox();
            this.Relay_Chg = new RelayControl();
            this.Relay_2Ohm = new RelayControl();
            this.Relay_1Ohm = new RelayControl();
            this.Relay_0_5Ohm = new RelayControl();
            this.RelaysGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // RelaysGroupBox
            // 
            this.RelaysGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.RelaysGroupBox.Controls.Add(this.Relay_Chg);
            this.RelaysGroupBox.Controls.Add(this.Relay_2Ohm);
            this.RelaysGroupBox.Controls.Add(this.Relay_1Ohm);
            this.RelaysGroupBox.Controls.Add(this.Relay_0_5Ohm);
            this.RelaysGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RelaysGroupBox.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.RelaysGroupBox.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.RelaysGroupBox.Location = new System.Drawing.Point(0, 0);
            this.RelaysGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.RelaysGroupBox.Name = "RelaysGroupBox";
            this.RelaysGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.RelaysGroupBox.Size = new System.Drawing.Size(88, 170);
            this.RelaysGroupBox.TabIndex = 0;
            this.RelaysGroupBox.TabStop = false;
            this.RelaysGroupBox.Text = "Relays";
            // 
            // Relay_Chg
            // 
            this.Relay_Chg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.Relay_Chg.ControlLabelText = "Not used";
            this.Relay_Chg.Location = new System.Drawing.Point(5, 126);
            this.Relay_Chg.Name = "Relay_Chg";
            this.Relay_Chg.Size = new System.Drawing.Size(78, 28);
            this.Relay_Chg.TabIndex = 3;
            // 
            // Relay_2Ohm
            // 
            this.Relay_2Ohm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.Relay_2Ohm.ControlLabelText = "Not used";
            this.Relay_2Ohm.Location = new System.Drawing.Point(5, 24);
            this.Relay_2Ohm.Name = "Relay_2Ohm";
            this.Relay_2Ohm.Size = new System.Drawing.Size(78, 28);
            this.Relay_2Ohm.TabIndex = 0;
            // 
            // Relay_1Ohm
            // 
            this.Relay_1Ohm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.Relay_1Ohm.ControlLabelText = "Not used";
            this.Relay_1Ohm.Location = new System.Drawing.Point(5, 58);
            this.Relay_1Ohm.Name = "Relay_1Ohm";
            this.Relay_1Ohm.Size = new System.Drawing.Size(78, 28);
            this.Relay_1Ohm.TabIndex = 1;
            // 
            // Relay_0_5Ohm
            // 
            this.Relay_0_5Ohm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.Relay_0_5Ohm.ControlLabelText = "Not used";
            this.Relay_0_5Ohm.Location = new System.Drawing.Point(5, 92);
            this.Relay_0_5Ohm.Name = "Relay_0_5Ohm";
            this.Relay_0_5Ohm.Size = new System.Drawing.Size(78, 28);
            this.Relay_0_5Ohm.TabIndex = 2;
            // 
            // RelayControlContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.RelaysGroupBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RelayControlContainer";
            this.Size = new System.Drawing.Size(88, 170);
            this.RelaysGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox RelaysGroupBox;
        private RelayControl Relay_Chg;
        private RelayControl Relay_0_5Ohm;
        private RelayControl Relay_1Ohm;
        private RelayControl Relay_2Ohm;
    }
}
