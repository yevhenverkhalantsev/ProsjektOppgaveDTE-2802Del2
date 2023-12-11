using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Models.Blog;
using ProsjektOppgaveWebAPI.Models.Comment;
using ProsjektOppgaveWebAPI.Models.Post;
using ProsjektOppgaveWebAPI.Models.ViewModel;
using ProsjektOppgaveWebAPI.Services;
using ProsjektOppgaveWebAPI.Services.BlogServices;
using ProsjektOppgaveWebAPI.Services.BlogServices.Models;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogService _service;
    
    public BlogController(IBlogService service)
    {
        _service = service;
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

    
    [Authorize]
    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        return BadRequest();
        // var blog = _service.GetBlog(id);
        // if (blog is null)
        //     return NotFound();
        //
        // var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // if (blog.OwnerId != userId)
        // {
        //     return Unauthorized();
        // }
        //
        // _service.Delete(id, User);
        //
        // return NoContent();
    }
}