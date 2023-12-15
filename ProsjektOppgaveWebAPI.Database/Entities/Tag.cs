using Microsoft.AspNetCore.Identity;

namespace ProsjektOppgaveWebAPI.Database.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public Guid UserFk { get; set; }
    public IdentityUser User { get; set; }
    
    public ICollection<PostTags> PostTags { get; set; }
}