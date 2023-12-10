using System.ComponentModel.DataAnnotations;

namespace ProsjektOppgaveBlazor.Data.CommonModels;

public class BlogViewModel
{
    public int BlogId { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }
}