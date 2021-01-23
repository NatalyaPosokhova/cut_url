using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cut_URL.DataAccess
{
    public class ShortcutUrlData : IEquatable<ShortcutUrlData>
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TransferQuantity { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public string UserId { get; set; }

        public bool Equals(ShortcutUrlData anotherUrlData)
        {
            if (anotherUrlData == null)
                return false;

            return this.TransferQuantity.Equals(anotherUrlData.TransferQuantity) &&
                (
                    object.ReferenceEquals(this.LongUrl, anotherUrlData.LongUrl) ||
                    this.LongUrl != null &&
                    this.LongUrl.Equals(anotherUrlData.LongUrl)
                ) &&
                (
                    object.ReferenceEquals(this.ShortUrl, anotherUrlData.ShortUrl) ||
                    this.ShortUrl != null &&
                    this.ShortUrl.Equals(anotherUrlData.ShortUrl)
                ) &&
                (
                    object.ReferenceEquals(this.UserId, anotherUrlData.UserId) ||
                    this.UserId != null &&
                    this.UserId.Equals(anotherUrlData.UserId)
                );
        }

        //public bool Equals([AllowNull] ShortcutUrlData obj)
        //{
        //    return this.Equals(obj);
        //}
    }
}