using NUnit.Framework;
using Cut_URL.Models;
using System;

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
        public void TryGetShortUrlLengthShouldBeSuccessTest()
        {
            //Arrange
            var expected = 6;
            var longUrl = "https://docs.google.com/document/d/1tH70Xqis12ad0o-ySawhDjMozNrqTpfhl_D30aJ0RjQ/edit";


            //Actual
            var shortUrl = converter.GetShortUrl(longUrl);
            var index = shortUrl.IndexOf("/");
            var actual = shortUrl.Substring(index + 1).Length;

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void EmptyUrlTryGetShortUrlShouldBeErrorTest()
        {
            //Arrange
            //Actual
            //Assert
            Assert.Throws<ConvertUrlException>(() => converter.GetShortUrl(""));
        }
        [Test]
        public void NullUrlTryGetShortUrlShouldBeErrorTest()
        {
            //Arrange
            //Actual
            //Assert
            Assert.Throws<ConvertUrlException>(() => converter.GetShortUrl(null));
        }
    }
}