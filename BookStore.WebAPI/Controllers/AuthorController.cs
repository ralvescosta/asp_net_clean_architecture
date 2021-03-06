﻿using BookStore.Application.Notifications;
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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorUseCase authorUseCase;
        public AuthorController(IAuthorUseCase authorUseCase)
        {
            this.authorUseCase = authorUseCase;
        }

        [HttpPost]
        [Authorize]
        [AdminPermission]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequestDTO author)
        {
            var result = await authorUseCase.CreateAuthor(author);

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
        [ProducesResponseType(typeof(Author), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAnAuthorId(int id)
        {
            var result = await authorUseCase.GetAnAuthorById(id);
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
        [ProducesResponseType(typeof(List<Author>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAuthors()
        {
            var author = await authorUseCase.GetAllAuthors();
            if (author.IsLeft())
                return Problem();

            return Ok(author.GetRight());
        }

        [HttpPut("{id}")]
        [Authorize]
        [UserPermission]
        [ProducesResponseType(typeof(Author), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotificationBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAnAuthorById(int id, [FromBody] UpdateAuthorRequestDTO update)
        {
            var result = await authorUseCase.UpdateAnAuthorById(id, update);
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
            var result = await authorUseCase.DeleteAnAuthorById(id);
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
