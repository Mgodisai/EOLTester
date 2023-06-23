using System;
using System.Runtime.Serialization;

namespace AlberEOL.Exceptions
{
    public class DeviceException : ApplicationException
    {
        public DeviceException() : base()
        {

        }

        public DeviceException(string s) : base(s)
        {

        }

        public DeviceException(string s, Exception e) : base(s, e)
        {

        }

        public DeviceException(SerializationInfo info, StreamingContext cxt) : base(info, cxt)
        {

        }
    }
}
