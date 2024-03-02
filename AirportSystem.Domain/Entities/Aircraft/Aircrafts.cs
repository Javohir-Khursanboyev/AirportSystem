using AirportSystem.Domain.Commons;

namespace AirportSystem.Domain.Entities.Aircraft;

public class Aircrafts:Auditable
{
    public string Name { get; set; }
    public int TotalNumberOfSeats { get; set; }
}
