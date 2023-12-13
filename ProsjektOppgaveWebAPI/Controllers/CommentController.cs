using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Hubs;
using ProsjektOppgaveWebAPI.Services.CommentServices;
using ProsjektOppgaveWebAPI.Services.CommentServices.Models;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IHubContext<CommentHub> _hubContext;

    public CommentController(ICommentService commentService, IHubContext<CommentHub> hubContext)
    {
        _commentService = commentService;
        _hubContext = hubContext;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Comment>> GetComments(int postId)
    {
        return await _commentService.GetCommentsForPost(postId);
    }
    
    
    [HttpGet("{id:int}")]
    public Comment GetComment([FromRoute] int id)
    {
        return _commentService.GetComment(id);
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] CreateCommentHttpPostModel vm)
    {
        var response = await _commentService.Save(vm);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMesage = response.ErrorMessage
            });
        }

        await _hubContext.Clients.All.SendAsync("CreateCommentHandler", new
        {
            CommentId = response.Value,
            Text = vm.Text,
            PostId = vm.PostId
        });
        
        return Ok(response.Value);
    }  
    
    [HttpPost]
    [Route("/TestNotify")]
    public async Task<IActionResult> TestNotify([FromBody] CreateCommentHttpPostModel vm)
    {
        CreateCommentHttpPostModel createCommentHttpPostModel = new CreateCommentHttpPostModel()
        {
            PostId = -1,
            Text = "Test"
        };
        
        await _hubContext.Clients.All.SendAsync("CreateCommentHandler", createCommentHttpPostModel);
        
        return Ok(createCommentHttpPostModel);
    }
    
    
    [Authorize]
    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] Comment comment)
    {
        // if (id != comment.CommentId)
        //     return BadRequest();
        //
        // var existingComment = _service.GetComment(id);
        // if (existingComment is null)
        //     return NotFound();
        //
        // var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // if (existingComment.OwnerId != userId)
        // {
        //     return Unauthorized();
        // }
        //
        // _service.Save(comment, User);
        //
        // return NoContent();

        return Ok();
    }
    
    
    [Authorize]
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        return Ok();
    }
}