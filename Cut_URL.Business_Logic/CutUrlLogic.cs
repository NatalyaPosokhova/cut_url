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
            try
            {
                do
                {
                    shortUrl = _urlGenerator.GetShortUrl(longUrl);
                    var test = _repository.IsShortUrlExists(shortUrl);

                } while (_repository.IsShortUrlExists(shortUrl));

                _repository.GetUrlDataByShortUrl(shortUrl);
                _repository.AddShortUrlData(userId, shortUrl, longUrl);
            }
            catch(DataAccessException ex)
            {
                throw new DataAccessException(ex.Message);
            }
          
            return shortUrl;
        }

        public string GetLongUrlFromShort(string shortUrl, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
