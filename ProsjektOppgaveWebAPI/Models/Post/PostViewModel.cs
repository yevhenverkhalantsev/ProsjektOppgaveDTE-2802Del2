using ProsjektOppgaveWebAPI.Models.Comment;
using ProsjektOppgaveWebAPI.Models.Tag;
using ProsjektOppgaveWebAPI.Services.PostServices.Models;

namespace ProsjektOppgaveWebAPI.Models.Post;

public class PostViewModel
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    
    public int BlogId { get; set; }
    
    public List<CommentViewModel> Comments { get; set; }
    
    public List<TagViewModel> Tags { get; set; }

    public PostViewModel()
    {
        Comments = new List<CommentViewModel>();
        Tags = new List<TagViewModel>();
    }
}