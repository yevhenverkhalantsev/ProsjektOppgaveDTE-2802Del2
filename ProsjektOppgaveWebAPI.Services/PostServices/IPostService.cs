using System.Security.Principal;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Models.ViewModel;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.PostServices;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPostsForBlog(int blogId);

    Task<ResponseService<Post>> GetPost(int id);
    
    Task SavePost(Post post, IPrincipal principal);

    Task DeletePost(int id, IPrincipal principal);
    
    PostViewModel GetPostViewModel();

    PostViewModel GetPostViewModel(int id);
}