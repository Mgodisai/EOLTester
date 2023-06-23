using Ivi.Visa;
//using Keysight.Visa;
using System;

namespace VISA
{
    public enum VISA_Device_STATUS
    {
        NON_INITIALIZED = 0,    // default when starting
        CLASS_INITIALIZED,    // constructor was success
        SESSION_OPENED, // session open was success
        PORT_CONFIGURED,    // port configuration was success
    }

    public enum VISA_Device_ERROR
    {
        NONE = 0,
        READ_TIMEOUT,
        VISA_IO_ERROR,
        DEVICE_CONFIGURATION_ERROR
    }

    public enum DATA_TYPE
    {
        STR = 1,
        FL,
        FL_STR,
        STR_FL,
        DOU,
        LINE,
        BYTE,
        NOTHING
    }

    public enum PORT_TYPE
    {
        SERIAL = 1,
        USB,
        ETHERNET
    }

    /// <summary>
    /// Class for Multimeter
    /// </summary>
    public class VISA_Device
    {
        public event EventHandler<StatusEventArgs> StatusChanged;

        public class StatusEventArgs : EventArgs
        {
            public VISA_Device_STATUS Status { get; }

            public StatusEventArgs(VISA_Device_STATUS s)
            {
                Status = s;
            }
        }
        //Stopwatch st = new Stopwatch();
        private double _dResult;
        private float _fResult;
        private string _sResult;
        private string fCommMessage;
        //IMessageBasedSession session;
        //UsbSession usbSession;
        //SerialSession serialSession;
        IMessageBasedSession sersession;
        IMessageBasedSession session;
        //string porttype;
        //public volatile VISA_Device_STATUS VDStatus = VISA_Device_STATUS.NON_INITIALIZED;
        private VISA_Device_STATUS _vDStatus;
        public VISA_Device_STATUS VDStatus
        {
            get
            {
                return _vDStatus;
            }
            private set
            {
                if (_vDStatus != value)
                {
                    _vDStatus = value;
                    StatusChanged?.Invoke(this, new StatusEventArgs(value));
                }
            }
        }
        //public volatile VISA_Device_ERROR VDError = VISA_Device_ERROR.NONE;
        public VISA_Device_ERROR VDError { get; private set; }
        public volatile bool VDBusy = false;
        public byte TermChar { get; set; }

        //Result properties
        public double dResult
        {
            get { return _dResult; }
        }

