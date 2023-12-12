namespace ProsjektOppgaveWebAPI.Services.CommentServices.Models;

public class CreateCommentHttpPostModel
{
    public string Text { get; set; }
    
    public int PostId { get; set; }
}