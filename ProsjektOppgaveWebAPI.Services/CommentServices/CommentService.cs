using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework;

namespace ProsjektOppgaveWebAPI.Services.CommentServices;

public class CommentService : ICommentService
{
    private readonly BlogDbContext _db;
    private readonly UserManager<IdentityUser> _manager;
    
    public CommentService(UserManager<IdentityUser> userManager, BlogDbContext db)
    {
        _manager = userManager;
        _db = db;
    }
    
    public async Task<IEnumerable<Comment>> GetCommentsForPost(int postId)
    {
        return null;
    }

    public Comment? GetComment(int id)
    {
        var c = (from comment in _db.Comment
                where comment.CommentId == id
                select comment)
            .FirstOrDefault();
        return c;
    }
    
    public async Task Save(Comment comment, IPrincipal principal)
    {
        return;
    }
    
    public async Task Delete(int id, IPrincipal principal)
    {
    }
    
}