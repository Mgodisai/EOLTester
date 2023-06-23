using System;

namespace VTEP.Electronic
{
    public class NTC
    {
        public const double KELVIN_0_CELSIUS = 273.0; 

        public double R0 { get;  }

        public double T0 { get;  }

        public double B { get;  }

        public NTC(double r0, double t0, double b)
        {
            R0 = r0;
            T0 = t0;
            B = b;
        }

        public double GetR(double t)
        {
            return R0 / Math.Exp(B * (1 / (T0 + KELVIN_0_CELSIUS) - 1 / (t + KELVIN_0_CELSIUS)));
        }

        public double GetT(double r)
        {
            return 1 / (1 / (T0 + KELVIN_0_CELSIUS) - Math.Log(R0 / r)/B) - KELVIN_0_CELSIUS;
        }
    }
}
