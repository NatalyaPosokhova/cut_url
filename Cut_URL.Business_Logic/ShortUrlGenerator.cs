﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.Business_Logic
{
    public class ShortUrlGenerator : IShortUrlGenerator
    {
        public string GetShortUrl(string longUrl)
        {
            Random rnd = new Random();
            const string src = "abcdefghijklmnopqrstuvwxyz0123456789";
            int rndStrLength = 6;
            string randomStr = string.Empty;

            if (string.IsNullOrEmpty(longUrl))
            {
                throw new ConvertUrlException("URL is null or empty.");
            }

            for (var i = 0; i < rndStrLength; i++)
            {
                randomStr += src[rnd.Next(0, src.Length)];
            }
            return "cuturl.com/" + randomStr;
        }
    }
}
