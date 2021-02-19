using BookStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class AuthenticatedUser
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public Email Email { get; set; }
        public Permissions Permission { get; set; }
    }
}
