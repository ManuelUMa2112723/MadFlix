namespace MadFlix.Services.Profile;

public interface IUserProfilePhotoChangeNotifier
{
    event Action<int, string?>? PhotoChanged;
    void NotifyChanged(int utilizadorId, string? photoUrl);
}
