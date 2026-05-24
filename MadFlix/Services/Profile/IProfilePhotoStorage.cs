namespace MadFlix.Services.Profile;

public interface IProfilePhotoStorage
{
    Task<string> SaveAsync(Stream content, string extension, CancellationToken cancellationToken = default);
    Task DeleteAsync(string? photoUrl, CancellationToken cancellationToken = default);
}
