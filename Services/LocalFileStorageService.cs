

using Microsoft.Extensions.Options;
using Trainee.api.Configurations;
using Trainee.api.Controllers;
using Trainee.api.Exceptions;
using Trainee.api.Models;
using System.Security.Cryptography;


namespace Trainee.api.Services;

public class LocalFileStorageService : IFileStorageService
{

    FileStorageConfig _fileStorageConfig;
    private readonly ILogger<LocalFileStorageService> _logger;
    private readonly string _storageRoot;

    public LocalFileStorageService(
        IOptions<FileStorageConfig> options,
        ILogger<LocalFileStorageService> logger
    )
    {
        _logger = logger;
        _fileStorageConfig = options.Value;
        _storageRoot = Path.GetFullPath(_fileStorageConfig.RootPath);

        // create directory if it doesn't exist
        Directory.CreateDirectory(_storageRoot);
    }


    public bool validate(IFormFile file)
    {

        if (file == null || file.Length == 0)
            throw new BadRequestException("File is empty");

        if (file.Length > _fileStorageConfig.MaxFileSizeBytes)
            throw new BadRequestException($"File size exceeds the {_fileStorageConfig.MaxFileSizeBytes} bytes limit");

        var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileContentType = file.ContentType;

        if (!_fileStorageConfig.AllowedExtensions.Contains(fileExt))
            throw new BadRequestException($"File extension {fileExt} is not allowed");

        if(!_fileStorageConfig.AllowedContentTypes.Contains(fileContentType)) 
            throw new BadRequestException($"File content-type {fileContentType} is not allowed");

        return true;
    }

    public async Task<string> ComputeCheckSum(Stream stream)
    {
        stream.Position = 0;
        var hash = await SHA256.Create().ComputeHashAsync(stream);
        stream.Position = 0;
        return Convert.ToHexString(hash);
    }

    // get safe path and avoid path traversal attack
    private string GetSafePath(string storageName)
    {
        string fileName = Path.GetFileName(storageName);
        string fullPath = Path.GetFullPath(Path.Combine(_storageRoot, fileName));

        if (!fullPath.StartsWith(_storageRoot, StringComparison.OrdinalIgnoreCase))
        {
            throw new BadRequestException("Provided path is not valid");
        }

        return fullPath;
    }


    public async Task<string> SaveAsync(Stream fileStream, string storageName)
    {
        var path = GetSafePath(storageName);
        await using var dest = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        await fileStream.CopyToAsync(dest);
        return path;
    }

    public async Task<Stream> OpenReadAsync(string storageName)
    {
        var path = GetSafePath(storageName);

        if (!File.Exists(path)) {
            throw new KeyNotFoundException($"File with filename : {storageName} was not found");
        }

        Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return stream;
    }

    public async Task<bool> ExistsAsync(string storageName)
    {
        var path = GetSafePath(storageName);

        if (!File.Exists(path))
            return false;

        return true;
    }

    public async Task DeleteAsync(string storageName)
    {
        var path = GetSafePath(storageName);

        if (!File.Exists(path)) {
            throw new KeyNotFoundException($"File with filename : {storageName} was not found");
        }

        File.Delete(path);
    }
}