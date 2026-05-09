namespace MadFlix.Services.Auth;

public enum PasswordVerificationStatus
{
    Failed = 0,
    Succeeded = 1,
    SucceededRehashRequired = 2
}
