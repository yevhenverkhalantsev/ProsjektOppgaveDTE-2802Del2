using ProsjektOppgaveWebAPI.Models.Post;

namespace ProsjektOppgaveWebAPI.Models.Tag;

public class HttpGetTagModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<PostViewModel> Posts { get; set; }
    
    public HttpGetTagModel()
    {
        Posts = new List<PostViewModel>();
    }
}