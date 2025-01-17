using System.Security.Principal;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.Services.BlogServices.Models;
using ProsjektOppgaveWebAPI.Services.Response;

namespace ProsjektOppgaveWebAPI.Services.BlogServices;

public interface IBlogService
{
    Task<IEnumerable<Blog>> GetAllBlogs();

    Task<ResponseService<Blog>> GetBlog(int id);
    
    Task<ICollection<Blog>> GetAllBlogsByUserId(string userId);
    
    Task<ResponseService<Blog>> Save(CreateBlogHttpPostModel vm, IPrincipal principal);
     
    Task<ResponseService<bool>> Delete(int id);
    
    Task<ICollection<Blog>> Search(string searchQuery);
}