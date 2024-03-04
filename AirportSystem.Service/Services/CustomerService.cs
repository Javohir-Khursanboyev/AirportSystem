using AirportSystem.Data.IRepositories;
using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.Customer;
using AirportSystem.Domain.Entities.Employee;
using AirportSystem.Service.Extensions;
using AirportSystem.Service.Interfaces;

namespace AirportSystem.Service.Services;

public class CustomerService : ICustomerService
{
    private readonly CustomerRepository customerRepository;
    public CustomerService(CustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }
    public async Task<CustomerViewModel> CreateAsync(CustomerCreationModel model)
    {
        var customers = await customerRepository.GetAllAsync();
        var existCustomer = customers.FirstOrDefault(c => c.Email == model.Email);
        if(existCustomer != null)
        {
            if(existCustomer.IsDeleted)
                return await UpdateAsync(existCustomer.Id, model.MapTo<CustomerUpdateModel> (), true);

            throw new Exception($"This customer is already exist With this email : {model.Email}");
        }

        var createdCustomer = await customerRepository.InsertAsync(model.MapTo<Customers>());
        return createdCustomer.MapTo<CustomerViewModel> ();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var customers = await customerRepository.GetAllAsync();
        var existCustomer = customers.FirstOrDefault(c => c.Id == id && !c.IsDeleted)
            ?? throw new Exception($"This customer is not found With this id : {id}");

        await customerRepository.DeleteAsync(id);
        return true;
    }

    public async Task<CustomerViewModel> DepositAsync(long id, decimal amount)
    {
        var customers = await customerRepository.GetAllAsync();
        var existCustomer = customers.FirstOrDefault(c => c.Id == id && !c.IsDeleted)
            ?? throw new Exception($"This customer is not found With this id : {id}");

        existCustomer.Balance += amount;
        await customerRepository.UpdateAsync(id, existCustomer);
        return existCustomer.MapTo<CustomerViewModel> ();
    }

    public async Task<IEnumerable<CustomerViewModel>> GetAllAsync()
    {
        var customers = await customerRepository.GetAllAsync();
        return customers.Where(c => !c.IsDeleted).MapTo<CustomerViewModel> ();
    }

    public async Task<CustomerViewModel> GetByIdAsync(long id)
    {
        var customers = await customerRepository.GetAllAsync();
        var existCustomer = customers.FirstOrDefault(c => c.Id == id && !c.IsDeleted)
            ?? throw new Exception($"This customer is not found With this id : {id}");

        return existCustomer.MapTo<CustomerViewModel> ();
    }

    public async Task<CustomerViewModel> SecurityCheckAsync(string email, string password)
    {
        var customers = await customerRepository.GetAllAsync();
        var existCustomer = customers.FirstOrDefault(c => c.Email == email && !c.IsDeleted)
            ?? throw new Exception($"This user is not found With this email : {email}");

        if (existCustomer.Password != password)
            throw new Exception($"Password is incorrect");

        return existCustomer.MapTo<CustomerViewModel> ();
    }

    public async Task<CustomerViewModel> UpdateAsync(long id, CustomerUpdateModel model, bool isUsesDeleted = false)
    {
        var customers = await customerRepository.GetAllAsync();
        var existCustomer = new Customers();
        if (isUsesDeleted)
            existCustomer = customers.FirstOrDefault(e => e.Id == id);
        else
            existCustomer = customers.FirstOrDefault(c => c.Id == id && !c.IsDeleted)
                ?? throw new Exception($"This customer is not found With this id : {id}");

        var updatedCustomer = customerRepository.UpdateAsync(id, model.MapTo<Customers>());
        return updatedCustomer.MapTo<CustomerViewModel> ();
    }
}
