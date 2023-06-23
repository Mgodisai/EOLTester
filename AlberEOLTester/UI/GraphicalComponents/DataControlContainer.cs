using AlberEOL.Base;
using AlberEOL.Station;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTEP.TI.BatteryManagement.BQ76942_769142_76952;

namespace AlberEOL.UI.GraphicalComponents
{
    public partial class DataControlContainer : UserControl
    {
        private BQ76942_769142_76952 BMS;
        protected StationBase Station { get; private set; }
        public DataControlContainer()
        {
            InitializeComponent();
        }
        public void SetCompontents(BQ76942_769142_76952 bms, StationBase station)
        {
            this.BMS = bms;
            this.Station = station;
            Station.PropertyChanged += Station_PropertyChanged;
            BMS.Subcommands.PropertyChanged += Subcommands_PropertyChanged;
            BMS.DataMemory.PropertyChanged += DataMemory_PropertyChanged;
            BMS.DirectRam.PropertyChanged += DirectRam_PropertyChanged;

            SubcommandsPGrid.SelectedObject = BMS.Subcommands;
            DirectRamPGrid.SelectedObject = BMS.DirectRam;
            DataMemoryPGrid.SelectedObject = BMS.DataMemory;

            ReadRamComponent.Init(DataMemory.START, BMS.DataMemory.RamRead);
            WriteRamComponent.Init(DataMemory.START, BMS.DataMemory.RamWrite);
            //PrevDMRamComponent.Init(DataMemory.START, ((AlberEOLStation)station).PrevDataMemory);
            WriteRamComponent.ContextMenuStrip = ContextMenu;
            TestResultDataGridView.ContextMenuStrip = TestResultContextMenu;
            InitDataGridView();
        }

        public void CloseControl()
        {
            Station.PropertyChanged -= Station_PropertyChanged;
            BMS.Subcommands.PropertyChanged -= Subcommands_PropertyChanged;
            BMS.DataMemory.PropertyChanged -= DataMemory_PropertyChanged;
            BMS.DirectRam.PropertyChanged -= DirectRam_PropertyChanged;
            this.BMS = null;
            this.Station = null;
        }


        private void Station_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StationBase AlberEOLStation = (StationBase)sender;
            InvokeGuiThread2(() =>
            {
                switch (e.PropertyName)
                {
                    case "LastTestDetail":
                        if (AlberEOLStation.LastTestDetail != null)
                        {
                            TestResultDataGridView.Rows.Add(AlberEOLStation.LastTestDetail.GetDataGridViewRow());
                            TestResultDataGridView.FirstDisplayedScrollingRowIndex = TestResultDataGridView.RowCount - 1;
                        }
                        else
                        {
                            TestResultDataGridView.Rows.Clear();
                        }
                        break;
                }
            });
        }

