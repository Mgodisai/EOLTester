using Alber.Eol.Hardware;
using AlberEOL.Base;
using AlberEOL.Devices;
using AlberEOL.Properties;
using AlberEOL.Station;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VTEP.TI.BatteryManagement.BQ76942_769142_76952;
using static VTEP.TI.BatteryManagement.BQ76942_769142_76952.DataMemory;

namespace AlberEOL.UI.GraphicalComponents
{
    public partial class SettingsControl : UserControl
    {
        private Tester Tester;
        private AlberEOLStation Station;
        private CommInterface COM;
        private BQ76942_769142_76952 BMS;
        private Cpx400sp CPX;
        public SettingsControl()
        {
            InitializeComponent();
        }

        public void SetStation(Tester tester)
        {
            Tester = tester;
            Station = (AlberEOLStation)Tester.AlberEOLStation;
            COM = Station.CommInterface;
            BMS = COM.BMS;
            CPX = Station.CPX;
            Tester.PropertyChanged += Tester_PropertyChanged;
            COM.PropertyChanged += COM_PropertyChanged;
            OperationModeTextBox.Texts = Tester.OperationMode.ToString();
            TestStepsListBox.DataSource = Enum.GetValues(typeof(TestStep));
            SetTestSteps();
        }

        private void Station_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Action action = () =>
            {
                switch (e.PropertyName)
                {
                    case "State":
                        this.OperationModeTextBox.Texts = Tester.OperationMode.ToString();
                        break;
                }
            };
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        public void Close()
        {
            ServiceTimer.Stop();
            ServiceTimer.Dispose();
            SaveActualTestSteps();
            COM.PropertyChanged -= COM_PropertyChanged;
            Tester.PropertyChanged -= Tester_PropertyChanged;
            this.BMS = null;
            this.COM = null;
            this.Station = null;
        }

        private void SetAllRelaysButtonStateOff()
        {
            foreach (Button item in RelaysGroupBox.Controls)
            {
                item.BackColor = Color.MediumSlateBlue;
            }
        }

        private void SaveActualTestSteps()
        {
            TestStep number = 0;
            for (int i = 0; i < TestStepsListBox.Items.Count; i++)
            {
                if (TestStepsListBox.GetItemChecked(i) == true)
                {
                    number |= (TestStep)TestStepsListBox.Items[i];
                }
            }
            Settings.Default.TestNumber = (int)number;
            Settings.Default.Save();
        }

        private void SetTestSteps()
        {
            TestStep testNumber = (TestStep)Settings.Default.TestNumber;

            for (int i = 0; i < TestStepsListBox.Items.Count; i++)
            {
                TestStep number = (TestStep)TestStepsListBox.Items[i];
                if ((testNumber & number) != TestStep.None)
                {
                    TestStepsListBox.SetItemChecked(i, true);
                }
            }
        }

        #region EventHandlers
        private void COM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Action action = () =>
            {
                switch (e.PropertyName)
                {
                    case "RelayStatus":
                        RelayStatus status = COM.RelayStatus;
                        SetAllRelaysButtonStateOff();
                        switch (status)
                        {
                            case RelayStatus.AllOFF:
                                break;
                            case RelayStatus.Load2Ohm:
                                RelayTwoButton.BackColor = Color.Lime;
                                break;
                            case RelayStatus.Load1Ohm:
                                RelayOneButton.BackColor = Color.Lime;
                                break;
                            case RelayStatus.Load0_5Ohm:
                                RelayHalfButton.BackColor = Color.Lime;
                                break;
                            case RelayStatus.Charge:
                                RelayChargeButton.BackColor = Color.Lime;
                                break;
                        }
                        break;
                }
            };
            if (this != null)
            {
                this.BeginInvoke(action);
            }

        }

