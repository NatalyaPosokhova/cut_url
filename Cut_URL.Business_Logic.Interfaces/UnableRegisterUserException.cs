using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.Business_Logic
{
    public class UnableRegisterUserException : UserManageException
    {
        public UnableRegisterUserException(string message, Exception ex = null) : base(message, ex)
        {

        }
    }
}
