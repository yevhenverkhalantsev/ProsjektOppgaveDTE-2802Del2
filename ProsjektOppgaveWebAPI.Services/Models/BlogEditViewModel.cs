using System.ComponentModel.DataAnnotations;

namespace ProsjektOppgaveWebAPI.Models.ViewModel;

public class BlogViewModel
{
    public int BlogId { get; set; }
    public string Name { get; set; }
    public bool IsOpen { get; set; }
}