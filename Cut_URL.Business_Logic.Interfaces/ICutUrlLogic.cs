using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.Business_Logic
{
    public interface ICutUrlLogic
    {
        string CreateShortUrlFromLong(string longUrl, string userId);
        string GetLongUrlFromShort(string shortUrl, string userId);
    }
}
