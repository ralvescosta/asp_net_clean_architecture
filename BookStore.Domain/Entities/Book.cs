namespace BookStore.Domain.Entities
{
    public class Book
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
