using AirportSystem.Domain.Enums;

namespace AirportSystem.Domain.Entities.Flight;

public class FlightUpdateModel
{
    public long AircraftId { get; set; }
    public string PlaceOfDeparture { get; set; }
    public string PlaceOfArrival { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public FlightStatus FlightStatus { get; set; }
}
