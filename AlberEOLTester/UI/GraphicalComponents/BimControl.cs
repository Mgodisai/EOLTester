using AlberEOL.Devices;
using System;
using System.Windows.Forms;

namespace AlberEOL.UI.GraphicalComponents
{
    public partial class BimControl : UserControl
    {
        BIM BIM;
        public BimControl()
        {
            InitializeComponent();
        }

        public void SetDevice(BIM Bim)
        {
            BIM = Bim;
            BIM.BimResultChange += BIM_BimResultChange;
        }

        private void BIM_BimResultChange(object sender, BimEventArgs e)
        {
            InvokeGuiThread(() =>
            {
                ImpedanceTextBox.Texts = e.Data.Impedance.ToString();
                VoltageTextBox.Texts = e.Data.Voltage.ToString();
            });
        }

        public void Close()
        {
            BIM.BimResultChange -= BIM_BimResultChange;
            BIM = null;
        }

        private void InvokeGuiThread(Action action)
        {
            this.BeginInvoke(action);
        }
    }
}
