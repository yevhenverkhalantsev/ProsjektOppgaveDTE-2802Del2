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
    [Route("[action]")]
    public async Task<IActionResult> Update([FromBody] UpdateCommentHttpPostModel vm)
    {
        var response = await _commentService.Update(vm);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMesage = response.ErrorMessage
            });
        }
        await _hubContext.Clients.All.SendAsync("UpdateCommentNotify", new
        {
            CommentId = vm.CommentId,
            Text = vm.Text,
            PostId = response.Value.PostId
        });
        
        return Ok();
    }
    
    [HttpDelete]
    [Route("[action]/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = await _commentService.Delete(id);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMesage = response.ErrorMessage
            });
        }
        
        await _hubContext.Clients.All.SendAsync("DeleteCommentHandler", id);
        
        return Ok(response.Value);
    }
}