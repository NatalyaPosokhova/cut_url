using Cut_URL.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Configuration;
using MySqlConnector;
using System.Data;
using System.Linq;
using Cut_Url.DataAccess;

namespace CutUrlLogic.DataAccess
{
    public class Repository : IRepository
    {
        string _connectionString = null;


        public Repository()
        {
            _connectionString = "server=localhost;database=cutUrlDB;user=root;password=qwerty";//connectionString;

            SqlMapper.AddTypeHandler(new GuidHandler());
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
                    if (urlData != null)
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
            try
            {
                using (IDbConnection db = new MySqlConnection(_connectionString))
                {
                    User user = new User
                    {
                        UserId = token,
                        Login = login,
                        Password = password
                    };
                    var mySqlQuery = "INSERT INTO User (UserId, Login, Password) VALUES(@UserId, @Login, @Password)";
                    db.Execute(mySqlQuery, user);
                }
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error during adding data to database occured.");
            }
        }

        public User GetUserByLogin(string login)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(_connectionString))
                {
                    String sql = "SELECT UserId, Password, Login FROM User WHERE Login = @login";
                    User user = db.Query<User>(sql, new { login }).FirstOrDefault();

                    return user;
                }
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error occured during get data from database.");
            }
        }

        public Session GetSessionByGuid(Guid guid)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                String sql = "SELECT Id, LastAccessTime FROM Session WHERE Id = @guid";
                Session session = db.Query<Session>(sql, new { guid }).FirstOrDefault();

                return session;
            }
        }

        public IEnumerable<ShortcutUrlData> GetAllUserUrlData(string userId)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(_connectionString))
                {
                    return db.Query<ShortcutUrlData>("SELECT * FROM ShortcutUrlData WHERE UserId = @userId", new { userId });
                }
            }
            catch (DataAccessException)
            {
                throw new DataAccessException("Error occured during get data from database.");
            }
        }
    }
}
