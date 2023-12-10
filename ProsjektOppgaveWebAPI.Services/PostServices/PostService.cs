using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Common;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework.Repository;
using ProsjektOppgaveWebAPI.Models.ViewModel;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.PostServices;

public class PostService: IPostService
{
    private readonly IGenericRepository<Post> _postRepository;

    public PostService(IGenericRepository<Post> repository)
    {
        _postRepository = repository;
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
    
    public async Task SavePost(Post post, IPrincipal principal)
    {
        return;
        // var user = await _manager.FindByNameAsync(principal.Identity.Name);
        //
        // var existingPost = _db.Post.Find(post.PostId);
        // if (existingPost != null)
        // {
        //     if (existingPost.Owner != user)
        //     {
        //         throw new UnauthorizedAccessException("You are not the owner of this post.");
        //     }
        //     _db.Entry(existingPost).State = EntityState.Detached;
        // }
        //
        // post.Owner = user;
        // _db.Post.Update(post);
        // await _db.SaveChangesAsync();
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
    
    public PostViewModel GetPostViewModel()
    {
        return null;
        // _postViewModel = new PostViewModel();
        // return _postViewModel;
    }

    public PostViewModel GetPostViewModel(int id)
    {
        return null;
        // var post = _db.Post.Find(id);
        // if (post == null) return null;
        //
        // _postViewModel = new PostViewModel
        // {
        //     PostId = post.PostId,
        //     Title = post.Title,
        //     Content = post.Content,
        //     BlogId = post.BlogId
        // };
        // return _postViewModel;
    }
}