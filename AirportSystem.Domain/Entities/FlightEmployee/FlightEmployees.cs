using AirportSystem.Domain.Commons;

namespace AirportSystem.Domain.Entities.FlightEmployee;

public class FlightEmployees:Auditable
{
    public long FlightId { get; set; }
    public long EmployeeId { get; set; }
}