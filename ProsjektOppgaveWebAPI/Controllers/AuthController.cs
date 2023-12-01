using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.JwtServices;

namespace ProsjektOppgaveWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;

    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }
    
    [HttpPost]
    [Route("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] UserEntity vm)
    {
        var response = await _jwtService.GenerateToken(vm);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
            
        }
        return Ok(new
        {
            success = true,
            token = response.Value
        });
    }
    
    [HttpGet]
    [Route ("/CheckToken")]
    [Authorize]
    public async Task<IActionResult> CheckToken()
    {
        return Ok(new
        {
            success = true
        });
    }
}
