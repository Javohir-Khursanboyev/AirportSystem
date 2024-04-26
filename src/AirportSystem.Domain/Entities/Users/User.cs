using AirportSystem.Domain.Commons;

namespace AirportSystem.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public long UserRoleId { get; set; }

    public UserRole UserRole { get; set; }
}
