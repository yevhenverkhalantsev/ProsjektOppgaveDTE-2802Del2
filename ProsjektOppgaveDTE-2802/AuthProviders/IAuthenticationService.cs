using ProsjektOppgaveBlazor.Data.CommonModels;

namespace ProsjektOppgaveBlazor.AuthProviders;

public interface IAuthenticationService
{
    Task<LoginResponse> Login(LoginViewModel loginViewModel);
    Task Logout();
    Task<RegisterResponse> RegisterUser(RegisterViewModel registerViewModel);
}