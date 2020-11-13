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
            var generator = Substitute.For<IShortUrlGenerator>();
            generator.GetShortUrl(longUrl).Returns("cuturl.local/google");

            //Actual


            //Assert
        }

        [Test]
        public void CannotAddUrlToDatabase()
        {
            //Arrange
             //Actual

            //Assert

        }
        [Test]
        public void NotUniqueUrlShouldBeGeneratedNew()
        {
            //Arrange
            //Actual

            //Assert
        }
        [Test]
        public void CannotGetUrlDataShouldBeException()
        {
            //Arrange
            //Actual

            //Assert
        }
    }
}