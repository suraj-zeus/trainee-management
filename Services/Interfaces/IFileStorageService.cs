

namespace Trainee.api.Services;


public interface IFileStorageService
{

      public bool validate(IFormFile file);

    public Task<string> ComputeCheckSum(Stream stream);
    public Task<string> SaveAsync(Stream fileStream, string storageName);

    public Task<Stream> OpenReadAsync(string storageName);

    public Task<bool> ExistsAsync(string storageName);

    public Task DeleteAsync(string storageName);
}