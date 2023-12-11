namespace ProsjektOppgaveWebAPI.Services.PostServices.Models;

public class CreatePostHttpPostModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
}