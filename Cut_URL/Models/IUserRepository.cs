using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cut_URL.Models
{
    public interface IUserRepository
    {

        public List<Url> GetUrls(int userId);
        public void AddUrl(Url Url);
        public void UpdateUrlTransitionsQuantity(Url Url);
        public void Create(User user);
        //public void Delete(int userId);
        //public List<User> GetUsers();
        //public User Get(int id);
    }
}
