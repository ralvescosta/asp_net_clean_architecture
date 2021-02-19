using System;

namespace BookStore.Domain.Entities
{
    public class TokenData
    {
        public int  Id { get; set; }
        public string Guid { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
