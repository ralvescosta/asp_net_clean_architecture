using BookStore.Application.Exceptions;
using BookStore.Domain.DTOs.Inputs;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionUseCase sessionUsecase;
        public SessionController(ISessionUseCase sessionUsecase)
        {
            this.sessionUsecase = sessionUsecase;
        }

        [HttpPost]
        public async Task<IActionResult> Session([FromBody] InputSessionDTO input)
        {
            UserCredentials credentials;
            try
            {
                credentials = new UserCredentials()
                {
                    Email = input.Email,
                    Password = input.Password,
                };
            }
            catch
            {
                var response = new Dictionary<string, string>
                {
                    { "message", "Wrong Body" }
                };
                return BadRequest(response);
            }

            return await ExecuteSession(credentials);
        }

        #region privateMethod
        private async Task<IActionResult> ExecuteSession(UserCredentials credentials) 
        { 
            try
            {
                var result = await sessionUsecase.CreateUserSession(credentials);
                return Ok(result);
            }
            catch (NotFoundException) 
            {
                var response = new Dictionary<string, string>
                {
                    { "message", "Email Not Found" }
                };
                return NotFound(response);
            }
            catch
            {
                return Problem("Internal Server Error", null, 500, "Internal Server Error", "Internal Server Error");
            }
        }
        #endregion
    }
}
