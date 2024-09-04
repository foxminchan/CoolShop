namespace CoolShop.Catalog.Infrastructure.Storage;

public interface IAzuriteService
{
    Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string? url, CancellationToken cancellationToken = default);
}
