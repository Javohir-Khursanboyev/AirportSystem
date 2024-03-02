using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.Customer;
using AirportSystem.Domain.Entities.Employee;
using AirportSystem.Domain.Enums;
using AirportSystem.Service.Services;

var customerRepository = new CustomerRepository();
var model = new CustomerCreationModel()
{
    FirstName = "Javohir",
    LastName = "Xursanboyev",
    PassportNumber = "AC2278929",
    PhoneNumber = "+998979898188",
    DateOfBirth = DateTime.Now,
    Email = "JavohirInfo@gmail.com",
};
var employeeService = new CustomerService(customerRepository);
var result = await employeeService.DepositAsync(1,1000);
Console.WriteLine(result);