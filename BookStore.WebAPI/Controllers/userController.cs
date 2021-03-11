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

        [HttpGet]
        [Authorize]
        [AdminPermission]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers()
        {
            var auth = HttpContext.Items["auth"] as AuthenticatedUser;
            var users = await userUseCase.GetAllUsers(auth);
            return Ok(users);
        }
    }
}
