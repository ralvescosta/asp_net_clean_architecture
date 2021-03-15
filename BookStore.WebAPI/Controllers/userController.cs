using BookStore.Domain.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Shared.Notifications;
using BookStore.WebAPI.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var user = await userUseCase.GetAnUserById(id);
            if (user.IsLeft())
                return Problem();

            return Ok(user.GetRight());
           
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
            var user = await userUseCase.UpdateAnUserById(id, update);
            if (user.IsLeft())
                return Problem();

            return Ok();
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
            var rsult = await userUseCase.DeleteAnUserById(id);
            if (rsult.IsLeft())
                return Problem();

            return Ok();
        }
    }
}
