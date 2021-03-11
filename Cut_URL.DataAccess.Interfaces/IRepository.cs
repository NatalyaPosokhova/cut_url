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
        void SaveUrlData(ShortcutUrlData urlData);
        void AddUser(Guid token, string login, string password);
        User GetUserByLogin(string login);
        Session GetSessionByGuid(Guid guid);
    }
}
