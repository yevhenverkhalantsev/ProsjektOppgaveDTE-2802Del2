namespace ProsjektOppgaveWebAPI.Services.JwtServices.Models;

public class JwtOptions
{
    public string Key { get; set; }
    public int ExpirationHours { get; set; }
}