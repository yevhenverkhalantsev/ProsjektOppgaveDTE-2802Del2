using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.EntityFramework.Configurations;

public class PostUsersConfiguration: IEntityTypeConfiguration<PostUsersEntity>
{
    public void Configure(EntityTypeBuilder<PostUsersEntity> builder)
    {
        builder.ToTable("Post Users")
            .HasKey(x => new { x.PostFk, x.UserFk });
        
        builder.HasOne<PostEntity>(x=>x.Post)
            .WithMany(x=>x.Users)
            .HasForeignKey(x=>x.PostFk);
        
        builder.HasOne<UserEntity>(x=>x.User)
            .WithMany(x=>x.Posts)
            .HasForeignKey(x=>x.UserFk);
    }
}