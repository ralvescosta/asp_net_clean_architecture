using BookStore.Application.Notifications;
using BookStore.Domain.DTOs.Inputs;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Shared.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [ProducesResponseType(typeof(Session), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Session([FromBody] SessionRequestDTO request)
        {
            var result = await sessionUsecase.CreateUserSession(request);
            if(result.IsRight())
                return Ok(result.GetRight());

            return result.GetLeft().GetType() switch
            {
                Type t when t == typeof(NotFoundNotification) => NotFound(result.GetLeft()),
                Type t when t == typeof(UnauthorizedNotification) => Unauthorized(result.GetLeft()),
                Type t when t == typeof(WrongPasswordNotification) => Unauthorized(result.GetLeft()),
                _ => Problem("Internal Server Error", null, 500, "Internal Server Error", "Internal Server Error"),
            };
        }
    }
}
