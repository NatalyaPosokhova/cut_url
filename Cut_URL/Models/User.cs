using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cut_URL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
