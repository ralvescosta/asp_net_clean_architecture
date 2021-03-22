using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.DTOs
{
    public class CreateBorrowBookRequestDTO
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime ExpiredAt { get; set; }
    }
}
