using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MeuProjeto.Api.DTOs;
using MeuProjeto.Api.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeuProjeto.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task<string?> AuthenticateAsync(LoginDto login)
        {
            var user = await _userRepo.GetByUsernameAsync(login.Username);
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash)) return null;

            var jwtSection = _config.GetSection("Jwt");
            var key = jwtSection.GetValue<string>("Key") ?? "change_this_secret_in_production";
            var issuer = jwtSection.GetValue<string>("Issuer") ?? "MeuProjeto";

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(jwtSection.GetValue<int>("ExpireMinutes")),
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
