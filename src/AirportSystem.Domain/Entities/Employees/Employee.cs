using AirportSystem.Domain.Commons;
using AirportSystem.Domain.Entities.Users;
using AirportSystem.Domain.Entities.Assets;
using AirportSystem.Domain.Entities.Flights;
using AirportSystem.Domain.Entities.Positions;

namespace AirportSystem.Domain.Entities.Employees;

public class Employee : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long PositionId { get; set; }
    public Position Position { get; set; }
    public long? AssetId { get; set; }
    public Asset Asset { get; set; }

    public IEnumerable<FlightEmployee> FlightEmployees { get; set; }
}
