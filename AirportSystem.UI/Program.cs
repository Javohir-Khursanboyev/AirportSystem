using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.Aircraft;
using AirportSystem.Domain.Entities.Customer;
using AirportSystem.Domain.Entities.Employee;
using AirportSystem.Domain.Enums;
using AirportSystem.Service.Services;

var customerRepository = new AircraftRepository();
var model = new AircraftUpdateModel()
{
   Name = "Air Faly bshbcneb 900",
   TotalNumberOfSeats = 100,
};
var employeeService = new AircraftService(customerRepository);
var result = await employeeService.UpdateAsync(1,model);
Console.WriteLine(result);