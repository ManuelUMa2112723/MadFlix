namespace MadFlix.Services.Profile;

public sealed class UserProfilePhotoChangeNotifier : IUserProfilePhotoChangeNotifier
{
    public event Action<int, string?>? PhotoChanged;

    public void NotifyChanged(int utilizadorId, string? photoUrl)
    {
        PhotoChanged?.Invoke(utilizadorId, photoUrl);
    }
}
