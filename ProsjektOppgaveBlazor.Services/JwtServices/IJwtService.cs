using System.Security.Claims;

namespace ProsjektOppgaveBlazor.Services.JwtServices;

public interface IJwtService
{
    IEnumerable<Claim> Decode(string token);
}