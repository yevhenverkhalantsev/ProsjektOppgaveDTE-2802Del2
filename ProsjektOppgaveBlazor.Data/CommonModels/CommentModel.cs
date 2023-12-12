using Microsoft.AspNetCore.Identity;

namespace ProsjektOppgaveBlazor.Data.CommonModels;

public class CommentModel
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public int PostId { get; set; }
}