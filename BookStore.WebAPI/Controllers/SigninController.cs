using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStore.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SigninController : ControllerBase
    {
       private readonly ILogger<SigninController> _logger;

        public SigninController(ILogger<SigninController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
