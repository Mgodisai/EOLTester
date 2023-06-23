using System;
using System.ComponentModel;
using VTEP.Arrays;
using VTEP.TypeConverters;

namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public abstract class BitRegister
    {
        private DirectRam DirectRam;

        [TypeConverter(typeof(ByteHexTypeConverter))]
        public byte Address { get; }

        [Browsable(false)]
        public byte Data => DirectRam[Address];

        public bool GetBit(byte bit)
        {
            return ArrayMapper.GetBit(Data, bit);
        }

        public BitRegister(DirectRam directRam)
        {
            DirectRam = directRam;
            Address = 0xFF;
            string className = this.GetType().Name;
            for (byte i = 0; i < DirectRam.SIZE; i++)
            {
                DirectRamRegister register = (DirectRamRegister)i;
                if (register.ToString() == className)
                {
                    Address = i;
                    break;
                }
            }
            if (Address == 0xFF)
            {
                throw new ApplicationException(String.Format("Can not find Address for {0}.{1} {2}!", typeof(DirectRam).Name, className, typeof(BitRegister).Name));
            }
        }

        public override string ToString()
        {
            return string.Format("0x{0:X2}", Data);
        }
    }

    public class Control_Status_low : BitRegister
    {
        public bool RSVD7_0 => GetBit(7);
        public bool RSVD6_0 => GetBit(6);
        public bool RSVD5_0 => GetBit(5);
        public bool RSVD4_0 => GetBit(4);
        public bool RSVD3_0 => GetBit(3);
        public bool DEEPSLEEP => GetBit(2);
        public bool LD_TIMEOUT => GetBit(1);
        public bool LD_ON => GetBit(0);

        public Control_Status_low(DirectRam directRam) : base(directRam) { }
    }

    public class Control_Status_high : BitRegister
    {
        public bool RSVD15_0 => GetBit(7);
        public bool RSVD14_0 => GetBit(6);
        public bool RSVD13_0 => GetBit(5);
        public bool RSVD12_0 => GetBit(4);
        public bool RSVD11_0 => GetBit(3);
        public bool RSVD10_0 => GetBit(2);
        public bool RSVD9_0 => GetBit(1);
        public bool RSVD8_0 => GetBit(0);

        public Control_Status_high(DirectRam directRam) : base(directRam) { }
    }

    public class Safety_Alert_A : BitRegister
    {
        public bool SCD => GetBit(7);
        public bool OCD2 => GetBit(6);
        public bool OCD1 => GetBit(5);
        public bool OCC => GetBit(4);
        public bool COV => GetBit(3);
        public bool CUV => GetBit(2);
        public bool RSVD1_0 => GetBit(1);
        public bool RSVD0_0 => GetBit(0);

        public Safety_Alert_A(DirectRam directRam) : base(directRam) { }
    }

    public class Safety_Status_A : BitRegister
    {
        public bool SCD => GetBit(7);
        public bool OCD2 => GetBit(6);
        public bool OCD1 => GetBit(5);
        public bool OCC => GetBit(4);
        public bool COV => GetBit(3);
        public bool CUV => GetBit(2);
        public bool RSVD1_0 => GetBit(1);
        public bool RSVD0_0 => GetBit(0);

        public Safety_Status_A(DirectRam directRam) : base(directRam) { }
    }

    public class Safety_Alert_B : BitRegister
    {
        public bool OTF => GetBit(7);
        public bool OTINT => GetBit(6);
        public bool OTD => GetBit(5);
        public bool OTC => GetBit(4);
        public bool RSVD3_0 => GetBit(3);
        public bool UTINT => GetBit(2);
        public bool UTD => GetBit(1);
        public bool UTC => GetBit(0);

        public Safety_Alert_B(DirectRam directRam) : base(directRam) { }
    }

    public class Safety_Status_B : BitRegister
    {
        public bool OTF => GetBit(7);
        public bool OTINT => GetBit(6);
        public bool OTD => GetBit(5);
        public bool OTC => GetBit(4);
        public bool RSVD3_0 => GetBit(3);
        public bool UTINT => GetBit(2);
        public bool UTD => GetBit(1);
        public bool UTC => GetBit(0);

        public Safety_Status_B(DirectRam directRam) : base(directRam) { }
    }

    public class Safety_Alert_C : BitRegister
    {
        public bool OCD3 => GetBit(7);
        public bool SCDL => GetBit(6);
        public bool OCDL => GetBit(5);
        public bool COVL => GetBit(4);
        public bool PTOS => GetBit(3);
        public bool RSVD2_0 => GetBit(2);
        public bool RSVD1_0 => GetBit(1);
        public bool RSVD0_0 => GetBit(0);

        public Safety_Alert_C(DirectRam directRam) : base(directRam) { }
    }

    public class Safety_Status_C : BitRegister
    {
        public bool OCD3 => GetBit(7);
        public bool SCDL => GetBit(6);
        public bool OCDL => GetBit(5);
        public bool COVL => GetBit(4);
        public bool RSVD3_0 => GetBit(3);
        public bool PTO => GetBit(2);
        public bool HWDF => GetBit(1);
        public bool RSVD0_0 => GetBit(0);

        public Safety_Status_C(DirectRam directRam) : base(directRam) { }
    }

    public class PF_Alert_A : BitRegister
    {
        public bool CUDEP => GetBit(7);
        public bool SOTF => GetBit(6);
        public bool RSVD5_0 => GetBit(5);
        public bool SOT => GetBit(4);
        public bool SOCD => GetBit(3);
        public bool SOCC => GetBit(2);
        public bool SOV => GetBit(1);
        public bool SUV => GetBit(0);

        public PF_Alert_A(DirectRam directRam) : base(directRam) { }
    }

    public class PF_Status_A : BitRegister
    {
        public bool CUDEP => GetBit(7);
        public bool SOTF => GetBit(6);
        public bool RSVD5_0 => GetBit(5);
        public bool SOT => GetBit(4);
        public bool SOCD => GetBit(3);
        public bool SOCC => GetBit(2);
        public bool SOV => GetBit(1);
        public bool SUV => GetBit(0);

        public PF_Status_A(DirectRam directRam) : base(directRam) { }
    }

    public class PF_Alert_B : BitRegister
    {
        public bool SCDL => GetBit(7);
        public bool RSVD6_0 => GetBit(6);
        public bool RSVD5_0 => GetBit(5);
        public bool VIMA => GetBit(4);
        public bool VIMR => GetBit(3);
        public bool _2LVL => GetBit(2);
        public bool DFETF => GetBit(1);
        public bool CFETF => GetBit(0);

        public PF_Alert_B(DirectRam directRam) : base(directRam) { }
    }

    public class PF_Status_B : BitRegister
    {
        public bool SCDL => GetBit(7);
        public bool RSVD6_0 => GetBit(6);
        public bool RSVD5_0 => GetBit(5);
        public bool VIMA => GetBit(4);
        public bool VIMR => GetBit(3);
        public bool _2LVL => GetBit(2);
        public bool DFETF => GetBit(1);
        public bool CFETF => GetBit(0);

        public PF_Status_B(DirectRam directRam) : base(directRam) { }
    }

    public class PF_Alert_C : BitRegister
    {
        public bool RSVD7_0 => GetBit(7);
        public bool HWMX => GetBit(6);
        public bool VSSF => GetBit(5);
        public bool VREF => GetBit(4);
        public bool LFOF => GetBit(3);
        public bool RSVD2_0 => GetBit(2);
        public bool RSVD1_0 => GetBit(1);
        public bool RSVD0_0 => GetBit(0);

        public PF_Alert_C(DirectRam directRam) : base(directRam) { }
    }

    public class PF_Status_C : BitRegister
    {
        public bool CMDF => GetBit(7);
        public bool HWMX => GetBit(6);
        public bool VSSF => GetBit(5);
        public bool VREF => GetBit(4);
        public bool LFOF => GetBit(3);
        public bool IRMF => GetBit(2);
        public bool DRMF => GetBit(1);
        public bool OTPF => GetBit(0);

        public PF_Status_C(DirectRam directRam) : base(directRam) { }
    }

    public class PF_Alert_D : BitRegister
    {
        public bool RSVD7_0 => GetBit(7);
        public bool RSVD6_0 => GetBit(6);
        public bool RSVD5_0 => GetBit(5);
        public bool RSVD4_0 => GetBit(4);
        public bool RSVD3_0 => GetBit(3);
        public bool RSVD2_0 => GetBit(2);
        public bool RSVD1_0 => GetBit(1);
        public bool TOSF => GetBit(0);

        public PF_Alert_D(DirectRam directRam) : base(directRam) { }
    }

    public class PF_Status_D : BitRegister
    {
        public bool RSVD7_0 => GetBit(7);
        public bool RSVD6_0 => GetBit(6);
        public bool RSVD5_0 => GetBit(5);
        public bool RSVD4_0 => GetBit(4);
        public bool RSVD3_0 => GetBit(3);
        public bool RSVD2_0 => GetBit(2);
        public bool RSVD1_0 => GetBit(1);
        public bool TOSF => GetBit(0);

        public PF_Status_D(DirectRam directRam) : base(directRam) { }
    }

    public class Battery_Status_low : BitRegister
    {
        public bool OTPB => GetBit(7);
        public bool OTPW => GetBit(6);
        public bool COW_CHK => GetBit(5);
        public bool WD => GetBit(4);
        public bool POR => GetBit(3);
        public bool SLEEP_EN => GetBit(2);
        public bool PCHG_MODE => GetBit(1);
        public bool CFGUPDATE => GetBit(0);

        public Battery_Status_low(DirectRam directRam) : base(directRam) { }
    }

    public class Battery_Status_high : BitRegister
    {
        public bool SLEEP => GetBit(7);
        public bool RSVD14_0 => GetBit(6);
        public bool SD_CMD => GetBit(5);
        public bool PF => GetBit(4);
        public bool SS => GetBit(3);
        public bool FUSE => GetBit(2);
        public bool SEC1 => GetBit(1);
        public bool SEC0 => GetBit(0);

        public Battery_Status_high(DirectRam directRam) : base(directRam) { }
    }

    public class FET_Status : BitRegister
    {
        public bool RSVD7_0 => GetBit(7);
        public bool ALRT_PIN => GetBit(6);
        public bool DDSG_PIN => GetBit(5);
        public bool DCHG_PIN => GetBit(4);
        public bool PDSG_FET => GetBit(3);
        public bool DSG_FET => GetBit(2);
        public bool PCHG_FET => GetBit(1);
        public bool CHG_FET => GetBit(0);

        public FET_Status(DirectRam directRam) : base(directRam) { }
    }
}
