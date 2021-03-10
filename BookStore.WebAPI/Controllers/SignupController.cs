using BookStore.Domain.DTOs;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStore.Application.Notifications;

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
            else if(result.GetLeft() is EmailAlreadyExistNotification)
                return Conflict(result.GetLeft().Message);

            return Problem();
        }
    }
}
