using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.Models.ViewModel;

public class PostCommentViewModel
{
    public Post Post { get; set; }
    public CommentViewModel CommentViewModel { get; set; }
}
