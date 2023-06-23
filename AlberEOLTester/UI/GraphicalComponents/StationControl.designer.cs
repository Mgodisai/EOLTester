namespace AlberEOL.UI.GraphicalComponents
{
    partial class StationControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StationControl));
            this.txt_ErrorLog = new System.Windows.Forms.TextBox();
            this.txt_Message = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_operation = new System.Windows.Forms.TextBox();
            this.lbl_Operation = new System.Windows.Forms.Label();
            this.txt_TaskBox = new System.Windows.Forms.Label();
            this.lbl_task = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ProductTypeLabel = new System.Windows.Forms.Label();
            this.BackButton = new AlberEOL.DesignElements.InterfaceButton();
            this.DoneOrStartButton = new AlberEOL.DesignElements.InterfaceButton();
            this.SerialNumberLabel = new System.Windows.Forms.Label();
            this.ProductIDLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_ErrorLog
            // 
            this.txt_ErrorLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_ErrorLog.Font = new System.Drawing.Font("Oswald", 10F);
            this.txt_ErrorLog.Location = new System.Drawing.Point(231, 370);
            this.txt_ErrorLog.Margin = new System.Windows.Forms.Padding(2);
            this.txt_ErrorLog.Multiline = true;
            this.txt_ErrorLog.Name = "txt_ErrorLog";
            this.txt_ErrorLog.ReadOnly = true;
            this.txt_ErrorLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_ErrorLog.Size = new System.Drawing.Size(423, 95);
            this.txt_ErrorLog.TabIndex = 11;
            // 
            // txt_Message
            // 
            this.txt_Message.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.txt_Message.Font = new System.Drawing.Font("Oswald", 12F, System.Drawing.FontStyle.Bold);
            this.txt_Message.ForeColor = System.Drawing.Color.Black;
            this.txt_Message.Location = new System.Drawing.Point(231, 235);
            this.txt_Message.Margin = new System.Windows.Forms.Padding(0);
            this.txt_Message.Name = "txt_Message";
            this.txt_Message.Size = new System.Drawing.Size(423, 98);
            this.txt_Message.TabIndex = 10;
            this.txt_Message.Text = "STATION MESSAGE";
            this.txt_Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(227, 346);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 22);
            this.label1.TabIndex = 12;
            this.label1.Text = "HIBANAPLÓ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_operation
            // 
            this.txt_operation.BackColor = System.Drawing.Color.White;
            this.txt_operation.Font = new System.Drawing.Font("Oswald", 9F);
            this.txt_operation.Location = new System.Drawing.Point(16, 162);
            this.txt_operation.Margin = new System.Windows.Forms.Padding(2);
            this.txt_operation.Multiline = true;
            this.txt_operation.Name = "txt_operation";
            this.txt_operation.ReadOnly = true;
            this.txt_operation.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_operation.Size = new System.Drawing.Size(200, 365);
            this.txt_operation.TabIndex = 14;
            // 
            // lbl_Operation
            // 
            this.lbl_Operation.AutoSize = true;
            this.lbl_Operation.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Operation.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_Operation.Location = new System.Drawing.Point(13, 138);
            this.lbl_Operation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Operation.Name = "lbl_Operation";
            this.lbl_Operation.Size = new System.Drawing.Size(134, 22);
            this.lbl_Operation.TabIndex = 13;
            this.lbl_Operation.Text = "AKTUÁLIS MŰVELETEK";
            this.lbl_Operation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_TaskBox
            // 
            this.txt_TaskBox.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.txt_TaskBox.Font = new System.Drawing.Font("Oswald", 14F, System.Drawing.FontStyle.Bold);
            this.txt_TaskBox.ForeColor = System.Drawing.SystemColors.Control;
            this.txt_TaskBox.Location = new System.Drawing.Point(231, 39);
            this.txt_TaskBox.Margin = new System.Windows.Forms.Padding(0);
            this.txt_TaskBox.Name = "txt_TaskBox";
            this.txt_TaskBox.Size = new System.Drawing.Size(423, 187);
            this.txt_TaskBox.TabIndex = 17;
            this.txt_TaskBox.Text = "STATION TASKBOX";
            this.txt_TaskBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_task
            // 
            this.lbl_task.AutoSize = true;
            this.lbl_task.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_task.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.lbl_task.Location = new System.Drawing.Point(228, 19);
            this.lbl_task.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_task.Name = "lbl_task";
            this.lbl_task.Size = new System.Drawing.Size(74, 22);
            this.lbl_task.TabIndex = 18;
            this.lbl_task.Text = "FELADATOK";
            this.lbl_task.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ProductTypeLabel);
            this.groupBox1.Controls.Add(this.BackButton);
            this.groupBox1.Controls.Add(this.DoneOrStartButton);
            this.groupBox1.Controls.Add(this.SerialNumberLabel);
            this.groupBox1.Controls.Add(this.ProductIDLabel);
            this.groupBox1.Controls.Add(this.txt_Message);
            this.groupBox1.Controls.Add(this.txt_operation);
            this.groupBox1.Controls.Add(this.lbl_Operation);
            this.groupBox1.Controls.Add(this.txt_TaskBox);
            this.groupBox1.Controls.Add(this.lbl_task);
            this.groupBox1.Controls.Add(this.txt_ErrorLog);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 554);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TESZTER ÁLLOMÁS";
            // 
            // ProductTypeLabel
            // 
            this.ProductTypeLabel.BackColor = System.Drawing.Color.White;
            this.ProductTypeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProductTypeLabel.Font = new System.Drawing.Font("Oswald", 10F, System.Drawing.FontStyle.Bold);
            this.ProductTypeLabel.Location = new System.Drawing.Point(16, 39);
            this.ProductTypeLabel.Name = "ProductTypeLabel";
            this.ProductTypeLabel.Padding = new System.Windows.Forms.Padding(4);
            this.ProductTypeLabel.Size = new System.Drawing.Size(200, 30);
            this.ProductTypeLabel.TabIndex = 24;
            this.ProductTypeLabel.Text = "Terméktípus";
            this.ProductTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.SlateGray;
            this.BackButton.BackgroundColor = System.Drawing.Color.SlateGray;
            this.BackButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BackButton.BackgroundImage")));
            this.BackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BackButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.BackButton.BorderRadius = 0;
            this.BackButton.BorderSize = 0;
            this.BackButton.Enabled = false;
            this.BackButton.FlatAppearance.BorderSize = 0;
            this.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackButton.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.BackButton.ForeColor = System.Drawing.Color.LightGray;
            this.BackButton.Location = new System.Drawing.Point(605, 482);
            this.BackButton.Margin = new System.Windows.Forms.Padding(2);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(49, 45);
            this.BackButton.TabIndex = 23;
            this.BackButton.TextColor = System.Drawing.Color.LightGray;
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.ButtonBack_Click);
            // 
            // DoneOrStartButton
            // 
            this.DoneOrStartButton.BackColor = System.Drawing.Color.SlateGray;
            this.DoneOrStartButton.BackgroundColor = System.Drawing.Color.SlateGray;
            this.DoneOrStartButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.DoneOrStartButton.BorderRadius = 0;
            this.DoneOrStartButton.BorderSize = 0;
            this.DoneOrStartButton.Enabled = false;
            this.DoneOrStartButton.FlatAppearance.BorderSize = 0;
            this.DoneOrStartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DoneOrStartButton.Font = new System.Drawing.Font("Mohave", 12F, System.Drawing.FontStyle.Bold);
            this.DoneOrStartButton.ForeColor = System.Drawing.Color.LightGray;
            this.DoneOrStartButton.Location = new System.Drawing.Point(231, 482);
            this.DoneOrStartButton.Margin = new System.Windows.Forms.Padding(2);
            this.DoneOrStartButton.Name = "DoneOrStartButton";
            this.DoneOrStartButton.Size = new System.Drawing.Size(367, 45);
            this.DoneOrStartButton.TabIndex = 22;
            this.DoneOrStartButton.Text = "Nyugta/Start";
            this.DoneOrStartButton.TextColor = System.Drawing.Color.LightGray;
            this.DoneOrStartButton.UseVisualStyleBackColor = false;
            this.DoneOrStartButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // SerialNumberLabel
            // 
            this.SerialNumberLabel.BackColor = System.Drawing.Color.White;
            this.SerialNumberLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SerialNumberLabel.Font = new System.Drawing.Font("Oswald", 10F, System.Drawing.FontStyle.Bold);
            this.SerialNumberLabel.Location = new System.Drawing.Point(16, 101);
            this.SerialNumberLabel.Name = "SerialNumberLabel";
            this.SerialNumberLabel.Padding = new System.Windows.Forms.Padding(4);
            this.SerialNumberLabel.Size = new System.Drawing.Size(200, 30);
            this.SerialNumberLabel.TabIndex = 20;
            this.SerialNumberLabel.Text = "Szériaszám";
            this.SerialNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProductIDLabel
            // 
            this.ProductIDLabel.BackColor = System.Drawing.Color.White;
            this.ProductIDLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProductIDLabel.Font = new System.Drawing.Font("Oswald", 10F, System.Drawing.FontStyle.Bold);
            this.ProductIDLabel.Location = new System.Drawing.Point(16, 70);
            this.ProductIDLabel.Name = "ProductIDLabel";
            this.ProductIDLabel.Padding = new System.Windows.Forms.Padding(4);
            this.ProductIDLabel.Size = new System.Drawing.Size(200, 30);
            this.ProductIDLabel.TabIndex = 19;
            this.ProductIDLabel.Text = "Termék Azonosító";
            this.ProductIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StationControl";
            this.Size = new System.Drawing.Size(660, 554);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label txt_Message;
        private System.Windows.Forms.TextBox txt_ErrorLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_operation;
        private System.Windows.Forms.Label lbl_Operation;
        private System.Windows.Forms.Label txt_TaskBox;
        private System.Windows.Forms.Label lbl_task;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label SerialNumberLabel;
        private System.Windows.Forms.Label ProductIDLabel;
        private DesignElements.InterfaceButton DoneOrStartButton;
        private DesignElements.InterfaceButton BackButton;
        private System.Windows.Forms.Label ProductTypeLabel;
    }
}
