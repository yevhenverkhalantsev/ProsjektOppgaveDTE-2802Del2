namespace ProsjektOppgaveWebAPI.Database.Entities;

public class PostTags
{
    public int PostFk { get; set; }
    public Post Post { get; set; }
    
    public int TagFk { get; set; }
    public Tag Tag { get; set; }
}