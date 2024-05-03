using AirportSystem.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace AirportSystem.Service.Helper;

public static class FileHelper
{
    public static async ValueTask<(string Path, string Name)> CreateFileAsync(IFormFile formFile, FileType fileType)
    {
        var directoryPath = Path.Combine(EnvironmentHelper.WebRootPath, fileType.ToString());
        if(!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var fullPath = Path.Combine(directoryPath, formFile.FileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        var memoryStream = new MemoryStream();
        formFile.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();
        await fileStream.WriteAsync(bytes);

        return (fullPath, formFile.FileName);
    }
}
