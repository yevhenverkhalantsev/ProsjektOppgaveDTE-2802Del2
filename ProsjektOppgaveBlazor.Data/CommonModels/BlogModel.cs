using Microsoft.AspNetCore.Identity;

namespace ProsjektOppgaveBlazor.Data.CommonModels;

public class BlogModel
{
    public int BlogId { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public IdentityUser Owner { get; set; }
    public List<PostModel> Posts { get; set; }
    public bool IsOpen { get; set; }
    public ICollection<BlogTagRelations> BlogTags { get; set; }
}