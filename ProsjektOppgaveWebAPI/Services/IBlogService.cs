using System.Security.Principal;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Models.ViewModel;

namespace ProsjektOppgaveWebAPI.Services;

public interface IBlogService
{
    Task <IEnumerable<Blog>> GetAllBlogs();

    Blog GetBlog(int id);
    
    Task Save(Blog blog, IPrincipal principal);
    
    Task Delete(int id , IPrincipal principal);
    
    IEnumerable<Post> GetPostsForBlog(int blogId);

    PostViewModel GetPostViewModel();

    PostViewModel GetPostViewModel(int id);

    Task SavePost(Post post, IPrincipal principal);

    Task DeletePost(int id, IPrincipal principal);

    Task SaveComment(Comment comment, IPrincipal principal);

    Task DeleteComment(int id, IPrincipal principal);

    CommentViewModel GetCommentViewModel(int id);
    
}