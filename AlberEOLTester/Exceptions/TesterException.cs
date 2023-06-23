using System;
using System.Runtime.Serialization;

namespace AlberEOL.Exceptions
{
    public class TesterException : ApplicationException
    {
        public TesterException() : base()
        {

        }

        public TesterException(string s) : base(s)
        {

        }

        public TesterException(string s, Exception e) : base(s, e)
        {

        }

        public TesterException(SerializationInfo info, StreamingContext cxt) : base(info, cxt)
        {

        }
    }
}
