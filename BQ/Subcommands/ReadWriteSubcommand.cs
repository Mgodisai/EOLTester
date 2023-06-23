using System;
using System.ComponentModel;
using VTEP.Arrays;

namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public abstract class ReadWriteSubcommand : ReadSubcommand
    {
        public ReadWriteSubcommand(ISubcommandExecutor executor, int bufferSize)
            : base(executor, bufferSize)
        { }

        public void Write()
        {
            Executor.Write(Code, buffer);
        }
    }

    public class MANU_DATA : ReadWriteSubcommand
    {
        public override ushort Code => 0x0070;

        public MANU_DATA(ISubcommandExecutor executor) : base(executor, 32) { }

        public void SetBuffer(byte[] data)
        {
            Array.Copy(data, 0, buffer, 0, data.Length);
        }
    }

    public class CB_ACTIVE_CELLS : ReadWriteSubcommand
    {
        public override ushort Code => 0x0083;

        public CB_ACTIVE_CELLS(ISubcommandExecutor executor) : base(executor, 2) { }

        [DisplayName("Active Cells")]
        public ushort Active_Cells
        {
            get => ArrayMapper.GetUshort(buffer, 0);
            set => ArrayMapper.SetUshort(buffer, 0, value);
        }
    }
}
