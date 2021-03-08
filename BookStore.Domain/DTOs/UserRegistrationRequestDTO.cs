using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.DTOs
{
    [Display(Name = "UserRegistrationRequestSchema")]
    public class UserRegistrationRequestDTO
    {
        [Required]
        [StringLength(80, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
