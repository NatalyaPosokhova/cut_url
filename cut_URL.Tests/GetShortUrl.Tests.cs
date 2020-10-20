using NUnit.Framework;
using Cut_URL.Models;

namespace cut_URL.Tests
{
    public class Tests
    {
        private UrlConverter converter;
        [SetUp]
        public void Setup()
        {
            converter = new UrlConverter();
        }

        [Test]
        public void TryGetShortUrlShouldBeSuccessTest()
        {
            //Arrange
            var expected = "https://cuturl.com/yxkbabvl";

            //Actual
            var actual = converter.GetShortUrl("https://docs.google.com/document/d/1tH70Xqis12ad0o-ySawhDjMozNrqTpfhl_D30aJ0RjQ/edit");

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}