using System;
using System.ComponentModel;
using VTEP.TypeConverters;

namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public abstract class DataMemoryProperty<T>
    {
        protected DataMemory DataMemory { get; }

        [TypeConverter(typeof(UshortHexTypeConverter))]
        public ushort Address { get; }

        public T Min { get; }

        public T Max { get; }

        public T Default { get; }

        public virtual T Data { get; set; }

        public DataMemoryProperty(DataMemory dataMemory, ushort address, T min, T max, T _default)
        {
            DataMemory = dataMemory;
            Address = address;
            Min = min;
            Max = max;
            Default = _default;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }

    public class ByteDataMemory : DataMemoryProperty<byte>
    {
        public override byte Data
        {
            get => DataMemory.GetByte(Address);
            set => DataMemory.SetByte(Address, value);
        }

        public ByteDataMemory(DataMemory dataMemory, ushort address, byte min, byte max, byte _default)
            : base(dataMemory, address, min, max, _default) { }
    }

    public class SbyteDataMemory : DataMemoryProperty<sbyte>
    {
        public override sbyte Data
        {
            get => DataMemory.GetSbyte(Address);
            set => DataMemory.SetSbyte(Address, value);
        }

        public SbyteDataMemory(DataMemory dataMemory, ushort address, sbyte min, sbyte max, sbyte _default)
            : base(dataMemory, address, min, max, _default) { }
    }

    public class Bit8DataMemory : ByteDataMemory
    {
        private bool GetBit(byte bit) => DataMemory.GetBit(Address, bit);

        private void SetBit(byte bit, bool value) => DataMemory.SetBit(Address, bit, value);

        public bool Bit7
        {
            get => GetBit(7);
            set => SetBit(7, value);
        }

        public bool Bit6
        {
            get => GetBit(6);
            set => SetBit(6, value);
        }

        public bool Bit5
        {
            get => GetBit(5);
            set => SetBit(5, value);
        }

        public bool Bit4
        {
            get => GetBit(4);
            set => SetBit(4, value);
        }

        public bool Bit3
        {
            get => GetBit(3);
            set => SetBit(3, value);
        }

        public bool Bit2
        {
            get => GetBit(2);
            set => SetBit(2, value);
        }

        public bool Bit1
        {
            get => GetBit(1);
            set => SetBit(1, value);
        }

        public bool Bit0
        {
            get => GetBit(0);
            set => SetBit(0, value);
        }

        public Bit8DataMemory(DataMemory dataMemory, ushort address, byte min, byte max, byte _default)
            : base(dataMemory, address, min, max, _default) { }

        public override string ToString()
        {
            return string.Format("0x{0:X2}", Data);
        }
    }

    public class CellThresholdDataMemory : ByteDataMemory
    {
        public const decimal K = 0.0506m;

        public decimal Threshold
        {
            get => K * Data;
            set => Data = (byte)Math.Round(value / K);
        }
        public CellThresholdDataMemory(DataMemory dataMemory, ushort address, byte min, byte max, byte _default)
            : base(dataMemory, address, min, max, _default) { }

        public override string ToString()
        {
            return Threshold.ToString();
        }
    }

    public class UshortDataMemory : DataMemoryProperty<ushort>
    {
        public override ushort Data
        {
            get => DataMemory.GetUshort(Address);
            set => DataMemory.SetUshort(Address, value);
        }

        public UshortDataMemory(DataMemory dataMemory, ushort address, ushort min, ushort max, ushort _default)
            : base(dataMemory, address, min, max, _default) { }
    }

    public class ShortDataMemory : DataMemoryProperty<short>
    {
        public override short Data
        {
            get => DataMemory.GetShort(Address);
            set => DataMemory.SetShort(Address, value);
        }

        public ShortDataMemory(DataMemory dataMemory, ushort address, short min, short max, short _default)
            : base(dataMemory, address, min, max, _default) { }
    }

    public class Bit16DataMemory : UshortDataMemory
    {
        private bool GetBit(byte bit) => DataMemory.GetBit(Address, bit);

        private void SetBit(byte bit, bool value) => DataMemory.SetBit(Address, bit, value);

        public bool Bit15
        {
            get => GetBit(15);
            set => SetBit(15, value);
        }

        public bool Bit14
        {
            get => GetBit(14);
            set => SetBit(14, value);
        }

        public bool Bit13
        {
            get => GetBit(13);
            set => SetBit(13, value);
        }

        public bool Bit12
        {
            get => GetBit(12);
            set => SetBit(12, value);
        }

        public bool Bit11
        {
            get => GetBit(11);
            set => SetBit(11, value);
        }

        public bool Bit10
        {
            get => GetBit(10);
            set => SetBit(10, value);
        }

        public bool Bit9
        {
            get => GetBit(9);
            set => SetBit(9, value);
        }

        public bool Bit8
        {
            get => GetBit(8);
            set => SetBit(8, value);
        }

        public bool Bit7
        {
            get => GetBit(7);
            set => SetBit(7, value);
        }

        public bool Bit6
        {
            get => GetBit(6);
            set => SetBit(6, value);
        }

        public bool Bit5
        {
            get => GetBit(5);
            set => SetBit(5, value);
        }

        public bool Bit4
        {
            get => GetBit(4);
            set => SetBit(4, value);
        }

        public bool Bit3
        {
            get => GetBit(3);
            set => SetBit(3, value);
        }

        public bool Bit2
        {
            get => GetBit(2);
            set => SetBit(2, value);
        }

        public bool Bit1
        {
            get => GetBit(1);
            set => SetBit(1, value);
        }

        public bool Bit0
        {
            get => GetBit(0);
            set => SetBit(0, value);
        }

        public Bit16DataMemory(DataMemory dataMemory, ushort address, ushort min, ushort max, ushort _default)
            : base(dataMemory, address, min, max, _default) { }

        public override string ToString()
        {
            return string.Format("0x{0:X4}", Data);
        }
    }

    public class FloatDataMemory : DataMemoryProperty<float>
    {
        public override float Data
        {
            get => DataMemory.GetFloat(Address);
            set => DataMemory.SetFloat(Address, value);
        }

        public FloatDataMemory(DataMemory dataMemory, ushort address, float min, float max, float _default)
            : base(dataMemory, address, min, max, _default) { }
    }

    public class Current__CC_Gain : FloatDataMemory
    {
        public const float K = 7.4768f;

        public float Resistance
        {
            get => K / base.Data;
            set => base.Data = K / value;
        }

        public Current__CC_Gain(DataMemory dataMemory, ushort address, float min, float max, float _default)
            : base(dataMemory, address, min, max, _default) { }
    }

    public class Current__Capacity_Gain : FloatDataMemory
    {
        public const float K = 2230042.463f;

        public float Resistance
        {
            get => K / base.Data;
            set => base.Data = K / value;
        }

        public Current__Capacity_Gain(DataMemory dataMemory, ushort address, float min, float max, float _default)
            : base(dataMemory, address, min, max, _default) { }
    }

    public class REG12_Config : Bit8DataMemory
    {
        public enum REGV
        {
            V1_8_0 = 0,
            V1_8_1 = 1,
            V1_8_2 = 2,
            V1_8_3 = 3,
            V2_5 = 4,
            V3 = 5,
            V3_3 = 6,
            V5 = 7
        }

        public REGV REG2V
        {
            get => (REGV)DataMemory.GetBits(Address, 5, 3);
            set => DataMemory.SetBits(Address, 5, 3, (byte)value);
        }

        public bool REG2_EN
        {
            get => Bit4;
            set => Bit4 = value;
        }

        public REGV REG1V
        {
            get => (REGV)DataMemory.GetBits(Address, 1, 3);
            set => DataMemory.SetBits(Address, 1, 3, (byte)value);
        }

        public bool REG1_EN
        {
            get => Bit0;
            set => Bit0 = value;
        }

        public REG12_Config(DataMemory dataMemory, ushort address, byte min, byte max, byte _default)
            : base(dataMemory, address, min, max, _default) { }
    }

    public class REG0_Config : Bit8DataMemory
    {
        public bool RSVD7_0
        {
            get => Bit7;
        }

        public bool RSVD6_0
        {
            get => Bit6;
        }

        public bool RSVD5_0
        {
            get => Bit5;
        }

        public bool RSVD4_0
        {
            get => Bit4;
        }

        public bool RSVD3_0
        {
            get => Bit3;
        }

        public bool RSVD2_0
        {
            get => Bit2;
        }

        public bool RSVD1_0
        {
            get => Bit1;
        }

        public bool REG0_EN
        {
            get => Bit0;
            set => Bit0 = value;
        }

        public REG0_Config(DataMemory dataMemory, ushort address, byte min, byte max, byte _default)
            : base(dataMemory, address, min, max, _default) { }
    }

    public class Comm_Type : ByteDataMemory
    {
        public enum Types : byte
        {
            Default_00 = 0x00,
            Default_FF = 0xFF,
            HDQ_ALERT_pin = 0x03,
            HDQ_HDQ_pin = 0x04,
            I2C = 0x07,
            I2C_Fast = 0x08,
            I2C_Fast_with_Timeouts = 0x09,
            SPI = 0x0F,
            SPI_with_CRC = 0x10,
            I2C_with_CRC = 0x11,
            I2C_Fast_with_CRC = 0x12,
            I2C_with_Timeouts = 0x1E,
        }

        public Types Type
        {
            get => (Types)Data;
            set => Data = (byte)value;
        }
        public Comm_Type(DataMemory dataMemory, ushort address, byte min, byte max, byte _default)
            : base(dataMemory, address, min, max, _default) { }

        public override string ToString()
        {
            return Type.ToString();
        }
    }

    public class SPI_Configuration : Bit8DataMemory
    {
        public bool RSVD7_0
        {
            get => Bit7;
        }

        public bool MISO_REG1
        {
            get => Bit6;
            set => Bit6 = value;
        }

        public bool FILT
        {
            get => Bit5;
            set => Bit5 = value;
        }

        public bool RSVD4_0
        {
            get => Bit4;
        }

        public bool RSVD3_0
        {
            get => Bit3;
        }

        public bool RSVD2_0
        {
            get => Bit2;
        }

        public bool RSVD1_0
        {
            get => Bit1;
        }

        public bool RSVD0_0
        {
            get => Bit0;
        }

        public SPI_Configuration(DataMemory dataMemory, ushort address, byte min, byte max, byte _default)
            : base(dataMemory, address, min, max, _default) { }
    }

    public class Protection_Configuration : Bit16DataMemory
    {
        public bool RSVD15_0
        {
            get => Bit15;
        }

        public bool RSVD14_0
        {
            get => Bit14;
        }

        public bool RSVD13_0
        {
            get => Bit13;
        }

        public bool RSVD12_0
        {
            get => Bit12;
        }

        public bool RSVD11_0
        {
            get => Bit11;
        }

        public bool SCDL_CURR_RECOV
        {
            get => Bit10;
            set => Bit10 = value;
        }

        public bool OCDL_CURR_RECOV
        {
            get => Bit9;
            set => Bit9 = value;
        }

        public bool FETF_FUSE
        {
            get => Bit8;
            set => Bit8 = value;
        }

        public bool PACK_FUSE
        {
            get => Bit7;
            set => Bit7 = value;
        }

        public bool RSVD6_0
        {
            get => Bit6;
        }

        public bool PF_OTP
        {
            get => Bit5;
            set => Bit5 = value;
        }

        public bool PF_FUSE
        {
            get => Bit4;
            set => Bit4 = value;
        }

        public bool PF_DPSLP
        {
            get => Bit3;
            set => Bit3 = value;
        }

        public bool PF_REGS
        {
            get => Bit2;
            set => Bit2 = value;
        }

        public bool PF_FETS
        {
            get => Bit1;
            set => Bit1 = value;
        }

        public bool RSVD0_0
        {
            get => Bit0;
        }

        public Protection_Configuration(DataMemory dataMemory, ushort address, ushort min, ushort max, ushort _default)
            : base(dataMemory, address, min, max, _default) { }
    }

    public class Balancing_Configuration : Bit8DataMemory
    {
        public bool RSVD7_0
        {
            get => Bit7;
        }

        public bool RSVD6_0
        {
            get => Bit6;
        }

        public bool RSVD5_0
        {
            get => Bit5;
        }

        public bool CB_NO_CMD
        {
            get => Bit4;
            set => Bit4 = value;
        }

        public bool CB_NOSLEEP
        {
            get => Bit3;
            set => Bit3 = value;
        }

        public bool CB_SLEEP
        {
            get => Bit2;
            set => Bit2 = value;
        }

        public bool CB_RLX
        {
            get => Bit1;
            set => Bit1 = value;
        }

        public bool CB_CHG
        {
            get => Bit0;
            set => Bit0 = value;
        }

        public Balancing_Configuration(DataMemory dataMemory, ushort address, byte min, byte max, byte _default)
            : base(dataMemory, address, min, max, _default) { }
    }

    public class Mfg_Status_Init : Bit16DataMemory
    {
        public bool RSVD15_0
        {
            get => Bit15;
        }

        public bool RSVD14_0
        {
            get => Bit14;
        }

        public bool RSVD13_0
        {
            get => Bit13;
        }

        public bool RSVD12_0
        {
            get => Bit12;
        }

        public bool RSVD11_0
        {
            get => Bit11;
        }

        public bool RSVD10_0
        {
            get => Bit10;
        }

        public bool RSVD9_0
        {
            get => Bit9;
        }

        public bool RSVD8_0
        {
            get => Bit8;
        }

        public bool OTPW_EN
        {
            get => Bit7;
            set => Bit7 = value;
        }

        public bool PF_EN
        {
            get => Bit6;
            set => Bit6 = value;
        }

        public bool RSVD5_0
        {
            get => Bit5;
        }

        public bool FET_EN
        {
            get => Bit4;
            set => Bit4 = value;
        }

        public bool RSVD3_0
        {
            get => Bit3;
        }

        public bool RSVD2_0
        {
            get => Bit2;
        }

        public bool RSVD1_0
        {
            get => Bit1;
        }

        public bool RSVD0_0
        {
            get => Bit0;
        }

        public Mfg_Status_Init(DataMemory dataMemory, ushort address, ushort min, ushort max, ushort _default)
            : base(dataMemory, address, min, max, _default) { }
    }
}
