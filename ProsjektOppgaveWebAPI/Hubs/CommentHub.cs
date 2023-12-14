using Microsoft.AspNetCore.SignalR;
using ProsjektOppgaveWebAPI.Services.CommentServices.Models;

namespace ProsjektOppgaveWebAPI.Hubs;

public class CommentHub: Hub
{
    public async Task NotifyAboutCreate(CreateCommentHttpPostModel vm)
    {
        await Clients.All.SendAsync("CreateCommentHandler", vm);
    }
    
    public async Task NotifyAboutDelete(int id)
    {
        await Clients.All.SendAsync("DeleteCommentHandler", id);
    }
}