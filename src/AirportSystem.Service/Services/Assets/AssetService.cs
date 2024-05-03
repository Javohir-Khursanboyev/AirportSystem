using AutoMapper;
using AirportSystem.Service.Helper;
using AirportSystem.Data.UnitOfWorks;
using AirportSystem.Service.Exceptions;
using AirportSystem.Service.Extensions;
using AirportSystem.Service.DTOs.Assets;
using AirportSystem.Domain.Entities.Assets;

namespace AirportSystem.Service.Services.Assets;

public class AssetService(IMapper mapper, IUnitOfWork unitOfWork) : IAssetService
{
    public async ValueTask<AssetViewModel> UploadAsync(AssetCreateModel createModel)
    {
        var assetData = await FileHelper.CreateFileAsync(createModel.File, createModel.FileType);
        var existAsset = new Asset()
        {
            Name = assetData.Name,
            Path = assetData.Path
        };
        existAsset.Create();
        var createdAsset = await unitOfWork.Assets.InsertAsync(existAsset);
        await unitOfWork.SaveAsync();

        return mapper.Map<AssetViewModel>(createdAsset);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existAsset = await unitOfWork.Assets.SelectAsync(a => a.Id == id)
            ?? throw new NotFoundException("Asset is not found");

        await unitOfWork.Assets.DropAsync(existAsset);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<AssetViewModel> GetById(long id)
    {
        var existAsset = await unitOfWork.Assets.SelectAsync(a => a.Id == id)
            ?? throw new NotFoundException("Asset is not found");

        return mapper.Map<AssetViewModel>(existAsset);
    }
}
