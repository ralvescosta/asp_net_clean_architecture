using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.DTOs
{
    public class UpdateBookRequestDTO
    {
        [StringLength(80, MinimumLength = 4)]
        public string Title { get; set; }

        [StringLength(80, MinimumLength = 4)]
        public string Subtitle { get; set; }

        [StringLength(80, MinimumLength = 4)]
        public string Subject { get; set; }
    }
}
