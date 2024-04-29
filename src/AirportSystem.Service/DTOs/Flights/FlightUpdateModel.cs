using AirportSystem.Domain.Enums;

namespace AirportSystem.Service.DTOs.Flights;

public class FlightUpdateModel
{
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public long DepartureAirportId { get; set; }
    public long ArrivalAirportId { get; set; }
    public long AircraftId { get; set; }
    public FlightStatus Status { get; set; }
}
