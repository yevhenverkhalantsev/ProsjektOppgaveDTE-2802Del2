using Microsoft.AspNetCore.Identity;
using ProsjektOppgaveWebAPI.Data;
using ProsjektOppgaveWebAPI.Models;

namespace ProsjektOppgaveWebAPI.Services.TagServices;

public class TagService : ITagService
{
    private readonly BlogDbContext _db;
    private readonly UserManager<IdentityUser> _manager;
    
    public TagService(UserManager<IdentityUser> userManager, BlogDbContext db)
    {
        _manager = userManager;
        _db = db;
    }
    
    public async Task Save(Tag tag)
    {
        var existingTag = _db.Tag.Find(tag.Id);
        if (existingTag == null)
        {
            _db.Tag.Add(tag);
            await _db.SaveChangesAsync();
        }
    }
}