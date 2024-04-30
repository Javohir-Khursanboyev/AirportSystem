using AirportSystem.Service.DTOs.Users;
using AirportSystem.Service.DTOs.Assets;
using AirportSystem.Service.DTOs.Positions;

namespace AirportSystem.Service.DTOs.Employees;

public class EmployeeViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public PositionViewModel Position { get; set; }
    public AssetViewModel Asset { get; set; }
}
