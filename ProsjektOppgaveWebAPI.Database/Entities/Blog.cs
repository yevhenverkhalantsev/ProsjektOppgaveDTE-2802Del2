using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProsjektOppgaveWebAPI.Database.Entities;

public class Blog
{
    public int BlogId { get; set; }
    public string Name { get; set; }
    
    [Column( "IsOpen")]
    public bool IsOpen { get; set; }
    
    public string OwnerId { get; set; }
    public IdentityUser Owner { get; set; }
    
    public List<Post> Posts { get; set; }

    public Blog()
    {
        Posts = new List<Post>();
    }
}