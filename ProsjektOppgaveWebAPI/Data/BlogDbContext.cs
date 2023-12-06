using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Models;


namespace ProsjektOppgaveWebAPI.Data;

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
        
        
        // SEEDING PREPARATION
        var hasher = new PasswordHasher<IdentityUser>();
        
        var adminUser = new IdentityUser
        {
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@example.com",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "AdminPassword123!"),
            SecurityStamp = string.Empty
        };
        
        // SEEDING
        builder.Entity<IdentityUser>().HasData(adminUser);
    }
}