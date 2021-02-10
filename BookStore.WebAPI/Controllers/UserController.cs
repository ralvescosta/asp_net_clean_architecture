using BookStore.Domain.DTOs;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRegisterUser registerUser;
        public UserController(IRegisterUser registerUser)
        {
            this.registerUser = registerUser;
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] InputUserRegistrationDTO input)
        {
            UserRegistrationDTO user;
            try
            {
                user = new UserRegistrationDTO()
                {
                    Name = input.Name,
                    LastName = input.LastName,
                    Email = input.Email,
                    Password = input.Password,
                };
            }
            catch (ArgumentException ex)
            {
                var response = new Dictionary<string, string>
                {
                    { "message", ex.Message }
                };
                return BadRequest(response);
            }

            try
            {
                registerUser.Register(user);
                return Ok();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
