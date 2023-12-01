namespace ProsjektOppgaveWebAPI.Database.Entities;

public class TagEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string ColorRgb { get; set; }
    
    public long UserFk { get; set; }
    public UserEntity User { get; set; }
    
    public ICollection<PostTagsEntity> Posts { get; set; }
}