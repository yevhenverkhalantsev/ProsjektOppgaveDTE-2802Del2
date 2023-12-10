using Microsoft.AspNetCore.Identity;

namespace ProsjektOppgaveWebAPI.Database.Entities;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string OwnerId { get; set; }
    public IdentityUser Owner { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}