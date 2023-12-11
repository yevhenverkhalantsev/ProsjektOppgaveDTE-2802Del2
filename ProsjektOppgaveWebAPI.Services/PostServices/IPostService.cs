using System.Security.Principal;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.PostServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.PostServices;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPostsForBlog(int blogId);

    Task<ResponseService<Post>> GetPost(int id);
    
    Task<ResponseService<long>> SavePost(CreatePostHttpPostModel vm);

    Task DeletePost(int id, IPrincipal principal);
    
}