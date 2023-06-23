using System;

namespace AlberEOL.CustomClasses
{
    public class TOTimer
    {
        public double Interval { get; set; }
        public DateTime StartTime { get; private set; }
        public bool TimeOut
        {
            get
            {
                TimeSpan elapsed = DateTime.Now - StartTime;
                return elapsed.TotalSeconds >= Interval;
            }
        }

        public void Start(double interval)
        {
            Interval = interval;
            StartTime = DateTime.Now;
        }

        public void Wait(double interval)
        {
            Start(interval);
            while (!TimeOut)
            {
            }
        }
    }
}
