using System;
using System.ComponentModel.DataAnnotations;

namespace Cut_URL.DataAccess
{
    public class ShortcutUrlData
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TransferQuantity { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public string UserId { get; set; }
    }
}