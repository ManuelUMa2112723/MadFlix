namespace MadFlix.Contracts.Auth;

public sealed class LogoutRequest
{
    public string? ReturnUrl { get; set; }
}
