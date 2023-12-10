using Microsoft.AspNetCore.Identity;

namespace ProsjektOppgaveBlazor.Data.CommonModels;

public class CommentModel
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public string OwnerId { get; set; }
    public IdentityUser Owner { get; set; }
    public int PostId { get; set; }
    public PostModel PostModel { get; set; }
}