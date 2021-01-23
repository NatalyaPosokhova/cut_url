using System;
using CutUrlLogic.DataAccess;
using Cut_URL.Business_Logic;
using Cut_URL.DataAccess;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using KellermanSoftware.CompareNetObjects;

namespace Cut_URL.Tests
{
    public class IntegrationTests
    {
        private IRepository _repository;
        private ShortUrlGenerator _generator;
        private ICutUrlLogic _logic;

        [SetUp]
        public void SetUp()
        {
            _repository = new Repository("server=localhost;database=cutUrlDB;user=root;password=qwerty");
            _generator = Substitute.For<ShortUrlGenerator>();
            _logic = new Business_Logic.CutUrlLogic(_repository, _generator);
        }

        //[TestFixture]
        //public class SuccessTests
        //{
        //    [TestFixtureSetUp]
        //    public void Init()
        //    { /* ... */ }
        //}

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
            ShortcutUrlData actualUrlData  = _repository.GetUrlDataByShortUrl(shortUrl);

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
