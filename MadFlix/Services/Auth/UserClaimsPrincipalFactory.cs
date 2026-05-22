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
            new Claim(ClaimTypes.NameIdentifier, utilizador.Id.ToString()),
            new Claim(ClaimTypes.Name, utilizador.NomeUtilizador),
            new Claim(ClaimTypes.Email, utilizador.Email),
            new Claim(ClaimTypes.Role, "User")
        };

        if (utilizador.IsAdmin)
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }
}