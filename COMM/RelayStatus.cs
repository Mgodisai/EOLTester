using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alber.Eol.Hardware
{
    public enum RelayStatus
    {
        AllOFF = 0,
        Load2Ohm = 1,
        Load1Ohm = 2,
        Load0_5Ohm = 6,
        Charge = 8
    }
}
