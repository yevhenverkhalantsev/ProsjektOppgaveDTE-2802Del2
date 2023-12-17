using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Common;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework;
using ProsjektOppgaveWebAPI.EntityFramework.Repository;
using ProsjektOppgaveWebAPI.Services.PostServices.Models;
using ProsjektOppgaveWebAPI.Services.PostTagsService;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.PostServices;

public class PostService: IPostService
{
    private readonly IGenericRepository<Post> _postRepository;
    private readonly IPostTagsService _postTagsService;

    public PostService(IGenericRepository<Post> repository, IPostTagsService postTagsService)
    {
        _postRepository = repository;
        _postTagsService = postTagsService;
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
    
    public async Task<ResponseService<int>> SavePost(CreatePostHttpPostModel vm)
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

        foreach (int id in vm.TagIds)
        {
            var response = await _postTagsService.Create(post.PostId, id);

            if (response.IsError)
            {
                return ResponseService<int>.Error(response.ErrorMessage);
            }
        }
        
        return ResponseService<int>.Ok(post.PostId);
    }

    public async Task<ResponseService<bool>> DeletePost(int postId)
    {
        Post post = await _postRepository.GetAll()
            .FirstOrDefaultAsync(x => x.PostId == postId);
    
        if (post == null)
        {
            throw new Exception(Errors.POST_NOT_FOUND_ERROR);
        }

        try
        {
            await _postRepository.Delete(post);
        }
        catch (Exception e)
        {
            throw new Exception(Errors.CANT_DELETE_POST_ERROR, e);
        }

        return ResponseService<bool>.Ok(true);
    }

    public async Task<ResponseService<Post>> UpdatePost(UpdatePostHttpPutModel vm)
    {
        Post post = await _postRepository.GetAll()
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.PostId == vm.PostId);
        
        if (post == null)
        {
            return ResponseService<Post>.Error(Errors.POST_NOT_FOUND_ERROR);
        }
        
        post.Title = vm.Title;
        post.Content = vm.Content;
        
        try
        {
            await _postRepository.Update(post);
        }
        catch (Exception e)
        {
            return ResponseService<Post>.Error(Errors.CANT_UPDATE_POST_ERROR);
        }
        
        return ResponseService<Post>.Ok(post);
    }
}