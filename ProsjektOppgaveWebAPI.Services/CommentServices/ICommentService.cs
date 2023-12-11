using System.Security.Principal;
using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.Services.CommentServices;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetCommentsForPost(int postId);

    Comment? GetComment(int id);
    
    Task Save(Comment comment, IPrincipal principal);

    Task Delete(int id, IPrincipal principal);
}