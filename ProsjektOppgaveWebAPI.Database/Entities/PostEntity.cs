using System.Collections.ObjectModel;

namespace ProsjektOppgaveWebAPI.Database.Entities;

public class PostEntity
{
    public PostEntity()
    {
        Comments = new Collection<CommentEntity>();
    }
    
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public long BlogFk { get; set; }
    public BlogEntity Blog { get; set; }
    
    public ICollection<CommentEntity> Comments { get; set; }
    public ICollection<PostTagsEntity> Tags { get; set; }
    public ICollection<PostUsersEntity> Users { get; set; }
}