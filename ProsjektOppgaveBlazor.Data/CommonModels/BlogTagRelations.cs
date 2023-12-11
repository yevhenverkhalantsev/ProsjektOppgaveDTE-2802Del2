namespace ProsjektOppgaveBlazor.Data.CommonModels;

public class BlogTagRelations
{
    public int BlogId { get; set; }
    public int TagId { get; set; }
    
    public BlogViewModel Blog { get; set; }
    public TagModel Tag { get; set; }
}