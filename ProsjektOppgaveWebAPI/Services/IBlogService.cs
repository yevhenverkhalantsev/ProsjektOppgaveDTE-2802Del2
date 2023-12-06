using System.Security.Principal;
using ProsjektOppgaveWebAPI.Models;
using ProsjektOppgaveWebAPI.Models.ViewModel;

namespace ProsjektOppgaveWebAPI.Services;

public interface IBlogService
{
    // Blog
    Task<IEnumerable<Blog>> GetAllBlogs();

    Blog? GetBlog(int id);
    
    Task Save(Blog blog, IPrincipal principal);
    
    Task Delete(int id , IPrincipal principal);

    BlogViewModel GetBlogViewModel();

    BlogViewModel GetBlogViewModel(int id);

    
    // Post
    Task<IEnumerable<Post>> GetPostsForBlog(int blogId);
    
    Task SavePost(Post post, IPrincipal principal);

    Task DeletePost(int id, IPrincipal principal);
    
    PostViewModel GetPostViewModel();

    PostViewModel GetPostViewModel(int id);
}