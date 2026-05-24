using System.Security.Cryptography;

namespace MadFlix.Services.Profile;

public sealed class LocalProfilePhotoStorage : IProfilePhotoStorage
{
    private const string PublicFolder = "/uploads/profile-photos";
    private readonly IWebHostEnvironment _environment;

    public LocalProfilePhotoStorage(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveAsync(
        Stream content,
        string extension,
        CancellationToken cancellationToken = default)
    {
        var safeExtension = extension.StartsWith('.') ? extension : $".{extension}";
        var fileName = $"{RandomNumberGenerator.GetHexString(24).ToLowerInvariant()}{safeExtension}";
        var folderPath = Path.Combine(_environment.WebRootPath, "uploads", "profile-photos");
        Directory.CreateDirectory(folderPath);

        var filePath = Path.Combine(folderPath, fileName);

        await using var output = File.Create(filePath);
        await content.CopyToAsync(output, cancellationToken);

        return $"{PublicFolder}/{fileName}";
    }

    public Task DeleteAsync(string? photoUrl, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(photoUrl) || !photoUrl.StartsWith(PublicFolder, StringComparison.Ordinal))
            return Task.CompletedTask;

        var fileName = Path.GetFileName(photoUrl);

        if (string.IsNullOrWhiteSpace(fileName))
            return Task.CompletedTask;

        var filePath = Path.Combine(_environment.WebRootPath, "uploads", "profile-photos", fileName);

        if (File.Exists(filePath))
            File.Delete(filePath);

        return Task.CompletedTask;
    }
}
