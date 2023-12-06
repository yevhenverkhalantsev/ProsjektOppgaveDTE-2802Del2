using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Services;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly IBlogService _service;

    public TagController(IBlogService service)
    {
        _service = service;
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Tag tag)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _service.SaveTag(tag);

        return Ok();
    }
    
    
    
}