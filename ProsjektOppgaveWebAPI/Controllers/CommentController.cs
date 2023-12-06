using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Models.ViewModel;
using ProsjektOppgaveWebAPI.Services.CommentServices;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _service;

    public CommentController(ICommentService service)
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
        
        await _service.Save(comment, User);
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

        _service.Save(comment, User);

        return NoContent();
    }
    
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var comment = _service.GetCommentViewModel(id);
        if (comment is null)
            return NotFound();

        _service.Delete(id, User);

        return NoContent();
    }
}