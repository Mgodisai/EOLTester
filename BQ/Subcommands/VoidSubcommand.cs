using System.ComponentModel;

namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public abstract class VoidSubcommand : BaseSubcommand
    {
        [Browsable(false)]
        public int CallCount { get; private set; }

        public VoidSubcommand(ISubcommandExecutor executor) : base(executor) { }

        public void Send()
        {
            CallCount++;
            Executor.Send(Code);
        }

        public override string ToString()
        {
            return base.ToString() + " " + CallCount;
        }
    }

    public class SHUTDOWN : VoidSubcommand
    {
        public override ushort Code => 0x0010;

        public SHUTDOWN(ISubcommandExecutor executor) : base(executor) { }
    }

    public class RESET : VoidSubcommand
    {
        public override ushort Code => 0x0012;

        public RESET(ISubcommandExecutor executor) : base(executor) { }
    }

    public class FUSE_TOGGLE : VoidSubcommand
    {
        public override ushort Code => 0x001D;

        public FUSE_TOGGLE(ISubcommandExecutor executor) : base(executor) { }
    }

    public class PF_ENABLE : VoidSubcommand
    {
        public override ushort Code => 0x0024;

        public PF_ENABLE(ISubcommandExecutor executor) : base(executor) { }
    }

    public class RESET_PASSQ : VoidSubcommand
    {
        public override ushort Code => 0x0082;

        public RESET_PASSQ(ISubcommandExecutor executor) : base(executor) { }
    }

    public class SET_CFGUPDATE : VoidSubcommand
    {
        public override ushort Code => 0x0090;

        public SET_CFGUPDATE(ISubcommandExecutor executor) : base(executor) { }
    }

    public class EXIT_CFGUPDATE : VoidSubcommand
    {
        public override ushort Code => 0x0092;

        public EXIT_CFGUPDATE(ISubcommandExecutor executor) : base(executor) { }
    }

    public class DSG_PDSG_OFF : VoidSubcommand
    {
        public override ushort Code => 0x0093;

        public DSG_PDSG_OFF(ISubcommandExecutor executor) : base(executor) { }
    }

    public class ALL_FETS_OFF : VoidSubcommand
    {
        public override ushort Code => 0x0095;

        public ALL_FETS_OFF(ISubcommandExecutor executor) : base(executor) { }
    }

    public class ALL_FETS_ON : VoidSubcommand
    {
        public override ushort Code => 0x0096;

        public ALL_FETS_ON(ISubcommandExecutor executor) : base(executor) { }
    }

    public class SLEEP_ENABLE : VoidSubcommand
    {
        public override ushort Code => 0x0099;

        public SLEEP_ENABLE(ISubcommandExecutor executor) : base(executor) { }
    }

    public class SLEEP_DISABLE : VoidSubcommand
    {
        public override ushort Code => 0x009A;

        public SLEEP_DISABLE(ISubcommandExecutor executor) : base(executor) { }
    }

    public class SWAP_COMM_MODE : VoidSubcommand
    {
        public override ushort Code => 0x29BC;

        public SWAP_COMM_MODE(ISubcommandExecutor executor) : base(executor) { }
    }
}
