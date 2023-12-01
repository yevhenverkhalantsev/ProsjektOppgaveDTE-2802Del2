using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.EntityFramework.Configurations;

public class TagConfiguration: IEntityTypeConfiguration<TagEntity>
{
    public void Configure(EntityTypeBuilder<TagEntity> builder)
    {
        builder.ToTable("Tags")
            .HasKey(x => x.Id);
        
        builder.HasOne<UserEntity>(x => x.User)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.UserFk);
    }
}