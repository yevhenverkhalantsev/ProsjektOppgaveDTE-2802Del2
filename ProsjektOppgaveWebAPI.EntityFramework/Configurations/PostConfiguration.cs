using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.EntityFramework.Configurations;

public class PostConfiguration: IEntityTypeConfiguration<PostEntity>
{
    public void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        builder.ToTable("Posts").HasKey(p => p.Id);
        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.Content).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();
        
        builder.HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .HasForeignKey(p => p.BlogFk)
            .OnDelete(DeleteBehavior.Cascade);
    }
}