namespace ProsjektOppgaveWebAPI.Services.PostServices.Models;

public class UpdatePostHttpPutModel
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}