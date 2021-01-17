using System;
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
            string userId = "1234";

            var generator = Substitute.For<IShortUrlGenerator>();
            var repository = Substitute.For<IRepository>();
            repository.When(x => x.GetUrlDataByShortUrl(shortUrl)).Do(x => { throw new DataAccessException("Cannot get Url from database."); });

            var logic = new CutUrlLogic(repository, generator);

            //Actual
            //Assert
            Assert.Throws<GetLongUrlException>(() => logic.GetLongUrlFromShort(shortUrl, userId));
        }

        [Test]
        public void TryGetLongUrlTransferQuantityShouldBeIncreased()
        {
            //Arrange

            string shortUrl = "cuturl.local/google";
            string userId = "1234";

            ShortcutUrlData urlData = new ShortcutUrlData()
            {
                UserId = "1234",
                ShortUrl = "cuturl.local/google",
                LongUrl = "https://docs.google.com/",
                Date = DateTime.Now,
                TransferQuantity = 0
            };

            var generator = Substitute.For<IShortUrlGenerator>();
            var repository = Substitute.For<IRepository>();
            var old_val = urlData.TransferQuantity;

            repository.GetUrlDataByShortUrl(shortUrl).Returns(urlData);

            var logic = new CutUrlLogic(repository, generator);
            logic.GetLongUrlFromShort(shortUrl, userId);

            var new_val = urlData.TransferQuantity;
            //Actual
            //Assert
            repository.Received().SaveUrlData(urlData);
            Assert.AreEqual(old_val + 1, new_val);
        }

        //Test deleted as returned data should be checked on DataAccess Layer
        //[Test]
        //public void TryGetLongUrlWithDatabaseMissedShortUrlShouldBeError()
        //{
        //    //Arrange
        //    string shortUrl = "cuturl.local/google";
        //    string userId = "1234";
        //    ShortcutUrlData urlData = new ShortcutUrlData()
        //    {
        //        UserId = null,
        //        ShortUrl = null,
        //        LongUrl = null,
        //        Date = DateTime.Now,
        //        TransferQuantity = 0
        //    };

        //    var generator = Substitute.For<IShortUrlGenerator>();
        //    var repository = Substitute.For<IRepository>();
        //    repository.GetUrlDataByShortUrl(shortUrl).Returns(urlData);

        //    var logic = new CutUrlLogic(repository, generator);

        //    //Actual
        //    //Assert
        //    Assert.Throws<GetLongUrlException>(() => logic.GetLongUrlFromShort(shortUrl, userId));
        //}
    }
}
