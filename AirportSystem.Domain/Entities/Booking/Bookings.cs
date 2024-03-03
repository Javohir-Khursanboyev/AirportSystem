using AirportSystem.Domain.Commons;

namespace AirportSystem.Domain.Entities.Booking;

public class Bookings:Auditable
{
    public long TicketId { get; set; }
    public long CustomerId { get; set; }
}