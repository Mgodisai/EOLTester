using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public class BQ76942_769142_76952 : INotifyPropertyChanged
    {
        private readonly IInterfaceProvider InterfaceProvider;

        public bool SendPropertyChanged { get; set; }

        #region I2C
        public byte I2cAddressWrite { get; }

        public byte I2cAddressRead => (byte)(I2cAddressWrite | 0x01);

        public uint I2cSpeed { get; set; }

        private void WriteI2C(byte[] data, int offset)
        {
            byte[] txd;
            if (offset == 0)
            {
                txd = data;
            }
            else
            {
                txd = new byte[data.Length - offset];
                Array.Copy(data, offset, txd, 0, txd.Length);
            }
            IOException error = null;
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    error = null;
                    InterfaceProvider.WriteI2C(I2cAddressWrite, txd, I2cSpeed);
                    break;
                }
                catch (IOException e)
                {
                    error = e;
                }
            }
            if (error != null)
            {
                throw error;
            }
        }

        private byte[] ReadI2C(int length)
        {
            return InterfaceProvider.ReadI2C(I2cAddressRead, length, I2cSpeed);
        }
        #endregion

        #region SPI
        public uint SpiSpeed { get; set; }

        public byte[] TransferSpi(byte[] txd)
        {
            return InterfaceProvider.TransferSpi(txd, SpiSpeed, 0);
        }

        public byte[] TransferByteSpi(byte address, byte data)
        {
            byte[] txd = new byte[3];
            txd[0] = address;
            txd[1] = data;
            txd[2] = GetCRC8210(txd, 0, 2);

            byte[] rxd;

            int cnt = 0;
            bool stop = false;
            do
            {
                rxd = TransferSpi(txd);
                //Console.WriteLine("{0:X2} {1:X2} {2:X2} -> {3:X2} {4:X2} {5:X2} {6}",
                //    txd[0], txd[1], txd[2],
                //    rxd[0], rxd[1], rxd[2],
                //    rxd[2] == GetCRC8210(rxd, 0, 2)
                //);
                byte rxAddress = rxd[0];
                byte rxData = rxd[1];
                byte crc = rxd[2];
                if (rxAddress == 0xFF && rxData == 0xFF)
                {
                    switch (crc)
                    {
                        case 0x00:
                            stop = true;
                            break;
                        case 0xFF:
                            break;
                        case 0xAA:
                        default:
                            ThrowSpiProtocolError();
                            break;
                    }
                }
                else if ((rxAddress & 0x7F) < 0x80)
                {
                    if (UseCRC && crc != GetCRC8210(rxd, 0, 2))
                    {
                        ThrowReceivedCrcError();
                    }
                    stop = true;
                }
                else
                {
                    ThrowSpiProtocolError();
                }

                if (!stop && ++cnt >= 3)
                {
                    ThrowSpiTimeoutError();
                }
            } while (!stop);

            return rxd;
        }
        #endregion

        #region Interface protocol
        private bool _useCRC;

        public bool UseCRC
        {
            get => _useCRC;
            set
            {
                if (_useCRC != value)
                {
                    _useCRC = value;
                    OnPropertyChanged(nameof(UseCRC));
                }
            }
        }

        internal byte[] ReadInterface(byte address, byte size)
        {
            switch (InterfaceProvider.CommType)
            {
                case CommType.I2C:
                    return ReadInterfaceI2c(address, size);
                case CommType.SPI:
                    return ReadInterfaceSpi(address, size);
                default:
                    throw new NotImplementedException(nameof(ReadInterface) + " CommType = " + InterfaceProvider.CommType);
            }
        }

        private byte[] ReadInterfaceI2c(byte address, byte size)
        {
            WriteI2C(new byte[] { address }, 0);
            byte[] rxd = ReadI2C(UseCRC ? size * 2 : size);
            byte[] data;
            if (UseCRC)
            {
                data = new byte[rxd.Length / 2];
                for (int i = 0; i < data.Length; i++)
                {
                    byte dataByte = rxd[i * 2];
                    byte crcByte = rxd[i * 2 + 1];
                    byte[] crcBlock = i == 0
                        ? new byte[] { I2cAddressRead, dataByte }
                        : new byte[] { dataByte };
                    byte crc = GetCRC8210(crcBlock, 0, crcBlock.Length);
                    if (crcByte != crc)
                    {
                        ThrowReceivedCrcError();
                    }
                    data[i] = dataByte;
                }
            }
            else
            {
                data = rxd;
            }
            return data;
        }

        private byte[] ReadInterfaceSpi(byte startAddress, byte size)
        {
            byte[] data = new byte[size];

            for (byte i = 0; i <= data.Length; i++)
            {
                int offset = i == data.Length ? i - 1 : i;
                byte[] rxd = TransferByteSpi((byte)(startAddress + offset), 0);
                if (i > 0)
                {
                    int expAddress = startAddress + i - 1;
                    if (rxd[0] != expAddress)
                    {
                        ThrowSpiProtocolError();
                    }
                    data[i - 1] = rxd[1];
                }
            }

            return data;
        }

        internal void WriteInterface(byte address, byte[] data)
        {
            switch (InterfaceProvider.CommType)
            {
                case CommType.I2C:
                    WriteInterfaceI2c(address, data);
                    break;
                case CommType.SPI:
                    WriteInterfaceSpi(address, data);
                    break;
                default:
                    throw new NotImplementedException(nameof(WriteInterface) + " CommType = " + InterfaceProvider.CommType);
            }
        }

        private void WriteInterfaceI2c(byte address, byte[] data)
        {
            byte[] payload = new byte[(UseCRC ? data.Length * 2 : data.Length) + 2];
            payload[0] = I2cAddressWrite;
            payload[1] = address;
            for (int i = 0; i < data.Length; i++)
            {
                int dataIndex = (UseCRC ? i * 2 : i) + 2;
                payload[dataIndex] = data[i];
                if (UseCRC)
                {
                    payload[dataIndex + 1] = i == 0
                                ? GetCRC8210(payload, 0, 3)
                                : GetCRC8210(payload, dataIndex, 1);
                }
            }
            WriteI2C(payload, 1);
        }

        private void WriteInterfaceSpi(byte startAddress, byte[] data)
        {
            bool ignoreLastByte = false;
            if (startAddress == (byte)DirectRamRegister.Subcommand_L && data.Length == 2)
            {
                ushort command = BitConverter.ToUInt16(data, 0);
                ignoreLastByte = 
                    command == Subcommands.RESET.Code ||
                    command == Subcommands.SWAP_COMM_MODE.Code;
            }
            int length = ignoreLastByte ? data.Length : data.Length + 1;
            for (int i = 0; i < length; i++)
            {
                int offset = i == data.Length ? i - 1 : i;
                byte[] rxd = TransferByteSpi((byte)((startAddress + offset) | 0x80), data[offset]);
                if (i > 0)
                {
                    int expAddress = (startAddress + i - 1) | 0x80;
                    byte expData = data[i - 1];
                    if (rxd[0] != expAddress || rxd[1] != expData)
                    {
                        ThrowSpiProtocolError();
                    }
                }
            }
        }

        private static byte GetCRC8210(byte[] payload, int start, int length)
        {
            byte crc = 0;

            for (int i = 0; i < length; i++)
            {
                for (int m = 0x80; m != 0; m /= 2)
                {
                    if ((crc & 0x80) != 0)
                    {
                        crc *= 2;
                        crc ^= 0x07;
                    }
                    else
                    {
                        crc *= 2;
                    }
                    if ((payload[i + start] & m) != 0)
                    {
                        crc ^= 0x07;
                    }
                }
            }

            return crc;
        }

        private static void ThrowReceivedCrcError()
        {
            throw new IOException("Received CRC error!");
            // TODO Custom Exception
        }

        private static void ThrowSpiProtocolError()
        {
            throw new IOException("Received SPI Protocol error!");
            // TODO Custom Exception
        }

        private static void ThrowSpiTimeoutError()
        {
            throw new IOException("Received SPI Timeout error!");
            // TODO Custom Exception
        }
        #endregion

        public DirectRam DirectRam { get; }

        public Subcommands Subcommands { get; }

        public void DisableSleep()
        {
            DirectRam.ReadBattery_Status();
            if (DirectRam.Battery_Status_low.SLEEP_EN)
            {
                Subcommands.SLEEP_DISABLE.Send();
                DirectRam.ReadBattery_Status();
            }
            int cnt = 0;
            while (DirectRam.Battery_Status_high.SLEEP)
            {
                if (cnt > 20)
                {
                    throw new TimeoutException(nameof(SLEEP_DISABLE));
                }
                DirectRam.ReadBattery_Status();
                cnt++;
            }
        }

        public DataMemory DataMemory { get; }

        public BQ76942_769142_76952(IInterfaceProvider interfaceProvider)
        {
            InterfaceProvider = interfaceProvider;

            SendPropertyChanged = true;

            I2cAddressWrite = 0x10;
            I2cSpeed = 100000;

            SpiSpeed = 125000;

            _useCRC = true;

            DirectRam = new DirectRam(this);
            Subcommands = new Subcommands(this);
            DataMemory = new DataMemory(this);
        }

        public void GetVoltageADCs(int sampleCount, int[] cellADCs, int[] sumADCs, int[] cc2ADCs)
        {
            var ds1 = Subcommands.DASTATUS1;
            var ds2 = Subcommands.DASTATUS2;
            var ds3 = Subcommands.DASTATUS3;
            var rc1 = Subcommands.READ_CAL1;

            List<int[]> cellsSamples = new List<int[]>();
            List<int[]> sumsSamples = new List<int[]>();

            rc1.Read();
            short cdc = rc1.Calibration_Data_Counter;
            for (int i = 0; i < sampleCount; i++)
            {
                int cnt = 0;
                do
                {
                    rc1.Read();
                    cnt++;
                } while (rc1.Calibration_Data_Counter == cdc);
                cdc = rc1.Calibration_Data_Counter;
                ds1.Read();
                ds2.Read();
                ds3.Read();

                cellsSamples.Add(new int[] {
                        ds1.Cell_1_Voltage_Counts,
                        ds1.Cell_2_Voltage_Counts,
                        ds1.Cell_3_Voltage_Counts,
                        ds1.Cell_4_Voltage_Counts,
                        ds2.Cell_5_Voltage_Counts,
                        ds2.Cell_6_Voltage_Counts,
                        ds2.Cell_7_Voltage_Counts,
                        ds2.Cell_8_Voltage_Counts,
                        ds3.Cell_9_Voltage_Counts,
                        ds3.Cell_10_Voltage_Counts
                    });
                sumsSamples.Add(new int[] {
                        rc1.PACK_pin_ADC_Counts,
                        rc1.Top_of_Stack_ADC_Counts,
                        rc1.LD_pin_ADC_Counts
                    });
                cc2ADCs[i] = rc1.CC2_Counts;
            }

            for (int iCell = 0; iCell < cellADCs.Length; iCell++)
            {
                long sum = 0;
                int min = int.MaxValue;
                int max = int.MinValue;
                foreach (int[] cellsSample in cellsSamples)
                {
                    int adc = cellsSample[iCell];
                    sum += adc;
                    min = Math.Min(min, adc);
                    max = Math.Max(max, adc);
                }
                int delta = max - min;
                // TODO max delta
                cellADCs[iCell] = (int)(sum / cellsSamples.Count);
            }
            for (int iSum = 0; iSum < sumADCs.Length; iSum++)
            {
                long sum = 0;
                int min = int.MaxValue;
                int max = int.MinValue;
                foreach (int[] sumsSample in sumsSamples)
                {
                    int adc = sumsSample[iSum];
                    sum += adc;
                    min = Math.Min(min, adc);
                    max = Math.Max(max, adc);
                }
                int delta = max - min;
                // TODO max delta
                sumADCs[iSum] = (int)(sum / sumsSamples.Count);
            }
        }

        public int GetCurrentADC(int sampleCount)
        {
            var rc1 = Subcommands.READ_CAL1;

            int[] currentADCs = new int[sampleCount];
            long sum = 0;
            int min = int.MaxValue;
            int max = int.MinValue;

            rc1.Read();
            short cdc = rc1.Calibration_Data_Counter;
            for (int i = 0; i < sampleCount; i++)
            {
                int cnt = 0;
                do
                {
                    rc1.Read();
                    cnt++;
                } while (rc1.Calibration_Data_Counter == cdc);
                cdc = rc1.Calibration_Data_Counter;
                int adc = rc1.CC2_Counts;
                currentADCs[i] = adc;
                sum += adc;
                min = Math.Min(min, adc);
                max = Math.Max(max, adc);
            }
            int delta = max - min;
            // TODO max delta

            return (int)Math.Round(sum / (currentADCs.Length * 256m));
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (SendPropertyChanged && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
