using BlogProject.Models;

namespace BlogProject.Models.ViewModel;

public class PostCommentViewModel
{
    public Post Post { get; set; }
    public CommentViewModel CommentViewModel { get; set; }
}
