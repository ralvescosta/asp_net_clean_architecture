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
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowBookController : ControllerBase
    {
        private readonly IBorrowBookUseCase borrowBookUseCase;
        public BorrowBookController(IBorrowBookUseCase borrowBookUseCase)
        {
            this.borrowBookUseCase = borrowBookUseCase;
        }

        [HttpPost]
        [Authorize]
        [AdminPermission]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateBorrowBookRequestDTO borrowBook)
        {
            var result = await borrowBookUseCase.BorrowABook(borrowBook);

            if (result.IsRight())
                return Ok();

            return result.GetLeft().GetType() switch
            {
                Type t when t == typeof(AlreadyExistNotification) => Conflict(result.GetLeft()),
                _ => Problem("Internal Server Error", null, 500, "Internal Server Error", "Internal Server Error"),
            };
        }

        [HttpGet("{id}")]
        [Authorize]
        [AdminPermission]
        [ProducesResponseType(typeof(UserBook), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAnAuthorId(int id)
        {
            var result = await borrowBookUseCase.GetAnBorrowedBookById(id);
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
        [ProducesResponseType(typeof(List<UserBook>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAuthors()
        {
            var author = await borrowBookUseCase.GetAllBorrowedBook();
            if (author.IsLeft())
                return Problem();

            return Ok(author.GetRight());
        }

        [HttpPut("{id}")]
        [Authorize]
        [UserPermission]
        [ProducesResponseType(typeof(UserBook), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAnAuthorById(int id)
        {
            var result = await borrowBookUseCase.UpdateABorrowedBookById(id);
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
        public async Task<IActionResult> DeleteAnAuthor(int id)
        {
            var result = await borrowBookUseCase.DeleteAnAuthorById(id);
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
