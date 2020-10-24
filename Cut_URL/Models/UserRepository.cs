using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cut_URL.Models
{
    public class UserRepository : IUserRepository
    {
        string connectionString = null;
        public UserRepository(string connecttion)
        {
            connectionString = connecttion;
        }

        public List<Url> GetUrls(int userId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Url>($"SELECT * FROM Url WHERE Url.userId = {userId}").ToList();
            }
        }

        public void AddUrl(int userId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Url (longUrl, shortUrl, transitionsQuantity, creationDate) VALUES(@longUrl, @shortUrl, @transitionsQuantity, @creationDate)";
                db.Execute(sqlQuery, userId);

                // если мы хотим получить id добавленного пользователя
                //var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                //int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                //user.Id = userId.Value;
            }
        }

        public void UpdateUrlTransitionsQuantity(int userId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                int _transitionsQuantity = Convert.ToInt32(db.Query<Url>("SELECT transitionsQuantity FROM Url WHERE userId = @userId")) + 1;
                var sqlQuery = $"UPDATE Url SET {_transitionsQuantity} = @transitionsQuantity WHERE userId = @userId";
                db.Execute(sqlQuery, userId);
            }
        }

        //public List<User> GetUsers()
        //{
        //    using (IDbConnection db = new SqlConnection(connectionString))
        //    {
        //        return db.Query<User>("SELECT * FROM Users").ToList();
        //    }
        //}

        //public User Get(int id)
        //{
        //    using (IDbConnection db = new SqlConnection(connectionString))
        //    {
        //        return db.Query<User>("SELECT * FROM Users WHERE Id = @id", new { id }).FirstOrDefault();
        //    }
        //}

        //public void Delete(int id)
        //{
        //    using (IDbConnection db = new SqlConnection(connectionString))
        //    {
        //        var sqlQuery = "DELETE FROM Users WHERE Id = @id";
        //        db.Execute(sqlQuery, new { id });
        //    }
        //}
    }
}
