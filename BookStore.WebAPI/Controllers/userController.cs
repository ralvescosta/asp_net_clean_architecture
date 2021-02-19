using BookStore.Application.Exceptions;
using BookStore.Domain.DTOs.Inputs;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var auth = HttpContext.Items["auth"] as AuthenticatedUser;
            var users = await userUseCase.GetAllUsers(auth);
            return Ok(users);
        }
    }
}
