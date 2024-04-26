using AirportSystem.Domain.Commons;

namespace AirportSystem.Domain.Entities.Users;

public class RolePermission : Auditable
{
    public long RoleId { get; set; }    
    public long PermissionId { get; set; }

    public UserRole Role { get; set; }
    public Permission Permission { get; set; }
}
