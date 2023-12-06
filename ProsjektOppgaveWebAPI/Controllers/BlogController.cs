using Microsoft.AspNetCore.Mvc;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Services;

namespace ProsjektOppgaveWebAPI.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogService _service;
    
    public BlogController(IBlogService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IEnumerable<Blog>> GetAll()
    {
        return await _service.GetAllBlogs();
    }


    [HttpGet("{id:int}")]
    public IActionResult Get([FromRoute] int id)
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


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Blog blog)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _service.Save(blog, User);

        return CreatedAtAction("Get", new { id = blog.BlogId }, blog);
    }

    
    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] Blog blog)
    {
        if (id != blog.BlogId)
            return BadRequest();

        var existingBlog = _service.GetBlog(id);
        if (existingBlog is null)
            return NotFound();
        
        _service.Save(blog, User);

        return NoContent();
    }

    
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var blog = _service.GetBlog(id);
        if (blog is null)
            return NotFound();

        _service.Delete(id, User);

        return NoContent();
    }
}