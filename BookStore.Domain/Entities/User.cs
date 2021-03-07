using BookStore.Domain.Enums;
using System;

namespace BookStore.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Permissions Permission { get; set; }
    }
}
