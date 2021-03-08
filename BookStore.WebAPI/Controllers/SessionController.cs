using BookStore.Application.Exceptions;
using BookStore.Domain.DTOs.Inputs;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<IActionResult> Session([FromBody] SessionRequestDTO request)
        {
            try
            {
                var result = await sessionUsecase.CreateUserSession(request);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                var response = new Dictionary<string, string>
                {
                    { "message", "Email Not Found" }
                };
                return NotFound(response);
            }
            catch
            {
                return Problem("Internal Server Error", null, 500, "Internal Server Error", "Internal Server Error");
            }
        }
    }
}
