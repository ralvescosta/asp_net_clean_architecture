using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.DTOs
{
    public class UserRegistrationDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }        
        public Email Email { get; set; }        
        public Password Password { get; set; }
    }
}
