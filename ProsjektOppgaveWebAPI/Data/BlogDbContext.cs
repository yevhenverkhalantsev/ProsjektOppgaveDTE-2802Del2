using BlogProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Data;

public class BlogDbContext : IdentityDbContext<IdentityUser>
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    {
    }

    public DbSet<Blog>? Blog { get; set; }
    public DbSet<Post>? Post { get; set; }
    public DbSet<Comment>? Comment { get; set; }
    public DbSet<IdentityUser>? User { get; set; }
    public DbSet<BlogTagRelations>? BlogTagRelations { get; set; }
    public DbSet<Tag>? Tag { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<BlogTagRelations>()
            .HasKey(bt => new { bt.BlogId, bt.TagId }); // composite key

        builder.Entity<BlogTagRelations>()
            .HasOne(bt => bt.Blog)
            .WithMany(b => b.BlogTags)
            .HasForeignKey(bt => bt.BlogId);

        builder.Entity<BlogTagRelations>()
            .HasOne(bt => bt.Tag)
            .WithMany(t => t.BlogTags)
            .HasForeignKey(bt => bt.TagId);
    }
}