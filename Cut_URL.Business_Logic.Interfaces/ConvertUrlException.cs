using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.Business_Logic
{
    public class ConvertUrlException : Exception
    {
        public ConvertUrlException(string message, Exception ex = null) : base(message, ex) { }
    }
}
