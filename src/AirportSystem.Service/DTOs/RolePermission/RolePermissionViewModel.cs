using AirportSystem.Service.DTOs.UserRoles;
using AirportSystem.Service.DTOs.Permissions;

namespace AirportSystem.Service.DTOs.RolePermission;

public class RolePermissionViewModel
{
    public long Id { get; set; }
    public UserRolesViewModel UserRole { get; set; }
    public PermissionViewModel Permission { get; set; }
}
