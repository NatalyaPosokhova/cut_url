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

                } while (_repository.IsShortUrlExists(shortUrl));

                _repository.GetUrlDataByShortUrl(shortUrl);
                _repository.AddShortUrlData(userId, shortUrl, longUrl);
                return shortUrl;
            }
            catch (DataAccessException ex)
            {
                throw new CreateShortUrlException(ex.Message, ex);
            }
        }

        public IEnumerable<ShortcutUrlData> GetAllUserInformation(string userId)
        {
            try
            {
                IEnumerable<ShortcutUrlData> userData= _repository.GetAllUserUrlData(userId);
                return userData;
            }
            catch (DataAccessException ex)
            {
                throw new GetAllUserDataException(ex.Message, ex);
            }
        }

        public string GetLongUrlFromShort(string shortUrl, string userId)
        {
            try
            {
                ShortcutUrlData urlData = _repository.GetUrlDataByShortUrl(shortUrl);
                urlData.TransferQuantity++;
                _repository.SaveUrlData(urlData);
                return urlData.LongUrl;
            }
            catch(DataAccessException ex)
            {
                throw new GetLongUrlException(ex.Message, ex);
            }
        }
    }
}
