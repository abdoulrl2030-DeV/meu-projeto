using Microsoft.AspNetCore.Mvc;
using MeuProjeto.Api.DTOs;
using MeuProjeto.Api.Services;

namespace MeuProjeto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var token = await _auth.AuthenticateAsync(login);
            if (token == null) return Unauthorized(new { message = "Invalid credentials" });
            return Ok(new { token });
        }
    }
}
