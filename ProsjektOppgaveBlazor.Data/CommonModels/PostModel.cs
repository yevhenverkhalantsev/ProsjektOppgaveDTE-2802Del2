using Microsoft.AspNetCore.Identity;

namespace ProsjektOppgaveBlazor.Data.CommonModels;

public class PostModel
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string OwnerId { get; set; }
    public IdentityUser Owner { get; set; }
    public int BlogId { get; set; }
    public BlogModel BlogModel { get; set; }
}