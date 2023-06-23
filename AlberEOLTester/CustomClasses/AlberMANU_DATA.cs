using VTEP.Arrays;
using VTEP.TI.BatteryManagement.BQ76942_769142_76952;

namespace AlberEOL.CustomClasses
{
    public class VtepMANU_DATA : MANU_DATA
    {
        public enum Customers
        {
            Unprogrammed = 0,
            Alber_GmbH = 1
        }

        public enum Types
        {
            Unprogrammed = 0,
            Product = 1,
            Sample = 2,
            CalDummy = 3,
            EolDummy = 4
        }

        public enum States
        {
            Unprogrammed = 0b00000000,
            Calibrated = 0b00000001,
            FunctionTest = 0b00000011,
            EolTest = 0b00000111
        }

        public VtepMANU_DATA(ISubcommandExecutor executor) : base(executor) { }

        public Customers Customer
        {
            get => (Customers)buffer[0];
            set => buffer[0] = (byte)value;
        }

        public Types Type
        {
            get => (Types)buffer[1];
            set => buffer[1] = (byte)value;
        }

        public States State
        {
            get => (States)buffer[2];
            set => buffer[2] = (byte)value;
        }

        public byte LayoutVersion
        {
            get => buffer[3];
            set => buffer[3] = value;
        }

        public long ItemCode
        {
            get => ArrayMapper.GetBCD(buffer, 4, 7);
            set => ArrayMapper.SetBCD(buffer, 4, 7, value);
        }

        public long SerialNumber
        {
            get => ArrayMapper.GetBCD(buffer, 8, 10);
            set => ArrayMapper.SetBCD(buffer, 8, 10, value);
        }
    }
}
