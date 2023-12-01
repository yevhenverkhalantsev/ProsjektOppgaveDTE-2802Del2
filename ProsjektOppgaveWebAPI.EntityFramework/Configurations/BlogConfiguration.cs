using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.EntityFramework.Configurations;

public class BlogConfiguration: IEntityTypeConfiguration<BlogEntity>
{
    public void Configure(EntityTypeBuilder<BlogEntity> builder)
    {
        builder.ToTable("Blogs");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).HasMaxLength(255).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
        builder.Property(b => b.IsOpen);
        
        builder.HasOne(b => b.User)
            .WithMany(u => u.Blogs)
            .HasForeignKey(b => b.UserFk);
    }
}