using System;
using System.Collections.Generic;
using System.Text;

namespace Cut_URL.DataAccess
{
    public class Session
    {
        public Guid Id { get; set; }
        public DateTime LastAccessTime { get; set; }
    }
}
