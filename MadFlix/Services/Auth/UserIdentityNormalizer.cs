namespace MadFlix.Services.Auth;

public sealed class UserIdentityNormalizer : IUserIdentityNormalizer
{
    public string Normalize(string value)
    {
        return Trim(value).ToUpperInvariant();
    }

    public string Trim(string? value)
    {
        return value?.Trim() ?? string.Empty;
    }
}
