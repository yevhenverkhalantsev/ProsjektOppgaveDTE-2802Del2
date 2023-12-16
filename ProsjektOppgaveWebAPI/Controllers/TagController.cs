using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Models.Post;
using ProsjektOppgaveWebAPI.Models.Tag;
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
        var response = await _service.Create(vm);

        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMessage = response.ErrorMessage
            });
        }

        return Ok(new HttpGetTagModel()
        {
            Id = response.Value.Id,
            Name = response.Value.Name,
            Posts = new List<PostViewModel>()
        });
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAll(string userName)
    {
        var tags = await _service.GetAll(userName);
        
        return Ok(tags.Select(x=> new HttpGetTagModel()
        {
            Id = x.Id,
            Name = x.Name,
            Posts = new List<PostViewModel>()
        }));
    }
}