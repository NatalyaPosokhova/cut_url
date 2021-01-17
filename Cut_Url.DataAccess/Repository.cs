using Cut_URL.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_Url.DataAccess
{
    class Repository : IRepository
    {
        private ShortcutUrlData urlData;
        public Repository()
        {
            urlData = new ShortcutUrlData();
        }
        public void AddShortUrlData(string userId, string shortUrl, string longUrl)
        {
            throw new NotImplementedException();
        }

        public ShortcutUrlData GetUrlDataByShortUrl(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public bool IsShortUrlExists(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public void SaveUrlData(ShortcutUrlData urlData)
        {
            throw new NotImplementedException();
        }
    }
}
