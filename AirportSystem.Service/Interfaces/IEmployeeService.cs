using AirportSystem.Domain.Entities.Employee;

namespace AirportSystem.Service.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeViewModel> CreateAsync (EmployeeCreationModel model);
    Task<EmployeeViewModel> UpdateAsync (long id ,EmployeeUpdateModel model ,bool isUsesDeleted);
    Task<IEnumerable<EmployeeViewModel>> GetAllAsync ();
    Task<EmployeeViewModel> GetByIdAsync (long  id);
    Task<bool> DeleteAsync (long id);
}