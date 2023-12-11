using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Common;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework;
using ProsjektOppgaveWebAPI.EntityFramework.Repository;
using ProsjektOppgaveWebAPI.Services.PostServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.PostServices;

public class PostService: IPostService
{
    private readonly IGenericRepository<Post> _postRepository;
    private readonly UserManager<IdentityUser> _manager;
    private readonly BlogDbContext _db;

    public PostService(IGenericRepository<Post> repository, UserManager<IdentityUser> manager, BlogDbContext db)
    {
        _postRepository = repository;
        _manager = manager;
        _db = db;
    }
    
    public async Task<ResponseService<Post>> GetPost(int id)
    {
        Post dbRecord = await _postRepository.GetAll()
            .FirstOrDefaultAsync(x=>x.PostId == id);
        if (dbRecord == null)
        {
            return ResponseService<Post>.Error(Errors.POST_NOT_FOUND_ERROR);
        }
        return ResponseService<Post>.Ok(dbRecord);
    }
    
    public async Task<IEnumerable<Post>> GetPostsForBlog(int blogId)
    {
        return await _postRepository.GetAll()
            .Where(x => x.BlogId == blogId)
            .ToListAsync();
    }
    
    public async Task<ResponseService<long>> SavePost(CreatePostHttpPostModel vm)
    {
        Post post = await _postRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Title == vm.Title 
                                      && x.BlogId == vm.BlogId);
        if (post != null)
        {
            throw new Exception(Errors.POST_ALREADY_EXISTS_ERROR);
        }
        post = new Post()
        {
            Title = vm.Title,
            Content = vm.Content,
            BlogId = vm.BlogId
        };

        try
        {
            await _postRepository.Create(post);
        }
        catch (Exception e)
        {
            throw new Exception(Errors.CANT_CREATE_POST_ERROR);
        }

        return ResponseService<long>.Ok(post.PostId);
    }

    public async Task DeletePost(int id, IPrincipal principal)
    {
        return;
        // var user = await _manager.FindByNameAsync(principal.Identity.Name);
        // var post = _db.Post.Find(id);
        //
        // if (post.Owner == user)
        // {
        //     _db.Post.Remove(post);
        //     await _db.SaveChangesAsync();
        // }
        // else
        // {
        //     throw new UnauthorizedAccessException("You are not the owner of this post.");
        // }
    }
}