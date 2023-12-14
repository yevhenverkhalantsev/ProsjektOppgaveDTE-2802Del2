using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Hubs;
using ProsjektOppgaveWebAPI.Models.Comment;
using ProsjektOppgaveWebAPI.Services.BlogServices;
using ProsjektOppgaveWebAPI.Services.PostServices;
using ProsjektOppgaveWebAPI.Services.PostServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

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
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Update([FromBody] UpdatePostHttpPutModel vm)
    {
        var response = await _postService.UpdatePost(vm);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMesage = response.ErrorMessage
            });
        }
        await _hubContext.Clients.All.SendAsync("UpdatePostNotify", new
        {
            PostId = vm.PostId,
            Title = vm.Title,
            Content = vm.Content,
            BlogId = response.Value.BlogId,
            Comments = response.Value.Comments
        });

        return Ok();
    }

    
    [HttpDelete]
    [Route("[action]/{postId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int postId)
    {
        var response = await _postService.DeletePost(postId);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMesage = response.ErrorMessage
            });
        }

        await _hubContext.Clients.All.SendAsync("PostDeleteNotify", postId);
        
        return Ok(response.Value);
    }
}