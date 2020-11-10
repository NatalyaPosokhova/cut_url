using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.Business_Logic
{
    public interface IShortUrlGenerator
    {
        public string GetShortUrl(string longUrl);
    }
}
