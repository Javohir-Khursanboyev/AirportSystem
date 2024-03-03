using AirportSystem.Domain.Entities.FlightEmployee;

namespace AirportSystem.Data.IRepositories;

public interface IFlightEmployeeRepository
{
    public Task<FlightEmployees> InsertAsync(FlightEmployees flightEmployee);
    public Task<FlightEmployees> UpdateAsync(long id, FlightEmployees flightEmployee);
    public Task<bool> DeleteAsync(long id);
    public Task<IEnumerable<FlightEmployees>> GetAllAsync();
}
