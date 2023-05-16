using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<string>> GetSomething()
        {
            return "You are Authenticated";
        }

        [HttpGet("{id}")]
        [Authorize(Roles="admin")]
        public async Task<ActionResult<string>> GetSomething(int id)
        {
            return "You are authorized with the role of admin";
        }

    }
}
