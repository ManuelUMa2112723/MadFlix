namespace MadFlix.Models;

public sealed class UserNotification
{
    public string Titulo { get; init; } = string.Empty;
    public string Mensagem { get; init; } = string.Empty;
    public string Tipo { get; init; } = string.Empty;
    public DateTime CriadoEmUtc { get; init; } = DateTime.UtcNow;
}
