using AirportSystem.Domain.Entities.Aircraft;
using AirportSystem.Domain.Entities.Flight;

namespace AirportSystem.Service.Interfaces;

public interface IFlightService
{
    Task<FlightViewModel> CreateAsync(FlightCreationModel model);
    Task<FlightViewModel> UpdateAsync(long id, FlightUpdateModel model, bool isUsesDeleted);
    Task<IEnumerable<FlightViewModel>> GetAllAsync(DateTime ? date);
    Task<FlightViewModel> GetByIdAsync(long id);
    Task<bool> DeleteAsync(long id);
}