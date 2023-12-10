using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Services;
using ProsjektOppgaveWebAPI.Services.BlogServices;
using ProsjektOppgaveWebAPI.Services.PostServices;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IBlogService _blogService;
    private readonly IPostService _postService;

    public PostController(IBlogService blogService, IPostService postService)
    {
        _blogService = blogService;
        _postService = postService;
    }

    [HttpGet]
    public async Task<IEnumerable<Post>> GetPosts(int blogId)
    {
        return await _postService.GetPostsForBlog(blogId);
    }
    
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPost([FromRoute] int id)
    {
        return Ok(await _postService.GetPost(id));
    }
    
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Post post)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var blog = _blogService.GetBlog(post.BlogId);
       
        
        await _postService.SavePost(post, User);
        return CreatedAtAction("GetPosts", new { id = post.BlogId }, post);
    }

    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Post post)
    {
        if (id != post.PostId)
            return BadRequest();

        var existingPost = await _postService.GetPost(id);
        if (existingPost is null)
            return NotFound();
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (existingPost.Value.OwnerId != userId)
        {
            return Unauthorized();
        }
        
        _postService.SavePost(post, User);

        return NoContent();
    }

    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var post = await _postService.GetPost(id);
        if (post is null)
            return NotFound();
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (post.Value.OwnerId != userId)
        {
            return Unauthorized();
        }

        _postService.DeletePost(id, User);

        return NoContent();
    }
}