namespace ProsjektOppgaveBlazor.Data.CommonModels;

public class PostViewModel
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    
    public int BlogId { get; set; }
    
    public List<CommentViewModel> Comments { get; set; }
    
    public List<TagModel> Tags { get; set; }

    public PostViewModel()
    {
        Comments = new List<CommentViewModel>();
        Tags = new List<TagModel>();
    }
}