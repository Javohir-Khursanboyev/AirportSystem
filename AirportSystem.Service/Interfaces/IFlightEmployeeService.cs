using AirportSystem.Domain.Entities.FlightEmployee;

namespace AirportSystem.Service.Interfaces;

public interface IFlightEmployeeService
{
    Task<FlightEmployeeViewModel> CreateAsync(FlightEmployeeCreationModel model);
    Task<FlightEmployeeViewModel> UpdateAsync(long id, FlightEmployeeUpdateModel model, bool isUsesDeleted);
    Task<IEnumerable<FlightEmployeeViewModel>> GetAllAsync();
    Task<FlightEmployeeViewModel> GetByIdAsync(long id);
    Task<bool> DeleteAsync(long id);
}