using AirportSystem.Domain.Commons;

namespace AirportSystem.Domain.Entities.Positions;

public class Position : Auditable
{
    public string Name { get; set; }
}
