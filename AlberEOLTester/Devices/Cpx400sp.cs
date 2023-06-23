using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VISA;
using static VISA.VISA_Device;

namespace AlberEOL.Devices
{

    public enum Cpx400Function
    {
        // WriteOnly
        Reset,
        ClearStatus,
        SetInterfaceLock,
        ReleaseInterfaceLock,

        // WriteOnlyWithParameter
        SetVoltage,
        SetVoltageWithVerify,
        SetCurrentLimit,
        SetOutput,                      // parameter ON=1|OFF=0

        // WriteAndRead
        Identification,
        GetVoltage,
        GetCurrentLimit,
        GetReadBackVoltage,
        GetReadBackCurrent,
        GetOutputStatus,
        GetServiceRequestEnableRegister,
        GetStatusByteRegister,
        GetParallelPollEnableRegister,
        GetInterfaceLockStatus,
        GetBusAddress,
        GetOperationComplete
    }

    public enum Cpx400CommandType
    {
        WriteOnly, WriteWithParameter, WriteAndRead
    }

    public enum Cpx400OutputState
    {
        Off, On
    }

    public struct Cpx400Command
    {
        public Cpx400Function Cpx400Function;
        public Cpx400CommandType Type;
        public string CommandString;
        public DATA_TYPE DataType;

        public Cpx400Command(Cpx400Function function, Cpx400CommandType commandType, DATA_TYPE dataType, string commandString)
        {
            Cpx400Function = function;
            Type = commandType;
            CommandString = commandString;
            DataType = dataType;
        }
    }


    public class Cpx400sp
    {
        private List<Cpx400Command> CommandList;
        private VISA_Device VD { get; }

        private string address;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                if (address != value)
                {
                    address = value;
                    OnPropertyChanged("Address");
                }
            }
        }
        public VISA_Device_STATUS Status
        {
            get
            {
                return VD.VDStatus;
            }
        }
        public Cpx400sp()
        {
            VD = new VISA_Device();
            VD.StatusChanged += StatusChangeHandler;

            CommandList = new List<Cpx400Command>();
            CommandList.Add(new Cpx400Command(Cpx400Function.Reset, Cpx400CommandType.WriteOnly, DATA_TYPE.NOTHING, "*RST"));
            CommandList.Add(new Cpx400Command(Cpx400Function.ClearStatus, Cpx400CommandType.WriteOnly, DATA_TYPE.NOTHING, "*CLS"));
            CommandList.Add(new Cpx400Command(Cpx400Function.SetInterfaceLock, Cpx400CommandType.WriteOnly, DATA_TYPE.NOTHING, "IFLOCK"));
            CommandList.Add(new Cpx400Command(Cpx400Function.ReleaseInterfaceLock, Cpx400CommandType.WriteOnly, DATA_TYPE.NOTHING, "IFUNLOCK"));

            CommandList.Add(new Cpx400Command(Cpx400Function.SetVoltage, Cpx400CommandType.WriteWithParameter, DATA_TYPE.NOTHING, "V1"));
            CommandList.Add(new Cpx400Command(Cpx400Function.SetVoltageWithVerify, Cpx400CommandType.WriteWithParameter, DATA_TYPE.NOTHING, "V1V"));
            CommandList.Add(new Cpx400Command(Cpx400Function.SetCurrentLimit, Cpx400CommandType.WriteWithParameter, DATA_TYPE.NOTHING, "I1"));
            CommandList.Add(new Cpx400Command(Cpx400Function.SetOutput, Cpx400CommandType.WriteWithParameter, DATA_TYPE.NOTHING, "OP1"));

            CommandList.Add(new Cpx400Command(Cpx400Function.Identification, Cpx400CommandType.WriteAndRead, DATA_TYPE.STR, "*IDN?"));
            CommandList.Add(new Cpx400Command(Cpx400Function.GetVoltage, Cpx400CommandType.WriteAndRead, DATA_TYPE.STR_FL, "V1?"));
            CommandList.Add(new Cpx400Command(Cpx400Function.GetCurrentLimit, Cpx400CommandType.WriteAndRead, DATA_TYPE.STR_FL, "I1?"));
            CommandList.Add(new Cpx400Command(Cpx400Function.GetReadBackVoltage, Cpx400CommandType.WriteAndRead, DATA_TYPE.FL_STR, "V1O?"));
            CommandList.Add(new Cpx400Command(Cpx400Function.GetReadBackCurrent, Cpx400CommandType.WriteAndRead, DATA_TYPE.FL_STR, "I1O?"));
            CommandList.Add(new Cpx400Command(Cpx400Function.GetOutputStatus, Cpx400CommandType.WriteAndRead, DATA_TYPE.FL_STR, "OP1?"));
            CommandList.Add(new Cpx400Command(Cpx400Function.GetInterfaceLockStatus, Cpx400CommandType.WriteAndRead, DATA_TYPE.FL, "IFLOCK?"));
            CommandList.Add(new Cpx400Command(Cpx400Function.GetBusAddress, Cpx400CommandType.WriteAndRead, DATA_TYPE.FL, "ADDRESS?"));
            CommandList.Add(new Cpx400Command(Cpx400Function.GetOperationComplete, Cpx400CommandType.WriteAndRead, DATA_TYPE.STR, "*OPC?"));
        }

        public void Open(string portname, PORT_TYPE porttype)
        {
            Address = portname;
            VD.Open(portname, porttype);
        }

        public void Close()
        {
            Address = null;
            VD.Close();
        }

        public Cpx400Command GetCommand(Cpx400Function function)
        {
            return CommandList.Where(Command => Command.Cpx400Function == function).First();
        }

        public void Command(Cpx400Command command, string parameter = "")
        {
            Command(command, out string resultString, parameter);
        }

        public void Command(Cpx400Command command, out string resultString, string parameter = "")
        {
            resultString = "";
            switch (command.Type)
            {
                case Cpx400CommandType.WriteOnly:
                    resultString = VD.WriteLine(command.CommandString).ToString();
                    break;
                case Cpx400CommandType.WriteWithParameter:
                    resultString = VD.WriteLine(command.CommandString + " " + parameter).ToString();
                    break;
                case Cpx400CommandType.WriteAndRead:
                    VD.WriteLine(command.CommandString).ToString();
                    resultString = ReadData(command.DataType);
                    break;
            }
        }
        private string ReadData(DATA_TYPE dataType)
        {
            string resultString = "";
            VD.Read(dataType);
            switch (dataType)
            {
                case DATA_TYPE.STR:
                case DATA_TYPE.LINE:
                    resultString = VD.sResult;
                    break;
                case DATA_TYPE.FL:
                case DATA_TYPE.STR_FL:
                case DATA_TYPE.FL_STR:
                    resultString = VD.fResult.ToString();
                    break;
                case DATA_TYPE.DOU:
                    resultString = VD.dResult.ToString();
                    break;
            }
            return resultString;
        }

        public void StatusChangeHandler(object sender, StatusEventArgs e)
        {
            OnPropertyChanged(nameof(Status));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