        private void Tester_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Action action = () =>
            {
                switch (e.PropertyName)
                {
                    case "OperationMode":
                        this.OperationModeTextBox.Texts = Tester.OperationMode.ToString();
                        break;
                }
            };
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action();
            }

        }

        private void ManualButton_Click(object sender, EventArgs e)
        {
            Tester.SetManualMode(true);
        }

        private void StandAloneButton_Click(object sender, EventArgs e)
        {
            Tester.SetManualMode(false);
        }


        private void RelayTwoButton_Click(object sender, EventArgs e)
        {
            if (COM.RelayStatus == RelayStatus.Load2Ohm)
            {
                COM.RelayStatus = RelayStatus.AllOFF;
            }
            else
            {
                COM.RelayStatus = RelayStatus.Load2Ohm;
            }
        }

        private void RelayOneButton_Click(object sender, EventArgs e)
        {
            if (COM.RelayStatus == RelayStatus.Load1Ohm)
            {
                COM.RelayStatus = RelayStatus.AllOFF;
            }
            else
            {
                COM.RelayStatus = RelayStatus.Load1Ohm;
            }
        }

        private void RelayHalfButton_Click(object sender, EventArgs e)
        {
            if (COM.RelayStatus == RelayStatus.Load0_5Ohm)
            {
                COM.RelayStatus = RelayStatus.AllOFF;
            }
            else
            {
                COM.RelayStatus = RelayStatus.Load0_5Ohm;
            }
        }

        private void RelayChargeButton_Click(object sender, EventArgs e)
        {
            if (COM.RelayStatus == RelayStatus.Charge)
            {
                COM.RelayStatus = RelayStatus.AllOFF;
            }
            else
            {
                COM.RelayStatus = RelayStatus.Charge;
            }
        }

        private void btnALL_FETS_OFF_Click(object sender, EventArgs e)
        {
            COM.BMS.Subcommands.ALL_FETS_OFF.Send();
        }

        private void btnALL_FETS_ON_Click(object sender, EventArgs e)
        {
            COM.BMS.Subcommands.ALL_FETS_ON.Send();
        }

        private void DischargeFETOffButton_Click(object sender, EventArgs e)
        {
            COM.BMS.Subcommands.DSG_PDSG_OFF.Send();
        }

        private void ServiceGroupBox_Leave(object sender, EventArgs e)
        {
            SaveActualTestSteps();
        }

        private void TimerButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ServiceTimer.Enabled)
            {
                ServiceTimer.Stop();
            }
            else
            {
                ServiceTimer.Start();
            }
        }

        private void ServiceTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                BMS.DirectRam.ReadAll();
            }
            catch (IOException)
            {
                Action action = () =>
                {
                    TimerButton.Checked = false;
                };
                this.BeginInvoke(action);
                MessageBox.Show("Kommunikációs hiba, valószínűleg nincs kapcsolat a BMS-sel!");

            }

        }
        #endregion

        private void Charge3AButton_Click(object sender, EventArgs e)
        {

            string result;
            lock (CPX)
            {
                CPX.Command(CPX.GetCommand(Cpx400Function.GetOutputStatus), out result);
            }


            if (result == "1")
            {
                // Switch off Power Supply
                lock (CPX)
                {
                    CPX.Command(CPX.GetCommand(Cpx400Function.SetOutput), "0");
                }

                //Thread.Sleep(200);
            }
            else
            {
                lock (CPX)
                {
                    //Setup PowerSupply
                    CPX.Command(CPX.GetCommand(Cpx400Function.SetVoltage), "42");
                    Thread.Sleep(50);
                    CPX.Command(CPX.GetCommand(Cpx400Function.SetCurrentLimit), "3.0");
                    Thread.Sleep(50);
                    // Switch on Power Supply
                    CPX.Command(CPX.GetCommand(Cpx400Function.SetOutput), "1");
                    Thread.Sleep(50);
                }
            }
        }



        private void StopCPXButton_Click(object sender, EventArgs e)
        {
            lock (CPX)
            {
                CPX.Command(CPX.GetCommand(Cpx400Function.SetOutput), "0");
            }
        }

        private void DMResetButton_Click(object sender, EventArgs e)
        {
            BMS.Subcommands.RESET.Send();
        }

        private void DMReadAllButton_Click(object sender, EventArgs e)
        {
            BMS.DataMemory.ReadAll();
        }

        private void FDOffButton_Click(object sender, EventArgs e)
        {
            BMS.DataMemory.Read(DataMemoryRegister.Settings__Protection__Protection_Configuration);
            if (BMS.DataMemory.Settings__Protection__Protection_Configuration.Bit4)
            {
                BMS.DataMemory.Settings__Protection__Protection_Configuration.Bit4 = false;
                BMS.DataMemory.Write(new Block(DataMemoryRegister.Settings__Protection__Protection_Configuration, 1));
                BMS.DataMemory.Read(DataMemoryRegister.Settings__Protection__Protection_Configuration);
            }
        }

        private void ShutdownButton_Click(object sender, EventArgs e)
        {
            BMS.Subcommands.SHUTDOWN.Send();
        }

        private void TestStepsListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (TestStepsListBox.GetItemChecked(0))
            {
                for (int i = 0; i < TestStepsListBox.Items.Count; i++)
                {
                    TestStepsListBox.SetItemChecked(i, false);
                }
            }
        }
    }
}

