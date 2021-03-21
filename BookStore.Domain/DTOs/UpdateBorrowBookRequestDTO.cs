using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.DTOs
{
    public class UpdateBorrowBookRequestDTO
    {
        [Required]
        public DateTime ExpiredAt { get; set; }
    }
}
