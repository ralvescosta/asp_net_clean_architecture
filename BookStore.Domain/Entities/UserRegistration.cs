namespace BookStore.Domain.Entities
{
    public class UserRegistration
    {
        public string Name { get; set; }
        public string LastName { get; set; }        
        public string Email { get; set; }        
        public Password Password { get; set; }
    }
}
