using Alber.Eol.Hardware;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AlberEOL.UI.GraphicalComponents
{
    public partial class RelayControlContainer : UserControl
    {
        CommInterface COM;
        public RelayControlContainer()
        {
            InitializeComponent();
            SetupControl();
        }

        private void SetupControl()
        {
            Relay_2Ohm.ControlLabelText = "2 Ohm";
            Relay_1Ohm.ControlLabelText = "1 Ohm";
            Relay_0_5Ohm.ControlLabelText = "0,5 Ohm";
            Relay_Chg.ControlLabelText = "Charge";
        }

        public void SetCommInterface(CommInterface commInterface)
        {
            COM = commInterface;
            COM.PropertyChanged += CommInterface_PropertyChanged;
        }

        public void Close()
        {
            COM.PropertyChanged -= CommInterface_PropertyChanged;
            COM = null;
        }

        private void CommInterface_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvokeGuiThread(() =>
            {
                switch (e.PropertyName)
                {
                    case "RelayStatus":
                        RelayStatus status = COM.RelayStatus;
                        SetAllRelayOff();
                        switch (status)
                        {
                            case RelayStatus.AllOFF:
                                break;
                            case RelayStatus.Load2Ohm:
                                Relay_2Ohm.BackColor = Color.Lime;
                                break;
                            case RelayStatus.Load1Ohm:
                                Relay_1Ohm.BackColor = Color.Lime;
                                break;
                            case RelayStatus.Load0_5Ohm:
                                Relay_0_5Ohm.BackColor = Color.Lime;
                                break;
                            case RelayStatus.Charge:
                                Relay_Chg.BackColor = Color.Lime;
                                break;
                        }
                        break;
                }
            });
        }

        private void SetAllRelayOff()
        {
            foreach (RelayControl item in RelaysGroupBox.Controls)
            {
                item.BackColor = Color.SlateGray;
            }
        }

        private void InvokeGuiThread(Action action)
        {
            this.BeginInvoke(action);
        }
    }
}
