namespace ProsjektOppgaveWebAPI.Database.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<BlogTagRelations> BlogTags { get; set; }
}