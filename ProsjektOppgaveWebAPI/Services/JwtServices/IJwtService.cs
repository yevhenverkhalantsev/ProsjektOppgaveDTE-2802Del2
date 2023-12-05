using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.JwtServices;

public interface IJwtService
{
    Task<ResponseService<string>> GenerateToken(IdentityUser userEntity);
    Task<List<Claim>> GetClaims(IdentityUser userEntity);
}