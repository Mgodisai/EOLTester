using System;

namespace VTEP.Mathematics
{
    public class LinearFunction<T> 
    {
        T A { get; set; }

        T B { get; set; }

        public LinearFunction(T a, T b)
        {
            A = a;
            B = b;
        }

        public LinearFunction(FunctionPoint<T> p1, FunctionPoint<T> p2)
        {
            A = (p2.Y - p1.Y) / (p2.X - p1.X);
            B = p2.Y - A * p2.X;
        }

        public T GetY(dynamic x) => A * x + B;

        public T GetX(dynamic y) => (y - B) / A;

        public override string ToString()
        {
            return string.Format("A = {0}, B = {1}", A, B);
        }
    }
}
