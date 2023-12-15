namespace ProsjektOppgaveWebAPI.Services.CommentServices.Models;

public class UpdateCommentHttpPostModel
{
    public int CommentId { get; set; }
    public string Text { get; set; }
}