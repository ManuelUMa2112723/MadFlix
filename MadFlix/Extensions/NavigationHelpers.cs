using Microsoft.AspNetCore.Components;

namespace MadFlix.Extensions;

public static class NavigationHelpers
{
    public static string NormalizeReturnUrl(string? returnUrl)
    {
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            return "/";
        }

        if (!returnUrl.StartsWith('/') || returnUrl.StartsWith("//"))
        {
            return "/";
        }

        return returnUrl;
    }

    public static string ToLocalPath(this NavigationManager navigationManager)
    {
        var relativePath = navigationManager.ToBaseRelativePath(navigationManager.Uri);

        if (string.IsNullOrWhiteSpace(relativePath))
        {
            return "/";
        }

        return relativePath.StartsWith('/') ? relativePath : $"/{relativePath}";
    }
}
