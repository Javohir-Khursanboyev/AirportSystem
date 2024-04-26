using AirportSystem.Domain.Commons;
using AirportSystem.Domain.Entities.Flights;

namespace AirportSystem.Domain.Entities.Aircrafts;

public class Aircraft : Auditable
{
    public string Name { get; set; }
    public int TotalOfSeats { get; set; }

    public IEnumerable<Flight> Flights { get; set; }
}
