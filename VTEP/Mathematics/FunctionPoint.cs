using System;

namespace VTEP.Mathematics
{
    public class FunctionPoint<T>
    {
        public dynamic X { get; set; }

        public dynamic Y { get; set; }

        public FunctionPoint(T x, T y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Format("X = {0}, Y = {1}", X, Y);
        }
    }
}
