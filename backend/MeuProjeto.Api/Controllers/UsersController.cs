using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MeuProjeto.Api.DTOs;
using MeuProjeto.Api.Models;
using MeuProjeto.Api.Repositories;

namespace MeuProjeto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (idClaim == null) return Unauthorized();
            if (!int.TryParse(idClaim, out var id)) return Unauthorized();

            var user = await _repo.GetByIdAsync(id);
            if (user == null) return NotFound();

            var dto = new UserDto { Id = user.Id, Username = user.Username, Role = user.Role };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] LoginDto create)
        {
            var exists = await _repo.GetByUsernameAsync(create.Username);
            if (exists != null) return BadRequest(new { message = "User already exists" });

            var user = new User
            {
                Username = create.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(create.Password),
                Role = "User"
            };
            var created = await _repo.CreateAsync(user);
            return CreatedAtAction(nameof(Me), new { id = created.Id }, new UserDto { Id = created.Id, Username = created.Username, Role = created.Role });
        }
    }
}
