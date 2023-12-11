namespace ProsjektOppgaveWebAPI.Models.Comment;

public class CommentViewModel
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public int PostId { get; set; }
}