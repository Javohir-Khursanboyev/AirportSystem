using AirportSystem.Domain.Commons;
using AirportSystem.Domain.Entities.Flights;

namespace AirportSystem.Domain.Entities.Airports;

public class Airport : Auditable
{
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string IATACode { get; set; }
    public string ICAOCode { get; set; }

    public IEnumerable<Flight> StartFlights { get; set; }
    public IEnumerable<Flight> EndFlights { get; set; }
}
