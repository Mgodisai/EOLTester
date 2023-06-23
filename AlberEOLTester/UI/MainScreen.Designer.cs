using AlberEOL.UI.GraphicalComponents;

namespace AlberEOL.UI
{
    partial class MainScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.Header = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SoftwareVersion = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.UserIDLabel = new System.Windows.Forms.Label();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.StationTab = new System.Windows.Forms.TabPage();
            this.StationControl = new AlberEOL.UI.GraphicalComponents.StationControl();
            this.ServiceTab = new System.Windows.Forms.TabPage();
            this.SettingsControl = new AlberEOL.UI.GraphicalComponents.SettingsControl();
            this.printerControl1 = new AlberEOL.UI.GraphicalComponents.PrinterControl();
            this.bimControl1 = new AlberEOL.UI.GraphicalComponents.BimControl();
            this.ExitButton = new AlberEOL.DesignElements.InterfaceButton();
            this.DataControlContainer = new AlberEOL.UI.GraphicalComponents.DataControlContainer();
            this.RegisterControl = new AlberEOL.UI.GraphicalComponents.RegisterControlContainer();
            this.PowerSupplyDisplay = new AlberEOL.UI.GraphicalComponents.PowerSupplyDisplay();
            this.RelayControlContainer = new AlberEOL.UI.GraphicalComponents.RelayControlContainer();
            this.btnEXIT = new AlberEOL.DesignElements.InterfaceButton();
            this.TesterStatusStrip = new System.Windows.Forms.StatusStrip();
            this.TesterStatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.MainTabControl.SuspendLayout();
            this.StationTab.SuspendLayout();
            this.ServiceTab.SuspendLayout();
            this.TesterStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.Transparent;
            this.Header.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.Header.Location = new System.Drawing.Point(147, 13);
            this.Header.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(521, 54);
            this.Header.TabIndex = 47;
            this.Header.Text = "ALBER EOL TESZTER";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::AlberEOL.Properties.Resources.vtep_logo_medium;
            this.pictureBox1.Location = new System.Drawing.Point(20, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 48;
            this.pictureBox1.TabStop = false;
            // 
            // SoftwareVersion
            // 
            this.SoftwareVersion.AutoSize = true;
            this.SoftwareVersion.BackColor = System.Drawing.Color.Transparent;
            this.SoftwareVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SoftwareVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.SoftwareVersion.Location = new System.Drawing.Point(5, 1303);
            this.SoftwareVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SoftwareVersion.Name = "SoftwareVersion";
            this.SoftwareVersion.Size = new System.Drawing.Size(78, 16);
            this.SoftwareVersion.TabIndex = 50;
            this.SoftwareVersion.Text = "ver.1.0.0.1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.label7.Location = new System.Drawing.Point(2259, 1300);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(228, 16);
            this.label7.TabIndex = 51;
            this.label7.Text = "VIDEOTON Elektro-PLAST KFT.";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::AlberEOL.Properties.Resources.user_ico_white;
            this.pictureBox2.Location = new System.Drawing.Point(1085, 12);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 54;
            this.pictureBox2.TabStop = false;
            // 
            // UserIDLabel
            // 
            this.UserIDLabel.AutoSize = true;
            this.UserIDLabel.BackColor = System.Drawing.Color.Transparent;
            this.UserIDLabel.Font = new System.Drawing.Font("Oswald", 9.749999F, System.Drawing.FontStyle.Bold);
            this.UserIDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.UserIDLabel.Location = new System.Drawing.Point(1113, 10);
            this.UserIDLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UserIDLabel.Name = "UserIDLabel";
            this.UserIDLabel.Size = new System.Drawing.Size(71, 22);
            this.UserIDLabel.TabIndex = 53;
            this.UserIDLabel.Text = "loggeduser";
            // 
            // MainTabControl
            // 
            this.MainTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.MainTabControl.Controls.Add(this.StationTab);
            this.MainTabControl.Controls.Add(this.ServiceTab);
            this.MainTabControl.ItemSize = new System.Drawing.Size(42, 15);
            this.MainTabControl.Location = new System.Drawing.Point(10, 90);
            this.MainTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.Padding = new System.Drawing.Point(0, 0);
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(678, 570);
            this.MainTabControl.TabIndex = 62;
            this.MainTabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.MainTabControl_Selecting);
            // 
            // StationTab
            // 
            this.StationTab.Controls.Add(this.StationControl);
            this.StationTab.Location = new System.Drawing.Point(4, 4);
            this.StationTab.Name = "StationTab";
            this.StationTab.Padding = new System.Windows.Forms.Padding(3);
            this.StationTab.Size = new System.Drawing.Size(670, 547);
            this.StationTab.TabIndex = 0;
            this.StationTab.Text = "Main";
            // 
            // StationControl
            // 
            this.StationControl.BackColor = System.Drawing.Color.Black;
            this.StationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StationControl.ForeColor = System.Drawing.Color.Transparent;
            this.StationControl.Location = new System.Drawing.Point(3, 3);
            this.StationControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.StationControl.Name = "StationControl";
            this.StationControl.Size = new System.Drawing.Size(664, 541);
            this.StationControl.TabIndex = 52;
            // 
            // ServiceTab
            // 
            this.ServiceTab.Controls.Add(this.SettingsControl);
            this.ServiceTab.Location = new System.Drawing.Point(4, 4);
            this.ServiceTab.Name = "ServiceTab";
            this.ServiceTab.Padding = new System.Windows.Forms.Padding(3);
            this.ServiceTab.Size = new System.Drawing.Size(670, 547);
            this.ServiceTab.TabIndex = 1;
            this.ServiceTab.Text = "Service";
            this.ServiceTab.UseVisualStyleBackColor = true;
            // 
            // SettingsControl
            // 
            this.SettingsControl.BackColor = System.Drawing.Color.Black;
            this.SettingsControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsControl.Location = new System.Drawing.Point(3, 3);
            this.SettingsControl.Margin = new System.Windows.Forms.Padding(4);
            this.SettingsControl.Name = "SettingsControl";
            this.SettingsControl.Size = new System.Drawing.Size(664, 541);
            this.SettingsControl.TabIndex = 0;
            // 
            // printerControl1
            // 
            this.printerControl1.BackColor = System.Drawing.Color.Transparent;
            this.printerControl1.Location = new System.Drawing.Point(20, 841);
            this.printerControl1.Margin = new System.Windows.Forms.Padding(2);
            this.printerControl1.Name = "printerControl1";
            this.printerControl1.Size = new System.Drawing.Size(568, 71);
            this.printerControl1.TabIndex = 64;
            // 
            // bimControl1
            // 
            this.bimControl1.BackColor = System.Drawing.Color.Transparent;
            this.bimControl1.Location = new System.Drawing.Point(380, 662);
            this.bimControl1.Margin = new System.Windows.Forms.Padding(2);
            this.bimControl1.Name = "bimControl1";
            this.bimControl1.Size = new System.Drawing.Size(94, 175);
            this.bimControl1.TabIndex = 63;
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ExitButton.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.ExitButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ExitButton.BorderRadius = 0;
            this.ExitButton.BorderSize = 0;
            this.ExitButton.FlatAppearance.BorderSize = 0;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.ExitButton.ForeColor = System.Drawing.Color.White;
            this.ExitButton.Location = new System.Drawing.Point(1230, 9);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(2);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(24, 24);
            this.ExitButton.TabIndex = 56;
            this.ExitButton.Text = "X";
            this.ExitButton.TextColor = System.Drawing.Color.White;
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // DataControlContainer
            // 
            this.DataControlContainer.BackColor = System.Drawing.Color.Transparent;
            this.DataControlContainer.Location = new System.Drawing.Point(694, 68);
            this.DataControlContainer.Margin = new System.Windows.Forms.Padding(4);
            this.DataControlContainer.Name = "DataControlContainer";
            this.DataControlContainer.Size = new System.Drawing.Size(568, 577);
            this.DataControlContainer.TabIndex = 60;
            // 
            // RegisterControl
            // 
            this.RegisterControl.Location = new System.Drawing.Point(587, 662);
            this.RegisterControl.Margin = new System.Windows.Forms.Padding(2);
            this.RegisterControl.Name = "RegisterControl";
            this.RegisterControl.Size = new System.Drawing.Size(675, 147);
            this.RegisterControl.TabIndex = 59;
            // 
            // PowerSupplyDisplay
            // 
            this.PowerSupplyDisplay.Location = new System.Drawing.Point(20, 662);
            this.PowerSupplyDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.PowerSupplyDisplay.Name = "PowerSupplyDisplay";
            this.PowerSupplyDisplay.Size = new System.Drawing.Size(346, 175);
            this.PowerSupplyDisplay.TabIndex = 57;
            // 
            // RelayControlContainer
            // 
            this.RelayControlContainer.BackColor = System.Drawing.Color.Transparent;
            this.RelayControlContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.RelayControlContainer.Location = new System.Drawing.Point(492, 662);
            this.RelayControlContainer.Margin = new System.Windows.Forms.Padding(2);
            this.RelayControlContainer.Name = "RelayControlContainer";
            this.RelayControlContainer.Size = new System.Drawing.Size(88, 175);
            this.RelayControlContainer.TabIndex = 55;
            // 
            // btnEXIT
            // 
            this.btnEXIT.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnEXIT.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.btnEXIT.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnEXIT.BorderRadius = 0;
            this.btnEXIT.BorderSize = 0;
            this.btnEXIT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEXIT.FlatAppearance.BorderSize = 0;
            this.btnEXIT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEXIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnEXIT.ForeColor = System.Drawing.Color.White;
            this.btnEXIT.Location = new System.Drawing.Point(1365, 30);
            this.btnEXIT.Name = "btnEXIT";
            this.btnEXIT.Size = new System.Drawing.Size(23, 27);
            this.btnEXIT.TabIndex = 49;
            this.btnEXIT.Text = "X";
            this.btnEXIT.TextColor = System.Drawing.Color.White;
            this.btnEXIT.UseVisualStyleBackColor = false;
            this.btnEXIT.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // TesterStatusStrip
            // 
            this.TesterStatusStrip.BackColor = System.Drawing.Color.Transparent;
            this.TesterStatusStrip.Font = new System.Drawing.Font("Oswald", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TesterStatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TesterStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TesterStatusStripLabel});
            this.TesterStatusStrip.Location = new System.Drawing.Point(0, 945);
            this.TesterStatusStrip.Name = "TesterStatusStrip";
            this.TesterStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 11, 0);
            this.TesterStatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.TesterStatusStrip.Size = new System.Drawing.Size(1273, 24);
            this.TesterStatusStrip.SizingGrip = false;
            this.TesterStatusStrip.Stretch = false;
            this.TesterStatusStrip.TabIndex = 65;
            this.TesterStatusStrip.Text = "statusStrip1";
            this.TesterStatusStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // TesterStatusStripLabel
            // 
            this.TesterStatusStripLabel.Font = new System.Drawing.Font("Oswald", 8F);
            this.TesterStatusStripLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.TesterStatusStripLabel.Name = "TesterStatusStripLabel";
            this.TesterStatusStripLabel.Size = new System.Drawing.Size(52, 19);
            this.TesterStatusStripLabel.Text = "sdfasdfasd";
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1273, 969);
            this.ControlBox = false;
            this.Controls.Add(this.TesterStatusStrip);
            this.Controls.Add(this.printerControl1);
            this.Controls.Add(this.bimControl1);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.MainTabControl);
            this.Controls.Add(this.DataControlContainer);
            this.Controls.Add(this.RegisterControl);
            this.Controls.Add(this.PowerSupplyDisplay);
            this.Controls.Add(this.RelayControlContainer);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.UserIDLabel);
            this.Controls.Add(this.SoftwareVersion);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnEXIT);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Header);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainScreen";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alber EOL Tester";
            this.Load += new System.EventHandler(this.MainScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.MainTabControl.ResumeLayout(false);
            this.StationTab.ResumeLayout(false);
            this.ServiceTab.ResumeLayout(false);
            this.TesterStatusStrip.ResumeLayout(false);
            this.TesterStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DesignElements.InterfaceButton btnEXIT;
        private System.Windows.Forms.Label SoftwareVersion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label UserIDLabel;
        private RelayControlContainer RelayControlContainer;
        private DesignElements.InterfaceButton ExitButton;
        private PowerSupplyDisplay PowerSupplyDisplay;
        private RegisterControlContainer RegisterControl;
        private DataControlContainer DataControlContainer;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage ServiceTab;
        private SettingsControl SettingsControl;
        private StationControl StationControl;
        private System.Windows.Forms.TabPage StationTab;
        private BimControl bimControl1;
        private PrinterControl printerControl1;
        private System.Windows.Forms.StatusStrip TesterStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel TesterStatusStripLabel;
    }
}

