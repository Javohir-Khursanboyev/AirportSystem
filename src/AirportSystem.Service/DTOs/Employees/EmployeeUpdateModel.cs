namespace AirportSystem.Service.DTOs.Employees;

public class EmployeeUpdateModel
{
    public long UserId { get; set; }
    public long PositionId { get; set; }
    public long? AssetId { get; set; }
}
