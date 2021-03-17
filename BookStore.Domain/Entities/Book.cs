using System;

namespace BookStore.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
