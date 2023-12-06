using System.Security.Principal;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Models.ViewModel;

namespace ProsjektOppgaveWebAPI.Services.CommentServices;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetCommentsForPost(int postId);
    
    Task Save(Comment comment, IPrincipal principal);

    Task Delete(int id, IPrincipal principal);

    CommentViewModel GetCommentViewModel(int id);
}