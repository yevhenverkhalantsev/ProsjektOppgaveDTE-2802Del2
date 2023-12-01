namespace ProsjektOppgaveWebAPI.Database.Entities;

public class PostUsersEntity
{
    public bool IsNotified { get; set; }
    
    public long PostFk { get; set; }
    public PostEntity Post { get; set; }
    
    public long UserFk { get; set; }
    public UserEntity User { get; set; }
}