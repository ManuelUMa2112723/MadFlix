namespace MadFlix.Services.Auth;

public interface IUserIdentityNormalizer
{
    string Normalize(string value);
    string Trim(string? value);
}
