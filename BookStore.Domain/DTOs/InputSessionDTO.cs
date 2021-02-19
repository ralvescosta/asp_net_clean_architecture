using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.DTOs.Inputs
{
    [Display(Name = "InputSessionSchema")]
    public class InputSessionDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
