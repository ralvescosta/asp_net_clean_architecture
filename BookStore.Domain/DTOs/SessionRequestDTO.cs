using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.DTOs.Inputs
{
    [Display(Name = "SessionRequestSchema")]
    public class SessionRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
