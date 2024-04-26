using AirportSystem.Domain.Commons;
using AirportSystem.Domain.Entities.Employees;

namespace AirportSystem.Domain.Entities.Flights;

public class FlightEmployee : Auditable
{
    public long FlightId { get; set; }
    public long EmployeeId { get; set; }

    public Flight Flight { get; set; }
    public Employee Employee { get; set; }
}
