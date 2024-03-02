using AirportSystem.Domain.Entities.Customer;

namespace AirportSystem.Data.IRepositories;

public interface ICustomerRepository
{
    public Task<Customers> InsertAsync(Customers customer);
    public Task<Customers> UpdateAsync(long id, Customers customer);
    public Task<bool> DeleteAsync(long id);
    public Task<IEnumerable<Customers>> GetAllAsync();
}
