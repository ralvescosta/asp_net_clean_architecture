using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.DTOs
{
    public class UpdateUserRequestDTO
    {
        [StringLength(80, MinimumLength = 4)]
        public string Name { get; set; }

        [StringLength(80, MinimumLength = 4)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
