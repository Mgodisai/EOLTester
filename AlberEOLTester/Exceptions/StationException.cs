using System;
using System.Runtime.Serialization;

namespace AlberEOL.Exceptions
{
    class StationException : ApplicationException
    {
        public StationException() : base()
        {

        }

        public StationException(string s) : base(s)
        {

        }

        public StationException(string s, Exception e) : base(s, e)
        {

        }

        public StationException(SerializationInfo info, StreamingContext cxt) : base(info, cxt)
        {

        }
    }
}
