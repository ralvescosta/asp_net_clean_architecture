using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.DTOs
{
    public class CreateBookRequestDTO
    {
        [Required]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 4)]
        public string Title { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 4)]
        public string Subtitle { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 4)]
        public string Subject { get; set; }
    }
}
