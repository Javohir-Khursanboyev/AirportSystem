using AirportSystem.Domain.Entities.Assets;

namespace AirportSystem.Service.DTOs.Employees;

public class EmployeeCreateModel
{
    public long UserId { get; set; }
    public long PositionId { get; set; }
    public long? AssetId { get; set; }
    public Asset Asset { get; set; }
}
