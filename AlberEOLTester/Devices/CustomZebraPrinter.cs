using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Printer.Discovery;

namespace AlberEOL.Devices
{
    public enum CustomZebraPrinterStatus
    {
        ClassInitialized,
        ReadyToPrint,
        Paused,
        HeadOpen,
        PaperOut,
        OtherError
    }

    public class CustomZebraPrinter : INotifyPropertyChanged
    {
        public ZebraPrinter ZebraPrinter;

        private CustomZebraPrinterStatus status;
        public CustomZebraPrinterStatus Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                }
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        public CustomZebraPrinter()
        {
            ZebraPrinter = null;
            Status = CustomZebraPrinterStatus.ClassInitialized;
        }

        public bool Connect(string connectionString)
        {
            Connection connection = ConnectionBuilder.Build(connectionString);
            return Connect(connection);
        }

        public bool Connect(DiscoveredPrinter discoveredPrinter)
        {
            Connection connection = discoveredPrinter.GetConnection();
            return Connect(connection);
        }

        public bool Connect(Connection connection)
        {
            try
            {
                connection.Open();
                if (connection.Connected)
                {
                    ZebraPrinter = ZebraPrinterFactory.GetInstance(PrinterLanguage.ZPL, connection);
                    Message = "Printer connected!";
                    return true;
                }
                Message = "Printer not connected!";
                return false;
            }
            catch (ConnectionException ex)
            {
                Message = $"Error connecting to printer: {ex.Message}";
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                ZebraPrinter.Connection.Close();
                Message = "Printer disconnected!";
                Status = CustomZebraPrinterStatus.ClassInitialized;
            }
            catch (ConnectionException e)
            {
                Message = $"Error disconnecting from printer: {e.Message}";
            }
        }

        public bool VerifyConnection()
        {
            bool ok = false;
            try
            {
                if (!ZebraPrinter.Connection.Connected)
                {
                    Connect(ZebraPrinter.Connection);
                    ZebraPrinter.Connection.Open();
                    if (ZebraPrinter.Connection.Connected)
                        ok = true;
                }
                else ok = true;
            }
            catch (ConnectionException e)
            {
                Message = $"Unable to connect to printer: {e.Message}";
            }
            return ok;
        }


        public void CheckStatus(bool before)
        {
            PrinterStatus printerStatus = null;
            try
            {
                VerifyConnection();
                printerStatus = ZebraPrinter.GetCurrentStatus();
                if (!before)
                {
                    while (printerStatus.numberOfFormatsInReceiveBuffer > 0 && printerStatus.isReadyToPrint)
                    {
                        Thread.Sleep(500);
                        printerStatus = ZebraPrinter.GetCurrentStatus();
                    }
                }

            }
            catch (ConnectionException ex)
            {
                Message = $"Error getting status from printer: {ex.Message}";
                Status = CustomZebraPrinterStatus.OtherError;
            }

            if (printerStatus == null)
            {
                Message = $"Unable to get status.";
                Status = CustomZebraPrinterStatus.ClassInitialized;
                return;
            }

            if (printerStatus.isReadyToPrint)
            {
                Message = $"Ready To Print";
                Status = CustomZebraPrinterStatus.ReadyToPrint;
            }
            else if (printerStatus.isPaused)
            {
                Message = $"Cannot Print because the printer is paused.";
                Status = CustomZebraPrinterStatus.Paused;
            }
            else if (printerStatus.isHeadOpen)
            {
                Message = $"Cannot Print because the printer head is open.";
                Status = CustomZebraPrinterStatus.HeadOpen;
            }
            else if (printerStatus.isPaperOut)
            {
                Message = $"Cannot Print because the paper is out.";
                Status = CustomZebraPrinterStatus.PaperOut;
            }
            else
            {
                Message = $"Cannot Print.";
                Status = CustomZebraPrinterStatus.OtherError;
            }
        }

        public bool Print(string printstring)
        {
            bool sent = false;
            try
            {
                if (VerifyConnection())
                {
                    ZebraPrinter.Connection.Write(Encoding.UTF8.GetBytes(printstring));
                    sent = true;

                };
            }
            catch (ConnectionException e)
            {
                sent = false;
                Message = $"Unable to write to printer: {e.Message}";
            }
            return sent;
        }

        public DiscoveredPrinter GetUSBPrinter()
        {
            DiscoveredPrinter discoveredPrinter = null;
            try
            {
                foreach (DiscoveredUsbPrinter usbPrinter in UsbDiscoverer.GetZebraUsbPrinters())
                {
                    discoveredPrinter = usbPrinter;
                }
            }
            catch (ConnectionException ex)
            {
                Message = $"Error discovering local printers: {ex.Message}";
            }
            return discoveredPrinter;
        }

        public async Task PrintUSBTask(string ZPL_STRING)
        {
            await Task.Run(() =>
            {
                VerifyConnection();
                CheckStatus(true);
                if (Status == CustomZebraPrinterStatus.ReadyToPrint)
                {
                    Message = "Trying to print...";
                    Print(ZPL_STRING);
                    CheckStatus(false);
                    if (Status == CustomZebraPrinterStatus.ReadyToPrint)
                    {
                        Message = $"Label Printed";
                    }
                }
                else
                {
                    Message = $"Cannot print, Reason: {Status}";
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
