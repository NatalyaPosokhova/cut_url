using Cut_URL.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Configuration;
using MySqlConnector;
using System.Data;

namespace Cut_Url.DataAccess
{
    class Repository : IRepository
    {
        string _connectionString = null;
        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddShortUrlData(string userId, string shortUrl, string longUrl)
        {
            ShortcutUrlData urlData = new ShortcutUrlData()
            {
                UserId = userId,
                ShortUrl = shortUrl,
                LongUrl = longUrl,
                Date = DateTime.Now,
                TransferQuantity = 0
            };

            try
            {
                using (IDbConnection db = new MySqlConnection(_connectionString))
                {
                    var mySqlQuery = "INSERT INTO UrlData (UserId, ShortUrl, LongUrl, Date, TransferQuantity) VALUES(@UserId, @ShortUrl, @LongUrl, @Date, @TransferQuantity)";
                    db.Execute(mySqlQuery, urlData);
                }
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error during adding data to database occured.");
            }
        }

        public ShortcutUrlData GetUrlDataByShortUrl(string shortUrl)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(_connectionString))
                {
                    return (ShortcutUrlData)db.Query<ShortcutUrlData>("SELECT * FROM UrlData WHERE ShortUrl = @shortUrl", new { shortUrl });
                }
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error during adding data to database occured.");
            }
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
