using System;
using CutUrlLogic.DataAccess;
using Cut_URL.Business_Logic;
using Cut_URL.DataAccess;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using KellermanSoftware.CompareNetObjects;
using MySqlConnector;
using System.Data;
using Dapper;

namespace Cut_URL.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        private IRepository _repository;
        private ShortUrlGenerator _generator;
        private ICutUrlLogic _logic;
        private const string connectionString = "server=localhost;database=cutUrlDB;user=root;password=qwerty";

        [SetUp]
        public void SetUp()
        {
            _repository = new Repository(connectionString);
            _generator = Substitute.For<ShortUrlGenerator>();
            _logic = new Business_Logic.CutUrlLogic(_repository, _generator);
        }
        [OneTimeSetUp]
        public void Init()
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                var mySqlQuery = "DELETE FROM ShortcutUrlData";
                db.Execute(mySqlQuery);
            }
        }

        [Test]
        public void TryInsertShortCutUrlDataShouldBeSuccess()
        {
            string userId = "1234";
            string shortUrl = "cuturl.local/google";
            string longUrl = "https://docs.google.com/";

            _repository.AddShortUrlData(userId, shortUrl, longUrl);
        }

        [Test]
        public void TryGetUrlDataByShortUrlShouldBeSuccess()
        {
            //Arrange
            string userId = "1234";
            string shortUrl = "cuturl.local/google";
            string longUrl = "https://docs.google.com/";
            int transferQuantity = 0;

            ShortcutUrlData expectedUrlData = new ShortcutUrlData
            {
                UserId = userId,
                ShortUrl = shortUrl,
                LongUrl = longUrl,
                TransferQuantity = transferQuantity
            };

            //Actual
            _repository.AddShortUrlData(userId, shortUrl, longUrl);
            ShortcutUrlData actualUrlData = _repository.GetUrlDataByShortUrl(shortUrl);

            //Assert
            Assert.IsTrue(expectedUrlData.Equals(actualUrlData));
        }

        [Test]
        public void CheckShortUrlExistsShouldBeSuccess()
        {
            //Arrange
            string userId = "1234";
            string shortUrl = "cuturl.local/google";
            string longUrl = "https://docs.google.com/";

            //Actual
            _repository.AddShortUrlData(userId, shortUrl, longUrl);

            //Assert
            Assert.IsTrue(_repository.IsShortUrlExists(shortUrl));
        }
    }
}
