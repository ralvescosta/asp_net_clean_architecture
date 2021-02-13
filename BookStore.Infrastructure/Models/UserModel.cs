using BookStore.Domain.Entities;
using System;

namespace BookStore.Infrastructure.Models
{
    public class UserModel : User
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
