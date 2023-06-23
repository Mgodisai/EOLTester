using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public interface ISubcommandExecutor
    {
        void Send(ushort code);

        void Read(ushort code, byte[] outBuffer);

        void Write(ushort code, byte[] buffer);
    }
}
