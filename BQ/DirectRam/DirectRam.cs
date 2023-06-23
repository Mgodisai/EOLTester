using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using VTEP.Arrays;
using VTEP.TypeConverters;

namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public class DirectRam : INotifyPropertyChanged
    {
        public const byte SIZE = 0x80;

        public class Block
        {
            private KeyValuePair<byte, byte> kvp;

            public byte Address => kvp.Key;

            public byte Size => kvp.Value;

            public Block(DirectRamRegister register, byte size)
                : this((byte)register, size) { }

            public Block(byte address, byte size)
            {
                kvp = new KeyValuePair<byte, byte>(address, size);
            }
        }

        private readonly BQ76942_769142_76952 BMS;

        private byte[] Ram;

        public byte this[int i]
        {
            get { return Ram[i]; }
        }

        #region Registers
        [DisplayName("Control Status low"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Control_Status_low Control_Status_low { get; }
            
        [DisplayName("Control Status high"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Control_Status_high Control_Status_high { get; }

        [DisplayName("Safety Alert A"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Safety_Alert_A Safety_Alert_A { get; }

        [DisplayName("Safety Status A"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Safety_Status_A Safety_Status_A { get; }

        [DisplayName("Safety Alert B"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Safety_Alert_B Safety_Alert_B { get; }

        [DisplayName("Safety Status B"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Safety_Status_B Safety_Status_B { get; }

        [DisplayName("Safety Alert C"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Safety_Alert_C Safety_Alert_C { get; }

        [DisplayName("Safety Status C"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Safety_Status_C Safety_Status_C { get; }


        [DisplayName("PF Alert A"), TypeConverter(typeof(ExpandableObjectConverter))]
        public PF_Alert_A PF_Alert_A { get; }

        [DisplayName("PF Status A"), TypeConverter(typeof(ExpandableObjectConverter))]
        public PF_Status_A PF_Status_A { get; }

        [DisplayName("PF Alert B"), TypeConverter(typeof(ExpandableObjectConverter))]
        public PF_Alert_B PF_Alert_B { get; }

        [DisplayName("PF Status B"), TypeConverter(typeof(ExpandableObjectConverter))]
        public PF_Status_B PF_Status_B { get; }

        [DisplayName("PF Alert C"), TypeConverter(typeof(ExpandableObjectConverter))]
        public PF_Alert_C PF_Alert_C { get; }

        [DisplayName("PF Status C"), TypeConverter(typeof(ExpandableObjectConverter))]
        public PF_Status_C PF_Status_C { get; }

        [DisplayName("PF Alert D"), TypeConverter(typeof(ExpandableObjectConverter))]
        public PF_Alert_D PF_Alert_D { get; }

        [DisplayName("PF Status D"), TypeConverter(typeof(ExpandableObjectConverter))]
        public PF_Status_D PF_Status_D { get; }

        [DisplayName("Battery Status low"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Battery_Status_low Battery_Status_low { get; }

        [DisplayName("Battery Status high"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Battery_Status_high Battery_Status_high { get; }

        [DisplayName("Cell 1 Voltage")]
        public decimal Cell_1_Voltage => GetCellVoltage(1);

        [DisplayName("Cell 2 Voltage")]
        public decimal Cell_2_Voltage => GetCellVoltage(2);

        [DisplayName("Cell 3 Voltage")]
        public decimal Cell_3_Voltage => GetCellVoltage(3);

        [DisplayName("Cell 4 Voltage")]
        public decimal Cell_4_Voltage => GetCellVoltage(4);

        [DisplayName("Cell 5 Voltage")]
        public decimal Cell_5_Voltage => GetCellVoltage(5);

        [DisplayName("Cell 6 Voltage")]
        public decimal Cell_6_Voltage => GetCellVoltage(6);

        [DisplayName("Cell 7 Voltage")]
        public decimal Cell_7_Voltage => GetCellVoltage(7);

        [DisplayName("Cell 8 Voltage")]
        public decimal Cell_8_Voltage => GetCellVoltage(8);

        [DisplayName("Cell 9 Voltage")]
        public decimal Cell_9_Voltage => GetCellVoltage(9);

        [DisplayName("Cell 10 Voltage")]
        public decimal Cell_10_Voltage => GetCellVoltage(10);

        [DisplayName("Stack Voltage")]
        public decimal Stack_Voltage => ArrayMapper.GetShortDecimal(Ram, (int)DirectRamRegister.Stack_Voltage_low) / 100;

        [DisplayName("PACK Pin Voltage")]
        public decimal PACK_Pin_Voltage => ArrayMapper.GetShortDecimal(Ram, (int)DirectRamRegister.PACK_Pin_Voltage_low) / 100;

        [DisplayName("LD Pin Voltage")]
        public decimal LD_Pin_Voltage => ArrayMapper.GetShortDecimal(Ram, (int)DirectRamRegister.LD_Pin_Voltage_low) / 100;

        [DisplayName("CC2 Current")]
        public decimal CC2_Current => ArrayMapper.GetShortDecimal(Ram, (int)DirectRamRegister.CC2_Current_low) / 1000;

        [TypeConverter(typeof(UshortHexTypeConverter))]
        public ushort Subcommand => ArrayMapper.GetUshort(Ram, (int)DirectRamRegister.Subcommand_L);

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer0 => Ram[(int)DirectRamRegister.Buffer0];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer1 => Ram[(int)DirectRamRegister.Buffer1];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer2 => Ram[(int)DirectRamRegister.Buffer2];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer3 => Ram[(int)DirectRamRegister.Buffer3];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer4 => Ram[(int)DirectRamRegister.Buffer4];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer5 => Ram[(int)DirectRamRegister.Buffer5];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer6 => Ram[(int)DirectRamRegister.Buffer6];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer7 => Ram[(int)DirectRamRegister.Buffer7];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer8 => Ram[(int)DirectRamRegister.Buffer8];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer9 => Ram[(int)DirectRamRegister.Buffer9];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer10 => Ram[(int)DirectRamRegister.Buffer10];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer11 => Ram[(int)DirectRamRegister.Buffer11];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer12 => Ram[(int)DirectRamRegister.Buffer12];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer13 => Ram[(int)DirectRamRegister.Buffer13];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer14 => Ram[(int)DirectRamRegister.Buffer14];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer15 => Ram[(int)DirectRamRegister.Buffer15];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer16 => Ram[(int)DirectRamRegister.Buffer16];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer17 => Ram[(int)DirectRamRegister.Buffer17];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer18 => Ram[(int)DirectRamRegister.Buffer18];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer19 => Ram[(int)DirectRamRegister.Buffer19];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer20 => Ram[(int)DirectRamRegister.Buffer20];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer21 => Ram[(int)DirectRamRegister.Buffer21];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer22 => Ram[(int)DirectRamRegister.Buffer22];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer23 => Ram[(int)DirectRamRegister.Buffer23];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer24 => Ram[(int)DirectRamRegister.Buffer24];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer25 => Ram[(int)DirectRamRegister.Buffer25];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer26 => Ram[(int)DirectRamRegister.Buffer26];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer27 => Ram[(int)DirectRamRegister.Buffer27];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer28 => Ram[(int)DirectRamRegister.Buffer28];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer29 => Ram[(int)DirectRamRegister.Buffer29];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer30 => Ram[(int)DirectRamRegister.Buffer30];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Buffer31 => Ram[(int)DirectRamRegister.Buffer31];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Checksum => Ram[(int)DirectRamRegister.Checksum];

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte DataLength => Ram[(int)DirectRamRegister.DataLength];


        [DisplayName("Int Temperature")]
        public decimal Int_Temperature => GetTemperature(0);

        [DisplayName("CFETOFF Temperature")]
        public decimal CFETOFF_Temperature => GetTemperature(1);

        [DisplayName("DFETOFF Temperature")]
        public decimal DFETOFF_Temperature => GetTemperature(2);

        [DisplayName("ALERT Temperature")]
        public decimal ALERT_Temperature => GetTemperature(3);

        [DisplayName("TS1 Temperature")]
        public decimal TS1_Temperature => GetTemperature(4);

        [DisplayName("TS2 Temperature")]
        public decimal TS2_Temperature => GetTemperature(5);

        [DisplayName("TS3 Temperature")]
        public decimal TS3_Temperature => GetTemperature(6);

        [DisplayName("HDQ Temperature")]
        public decimal HDQ_Temperature => GetTemperature(7);

        [DisplayName("DCHG Temperature")]
        public decimal DCHG_Temperature => GetTemperature(8);

        [DisplayName("DDSG Temperature")]
        public decimal DDSG_Temperature => GetTemperature(9);

        [DisplayName("FET Status"), TypeConverter(typeof(ExpandableObjectConverter))]
        public FET_Status FET_Status { get; }
        #endregion

        public decimal GetCellVoltage(int index)
        {
            return ArrayMapper.GetShortDecimal(Ram, (int)DirectRamRegister.Cell_1_Voltage_low + (index - 1) * 2) / 1000;
        }

        private decimal GetTemperature(int index)
        {
            return ArrayMapper.GetShortDecimal(Ram, (int)DirectRamRegister.Int_Temperature_low + index * 2) / 10 - 273;
        }

        public DirectRam(BQ76942_769142_76952 bms)
        {
            BMS = bms;
            Reset();
            Control_Status_low = new Control_Status_low(this);
            Control_Status_high = new Control_Status_high(this);
            Safety_Alert_A = new Safety_Alert_A(this);
            Safety_Status_A = new Safety_Status_A(this);
            Safety_Alert_B = new Safety_Alert_B(this);
            Safety_Status_B = new Safety_Status_B(this);
            Safety_Alert_C = new Safety_Alert_C(this);
            Safety_Status_C = new Safety_Status_C(this);

            PF_Alert_A = new PF_Alert_A(this);
            PF_Status_A = new PF_Status_A(this);
            PF_Alert_B = new PF_Alert_B(this);
            PF_Status_B = new PF_Status_B(this);
            PF_Alert_C = new PF_Alert_C(this);
            PF_Status_C = new PF_Status_C(this);
            PF_Alert_D = new PF_Alert_D(this);
            PF_Status_D = new PF_Status_D(this);

            Battery_Status_low = new Battery_Status_low(this);
            Battery_Status_high = new Battery_Status_high(this);
            FET_Status = new FET_Status(this);
        }

        internal void Reset()
        {
            // TODO clear or new?
            Ram = new byte[SIZE];
        }

        public void Read(DirectRamRegister register, byte size)
        {
            Read(new Block((byte)register, size));
        }

        public void Read(Block block)
        {
            Read(new List<Block>() { block });
        }

        public void Read(List<Block> blocks)
        {
            foreach (Block block in blocks)
            {
                byte[] data = BMS.ReadInterface(block.Address, block.Size);
                Array.Copy(data, 0, Ram, block.Address, data.Length);
            }
            OnPropertyChanged("");
        }

        public void ReadAll()
        {
            Read(DirectRamRegister.Control_Status_low, SIZE);
        }

        public void ReadBattery_Status()
        {
            Read(DirectRamRegister.Battery_Status_low, 2);
        }

        private void Write(DirectRamRegister register, byte[] data)
        {
            Write((byte)register, data);
        }

        private void Write(byte address, byte[] data)
        {
            BMS.WriteInterface(address, data);
        }

        internal byte[] WriteSubcommand(ushort code)
        {
            byte[] commandBytes = BitConverter.GetBytes(code);
            Write(DirectRamRegister.Subcommand_L, commandBytes);
            return commandBytes;
        }

        internal void WaitSubcommand(ushort code)
        {
            int cnt = 0;
            do
            {
                Read(DirectRamRegister.Subcommand_L, 2);
                cnt++;
            } while (Subcommand != code); //TODO timeout
            if (cnt > 1)
            {
                Console.WriteLine(cnt);
            }
        }

        internal byte[] ReadBuffer(byte[] commandBytes, byte size, bool bufferFirst)
        {
            if (bufferFirst)
            {
                Read(new List<Block>() {
                   // new Block(DirectRamRegister.Subcommand_L, 2),
                    new Block(DirectRamRegister.Buffer0, size),
                    new Block(DirectRamRegister.Checksum, 2)
                });
            }
            else
            {
                Read(new List<Block>() {
                   // new Block(DirectRamRegister.Subcommand_L, 2),
                    new Block(DirectRamRegister.Buffer0, size),
                    new Block(DirectRamRegister.Checksum, 2)
                });
            }
            if (DataLength - 4 != size)
            {
                ThrowReceivedDataLengthError();
            }
            byte[] buffer = new byte[size];
            Array.Copy(Ram, (int)DirectRamRegister.Buffer0, buffer, 0, size);
            byte checksum = 0;
            AccumulateChecksum(ref checksum, commandBytes);
            AccumulateChecksum(ref checksum, buffer);
            checksum = (byte)(0xFF - checksum);
            if (Checksum != checksum)
            {
                ThrowReceivedChecksumError();
            }
            return buffer;
        }

        internal void WriteBuffer(byte[] commandBytes, byte[] buffer)
        {
            byte checksum = 0;
            AccumulateChecksum(ref checksum, commandBytes);
            AccumulateChecksum(ref checksum, buffer);
            checksum = (byte)(0xFF - checksum);
            Write(DirectRamRegister.Buffer0, buffer);
            Write(DirectRamRegister.Checksum, new byte[] { checksum, (byte)(buffer.Length + 4) });
        }

        private static void AccumulateChecksum(ref byte checksum, byte[] payload)
        {
            foreach (byte item in payload)
            {
                checksum = (byte)(checksum + item);
            }
        }

        private static void ThrowReceivedChecksumError()
        {
            throw new IOException("Received Checksum error!");
            // TODO Custom Exception
        }

        private static void ThrowReceivedDataLengthError()
        {
            throw new IOException("Received DataLength error!");
            // TODO Custom Exception
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (BMS.SendPropertyChanged && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
