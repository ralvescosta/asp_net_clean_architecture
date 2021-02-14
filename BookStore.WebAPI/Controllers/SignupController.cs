using BookStore.Application.Exceptions;
using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IRegisterUserUseCase registerUserUseCase;
        public SignupController(IRegisterUserUseCase registerUserUseCase)
        {
            this.registerUserUseCase = registerUserUseCase;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] InputUserRegistrationDTO input)
        {
            UserRegistration user;
            try
            {
                user = new UserRegistration()
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

            return await ExecuteRegisterUserUseCase(user);
        }

        #region privateMethods
        private async Task<IActionResult> ExecuteRegisterUserUseCase(UserRegistration user)
        {
            try
            {
                await registerUserUseCase.Register(user);
                return Ok();
            }
            catch (EmailAlreadyExistException ex)
            {
                var response = new Dictionary<string, string>
                {
                    { "message", ex.Message }
                };

                return Conflict(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion
    }
}
