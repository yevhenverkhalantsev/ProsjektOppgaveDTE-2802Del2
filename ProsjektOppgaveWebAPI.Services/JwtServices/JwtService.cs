using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProsjektOppgaveWebAPI.Common;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.JwtServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.JwtServices;

public class JwtService: IJwtService
{
    private readonly JwtOptions _jwtOptions;

    public JwtService(IOptionsMonitor<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.CurrentValue;
    }
    
    public async Task<ResponseService<string>> GenerateToken(UserEntity userEntity)
    {
        byte[] byteKey = Encoding.UTF8.GetBytes(_jwtOptions.Key);
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(byteKey);
        
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        List<Claim> claims = await GetClaims(userEntity);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(_jwtOptions.ExpirationHours),
            signingCredentials: credentials);
        
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        
        try
        {
            return ResponseService<string>.Ok(tokenHandler.WriteToken(jwtToken));
        }
        catch (Exception e)
        {
            return ResponseService<string>.Error(Errors.GENERATE_JWT_TOKEN_ERROR);
        }
    }

    public async Task<List<Claim>> GetClaims(UserEntity userEntity)
    {
        return new List<Claim>()
        {
            new Claim("Id", userEntity.Id.ToString()),
            new Claim("Username", userEntity.Username),
        };
    }
}