        public float fResult
        {
            get { return _fResult; }
        }
        public string sResult
        {
            get { return _sResult; }
        }
        public string CommMessage
        {
            get { return fCommMessage; }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public VISA_Device()
        {
            VDStatus = VISA_Device_STATUS.CLASS_INITIALIZED;
        }

        /// <summary>
        /// Open session
        /// </summary>
        /// <param name="srcAddress"></param>
        public void Open(string srcAddress, PORT_TYPE PortType)
        {
            VDBusy = true;
            //fCommMessage = "success";
            //if (PortType == "USB")
            //{
            //    UsbPortSetting(srcAddress);
            //    if (VDStatus != VISA_Device_STATUS.PORT_CONFIGURED)
            //    {
            //        fCommMessage = "Mutimeter port open error";
            //    }
            //}
            //if (PortType == "SERIAL")
            //{
            //    SerialPortSetting(srcAddress);
            //    if (VDStatus != VISA_Device_STATUS.PORT_CONFIGURED)
            //    {
            //        fCommMessage = "Mutimeter port open error";
            //    }
            //}

            try
            {
                session = GlobalResourceManager.Open(srcAddress) as IMessageBasedSession;

                if (PortType == PORT_TYPE.ETHERNET)
                {
                    session.TerminationCharacterEnabled = true;
                    ITcpipSocketSession socket = session as ITcpipSocketSession;
                    socket.KeepAlive = false;
                    socket.NoDelay = false;
                }

                if (PortType == PORT_TYPE.SERIAL)
                {
                    ISerialSession serial = session as ISerialSession;
                    serial.BaudRate = 9600;
                    serial.DataBits = 8;
                    serial.Parity = SerialParity.None;
                    serial.StopBits = SerialStopBitsMode.One;
                    serial.FlowControl = SerialFlowControlModes.None;
                    if (TermChar == 0 || TermChar == 0xa)
                        serial.TerminationCharacter = 0xa;
                    if (TermChar == 0xd)
                        serial.TerminationCharacter = 0xd;
                    session.TerminationCharacterEnabled = true;
                }

                if (PortType == PORT_TYPE.USB)
                {
                    IUsbSession usb = session as IUsbSession;
                    usb.TerminationCharacter = 0xa;
                    usb.TerminationCharacterEnabled = true;
                }

                VDStatus = VISA_Device_STATUS.PORT_CONFIGURED;
            }
            catch (Exception)
            {
                VDStatus = VISA_Device_STATUS.CLASS_INITIALIZED;
            }

            VDBusy = false;
        }

        /// <summary>
        /// Setting serial communication port parameters
        /// </summary>
        public void SerialPortSetting(string Address) //set serial port
        {
            sersession = GlobalResourceManager.Open(Address) as IMessageBasedSession;
            sersession.TerminationCharacterEnabled = true;


            //ISerialSession serial = session as ISerialSession;
            //serial.BaudRate = 9600;
            //serial.DataBits = 8;
            //serial.Parity = SerialParity.None;
            //serial.FlowControl = SerialFlowControlModes.None;
            //serial.TimeoutMilliseconds = 2000;
            //session.TerminationCharacter = 0xd;
            //session.TerminationCharacterEnabled = true;

            // Multimeter
            VDBusy = true;
            fCommMessage = "success";
            //if (serialSession != null)
            //    Close();
            //serialSession = new SerialSession(Address, AccessModes.None, 2000)
            //{
            //    BaudRate = 9600,
            //    DataBits = 8,
            //    Parity = SerialParity.None,
            //    StopBits = SerialStopBitsMode.One,
            //    FlowControl = SerialFlowControlModes.None,
            //    TimeoutMilliseconds = 2000,
            //    TerminationCharacter = 0xa,
            //    TerminationCharacterEnabled = true
            //};
            VDStatus = VISA_Device_STATUS.PORT_CONFIGURED;
            VDBusy = false;
        }

        /// <summary>
        /// Setting USB communication port parameters
        /// </summary>
        void UsbPortSetting(string Address) //set usb port
        {
            //MultimeterStatus = Multimeter_STATUS.PORT_CONFIGURED;

            // Multimeter 
            VDBusy = true;
            fCommMessage = "success";
            /*if (usbSession != null)
                Close();
            usbSession = new UsbSession(Address, AccessModes.None, 2000)
            {
                TerminationCharacter = 0xa,
                TerminationCharacterEnabled = true
            };*/

            VDStatus = VISA_Device_STATUS.PORT_CONFIGURED;
            VDBusy = false;
        }

        /// <summary>
        /// Write command to VISA Device
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public VISA_Device_ERROR WriteLine(string command)
        {
            if (VDStatus != VISA_Device_STATUS.PORT_CONFIGURED)
            {
                VDBusy = false;
                return VISA_Device_ERROR.DEVICE_CONFIGURATION_ERROR;
            }

            VDBusy = true;

            //MessageBasedFormattedIO formattedio = new MessageBasedFormattedIO(session);
            IMessageBasedFormattedIO formattedio = session.FormattedIO;
            try
            {
                formattedio.WriteLine(command);
            }
            catch (NativeVisaException)
            {
                VDBusy = false;
                return VISA_Device_ERROR.VISA_IO_ERROR;
            }


            VDBusy = false;
            return VISA_Device_ERROR.NONE;
        }

        public void Read(DATA_TYPE type)
        {
            if (VDStatus != VISA_Device_STATUS.PORT_CONFIGURED)
            {
                fCommMessage = "Device configure error!";
                VDBusy = false;
                return;
            }

            VDBusy = true;

            IMessageBasedFormattedIO formattedio = session.FormattedIO;
            _dResult = 0;

            try
            {
                switch (type)
                {
                    case DATA_TYPE.STR:
                        //ioserial.Scanf("%s", out _sResult);    // kellhet string méret pl. %5s
                        _sResult = formattedio.ReadString();
                        break;
                    case DATA_TYPE.FL:
                        formattedio.Scanf("%f", out _fResult);   // tizedespontos szám pl. 3.2
                        break;
                    case DATA_TYPE.FL_STR:
                        formattedio.Scanf("%f%s", out _fResult, out _sResult);
                        break;
                    case DATA_TYPE.STR_FL:
                        formattedio.Scanf("%s%f", out _sResult, out _fResult);
                        break;
                    case DATA_TYPE.DOU:
                        formattedio.Scanf("%E", out _dResult);   //%E ha tartalmaz exponenst e vagy E pl. 3.2E4
                        break;
                    case DATA_TYPE.LINE:
                        _sResult = formattedio.ReadLine();
                        break;
                    case DATA_TYPE.BYTE:
                        //_sResult = new string(Encoding.ASCII.GetChars(bytearray));
                        break;
                    default:
                        break;
                }
            }
            catch (TypeFormatterException) // when scanf can not convert value
            {
                fCommMessage = "Device reading data format error";
            }
            catch (FormatException) // when scanf can not convert value
            {
                fCommMessage = "Device reading data format error";
            }
            catch (IOTimeoutException) // when Multimeter does not answer
            {
                fCommMessage = "Device read Timeout";
                VDError = VISA_Device_ERROR.READ_TIMEOUT;
                VDBusy = false;
                return;
            }
            catch (NativeVisaException)    // when I/O line has broken
            {
                fCommMessage = "Device I/O error";
                VDError = VISA_Device_ERROR.VISA_IO_ERROR;
                VDBusy = false;
                return;
            }

        }


        /// <summary>
        /// Device configuration
        /// </summary>
        //void Config(int range)
        //{
        //    VDBusy = true;
        //    fCommMessage = "success";
        //    MessageBasedFormattedIO iousb = new MessageBasedFormattedIO(session);
        //    MessageBasedFormattedIO ioserial = new MessageBasedFormattedIO(session);
        //    //IMessageBasedFormattedIO iousb = usbSession.FormattedIO;
        //    //IMessageBasedFormattedIO ioserial = serialSession.FormattedIO;
        //    if (porttype == "USB")
        //    { 
        //        iousb.WriteLine("*RST");
        //        //iousb.WriteLine("SYSTem:REMote");
        //        iousb.WriteLine("CONFigure:VOLTage:DC " + range.ToString());
        //        iousb.WriteLine("SAMPle:COUNt 1");
        //        iousb.WriteLine("TRIGger:SOURce IMMediate");
        //        //iousb.WriteLine("SENSe:VOLTage:DC:NPLCycles 0.2");  // 10 V határ, 34461A, 100 mikroV (1E-4) pontosság -> ~9 ms/mérés (451 oldal)
        //        iousb.WriteLine("SENSe:VOLTage:DC:NPLCycles 10");  // 10 V határ, 34461A, 10 mikroV (1E-5) pontosság -> ~0.2 s/mérés (451 oldal)
        //        // 1-nél ~40 ms, 10-nél ~400 ms

        //        // event registerek törlése
        //        try
        //        {
        //            iousb.WriteLine("*CLS");
        //        }
        //        catch (NativeVisaException)
        //        {
        //            fCommMessage = "Multimeter: I/O error";
        //            VDError = VISA_Device_ERROR.VISA_IO_ERROR;
        //            VDBusy = false;
        //            return;
        //        }
        //    }
        //    if (porttype == "SERIAL")
        //    {
        //        ioserial.WriteLine("*RST");
        //        //ioserial.WriteLine("SYSTem:REMote");
        //        ioserial.WriteLine("CONFigure:VOLTage:DC " + range.ToString());
        //        ioserial.WriteLine("SAMPle:COUNt 1");
        //        ioserial.WriteLine("TRIGger:SOURce IMMediate");
        //        ioserial.WriteLine("SENSe:VOLTage:DC:NPLCycles 0.2");

        //        // event registerek törlése
        //        try
        //        {
        //            iousb.WriteLine("*CLS");
        //        }
        //        catch (NativeVisaException)
        //        {
        //            fCommMessage = "Multimeter: I/O error";
        //            VDError = VISA_Device_ERROR.VISA_IO_ERROR;
        //            VDBusy = false;
        //            return;
        //        }
        //    }
        //    //io.WriteLine("*RST");
        //    //io.WriteLine(":HEAD OFF"); // header off in answer
        //    //io.WriteLine(":RRAN"+' '+Convert.ToString(3));   // resistance range
        //    //io.WriteLine(":VRAN"+' '+Convert.ToString(5));    // voltage range
        //    //io.WriteLine(":MOD RV");  // resistance and voltage measurement mode
        //    //io.WriteLine(":SAMP FAST");   //sampling rate
        //    //VDStatus = VISA_Device_STATUS.DEVICE_CONFIGURED;
        //    VDBusy = false;
        //}

        /// <summary>
        /// Measuring process
        /// </summary>
        //void Measure()
        //{
        //    VDBusy = true;
        //    fCommMessage = "success";

        //    //if (VDStatus != VISA_Device_STATUS.DEVICE_CONFIGURED)
        //    //{
        //    //    fCommMessage = "Multimeter configure error!";
        //    //    VDBusy = false;
        //    //    return;
        //    //}
        //    fCommMessage = "success";
        //    MessageBasedFormattedIO iousb = new MessageBasedFormattedIO(session);
        //    MessageBasedFormattedIO ioserial = new MessageBasedFormattedIO(session);

        //    //IMessageBasedFormattedIO iousb = usbSession.FormattedIO;
        //    //IMessageBasedFormattedIO ioserial = usbSession.FormattedIO;

        //    if (porttype == "USB")
        //    {
        //        // CLS-t kivesszük, úgyis olyan gyors a mérés, hogy a műszer kijelzőjén nem követhető
        //        //try
        //        //{
        //        //    iousb.WriteLine("*CLS");
        //        //}
        //        //catch (NativeVisaException)
        //        //{
        //        //    fCommMessage = "Multimeter: I/O error";
        //        //    MultimeterError = MULTIMETER_ERROR.VISA_IO_ERROR;
        //        //    MultimeterBusy = false;
        //        //    return;
        //        //}
        //        try
        //        {
        //            // iousb.WriteLine("MEAS:VOLT?"); elrontja a configot
        //            //st.Restart();
        //            iousb.WriteLine("READ?");
        //        }
        //        catch (NativeVisaException)
        //        {
        //            fCommMessage = "Multimeter: I/O error";
        //            VDError = VISA_Device_ERROR.VISA_IO_ERROR;
        //            VDBusy = false;
        //            return;
        //        }
        //        _dResult = 0;

        //        try
        //        {
        //            // expected format: 3.5678E+0
        //            //string[] response = new string[] { "", "" };
        //            //iousb.Scanf("%,s", out response);
        //            iousb.Scanf("%E", out _dResult);
        //            //st.Stop();
        //            //long el = st.ElapsedMilliseconds;
        //        }
        //        catch (TypeFormatterException) // when scanf can not convert value
        //        {
        //            fCommMessage = "Multimeter: Reading data format error";
        //        }
        //        catch (FormatException) // when scanf can not convert value
        //        {
        //            fCommMessage = "Multimeter: Reading data format error";
        //        }
        //        catch (IOTimeoutException) // when Multimeter does not answer
        //        {
        //            fCommMessage = "Multimeter: Read Timeout";
        //            VDError = VISA_Device_ERROR.READ_TIMEOUT;
        //            VDBusy = false;
        //            return;
        //        }
        //        catch (NativeVisaException)    // when I/O line has broken
        //        {
        //            fCommMessage = "Multimeter: I/O error";
        //            VDError = VISA_Device_ERROR.VISA_IO_ERROR;
        //            VDBusy = false;
        //            return;
        //        }
        //    }

        //    if (porttype == "SERIAL")
        //    {
        //        //try
        //        //{
        //        //    ioserial.WriteLine("*CLS");
        //        //}
        //        //catch (NativeVisaException)
        //        //{
        //        //    fCommMessage = "Multimeter: I/O error";
        //        //    MultimeterError = MULTIMETER_ERROR.VISA_IO_ERROR;
        //        //    MultimeterBusy = false;
        //        //    return;
        //        //}
        //        try
        //        {
        //            //ioserial.WriteLine("MEAS:VOLT?");
        //            ioserial.WriteLine("READ?");
        //        }
        //        catch (NativeVisaException)
        //        {
        //            fCommMessage = "Multimeter: I/O error";
        //            VDError = VISA_Device_ERROR.VISA_IO_ERROR;
        //            VDBusy = false;
        //            return;
        //        }
        //        _dResult = 0;

        //        try
        //        {
        //            // expected format: 3.5678E+0
        //            //string[] response = new string[] { "", "" };
        //            //iousb.Scanf("%,s", out response);
        //            ioserial.Scanf("%E", out _dResult);
        //        }
        //        catch (TypeFormatterException) // when scanf can not convert value
        //        {
        //            fCommMessage = "Multimeter: Reading data format error";
        //        }
        //        catch (FormatException) // when scanf can not convert value
        //        {
        //            fCommMessage = "Multimeter: Reading data format error";
        //        }
        //        catch (IOTimeoutException) // when Multimeter does not answer
        //        {
        //            fCommMessage = "Multimeter: Read Timeout";
        //            VDError = VISA_Device_ERROR.READ_TIMEOUT;
        //            VDBusy = false;
        //            return;
        //        }
        //        catch (NativeVisaException)    // when I/O line has broken
        //        {
        //            fCommMessage = "Multimeter: I/O error";
        //            VDError = VISA_Device_ERROR.VISA_IO_ERROR;
        //            VDBusy = false;
        //            return;
        //        }
        //    }

        //    VDBusy = false;
        //}

        // Setting external I/O Pins
        // valid output value: 0..1023
        //-------------------------------------
        //public void ExtIOSet(short Pin)
        //{
        //    fCommMessage = "success";
        //    if (Pin >= 0 && Pin < 1024)
        //        succes = visa32.viPrintf(dmm, ":IO:OUT" + ' ' + Convert.ToInt16(Pin) + "\r\n");
        //    else
        //        succes = -1;
        //    if (succes != 0)
        //        fCommMessage = "Multimeter Externel IO Write Error!";
        //}

        // Read external I/O Pins
        // valid input value: 0..31
        //-------------------------------------
        //public void ExtIOInput()
        //{
        //    byte[] tempbyte = new byte[10]; // to store the answer
        //    int Maxcount;
        //    int count = 4;    // the answer is between 0..31 {number,number,0d,0a}

        //    succes = visa32.viPrintf(dmm, ":IO:IN?\r\n");
        //    if (succes == 0)
        //        succes = visa32.viRead(dmm, tempbyte, count, out Maxcount);
        //    fCommMessage = "success";
        //    if (succes != 0)
        //        fCommMessage = "Multimeter Externel IO Read Error!";
        //    else
        //    {
        //        string tempstring = new string(Encoding.ASCII.GetChars(tempbyte));
        //        EInput = Convert.ToInt32(tempstring);
        //    }
        //}

        /// <summary>
        /// Closing session
        /// </summary>
        public void Close()
        {
            VDBusy = true;
            session.Dispose();
            VDStatus = VISA_Device_STATUS.CLASS_INITIALIZED;
            VDBusy = false;
        }
    }
}
