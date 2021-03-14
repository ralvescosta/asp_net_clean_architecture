using System;

namespace BookStore.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
