using AirportSystem.Service.DTOs.Assets;

namespace AirportSystem.Service.Services.Assets;

public interface IAssetService
{
    ValueTask<AssetViewModel> UploadAsync(AssetCreateModel createModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<AssetViewModel> GetById(long id);
}
