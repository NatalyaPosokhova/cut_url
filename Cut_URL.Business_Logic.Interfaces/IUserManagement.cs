﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.Business_Logic
{
    public interface IUserManagement
    {
        Guid RegisterNewUser(string login, string password);
        Guid LoginUser(string login, string password);
    }
}
