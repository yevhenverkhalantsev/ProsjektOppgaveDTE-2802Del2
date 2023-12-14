using System.Security.Principal;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.PostServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.PostServices;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPostsForBlog(int blogId);

    Task<ResponseService<Post>> GetPost(int id);
    
    Task<ResponseService<int>> SavePost(CreatePostHttpPostModel vm);

    Task<ResponseService<bool>> DeletePost(int postId);
    
    Task<ResponseService<Post>> UpdatePost(UpdatePostHttpPutModel vm);
    
}