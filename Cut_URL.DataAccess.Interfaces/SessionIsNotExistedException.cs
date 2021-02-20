using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.DataAccess
{
    public class SessionIsNotExistedException : Exception
    {
        public SessionIsNotExistedException(string message, Exception ex = null) : base(message, ex)
        {

        }
    }
}
