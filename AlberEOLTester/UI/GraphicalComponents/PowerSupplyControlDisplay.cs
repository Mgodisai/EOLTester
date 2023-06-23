using AlberEOL.CustomClasses;
using AlberEOL.Devices;
using AlberEOL.Exceptions;
using AlberEOL.Properties;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using VISA;

namespace AlberEOL.UI.GraphicalComponents
{
    public partial class PowerSupplyControlDisplay : UserControl
    {
        Cpx400sp CPX;
        private string ReadBackVoltage;
        private string ReadBackCurrent;
        private string OutputVoltage;
        private string OutputCurrentLimit;
        private int OutputStatus;


        public PowerSupplyControlDisplay()
        {
            InitializeComponent();
            CPX = new Cpx400sp();
        }

        public void SetPowerSupply()
        {
            CPX.PropertyChanged += CPX_PropertyChanged;
            DeviceStatusTextBox.Text = CPX.Status.ToString();
            DeviceAddressTextBox.Text = CPX.Address;
        }

        public void ClosePowerSupply()
        {
            CPX.Close();
        }

        private void CPX_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string id;
            lock (CPX)
            {
                CPX.Command(CPX.GetCommand(Cpx400Function.Identification), out id);
            }

            InvokeGuiThread(() =>
            {
                DeviceStatusTextBox.Text = CPX.Status.ToString();
                DeviceAddressTextBox.Text = CPX.Address;
                StatusPictureBox.BackgroundImage = CPX.Status == VISA_Device_STATUS.PORT_CONFIGURED ? Resources.usb_conn : Resources.usb_disconn;
                if (CPX.Status == VISA_Device_STATUS.PORT_CONFIGURED)
                {
                    if (!ReadBackDataWorker.IsBusy) ReadBackDataWorker.RunWorkerAsync();
                }
                else
                {
                    if (ReadBackDataWorker.IsBusy) ReadBackDataWorker.CancelAsync();
                }
            });


        }

        private void ReadBackDataWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DeviceStatusTextBox.Text = CPX.Status.ToString();
            int waitTime = 50;
            string readBackVoltage, voltage, readBackCurrent, current, outputStatus;
            while (!ReadBackDataWorker.CancellationPending)
            {

                //ReadBackVoltage
                lock (CPX)
                {
                    CPX.Command(CPX.GetCommand(Cpx400Function.GetReadBackVoltage), out readBackVoltage);
                }
                ReadBackVoltage = readBackVoltage;
                Thread.Sleep(waitTime);

                //OutputVoltage
                lock (CPX)
                {
                    CPX.Command(CPX.GetCommand(Cpx400Function.GetVoltage), out voltage);
                }
                OutputVoltage = voltage;
                Thread.Sleep(waitTime);

                //ReadBackCurrent
                lock (CPX)
                {
                    CPX.Command(CPX.GetCommand(Cpx400Function.GetReadBackCurrent), out readBackCurrent);
                }
                ReadBackCurrent = readBackCurrent;
                Thread.Sleep(waitTime);

                //OutputCurrent
                lock (CPX)
                {
                    CPX.Command(CPX.GetCommand(Cpx400Function.GetCurrentLimit), out current);
                }
                OutputCurrentLimit = current;
                Thread.Sleep(waitTime);

                //OutputStatus
                lock (CPX)
                {
                    CPX.Command(CPX.GetCommand(Cpx400Function.GetOutputStatus), out outputStatus);
                }
                OutputStatus = int.Parse(outputStatus);
                Thread.Sleep(waitTime);

                ReadBackDataWorker.ReportProgress(0);
            }
        }

        private void ReadBackDataWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InvokeGuiThread(() =>
            {
                VoltageReadBackTextBox.Text = ReadBackVoltage;
                CurrentReadBackTextBox.Text = ReadBackCurrent;
                VoltageSettingsTextBox.Text = OutputVoltage;
                CurrentSettingsTextBox.Text = OutputCurrentLimit;
                OnStatePictureBox.BackgroundImage = OutputStatus == 0 ? Resources.switch_off : Resources.switch_on;
            });
        }

        #region Invoke
        private void InvokeGuiThread(Action action)
        {
            this.BeginInvoke(action);
        }

        #endregion

        private void DeviceDataGroup_Enter(object sender, EventArgs e)
        {

        }

        private void OnStatePictureBox_Click(object sender, EventArgs e)
        {

        }

        private void StatusPictureBox_Click(object sender, EventArgs e)
        {
            if (CPX != null && CPX.Status == VISA_Device_STATUS.CLASS_INITIALIZED)
            {
                try
                {
                    CPX.Open(CPX.Address, PORT_TYPE.SERIAL);
                    CPX.Command(CPX.GetCommand(Cpx400Function.Reset));
                    Logger.WriteGeneralLog("CPX initialized successful!", "CPX");
                }
                catch (Exception ex)
                {
                    StationException stationException = new StationException("CPX inicializálási hiba", ex);
                    ex.Source = "CPX";
                    Logger.WriteExceptionLog($"CPX inicializálási hiba: {ex.Message}", ex.Source);
                    throw stationException;
                }
            }
            else
            {
                if (CPX != null)
                {
                    CPX = null;
                }
                CPX = new Cpx400sp();
                try
                {
                    CPX.Open(CPX.Address, PORT_TYPE.SERIAL);
                    CPX.Command(CPX.GetCommand(Cpx400Function.Reset));
                    Logger.WriteGeneralLog("CPX initialized successful!", "CPX");
                }
                catch (Exception ex)
                {
                    StationException stationException = new StationException("CPX inicializálási hiba", ex);
                    ex.Source = "CPX";
                    Logger.WriteExceptionLog($"CPX inicializálási hiba: {ex.Message}", ex.Source);
                    throw stationException;
                }
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            SetPowerSupply();
            CPX.Open(CPX.Address, PORT_TYPE.SERIAL);
        }
    }
}
