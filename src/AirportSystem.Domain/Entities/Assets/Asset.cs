using AirportSystem.Domain.Commons;

namespace AirportSystem.Domain.Entities.Assets;

public class Asset : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
}
