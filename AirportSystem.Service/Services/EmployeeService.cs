using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.Employee;
using AirportSystem.Service.Extensions;
using AirportSystem.Service.Interfaces;

namespace AirportSystem.Service.Services;

public class EmployeeService : IEmployeeService
{
    private readonly EmployeeRepository employeeRepository;
    public EmployeeService(EmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }
    public async Task<EmployeeViewModel> CreateAsync(EmployeeCreationModel model)
    {
        var employees = await employeeRepository.GetAllAsync();
        var existEmployee = employees.FirstOrDefault(e => e.Email == model.Email);
        if (existEmployee is not null)
        {
            if (existEmployee.IsDeleted)
                return await UpdateAsync(existEmployee.Id, model.MapTo<EmployeeUpdateModel>(), true);

            throw new Exception($"This employee is already exist With this email : {model.Email}");
        }

        var createdEmployee = await employeeRepository.InsertAsync(model.MapTo<Employees>());

        return createdEmployee.MapTo<EmployeeViewModel> ();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var employees = await employeeRepository.GetAllAsync();
        var existEmployee = employees.FirstOrDefault(e => e.Id == id && !e.IsDeleted)
            ?? throw new Exception($"This employee is not found With this id {id}");

        await employeeRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<EmployeeViewModel>> GetAllAsync()
    {
        var employees = await employeeRepository.GetAllAsync();
        return employees.Where(e => !e.IsDeleted).MapTo<EmployeeViewModel> ();
    }

    public async Task<EmployeeViewModel> GetByIdAsync(long id)
    {
        var employees = await employeeRepository.GetAllAsync();
        var existEmployee = employees.FirstOrDefault(e => e.Id == id && !e.IsDeleted)
            ?? throw new Exception($"This employee is not found With this id {id}");

        return existEmployee.MapTo<EmployeeViewModel> ();
    }

    public async Task<EmployeeViewModel> UpdateAsync(long id, EmployeeUpdateModel model , bool IsUsesDeleted = false)
    {
        var employees = await employeeRepository.GetAllAsync();
        var existEmployee = new Employees();
        if (IsUsesDeleted)
            existEmployee = employees.FirstOrDefault(e => e.Id == id);
        else
            existEmployee = employees.FirstOrDefault(e => e.Id == id && !e.IsDeleted)
                ?? throw new Exception($"This employee is not found With this id {id}");

        var updatedEmployee = await employeeRepository.UpdateAsync(id, model.MapTo<Employees>());

        return updatedEmployee.MapTo<EmployeeViewModel> ();
    }
}