using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProsjektOppgaveBlazor.Services.JwtServices;

public class JwtService: IJwtService
{
    public IEnumerable<Claim> Decode(string token)
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
        return jwtToken.Claims;
    }
}