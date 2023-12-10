using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Models;
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
    public async Task<IEnumerable<Blog>> GetAll()
    {
        return await _service.GetAllBlogs();
    }


    [HttpGet]
    [Route("{id:int}")]
    public IActionResult Get(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var blog = _service.GetBlog(id);
        if (blog == null)
        {
            return NotFound();
        }
        return Ok(blog);
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