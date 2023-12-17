using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.Response;
using ProsjektOppgaveWebAPI.Services.TagServices.Models;

namespace ProsjektOppgaveWebAPI.Services.TagServices;

public interface ITagService
{
    Task<ResponseService<Tag>> Create(CreateTagHttpPostModel vm);
    
    Task<ICollection<Tag>> GetAll(string userName);
    
    Task<ResponseService<bool>> DeleteTag(int tagId);
}