using AirportSystem.Service.DTOs.Flights;
using AirportSystem.Service.DTOs.Employees;

namespace AirportSystem.Service.DTOs.FlightEmployee;

public class FlightEmployeeViewModel
{
    public long Id { get; set; }
    public FlightViewModel Flight { get; set; }
    public EmployeeViewModel Employee{ get; set; }
}
