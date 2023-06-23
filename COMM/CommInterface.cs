using System;
using System.ComponentModel;
using System.Threading;
using VTEP.TI.BatteryManagement.BQ76942_769142_76952;

namespace Alber.Eol.Hardware
{
    public class CommInterface : IInterfaceProvider
    {
        private const byte RELAY_2OHM_PIN = 0;
        private const byte RELAY_1OHM_PIN = 1;
        private const byte RELAY_0_5OHM_PIN = 2;
        private const byte RELAY_CHG_PIN = 3;

        private readonly MCP2221 MCP2221;

        public CommType CommType => CommType.I2C;
        public bool IsConnected => MCP2221.IsConnected;

        public void WriteI2C(byte address, byte[] txd, uint speed)
        {
            MCP2221.Write(address, txd, speed);
        }

        public byte[] ReadI2C(byte address, int length, uint speed)
        {
            return MCP2221.Read(address, length, speed);
        }

        public byte[] TransferSpi(byte[] txd, uint speed, byte mode)
        {
            throw new NotImplementedException();
        }


        private bool relay2Ohm;

        public bool Relay2Ohm
        {
            get { return relay2Ohm; }
            private set
            {
                if (relay2Ohm != value)
                {
                    MCP2221.SetGpioPin(RELAY_2OHM_PIN, value);
                    relay2Ohm = value;
                }
            }
        }

        private bool relay1Ohm;

        public bool Relay1Ohm
        {
            get { return relay1Ohm; }
            private set
            {
                if (relay1Ohm != value)
                {
                    MCP2221.SetGpioPin(RELAY_1OHM_PIN, value);
                    relay1Ohm = value;
                }
            }
        }

        private bool relay0_5Ohm;

        public bool Relay0_5Ohm
        {
            get { return relay0_5Ohm; }
            private set
            {
                if (relay0_5Ohm != value)
                {
                    MCP2221.SetGpioPin(RELAY_0_5OHM_PIN, value);
                    relay0_5Ohm = value;
                }
            }
        }

        private bool relayCHG;

        public bool RelayCHG
        {
            get { return relayCHG; }
            private set
            {
                if (relayCHG != value)
                {
                    MCP2221.SetGpioPin(RELAY_CHG_PIN, value);
                    relayCHG = value;
                }
            }
        }

        public RelayStatus RelayStatus
        {
            get
            {
                int i = 0;
                if (Relay2Ohm)
                {
                    i += 1 << RELAY_2OHM_PIN;
                }
                if (Relay1Ohm)
                {
                    i += 1 << RELAY_1OHM_PIN;
                }
                if (Relay0_5Ohm)
                {
                    i += 1 << RELAY_0_5OHM_PIN;
                }
                if (RelayCHG)
                {
                    i += 1 << RELAY_CHG_PIN;
                }
                return (RelayStatus)i;
            }
            set
            {
                if (RelayStatus == value)
                {
                    return;
                }
                Relay2Ohm = false;
                Relay1Ohm = false;
                Relay0_5Ohm = false;
                RelayCHG = false;
                switch (value)
                {
                    case RelayStatus.AllOFF:
                        break;
                    case RelayStatus.Load2Ohm:
                        Thread.Sleep(200);
                        Relay2Ohm = true;
                        Relay1Ohm = false;
                        Relay0_5Ohm = false;
                        RelayCHG = false;
                        break;
                    case RelayStatus.Load1Ohm:
                        Thread.Sleep(200);
                        Relay2Ohm = false;
                        Relay1Ohm = true;
                        Relay0_5Ohm = false;
                        RelayCHG = false;
                        break;
                    case RelayStatus.Load0_5Ohm:
                        Thread.Sleep(200);
                        Relay2Ohm = false;
                        Relay1Ohm = true;
                        Relay0_5Ohm = true;
                        RelayCHG = false;
                        break;
                    case RelayStatus.Charge:
                        Thread.Sleep(200);
                        Relay2Ohm = false;
                        Relay1Ohm = false;
                        Relay0_5Ohm = false;
                        RelayCHG = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(RelayStatus), value, "Invalid RelayStatus!");
                }
                OnPropertyChanged("RelayStatus");

            }
        }

        public BQ76942_769142_76952 BMS { get; }

        public CommInterface()
        {
            MCP2221 = new MCP2221();
            BMS = new BQ76942_769142_76952(this);
        }

        public void Init()
        {
            MCP2221.Init();
            relay2Ohm = MCP2221.GetGpioPin(RELAY_2OHM_PIN);
            relay1Ohm = MCP2221.GetGpioPin(RELAY_1OHM_PIN);
            relay0_5Ohm = MCP2221.GetGpioPin(RELAY_0_5OHM_PIN);
            relayCHG = MCP2221.GetGpioPin(RELAY_CHG_PIN);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
