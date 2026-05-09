using System.Security.Claims;
using MadFlix.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MadFlix.Services.Auth;

public sealed class UserClaimsPrincipalFactory : IUserClaimsPrincipalFactory
{
    public ClaimsPrincipal Create(Utilizador utilizador)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, utilizador.Id.ToString()),
            new(ClaimTypes.Name, utilizador.NomeUtilizador),
            new(ClaimTypes.Email, utilizador.Email)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }
}
