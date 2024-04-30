﻿using Microsoft.AspNetCore.Http;

namespace AirportSystem.Service.DTOs.Assets;

public class AssetCreateModel
{
    public IFormFile File { get; set; }
    public FileType FileType { get; set; }
}
