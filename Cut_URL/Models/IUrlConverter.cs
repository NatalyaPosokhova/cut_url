﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cut_URL.Models
{
    interface IUrlConverter
    {
        public string GetShortUrl(string longUrl);
    }
}