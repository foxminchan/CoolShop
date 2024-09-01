namespace CoolShop.Catalog.Infrastructure.Storage;

public interface ILocalStorage
{
    Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default);
    void RemoveFile(string? fileName);
}
