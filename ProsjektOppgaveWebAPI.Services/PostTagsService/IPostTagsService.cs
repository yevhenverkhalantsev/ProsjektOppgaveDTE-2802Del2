using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.PostTagsService;

public interface IPostTagsService
{
    public Task<ResponseService> Create(int postId, int tagId);
    public Task<ResponseService> Delete(int postId, int tagId);
}