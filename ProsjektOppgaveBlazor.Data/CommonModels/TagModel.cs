namespace ProsjektOppgaveBlazor.Data.CommonModels;

public class TagModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<BlogTagRelations> BlogTags { get; set; }
}