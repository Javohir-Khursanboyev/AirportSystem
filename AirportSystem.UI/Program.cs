using AirportSystem.Data.Repositories;
using AirportSystem.Domain.Entities.Aircraft;
using AirportSystem.Domain.Entities.Customer;
using AirportSystem.Domain.Entities.Employee;
using AirportSystem.Domain.Entities.Flight;
using AirportSystem.Domain.Entities.FlightEmployee;
using AirportSystem.Domain.Entities.Ticket;
using AirportSystem.Domain.Enums;
using AirportSystem.Service.Services;

var customerRepository = new FlightEmployeeRepository();
var flightRepository = new FlightRepository();
AircraftRepository aircraftRepository = new AircraftRepository();
AircraftService aircraftService = new AircraftService(aircraftRepository);
FlightService flightService = new FlightService(flightRepository,aircraftService);
EmployeeRepository employeeRepository = new EmployeeRepository();
EmployeeService employeeService1 = new EmployeeService(employeeRepository);

var model = new FlightEmployeeCreationModel()
{
    FlightId = 2,
    EmployeeId = 3,
};
var employeeService = new FlightEmployeeService(customerRepository,flightService,employeeService1);
var result = await employeeService.CreateAsync(model);
Console.WriteLine(result);