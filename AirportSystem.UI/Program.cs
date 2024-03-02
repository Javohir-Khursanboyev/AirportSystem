using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.Aircraft;
using AirportSystem.Domain.Entities.Customer;
using AirportSystem.Domain.Entities.Employee;
using AirportSystem.Domain.Entities.Flight;
using AirportSystem.Domain.Enums;
using AirportSystem.Service.Services;

var customerRepository = new FlightRepository();
var model = new FlightUpdateModel()
{
   AircraftId = 1,
   PlaceOfDeparture = "Toshkent",
   PlaceOfArrival = "Samarqand",
   DepartureTime = DateTime.Now,
   ArrivalTime = DateTime.Now,
   FlightStatus = FlightStatus.OnTime
};
var employeeService = new FlightService(customerRepository);
var result = await employeeService.UpdateAsync(2,model);
Console.WriteLine(result);