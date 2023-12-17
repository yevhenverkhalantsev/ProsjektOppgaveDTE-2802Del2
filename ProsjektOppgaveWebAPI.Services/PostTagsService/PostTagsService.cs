using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Common;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework.Repository;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.PostTagsService;

public class PostTagsService: IPostTagsService
{
    private readonly IGenericRepository<PostTags> _postTagsRepository;

    public PostTagsService(IGenericRepository<PostTags> postTagsRepository)
    {
        _postTagsRepository = postTagsRepository;
    }
    
    public async Task<ResponseService> Create(int postId, int tagId)
    {
        PostTags dbRecord = await _postTagsRepository.GetAll()
            .FirstOrDefaultAsync(x => x.PostFk == postId && x.TagFk == tagId);

        if (dbRecord!=null)
        {
            return ResponseService.Error(Errors.TAG_ALREADY_CONNECTED_TO_POST_ERROR);
        }

        dbRecord = new PostTags()
        {
            PostFk = postId,
            TagFk = tagId
        };

        try
        {
            await _postTagsRepository.Create(dbRecord);
        }
        catch (Exception e)
        {
            return ResponseService.Error(Errors.CANT_CONNECT_TAG_TO_POST_ERROR);
        }
        
        return ResponseService.Ok();
    }

    public async Task<ResponseService> Delete(int postId, int tagId)
    {
        PostTags dbRecord = _postTagsRepository.GetAll()
            .FirstOrDefault(x => x.PostFk == postId && x.TagFk == tagId);

        if (dbRecord == null)
        {
            return ResponseService.Error(Errors.TAG_NOT_FOUND_ERROR);
        }
        
        try
        {
            await _postTagsRepository.Delete(dbRecord);
        }
        catch (Exception e)
        {
            return ResponseService.Error(Errors.CANT_DELETE_TAG_ERROR);
        }
        
        return ResponseService.Ok();
    }
}