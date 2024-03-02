using AirportSystem.Domain.Entities.Customer;
using AirportSystem.Domain.Entities.Flight;

namespace AirportSystem.Data.IRepositories;

public interface IFlightRepository
{
    public Task<Flights> InsertAsync(Flights flight);
    public Task<Flights> UpdateAsync(long id, Flights flight);
    public Task<bool> DeleteAsync(long id);
    public Task<IEnumerable<Flights>> GetAllAsync();
}
