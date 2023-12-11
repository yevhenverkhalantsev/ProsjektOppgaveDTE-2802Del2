using Microsoft.AspNetCore.Identity;

namespace ProsjektOppgaveBlazor.Data.CommonModels;

public class PostModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
}