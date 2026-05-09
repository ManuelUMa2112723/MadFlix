using System.Security.Claims;

namespace MadFlix.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int? GetMadFlixUserId(this ClaimsPrincipal principal)
    {
        var rawValue = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(rawValue, out var userId) ? userId : null;
    }
}
