using MadFlix.Models;

namespace MadFlix.Services.Auth;

public interface IUserSessionService
{
    Task SignInAsync(HttpContext httpContext, Utilizador utilizador);
    Task SignOutAsync(HttpContext httpContext);
}
