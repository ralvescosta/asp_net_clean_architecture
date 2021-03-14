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
    public class AuthorController : ControllerBase
    {

        [HttpPost]
        [Authorize]
        [AdminPermission]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateAuthor(int id)
        {
            var auth = HttpContext.Items["auth"] as AuthenticatedUser;
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize]
        [AdminPermission]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAnAuthorId()
        {
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [AdminPermission]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllAuthors()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        [UserPermission]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAnAuthorById()
        {
            var auth = HttpContext.Items["auth"] as AuthenticatedUser;
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [AdminPermission]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAnAuthor()
        {
            var auth = HttpContext.Items["auth"] as AuthenticatedUser;
            return Ok();
        }
    }
}
