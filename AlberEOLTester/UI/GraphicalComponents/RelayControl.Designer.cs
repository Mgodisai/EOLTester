namespace AlberEOL.UI.GraphicalComponents
{
    partial class RelayControl
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
            this.RelayControlLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RelayControlLabel
            // 
            this.RelayControlLabel.BackColor = System.Drawing.Color.Transparent;
            this.RelayControlLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RelayControlLabel.Font = new System.Drawing.Font("Mohave", 10.2F, System.Drawing.FontStyle.Bold);
            this.RelayControlLabel.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.RelayControlLabel.Location = new System.Drawing.Point(0, 0);
            this.RelayControlLabel.Margin = new System.Windows.Forms.Padding(0);
            this.RelayControlLabel.Name = "RelayControlLabel";
            this.RelayControlLabel.Size = new System.Drawing.Size(78, 28);
            this.RelayControlLabel.TabIndex = 0;
            this.RelayControlLabel.Text = "Not used";
            this.RelayControlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RelayControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.Controls.Add(this.RelayControlLabel);
            this.Name = "RelayControl";
            this.Size = new System.Drawing.Size(78, 28);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label RelayControlLabel;
    }
}
