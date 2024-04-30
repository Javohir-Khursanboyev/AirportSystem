using AirportSystem.Domain.Enums;
using AirportSystem.Service.DTOs.Users;
using AirportSystem.Service.DTOs.Tickets;

namespace AirportSystem.Service.DTOs.Bookings;

public class BookingViewModel
{
    public long Id { get; set; }
    public TicketViewModel Ticket { get; set; }
    public UserViewModel User { get; set; }
    public DateTime PurchaseDateTime { get; set; }
    public BookingStatus BookingStatus { get; set; }
}
