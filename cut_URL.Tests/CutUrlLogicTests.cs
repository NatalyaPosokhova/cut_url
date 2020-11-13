using NUnit.Framework;
using NSubstitute;
using Cut_URL.DataAccess;
using Cut_URL.Business_Logic;

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
            string shortUrl = "cuturl.local/google";
            var generator = Substitute.For<IShortUrlGenerator>();
            generator.GetShortUrl(longUrl).Returns(shortUrl);
           
            //Actual


            //Assert
        }

        [Test]
        public void CannotAddUrlToDatabase()
        {
            //Arrange
            string userId = "1";
            string shortUrl = "cuturl.local/google";
            string longUrl = "https://docs.google.com/";
            
            var repository = Substitute.For<IRepository>();
            repository.When(x => x.AddShortUrlData(userId, shortUrl, longUrl)).Do(x => { throw new DataAccessException("Cannot add Url to database."); });

            var generator = Substitute.For<IShortUrlGenerator>();

            var logic = new CutUrlLogic(repository, generator);
            //Actual
            //Assert
            Assert.Throws<DataAccessException>(() => logic.Run());
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

            var logic = new CutUrlLogic(repository, generator);

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

            var logic = new CutUrlLogic(repository, generator);
            //Actual
            //Assert
            Assert.Throws<DataAccessException>(() => logic.Run());
        }
    }
}