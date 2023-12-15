using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.EntityFramework.Configurations;

public class BlogConfiguration: IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("Blog");
        builder.HasKey(x => x.BlogId);

        builder.Property(x => x.IsOpen).HasColumnName("IsOpen");
        
        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId);
    }
}