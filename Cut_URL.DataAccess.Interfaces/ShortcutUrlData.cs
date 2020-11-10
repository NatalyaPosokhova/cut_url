using System;

namespace Cut_URL.DataAccess
{
    public class ShortcutUrlData
    {
        public DateTime Date { get; set; }
        public int TransferQuantity { get; set; }
        public string LongUrl { get; set; }
        public string shortUrl { get; set; }
        public string UserId { get; set; }
    }
}