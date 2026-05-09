namespace MadFlix.Contracts.Auth;

public sealed class LoginRequest
{
    public string Identificador { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; }
}
