using System.Security.Claims;
using MadFlix.Models;

namespace MadFlix.Services.Auth;

public interface IUserClaimsPrincipalFactory
{
    ClaimsPrincipal Create(Utilizador utilizador);
}
