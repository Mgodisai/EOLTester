using AlberEOL.Devices;
using AlberEOL.Properties;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using VISA;

namespace AlberEOL.UI.GraphicalComponents
{
    public partial class PowerSupplyDisplay : UserControl
    {
        Cpx400sp CPX;

        public PowerSupplyDisplay()
        {
            InitializeComponent();
        }

        public void SetPowerSupply(Cpx400sp cpx)
        {
            this.CPX = cpx;
            CPX.PropertyChanged += CPX_PropertyChanged;
            DeviceStatusTextBox.Text = CPX.Status.ToString();
            DeviceAddressTextBox.Text = CPX.Address;
        }

        public void CloseControl()
        {
            this.ReadBackDataWorker.CancelAsync();
            Thread.Sleep(200);
            CPX.PropertyChanged -= CPX_PropertyChanged;
            this.CPX = null;
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
            while (true)
            {
                //ReadBackVoltage
                if (!ReadBackDataWorker.CancellationPending)
                {

                    lock (CPX)
                    {
                        CPX.Command(CPX.GetCommand(Cpx400Function.GetReadBackVoltage), out readBackVoltage);
                    }
                    InvokeGuiThread(() =>
                    {
                        VoltageReadBackTextBox.Text = readBackVoltage;
                    });
                    Thread.Sleep(waitTime);
                }

                //OutputVoltage
                if (!ReadBackDataWorker.CancellationPending)
                {
                    lock (CPX)
                    {
                        CPX.Command(CPX.GetCommand(Cpx400Function.GetVoltage), out voltage);
                    }
                    InvokeGuiThread(() =>
                    {
                        VoltageSettingsTextBox.Text = voltage;
                    });
                    Thread.Sleep(waitTime);
                }

                //ReadBackCurrent
                if (!ReadBackDataWorker.CancellationPending)
                {
                    lock (CPX)
                    {
                        CPX.Command(CPX.GetCommand(Cpx400Function.GetReadBackCurrent), out readBackCurrent);
                    }
                    InvokeGuiThread(() =>
                    {
                        CurrentReadBackTextBox.Text = readBackCurrent;
                    });
                    Thread.Sleep(waitTime);
                }

                //OutputCurrent
                if (!ReadBackDataWorker.CancellationPending)
                {
                    lock (CPX)
                    {
                        CPX.Command(CPX.GetCommand(Cpx400Function.GetCurrentLimit), out current);
                    }
                    InvokeGuiThread(() =>
                    {
                        CurrentSettingsTextBox.Text = current;
                    });
                    Thread.Sleep(waitTime);
                }

                //OutputStatus
                if (!ReadBackDataWorker.CancellationPending)
                {
                    lock (CPX)
                    {
                        CPX.Command(CPX.GetCommand(Cpx400Function.GetOutputStatus), out outputStatus);
                    }
                    OnStatePictureBox.BackgroundImage = outputStatus == "0" ? Resources.switch_off : Resources.switch_on;
                    Thread.Sleep(waitTime);
                }
                if (!ReadBackDataWorker.CancellationPending)
                {
                    ReadBackDataWorker.ReportProgress(0);
                }
            }
        }


        #region Invoke
        private void InvokeGuiThread(Action action)
        {
            this.BeginInvoke(action);
        }

        #endregion
    }
}
