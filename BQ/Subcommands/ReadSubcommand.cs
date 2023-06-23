using System.ComponentModel;
using VTEP.Arrays;
using VTEP.TypeConverters;

namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public abstract class ReadSubcommand : BaseSubcommand
    {
        protected byte[] buffer;

        public byte[] Buffer => buffer.Clone() as byte[];

        public ReadSubcommand(ISubcommandExecutor executor, int bufferSize) : base(executor)
        {
            buffer = new byte[bufferSize];
        }

        public void Read()
        {
            Executor.Read(Code, buffer);
        }
    }

    public class DEVICE_NUMBER : ReadSubcommand
    {
        public override ushort Code => 0x0001;

        [DisplayName("Device Number")]
        public string Device_Number => buffer[1].ToString("X2") + buffer[0].ToString("X2");

        public DEVICE_NUMBER(ISubcommandExecutor executor) : base(executor, 2) { }
    }

    public class MANUFACTURING_STATUS : ReadSubcommand
    {
        public override ushort Code => 0x0057;

        public bool OTPW_EN
        {
            get => ArrayMapper.GetBit(Buffer, 0, 7);
        }
        public bool PF_EN
        {
            get => ArrayMapper.GetBit(Buffer, 0, 6);
        }
        public bool PDSG_TEST
        {
            get => ArrayMapper.GetBit(Buffer, 0, 5);
        }
        public bool FET_EN
        {
            get => ArrayMapper.GetBit(Buffer, 0, 4);
        }
        public bool RSVD3_0
        {
            get => ArrayMapper.GetBit(Buffer, 0, 3);
        }
        public bool DSG_TEST
        {
            get => ArrayMapper.GetBit(Buffer, 0, 2);
        }
        public bool CHG_TEST
        {
            get => ArrayMapper.GetBit(Buffer, 0, 1);
        }
        public bool PCHG_TEST
        {
            get => ArrayMapper.GetBit(Buffer, 0, 0);
        }

        public MANUFACTURING_STATUS(ISubcommandExecutor executor) : base(executor, 2) { }
    }

    public class DASTATUS1 : ReadSubcommand
    {
        public override ushort Code => 0x0071;

        [DisplayName("Cell 1 Voltage Counts")]
        public int Cell_1_Voltage_Counts => ArrayMapper.GetInt(buffer, 0);

        [DisplayName("Cell 1 Current Counts")]
        public int Cell_1_Current_Counts => ArrayMapper.GetInt(buffer, 4);

        [DisplayName("Cell 2 Voltage Counts")]
        public int Cell_2_Voltage_Counts => ArrayMapper.GetInt(buffer, 8);

        [DisplayName("Cell 2 Current Counts")]
        public int Cell_2_Current_Counts => ArrayMapper.GetInt(buffer, 12);

        [DisplayName("Cell 3 Voltage Counts")]
        public int Cell_3_Voltage_Counts => ArrayMapper.GetInt(buffer, 16);

        [DisplayName("Cell 3 Current Counts")]
        public int Cell_3_Current_Counts => ArrayMapper.GetInt(buffer, 20);

        [DisplayName("Cell 4 Voltage Counts")]
        public int Cell_4_Voltage_Counts => ArrayMapper.GetInt(buffer, 24);

        [DisplayName("Cell 4 Current Counts")]
        public int Cell_4_Current_Counts => ArrayMapper.GetInt(buffer, 28);

        public DASTATUS1(ISubcommandExecutor executor) : base(executor, 32) { }
    }

    public class DASTATUS2 : ReadSubcommand
    {
        public override ushort Code => 0x0072;

        [DisplayName("Cell 5 Voltage Counts")]
        public int Cell_5_Voltage_Counts => ArrayMapper.GetInt(buffer, 0);

        [DisplayName("Cell 5 Current Counts")]
        public int Cell_5_Current_Counts => ArrayMapper.GetInt(buffer, 4);

        [DisplayName("Cell 6 Voltage Counts")]
        public int Cell_6_Voltage_Counts => ArrayMapper.GetInt(buffer, 8);

        [DisplayName("Cell 6 Current Counts")]
        public int Cell_6_Current_Counts => ArrayMapper.GetInt(buffer, 12);

        [DisplayName("Cell 7 Voltage Counts")]
        public int Cell_7_Voltage_Counts => ArrayMapper.GetInt(buffer, 16);

        [DisplayName("Cell 7 Current Counts")]
        public int Cell_7_Current_Counts => ArrayMapper.GetInt(buffer, 20);

        [DisplayName("Cell 8 Voltage Counts")]
        public int Cell_8_Voltage_Counts => ArrayMapper.GetInt(buffer, 24);

        [DisplayName("Cell 8 Current Counts")]
        public int Cell_8_Current_Counts => ArrayMapper.GetInt(buffer, 28);

        public DASTATUS2(ISubcommandExecutor executor) : base(executor, 32) { }
    }

    public class DASTATUS3 : ReadSubcommand
    {
        public override ushort Code => 0x0073;

        [DisplayName("Cell 9 Voltage Counts")]
        public int Cell_9_Voltage_Counts => ArrayMapper.GetInt(buffer, 0);

        [DisplayName("Cell 9 Current Counts")]
        public int Cell_9_Current_Counts => ArrayMapper.GetInt(buffer, 4);

        [DisplayName("Cell 10 Voltage Counts")]
        public int Cell_10_Voltage_Counts => ArrayMapper.GetInt(buffer, 8);

        [DisplayName("Cell 10 Current Counts")]
        public int Cell_10_Current_Counts => ArrayMapper.GetInt(buffer, 12);

        [DisplayName("Cell 11 Voltage Counts")]
        public int Cell_11_Voltage_Counts => ArrayMapper.GetInt(buffer, 16);

        [DisplayName("Cell 11 Current Counts")]
        public int Cell_11_Current_Counts => ArrayMapper.GetInt(buffer, 20);

        [DisplayName("Cell 12 Voltage Counts")]
        public int Cell_12_Voltage_Counts => ArrayMapper.GetInt(buffer, 24);

        [DisplayName("Cell 12 Current Counts")]
        public int Cell_12_Current_Counts => ArrayMapper.GetInt(buffer, 28);

        public DASTATUS3(ISubcommandExecutor executor) : base(executor, 32) { }
    }

    public class DASTATUS6 : ReadSubcommand
    {
        public override ushort Code => 0x0076;

        [DisplayName("Accum Charge")]
        public int Accum_Charge => ArrayMapper.GetInt(buffer, 0);

        [DisplayName("Accum Charge Fraction")]
        public uint Accum_Charge_Fraction => ArrayMapper.GetUint(buffer, 4);

        [DisplayName("Accum Time")]
        public uint Accum_Time => ArrayMapper.GetUint(buffer, 8);

        [DisplayName("CFETOFF Counts")]
        public int CFETOFF_Counts => ArrayMapper.GetInt(buffer, 12);

        [DisplayName("DFETOFF Counts")]
        public int DFETOFF_Counts => ArrayMapper.GetInt(buffer, 16);

        [DisplayName("ALERT Counts")]
        public int ALERT_Counts => ArrayMapper.GetInt(buffer, 20);

        [DisplayName("TS1 Counts")]
        public int TS1_Counts => ArrayMapper.GetInt(buffer, 24);

        [DisplayName("TS2 Counts")]
        public int TS2_Counts => ArrayMapper.GetInt(buffer, 28);

        public DASTATUS6(ISubcommandExecutor executor) : base(executor, 32) { }
    }

    public class OTP_WR_CHECK : ReadSubcommand
    {
        public override ushort Code => 0x00a0;

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Result => buffer[0];

        public bool OK
        {
            get => ArrayMapper.GetBit(Result, 7);
        }
        public bool RSVD6_0
        {
            get => ArrayMapper.GetBit(Result, 6);
        }
        public bool LOCK
        {
            get => ArrayMapper.GetBit(Result, 5);
        }
        public bool NOSIG
        {
            get => ArrayMapper.GetBit(Result, 4);
        }
        public bool NODATA
        {
            get => ArrayMapper.GetBit(Result, 3);
        }
        public bool HT
        {
            get => ArrayMapper.GetBit(Result, 2);
        }
        public bool LV
        {
            get => ArrayMapper.GetBit(Result, 1);
        }
        public bool HV
        {
            get => ArrayMapper.GetBit(Result, 0);
        }

        [DisplayName("Data Fail Addr"), TypeConverter(typeof(UshortHexTypeConverter))]
        public ushort Data_Fail_Addr => ArrayMapper.GetUshort(buffer, 1);

        public OTP_WR_CHECK(ISubcommandExecutor executor) : base(executor, 3) { }
    }

    public class OTP_WR : ReadSubcommand
    {
        public override ushort Code => 0x00a1;

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Result => buffer[0];

        public bool OK
        {
            get => ArrayMapper.GetBit(Result, 7);
        }
        public bool RSVD6_0
        {
            get => ArrayMapper.GetBit(Result, 6);
        }
        public bool LOCK
        {
            get => ArrayMapper.GetBit(Result, 5);
        }
        public bool NOSIG
        {
            get => ArrayMapper.GetBit(Result, 4);
        }
        public bool NODATA
        {
            get => ArrayMapper.GetBit(Result, 3);
        }
        public bool HT
        {
            get => ArrayMapper.GetBit(Result, 2);
        }
        public bool LV
        {
            get => ArrayMapper.GetBit(Result, 1);
        }
        public bool HV
        {
            get => ArrayMapper.GetBit(Result, 0);
        }

        [DisplayName("Data Fail Addr"), TypeConverter(typeof(UshortHexTypeConverter))]
        public ushort Data_Fail_Addr => ArrayMapper.GetUshort(buffer, 1);

        public OTP_WR(ISubcommandExecutor executor) : base(executor, 3) { }
    }

    public class READ_CAL1 : ReadSubcommand
    {
        public override ushort Code => 0xF081;

        [DisplayName("Calibration Data Counter")]
        public short Calibration_Data_Counter => ArrayMapper.GetShort(buffer, 0);

        [DisplayName("CC2 Counts")]
        public int CC2_Counts => ArrayMapper.GetInt(buffer, 2);

        [DisplayName("PACK pin ADC Counts")]
        public short PACK_pin_ADC_Counts => ArrayMapper.GetShort(buffer, 6);

        [DisplayName("Top of Stack ADC Counts")]
        public short Top_of_Stack_ADC_Counts => ArrayMapper.GetShort(buffer, 8);

        [DisplayName("LD pin ADC Counts")]
        public short LD_pin_ADC_Counts => ArrayMapper.GetShort(buffer, 10);

        public READ_CAL1(ISubcommandExecutor executor) : base(executor, 14) { }
    }
}
