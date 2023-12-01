using System.Security.Claims;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.JwtServices;

public interface IJwtService
{
    Task<ResponseService<string>> GenerateToken(UserEntity userEntity);
    Task<List<Claim>> GetClaims(UserEntity userEntity);
}