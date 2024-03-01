using AirportSystem.Data.IRepositories;
using AirportSystem.Domain.Entities.Employee;

namespace AirportSystem.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    public IEnumerable<Employees> GetAllAsync()
    {
        throw new NotImplementedException();
    }

   
    public  Task<Employees> InsertAsync(Employees employee)
    {
        throw new NotImplementedException();
    }
}