using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.EntityFramework.Configurations;

public class PostTagsConfiguration: IEntityTypeConfiguration<PostTags>
{
    public void Configure(EntityTypeBuilder<PostTags> builder)
    {
        builder.ToTable("PostTags")
            .HasKey(pt => new {pt.PostFk, pt.TagFk});
        
        builder.HasOne<Post>(x=>x.Post)
            .WithMany(x=>x.PostTags)
            .HasForeignKey(x=>x.PostFk);
        
        builder.HasOne<Tag>(x=>x.Tag)
            .WithMany(x=>x.PostTags)
            .HasForeignKey(x=>x.TagFk);
        
        
    }
}