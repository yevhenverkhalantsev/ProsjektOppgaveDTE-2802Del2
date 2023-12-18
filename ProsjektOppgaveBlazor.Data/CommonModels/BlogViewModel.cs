namespace ProsjektOppgaveBlazor.Data.CommonModels;

public class BlogViewModel
{
    public string Title { get; set; }
    public bool IsOpen { get; set; }
    public List<PostViewModel> Posts { get; set; }
    
    public BlogViewModel()
    {
        Posts = new List<PostViewModel>();
    }
}