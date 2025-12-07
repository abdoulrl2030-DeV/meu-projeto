using MeuProjeto.Api.DTOs;

namespace MeuProjeto.Api.Services
{
    public interface IAuthService
    {
        Task<string?> AuthenticateAsync(LoginDto login);
    }
}
