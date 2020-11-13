using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.DataAccess
{
    public class DataAccessException : Exception
    {
        public DataAccessException(string message, Exception e = null) : base(message, e)
        {
        }
    }
}
