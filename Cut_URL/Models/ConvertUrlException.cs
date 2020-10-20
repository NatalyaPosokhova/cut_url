using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cut_URL.Models
{
    public class ConvertUrlException : Exception
    {
        public ConvertUrlException(string message, Exception e = null) : base(message, e)
        {

        }
    }
}
