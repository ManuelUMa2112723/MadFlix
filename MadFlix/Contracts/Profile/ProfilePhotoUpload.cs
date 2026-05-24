namespace MadFlix.Contracts.Profile;

public sealed class ProfilePhotoUpload
{
    public ProfilePhotoUpload(
        Stream content,
        string fileName,
        string contentType,
        long size)
    {
        Content = content;
        FileName = fileName;
        ContentType = contentType;
        Size = size;
    }

    public Stream Content { get; }
    public string FileName { get; }
    public string ContentType { get; }
    public long Size { get; }
}
