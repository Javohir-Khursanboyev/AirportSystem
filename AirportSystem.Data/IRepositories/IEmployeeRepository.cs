using AirportSystem.Domain.Entities.Employee;

namespace AirportSystem.Data.IRepositories;

public interface IEmployeeRepository
{
    public Task<Employees> InsertAsync(Employees employee);
    public Task<Employees> UpdateAsync(long id,Employees employee);
    public Task<bool> DeleteAsync(long id);
    public Task<IEnumerable<Employees>> GetAllAsync();
}