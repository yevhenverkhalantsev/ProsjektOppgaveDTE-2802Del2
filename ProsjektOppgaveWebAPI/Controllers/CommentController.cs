using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Models.ViewModel;
using ProsjektOppgaveWebAPI.Services;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly IBlogService _service;

    public CommentController(IBlogService service)
    {
        _service = service;
    }
    
    
    [HttpGet]
    public async Task<IEnumerable<Comment>> GetComments(int postId)
    {
        return await _service.GetCommentsForPost(postId);
    }
    
    
    [HttpGet("{id:int}")]
    public CommentViewModel GetComment([FromRoute] int id)
    {
        return _service.GetCommentViewModel(id);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Comment comment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var post = _service.GetPostViewModel(comment.PostId);
        
        await _service.SaveComment(comment, User);
        return CreatedAtAction("GetComment", new { id = comment.PostId }, comment);
    }
    
    
    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] Comment comment)
    {
        if (id != comment.CommentId)
            return BadRequest();

        var existingComment = _service.GetCommentViewModel(id);
        if (existingComment is null)
            return NotFound();

        _service.SaveComment(comment, User);

        return NoContent();
    }
    
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var comment = _service.GetCommentViewModel(id);
        if (comment is null)
            return NotFound();

        _service.DeleteComment(id, User);

        return NoContent();
    }
}