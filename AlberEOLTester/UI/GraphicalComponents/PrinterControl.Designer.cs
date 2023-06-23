namespace AlberEOL.UI.GraphicalComponents
{
    partial class PrinterControl
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
            this.PrinterGroupBox = new System.Windows.Forms.GroupBox();
            this.MessageTextBox = new AlberEOL.DesignElements.InterfaceTextBox();
            this.PrinterGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // PrinterGroupBox
            // 
            this.PrinterGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.PrinterGroupBox.Controls.Add(this.MessageTextBox);
            this.PrinterGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrinterGroupBox.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.PrinterGroupBox.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.PrinterGroupBox.Location = new System.Drawing.Point(0, 0);
            this.PrinterGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.PrinterGroupBox.Name = "PrinterGroupBox";
            this.PrinterGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.PrinterGroupBox.Size = new System.Drawing.Size(335, 82);
            this.PrinterGroupBox.TabIndex = 0;
            this.PrinterGroupBox.TabStop = false;
            this.PrinterGroupBox.Text = "Printer";
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.MessageTextBox.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.MessageTextBox.BorderFocusColor = System.Drawing.Color.HotPink;
            this.MessageTextBox.BorderRadius = 0;
            this.MessageTextBox.BorderSize = 2;
            this.MessageTextBox.Font = new System.Drawing.Font("Oswald", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MessageTextBox.Location = new System.Drawing.Point(6, 34);
            this.MessageTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.MessageTextBox.Multiline = false;
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.MessageTextBox.PasswordChar = false;
            this.MessageTextBox.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.MessageTextBox.PlaceholderText = "";
            this.MessageTextBox.ReadOnly = false;
            this.MessageTextBox.Size = new System.Drawing.Size(323, 42);
            this.MessageTextBox.TabIndex = 0;
            this.MessageTextBox.Texts = "";
            this.MessageTextBox.UnderlinedStyle = false;
            // 
            // PrinterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.PrinterGroupBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PrinterControl";
            this.Size = new System.Drawing.Size(335, 82);
            this.PrinterGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox PrinterGroupBox;
        private DesignElements.InterfaceTextBox MessageTextBox;
    }
}
