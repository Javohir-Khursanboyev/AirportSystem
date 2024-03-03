using AirportSystem.Domain.Entities.Customer;
using AirportSystem.Domain.Entities.Employee;

namespace AirportSystem.Service.Interfaces;

public interface ICustomerService
{
    Task<CustomerViewModel> CreateAsync(CustomerCreationModel model);
    Task<CustomerViewModel> UpdateAsync(long id, CustomerUpdateModel model, bool isUsesDeleted);
    Task<IEnumerable<CustomerViewModel>> GetAllAsync();
    Task<CustomerViewModel> GetByIdAsync(long id);
    Task<bool> DeleteAsync(long id);
    Task<CustomerViewModel> DepositAsync(long id , decimal amount);
    Task<CustomerViewModel> SecurityCheckAsync(string email , string password);
}