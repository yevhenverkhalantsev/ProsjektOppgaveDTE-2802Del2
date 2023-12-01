using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.EntityFramework.Configurations;

public class PostTagsConfiguration: IEntityTypeConfiguration<PostTagsEntity>
{
    public void Configure(EntityTypeBuilder<PostTagsEntity> builder)
    {
        builder.ToTable("Post Tags")
            .HasKey(x=> new{x.PostFk, x.TagFk});
        
        builder.HasOne<PostEntity>(x=>x.Post)
            .WithMany(x=>x.Tags)
            .HasForeignKey(x=>x.PostFk);

        builder.HasOne<TagEntity>(x => x.Tag)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.TagFk);
    }
}