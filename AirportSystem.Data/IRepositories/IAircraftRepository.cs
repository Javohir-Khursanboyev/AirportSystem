using AirportSystem.Domain.Entities.Aircraft;

namespace AirportSystem.Data.IRepositories;

public interface IAircraftRepository
{
    public Task<Aircrafts> InsertAsync(Aircrafts aircraft);
    public Task<Aircrafts> UpdateAsync(long id, Aircrafts aircraft);
    public Task<bool> DeleteAsync(long id);
    public Task<IEnumerable<Aircrafts>> GetAllAsync();
}
