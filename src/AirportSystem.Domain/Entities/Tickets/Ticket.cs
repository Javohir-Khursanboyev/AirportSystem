using AirportSystem.Domain.Commons;

namespace AirportSystem.Domain.Entities.Tickets;

public class Ticket : Auditable
{
    public string SeatNumber { get; set; }
    public long RateTicketsId { get; set; }
    public bool IsSold { get; set; }
}
