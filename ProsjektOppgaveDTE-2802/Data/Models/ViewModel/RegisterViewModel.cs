using System.ComponentModel.DataAnnotations;

namespace ProsjektOppgaveBlazor.data.Models.ViewModel;

public class RegisterViewModel
{
    [Required(ErrorMessage = "User Name is required")]
    public string? username { get; set; }
    
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string? password { get; set; }
}