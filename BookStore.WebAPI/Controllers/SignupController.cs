using BookStore.Domain.DTOs;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStore.Application.Notifications;
using System;
using Microsoft.AspNetCore.Http;
using BookStore.Shared.Notifications;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] SignUpRequestDTO request)
        {
            var result = await registerUserUseCase.Register(request);

            if (result.IsRight())
                return Ok();

            return result.GetLeft().GetType() switch
            {
                Type t when t == typeof(AlreadyExistNotification) => Conflict(result.GetLeft()),
                _ => Problem("Internal Server Error", null, 500, "Internal Server Error", "Internal Server Error"),
            };
        }
    }
}
