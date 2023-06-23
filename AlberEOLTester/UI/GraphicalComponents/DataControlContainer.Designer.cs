namespace AlberEOL.UI.GraphicalComponents
{
    partial class DataControlContainer
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.TestDetailsTab = new System.Windows.Forms.TabPage();
            this.TestResultDataGridView = new System.Windows.Forms.DataGridView();
            this.SubcommandsTab = new System.Windows.Forms.TabPage();
            this.SubcommandsPGrid = new System.Windows.Forms.PropertyGrid();
            this.DirectRamTab = new System.Windows.Forms.TabPage();
            this.DirectRamPGrid = new System.Windows.Forms.PropertyGrid();
            this.DataMemoryReadTab = new System.Windows.Forms.TabPage();
            this.ReadRamComponent = new AlberEOL.UI.GraphicalComponents.RamComponent();
            this.DataMemoryPGridTab = new System.Windows.Forms.TabPage();
            this.DataMemoryPGrid = new System.Windows.Forms.PropertyGrid();
            this.DataMemoryWriteTab = new System.Windows.Forms.TabPage();
            this.WriteRamComponent = new AlberEOL.UI.GraphicalComponents.RamComponent();
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ImportDataMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportDataMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrevDMTab = new System.Windows.Forms.TabPage();
            this.PrevDMRamComponent = new AlberEOL.UI.GraphicalComponents.RamComponent();
            this.TestResultContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExportTestResultToCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTabControl.SuspendLayout();
            this.TestDetailsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TestResultDataGridView)).BeginInit();
            this.SubcommandsTab.SuspendLayout();
            this.DirectRamTab.SuspendLayout();
            this.DataMemoryReadTab.SuspendLayout();
            this.DataMemoryPGridTab.SuspendLayout();
            this.DataMemoryWriteTab.SuspendLayout();
            this.ContextMenu.SuspendLayout();
            this.PrevDMTab.SuspendLayout();
            this.TestResultContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTabControl
            // 
            this.MainTabControl.CausesValidation = false;
            this.MainTabControl.Controls.Add(this.TestDetailsTab);
            this.MainTabControl.Controls.Add(this.SubcommandsTab);
            this.MainTabControl.Controls.Add(this.DirectRamTab);
            this.MainTabControl.Controls.Add(this.DataMemoryReadTab);
            this.MainTabControl.Controls.Add(this.DataMemoryPGridTab);
            this.MainTabControl.Controls.Add(this.DataMemoryWriteTab);
            this.MainTabControl.Controls.Add(this.PrevDMTab);
            this.MainTabControl.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            this.MainTabControl.ItemSize = new System.Drawing.Size(100, 30);
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MainTabControl.Multiline = true;
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(571, 575);
            this.MainTabControl.TabIndex = 59;
            // 
            // TestDetailsTab
            // 
            this.TestDetailsTab.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.TestDetailsTab.Controls.Add(this.TestResultDataGridView);
            this.TestDetailsTab.Location = new System.Drawing.Point(4, 34);
            this.TestDetailsTab.Name = "TestDetailsTab";
            this.TestDetailsTab.Padding = new System.Windows.Forms.Padding(3);
            this.TestDetailsTab.Size = new System.Drawing.Size(563, 537);
            this.TestDetailsTab.TabIndex = 5;
            this.TestDetailsTab.Text = "TestDetails";
            // 
            // TestResultDataGridView
            // 
            this.TestResultDataGridView.AllowUserToAddRows = false;
            this.TestResultDataGridView.AllowUserToDeleteRows = false;
            this.TestResultDataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Oswald", 10F);
            dataGridViewCellStyle1.NullValue = "-";
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TestResultDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.TestResultDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TestResultDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TestResultDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.TestResultDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TestResultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Mohave", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TestResultDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.TestResultDataGridView.EnableHeadersVisualStyles = false;
            this.TestResultDataGridView.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.TestResultDataGridView.Location = new System.Drawing.Point(4, 4);
            this.TestResultDataGridView.Name = "TestResultDataGridView";
            this.TestResultDataGridView.ReadOnly = true;
            this.TestResultDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TestResultDataGridView.RowHeadersVisible = false;
            this.TestResultDataGridView.RowHeadersWidth = 10;
            this.TestResultDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Oswald", 9F, System.Drawing.FontStyle.Bold);
            this.TestResultDataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.TestResultDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TestResultDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TestResultDataGridView.ShowEditingIcon = false;
            this.TestResultDataGridView.Size = new System.Drawing.Size(555, 530);
            this.TestResultDataGridView.TabIndex = 0;
            this.TestResultDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.TestResultDataGridView_CellFormatting);
            // 
            // SubcommandsTab
            // 
            this.SubcommandsTab.BackColor = System.Drawing.Color.SteelBlue;
            this.SubcommandsTab.Controls.Add(this.SubcommandsPGrid);
            this.SubcommandsTab.Font = new System.Drawing.Font("Mohave", 10F);
            this.SubcommandsTab.ForeColor = System.Drawing.Color.Coral;
            this.SubcommandsTab.Location = new System.Drawing.Point(4, 34);
            this.SubcommandsTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SubcommandsTab.Name = "SubcommandsTab";
            this.SubcommandsTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SubcommandsTab.Size = new System.Drawing.Size(563, 537);
            this.SubcommandsTab.TabIndex = 0;
            this.SubcommandsTab.Text = "Subcommands";
            // 
            // SubcommandsPGrid
            // 
            this.SubcommandsPGrid.Font = new System.Drawing.Font("Oswald", 9F);
            this.SubcommandsPGrid.HelpVisible = false;
            this.SubcommandsPGrid.LineColor = System.Drawing.Color.SteelBlue;
            this.SubcommandsPGrid.Location = new System.Drawing.Point(4, 4);
            this.SubcommandsPGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SubcommandsPGrid.Name = "SubcommandsPGrid";
            this.SubcommandsPGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.SubcommandsPGrid.Size = new System.Drawing.Size(555, 530);
            this.SubcommandsPGrid.TabIndex = 0;
            this.SubcommandsPGrid.ToolbarVisible = false;
            // 
            // DirectRamTab
            // 
            this.DirectRamTab.BackColor = System.Drawing.Color.Gold;
            this.DirectRamTab.Controls.Add(this.DirectRamPGrid);
            this.DirectRamTab.Location = new System.Drawing.Point(4, 34);
            this.DirectRamTab.Name = "DirectRamTab";
            this.DirectRamTab.Padding = new System.Windows.Forms.Padding(3);
            this.DirectRamTab.Size = new System.Drawing.Size(563, 537);
            this.DirectRamTab.TabIndex = 1;
            this.DirectRamTab.Text = "DirectRam";
            // 
            // DirectRamPGrid
            // 
            this.DirectRamPGrid.Font = new System.Drawing.Font("Oswald", 9F);
            this.DirectRamPGrid.HelpVisible = false;
            this.DirectRamPGrid.LineColor = System.Drawing.Color.Gold;
            this.DirectRamPGrid.Location = new System.Drawing.Point(4, 4);
            this.DirectRamPGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DirectRamPGrid.Name = "DirectRamPGrid";
            this.DirectRamPGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.DirectRamPGrid.SelectedItemWithFocusBackColor = System.Drawing.Color.Gold;
            this.DirectRamPGrid.SelectedItemWithFocusForeColor = System.Drawing.Color.Black;
            this.DirectRamPGrid.Size = new System.Drawing.Size(555, 530);
            this.DirectRamPGrid.TabIndex = 1;
            this.DirectRamPGrid.ToolbarVisible = false;
            // 
            // DataMemoryReadTab
            // 
            this.DataMemoryReadTab.BackColor = System.Drawing.Color.LawnGreen;
            this.DataMemoryReadTab.Controls.Add(this.ReadRamComponent);
            this.DataMemoryReadTab.Location = new System.Drawing.Point(4, 34);
            this.DataMemoryReadTab.Name = "DataMemoryReadTab";
            this.DataMemoryReadTab.Padding = new System.Windows.Forms.Padding(3);
            this.DataMemoryReadTab.Size = new System.Drawing.Size(563, 537);
            this.DataMemoryReadTab.TabIndex = 2;
            this.DataMemoryReadTab.Text = "DataMemoryR";
            // 
            // ReadRamComponent
            // 
            this.ReadRamComponent.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.ReadRamComponent.Location = new System.Drawing.Point(4, 4);
            this.ReadRamComponent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ReadRamComponent.Name = "ReadRamComponent";
            this.ReadRamComponent.Size = new System.Drawing.Size(555, 530);
            this.ReadRamComponent.TabIndex = 0;
            // 
            // DataMemoryPGridTab
            // 
            this.DataMemoryPGridTab.BackColor = System.Drawing.Color.LawnGreen;
            this.DataMemoryPGridTab.Controls.Add(this.DataMemoryPGrid);
            this.DataMemoryPGridTab.Location = new System.Drawing.Point(4, 34);
            this.DataMemoryPGridTab.Name = "DataMemoryPGridTab";
            this.DataMemoryPGridTab.Padding = new System.Windows.Forms.Padding(3);
            this.DataMemoryPGridTab.Size = new System.Drawing.Size(563, 537);
            this.DataMemoryPGridTab.TabIndex = 3;
            this.DataMemoryPGridTab.Text = "DataMemoryPGrid";
            // 
            // DataMemoryPGrid
            // 
            this.DataMemoryPGrid.Font = new System.Drawing.Font("Oswald", 9F);
            this.DataMemoryPGrid.HelpVisible = false;
            this.DataMemoryPGrid.LineColor = System.Drawing.Color.LawnGreen;
            this.DataMemoryPGrid.Location = new System.Drawing.Point(4, 4);
            this.DataMemoryPGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DataMemoryPGrid.Name = "DataMemoryPGrid";
            this.DataMemoryPGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.DataMemoryPGrid.SelectedItemWithFocusBackColor = System.Drawing.Color.LawnGreen;
            this.DataMemoryPGrid.Size = new System.Drawing.Size(555, 530);
            this.DataMemoryPGrid.TabIndex = 1;
            this.DataMemoryPGrid.ToolbarVisible = false;
            // 
            // DataMemoryWriteTab
            // 
            this.DataMemoryWriteTab.BackColor = System.Drawing.Color.IndianRed;
            this.DataMemoryWriteTab.Controls.Add(this.WriteRamComponent);
            this.DataMemoryWriteTab.Location = new System.Drawing.Point(4, 34);
            this.DataMemoryWriteTab.Name = "DataMemoryWriteTab";
            this.DataMemoryWriteTab.Padding = new System.Windows.Forms.Padding(3);
            this.DataMemoryWriteTab.Size = new System.Drawing.Size(563, 537);
            this.DataMemoryWriteTab.TabIndex = 4;
            this.DataMemoryWriteTab.Text = "DataMemoryW";
            // 
            // WriteRamComponent
            // 
            this.WriteRamComponent.ContextMenuStrip = this.ContextMenu;
            this.WriteRamComponent.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.WriteRamComponent.Location = new System.Drawing.Point(4, 4);
            this.WriteRamComponent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.WriteRamComponent.Name = "WriteRamComponent";
            this.WriteRamComponent.Size = new System.Drawing.Size(555, 530);
            this.WriteRamComponent.TabIndex = 1;
            // 
            // ContextMenu
            // 
            this.ContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportDataMemoryToolStripMenuItem,
            this.ExportDataMemoryToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.ContextMenu.Name = "ContextMenu";
            this.ContextMenu.Size = new System.Drawing.Size(183, 70);
            // 
            // ImportDataMemoryToolStripMenuItem
            // 
            this.ImportDataMemoryToolStripMenuItem.Name = "ImportDataMemoryToolStripMenuItem";
            this.ImportDataMemoryToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.ImportDataMemoryToolStripMenuItem.Text = "Import DataMemory";
            this.ImportDataMemoryToolStripMenuItem.Click += new System.EventHandler(this.ImportDataMemoryToolStripMenuItem_Click);
            // 
            // ExportDataMemoryToolStripMenuItem
            // 
            this.ExportDataMemoryToolStripMenuItem.Name = "ExportDataMemoryToolStripMenuItem";
            this.ExportDataMemoryToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.ExportDataMemoryToolStripMenuItem.Text = "Export DataMemory";
            this.ExportDataMemoryToolStripMenuItem.Click += new System.EventHandler(this.ExportDataMemoryToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // PrevDMTab
            // 
            this.PrevDMTab.Controls.Add(this.PrevDMRamComponent);
            this.PrevDMTab.Location = new System.Drawing.Point(4, 34);
            this.PrevDMTab.Margin = new System.Windows.Forms.Padding(2);
            this.PrevDMTab.Name = "PrevDMTab";
            this.PrevDMTab.Padding = new System.Windows.Forms.Padding(2);
            this.PrevDMTab.Size = new System.Drawing.Size(563, 537);
            this.PrevDMTab.TabIndex = 6;
            this.PrevDMTab.Text = "PrevDM";
            this.PrevDMTab.UseVisualStyleBackColor = true;
            // 
            // PrevDMRamComponent
            // 
            this.PrevDMRamComponent.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.PrevDMRamComponent.Location = new System.Drawing.Point(4, 7);
            this.PrevDMRamComponent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PrevDMRamComponent.Name = "PrevDMRamComponent";
            this.PrevDMRamComponent.Size = new System.Drawing.Size(555, 530);
            this.PrevDMRamComponent.TabIndex = 2;
            // 
            // TestResultContextMenu
            // 
            this.TestResultContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TestResultContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportTestResultToCSV});
            this.TestResultContextMenu.Name = "TestResultContextMenu";
            this.TestResultContextMenu.Size = new System.Drawing.Size(147, 26);
            // 
            // ExportTestResultToCSV
            // 
            this.ExportTestResultToCSV.Name = "ExportTestResultToCSV";
            this.ExportTestResultToCSV.Size = new System.Drawing.Size(146, 22);
            this.ExportTestResultToCSV.Text = "Export to CSV";
            this.ExportTestResultToCSV.Click += new System.EventHandler(this.ExportTestResultToCSV_Click);
            // 
            // DataControlContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.MainTabControl);
            this.Name = "DataControlContainer";
            this.Size = new System.Drawing.Size(602, 581);
            this.MainTabControl.ResumeLayout(false);
            this.TestDetailsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TestResultDataGridView)).EndInit();
            this.SubcommandsTab.ResumeLayout(false);
            this.DirectRamTab.ResumeLayout(false);
            this.DataMemoryReadTab.ResumeLayout(false);
            this.DataMemoryPGridTab.ResumeLayout(false);
            this.DataMemoryWriteTab.ResumeLayout(false);
            this.ContextMenu.ResumeLayout(false);
            this.PrevDMTab.ResumeLayout(false);
            this.TestResultContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage SubcommandsTab;
        private System.Windows.Forms.PropertyGrid SubcommandsPGrid;
        private System.Windows.Forms.TabPage DirectRamTab;
        private System.Windows.Forms.TabPage DataMemoryReadTab;
        private RamComponent ReadRamComponent;
        private System.Windows.Forms.ContextMenuStrip ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ImportDataMemoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportDataMemoryToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid DirectRamPGrid;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.TabPage DataMemoryPGridTab;
        private System.Windows.Forms.PropertyGrid DataMemoryPGrid;
        private System.Windows.Forms.TabPage DataMemoryWriteTab;
        private RamComponent WriteRamComponent;
        private System.Windows.Forms.TabPage TestDetailsTab;
        private System.Windows.Forms.DataGridView TestResultDataGridView;
        private System.Windows.Forms.ContextMenuStrip TestResultContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ExportTestResultToCSV;
        private System.Windows.Forms.TabPage PrevDMTab;
        private RamComponent PrevDMRamComponent;
    }
}
