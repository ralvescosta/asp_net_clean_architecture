using BookStore.Domain.DTOs;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStore.Application.Notifications;
using System;

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
        public async Task<IActionResult> CreateUser([FromBody] UserRegistrationRequestDTO request)
        {
            var result = await registerUserUseCase.Register(request);

            if(result.IsRight())
                return Ok();

            return result.GetLeft().GetType() switch
            {
                Type t when t == typeof(EmailAlreadyExistNotification) => Conflict(result.GetLeft()),
                _ => Problem(),
            };
        }
    }
}
