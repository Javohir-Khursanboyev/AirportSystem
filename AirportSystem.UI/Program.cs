using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.Employee;
using AirportSystem.Domain.Enums;
using AirportSystem.Service.Services;

var employeeRepository=new EmployeeRepository();
var model = new EmployeeUpdateModel()
{
    FirstName = "Javohir",
    LastName = "Xursanboyev",
    PassportNumber = "AC2278929",
    PhoneNumber = "+998979898188",
    DateOfBirth = DateTime.Now,
    Email = "JavohirInfo@gmail.com",
    Address = "Andijon",
    EmployeeType = EmployeeType.Passenger
};
var employeeService = new EmployeeService(employeeRepository);
var result = await employeeService.UpdateAsync(3,model);
Console.WriteLine(result);