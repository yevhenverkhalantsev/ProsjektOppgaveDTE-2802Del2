using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Hubs;
using ProsjektOppgaveWebAPI.Models.Comment;
using ProsjektOppgaveWebAPI.Services.BlogServices;
using ProsjektOppgaveWebAPI.Services.PostServices;
using ProsjektOppgaveWebAPI.Services.PostServices.Models;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IBlogService _blogService;
    private readonly IPostService _postService;
    private readonly IHubContext<PostHub> _hubContext;

    public PostController(IBlogService blogService, IPostService postService, IHubContext<PostHub> hubContext)
    {
        _blogService = blogService;
        _postService = postService;
        _hubContext = hubContext;
    }

    [HttpGet]
    [Route("/posts")]
    public async Task<IEnumerable<Post>> GetPosts(int blogId)
    {
        return await _postService.GetPostsForBlog(blogId);
    }
    
    
    [HttpGet]
    [Route("{id:int}")]  
    public async Task<IActionResult> GetPost([FromRoute] int id)
    {
        return Ok(await _postService.GetPost(id));
    }
    
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] CreatePostHttpPostModel vm)
    {
        var response = await _postService.SavePost(vm);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMesage = response.ErrorMessage
            });
        }

        await _hubContext.Clients.All.SendAsync("PostCreateNotify", new
        {
            PostId = response.Value,
            Title = vm.Title,
            Content = vm.Content,
            BlogId = vm.BlogId,
            Comments = new List<CommentViewModel>()
        });
        
        return Ok(response.Value);
    }

    
    // [Authorize]
    // [HttpPut("{id:int}")]
    // public Task<IActionResult> Update([FromRoute] int id, [FromBody] Post post)
    // {
    //     // if (id != post.PostId)
    //     //     return BadRequest();
    //     //
    //     // var existingPost = await _postService.GetPost(id);
    //     // if (existingPost is null)
    //     //     return NotFound();
    //     //
    //     // var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //     // if (existingPost.Value.OwnerId != userId)
    //     // {
    //     //     return Unauthorized();
    //     // }
    //     //
    //     // _postService.SavePost(post, User);
    //     //
    //      return
    // }

    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok();
    }
}