        private void DirectRam_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvokeGuiThread(() =>
            {
                DirectRamPGrid.Refresh();
            });
        }

        private void Subcommands_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvokeGuiThread(() =>
            {
                SubcommandsPGrid.Refresh();
            });
        }

        private void DataMemory_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvokeGuiThread(() =>
            {
                switch (e.PropertyName)
                {
                    case (nameof(BMS.DataMemory.RamRead)):
                        DataMemoryPGrid.Refresh();
                        ReadRamComponent.UpdateRam(BMS.DataMemory.RamRead, null);
                        ReadRamComponent.Refresh();
                        BMS.DataMemory.CopyReadToWrite();
                        WriteRamComponent.UpdateRam(BMS.DataMemory.RamWrite, BMS.DataMemory.RegistersToWrite);
                        WriteRamComponent.Refresh();
                        PrevDMRamComponent.UpdateRam(((AlberEOLStation)Station).PrevDataMemory, ((AlberEOLStation)Station).MemDiffs);
                        PrevDMRamComponent.Refresh();
                        break;
                    case nameof(BMS.DataMemory.RamWrite):
                        WriteRamComponent.UpdateRam(BMS.DataMemory.RamWrite, BMS.DataMemory.RegistersToWrite);
                        WriteRamComponent.Refresh();
                        if (((AlberEOLStation)Station).PrevDataMemory != null)
                        {
                            PrevDMRamComponent.Init(DataMemory.START, ((AlberEOLStation)Station).PrevDataMemory);
                            PrevDMRamComponent.UpdateRam(((AlberEOLStation)Station).PrevDataMemory, ((AlberEOLStation)Station).MemDiffs);
                            PrevDMRamComponent.Refresh();
                        }

                        break;
                    default:
                        break;
                }
            });
        }

        #region ToolStripMenu
        private void ImportDataMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Binary Files | *.bin",
                DefaultExt = "bin"
            };
            DialogResult dr = ofd.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    break;
                default:
                    return;
            }
            byte[] readData = File.ReadAllBytes(ofd.FileName);
            BMS.DataMemory.ImportImage(readData);
            WriteRamComponent.Init(DataMemory.START, readData);
            DataMemoryPGrid.Refresh();
        }

        private void ExportDataMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Binary Files | *.bin",
                DefaultExt = "bin"
            };
            DialogResult dr = sfd.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    break;
                default:
                    return;
            }
            File.WriteAllBytes(sfd.FileName, BMS.DataMemory.RamWrite);
        }
        #endregion

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteRamComponent.UpdateRam(BMS.DataMemory.RamWrite, null);
            WriteRamComponent.Refresh();
        }

        private void InvokeGuiThread(Action action)
        {
            this.BeginInvoke(action);
        }

        private void InvokeGuiThread2(Action action)
        {
            this.Invoke(action);
        }

        private void ExportTestResultToCSV_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            var headers = TestResultDataGridView.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

            foreach (DataGridViewRow row in TestResultDataGridView.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                sb.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV Files | *.csv",
                DefaultExt = "csv"
            };
            DialogResult dr = sfd.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                case DialogResult.Yes:
                    break;
                default:
                    return;
            }
            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
        }


        private void TestResultDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (TestResultDataGridView.Columns[e.ColumnIndex].Name == "Passed")
            {
                e.Value = (e.Value.ToString() == "True") ? Properties.Resources.active : Properties.Resources.deactivate;
            }
        }

        private void InitDataGridView()
        {
            DataGridViewTextBoxColumn paramName = new DataGridViewTextBoxColumn();
            paramName.Name = "ParamName";
            paramName.HeaderText = "Param";
            paramName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            paramName.DataPropertyName = "TestDetail.ParamName";
            paramName.FillWeight = 0.27F;

            DataGridViewTextBoxColumn value = new DataGridViewTextBoxColumn();
            value.Name = "Value";
            value.HeaderText = "Mért érték";
            value.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            value.DataPropertyName = "TestDetail.MeasuredValue";
            value.FillWeight = 0.28F;

            DataGridViewTextBoxColumn unit = new DataGridViewTextBoxColumn();
            unit.Name = "Unit";
            unit.HeaderText = "ME";
            unit.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            unit.DataPropertyName = "TestDetail.UnitOfMeasure";
            unit.FillWeight = 0.075F;

            DataGridViewTextBoxColumn min = new DataGridViewTextBoxColumn();
            min.Name = "Min";
            min.HeaderText = "Min";
            min.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            min.DataPropertyName = "TestDetail.Min";
            min.FillWeight = 0.1F;

            DataGridViewTextBoxColumn max = new DataGridViewTextBoxColumn();
            max.Name = "Max";
            max.HeaderText = "Max";
            max.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            max.DataPropertyName = "TestDetail.Max";
            max.FillWeight = 0.1F;

            DataGridViewTextBoxColumn nominal = new DataGridViewTextBoxColumn();
            nominal.Name = "Nominal";
            nominal.HeaderText = "Nominal";
            nominal.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nominal.DataPropertyName = "TestDetail.Nominal";
            nominal.FillWeight = 0.15F;

            DataGridViewImageColumn passed = new DataGridViewImageColumn();
            passed.Name = "Passed";
            passed.HeaderText = "?";
            passed.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            passed.DataPropertyName = "TestDetail.Passed";
            passed.FillWeight = 0.05F;

            TestResultDataGridView.Columns.Add(paramName);
            TestResultDataGridView.Columns.Add(value);
            TestResultDataGridView.Columns.Add(unit);
            TestResultDataGridView.Columns.Add(min);
            TestResultDataGridView.Columns.Add(max);
            TestResultDataGridView.Columns.Add(nominal);
            TestResultDataGridView.Columns.Add(passed);
        }
    }

}
