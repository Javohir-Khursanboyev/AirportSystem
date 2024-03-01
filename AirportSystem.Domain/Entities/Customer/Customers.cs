using AirportSystem.Domain.Commons;

namespace AirportSystem.Domain.Entities.Customer;

public class Customers : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PassportNumber { get; set; }
    public string Email {  get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public decimal Balance { get; set; }
}
