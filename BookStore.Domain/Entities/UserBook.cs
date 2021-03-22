using System;

namespace BookStore.Domain.Entities
{
    public class UserBook
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime ExpiredAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }
    }
}
