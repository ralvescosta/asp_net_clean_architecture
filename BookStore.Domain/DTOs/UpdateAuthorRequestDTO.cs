using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.DTOs
{
    public class UpdateAuthorRequestDTO
    {
        [StringLength(80, MinimumLength = 4)]
        public string FirstName { get; set; }

        [StringLength(80, MinimumLength = 4)]
        public string LastName { get; set; }

        public string Description { get; set; }
    }
}
