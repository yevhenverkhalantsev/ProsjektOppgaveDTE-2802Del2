using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.EntityFramework.Configurations;

public class CommentConfiguration: IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(comment => comment.Id);
        builder.Property(comment => comment.Content).IsRequired();
        builder.Property(comment => comment.CreatedAt).IsRequired();
        
        builder.HasOne(comment => comment.Post)
            .WithMany(post => post.Comments)
            .HasForeignKey(comment => comment.PostFk)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(comment => comment.User)
            .WithMany(user => user.Comments)
            .HasForeignKey(comment => comment.UserFk)
            .OnDelete(DeleteBehavior.Cascade);
    }
}