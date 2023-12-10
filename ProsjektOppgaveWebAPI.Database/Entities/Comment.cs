using Microsoft.AspNetCore.Identity;

namespace ProsjektOppgaveWebAPI.Database.Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public string OwnerId { get; set; }
    public IdentityUser Owner { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
}