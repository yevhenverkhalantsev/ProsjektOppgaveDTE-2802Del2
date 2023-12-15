using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Services.Response;
using ProsjektOppgaveWebAPI.Services.TagServices;
using ProsjektOppgaveWebAPI.Services.TagServices.Models;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagService _service;

    public TagController(ITagService service)
    {
        _service = service;
    }


    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] CreateTagHttpPostModel vm)
    {
        ResponseService<long> response = await _service.Create(vm);

        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }

        return Ok(response.Value);
    }
}