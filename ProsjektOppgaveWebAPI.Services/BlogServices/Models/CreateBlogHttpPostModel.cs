namespace ProsjektOppgaveWebAPI.Services.BlogServices.Models;

public class CreateBlogHttpPostModel
{
    public string OwnerName { get; set; }
    public string Title { get; set; }
    public bool IsOpen { get; set; }
    
}