namespace ProsjektOppgaveBlazor.Data.CommonModels;

public class TagModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<PostViewModel> Posts { get; set; }

    public TagModel()
    {
        Posts = new List<PostViewModel>();
    }
}