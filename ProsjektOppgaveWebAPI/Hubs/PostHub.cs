using Microsoft.AspNetCore.SignalR;
using ProsjektOppgaveWebAPI.Models.Post;

namespace ProsjektOppgaveWebAPI.Hubs;

public class PostHub: Hub
{
    public async Task NotifyAboutCreate(PostViewModel vm)
    {
        await Clients.All.SendAsync("PostCreateNotify", vm);
    }
}