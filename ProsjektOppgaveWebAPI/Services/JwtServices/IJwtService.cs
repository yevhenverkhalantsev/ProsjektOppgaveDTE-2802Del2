using System.Security.Claims;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.JwtServices;

public interface IJwtService
{
    Task<ResponseService<string>> GenerateToken(IdentityUser userEntity);
    Task<List<Claim>> GetClaims(UserEntity userEntity);
}