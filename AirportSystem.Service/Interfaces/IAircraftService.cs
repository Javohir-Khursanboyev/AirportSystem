using AirportSystem.Domain.Entities.Aircraft;

namespace AirportSystem.Service.Interfaces;

public interface IAircraftService
{
    Task<AircraftViewModel> CreateAsync(AircraftCreationModel model);
    Task<AircraftViewModel> UpdateAsync(long id, AircraftUpdateModel model, bool isUsesDeleted);
    Task<IEnumerable<AircraftViewModel>> GetAllAsync();
    Task<AircraftViewModel> GetByIdAsync(long id);
    Task<bool> DeleteAsync(long id);
}
