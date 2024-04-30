using AirportSystem.Domain.Enums;
using AirportSystem.Service.DTOs.Airports;
using AirportSystem.Service.DTOs.Aircrafts;

namespace AirportSystem.Service.DTOs.Flights;

public class FlightViewModel
{
    public long Id { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public AirportViewModel DepartureAirport { get; set; }
    public AirportViewModel ArrivalAirport { get; set; }
    public AircraftViewModel Aircraft { get; set; }
    public FlightStatus Status { get; set; }
}
