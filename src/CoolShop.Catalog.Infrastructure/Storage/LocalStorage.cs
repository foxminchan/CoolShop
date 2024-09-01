using Microsoft.Extensions.Logging;

namespace CoolShop.Catalog.Infrastructure.Storage;

public sealed class LocalStorage(ILogger<LocalStorage> logger) : ILocalStorage
{
    public async Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var newFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Pics", newFileName);

        logger.LogInformation("[{Service}] Uploading file {FileName} to {FilePath}", nameof(LocalStorage), newFileName,
            filePath);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        return newFileName;
    }

    public void RemoveFile(string? fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return;
        }

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Pics", fileName);

        logger.LogInformation("[{Service}] Removing file {FileName} from {FilePath}", nameof(LocalStorage), fileName,
            filePath);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
