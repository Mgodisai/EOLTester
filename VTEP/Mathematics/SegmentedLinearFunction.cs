using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTEP.Mathematics
{
    public class SegmentedLinearFunction<T>
    {
        public List<FunctionPoint<T>> Points { get; set; }

        public SegmentedLinearFunction(List<FunctionPoint<T>> points)
        {
            Points = points;
        }

        public T GetY(T x)
        {
            FunctionPoint<T>[] xOrder = Points.OrderBy(p => p.X).ToArray();

            int i1 = xOrder.Length - 2;
            while (i1 > 0 && x < xOrder[i1].X)
            {
                i1--;
            }
            LinearFunction<T> f = new LinearFunction<T>(xOrder[i1], xOrder[i1 + 1]);
            return f.GetY(x);
        }

        public T GetX(T y)
        {
            FunctionPoint<T>[] yOrder = Points.OrderBy(p => p.Y).ToArray();

            int i1 = yOrder.Length - 2;
            while (i1 > 0 && y < yOrder[i1].Y)
            {
                i1--;
            }
            LinearFunction<T> f = new LinearFunction<T>(yOrder[i1], yOrder[i1 + 1]);
            return f.GetX(y);
        }
    }
}
