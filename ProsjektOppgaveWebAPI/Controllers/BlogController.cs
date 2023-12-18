using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Hubs;
using ProsjektOppgaveWebAPI.Models.Blog;
using ProsjektOppgaveWebAPI.Models.Comment;
using ProsjektOppgaveWebAPI.Models.Post;
using ProsjektOppgaveWebAPI.Models.Tag;
using ProsjektOppgaveWebAPI.Services.BlogServices;
using ProsjektOppgaveWebAPI.Services.BlogServices.Models;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogService _service;
    private readonly IHubContext<BlogHub> _hubContext;
    
    public BlogController(IBlogService service, IHubContext<BlogHub> hubContext)
    {
        _service = service;
        _hubContext = hubContext;
    }


    [HttpGet]
    [Route("/blogs")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(JsonConvert.SerializeObject(await _service.GetAllBlogs(), new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
    }


    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.GetBlog(id);
        if (response.IsError)
        {
            return NotFound();
        }

        BlogViewModel vm = new BlogViewModel()
        {
            IsOpen = response.Value.IsOpen,
            Title = response.Value.Name,
            Posts = response.Value.Posts.Select(x => new PostViewModel()
            {
                PostId = x.PostId,
                Title = x.Title,
                Content = x.Content,
                BlogId = x.BlogId,
                Comments = x.Comments.Select(c => new CommentViewModel()
                {
                    CommentId = c.CommentId,
                    Text = c.Text,
                    PostId = c.PostId
                }).ToList(),
                Tags = x.PostTags.Select(t => new TagViewModel()
                {
                    Id = t.Tag.Id,
                    Name = t.Tag.Name,
                    PostId = t.PostFk
                }).ToList()

            }).ToList()
        };
        
        return Ok(vm);
    }

    
    [HttpGet]
    [Route("[action]/{userId}")]
    public async Task<IActionResult> GetAllByUserId([FromRoute] string userId)
    {
        return Ok(await _service.GetAllBlogsByUserId(userId));
    }

    [Authorize]
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] CreateBlogHttpPostModel vm)
    {
       var response = await _service.Save(vm, User);
       if (response.IsError)
       {
           return BadRequest(new { responseMessage = response.ErrorMessage });
       }

       await _hubContext.Clients.All.SendAsync("CreateBlogHandler", JsonConvert.SerializeObject(response.Value,
           new JsonSerializerSettings()
           {
               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
           }));

       return Ok();
    }


    [Authorize]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Blog blog)
    {
        return BadRequest();
        // if (id != blog.BlogId)
        //     return BadRequest();
        //
        // var existingBlog = _service.GetBlog(id);
        //
        // if (existingBlog is null)
        //     return NotFound();
        //
        // var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // if (existingBlog.OwnerId != userId)
        // {
        //     return Unauthorized();
        // }
        //
        // await _service.Save(blog, User);
        //
        // return NoContent();
    }
    
    [HttpDelete]
    [Route("[action]/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = await _service.Delete(id);
        if (response.IsError)
        {
            return BadRequest(new
            {
                responseMesage = response.ErrorMessage
            });
        }
        
        await _hubContext.Clients.All.SendAsync("DeleteBlogHandler", id);
        
        return Ok(response.Value);
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Search(string searchQuery)
    {
        return Ok(JsonConvert.SerializeObject(await _service.Search(searchQuery), new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
    }
}