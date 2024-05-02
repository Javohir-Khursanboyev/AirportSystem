using Microsoft.AspNetCore.Http;
using AirportSystem.Domain.Enums;

namespace AirportSystem.Service.DTOs.Assets;

public class AssetCreateModel
{
    public IFormFile File { get; set; }
    public FileType FileType { get; set; }
}
