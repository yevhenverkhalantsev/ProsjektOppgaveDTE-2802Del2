using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework.Configurations;

namespace ProsjektOppgaveWebAPI.EntityFramework;

public class BlogDbContext: DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }

    public DbSet<Blog>? Blog { get; set; }
    public DbSet<Post>? Post { get; set; }
    public DbSet<Comment>? Comment { get; set; }
    public DbSet<IdentityUser>? AspNetUsers { get; set; }
    public DbSet<Tag>? Tag { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new BlogConfiguration());
        
        builder.Entity<BlogTagRelations>()
            .HasKey(bt => new { bt.BlogId, bt.TagId });
        
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
        builder.Entity<IdentityUser>().ToTable("AspNetUsers");
        builder.Entity<IdentityUser>().HasData(adminUser);
    }
}