using ProsjektOppgaveWebAPI.Models.Comment;
using ProsjektOppgaveWebAPI.Services.PostServices.Models;

namespace ProsjektOppgaveWebAPI.Models.Post;

public class PostViewModel
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    
    public int BlogId { get; set; }
    
    public List<CommentViewModel> Comments { get; set; }

    public PostViewModel()
    {
        Comments = new List<CommentViewModel>();
    }
}