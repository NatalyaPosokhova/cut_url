using NUnit.Framework;
using NSubstitute;
using Cut_URL.DataAccess;
using Cut_URL.Business_Logic;

namespace Cut_URL.Tests
{
    public class CreateShortUrlTests
    {
        private ICutUrlLogic logic;
        [SetUp]
        public void Setup()
        {
            logic = new CutUrlLogic(null);
        }

        [Test]
        public void LongUrtToShortShoulBeSuccess()
        {
            //Arrange
            string longUrl = "https://docs.google.com/";
            //Actual

            //Assert
        }
    }
}