using Microsoft.AspNetCore.Mvc;
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
    
    
    
    
}