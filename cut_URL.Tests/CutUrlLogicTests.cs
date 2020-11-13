using NUnit.Framework;
using NSubstitute;
using Cut_URL.DataAccess;
using Cut_URL.Business_Logic;
using System;

namespace Cut_URL.Tests
{
    public class CreateShortUrlTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void LongUrtToShortShoulBeSuccess()
        {
            //Arrange
            string longUrl = "https://docs.google.com/";
            string expectedUrl = "cuturl.local/google";
            string userId = "1234";

            IShortUrlGenerator generator = Substitute.For<IShortUrlGenerator>();
            generator.GetShortUrl(longUrl).Returns(expectedUrl);

            IRepository repository = Substitute.For<IRepository>();
            repository.IsShortUrlExists(expectedUrl).Returns(false);
            repository.AddShortUrlData(userId, expectedUrl, longUrl);

            ICutUrlLogic logic = new CutUrlLogic(repository, generator);

            //Actual
            string actualUrl = logic.CreateShortUrlFromLong(longUrl, userId);

            //Assert
            Assert.AreEqual(expectedUrl, actualUrl);
            repository.Received().IsShortUrlExists(expectedUrl);
            repository.Received().AddShortUrlData(userId, expectedUrl, longUrl);
        }

        [Test]
        public void CannotAddUrlToDatabase()
        {
            //Arrange
            string userId = "1234";
            string shortUrl = "cuturl.local/google";
            string longUrl = "https://docs.google.com/";
            ShortcutUrlData urlData = new ShortcutUrlData()
            {
                UserId = userId,
                ShortUrl = shortUrl,
                LongUrl = longUrl,
                Date = DateTime.Now,
                TransferQuantity = 0
            };
            
            var repository = Substitute.For<IRepository>();
            repository.IsShortUrlExists(shortUrl).Returns(false);
            repository.GetUrlDataByShortUrl(shortUrl).Returns(urlData);
            repository.When(x => x.AddShortUrlData(userId, shortUrl, longUrl)).Do(x => { throw new DataAccessException("Cannot add Url to database."); });

            var generator = Substitute.For<IShortUrlGenerator>();
            generator.GetShortUrl(longUrl).Returns(shortUrl);

            ICutUrlLogic logic = new CutUrlLogic(repository, generator);

            //Actual
            var actualUrl = logic.CreateShortUrlFromLong(longUrl, userId);

            //Assert
            Assert.Throws<DataAccessException>(() => logic.CreateShortUrlFromLong(longUrl, shortUrl));
        }

        [Test]
        public void NotUniqueUrlShouldBeGeneratedNew()
        {
            //Arrange
            string longUrl = "https://docs.google.com/";
            string shortUrl = "cuturl.local/google";

            var generator = Substitute.For<IShortUrlGenerator>();
            generator.GetShortUrl(longUrl).Returns(shortUrl);

            var repository = Substitute.For<IRepository>();
            repository.When(x => x.GetUrlDataByShortUrl(shortUrl)).Do(x => { throw new DataAccessException("The Url has already exists in DataBase."); });

            ICutUrlLogic logic = new CutUrlLogic(repository, generator);

            //Actual
            //Assert

        }
        [Test]
        public void CannotGetUrlDataShouldBeException()
        {
            //Arrange
            string shortUrl = "cuturl.local/google";

            var repository = Substitute.For<IRepository>();
            repository.When(x => x.GetUrlDataByShortUrl(shortUrl)).Do(x => { throw new DataAccessException("Cannot get data from Database."); });

            var generator = Substitute.For<IShortUrlGenerator>();

            ICutUrlLogic logic = new CutUrlLogic(repository, generator);
            //Actual
            //Assert

        }
    }
}