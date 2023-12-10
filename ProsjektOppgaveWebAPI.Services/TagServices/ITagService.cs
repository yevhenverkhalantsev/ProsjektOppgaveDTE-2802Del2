using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.Services.TagServices;

public interface ITagService
{
    Task Save(Tag tag);
}