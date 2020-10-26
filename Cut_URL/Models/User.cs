using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cut_URL.Models
{
    public class User
    {
        private List<Url> urlsList;
        public User()
        {
            urlsList = new List<Url>();
        }
        //[Required]
        //[System.ComponentModel.DataAnnotations.RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        //public string Email { get; set; }

        //[Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        //public string Password { get; set; }
        //public int Id { get; set; }
        //public string Name { get; set; }

    }

    public struct Url
    {
        public string longUrl;
        public string shortUrl;
        public int transitionsQuantity;
        public DateTime creationDate;
        public int userId;
    }
}
