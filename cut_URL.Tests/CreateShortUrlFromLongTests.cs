using NUnit.Framework;
using NSubstitute;
using Cut_URL.DataAccess;
using Cut_URL.Business_Logic;
using System;

namespace Cut_URL.Tests
{
    public class CreateShortUrlTests
    {
        private IShortUrlGenerator generator;
        private IRepository repository;
        private ICutUrlLogic logic;
        private string longUrl;
        private string expectedShortUrl;
        private string userId;
        [SetUp]
        public void Setup()
        {
            longUrl = "https://docs.google.com/";
            expectedShortUrl = "cuturl.local/google";
            userId = "1234";

            generator = Substitute.For<IShortUrlGenerator>();
            repository = Substitute.For<IRepository>();
            logic = new CutUrlLogic(repository, generator);
        }

        [Test]
        public void LongUrtToShortShoulBeSuccess()
        {
            //Arrange
            generator.GetShortUrl(longUrl).Returns(expectedShortUrl);

            repository.IsShortUrlExists(expectedShortUrl).Returns(false);
            repository.AddShortUrlData(userId, expectedShortUrl, longUrl);

            //Actual
            string actualUrl = logic.CreateShortUrlFromLong(longUrl, userId);

            //Assert
            Assert.AreEqual(expectedShortUrl, actualUrl);
            repository.Received().IsShortUrlExists(expectedShortUrl);
            repository.Received().AddShortUrlData(userId, expectedShortUrl, longUrl);
        }

        [Test]
        public void CannotAddUrlToDatabase()
        {
            ShortcutUrlData urlData = new ShortcutUrlData()
            {
                UserId = userId,
                ShortUrl = expectedShortUrl,
                LongUrl = longUrl,
                Date = DateTime.Now,
                TransferQuantity = 0
            };
            
            generator.GetShortUrl(longUrl).Returns(expectedShortUrl);

            repository.IsShortUrlExists(expectedShortUrl).Returns(false);
            repository.GetUrlDataByShortUrl(expectedShortUrl).Returns(urlData);
            repository.When(x => x.AddShortUrlData(userId, expectedShortUrl, longUrl)).Do(x => { throw new DataAccessException("Cannot add Url to database."); });

            //Actual
            //Assert
            Assert.Throws<DataAccessException>(() => logic.CreateShortUrlFromLong(longUrl, userId));
        }

        [Test]
        public void NotUniqueUrlShouldBeGeneratedNew()
        {
            //Arrange
            string secondShortUrl = "cuturl.local/ggl";

            generator.GetShortUrl(longUrl).Returns(expectedShortUrl, secondShortUrl);

            repository.IsShortUrlExists(expectedShortUrl).Returns(true);
            repository.IsShortUrlExists(secondShortUrl).Returns(false);

            //Actual
            var actual = logic.CreateShortUrlFromLong(longUrl, userId);

            //Assert
            Assert.AreEqual(actual, secondShortUrl);
        }
        [Test]
        public void CannotGetUrlDataShouldBeException()
        {
            //Arrange
            repository.IsShortUrlExists(expectedShortUrl).Returns(false);
            repository.When(x => x.GetUrlDataByShortUrl(expectedShortUrl)).Do(x => { throw new DataAccessException("Cannot get data from Database."); });

            generator.GetShortUrl(longUrl).Returns(expectedShortUrl);

            //Actual
            //Assert
            Assert.Throws<DataAccessException>(() => logic.CreateShortUrlFromLong(longUrl, userId));
        }
    }
}