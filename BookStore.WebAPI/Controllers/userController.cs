using BookStore.Application.Notifications;
using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Shared.Notifications;
using BookStore.WebAPI.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserUseCase userUseCase;
        public UserController(IUserUseCase userUseCase)
        {
            this.userUseCase = userUseCase;
        }

        [HttpGet("{id}")]
        [Authorize]
        [AdminPermission]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAnUserById(int id)
        {
            var result = await userUseCase.GetAnUserById(id);
            if (result.IsRight())
                return Ok(result.GetRight());

            return result.GetLeft().GetType() switch
            {
                Type t when t == typeof(NotFoundNotification) => NotFound(),
                _ => Problem("Internal Server Error", null, 500, "Internal Server Error", "Internal Server Error"),
            };
        }

        [HttpGet]
        [Authorize]
        [AdminPermission]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userUseCase.GetAllUsers();
            if (users.IsLeft())
                return Problem();

            return Ok(users.GetRight());
        }

        [HttpPut("{id}")]
        [Authorize]
        [UserPermission]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAnUserById(int id, [FromBody] UpdateUserRequestDTO update)
        {
            var result = await userUseCase.UpdateAnUserById(id, update);
            if (result.IsRight())
                return Ok(result.GetRight());

            return result.GetLeft().GetType() switch
            {
                Type t when t == typeof(NotFoundNotification) => NotFound(),
                _ => Problem("Internal Server Error", null, 500, "Internal Server Error", "Internal Server Error"),
            };
        }

        [HttpDelete("{id}")]
        [Authorize]
        [AdminPermission]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAnUser(int id)
        {
            //var auth = HttpContext.Items["auth"] as AuthenticatedUser;
            var result = await userUseCase.DeleteAnUserById(id);
            if (result.IsRight())
                return Ok(result.GetRight());

            return result.GetLeft().GetType() switch
            {
                Type t when t == typeof(NotFoundNotification) => NotFound(),
                _ => Problem("Internal Server Error", null, 500, "Internal Server Error", "Internal Server Error"),
            };
        }
    }
}
