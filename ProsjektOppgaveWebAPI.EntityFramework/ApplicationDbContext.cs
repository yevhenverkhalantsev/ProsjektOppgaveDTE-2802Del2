using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework.Configurations;

namespace ProsjektOppgaveWebAPI.EntityFramework;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<BlogEntity> Blogs { get; set; }
    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<PostTagsEntity> PostTags { get; set; }
    public DbSet<PostUsersEntity> PostUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new BlogConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new PostTagsConfiguration());
        modelBuilder.ApplyConfiguration(new PostUsersConfiguration());
    }
}