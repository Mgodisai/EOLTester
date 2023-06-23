using Alber.Eol.Hardware;
using AlberEOL.Base;
using AlberEOL.CustomClasses;
using AlberEOL.Devices;
using AlberEOL.Exceptions;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using VISA;

namespace AlberEOL.Station
{
    public partial class AlberEOLStation : StationBase
    {
        private void Initialize()
        {
            // ESZKÖZÖK INICIALIZÁLÁSA
            #region statecontrol
            // Állapotot váltunk
            State = StationState.Initialize;

            DetectingTesterExceptions = new List<TesterError>
            {
                TesterError.ERR_APPCLOSE,
                TesterError.ERR_MANUAL
            };

            TesterSuperVise();
            #endregion

            Message = new GeneralMessage("Eszközök inicializálása");
            TaskMessage = new GeneralMessage("Kérem, várjon!");
            Operation = "Inicializálás";


            Task<bool> InitPSTask = Task<bool>.Factory.StartNew(() => InitPowerSupply());
            Task<bool> InitBIMTask = Task<bool>.Factory.StartNew(() => InitBIM());

            Task<bool> InitCOMTask = Task<bool>.Factory.StartNew(() => InitCommInterface());
            Task<bool> InitCZPTask = Task<bool>.Factory.StartNew(() => InitCustomZebraPrinter());

            Task.WaitAll(InitBIMTask, InitPSTask, InitCOMTask, InitCZPTask);

            bool initResult = InitPSTask.Result && InitBIMTask.Result && InitCOMTask.Result && InitCZPTask.Result;

            if (initResult)
            {
                Message = new GeneralMessage("Init success!");
                Logger.WriteGeneralLog(Message.Text, "Initialize");
                // Turn off all relays for sure
                CommInterface.RelayStatus = RelayStatus.AllOFF;
            }
            else
            {
                ErrorCode = new ErrorCode("INIT", "Init failed");
                DeviceException exception = new DeviceException("Initialization failed");
                exception.Source = "DEVICE";
                Logger.WriteExceptionLog(Message.Text, exception.Source);
                throw exception;
            }
        }

        private bool InitBIM()
        {
            string BIMPort = Ini.IniReadValue("DEVICES", "BIM2.port");
            int BIMBaud = int.Parse(Ini.IniReadValue("DEVICES", "BIM2.baud"));
            int BIMDataBits = int.Parse(Ini.IniReadValue("DEVICES", "BIM2.databits"));
            Parity BIMParity = (Parity)Enum.Parse(typeof(Parity), Ini.IniReadValue("DEVICES", "BIM2.parity"));
            StopBits BIMStopBits = (StopBits)Enum.Parse(typeof(StopBits), Ini.IniReadValue("DEVICES", "BIM2.stopbits"));
            bool BIMEnabled = bool.Parse(Ini.IniReadValue("DEVICES", "BIM2.enabled"));

            if (BIMEnabled)
            {
                if (BIM != null && BIM.Status == BimStatus.PORT_CONFIGURED)
                {
                    BIM.PortClose();
                }
                BIM.InitBim(BIMPort, BIMBaud, BIMDataBits, BIMStopBits, BIMParity);
                try
                {
                    BIM.PortOpen();
                    BIM.GetResult(BimCommandType.Init);
                    BIM.GetResult(BimCommandType.Single);
                    Logger.WriteGeneralLog("BIM initialized successful!", "BIM");
                    return true;
                }
                catch (Exception)
                {
                    Logger.WriteExceptionLog($"BIM initialization failed!", "BIM");
                    return false;
                }
            }
            else
                Logger.WriteExceptionLog($"BIM is not enabled, check Settings.ini!", "BIM");
            return false;
        }

        private bool InitPowerSupply()
        {
            string CPXAddress = Ini.IniReadValue("DEVICES", "CPX.address");
            string CPXBaseVoltage = Ini.IniReadValue("DEVICES", "CPX.BaseVoltage");
            string CPXBaseCurrentLimit = Ini.IniReadValue("DEVICES", "CPX.BaseCurrentLimit");
            if (CPX != null)
            {
                if (CPX.Status != VISA_Device_STATUS.CLASS_INITIALIZED)
                {
                    CPX.Close();
                }
            }
            try
            {
                // Open port
                CPX.Open(CPXAddress, PORT_TYPE.SERIAL);
                if (CPX.Status != VISA_Device_STATUS.PORT_CONFIGURED)
                {
                    Logger.WriteExceptionLog($"CPX initialization failed, wrong status: {CPX.Status}", "CPX");

                    return false;
                }

                // Reset device and set base settings
                lock (CPX)
                {
                    CPX.Command(CPX.GetCommand(Cpx400Function.Reset));
                    CPX.Command(CPX.GetCommand(Cpx400Function.SetVoltage), CPXBaseVoltage);
                    CPX.Command(CPX.GetCommand(Cpx400Function.SetCurrentLimit), CPXBaseCurrentLimit);
                }
                Logger.WriteGeneralLog("CPX initialized successful!", "CPX");
                return true;
            }
            catch (Exception)
            {
                Logger.WriteExceptionLog("CPX initialization failed!", "CPX");
                return false;
            }
        }

        private bool InitCommInterface()
        {
            if (CommInterface == null)
            {
                CommInterface = new CommInterface();
            }
            try
            {
                CommInterface.Init();

                if (CommInterface.IsConnected)
                {
                    Logger.WriteGeneralLog("CommInterface initialized successful!", "COM");
                    CommInterface.RelayStatus = RelayStatus.AllOFF;
                    return true;
                }
                else
                {
                    Logger.WriteExceptionLog("CommInterface initialization failed!", "COM");
                    return false;
                }
            }
            catch (Exception)
            {
                Logger.WriteExceptionLog("CommInterface inicializálási hiba", "COM");
                return false;
            }
        }

        private bool InitCustomZebraPrinter()
        {
            string printerAddress = Ini.IniReadValue("DEVICES", "CZP.UsbAddress");
            bool isConnected = CZP.Connect(printerAddress);
            if (isConnected)
            {
                Logger.WriteGeneralLog($"CustomZebraPrinter inicializálás sikeres!", "ZebraPrinter");
            }
            else
            {
                Logger.WriteExceptionLog($"CustomZebraPrinter inicializálás sikertelen!", "ZebraPrinter");
            }
            return isConnected;
        }
    }
}
