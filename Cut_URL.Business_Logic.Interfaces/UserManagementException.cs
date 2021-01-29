using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.Business_Logic
{
    public class UserManagementException : Exception
    {
        public UserManagementException(string message, Exception ex = null) : base(message, ex)
        {

        }
    }
}
