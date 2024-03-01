using AirportSystem.Domain.Enums;

namespace AirportSystem.Domain.Entities.Employee;

public class EmployeeUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PassportNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public EmployeeType EmployeeType { get; set; }
}
