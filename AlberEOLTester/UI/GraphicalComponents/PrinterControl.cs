using AlberEOL.Devices;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AlberEOL.UI.GraphicalComponents
{
    public partial class PrinterControl : UserControl
    {
        CustomZebraPrinter CZP;
        public PrinterControl()
        {
            InitializeComponent();
        }

        public void SetDevice(CustomZebraPrinter czp)
        {
            this.CZP = czp;
            CZP.PropertyChanged += CZP_PropertyChanged;
        }

        private void CZP_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvokeGuiThread(() =>
            {
                MessageTextBox.Texts = CZP.Message;
            });
        }

        public void Close()
        {
            CZP.PropertyChanged -= CZP_PropertyChanged;
            CZP = null;
        }

        private void InvokeGuiThread(Action action)
        {
            this.BeginInvoke(action);
        }
    }
}
