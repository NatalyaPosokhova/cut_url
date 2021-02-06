using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.Business_Logic
{
    public class UnableToLoginUserException : UserManageException
    {
        public UnableToLoginUserException(string message, Exception ex = null) : base(message, ex)
        {

        }
    }
}
