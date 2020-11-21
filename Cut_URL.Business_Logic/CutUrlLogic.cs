using Cut_URL.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.Business_Logic
{
    public class CutUrlLogic : ICutUrlLogic
    {
        private IRepository _repository;
        private IShortUrlGenerator _urlGenerator;
        public CutUrlLogic(IRepository repository, IShortUrlGenerator urlGenerator)
        {
            _repository = repository;
            _urlGenerator = urlGenerator;
        }

        public string CreateShortUrlFromLong(string longUrl, string userId)
        {
            var shortUrl = String.Empty;
            do
            {
                shortUrl = _urlGenerator.GetShortUrl(longUrl);

            } while(_repository.IsShortUrlExists(shortUrl));

            _repository.AddShortUrlData(userId, shortUrl, longUrl);

            return shortUrl;
        }
    }
}
