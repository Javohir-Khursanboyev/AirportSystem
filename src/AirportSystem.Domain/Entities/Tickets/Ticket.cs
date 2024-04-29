using AirportSystem.Domain.Commons;
using AirportSystem.Domain.Entities.RateTickets;

namespace AirportSystem.Domain.Entities.Tickets;

public class Ticket : Auditable
{
    public string SeatNumber { get; set; }
    public long RateTicketId { get; set; }
    public bool IsSold { get; set; }

    public RateTicket RateTicket { get; set; }
}
