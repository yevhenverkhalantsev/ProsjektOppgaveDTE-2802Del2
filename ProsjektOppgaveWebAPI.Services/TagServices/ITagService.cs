using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.Response;
using ProsjektOppgaveWebAPI.Services.TagServices.Models;

namespace ProsjektOppgaveWebAPI.Services.TagServices;

public interface ITagService
{
    Task<ResponseService<long>> Create(CreateTagHttpPostModel vm);
    
    
}