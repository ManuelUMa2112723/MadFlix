using MadFlix.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MadFlix.Services.Auth;

public sealed class UserSessionService : IUserSessionService
{
    private readonly IUserClaimsPrincipalFactory _claimsPrincipalFactory;

    public UserSessionService(IUserClaimsPrincipalFactory claimsPrincipalFactory)
    {
        _claimsPrincipalFactory = claimsPrincipalFactory;
    }

    public async Task SignInAsync(HttpContext httpContext, Utilizador utilizador)
    {
        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            _claimsPrincipalFactory.Create(utilizador));
    }

    public async Task SignOutAsync(HttpContext httpContext)
    {
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
