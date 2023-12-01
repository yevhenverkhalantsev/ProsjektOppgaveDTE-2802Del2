namespace ProsjektOppgaveWebAPI.Database.Entities;

public class PostTagsEntity
{
    public long PostFk { get; set; }
    public PostEntity Post { get; set; }
    
    public long TagFk { get; set; }
    public TagEntity Tag { get; set; }
}