using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Common;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework.Repository;
using ProsjektOppgaveWebAPI.Services.BlogServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.BlogServices;

public class BlogService : IBlogService
{
    private readonly IGenericRepository<Blog> _blogRepository;
    private readonly UserManager<IdentityUser> _manager;

    public BlogService(UserManager<IdentityUser> userManager, IGenericRepository<Blog> blogRepository)
    {
        _blogRepository = blogRepository;
        _manager = userManager;
    }
    
    public async Task<IEnumerable<Blog>> GetAllBlogs()
    {
        return await _blogRepository.GetAll()
            .Include(x=>x.Posts)
            .Include(x=>x.Owner)
            .ToListAsync();
        
    }

    public async Task<ResponseService<Blog>> GetBlog(int id)
    {
        var blog = await _blogRepository.GetAll()
            .Where(x => x.BlogId == id)
            .Include(x => x.Posts)
            .ThenInclude(x => x.Comments)
            .FirstOrDefaultAsync();
        
        if (blog == null)
        {
            return ResponseService<Blog>.Error(Errors.BLOG_NOT_FOUND_ERROR);
        }
        
        return ResponseService<Blog>.Ok(blog);
    }

    public async Task<ICollection<Blog>> GetAllBlogsByUserId(string userId)
    {
        var blogs = await _blogRepository.GetAll()
            .Where(b=>b.Owner.Id == userId)
            .ToListAsync();
        
        return blogs;
    }

    public async Task<ResponseService<long>> Save(CreateBlogHttpPostModel vm, IPrincipal principal)
    {
        try
        {
        var user = await _manager.FindByNameAsync(principal.Identity?.Name);
        if (user == null)
        {
            return ResponseService<long>.Error(Errors.USER_NOT_FOUND_ERROR);
        }

        
        var dbRecord = await _blogRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Name == vm.Title && x.OwnerId == user.Id);
        if (dbRecord != null)
        {
            return ResponseService<long>.Error(Errors.BLOG_ALREADY_EXISTS_ERROR);
        }
        
        dbRecord = new Blog()
        {
            Name = vm.Title,
            IsOpen = vm.IsOpen,
            OwnerId = user.Id
        };
        
     
            await _blogRepository.Create(dbRecord);

        
        return ResponseService<long>.Ok(dbRecord.BlogId);
        
        }
        catch (Exception e)
        {
            return ResponseService<long>.Error(Errors.CANT_CREATE_BLOG_ERROR);
        }
    }


    public async Task<ResponseService<bool>> Delete(int id)
    {
        Blog blog = await _blogRepository.GetAll()
            .FirstOrDefaultAsync(x => x.BlogId == id);

        if (blog == null)
        {
            throw new Exception(Errors.BLOG_NOT_FOUND_ERROR);
        }

        try
        {
            await _blogRepository.Delete(blog);
        }
        catch (Exception e)
        {
            throw new Exception(Errors.CANT_DELETE_BLOG_ERROR);
        }

        return ResponseService<bool>.Ok(true);
    }

    public async Task<ICollection<Blog>> Search(string searchQuery)
    {
        return await _blogRepository.GetAll()
            .Include(x => x.Posts)
            .Include(x => x.Owner)
            .Where(x => x.Name.Contains(searchQuery))
            .ToListAsync();
    }
}