using AlberEOL.CustomClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace AlberEOL.Devices
{
    public class BimResult
    {
        public double Temperature { get; set; }
        public double Impedance { get; set; }
        public bool ResultOk { get; set; }
        public double Voltage { get; set; }
    }

    public enum BimCommandType
    {
        Init = 0,
        Mode_100Hz,
        Mode_1kHz,
        Single,
        Trigger0,
        Trigger1,
        Trigger2
    }

    public enum BimStatus
    {
        NON_INITIALIZED = 0,    // default when starting
        CLASS_INITIALIZED,    // constructor was success
        PORT_CONFIGURED,    // port configuration was success
    }

    public class BIM
    {
        private SerialPort SerialPort;
        private string port;
        private int baud;
        private Parity parity;
        private int databit;
        private StopBits stopbit;

        private List<string> BimCommands;
        public BimStatus Status = BimStatus.NON_INITIALIZED;

        public BIM(string _port, int _baud, int _databit, StopBits _stopbit, Parity _parity) : base()
        {

            port = _port;
            baud = _baud;
            databit = _databit;
            stopbit = _stopbit;
            parity = _parity;
        }

        public BIM()
        {
            BimCommands = new List<string>();
            // BIM parancsok
            BimCommands.Add("\x02@RI0,1,-,1,0,31.0,41.0,0,0,0,-,6\r\n\x03");  // Init
            BimCommands.Add("\x02@RM0,4,-,5\r\n\x03");                      // Impedance 100 Hz
            BimCommands.Add("\x02@RM0,4,-,0\r\n\x03");                      // Impedance 1 KHz
            BimCommands.Add("\x02@RG0,1,-,1\r\n\x03");                      // Single mode
            BimCommands.Add("\x02@RG1,0\r\n\x03");                          // Trigger0
            BimCommands.Add("\x02@RG1,1\r\n\x03");                          // Trigger1
            BimCommands.Add("\x02@RG1,2\r\n\x03");                          // Trigger2
            Status = BimStatus.CLASS_INITIALIZED;
        }

        public void InitBim(string _port, int _baud, int _databit, StopBits _stopbit, Parity _parity)
        {
            port = _port;
            baud = _baud;
            databit = _databit;
            stopbit = _stopbit;
            parity = _parity;
        }

        public bool PortOpen()
        {
            try
            {
                SerialPort = new SerialPort(port, baud, parity, databit, stopbit);

                SerialPort.ReadTimeout = 10000;
                SerialPort.Open();
                Status = BimStatus.PORT_CONFIGURED;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PortClose()
        {
            try
            {
                SerialPort.Close();
                Status = BimStatus.CLASS_INITIALIZED;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public BimResult GetResult(BimCommandType command)
        {
            CommandToBim(BimCommands[(int)command]);

            return GetFromBim(command);
        }

        public BimResult GetFromBim(BimCommandType command)
        {
            string startString = "";
            string endString = "";

            string getString = "";

            bool cont = true;
            bool listStarted = false;

            List<string> rows = new List<string>();

            /*
            Hőmérséklet: A paraméter, impedancia absolút érték: Be paraméter
            @$ D0,BIM2,V: 10.4878,Qv: 0,A: 22.1638,Qa: 0,VCE: 0,VC: 0,OD: 0,B: 1,-,S: 1
            @0 Re: 48.7223, Im: -2.1367, Be: 48.7691, Ph: -2.5111
            */

            string pattern_temperature = @"[A][:]\s{1,}\d{1,}[.]\d{1,}";
            string pattern_impedance = @"[R][e][:]\s{1,}\d{1,}[.]\d{1,}";
            string pattern_numeric = @"\d{1,}\.{1}\d{1,}";
            string pattern_voltage = @"[V][:]\s{1,}\d{1,}[.]\d{1,}";

            switch (command)
            {
                case BimCommandType.Init:
                    startString = "";
                    endString = "[M][!]";
                    break;
                case BimCommandType.Mode_100Hz:
                    startString = "";
                    endString = "[M][!]";
                    break;
                case BimCommandType.Mode_1kHz:
                    startString = "";
                    endString = "[M][!]";
                    break;
                case BimCommandType.Single:
                    startString = "";
                    endString = "[M][!]";
                    break;
                case BimCommandType.Trigger0:
                case BimCommandType.Trigger1:
                case BimCommandType.Trigger2:
                    startString = "[M][!]";
                    endString = "MF";
                    break;
            }

            while (cont)
            {
                getString = SerialPort.ReadLine();
                Logger.WriteGeneralLog(getString, "BIM");
                if (Regex.IsMatch(getString, startString))
                {
                    listStarted = true;
                }

                if (listStarted)
                {
                    rows.Add(getString);
                }

                if (Regex.IsMatch(getString, endString))
                {
                    cont = false;

                    listStarted = false;
                }
            }

            BimResult Result = new BimResult();

            foreach (string row in rows)
            {
                Match m;

                if (command != BimCommandType.Trigger0 && command != BimCommandType.Trigger1 && command != BimCommandType.Trigger2)
                {
                    Result.ResultOk = true;
                }
                else
                {
                    m = Regex.Match(row, pattern_temperature);
                    double temp;
                    if (double.TryParse(Regex.Match(m.Value, pattern_numeric).Value, NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                    {
                        Result.Temperature = temp;
                    }

                    m = Regex.Match(row, pattern_impedance);
                    double imp;
                    if (double.TryParse(Regex.Match(m.Value, pattern_numeric).Value, NumberStyles.Any, CultureInfo.InvariantCulture, out imp))
                    {
                        Result.Impedance = imp;
                    }

                    m = Regex.Match(row, pattern_voltage);
                    double voltage;
                    if (double.TryParse(Regex.Match(m.Value, pattern_numeric).Value, NumberStyles.Any, CultureInfo.InvariantCulture, out voltage))
                    {
                        Result.Voltage = voltage;
                    }

                    Result.ResultOk = true;

                    if (Result.Temperature != 0)
                    {
                        //    break;
                    }
                }
            }
            var e = new BimEventArgs(Result);
            OnResultChanging(e);

            return Result;
        }

        public void CommandToBim(string commandString)
        {
            SerialPort.DiscardInBuffer();
            SerialPort.Write(commandString);
        }

        public event EventHandler<BimEventArgs> BimResultChange;

        protected void OnResultChanging(BimEventArgs e)
        {
            BimResultChange?.Invoke(this, e);
        }
    }



    public class BimEventArgs : EventArgs
    {
        private BimResult bimResult;
        public BimEventArgs(BimResult bimResultArg)
        {
            bimResult = bimResultArg;
        }
        public BimResult Data { get { return bimResult; } }
    }
}
