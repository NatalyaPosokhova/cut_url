using Cut_URL.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Configuration;
using MySqlConnector;
using System.Data;
using System.Linq;

namespace CutUrlLogic.DataAccess
{
    public class Repository : IRepository
    {
        string _connectionString = null;

        public Repository()
        {
        }

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
                    var mySqlQuery = "INSERT INTO ShortcutUrlData (UserId, ShortUrl, LongUrl, Date, TransferQuantity) VALUES(@UserId, @ShortUrl, @LongUrl, @Date, @TransferQuantity)";
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
                    return db.Query<ShortcutUrlData>("SELECT * FROM ShortcutUrlData WHERE ShortUrl = @shortUrl", new { shortUrl }).FirstOrDefault();
                }
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error occured during get data from database.");
            }
        }

        public bool IsShortUrlExists(string shortUrl)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(_connectionString))
                {
                    var urlData = db.Query<ShortcutUrlData>("SELECT COUNT(1) FROM ShortcutUrlData where ShortUrl=@shortUrl", new { shortUrl });
                    if(urlData != null)
                    {
                        return true;
                    }
                }
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error occured during get data from database.");
            }
            return false;
        }
        public void SaveUrlData(ShortcutUrlData urlData)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE ShortcutUrlData SET Date = @Date, TransferQuantity = @TransferQuantity, ShortUrl = @ShortUrl, LongUrl = @LongUrl  WHERE Id = @Id";
                db.Execute(sqlQuery, urlData);
            }    
        }
        public void AddUser(Guid token, string login, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsUserExistsInDatabase(string login)
        {
            throw new NotImplementedException();
        }
    }
}
