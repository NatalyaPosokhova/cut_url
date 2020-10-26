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
                return db.Query<Url>("SELECT * FROM Url WHERE userId = @userId").ToList();
            }
        }

        public void AddUrl(Url Url)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Url (userId, longUrl, shortUrl, transitionsQuantity = 0, creationDate = DateTime.Now) VALUES(@userId, @longUrl, @shortUrl, @transitionsQuantity, @creationDate)";
                db.Execute(sqlQuery, Url);

                // если мы хотим получить id добавленного пользователя
                //var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                //int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                //user.Id = userId.Value;
            }
        }

        public void UpdateUrlTransitionsQuantity(Url Url)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                int updatedTransitionsQuantity = Convert.ToInt32(db.Query<Url>("SELECT transitionsQuantity FROM Url WHERE userId = @userId")) + 1;
                var sqlQuery = $"UPDATE Url SET transitionsQuantity = @updatedTransitionsQuantity WHERE userId = @userId";
                db.Execute(sqlQuery, Url);
            }
        }
        public void Create(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Users (Id, Name) VALUES(@Id, @Name)";
                db.Execute(sqlQuery, user);
            }
        }
        //public void Delete(int userId)
        //{
        //    using (IDbConnection db = new SqlConnection(connectionString))
        //    {
        //        var sqlQuery = "DELETE FROM Users WHERE Id = @userId";
        //        db.Execute(sqlQuery, new { userId });
        //    }
        //}
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

    }
}
