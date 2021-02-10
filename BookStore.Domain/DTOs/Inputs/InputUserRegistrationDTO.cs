﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.DTOs
{
    public class InputUserRegistrationDTO
    {       
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
