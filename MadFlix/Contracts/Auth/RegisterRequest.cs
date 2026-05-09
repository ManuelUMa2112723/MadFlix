namespace MadFlix.Contracts.Auth;

public sealed class RegisterRequest
{
    public string NomeUtilizador { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmarPassword { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; }
}
