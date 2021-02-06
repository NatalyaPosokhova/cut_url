using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.Business_Logic
{
    public class IncorrectPasswordException : UserManageException
    {
        public IncorrectPasswordException(string message, Exception ex = null) : base(message, ex)
        {

        }
    }
}
