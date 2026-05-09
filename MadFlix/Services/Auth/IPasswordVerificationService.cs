using MadFlix.Models;

namespace MadFlix.Services.Auth;

public interface IPasswordVerificationService
{
    string HashPassword(Utilizador utilizador, string password);
    PasswordVerificationStatus VerifyPassword(Utilizador utilizador, string passwordHash, string password);
}
