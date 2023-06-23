using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public abstract class BaseSubcommand
    {
        protected ISubcommandExecutor Executor { get; }

        [Browsable(false)]
        public abstract ushort Code { get; }

        public BaseSubcommand(ISubcommandExecutor executor)
        {
            Executor = executor;
        }

        public override string ToString()
        {
            return "0x" + Code.ToString("X4");
        }
    }
}
