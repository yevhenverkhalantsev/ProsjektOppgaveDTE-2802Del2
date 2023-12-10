namespace ProsjektOppgaveWebAPI.Services.BlogServices.Models;

public class CreateBlogHttpPostModel
{
    public Guid OwnerId { get; set; }
    public string Title { get; set; }
    public bool IsOpen { get; set; }
    
}