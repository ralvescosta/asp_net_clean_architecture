using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.DTOs
{
    public class CreateAuthorRequestDTO
    {
        [Required]
        [StringLength(80, MinimumLength = 4)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 4)]
        public string LastName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
