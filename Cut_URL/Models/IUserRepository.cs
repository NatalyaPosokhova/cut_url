using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cut_URL.Models
{
    interface IUserRepository
    {
        public List<Url> GetUrls(int userId);
        public void AddUrl(int userId);
        public void UpdateUrlTransitionsQuantity(int userId);

    }
}
