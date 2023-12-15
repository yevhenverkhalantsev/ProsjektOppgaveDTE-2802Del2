using System.Security.Principal;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.CommentServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.CommentServices;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetCommentsForPost(int postId);

    Comment? GetComment(int id);
    
    Task<ResponseService<int>> Save(CreateCommentHttpPostModel vm);
    
    Task<ResponseService<bool>> Delete(int id);
    
    Task<ResponseService<Comment>> Update(UpdateCommentHttpPostModel vm);
}