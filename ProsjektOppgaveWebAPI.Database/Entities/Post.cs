namespace ProsjektOppgaveWebAPI.Database.Entities;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
    
    public ICollection<Comment> Comments { get; set; }

    public Post()
    {
        Comments = new List<Comment>();
    }
}