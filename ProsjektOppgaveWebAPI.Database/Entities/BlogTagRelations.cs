namespace ProsjektOppgaveWebAPI.Database.Entities;

public class BlogTagRelations
{
    public int BlogId { get; set; }
    public int TagId { get; set; }
    
    public Blog Blog { get; set; }
    public Tag Tag { get; set; }
}