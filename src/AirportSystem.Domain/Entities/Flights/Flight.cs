using AirportSystem.Domain.Enums;
using AirportSystem.Domain.Commons;
using AirportSystem.Domain.Entities.Airports;
using AirportSystem.Domain.Entities.Aircrafts;

namespace AirportSystem.Domain.Entities.Flights;

public class Flight : Auditable
{
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public long DepartureAirportId { get; set; }
    public long ArrivalAirportId { get; set; }
    public long AircraftId { get; set; }
    public FlightStatus Status { get; set; }

    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }
    public Aircraft Aircraft { get; set; }

    public IEnumerable<FlightEmployee> FlightEmployees { get; set; }
}
