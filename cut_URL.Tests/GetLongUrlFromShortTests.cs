using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NSubstitute;
using Cut_URL.DataAccess;
using Cut_URL.Business_Logic;

namespace Cut_URL.Tests
{
    public class GetLongUrlFromShortTests
    {
        [Test]
        public void TryGetLongUrlShouldBeSuccess()
        {
            //Arrange
            string shortUrl = "cuturl.local/google";
            string longExpectedUrl = "https://docs.google.com/";
            string userId = "1234";
            ShortcutUrlData urlData = new ShortcutUrlData()
            {
                UserId = userId,
                ShortUrl = shortUrl,
                LongUrl = longExpectedUrl,
                Date = DateTime.Now,
                TransferQuantity = 0
            };

            var generator = Substitute.For<IShortUrlGenerator>();
            var repository = Substitute.For<IRepository>();
            repository.GetUrlDataByShortUrl(shortUrl).Returns(urlData);

            ICutUrlLogic logic = new CutUrlLogic(repository, generator);

            //Actual
            string longActualUrl = logic.GetLongUrlFromShort(shortUrl, userId);

            //Assert
            Assert.AreEqual(longExpectedUrl, longActualUrl);
            repository.Received().GetUrlDataByShortUrl(shortUrl);
        }

        [Test]
        public void CannotGetUrlFromDatabase()
        {
            //Arrange
            string shortUrl = "cuturl.local/google";
            string longExpectedUrl = "https://docs.google.com/";
            string userId = "1234";

            var generator = Substitute.For<IShortUrlGenerator>();
            var repository = Substitute.For<IRepository>();
            repository.GetUrlDataByShortUrl(shortUrl);
            repository.When(x => x.GetUrlDataByShortUrl(shortUrl)).Do(x => { throw new DataAccessException("Cannot get Url from database."); });

            var logic = new CutUrlLogic(repository, generator);

            //Actual
            //Assert
            Assert.Throws<DataAccessException>(() => logic.GetLongUrlFromShort(shortUrl, userId));
        }
    }
}
