using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.DataAccess
{
    public interface IRepository
    {
        public ShortcutUrlData GetUrlDataByShortUrl(string shortUrl);
        public void AddShortUrlData(string userId, string shortUrl, string longUrl);
        public bool IsShortUrlExists(string shortUrl);
    }
}
