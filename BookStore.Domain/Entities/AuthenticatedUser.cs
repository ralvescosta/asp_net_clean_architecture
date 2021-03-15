using BookStore.Domain.Enums;

namespace BookStore.Domain.Entities
{
    public class AuthenticatedUser
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Email { get; set; }
        public Permissions Permission { get; set; }
    }
}
