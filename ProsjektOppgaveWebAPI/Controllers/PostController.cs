using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Services;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IBlogService _service;

    public PostController(IBlogService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IEnumerable<Post>> GetPosts(int blogId)
    {
        return await _service.GetPostsForBlog(blogId);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Post post)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var blog = _service.GetBlog(post.BlogId);
        if (blog.Status != 0) return BadRequest("This blog is closed for new posts and comments!");
        
        await _service.SavePost(post, User);
        return CreatedAtAction("GetPosts", new { id = post.BlogId }, post);
    }
    
    


}