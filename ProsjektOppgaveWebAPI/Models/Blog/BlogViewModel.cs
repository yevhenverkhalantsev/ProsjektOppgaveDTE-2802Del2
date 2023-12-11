using ProsjektOppgaveWebAPI.Models.Post;

namespace ProsjektOppgaveWebAPI.Models.Blog;

public class BlogViewModel
{
    public string Title { get; set; }
    public List<PostViewModel> Posts { get; set; }
    
    public BlogViewModel()
    {
        Posts = new List<PostViewModel>();
    }
}