using System.ComponentModel.DataAnnotations;

namespace ProsjektOppgaveBlazor.Data.CommonModels;

public class PostViewModel
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
}