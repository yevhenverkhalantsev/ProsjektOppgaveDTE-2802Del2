namespace ProsjektOppgaveWebAPI.Database.Entities;

public class CommentEntity
{
    public long Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public long UserFk { get; set; }
    public UserEntity User { get; set; }
    
    public long PostFk { get; set; }
    public PostEntity Post { get; set; }
}